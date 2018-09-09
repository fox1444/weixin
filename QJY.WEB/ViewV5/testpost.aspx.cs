using QJY.API;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing;

namespace QJY.WEB.ViewV5
{
    public partial class testpost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                byte[] imgBytes = Convert.FromBase64String(Request.Form["img"]);
                Stream stream = new MemoryStream(imgBytes);
                ZXing.Result result = new BarcodeReader().Decode(new Bitmap(stream));
                Response.Write(Uri.EscapeDataString(result.Text));
            }
            catch { Response.Write("no"); }
        }

    }
}