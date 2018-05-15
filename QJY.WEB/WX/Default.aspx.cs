using QJY.API;
using QJY.Data;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Web;

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

            bool IsZiLvXiaoZu = false;
            //授权返回
            if (code.Length > 0)
            {
                //if (szhlcode != "")
                //{

                //}
                //else
                //{
                WX_User u = WXFWHelp.GetWXUserInfo(code);
                DateTime expires = DateTime.Now.AddMinutes(30);
                JH_Auth_User userInfo = new JH_Auth_UserB().GetEntity(d => d.WXopenid == u.Openid && d.IsWX == 1);
                if (userInfo != null)//已绑定手机号和姓名
                {
                    WXFWHelp.UpdateCookieAfterSignIn(userInfo);
                    IsZiLvXiaoZu = userInfo.ZiLvXiaoZu?.Trim().Length > 0;
                }
                else//未绑定手机号和姓名
                {
                    CommonHelp.SetCookie("openid", u.Openid, expires);
                    Response.Redirect("/WX/BindPhone.html");
                }
                //}
            }
            string redirect_uri = CommonHelp.GetCookieString("pagehistory");
            if (redirect_uri.Trim().Length > 0)
            {
                Response.Redirect(HttpUtility.UrlDecode(redirect_uri));
            }
            else//默认返回页面
            {
                if(IsZiLvXiaoZu)
                    Response.Redirect("/WX/zlxz/home.html");
                else
                    Response.Redirect("/WX/bgxt/index.html");
            }
        }
    }
}