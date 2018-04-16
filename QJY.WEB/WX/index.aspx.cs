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
        protected void Page_Load(object sender, EventArgs e)
        {
            
          
            WXFWHelp bm = new WXFWHelp();
            //CommonHelp.WriteLOG("aasad");
            Response.Write(bm.GetToken());
            //Response.Write( HttpContext.Current.Request.MapPath("/"));
        }

        private void WriteContent(string str)
        {
            Response.Output.Write(str);
        }

    }
}