using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using QJY.API;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using QJY.Data;

namespace QJY.WEB.WX
{
    public partial class _default : System.Web.UI.Page
    {
        string szhlcode = CommonHelp.Getszhlcode();
        string code = CommonHelp.GetQueryString("code");
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(code + "<br/>");
            //Response.Write(szhlcode + "<br/>");
            //Response.Write( HttpContext.Current.Request.MapPath("/"));

            if (szhlcode != "")
            {

            }
            else
            {
                UserInfoJson u = WXFWHelp.GetWXUserInfo(code);

                JH_Auth_User userInfo = new JH_Auth_UserB().GetEntity(d => d.WXopenid == u.openid && d.IsWX == 1);
                if (userInfo != null)
                {
                    DateTime expires = DateTime.Now.AddDays(3);
                    CommonHelp.SetCookie("szhlcode", userInfo.pccode, expires);
                    CommonHelp.SetCookie("username", userInfo.UserName, expires);
                }
            }
            Response.Redirect("/WX/me.html");
            //OpenIdResultJson urs = UserApi.Get(CommonHelp.AppConfig("AccessToken"), "");

            //foreach (var i in urs.data.openid)
            //{
            //    UserInfoJson openu = WXFWHelp.GetUserInfoByOpenidWithUpdateLocal(i);
            //    Response.Write("<img src=\"" + openu.headimgurl + "\"/>" + openu.nickname + " <br/>");
            //}

        }

    }
}