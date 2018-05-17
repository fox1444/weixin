using FastReflectionLib;
using System;
using System.Data;
using System.Reflection;
using System.Web;

namespace QJY.API
{
    public class ZCGLManage : IWsService
    {
        public void ProcessRequest(HttpContext context, ref Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            MethodInfo methodInfo = typeof(ZCGLManage).GetMethod(msg.Action.ToUpper());
            ZCGLManage model = new ZCGLManage();
            methodInfo.FastInvoke(model, new object[] { context, msg, P1, P2, UserInfo });
        }
        /// <summary>
        /// 资产列表
        /// </summary>
        public void GETZCGLLIST(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            string userName = UserInfo.User.UserName;
            string strWhere = " z.IsDel=0 and z.ComId=" + UserInfo.User.ComId;

            string typeid = context.Request["typeid"] ?? "";
            if (typeid != "")
            {
                strWhere += string.Format(" And z.TypeID='{0}' ", typeid);
            }
            string searchstr = context.Request["searchstr"] ?? "";
            searchstr = searchstr.TrimEnd();
            if (searchstr != "")
            {
                strWhere += string.Format(" And ( z.Name like '%{0}%' )", searchstr);
            }
            int DataID = -1;
            int.TryParse(context.Request.QueryString["ID"] ?? "-1", out DataID);//记录Id
            if (DataID != -1)
            {
                string strIsHasDataQX = new JH_Auth_QY_ModelB().ISHASDATAREADQX("ZCGL", DataID, UserInfo);
                if (strIsHasDataQX == "Y")
                {
                    strWhere += string.Format(" And z.ID = '{0}'", DataID);
                }
            }

            int page = 0;
            int pagecount = 10;
            int.TryParse(context.Request.QueryString["p"] ?? "1", out page);
            int.TryParse(context.Request.QueryString["pagecount"] ?? "10", out pagecount);//页数
            page = page == 0 ? 1 : page;
            int total = 0;

            DataTable dt = new DataTable();

            dt = new SZHL_ZCGLB().GetDataPager("SZHL_ZCGL z left join SZHL_ZCGL_Type t on z.TypeID=t.ID", "z.*,t.Title ", pagecount, page, " z.CRDate desc", strWhere, ref total);

            msg.Result = dt;
            msg.Result1 = total;

        }
        /// <summary>
        /// 所有资产类型列表
        /// </summary>
        public void GETZCGLTYPELIST(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            var list = new SZHL_ZCGL_TypeB().GetEntities(p => p.IsDel == 0);
            //DataTable dt = new SZHL_ZCGL_TypeB().GetDTByCommand("select * from dbo.SZHL_ZCGL_Type where IsDel=0 and ComId=" + UserInfo.User.ComId);
            msg.Result = list;
        }
        /// <summary>
        /// 资产类型分页列表
        /// </summary>  
        public void GETZCGLTYPELIST_PAGE(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            string strWhere = " cc.ComId=" + UserInfo.User.ComId;
            //strWhere += string.Format(" And cc.CRUser='{0}' ", UserInfo.User.UserName);
            if (P1 != "")
            {
                strWhere += string.Format(" And cc.Title like '%{0}%'", P1);
            }

            int page = 0;
            int pagecount = 10;
            int.TryParse(context.Request.QueryString["p"] ?? "1", out page);
            int.TryParse(context.Request.QueryString["pagecount"] ?? "10", out pagecount);//页数
            page = page == 0 ? 1 : page;
            int total = 0;
            DataTable dt = new SZHL_ZCGL_TypeB().GetDataPager(" SZHL_ZCGL_Type cc", "cc.*", pagecount, page, " cc.CRDate desc", strWhere, ref total);

            msg.Result = dt;
            msg.Result1 = total;
        }
        /// <summary>
        /// 资产管理
        /// </summary>
        public void GETZCGLMODEL(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            int Id = int.Parse(P1);
            string strWhere = " z.IsDel=0 and z.ComId=" + UserInfo.User.ComId + " and z.ID=" + Id;
            string colNme = @"z.*, t.Title ";
            string tableName = string.Format(@" SZHL_ZCGL z left join SZHL_ZCGL_Type t on z.TypeID=t.ID");

            string strSql = string.Format("Select {0}  From {1} where {2} order by z.CRDate desc", colNme, tableName, strWhere);
            DataTable dt = new SZHL_ZCGLB().GetDTByCommand(strSql);
            msg.Result = dt;
        }

    }
}