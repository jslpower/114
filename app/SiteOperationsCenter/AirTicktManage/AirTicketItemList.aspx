<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AirTicketItemList.aspx.cs"
    Inherits="SiteOperationsCenter.AirTicktManage.AirTicketItemList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>特价/免票/K位管理</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>      

    <script type="text/javascript">
        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF9E7";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
        $(function() {
            //****************************
            //选择性删除（可批量删除）
            //****************************
            $("#<%= a_Del.ClientID %>").click(function() {
                var CheckRow = $("#tbl_ItemList").find(":checkbox[name='ckId'][checked='true']").length;
                var ckId = $("#tbl_ItemList").find(":checkbox[name='ckId'][checked='true']:eq(0)").val();
                if (CheckRow == 0) {
                    alert("请选择您要操作的数据！");
                }
                else {
                    if (confirm('您确定要删除这' + CheckRow + '条信息吗？\n\n此操作不可恢复！')) {
                        return true;
                    }
                }
                return false;
            });
        });
        //********************
        //操作列(单个)删除请求
        //********************
        function DeleteItemInfo(Id) {
            if (confirm('您确定要删除此条信息吗？\n\n此操作不可恢复！')) {
                window.location.href = "/AirTicktManage/AirTicketItemList.aspx?DeleteId=" + Id;
                return false;
            }
        }
        //***************
        //全选、反选
        //***************
        function ckAll(obj) {
            $("#tbl_ItemList").find("input[type='checkbox'][name='ckId']").each(function() {
                $(this).attr("checked", $(obj).attr("checked"));
            });
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
                                <a title="新增" href="/AirTicktManage/AirTicketItemInfo.aspx">
                                    <img alt="新增" src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/xinzeng.gif"
                                        width="50" height="25" border="0" /></a>
                            </td>
                            <td width="5%">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_hang.gif" width="2"
                                    height="25" />
                            </td>
                            <td width="23%">
                                <asp:LinkButton runat="server" ID="a_Del" OnClick="a_Del_Click">
                                    <img alt="删除" src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/shanchu.gif" width="51"
                                        height="25" />
                                </asp:LinkButton>
                            </td>
                            <td width="17%">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_d.gif" width="11"
                                    height="25" />
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
        <cc1:customrepeater id="crp_AirTicketList" runat="server">
            <HeaderTemplate>
                <table width="98%" id="tbl_ItemList" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
                    <tr background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying//hangbg.gif"
                        class="white">
                        <td height="23" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                           <input  type="checkbox" onclick="ckAll(this)"/> <strong>序号</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>标题</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>类别</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>是否跳转至散客票平台</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>正文</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>联系人</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>联系方式</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>QQ</strong>
                        </td>
                        <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>操作</strong>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td height="25" align="center">
                        <strong><input type="checkbox" name="ckId" value="<%# Eval("Id") %>" /><%# ((PageIndex-1)*PageSize)+Container.ItemIndex+1 %></strong>
                       
                    </td>
                    <td align="center">
                    <div style='word-wrap:break-word;width:210px;overflow:hidden;'>
                        <%#Eval("Title") %></div>
                    </td>
                    <td align="center">
                        <%#Eval("SpecialFaresType")%>
                    </td>
                    <td align="center">                    
                        <%# Eval("IsJump").ToString().ToLower()=="false"?"否":"是"%>
                    </td>
                    <td align="center">                    
                    <%#Utils.GetText(Utils.LoseHtml(Eval("ContentText").ToString()),20)%>
                    </td>
                    <td align="center">
                        <%#Eval("Contact")%>
                    </td>
                    <td align="center">
                        <%#Eval("ContactWay")%>
                    </td>
                    <td align="center">
                        <%#Eval("QQ")%>
                    </td>
                    <td align="center">
                        <a href='javascript:;' onclick="return DeleteItemInfo('<%#Eval("Id") %>')">删除</a><strong>|</strong>
                         <a href='/AirTicktManage/AirTicketItemInfo.aspx?EditId=<%#Eval("Id") %>'>修改</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying//hangbg.gif"
                    class="white">
                    <td height="23" align="center" valign="middle" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>序号</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>标题</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>类别</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>是否跳转至散客票平台</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                            <strong>正文</strong>
                        </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>联系人</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>联系方式</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>QQ</strong>
                    </td>
                    <td align="center" background="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/hangbg.gif">
                        <strong>操作</strong>
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </cc1:customrepeater>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="center">
                    <cc2:exportpageinfo id="ExportPageInfo1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
