<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SiteOperationsCenter.AdvertiseTicket.Default" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>机票咨询列表</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF6C7";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="15%" height="25" background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif">
                <table width="88%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="4%" align="right">
                            <img src="<%=ImageServerUrl %>/images/yunying/ge_da.gif" width="3" height="20" />
                        </td>
                        <td width="4%">
                            <img src="<%=ImageServerUrl %>/images/yunying/ge_hang.gif" width="2" height="25" />
                        </td>
                        <td width="24%">
                            <img src="<%=ImageServerUrl %>/images/yunying/xiugai.gif" width="50" height="25"
                                border="0" id="btnEdit" style="cursor: pointer" />
                        </td>
                        <td width="5%">
                            <img src="<%=ImageServerUrl %>/images/yunying/ge_hang.gif" width="2" height="25" />
                        </td>
                        <td width="23%">
                            <img src="<%=ImageServerUrl %>/images/yunying/shanchu.gif" width="51" height="25"
                                id="btnDelete" style="cursor: pointer" />
                        </td>
                        <td width="17%">
                            <img src="<%=ImageServerUrl %>/images/yunying/ge_d.gif" width="11" height="25" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="85%" align="left" valign="middle" background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif"
                id="tdSearch">标题：<input name="txtTitle" type="text" id="txtTitle" size="15" runat="server" />
                航班类型 ：<asp:DropDownList ID="dropType" runat="server">
                    <asp:ListItem Value="0">全部</asp:ListItem>
                    <asp:ListItem Value="1">单程 </asp:ListItem>
                    <asp:ListItem Value="2">往返程</asp:ListItem>
                    <asp:ListItem Value="3">缺口程</asp:ListItem>
                </asp:DropDownList>
                联系人：
                <input name="txtContactName" type="text" id="txtContactName" size="7" runat="server" />
                出发时间：
                <input name="txtStartDate" type="text" id="txtStartDate" size="10" runat="server" />
                &nbsp;<img src="<%=ImageServerUrl %>/images/yunying/chaxun.gif" width="62" height="21"
                    id="btnSearch" style="cursor: pointer;" />
            </td>
        </tr>
    </table>
     <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang"
                id="tbTicketList">
                <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                    <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <input type="checkbox" name="ckAll" id="checkAll1" onclick="TicketDefault.ckAllTicket(this);"><label
                            for="checkAll1" style="cursor: pointer"><strong>序号</strong>
                    </td>
                    <td width="18%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>文章标题</strong>
                    </td>
                    <td width="8%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>航班类型</strong>
                    </td>
                    <td width="8%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>旅客类型</strong>
                    </td>
                    <td width="8%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>乘机人数</strong>
                    </td>
                    <td width="8%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>出发时间</strong>
                    </td>
                    <td width="8%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>返程时间</strong>
                    </td>
                    <td width="11%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>联系人</strong>
                    </td>
                    <td width="15%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>联系电话</strong>
                    </td>
                </tr>
    <cc1:CustomRepeater ID="repTicketList" runat="server">

        <ItemTemplate>
            <tr bgcolor="#F3F7FF" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                <td height="25" align="center" bgcolor="#F3F7FF">
                    <input type="checkbox" id="ckTicketId" name="ckTicketId" value='<%# Eval("ApplyId") %>'><%#GetCount() %>
                </td>
                <td height="25" align="center" bgcolor="#F3F7FF">
                    <a  href="javascript:void(0);" onclick="return TicketDefault.TitleGo('<%# Eval("ApplyId") %>');">
                        <%# Eval("TicketArticleTitle")%></a>
                </td>
                <td align="center" bgcolor="#F3F7FF">
                    <%# ((EyouSoft.Model.TicketStructure.TicketApply)GetDataItem()).TicketFlight.VoyageSet%>
                </td>
                <td align="center" bgcolor="#F3F7FF">
                    <%# ((EyouSoft.Model.TicketStructure.TicketApply)GetDataItem()).TicketFlight.PeopleCountryType.ToString() == "Foreign" ? "外宾" : "内宾"%>
                </td>
                <td align="center" bgcolor="#F3F7FF">
                    <%# ((EyouSoft.Model.TicketStructure.TicketApply)GetDataItem()).TicketFlight.PeopleNumber%>人
                </td>
                <td width="16%" align="center" bgcolor="#F3F7FF">
                    <%# ((EyouSoft.Model.TicketStructure.TicketApply)GetDataItem()).TicketFlight.TakeOffDate.Value.ToShortDateString()%>
                </td>
                <td width="12%" align="center" bgcolor="#F3F7FF">
                  <%# ((EyouSoft.Model.TicketStructure.TicketApply)GetDataItem()).TicketFlight.VoyageSet == EyouSoft.Model.TicketStructure.VoyageType.单程 ? "" : ((EyouSoft.Model.TicketStructure.TicketApply)GetDataItem()).TicketFlight.ReturnDate.Value.ToShortDateString()%>
                            </td>
                <td width="11%" align="center" bgcolor="#F3F7FF">
                    <%# Eval("ContactName")%>
                </td>
                <td width="15%" align="center" bgcolor="#F3F7FF">
                    <%#Eval("CompanyTel")%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                <td height="25" align="center">
                    <input type="checkbox" id="ckTicketId" name="ckTicketId" value='<%# Eval("ApplyId") %>'><%#GetCount() %>
                </td>
                <td height="25" align="center">
                    <a href="javascript:void(0);" onclick="return TicketDefault.TitleGo('<%# Eval("ApplyId") %>');">
                        <%# Eval("TicketArticleTitle")%></a>
                </td>
                <td align="center">
                    <%# ((EyouSoft.Model.TicketStructure.TicketApply)GetDataItem()).TicketFlight.VoyageSet%>
                </td>
                <td align="center">
                    <%# ((EyouSoft.Model.TicketStructure.TicketApply)GetDataItem()).TicketFlight.PeopleCountryType.ToString() == "Foreign"?"外宾":"内宾"%>
                </td>
                <td align="center">
                    <%# ((EyouSoft.Model.TicketStructure.TicketApply)GetDataItem()).TicketFlight.PeopleNumber%>人
                </td>
                <td width="16%" align="center">
                    <%# ((EyouSoft.Model.TicketStructure.TicketApply)GetDataItem()).TicketFlight.TakeOffDate.Value.ToShortDateString()%>
                </td>
                <td width="12%" align="center">
                   <%# ((EyouSoft.Model.TicketStructure.TicketApply)GetDataItem()).TicketFlight.VoyageSet == EyouSoft.Model.TicketStructure.VoyageType.单程 ? "" : ((EyouSoft.Model.TicketStructure.TicketApply)GetDataItem()).TicketFlight.ReturnDate.Value.ToShortDateString()%>
                </td>
                <td width="11%" align="center">
                    <%# Eval("ContactName")%>
                </td>
                <td width="15%" align="center">
                    <%#Eval("CompanyTel")%>
                </td>
            </tr>
        </AlternatingItemTemplate>
  
    </cc1:CustomRepeater>
      <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                <td width="8%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <input type="checkbox" name="ckAll" id="checkAll12" onclick="TicketDefault.ckAllTicket(this);"><label
                        for="checkAll12" style="cursor: pointer"><strong>序号</strong>
                </td>
                <td width="18%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>文章标题</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>航班类型</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>旅客类型</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>乘机人数</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>出发时间</strong>
                </td>
                <td width="8%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>返程时间</strong>
                </td>
                <td width="11%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>联系人</strong>
                </td>
                <td width="15%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>联系电话</strong>
                </td>
            </tr>
            </table>
       <table width="100%">
        <tr>
            <td align="right">
                <cc3:ExportPageInfo ID="ExportPageInfo1" runat="server" />
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript">
        var TicketDefault = {
            GetCheckList: function() {
                var ckList = new Array();
                $("#tbTicketList").find("input[type='checkbox'][name!='ckAll']:checked").each(function() {
                    ckList.push($(this).val());
                });
                return ckList;
            },
            GetParms: function() {
                var Parms = { Title: "", Type: 0, ContactName: "", StartDate: "" };
                Parms.Title = escape($.trim($("#txtTitle").val()));
                Parms.Type = $("#<%=dropType.ClientID %>").val();
                Parms.ContactName = escape($.trim($("#txtContactName").val()));
                Parms.StartDate = $.trim($("#txtStartDate").val());
                return Parms;

            },
            TicketEdit: function() {
                var ckList = this.GetCheckList();
                if (ckList.length == 0) {
                    alert("未选择修改项!");
                    return false;
                }
                if (ckList.length > 1) {
                    alert("只能选择一个修改项!");
                    return false;
                }
                if (ckList.length == 1) {
                    var Url = "Default.aspx?" + $.param(this.GetParms());
                    var returnUrl = "&returnUrl=" + escape(Url);
                    window.location.href = "EditTicketMessage.aspx?EditId=" + ckList[0] + returnUrl;
                }
            },
            TicketDelete: function() {
                var ckList = this.GetCheckList();
                if (ckList.length == 0) {
                    alert("未选择删除项!");
                    return false;
                }
                if (ckList.length > 0) {
                    if (window.confirm("您确定要删除此咨询信息吗?\n此操作不可恢复!")) {
                        $.ajax({
                            type: "POST",
                            dataType: 'html',
                            url: "Default.aspx?Type=Delete",
                            data: $("#form1").serializeArray(),
                            cache: false,
                            success: function(html) {
                                if (html == "True") {
                                    alert("删除成功!");
                                    window.location.href = "Default.aspx?" + $.param(TicketDefault.GetParms());
                                }
                            }
                        });
                    }
                }
            },
            TicketSearch: function() {
                window.location.href = "Default.aspx?" + $.param(this.GetParms());
            },
            ckAllTicket: function(obj) {//全选
                $("#tbTicketList").find("input[type='checkbox']").attr("checked", $(obj).attr("checked"));
            },
            TitleGo: function(EditId) {
                var Url = "Default.aspx?" + $.param(this.GetParms());
                var returnUrl = "&returnUrl=" + escape(Url);
                window.location.href = "EditTicketMessage.aspx?EditId=" + EditId + returnUrl;
                return false;
            }
        };
        $(document).ready(function() {
            $("#<%=txtStartDate.ClientID%>").focus(function() {
                WdatePicker();
            });
            $("#tdSearch input").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    TicketDefault.TicketSearch();
                    return false;
                }
            });
            $("#btnEdit").click(function() {
                TicketDefault.TicketEdit();
            });
            $("#btnDelete").click(function() {
                TicketDefault.TicketDelete();
            });
            $("#btnSearch").click(function() {
                TicketDefault.TicketSearch();
            });
            
        });
    </script>

    </form>
</body>
</html>
