using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using QJY.API;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.MP;
using Senparc.Weixin.Containers;
using Senparc.Weixin.MP.Containers;

namespace QJY.WEB
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval = (CommonHelp.AppConfigInt("AccessTokenExpireTime") * 60 * 1000);
            t.Elapsed += new System.Timers.ElapsedEventHandler(TimerNow);
            t.AutoReset = true;
            t.Enabled = true;
            t.Start();


        }

        public void TimerNow(object source, System.Timers.ElapsedEventArgs e)
        {
            string strIp = CommonHelp.getIP(HttpContext.Current);
            new JH_Auth_LogB().InsertLog("Application_Start", "Application启动", "Global.asax", "System", "System", 0, "");
            try
            {
                //Task<string> task1 = new Task<string>(() =>
                //  TryGetAccessTokenAsync("wx1b5c7dbfe9a3555d", "c37f667f8026820e34ff0a6a19e4033d", false).Result
                //);

                //task1.Start();
                //JH_Auth_UserB.UserInfo UserInfo = new JH_Auth_UserB.UserInfo();
                WXFWHelp bm = new WXFWHelp();

                string acc = bm.GetTokenAsync(true);
                if (acc.Length > 0)
                {
                }
                else
                {
                    new JH_Auth_LogB().InsertLog("Application_Start", "更新Access为空", "Global.asax", "System", "System", 0, strIp);
                }
            }
            catch (Exception ex)
            {
                new JH_Auth_LogB().InsertLog("Application_Start", "更新Access错误" + ex.ToString(), "Global.asax", "System", "System", 0, strIp);
            }
            //string path = Environment.CurrentDirectory;
            //try
            //{
            //    Random rd = new Random();
            //    string strUrl = CommonHelp.GetConfig("APITX") + "&r=" + rd.Next();
            //    HttpWebResponse ResponseDataXS = CommonHelp.CreateHttpResponse(strUrl, null, 0, "", null, "GET");
            //    string Returndata = new StreamReader(ResponseDataXS.GetResponseStream(), Encoding.UTF8).ReadToEnd();

            //}
            //catch (Exception ex)
            //{

            //    CommonHelp.WriteLOG(ex.Message.ToString());

            //}
        }
        protected void Session_Start(object sender, EventArgs e)
        {

        }



        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            // 在应用程序关闭时运行的代码 
            //解决应用池回收问题 
            //System.Threading.Thread.Sleep(5000);
            //Random rd = new Random();
            //string strUrl = CommonHelp.GetConfig("APITX") + "&r=" + rd.Next();
            //HttpWebResponse ResponseDataXS = CommonHelp.CreateHttpResponse(strUrl, null, 0, "", null, "GET");
            //string Returndata = new StreamReader(ResponseDataXS.GetResponseStream(), Encoding.UTF8).ReadToEnd();

            //更新AccessToken
            //JH_Auth_UserB.UserInfo UserInfo = new JH_Auth_UserB.UserInfo();
            //WXFWHelp bm = new WXFWHelp(UserInfo.QYinfo);

            //string acc = bm.GetToken("", true);
            string strIp = CommonHelp.getIP(HttpContext.Current);
            new JH_Auth_LogB().InsertLog("Application_End", "Application关闭", "Global.asax", "System", "System", 0, strIp);

            try
            {
                WXFWHelp bm = new WXFWHelp();

                string acc = bm.GetTokenAsync(true);
                if (acc.Length > 0)
                {
                }
                else
                {
                    new JH_Auth_LogB().InsertLog("Application_End", "更新Access为空", "Global.asax", "System", "System", 0, strIp);
                }
            }
            catch (Exception ex)
            {
                new JH_Auth_LogB().InsertLog("Application_End", "更新Access错误" + ex.ToString(), "Global.asax", "System", "System", 0, strIp);
            }
        }



    }
}