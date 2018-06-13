using FastReflectionLib;
using Newtonsoft.Json;
using QJY.Data;
using System;
using System.Collections.Generic;
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

        #region 资产管理
        /// <summary>
        /// 资产列表
        /// </summary>
        public void GETZCGLLIST(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            string userName = UserInfo.User.UserName;
            string strWhere = " z.IsDel=0 and z.ComId=" + UserInfo.User.ComId;

            string typeid = context.Request["typeid"] ?? "";
            string usergw = context.Request["usergw"] ?? "";
            if (typeid != "")
            {
                strWhere += string.Format(" And z.TypeID='{0}' ", typeid);
            }
            if (usergw != "")
            {
                strWhere += string.Format(" And z.UserGW='{0}' ", usergw);
            }
            string searchstr = context.Request["searchstr"] ?? "";
            searchstr = searchstr.TrimEnd();
            if (searchstr != "")
            {
                strWhere += string.Format(" And ( z.Name like '%{0}%'  or z.Code like '%{0}%')", searchstr);
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

            dt = new SZHL_ZCGLB().GetDataPager("SZHL_ZCGL z left join SZHL_ZCGL_Type t on z.TypeID=t.ID left join SZHL_ZCGL_Location l on z.LocationID=l.ID left join JH_Auth_User u on z.UserName=u.UserName left join  JH_Auth_Branch b on b.DeptCode=z.BranchCode", "z.*, u.UserRealName, t.Title, l.Title as LocTitle", pagecount, page, "b.DeptShort desc, u.UserRealName asc, z.CRDate desc", strWhere, ref total);

            msg.Result = dt;
            msg.Result1 = total;

        }
        /// <summary>
        /// 当前用户的资产列表
        /// </summary>

        public void GETZCGLLISTTHISUSER(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            string userName = UserInfo.User.UserName;
            string strWhere = " z.IsDel=0 and z.ComId=" + UserInfo.User.ComId + " and z.UserName='"+ userName + "'";

            string typeid = context.Request["typeid"] ?? "";
            if (typeid != "")
            {
                strWhere += string.Format(" And z.TypeID='{0}' ", typeid);
            }
            string searchstr = context.Request["searchstr"] ?? "";
            searchstr = searchstr.TrimEnd();
            if (searchstr != "")
            {
                strWhere += string.Format(" And ( z.Name like '%{0}%'  or z.Code like '%{0}%')", searchstr);
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

            dt = new SZHL_ZCGLB().GetDataPager("SZHL_ZCGL z left join SZHL_ZCGL_Type t on z.TypeID=t.ID left join SZHL_ZCGL_Location l on z.LocationID=l.ID left join JH_Auth_User u on z.UserName=u.UserName left join JH_Auth_Branch b on z.BranchCode=b.DeptCode", "z.*, b.DeptName,  u.UserRealName, t.Title, l.Title as LocTitle", pagecount, page, " z.CRDate desc", strWhere, ref total);

            msg.Result = dt;
            msg.Result1 = total;

        }
        /// <summary>
        /// 资产详细信息
        /// </summary>
        public void GETZCGLMODEL(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            int Id = int.Parse(P1);
            string strWhere = " z.IsDel=0  and z.ID=" + Id;
            string colNme = @"z.*, t.Title, l.Title as LocTitle, b.DeptName,u.UserRealName ";
            string tableName = string.Format(@" SZHL_ZCGL z left join SZHL_ZCGL_Type t on z.TypeID=t.ID left join SZHL_ZCGL_Location l on z.LocationID=l.ID left join JH_Auth_Branch b on z.BranchCode=b.DeptCode left join JH_Auth_User u on z.UserName=u.UserName");

            string strSql = string.Format("Select {0}  From {1} where {2} order by z.CRDate desc", colNme, tableName, strWhere);
            DataTable dt = new SZHL_ZCGLB().GetDTByCommand(strSql);
            msg.Result = dt;
        }
        /// <summary>
        /// 添加资产
        /// </summary>
        public void ADDZCGL(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            SZHL_ZCGL ZC = JsonConvert.DeserializeObject<SZHL_ZCGL>(P1);

            if (ZC == null)
            {
                msg.ErrorMsg = "添加失败";
                return;
            }
            if (string.IsNullOrWhiteSpace(ZC.Name))
            {
                msg.ErrorMsg = "名称不能为空！";
                return;
            }
            if (ZC.TypeID <= 0)
            {
                msg.ErrorMsg = "请选择资产类型！";
                return;
            }
            if (ZC.Status < 0)
            {
                msg.ErrorMsg = "请选择物品状态！";
                return;
            }
            if (ZC.Qty <= 0)
            {
                msg.ErrorMsg = "请输入数量！";
                return;
            }
            if (ZC.ID == 0)
            {
                ZC.CRDate = DateTime.Now;
                ZC.CRUser = UserInfo.User.UserName;
                ZC.ComId = UserInfo.User.ComId.Value;
                ZC.IsDel = 0;
                new SZHL_ZCGLB().Insert(ZC);
            }
            else
            {
                new SZHL_ZCGLB().Update(ZC);
            }
            msg.Result = ZC;
        }
        /// <summary>
        /// 删除资产
        /// </summary>
        public void DELZCGLMODEL(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            int Id = int.Parse(P1);
            SZHL_ZCGL model = new SZHL_ZCGLB().GetEntity(d => d.ID == Id && d.ComId == UserInfo.User.ComId);
            model.IsDel = 1;
            new SZHL_ZCGLB().Update(model);

        }
        #endregion

        #region 资产类型管理
        /// <summary>
        /// 所有资产类型列表
        /// </summary>
        public void GETZCGLTYPELIST(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            //var list = new SZHL_ZCGL_TypeB().GetEntities(p => p.IsDel == 0);
            DataTable dt = new SZHL_ZCGL_TypeB().GetDTByCommand("select * from dbo.SZHL_ZCGL_Type where IsDel=0 and ComId=" + UserInfo.User.ComId + " order by DisplayOrder");
            msg.Result = dt;
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
            DataTable dt = new SZHL_ZCGL_TypeB().GetDataPager(" SZHL_ZCGL_Type cc", "cc.*", pagecount, page, " cc.DisplayOrder", strWhere, ref total);

            msg.Result = dt;
            msg.Result1 = total;
        }

        /// <summary>
        /// 资产类型详细信息
        /// </summary>
        public void GETTYPEMODEL(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            int Id = int.Parse(P1);
            SZHL_ZCGL_Type model = new SZHL_ZCGL_TypeB().GetEntity(d => d.ID == Id && d.ComId == UserInfo.User.ComId);
            msg.Result = model;
        }
        /// <summary>
        /// 添加资产类型
        /// </summary>
        public void ADDTYPE(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            SZHL_ZCGL_Type t = JsonConvert.DeserializeObject<SZHL_ZCGL_Type>(P1);

            if (string.IsNullOrEmpty(t.Title))
            {
                msg.ErrorMsg = "类型名称不能为空!";
            }

            if (string.IsNullOrEmpty(msg.ErrorMsg))
            {
                if (t.ID == 0)
                {
                    var t1 = new SZHL_ZCGL_TypeB().GetEntity(p => p.ComId == UserInfo.User.ComId && p.Title == t.Title);
                    if (t1 != null)
                    {
                        msg.ErrorMsg = "系统已经存在此类型名称!";
                    }
                    else
                    {
                        t.CRDate = DateTime.Now;
                        t.CRUser = UserInfo.User.UserName;
                        t.ComId = Convert.ToInt16(UserInfo.User.ComId);
                        t.IsDel = 0;
                        new SZHL_ZCGL_TypeB().Insert(t);
                        msg.Result = t;
                    }
                }
                else
                {
                    var hys1 = new SZHL_ZCGL_TypeB().GetEntity(p => p.ComId == UserInfo.User.ComId && p.Title == t.Title && p.ID != t.ID);
                    if (hys1 != null)
                    {
                        msg.ErrorMsg = "系统已经存在此类型名称";
                    }
                    else
                    {
                        new SZHL_ZCGL_TypeB().Update(t);
                        msg.Result = t;
                    }
                }

            }
        }
        /// <summary>
        /// 删除资产类型
        /// </summary>
        public void DELZCGLTYPE(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            int Id = int.Parse(P1);
            int ss = int.Parse(P2);
            SZHL_ZCGL_Type model = new SZHL_ZCGL_TypeB().GetEntity(d => d.ID == Id && d.ComId == UserInfo.User.ComId);
            model.IsDel = ss;
            new SZHL_ZCGL_TypeB().Update(model);

        }
        #endregion

        #region 资产场地管理
        /// <summary>
        /// 所有资产类型列表
        /// </summary>
        public void GETZCGLLOCATIONLIST(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            //var list = new SZHL_ZCGL_TypeB().GetEntities(p => p.IsDel == 0);
            DataTable dt = new SZHL_ZCGL_TypeB().GetDTByCommand("select * from dbo.SZHL_ZCGL_LOCATION where IsDel=0 and ComId=" + UserInfo.User.ComId + " order by DisplayOrder");
            msg.Result = dt;
        }
        /// <summary>
        /// 资产类型分页列表
        /// </summary>  
        public void GETZCGLLOCATIONLIST_PAGE(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
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
            DataTable dt = new SZHL_ZCGL_TypeB().GetDataPager(" SZHL_ZCGL_LOCATION cc", "cc.*", pagecount, page, " cc.DisplayOrder", strWhere, ref total);

            msg.Result = dt;
            msg.Result1 = total;
        }

        /// <summary>
        /// 资产类型详细信息
        /// </summary>
        public void GETLOCATIONMODEL(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            int Id = int.Parse(P1);
            SZHL_ZCGL_Location model = new SZHL_ZCGL_LocationB().GetEntity(d => d.ID == Id && d.ComId == UserInfo.User.ComId);
            msg.Result = model;
        }
        /// <summary>
        /// 添加资产类型
        /// </summary>
        public void ADDLOCATION(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            SZHL_ZCGL_Location t = JsonConvert.DeserializeObject<SZHL_ZCGL_Location>(P1);

            if (string.IsNullOrEmpty(t.Title))
            {
                msg.ErrorMsg = "类型名称不能为空!";
            }

            if (string.IsNullOrEmpty(msg.ErrorMsg))
            {
                if (t.ID == 0)
                {
                    var t1 = new SZHL_ZCGL_LocationB().GetEntity(p => p.ComId == UserInfo.User.ComId && p.Title == t.Title);
                    if (t1 != null)
                    {
                        msg.ErrorMsg = "系统已经存在此类型名称!";
                    }
                    else
                    {
                        t.CRDate = DateTime.Now;
                        t.CRUser = UserInfo.User.UserName;
                        t.ComId = Convert.ToInt16(UserInfo.User.ComId);
                        t.IsDel = 0;
                        new SZHL_ZCGL_LocationB().Insert(t);
                        msg.Result = t;
                    }
                }
                else
                {
                    var hys1 = new SZHL_ZCGL_LocationB().GetEntity(p => p.ComId == UserInfo.User.ComId && p.Title == t.Title && p.ID != t.ID);
                    if (hys1 != null)
                    {
                        msg.ErrorMsg = "系统已经存在此类型名称";
                    }
                    else
                    {
                        new SZHL_ZCGL_LocationB().Update(t);
                        msg.Result = t;
                    }
                }

            }
        }
        /// <summary>
        /// 删除资产类型
        /// </summary>
        public void DELZCGLLOCATION(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            int Id = int.Parse(P1);
            int ss = int.Parse(P2);
            SZHL_ZCGL_Location model = new SZHL_ZCGL_LocationB().GetEntity(d => d.ID == Id && d.ComId == UserInfo.User.ComId);
            model.IsDel = ss;
            new SZHL_ZCGL_LocationB().Update(model);

        }
        #endregion
    }
}