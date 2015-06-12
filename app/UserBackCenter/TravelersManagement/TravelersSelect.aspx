<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TravelersSelect.aspx.cs"
    Inherits="UserBackCenter.TravelersManagement.TravelersSelect" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("jquery1_4") %>"></script>
    <style type="text/css">
        .ulStyle{width:805px;height:260px; list-style-type:none;border-left:solid 1px #AED7EE;border-top:solid 1px #AED7EE;margin:0px;padding:0px;}
        .liStyle{width:160px;height:25px;padding:0px;overflow:hidden;float:left;list-style-type:none;border-bottom:solid 1px #AED7EE;border-right:solid 1px #AED7EE;}
        body{font-size:12px;}
    </style>
</head>
<body>
    <table width="96%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <input type="checkbox" name="checkAll" id="checkAll" onclick="TravelersSelect.selectAll(this)"/>
                <label for="checkAll">全  选</label>
            </td>
            <td height="50" align="left">
                <img src="<%=ImageServerUrl %>/images/point16.gif" width="21" height="21" />关键字（姓名、手机号）：
                <label>
                    <input type="text" size="30" name="txtKeyWord" id="txtKeyWord" value='<%=Request.QueryString["kw"]%>'/>
                </label>
                <input type="button" name="Submit" value=" 搜索" onclick="TravelersSelect.GoSearch();"/>
            </td>
        </tr>
    </table>
    <table width="96%" border="0" align="center" cellpadding="0" cellspacing="0" class="tableStyle" id="tblCustomerList">
        <tr>
            <td style="padding:0px;">
                <ul class="ulStyle">
                     <cc1:CustomRepeater runat="server" ID="RepList">
                        <ItemTemplate>
                            <li class="liStyle">
                                    <table>
                                        <tr>
                                            <td><input type="checkbox" id="ckBox<%#Container.ItemIndex%>" name="ckBox<%#Container.ItemIndex%>"  moreinfo='{ "id": "<%#Eval("Id")%>", "txtName": "<%#this.EnCodeStr(Eval("ChinaName"))%>", "txtTel": "<%#this.EnCodeStr(Eval("Mobile"))%>", "txtCard": "<%#this.EnCodeStr(Eval("IdCardCode"))%>", "txtCardS": "<%#this.EnCodeStr(Eval("PassportCode"))%>", "txtCardT": "<%#Eval("CardType").ToString()%>", "sltSex": "<%#(int)((EyouSoft.Model.CompanyStructure.Sex)Eval("ContactSex"))%>", "sltChild": "<%#(int)((EyouSoft.Model.TicketStructure.TicketVistorType)Eval("VistorType"))%>", "txtNumber": "", "txtRemarks": "<%#this.EnCodeStr(Eval("Remark"))%>" }'/></td>
                                            <td><label for="ckBox<%#Container.ItemIndex%>"><span style="color:#A13B08;"><%#Eval("ChinaName")%></span>&nbsp;<%#Eval("Mobile")%></label></td>
                                        </tr>
                                    </table>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            <%if (this.RepList.Items.Count > 0)
                              {
                                  int count = 50 - this.RepList.Items.Count;
                                  if (count > 0)
                                  {
                                      StringBuilder str = new StringBuilder();
                                      for (int i = 1; i <= count; i++)
                                      {
                                          str.Append("<li class=\"liStyle\"></li>");

                                      }
                                      Response.Write(str);
                                  }
                              }
                             %>
                        </FooterTemplate>
                    </cc1:CustomRepeater>
                </ul>
            </td>
        </tr>
    </table>
    <table id="Table1" cellspacing="0" cellpadding="4" width="96%" align="center" border="0">
        <tr>
            <td class="F2Back" align="right">
                <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4" runat="server"></cc2:ExportPageInfo>
            </td>
        </tr>
    </table>
    <input type="button" id="btnSelect" value="选   择" style="height: 27px;" />
    <script type="text/javascript">
        var TravelersSelect = {
            selectAll: function(obj) {
                $("#tblCustomerList").find("input").attr("checked", obj.checked);
                return false;
            },
            GoSearch: function() {
                var v = $.trim($("#txtKeyWord").val());
                location.href = '/TravelersManagement/TravelersSelect.aspx?kw=' + v + '&CallBackFun=<%=EyouSoft.Common.Utils.GetQueryStringValue("CallBackFun")%>&key=<%=Request.QueryString["key"] %>';
                return false;
            }
        }
        $(function() {
            $("#btnSelect").click(function() {
                var data = [];
                $("#tblCustomerList").find("input:checked").each(function() {
                    if ($(this).attr("moreinfo") != "undefined" && $(this).attr("moreinfo") != "") {
                        data.push($.parseJSON($(this).attr("moreinfo")));
                    }
                });
                parent.window['<%=EyouSoft.Common.Utils.GetQueryStringValue("CallBackFun")%>'](data, '<%=Request.QueryString["key"] %>');
                window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            });
            $("#tblCustomerList li").hover(function() { $(this).css({ "background-color": "#D8E5FF" }); }, function() { $(this).css({ "background-color": "" }) });
            $cks = $("#tblCustomerList li input");
            $cks.live("click", function() {
                var flag = true;
                for (var i = 0; i < $cks.length; i++) {
                    if (!$cks[i].checked) {
                        flag = false; break;
                    }
                }
                $("#checkAll").attr({ "checked": flag });
            });
        });
    </script>
</body>
</html>