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
using System.Text;
using Senparc.Weixin.HttpUtility;

namespace QJY.WEB.WX
{
    public partial class _default : System.Web.UI.Page
    {
        string szhlcode = CommonHelp.Getszhlcode();
        string code = CommonHelp.GetQueryString("code");
        string commit = CommonHelp.GetQueryString("commit");
        protected void Page_Load(object sender, EventArgs e)
        {
            //重新跳转授权页面
            if (commit == "reauthrise")
            {
                string reauthriseurl = CommonHelp.AppConfig("Host") + "/wx/default.aspx";
                if (reauthriseurl.Trim().Length > 0)
                {
                    string authurl = OAuthApi.GetAuthorizeUrl(CommonHelp.AppConfig("AppId"), reauthriseurl, "reload", OAuthScope.snsapi_userinfo);
                    if (authurl.Length > 0)
                        Response.Redirect(authurl);
                }
            }

            //授权返回
            if (code.Length > 0)
            {
                //if (szhlcode != "")
                //{

                //}
                //else
                //{
                    WX_User u = WXFWHelp.GetWXUserInfo(code);
                    DateTime expires = DateTime.Now.AddMinutes(60);
                    JH_Auth_User userInfo = new JH_Auth_UserB().GetEntity(d => d.WXopenid == u.Openid && d.IsWX == 1);
                    if (userInfo != null)
                    {
                        CommonHelp.SetCookie("szhlcode", userInfo.pccode, expires);
                        CommonHelp.SetCookie("username", userInfo.UserName, expires);
                    }
                    else
                    {
                        CommonHelp.SetCookie("openid", u.Openid, expires);
                        Response.Redirect("/WX/BindPhone.html");
                    }
                //}
            }
            string redirect_uri = CommonHelp.GetCookieString("pagehistory");
            //if (redirect_uri.Trim().Length > 0)
            //{
            //    Response.Redirect(HttpUtility.UrlDecode(redirect_uri));
            //}
            //else
            //{
                Response.Redirect("/WX/home.html");
            //}

        }

    }
}