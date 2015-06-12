<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourTeamApplyList.aspx.cs"
    Inherits="SiteOperationsCenter.AirTicktManage.TourTeamApplyList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>团队票申请</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>

    <script type="text/javascript">
        //********************
        //操作列(单个)删除请求
        //********************
        function DeleteTourTeamInfo(Id) {
            if (confirm('您确定要删除此团队票申请信息吗？\n\n此操作不可恢复！')) {
                window.location.href = "/AirTicktManage/TourTeamApplyList.aspx?DeleteId=" + Id;
                return false;
            }
        }
        //***************
        //全选、反选
        //***************
        function ckAll(obj) {
            $("#tblTourTeamApplyList").find("input[type='checkbox'][name='ckId']").each(function() {
                $(this).attr("checked", $(obj).attr("checked"));
            });
        }
        $(function() {

            //****************************
            //选择性删除（可批量删除）
            //****************************
            $("#<%= a_Del.ClientID %>").click(function() {
                var CheckRow = $("#tblTourTeamApplyList").find(":checkbox[name='ckId'][checked='true']").length;
                var ckId = $("#tblTourTeamApplyList").find(":checkbox[name='ckId'][checked='true']:eq(0)").val();
                if (CheckRow == 0) {
                    alert("请选择您要删除的数据！");
                }
                else {
                    if (confirm('您确定要删除这' + CheckRow + '条信息吗？\n\n此操作不可恢复！')) {
                        return true;
                    }
                }
                return false;
            });



        });
        //****
        //样式
        //****
        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF9E7";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="23%" height="25" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/gongneng_bg.gif">
                    <table width="51%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="4%" align="right">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_da.gif" width="3"
                                    height="20" />
                            </td>
                            <td width="23%">
                                <asp:LinkButton runat="server" ID="a_Del" OnClick="a_Del_Click">
                                    <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/shanchu.gif" width="51"
                                        height="25" />
                                </asp:LinkButton>
                            </td>
                            <td width="5%">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_hang.gif" width="2"
                                    height="25" />
                            </td>
                            <td width="23%">
                                &nbsp;
                            </td>
                            <td width="17%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="77%" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/gongneng_bg.gif"
                    align="center">
                    &nbsp;
                </td>
            </tr>
        </table>
        <cc1:CustomRepeater ID="crp_TourTeamApplyList" runat="server">
            <HeaderTemplate>
                <table width="98%" id="tblTourTeamApplyList" border="0" align="center" cellpadding="0"
                    cellspacing="1" class="kuang">
                    <tr background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                        class="white">
                        <td height="23" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                           <input type="checkbox" onclick="ckAll(this)" /> <strong>序号</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>申请时间</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>申请人</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>类别</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>出发地</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying//hangbg.gif">
                            <strong>目的地</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>备注</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>查看</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>操作</strong>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td height="25" align="center">
                        <strong>
                            <input type="checkbox" name="ckId" value="<%# Eval("Id") %>" /><%# ((PageIndex - 1) * PageSize) +   Container.ItemIndex + 1%></strong>
                    </td>
                    <td align="center">
                        <%#Eval("IssueTime","{0:yyyy-MM-dd}")%>
                    </td>
                    <td align="center">
                        <%#Eval("Contact")%>
                    </td>
                    <td align="center">
                        <%#Eval("GroupType")%>
                    </td>
                    <td align="center">
                        <%#Eval("StartCity")%>
                    </td>
                    <td align="center">
                        <%#Eval("EndCity")%>
                    </td>
                    <td align="center">                     
                    <div style='word-wrap:break-word;width:210px;overflow:hidden;'>
                    <%#Eval("Notes")%>
                    </div>
                    </td>
                   <td align="center">
                        <a href='TourTeamApplyDetails.aspx?TourId=<%#Eval("ID") %>'>
                            点击查看</a>
                    </td>
                    <td align="center">
                        <a href='javascript:;' onclick="return DeleteTourTeamInfo('<%#Eval("Id") %>')">删除</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif"
                    class="white">
                    <td height="23" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying//hangbg.gif">
                        <strong>序号</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>申请时间</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>申请人</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>类别</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>出发地</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>目的地</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>备注</strong>
                    </td>
                     <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>查看</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>操作</strong>
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </cc1:CustomRepeater>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr align="center">
                <td>
                    <cc2:ExportPageInfo ID="ExportPageInfo1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
