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

        public string GetTokenAsync(bool getNewToken = false)
        {
            //AccessTokenResult r = CommonApi.GetToken(Qyinfo.corpId, Qyinfo.corpSecret, "client_credential");

            string _username = CommonHelp.GetUserNameByszhlcode();
            var task1 = new Task<string>(() =>
            AccessTokenContainer.TryGetAccessTokenAsync(CommonHelp.AppConfig("AppId"), CommonHelp.AppConfig("AppSecret"), getNewToken).Result
            );

            task1.Start();

            string accesstoken = task1.Result;
            if (accesstoken.Trim().Length > 0)
            {
                CommonHelp.UpdateAppConfig("AccessToken", accesstoken);
                new JH_Auth_LogB().InsertLog("WXFWHelper", "更新AccessToken为" + accesstoken, "WXFWHelper", _username, _username, 0, "");
            }
            return accesstoken;
        }


        //从公众号中进入获取用户信息并更新数据库中账号
        public static UserInfoJson GetWXUserInfo(string _code)
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

        public static UserInfoJson GetUserInfoByOpenid(string openid)
        {
            return UserApi.Info(CommonHelp.GetAccessToken(), openid);
        }

        //直接更新本地数据库中账号
        public static UserInfoJson GetUserInfoByOpenidWithUpdateLocal(string openid)
        {
            UserInfoJson u = GetUserInfoByOpenid(openid);
            if (u != null && u.errmsg == null)//有用户信息返回
            {
                UpdateLocalUserInfo(u);
                return u;
            }
            return u;
        }

        public static UserInfoJson GetUserInfoByOpenidWithUpdateLocal(string openid, OAuthAccessTokenResult _accresstoken, string _code)
        {
            UserInfoJson u = GetUserInfoByOpenid(openid);
            if (u != null && u.errmsg == null)//有用户信息返回
            {
                UpdateLocalUserInfo(u, _accresstoken, _code);
                return u;
            }
            return u;
        }

        public static void UpdateLocalUserInfo(UserInfoJson u)
        {
            UpdateLocalUserInfo(u, null, "");
        }

        public static void UpdateLocalUserInfo(UserInfoJson u, OAuthAccessTokenResult _accresstoken, string _code)
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

            JH_Auth_User localuser = new JH_Auth_UserB().GetEntity(d => d.WXopenid == u.openid && d.IsWX == 1);
            if (localuser == null)
            {
                localuser = new JH_Auth_User();

                localuser.UserName = "wx" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
                //新用户名随机生成
                localuser.UserRealName = u.nickname;
                localuser.UserPass = CommonHelp.GetMD5("a123456");
                localuser.pccode= EncrpytHelper.Encrypt(localuser.UserName + "@" + localuser.UserPass + "@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                localuser.ComId = 10334;
                localuser.Sex = u.sex == 1 ? "男" : (u.sex == 2 ? "女" : "未知");
                localuser.BranchCode = 0;
                localuser.CRDate = DateTime.Now;
                localuser.CRUser = "administrator";
                localuser.logindate = DateTime.Now;
                localuser.IsUse = "Y";
                localuser.IsWX = 1;
                localuser.WXopenid = u.openid;
                new JH_Auth_UserB().Insert(localuser);
            }
        }
    }
}
