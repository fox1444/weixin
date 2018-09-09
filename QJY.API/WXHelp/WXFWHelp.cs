using QJY.Data;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;
using System;
using System.Threading.Tasks;
using System.Web;

namespace QJY.API
{
    /// <summary>
    /// 微信公众号服务类
    /// </summary>
    public class WXFWHelp
    {
        public WXFWHelp()
        {
        }
        /// <summary>
        /// Global中定时更新AccessToken
        /// </summary>
        public static void UpdateTokenByTimer()
        {
            string strIp = CommonHelp.getIP(HttpContext.Current);
            try
            {
                string acc = GetTokenAsync(true);
                if (acc.Length > 0)
                {
                }
                else
                {
                    new JH_Auth_LogB().InsertLog("Application_Start", "更新Access为空", "Global.asax", "System", "www.lstobacco.com", 0, strIp);
                }
            }
            catch (Exception ex)
            {
                new JH_Auth_LogB().InsertLog("Application_Start", "更新Access错误" + ex.ToString(), "Global.asax", "System", "www.lstobacco.com", 0, strIp);
            }
        }
        /// <summary>
        /// 立即更新AccessToken
        /// </summary>
        public static string GetToken()
        {
            AccessTokenResult r = CommonApi.GetToken(CommonHelp.AppConfig("AppId"), CommonHelp.AppConfig("AppSecret"), "client_credential");
            string _username = CommonHelp.GetUserNameByszhlcode();

            string accesstoken = r.access_token;
            if (accesstoken.Trim().Length > 0)
            {
                CommonHelp.UpdateAppConfig("AccessToken", accesstoken);
                new JH_Auth_LogB().InsertLog("WXFWHelper", "立即更新AccessToken为" + accesstoken, "WXFWHelper", _username, _username, 0, "");
            }
            return accesstoken;
        }
        /// <summary>
        /// 异步更新AccessToken
        /// </summary>
        /// <param name="getNewToken"></param>
        public static string GetTokenAsync(bool getNewToken = false)
        {
            //AccessTokenResult r = CommonApi.GetToken(Qyinfo.corpId, Qyinfo.corpSecret, "client_credential");
            string strIp = CommonHelp.getIP(HttpContext.Current);
            string _username = CommonHelp.GetUserNameByszhlcode();
            var task1 = new Task<string>(() =>
            AccessTokenContainer.TryGetAccessTokenAsync(CommonHelp.AppConfig("AppId"), CommonHelp.AppConfig("AppSecret"), getNewToken).Result
            );

            task1.Start();

            string accesstoken = task1.Result;
            if (accesstoken.Trim().Length > 0)
            {
                CommonHelp.UpdateAppConfig("AccessToken", accesstoken);
                new JH_Auth_LogB().InsertLog("WXFWHelper", "更新AccessToken为" + accesstoken, "WXFWHelper", _username, _username, 0, strIp);

                JsApiTicketResult jsapi_ticket = CommonApi.GetTicketByAccessToken(accesstoken);
                if (jsapi_ticket != null)
                {
                    if (jsapi_ticket.ticket.Length > 0)
                    {
                        CommonHelp.UpdateAppConfig("jsapi_ticket", jsapi_ticket.ticket);
                        new JH_Auth_LogB().InsertLog("WXFWHelper", "更新jsapi_ticket为" + jsapi_ticket.ticket, "WXFWHelper", _username, _username, 0, strIp);

                    }
                }
            }
            return accesstoken;
        }
        /// <summary>
        /// 从公众号中进入获取用户信息并更新数据库中账号
        /// </summary>
        public static WX_User GetWXUserInfo(string _code)
        {
            try
            {
                OAuthAccessTokenResult _accresstoken = OAuthApi.GetAccessToken(CommonHelp.AppConfig("AppId"), CommonHelp.AppConfig("AppSecret"), _code);
                //return GetUserInfoByOpenidWithUpdateLocal(_accresstoken.openid);
                return GetUserInfoByOpenidWithUpdateLocal(_accresstoken.openid, _accresstoken, _code);
            }
            catch
            {

            }

            return null;
        }
        /// <summary>
        /// 根据公众号openid获取用户信息
        /// </summary>
        public static UserInfoJson GetUserInfoByOpenid(string openid)
        {
            return UserApi.Info(CommonHelp.GetAccessToken(), openid);
        }
        /// <summary>
        /// 直接更新本地数据库中账号 
        /// </summary>
        public static WX_User GetUserInfoByOpenidWithUpdateLocal(string openid, OAuthAccessTokenResult _accresstoken, string _code)
        {
            UserInfoJson u = GetUserInfoByOpenid(openid);
            WX_User wxuser = new WX_User();
            if (u != null && u.errmsg == null)//有用户信息返回
            {
                wxuser = new WX_UserB().GetEntity(d => d.Openid == u.openid);
                return UpdateLocalUserInfo(u, _accresstoken, _code);
            }
            return wxuser;
        }
        /// <summary>
        /// 更新本地数据库
        /// </summary>
        public static WX_User UpdateLocalUserInfo(UserInfoJson u, OAuthAccessTokenResult _accresstoken, string _code)
        {
            string _accesstokenstr = "";
            int _expires_in = 0;
            string _refreshtokenstr = "";
            string _scopestr = "";
            if (_accresstoken != null && _accresstoken.errmsg == null)
            {
                _accesstokenstr = _accresstoken.access_token;
                _expires_in = _accresstoken.expires_in;
                _refreshtokenstr = _accresstoken.refresh_token;
                _scopestr = _accresstoken.scope;
            }
            WX_User wxuser = new WX_UserB().GetEntity(d => d.Openid == u.openid);
            if (wxuser == null)
            {
                wxuser = new WX_User();
                wxuser.Openid = u.openid;
                wxuser.Nickname = u.nickname;
                wxuser.Sex = u.sex == 1 ? "男" : (u.sex == 2 ? "女" : "未知");
                wxuser.Province = u.province;
                wxuser.City = u.city;
                wxuser.Country = u.country;
                wxuser.HeadImgUrl = u.headimgurl;
                wxuser.Code = _code;
                wxuser.Access_token = _accesstokenstr;
                wxuser.Expires_in = _expires_in;
                wxuser.Refresh_token = _refreshtokenstr;
                wxuser.Scope = _scopestr;
                wxuser.CRDate = DateTime.Now;
                wxuser.LastLoginDate = DateTime.Now;
                wxuser.AuthUserID = 0;
                new WX_UserB().Insert(wxuser);
            }
            else
            {
                wxuser.Nickname = u.nickname;
                wxuser.Sex = u.sex == 1 ? "男" : (u.sex == 2 ? "女" : "未知");
                wxuser.Province = u.province;
                wxuser.City = u.city;
                wxuser.Country = u.country;
                wxuser.HeadImgUrl = u.headimgurl;
                wxuser.Code = _code;
                wxuser.Access_token = _accesstokenstr;
                wxuser.Expires_in = _expires_in;
                wxuser.Refresh_token = _refreshtokenstr;
                wxuser.Scope = _scopestr;
                wxuser.LastLoginDate = DateTime.Now;
                new WX_UserB().Update(wxuser);
            }
            return wxuser;
            //JH_Auth_User localuser = new JH_Auth_UserB().GetEntity(d => d.WXopenid == u.openid && d.IsWX == 1);
            //if (localuser == null)
            //{
            //    localuser = new JH_Auth_User();

            //    localuser.UserName = "wx" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
            //    //新用户名随机生成
            //    localuser.UserRealName = u.nickname;
            //    localuser.UserPass = CommonHelp.GetMD5("a123456");
            //    localuser.pccode= EncrpytHelper.Encrypt(localuser.UserName + "@" + localuser.UserPass + "@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            //    localuser.ComId = 10334;
            //    localuser.Sex = u.sex == 1 ? "男" : (u.sex == 2 ? "女" : "未知");
            //    localuser.BranchCode = 0;
            //    localuser.CRDate = DateTime.Now;
            //    localuser.CRUser = "administrator";
            //    localuser.logindate = DateTime.Now;
            //    localuser.IsUse = "Y";
            //    localuser.IsWX = 1;
            //    localuser.WXopenid = u.openid;
            //    new JH_Auth_UserB().Insert(localuser);
            //}
            //else
            //{
            //    //localuser.pccode = EncrpytHelper.Encrypt(localuser.UserName + "@" + localuser.UserPass + "@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            //    localuser.logindate = DateTime.Now;
            //    new JH_Auth_UserB().Update(localuser);//更新logindate  pccode不能更新
            //}
        }
        /// <summary>
        /// 微信授权登录成功后更新本地账号缓存
        /// </summary>
        public static void UpdateCookieAfterSignIn(JH_Auth_User userInfo)
        {
            DateTime expires = DateTime.Now.AddMinutes(30);
            CommonHelp.SetCookie("szhlcode", userInfo.pccode, expires);
            CommonHelp.SetCookie("username", userInfo.UserName, expires);
            CommonHelp.SetCookie("userphonenumber", userInfo.mobphone, expires);
        }
    }
}
