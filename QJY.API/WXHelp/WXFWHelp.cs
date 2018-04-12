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

namespace QJY.API
{
    class WXFWHelp
    {
        public JH_Auth_QY Qyinfo = null;

        public WXFWHelp(JH_Auth_QY QY)
        {
            //获取企业信息
            Qyinfo = QY;
        }
        public string GetToken(string appID = "")
        {
            AccessTokenResult r = CommonApi.GetToken(appID, Qyinfo.corpSecret, "client_credential");
            return "";
        }
    }
}
