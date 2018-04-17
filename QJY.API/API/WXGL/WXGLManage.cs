using QJY.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using FastReflectionLib;
using System.Data;
using QJY.Data;
using Newtonsoft.Json;
using Senparc.Weixin.QY.Entities;
using System.Collections;

namespace QJY.API
{
    public class WXGLManage : IWsService
    {
        public void ProcessRequest(HttpContext context, ref Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            MethodInfo methodInfo = typeof(WXGLManage).GetMethod(msg.Action.ToUpper());
            WXGLManage model = new WXGLManage();
            methodInfo.FastInvoke(model, new object[] { context, msg, P1, P2, UserInfo });
        }

        public void ADDGROUP(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            WX_Group GP = JsonConvert.DeserializeObject<WX_Group>(P1);
            if (GP == null)
            {
                msg.ErrorMsg = "添加失败";
                return;
            }

            if (string.IsNullOrWhiteSpace(GP.GroupName))
            {
                msg.ErrorMsg = "小组名称不能为空";
                return;
            }

            if (GP.GroupCode == 0)
            {
                GP.CRDate = DateTime.Now;
                GP.CRUser = UserInfo.User.UserName;
                GP.IsDel = 0;
                new WX_GroupB().Insert(GP);
            }
            else
            {
                new WX_GroupB().Update(GP);
            }
            msg.Result = GP;
        }

        public void GETGROUPMODEL(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            int _GroupCode = int.Parse(P1);
            WX_Group GP = new WX_GroupB().GetEntity(d => d.GroupCode == _GroupCode);

            msg.Result = GP;
        }

        public void GETGROUPLIST(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            DataTable dt = new DataTable();
            int total = 0;
            dt = new WX_GroupB().GetDataPager("WX_Group g left join JH_Auth_User u on g.CRUser=u.UserName", " g.*, u.UserRealName ", 99999, 1, " g.CRDate desc", " g.isDel=0 and g.CRUser='"+ UserInfo.User.UserName + "'", ref total);

            if (dt.Rows.Count > 0)
            {
              
            }
            msg.Result = dt;
            msg.Result1 = total;
        }
    }
}