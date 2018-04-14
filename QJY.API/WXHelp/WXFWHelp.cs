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

namespace QJY.API
{
    public class WXFWHelp
    {
        public JH_Auth_QY Qyinfo = null;

        public WXFWHelp(JH_Auth_QY QY)
        {
            //获取企业信息
            Qyinfo = QY;
        }
        public string GetToken(string appID = "")
        {
            //AccessTokenResult r = CommonApi.GetToken(Qyinfo.corpId, Qyinfo.corpSecret, "client_credential");


            var task1 = new Task<string>(() => 
            AccessTokenContainer.TryGetAccessTokenAsync("wx1b5c7dbfe9a3555d", "c37f667f8026820e34ff0a6a19e4033d", false).Result
            );

            task1.Start();
            return task1.Result;
        }
    }
}
