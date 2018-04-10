﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using QJY.Data;

namespace QJY.API
{
    public class ServiceContainerV
    {
        public static IUnityContainer Current()
        {

            IUnityContainer container = new UnityContainer();
           
            container.RegisterType<IWsService, Commanage>("Commanage".ToUpper());//免注册接口类

            #region 基础模块接口
            container.RegisterType<IWsService, AuthManage>("XTGL".ToUpper());//基础接口

            container.RegisterType<IWsService, INITManage>("INIT".ToUpper());//系统配置相关API
            #endregion

            container.RegisterType<IWsService, XXFBManage>("XXFB");//信息发布

            container.RegisterType<IWsService, CCXJManage>("CCXJ".ToUpper());//出差休假

            container.RegisterType<IWsService, LCSPManage>("LCSP".ToUpper());//流程审批

            #region JSAPI
            container.RegisterType<IWsService, JSAPI>("JSSDK".ToUpper());
            #endregion

            container.RegisterType<IWsService, DXGLManage>("DXGL".ToUpper());//短信管理 

            container.RegisterType<IWsService, TXLManage>("QYTX".ToUpper());//通讯录 

            container.RegisterType<IWsService, TXSXManage>("TXSX".ToUpper());//提醒事项 

            container.RegisterType<IWsService, GZBGManage>("GZBG");//工作报告

            container.RegisterType<IWsService, QYWDManage>("QYWD".ToUpper());//文档管理 

            container.RegisterType<IWsService, RWGLManage>("RWGL".ToUpper());//任务管理 

            container.RegisterType<IWsService, XMGLManage>("XMGL".ToUpper());//项目管理

            container.RegisterType<IWsService, NOTEManage>("NOTE".ToUpper());//记事本管理 

            container.RegisterType<IWsService, TSSQManage>("TSSQ".ToUpper());//同事社区

            container.RegisterType<IWsService, JFBXManage>("JFBX".ToUpper());//经费报销

            container.RegisterType<IWsService, KQGLManage>("KQGL".ToUpper());//考勤管理

            container.RegisterType<IWsService, WQQDManage>("WQQD".ToUpper());//外勤签到

            container.RegisterType<IWsService, XZGLManage>("XZGL".ToUpper());//薪资管理

            container.RegisterType<IWsService, DBGLManage>("DBGL".ToUpper());//数据库管理

            container.RegisterType<IWsService, CRMManage>("CRM".ToUpper());//数据库管理

            container.RegisterType<IWsService, YCGLManage>("YCGL".ToUpper());//用车管理

            container.RegisterType<IWsService, HYGLManage>("HYGL".ToUpper());//会议管理



            return container;
        }

    }
}
