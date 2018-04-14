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

namespace QJY.WEB.WX
{
    public partial class index : System.Web.UI.Page
    {
        string szhlcode = HttpContext.Current.Request.Cookies["szhlcode"].Value.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(szhlcode + "<br/>");
            JH_Auth_UserB.UserInfo UserInfo = new JH_Auth_UserB().GetUserInfo(szhlcode);
            WXFWHelp bm = new WXFWHelp(UserInfo.QYinfo);

            //CommonHelp.WriteLOG("aasad");
            //Response.Write(bm.GetToken());
            //Response.Write( HttpContext.Current.Request.MapPath("/"));
        }

        private void WriteContent(string str)
        {
            Response.Output.Write(str);
        }

    }
}