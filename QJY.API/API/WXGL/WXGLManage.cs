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
            dt = new WX_GroupB().GetDataPager("WX_Group g left join JH_Auth_User u on g.CRUser=u.UserName", " g.*, u.UserRealName ", 99999, 1, " g.CRDate desc", " g.isDel=0 and g.CRUser='" + UserInfo.User.UserName + "'", ref total);

            if (dt.Rows.Count > 0)
            {

            }
            msg.Result = dt;
            msg.Result1 = total;
        }

        public void BINDPHONE(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            JH_Auth_User j = JsonConvert.DeserializeObject<JH_Auth_User>(P1);
            if (j == null)
            {
                msg.ErrorMsg = "绑定失败";
                return;
            }
            if (string.IsNullOrWhiteSpace(j.UserRealName))
            {
                msg.ErrorMsg = "姓名不能为空";
                return;
            }
            if (string.IsNullOrWhiteSpace(j.mobphone))
            {
                msg.ErrorMsg = "手机号不能为空";
                return;
            }
            if (string.IsNullOrWhiteSpace(j.weixinCard))
            {
                msg.ErrorMsg = "微信号不能为空";
                return;
            }
            string _openid = CommonHelp.GetCookieString("openid");
            WX_User u = new WX_UserB().GetEntity(d => d.Openid == _openid);
            msg.Result = u;
            DateTime expires = DateTime.Now.AddDays(3);
            if (u != null)
            {
                JH_Auth_User localuser = new JH_Auth_UserB().GetEntity(d => d.mobphone == j.mobphone);
                if (localuser == null)
                {
                    //localuser = new JH_Auth_User();
                    //localuser.UserName = "wx" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
                    ////新用户名随机生成
                    ////localuser.UserRealName = u.Nickname;
                    //localuser.UserRealName = j.mobphone;
                    //localuser.UserPass = CommonHelp.GetMD5("a123456");
                    //localuser.pccode = EncrpytHelper.Encrypt(localuser.UserName + "@" + localuser.UserPass + "@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                    //localuser.ComId = 10334;
                    //localuser.Sex = u.Sex;
                    //localuser.mobphone = j.mobphone;
                    //localuser.BranchCode = 0;
                    //localuser.CRDate = DateTime.Now;
                    //localuser.CRUser = "administrator";
                    //localuser.logindate = DateTime.Now;
                    //localuser.IsUse = "Y";
                    //localuser.IsWX = 1;
                    //localuser.WXopenid = _openid;
                    //localuser.weixinCard = j.weixinCard;
                    //new JH_Auth_UserB().Insert(localuser);

                    //CommonHelp.SetCookie("szhlcode", localuser.pccode, expires);
                    //CommonHelp.SetCookie("username", localuser.UserName, expires);
                    msg.ErrorMsg = "手机号不存在，请联系管理员";
                    return;
                }
                else
                {
                    if (localuser.UserRealName == j.UserRealName)
                    {
                        localuser.WXopenid = _openid;
                        localuser.IsWX = 1;
                        localuser.weixinCard = j.weixinCard;
                        //localuser.pccode = EncrpytHelper.Encrypt(localuser.UserName + "@" + localuser.UserPass + "@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        localuser.logindate = DateTime.Now;
                        new JH_Auth_UserB().Update(localuser);//更新logindate  pccode不能更新

                        CommonHelp.SetCookie("szhlcode", localuser.pccode, expires);
                        CommonHelp.SetCookie("username", localuser.UserName, expires);
                 
                    }
                    else
                    {
                        msg.ErrorMsg = "姓名与手机号不匹配";
                        return;
                    }
                }
            }
            else
            {
                msg.ErrorMsg = "登录异常";
                return;
            }
        }
    }
}