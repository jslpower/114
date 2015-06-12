<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublishSupplierInfo.aspx.cs"
    Inherits="IMFrame.SupplierInfo.PublishSupplierInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发布供应</title>
    <style type="text/css">
        body
        {
            margin: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0px; padding: 0px;">
        <table width="100%" border="0" cellspacing="0.5px" >
            <tbody>
                <tr>
                    <td style="width: 277px; height: 17px; background: url(<%=ImageServerUrl%>/IM/images/reviewsBG.gif) no-repeat left top;
                        line-height: 17px; text-align: left; font-weight: bold; color: #fff; padding-left: 3px;
                        font-size: 12px;">
                        请填写您的供应信息内容
                    </td>
                    <td align="right">
                        <span style="color: #F00" id="InputNum"><strong style='font-size: 13px'>还可还输入500字!</strong></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea id="content" style="width: 100%; height: 73px; border: 1px solid #5b9bd1;"
                            runat="server" onkeyup="KeyUp();"></textarea>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span id="showMsg" style="color:Red; font-size:12px"></span>
                    </td>
                    <td align="right">
                        <input id="post" type="submit" style="width: 85px; height: 23px; background: url(<%=ImageServerUrl%>/IM/images/softSendBtn.gif) no-repeat left top;
                            border: 0; line-height: 555px; overflow: hidden; cursor: pointer;" value="" onclick="return post_onclick()" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>

    <script type="text/javascript">
        function KeyUp(){
            var InputNum=500-document.getElementById("content").value.length;
            var span=document.getElementById("InputNum");
            if(InputNum<=0){
                span.innerHTML="<strong style='font-size:13px'>还可输入0字!</strong>";
                document.getElementById("content").value=document.getElementById("content").value.substr(0,500);
                return false;
            }
            span.innerHTML="<strong style='font-size:13px'>还可输入"+InputNum+"字!</strong>";
            return true;
        }
        function post_onclick() {
            var InputNum=500-document.getElementById("content").value.length;
            if(InputNum==500){
                document.getElementById("showMsg").innerHTML="请输入供应内容";
                return false;
            }
            if(InputNum<0){
                document.getElementById("showMsg").innerHTML="供应内容最多500字";
                return false;
            }
        }
    </script>

</body>
</html>
