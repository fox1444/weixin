<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="QJY.WEB.WX._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="//g.alicdn.com/msui/sm/0.5.8/css/sm.min.css" />
    <link rel="stylesheet" href="//g.alicdn.com/msui/sm/0.5.8/css/sm-extend.min.css" />
    <link rel="stylesheet" type="text/css" href="/View_Mobile/CSS/animate.css" />
    <link href="/View_Mobile/CSS/WFmain.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="//at.alicdn.com/t/font_1464255787_8152707.css" />
    <link href="/View_Mobile/CSS/add.css?ver=2" rel="stylesheet" />
    <link rel="stylesheet" href="/View_Mobile/css/sm.min.css" />
    <link rel="stylesheet" href="/WX/CSS/wxstyle.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page page-current" style="background: #fff;" id="pageindex1">
            <asp:Panel ID="pnlBindPhone" runat="server" Visible="false">
                <div class="content" ms-controller="BindPhone">
                    <div style="background: #fbf9fe">
                        <div>
                            <!--<div class="weui_cells_title">基本信息</div>-->
                            <div class="weui_cells weui_cells_form">
                                <div class="weui_cell">
                                    <div class="weui_cell_hd">
                                        <label class="weui_label label">姓名</label>
                                    </div>
                                    <div class="weui_cell_bd weui_cell_primary">
                                        <input ms-duplex="tempmodel.modelData.UserRealName" type="text" placeholder="请输入姓名" class="szhl szhl_require weui_input" />
                                    </div>
                                </div>
                            </div>
                            <div class="weui_cells weui_cells_form">
                                <div class="weui_cell">
                                    <div class="weui_cell_hd">
                                        <label class="weui_label label">手机</label>
                                    </div>
                                    <div class="weui_cell_bd weui_cell_primary">
                                        <textarea ms-duplex="tempmodel.modelData.mobphone" type="text" placeholder="请输入手机" class="szhl szhl_require weui_input"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="weui_cells weui_cells_form">
                                <div class="weui_cell">
                                    <div class="weui_cell_hd">
                                        <label class="weui_label label">手机</label>
                                    </div>
                                    <div class="weui_cell_bd weui_cell_primary">
                                        <textarea ms-duplex="tempmodel.modelData.weixinCard" type="text" placeholder="请输入微信号" class="szhl szhl_require weui_input"></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table bar bar-tab" style="z-index: 20;">
                    <button class="button button-fill btnSucc" style="font-size: .9rem; background-color: #49d2ad" ms-click="SaveData(this,false)">
                        <!--<i class="fa fa-plus"></i>-->
                        保存</button>
                </div>
                <script src="/View_Mobile/JS/layer/layer.m.js"></script>
                <script type='text/javascript' src='//g.alicdn.com/sj/lib/zepto/zepto.min.js' charset='utf-8'></script>
                <script type='text/javascript' src='//g.alicdn.com/msui/sm/0.5.8/js/sm.min.js' charset='utf-8'></script>
                <script src="/View_Mobile/JS/ComFunJS.js?jsver=20160425"></script>
                <script src="/View_Mobile/JS/avalon.min.js"></script>
                <script>
                    var tempmodel = avalon.define({
                        $id: "BindPhone",
                        ColumnData: [],
                        name: "绑定手机",
                        iswf: true,//是否属于流程表单
                        tpData: [],
                        userpl: 0,
                        usertype: "",
                        isjl: "",
                        Name: "",
                        wximg: "",
                        dataid: "",
                        isDeploy: 0,
                        groupcode: ComFunJS.getQueryString("GroupCode"),
                        inittemp: function (strId) {


                        },//初始化
                        modelData: { "UserRealName": "", "mobphone": "", "weixinCard": "" },
                        SaveData: function (callback) {

                            $.post("/API/VIEWAPI.ashx?ACTION=WXGL_BINDPHONE", { P1: JSON.stringify(tempmodel.modelData.$model) }, function (result) {
                                if (result.ErrorMsg == "") {
                                    window.location.href = "/WX/Me.html?r=" + Math.random();
                                }
                                else if (result.ErrorMsg == "nomatch") {
                                    alert('手机号与姓名不匹配');
                                }
                            });
                        }
                    });

                </script>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
