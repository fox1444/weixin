<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="QJY.WEB.ViewV5.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <script src="/ViewV5/JS/jquery-1.11.2.min.js"></script>
</head>
<body>
    <form runat="server">
        <div>
            <asp:Button ID="btn" runat="server" Text="提交" OnClick="btn_Click" Visible="false" />
        </div>
    </form>
    <script>
        $(document).ready(function () {
            var j = $("#urlText");
            var i = $("#goumai");
            $.ajax({
                url: 'http://api.weibo.com/2/short_url/shorten.json?source=2849184197&url_long=' + i[0].href,
                type: "GET",
                dataType: "jsonp",
                cache: false,
                success: function (data) {
                    j.val(data.data.urls[0].url_short);
                }
            });

    </script>
</body>
</html>
