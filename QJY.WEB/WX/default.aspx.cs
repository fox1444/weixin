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

namespace QJY.WEB.WX
{
    public partial class _default : System.Web.UI.Page
    {
        string szhlcode = CommonHelp.Getszhlcode();
        string code = HttpContext.Current.Request.QueryString["code"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(code + "<br/>");
            //Response.Write(szhlcode + "<br/>");
            //Response.Write( HttpContext.Current.Request.MapPath("/"));

            UserInfoJson u = WXFWHelp.GetWXUserInfo(code);
            Response.Write(u.nickname + "<br/>" + u.sex + "<br/>" + u.province+"<br/>"+u.openid+"<br/>");
            Response.Write("<img src=\""+u.headimgurl+"\"/>");
        }

        private void WriteContent(string str)
        {
            Response.Output.Write(str);

            //string s = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx1b5c7dbfe9a3555d&redirect_uri=https://www.lgosoft.com/wx/default.aspx&response_type=code&scope=snsapi_base&state=1#wechat_redirect";

           // s = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx1b5c7dbfe9a3555d&redirect_uri=https%3A%2F%2www.lgosoft.com%2wx%2default.aspx&response_type=code&scope=snsapi_base&state=default#wechat_redirect";
        }

    }
}