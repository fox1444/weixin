using QJY.API;
using QJY.Data;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Web;

namespace QJY.WEB.WX.AUTH
{
    public partial class Redirect : System.Web.UI.Page
    {
        string code = CommonHelp.GetQueryString("code");
        protected void Page_Load(object sender, EventArgs e)
        {
            //客户零售系统的授权页面
            string redirectpage = "http://order.lstobacco.com:5222/tobacco_logist/login?from=%2F&code=" + code;
            Response.Redirect(redirectpage);
        }
    }
}