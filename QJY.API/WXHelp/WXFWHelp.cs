using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Senparc.Weixin.Entities;
using Senparc.Weixin.MP;
using Senparc.Weixin;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.CommonAPIs;
using QJY.Data;
using Senparc.Weixin.MP.Containers;
using System.Web;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace QJY.API
{
    public class WXFWHelp
    {

        string szhlcode = HttpContext.Current.Request.Cookies["szhlcode"].Value.ToString();

        public WXFWHelp()
        {
        }

        public string GetToken()
        {
            AccessTokenResult r = CommonApi.GetToken(CommonHelp.AppConfig("AppId"), CommonHelp.AppConfig("AppSecret"), "client_credential");
            string _username = CommonHelp.GetUserNameByszhlcode();

            string accesstoken = r.access_token;
            if (accesstoken.Trim().Length > 0)
            {
                CommonHelp.UpdateAppConfig("AccessToken", accesstoken);
                new JH_Auth_LogB().InsertLog("WXFWHelper", "更新AccessToken为" + accesstoken, "WXFWHelper", _username, _username, 0, "");
            }
            return accesstoken;
        }

        public string GetTokenAsync(string appID = "" , bool getNewToken = false)
        {
            //AccessTokenResult r = CommonApi.GetToken(Qyinfo.corpId, Qyinfo.corpSecret, "client_credential");

            string _username = CommonHelp.GetUserNameByszhlcode();
            var task1 = new Task<string>(() => 
            AccessTokenContainer.TryGetAccessTokenAsync(CommonHelp.AppConfig("AppId"), CommonHelp.AppConfig("AppSecret"), getNewToken).Result
            );

            task1.Start();

            string accesstoken = task1.Result;
            if (accesstoken.Trim().Length>0)
            {
                CommonHelp.UpdateAppConfig("AccessToken", accesstoken);
                new JH_Auth_LogB().InsertLog("WXFWHelper", "更新AccessToken为" + accesstoken, "WXFWHelper", _username, _username, 0, "");
            }
            return accesstoken;
        }

        public static UserInfoJson GetWXUserInfo(string _code)
        {
            try
            {
                OAuthAccessTokenResult _accresstoken = OAuthApi.GetAccessToken(CommonHelp.AppConfig("AppId"), CommonHelp.AppConfig("AppSecret"), _code);
                UserInfoJson u = UserApi.Info(CommonHelp.GetAccessToken(), _accresstoken.openid);
                return u;
            }
            catch
            {

            }

            return null;
        }

    }
}
