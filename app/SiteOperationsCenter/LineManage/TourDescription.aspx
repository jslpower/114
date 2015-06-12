<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourDescription.aspx.cs"
    Inherits="SiteOperationsCenter.LineManage.TourDescription" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <style>
        *
        {
            margin: 0px;
            padding: 0px;
            font-size:12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="300" style="position: absolute; height: 100%;">
        <tr>
            <td>
                <textarea id="txtAreaMsg" style="width:100%;" rows="6" cols="30" onfocus="this.value=''"></textarea>
               <span style=" color:Red;">&nbsp;&nbsp;&nbsp;请在这里填写团队推广的理由！</span> 
            </td>
        </tr>
        <tr>
            <td align="center">
                <input id="btnArea" type="button" onclick="saveTourMarkerNote()" value="确 定" />
            </td>
        </tr>
    </table>
        <script type="text/javascript" language="javascript">
        function saveTourMarkerNote()
        {
            var callBack='<%=Request["callBack"] %>';//'<%=Request["callBack"] %>'
            var msg=document.getElementById("txtAreaMsg").value.replace(/^(\s|\u00A0)+/, '').replace("'", "‘").replace("'","’");
            if(msg==""){
                alert('推广理由不能为空！')
                return;
            }
            parent.Boxy.getIframeDialog(parent.Boxy.queryString("iframeId")).hide(function(){
                  eval("parent."+callBack+"('"+msg+"')")
            });            
        }
    </script>
    </form>
</body>
</html>
