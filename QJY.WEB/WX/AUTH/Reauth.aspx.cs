using QJY.API;
using QJY.Data;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Web;

namespace QJY.WEB.WX.AUTH
{
    public partial class Reauth : System.Web.UI.Page
    {
        string redirecturl = CommonHelp.GetQueryString("redirecturl");
        protected void Page_Load(object sender, EventArgs e)
        {
            //客户零售系统的授权页面
            string reauthriseurl = CommonHelp.GetConfig("HostUrl") + "/wx/auth/redirect.aspx";
            //if (!string.IsNullOrWhiteSpace(redirecturl))
            //{
                //reauthriseurl = HttpUtility.UrlDecode(redirecturl);
                //Response.Write(reauthriseurl + "<br/>");
            //}
            if (reauthriseurl.Trim().Length > 0)
            {
                string authurl = OAuthApi.GetAuthorizeUrl(CommonHelp.GetConfig("AppId"), reauthriseurl, "reload", OAuthScope.snsapi_userinfo);
                if (authurl.Length > 0)
                {
                    Response.Redirect(HttpUtility.UrlDecode(authurl));
                }
            }
        }
    }
}