﻿using QJY.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using FastReflectionLib;
using System.Data;
using QJY.Data;
using Newtonsoft.Json;
using Senparc.Weixin.QY.Entities;
using System.Collections;

namespace QJY.API
{
    public class WXGLManage : IWsService
    {
        public void ProcessRequest(HttpContext context, ref Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            MethodInfo methodInfo = typeof(WXGLManage).GetMethod(msg.Action.ToUpper());
            WXGLManage model = new WXGLManage();
            methodInfo.FastInvoke(model, new object[] { context, msg, P1, P2, UserInfo });
        }

        public void ADDGROUP(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            WX_Group GP = JsonConvert.DeserializeObject<WX_Group>(P1);
            if (GP == null)
            {
                msg.ErrorMsg = "添加失败";
                return;
            }

            if (string.IsNullOrWhiteSpace(GP.GroupName))
            {
                msg.ErrorMsg = "小组名称不能为空";
                return;
            }

            if (GP.GroupCode == 0)
            {
                GP.CRDate = DateTime.Now;
                GP.CRUser = UserInfo.User.UserName;
                GP.IsDel = 0;
                new WX_GroupB().Insert(GP);
            }
            else
            {
                new WX_GroupB().Update(GP);
            }
            msg.Result = GP;
        }
        public void GETGROUPMODEL(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            int _GroupCode = int.Parse(P1);
            WX_Group GP = new WX_GroupB().GetEntity(d => d.GroupCode == _GroupCode);

            msg.Result = GP;
        }
        public void GETGROUPLIST(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            DataTable dt = new DataTable();
            int total = 0;
            dt = new WX_GroupB().GetDataPager("WX_Group g left join JH_Auth_User u on g.CRUser=u.UserName", " g.*, u.UserRealName ", 99999, 1, " g.CRDate desc", " g.isDel=0 and g.CRUser='" + UserInfo.User.UserName + "'", ref total);

            if (dt.Rows.Count > 0)
            {

            }
            msg.Result = dt;
            msg.Result1 = total;
        }
        public void GETMYGROUPTEAM(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            //我的自律小组名称
            JH_Auth_User ext = new JH_Auth_UserB().GetEntity(d => d.ID == UserInfo.User.ID);

            string viewname = "select U.* , wu.nickName from JH_Auth_User U LEFT JOIN WX_User wu on u.WxOpenid = wu.openid  " +
                "where u.ZiLvXiaoZu = '" + ext.ZiLvXiaoZu + "' " +
                "or  ('" + ext.ZiLvXiaoZu + "' like (select REPLACE(REPLACE(REPLACE(REPLACE(Items,' ',''),CHAR(10) ,''),CHAR(13),''),'/n','') from dbo.Split(u.jianduxiaozu,';') where items='" + ext.ZiLvXiaoZu + "'))" +
                " order by u.UserOrder, u.IsZuZhang desc, u.UserRealName asc";

            // DataTable dt = new JH_Auth_UserB().GetDataPager(viewname, " U.* , wu.nickName, ext.ExtendDataValue as XiaoZu, ext2.ExtendDataValue as IsZuZhang ", 999999, 1, " u.UserRealName asc ", strWhere, ref recordCount);
            DataTable dt = new JH_Auth_UserB().GetDTByCommand(viewname);

            msg.Result = dt;
            msg.Result1 = ext.ZiLvXiaoZu;
        }
        public void BINDPHONE(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            JH_Auth_User j = JsonConvert.DeserializeObject<JH_Auth_User>(P1);
            if (j == null)
            {
                msg.ErrorMsg = "绑定失败";
                return;
            }
            if (string.IsNullOrWhiteSpace(j.UserRealName))
            {
                msg.ErrorMsg = "姓名不能为空";
                return;
            }
            if (string.IsNullOrWhiteSpace(j.mobphone))
            {
                msg.ErrorMsg = "手机号不能为空";
                return;
            }
            //if (string.IsNullOrWhiteSpace(j.weixinCard))
            //{
            //    msg.ErrorMsg = "微信号不能为空";
            //    return;
            //}
            string _openid = CommonHelp.GetCookieString("openid");
            WX_User u = new WX_UserB().GetEntity(d => d.Openid == _openid);
            msg.Result = u;
            DateTime expires = DateTime.Now.AddMinutes(60);
            if (u != null)
            {
                JH_Auth_User localuser = new JH_Auth_UserB().GetEntity(d => d.mobphone == j.mobphone);
                if (localuser == null)
                {
                    //localuser = new JH_Auth_User();
                    //localuser.UserName = "wx" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
                    ////新用户名随机生成
                    ////localuser.UserRealName = u.Nickname;
                    //localuser.UserRealName = j.mobphone;
                    //localuser.UserPass = CommonHelp.GetMD5("a123456");
                    //localuser.pccode = EncrpytHelper.Encrypt(localuser.UserName + "@" + localuser.UserPass + "@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                    //localuser.ComId = 10334;
                    //localuser.Sex = u.Sex;
                    //localuser.mobphone = j.mobphone;
                    //localuser.BranchCode = 0;
                    //localuser.CRDate = DateTime.Now;
                    //localuser.CRUser = "administrator";
                    //localuser.logindate = DateTime.Now;
                    //localuser.IsUse = "Y";
                    //localuser.IsWX = 1;
                    //localuser.WXopenid = _openid;
                    //localuser.weixinCard = j.weixinCard;
                    //new JH_Auth_UserB().Insert(localuser);

                    //CommonHelp.SetCookie("szhlcode", localuser.pccode, expires);
                    //CommonHelp.SetCookie("username", localuser.UserName, expires);
                    msg.ErrorMsg = "手机号不存在，请联系管理员";
                    return;
                }
                else
                {
                    if (localuser.UserRealName == j.UserRealName)
                    {
                        new JH_Auth_UserB().ExsSql("update JH_Auth_User set WXopenid='', IsWX=0  where WXopenid='" + _openid + "'");//清除以前绑定的用户

                        localuser.WXopenid = _openid;
                        localuser.IsWX = 1;
                        localuser.weixinCard = j.weixinCard;
                        localuser.pccode = EncrpytHelper.Encrypt(localuser.UserName + "@" + localuser.UserPass + "@" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        localuser.logindate = DateTime.Now;
                        new JH_Auth_UserB().Update(localuser);//更新logindate  pccode不能更新

                        CommonHelp.SetCookie("szhlcode", localuser.pccode, expires);
                        CommonHelp.SetCookie("username", localuser.UserName, expires);

                    }
                    else
                    {
                        msg.ErrorMsg = "姓名与手机号不匹配";
                        return;
                    }
                }
            }
            else
            {
                msg.ErrorMsg = "登录异常";
                return;
            }
        }
        public void ADDRYMODELWX(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            WX_RY model = JsonConvert.DeserializeObject<WX_RY>(P2);
            if (model == null)
            {
                msg.ErrorMsg = "添加失败";
                return;
            }
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                msg.ErrorMsg = "标题不能为空";
                return;
            }
            if (string.IsNullOrWhiteSpace(model.Content))
            {
                msg.ErrorMsg = "内容不能为空";
                return;
            }
            if (model.ID == 0)
            {
                model.CRDate = DateTime.Now;
                model.CRUser = UserInfo.User.UserName;
                model.ZiLvXiaoZu = UserInfo.User.ZiLvXiaoZu;
                new WX_RYB().Insert(model);
            }
            else
            {
                new WX_RYB().Update(model);
            }

        }
        public void GETRYLIST(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            DataTable dt = new DataTable();
            int total = 0;
            string whestr = " ZiLvXiaoZu='" + UserInfo.User.ZiLvXiaoZu + "' " +
                "or  ZiLvXiaoZu in (select REPLACE(REPLACE(REPLACE(REPLACE(Items,' ',''),CHAR(10) ,''),CHAR(13),''),'/n','') from dbo.Split('" + UserInfo.User.JianDuXiaoZu + "',';'))";
            dt = new WX_GroupB().GetDataPager("WX_RY ", " * ", 99999, 1, " CRDate desc", whestr, ref total);

            msg.Result = dt;
            msg.Result1 = total;
            msg.Result2 = UserInfo.User.IsZuZhang;
        }
        public void GETRYMODEL(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            int Id = int.Parse(P1);

            WX_RY model = new WX_RYB().GetEntity(p => p.ID == Id);
            msg.Result = model;
            if (UserInfo.User.UserName == model.CRUser)
                msg.Result1 = 1;
            else
                msg.Result1 = 0;
        }
        public void DELRYMODEL(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            int Id = int.Parse(P1);
            WX_RY model = new WX_RYB().GetEntity(p => p.ID == Id);
            if (UserInfo.User.UserName == model.CRUser)
            {
                new WX_RYB().Delete(model);
            }
            else
            {
                msg.ErrorMsg = "您没有权限删除";
            }
        }
        public void ADDHUODONGMODEL(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            WX_HD model = JsonConvert.DeserializeObject<WX_HD>(P2);
            if (model == null)
            {
                msg.ErrorMsg = "添加失败";
                return;
            }
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                msg.ErrorMsg = "标题不能为空";
                return;
            }
            if (string.IsNullOrWhiteSpace(model.Content))
            {
                msg.ErrorMsg = "内容不能为空";
                return;
            }
            if (model.ID == 0)
            {
                model.CRDate = DateTime.Now;
                model.CRUser = UserInfo.User.UserName;
                model.ZiLvXiaoZu = UserInfo.User.ZiLvXiaoZu;
                new WX_HDB().Insert(model);
            }
            else
            {
                new WX_HDB().Update(model);
            }

        }
        public void GETHUODONGLIST(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            DataTable dt = new DataTable();
            int total = 0;
            string whestr = " ZiLvXiaoZu='" + UserInfo.User.ZiLvXiaoZu + "' " +
                "or  ZiLvXiaoZu in (select REPLACE(REPLACE(REPLACE(REPLACE(Items,' ',''),CHAR(10) ,''),CHAR(13),''),'/n','') from dbo.Split('" + UserInfo.User.JianDuXiaoZu + "',';'))";
            dt = new WX_GroupB().GetDataPager("WX_HD ", " * ", 99999, 1, " CRDate desc", whestr, ref total);

            msg.Result = dt;
            msg.Result1 = total;

        }
        public void GETHUODONGMODEL(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            int Id = int.Parse(P1);

            WX_HD model = new WX_HDB().GetEntity(p => p.ID == Id);
            msg.Result = model;
            if (UserInfo.User.UserName == model.CRUser)
                msg.Result1 = 1;
            else
                msg.Result1 = 0;
        }

        public void GETMESSAGEHISTORY(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {
            DataTable dt = new DataTable();
            int total = 0;
            int page = 0;
            int pagecount = 50;
            int.TryParse(context.Request.QueryString["p"] ?? "1", out page);
            int.TryParse(context.Request.QueryString["pagecount"] ?? "8", out pagecount);//页数
            string whestr = "GroupName='" + P1 + "'" + " and id in (select top 100 id from[Message] where groupname = '" + P1 + "' order by CRDate desc )";
            dt = new WX_GroupB().GetDataPager("MESSAGE ", " * ", 99999, page, " CRDate", whestr, ref total);

            msg.Result = dt;
            msg.Result1 = total;
        }

        public void INSERTMESSAGE(HttpContext context, Msg_Result msg, string P1, string P2, JH_Auth_UserB.UserInfo UserInfo)
        {

            if (string.IsNullOrWhiteSpace(P1))
            {
                msg.ErrorMsg = "内容不能为空";
                return;
            }
            if (string.IsNullOrWhiteSpace(P2))
            {
                msg.ErrorMsg = "小组不存在";
                return;
            }
            Message model = new Message();
            if (model.ID == 0)
            {
                model.Content = P1;
                model.CRDate = DateTime.Now;
                model.CRUser = UserInfo.User.UserName;
                model.CRUserRealName = UserInfo.User.UserRealName;
                model.GroupName = P2;
                model.Status = 0;
                new MessageB().Insert(model);
            }
            msg.Result = model;
        }

    }
}