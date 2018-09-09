<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="QJY.WEB.ViewV5.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
</head>
<body>
    <script src="/ViewV5/JS/jquery-1.11.2.min.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.4.0.js"></script>
    <script>
        wx.config({
            debug: true, // ��������ģʽ,���õ�����api�ķ���ֵ���ڿͻ���alert��������Ҫ�鿴����Ĳ�����������pc�˴򿪣�������Ϣ��ͨ��log���������pc��ʱ�Ż��ӡ��
            appId: 'wx1b5c7dbfe9a3555d', // ������ںŵ�Ψһ��ʶ
            timestamp: Date.parse(new Date()), // �������ǩ����ʱ���
            nonceStr: '', // �������ǩ���������
            signature: '',// ���ǩ��
            jsApiList: [] // �����Ҫʹ�õ�JS�ӿ��б�
        });

    </script>
    <%--<style type="text/css">
        html, body {
            height: 100%;
            width: 100%;
            text-align: center;
        }
    </style>
    
    <script>
        //��δ� ��Ҫ�ǻ�ȡ����ͷ����Ƶ������ʾ��Video ǩ��  
        var canvas = null, context = null, video = null;
        window.addEventListener("DOMContentLoaded", function () {
            try {
                canvas = document.getElementById("canvas");
                context = canvas.getContext("2d");
                video = document.getElementById("video");

                var videoObj = { "video": true, audio: false },
                    flag = true,
                    MediaErr = function (error) {
                        flag = false;
                        if (error.PERMISSION_DENIED) {
                            alert('�û��ܾ������������ý���Ȩ��', '��ʾ');
                        } else if (error.NOT_SUPPORTED_ERROR) {
                            alert('�Բ��������������֧�����չ��ܣ���ʹ�����������', '��ʾ');
                        } else if (error.MANDATORY_UNSATISFIED_ERROR) {
                            alert('ָ����ý������δ���յ�ý����', '��ʾ');
                        } else {
                            alert('ϵͳδ�ܻ�ȡ������ͷ����ȷ������ͷ����ȷ��װ������ˢ��ҳ�棬����', '��ʾ');
                        }
                    };
                //��ȡý��ļ��ݴ��룬Ŀǰֻ֧�֣�Firefox,Chrome,Opera��
                if (navigator.getUserMedia) {
                    //qq�������֧��
                    if (navigator.userAgent.indexOf('MQQBrowser') > -1) {
                        alert('�Բ��������������֧�����չ��ܣ���ʹ�����������1', '��ʾ');
                        return false;
                    }
                    navigator.getUserMedia(videoObj, function (stream) {
                        video.src = stream;
                        video.play();
                    }, MediaErr);
                }
                else if (navigator.webkitGetUserMedia) {
                    navigator.webkitGetUserMedia(videoObj, function (stream) {
                        video.src = window.webkitURL.createObjectURL(stream);
                        video.play();
                    }, MediaErr);
                }
                else if (navigator.mozGetUserMedia) {
                    navigator.mozGetUserMedia(videoObj, function (stream) {
                        video.src = window.URL.createObjectURL(stream);
                        video.play();
                    }, MediaErr);
                }
                else if (navigator.msGetUserMedia) {
                    navigator.msGetUserMedia(videoObj, function (stream) {
                        $(document).scrollTop($(window).height());
                        video.src = window.URL.createObjectURL(stream);
                        video.play();
                    }, MediaErr);
                } else {
                    alert('�Բ��������������֧�����չ��ܣ���ʹ�����������2');
                    return false;
                }
                if (flag) {
                    alert('Ϊ�˻�ø�׼ȷ�Ĳ��Խ�����뾡������ά�����ڿ��У�Ȼ��������㡢ɨ�衣 ��ȷ���������Ȩ��ʹ��������');
                }
                //��������հ�ť���¼���          
                $("#snap").click(function () { startPat(); }).show();
            } catch (e) {
                printHtml("�������֧��HTML5 CANVAS");
            }
        }, false);

        //��ӡ���ݵ�ҳ��      
        function printHtml(content) {
            $(window.document.body).append(content + "<br/>");
        }
        //��ʼ����
        function startPat() {
            setTimeout(function () {//��ֹ���ù���
                if (context) {
                    context.drawImage(video, 0, 0, 320, 320);
                    CatchCode();
                }
            }, 200);
        }
        //ץ����ȡͼ���������ϴ���������      
        function CatchCode() {
            if (canvas != null) {
                //���¿�ʼ�� ����   
                var imgData = canvas.toDataURL();
                //��ͼ��ת��Ϊbase64����
                var base64Data = imgData;//.substr(22); //��ǰ�˽�ȡ22λ֮����ַ�����Ϊͼ������
                //��ʼ�첽��
                $.post("testpost.aspx", { "img": base64Data }, function (result) {
                    printHtml("���������" + result.data);
                    if (result.status == "success" && result.data != "") {
                        printHtml("��������ɹ���");
                    } else {
                        startPat();//���û�н�������������ץ�Ľ���       
                    }
                }, "json");
            }
        }
    </script>

    <div id="support"></div>
    <div id="contentHolder">
        <video id="video" width="320" height="320" autoplay="autoplay">
        </video>
        <canvas style="display: none; background-color: #F00;" id="canvas" width="320" height="320"></canvas>
        <br />
        <button id="snap" style="display: none; height: 50px; width: 120px;">��ʼɨ��</button>
    </div>--%>
    <div>
        <asp:Button ID="btn" runat="server" Text="�ύ" OnClick="btn_Click" Visible="false" />
    </div>
</body>
</html>
