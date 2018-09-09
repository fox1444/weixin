using QJY.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QJY.WEB.ViewV5
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            string Mobiles = "13980444473,18282844528";
            string Content = "刘主任，凉山烟叶产业扶贫座谈会暨产销衔接会参会人员已更新，会议链接http://www.lstobacco.com/hy.html?id=271，如有打扰，请谅解！";
            string result = CommonHelp.SendMAS(Mobiles, Content);
            Response.Write(result);
        }
    }
}