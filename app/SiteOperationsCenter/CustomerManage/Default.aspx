<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SiteOperationsCenter.CustomerManage.Default" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Import Namespace="EyouSoft.Model" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary"  TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet"  TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc3" %>
<%@ Register Src="../usercontrol/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="cc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>客户资料管理</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table  width="100%" border="0" align="center" id="tb_SearchList" cellpadding="0" cellspacing="0">
        <tr>
            <td width="77%" background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif"
                align="left">
                <cc4:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />
                客户类型：<asp:DropDownList ID="ddlCustomerType" runat="server">
                </asp:DropDownList>
                适用产品：<asp:DropDownList ID="ddlSuitProduct" runat="server" Width="100px">
                </asp:DropDownList>
                单位名称：<input id="txtCompanyName" name="txtCompanyName" runat="server" type="text" class="textfield"
                    size="12" />
                <br />
                联系人：<input id="txtContacterFullname" name="txtContactFullname" runat="server" type="text" class="textfield" 
                    size="12" />
                手机号码：<input id="txtContacterMobile" name="txtContacterMobile" runat="server" type="text" class="textfield" 
                    size="12" />
                <img style="cursor: pointer" onclick="CustomerManage.OnSearch();" src="<%=ImageServerUrl %>/images/yunying/chaxun.gif"
                    width="62" height="21" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0"  align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="23%" height="25" background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif">
                <table width="51%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="4%" align="right">
                            <img src="<%=ImageServerUrl %>/images/yunying/ge_da.gif" width="3" height="20" />
                        </td>
                        <td width="23%">
                            <a id="a_Add" href="javascript:void(0);" style="cursor: pointer;display:none" >
                                <img src="<%=ImageServerUrl %>/images/yunying/xinzeng.gif" width="50" height="25"
                                    border="0" /></a>
                        </td>
                        <td width="4%">
                            <img src="<%=ImageServerUrl %>/images/yunying/ge_hang.gif" width="2" height="25" />
                        </td>
                        <td width="24%">
                            <a id="a_Edit" href="javascript:void(0);" style="cursor: pointer;display:none">
                                <img src="<%=ImageServerUrl %>/images/yunying/xiugai.gif" width="50" height="25"
                                    border="0" /></a>
                        </td>
                        <td width="5%">
                            <img src="<%=ImageServerUrl %>/images/yunying/ge_hang.gif" width="2" height="25" />
                        </td>
                        <td width="23%">
                            <asp:ImageButton runat="server" border="0"  ID="img_Delete"  Width="51" 
                                Height="25" style="cursor: pointer;" onclick="img_Delete_Click" Visible="false"  />
                        </td>
                        <td width="17%">
                            <img src="<%=ImageServerUrl %>/images/yunying/ge_d.gif" width="11" height="25" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="77%" background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif"
                align="left">
                
            </td>
        </tr>
    </table>
    <cc1:CustomRepeater ID="repCustomerList" runat="server" 
        onitemdatabound="repCustomerList_ItemDataBound">
        <HeaderTemplate>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang" 
            id="tbCustomerList">
            <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
            <td width="7%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <input name="ckAll" id="ckAllCustomer1" type="checkbox" onclick=" CustomerManage.ckAllCustomer(this);"><label
                    for="ckAllCompany1" style="cursor: pointer"><strong>序号</strong></label>
                </td>
                <td width="20%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>单位名称</strong>
                </td>
                <td width="20%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>联系人</strong>
                </td>
                <td width="10%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>生日</strong>
                </td>
                <td width="10%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>职务</strong>
                </td>
                <td width="15%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>手机</strong>
                </td>
                <td width="15%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>查看</strong>
                </td>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
            <td height="25" align="center">
                <asp:CheckBox runat="server" ID="cbCustomerId" ></asp:CheckBox>
                <asp:Label runat="server" ID="lblCustomerId" Text='<%# DataBinder.Eval(Container.DataItem,"CompanyId") %>'
                    Visible="false"></asp:Label>
            </td>
            <td align="center">
                <%# Eval("CompanyName")%>
            </td>
            <td height="25" align="center">
               <%# ((EyouSoft.Model.PoolStructure.ContacterInfo)((EyouSoft.Model.PoolStructure.CompanyInfo)GetDataItem()).Contacters[0]).Fullname %>
            </td>
            <td align="center">
                <%# Convert.ToDateTime(((EyouSoft.Model.PoolStructure.ContacterInfo)((EyouSoft.Model.PoolStructure.CompanyInfo)GetDataItem()).Contacters[0]).Birthday).ToShortDateString() %>
            </td>
            <td align="center">
                  <%# ((EyouSoft.Model.PoolStructure.ContacterInfo)((EyouSoft.Model.PoolStructure.CompanyInfo)GetDataItem()).Contacters[0]).JobTitle  %>
            </td>
            <td align="center">
                 <%# ((EyouSoft.Model.PoolStructure.ContacterInfo)((EyouSoft.Model.PoolStructure.CompanyInfo)GetDataItem()).Contacters[0]).Mobile %>
            </td>
            <td align="center">
               <a href="javascript:void(0)" onclick='CustomerManage.LookContacters("<%# Eval("CompanyId")  %>");return false;'>查看</a>
            </td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <tr bgcolor="#f3f7ff" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
            <td height="25" align="center">
                <asp:CheckBox runat="server" ID="cbCustomerId"></asp:CheckBox>
                <asp:Label runat="server" ID="lblCustomerId" Text='<%# DataBinder.Eval(Container.DataItem,"CompanyId") %>'
                    Visible="false"></asp:Label>
            </td>
            <td align="center">
                <%# Eval("CompanyName")%>
            </td>
            <td height="25" align="center">
               <%# ((EyouSoft.Model.PoolStructure.ContacterInfo)((EyouSoft.Model.PoolStructure.CompanyInfo)GetDataItem()).Contacters[0]).Fullname %>
            </td>
            <td align="center">
                <%# Convert.ToDateTime(((EyouSoft.Model.PoolStructure.ContacterInfo)((EyouSoft.Model.PoolStructure.CompanyInfo)GetDataItem()).Contacters[0]).Birthday).ToShortDateString() %>
            </td>
            <td align="center">
                  <%# ((EyouSoft.Model.PoolStructure.ContacterInfo)((EyouSoft.Model.PoolStructure.CompanyInfo)GetDataItem()).Contacters[0]).JobTitle %>
            </td>
            <td align="center">
                 <%# ((EyouSoft.Model.PoolStructure.ContacterInfo)((EyouSoft.Model.PoolStructure.CompanyInfo)GetDataItem()).Contacters[0]).Mobile %>
            </td>
            <td align="center">
               <a href="javascript:void(0)" onclick='CustomerManage.LookContacters("<%# Eval("CompanyId")  %>");return false;'>查看</a>
            </td>
        </tr>
    </AlternatingItemTemplate>
    <FooterTemplate>
        <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                <td width="7%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <input name="ckAll" id="ckAllCustomer1" type="checkbox" onclick=" CustomerManage.ckAllCustomer(this);"><label
                    for="ckAllCompany1" style="cursor: pointer"><strong>序号</strong></label>
                </td>
                <td width="20%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>单位名称</strong>
                </td>
                <td width="20%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>联系人</strong>
                </td>
                <td width="10%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>生日</strong>
                </td>
                <td width="10%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>职务</strong>
                </td>
                <td width="15%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>手机</strong>
                </td>
                <td width="15%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>查看</strong>
                </td>
            </tr>
        </table>
    </FooterTemplate>
    </cc1:CustomRepeater>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right">
                <cc2:ExportPageInfo ID="ExportPageInfo1" runat="server" />
            </td>
        </tr>
    </table>
    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script> 
    
    <script type="text/javascript">
        var CustomerManage = {
            OnSearch: function() {
                var SearchUrl = "Default.aspx?ProvinceId=" + $("#ProvinceAndCityList1_ddl_ProvinceList").val()
                + "&CityId=" + $("#ProvinceAndCityList1_ddl_CityList").val()
                + "&CustomerTypeId=" + $("#<%=ddlCustomerType.ClientID %>").val()
                + "&SuitProductId=" + $("#<%=ddlSuitProduct.ClientID %>").val()
                + "&CustomerName=" + encodeURIComponent($.trim($("#<%=txtCompanyName.ClientID %>").val()))
                + "&ContacterFullname=" + encodeURIComponent($.trim($("#<%=txtContacterFullname.ClientID %>").val()))
                + "&ContacterMobile=" + encodeURIComponent($.trim($("#<%=txtContacterMobile.ClientID %>").val()))
                + "&Page=" + 1;
                window.location.href = SearchUrl;
            },
            GetCkCustomerList: function() {
                var arr = new Array();
                var jQueryObj = $("#tbCustomerList tr input[type='checkbox']");
                jQueryObj.each(function(i) {
                    var parentObj = $(this).parent();
                    var checkBoxValue = parentObj.attr("InnerValue");
                    if (this.checked && i > 0 && i < (jQueryObj.length - 1)) {
                        arr.push([checkBoxValue, this]);
                    }
                })
                return arr;
            },
            ckAllCustomer: function(obj) {
                var ck = document.getElementsByTagName("input");
                for (var i = 0; i < ck.length; i++) {
                    if (ck[i].type == "checkbox") {
                        ck[i].checked = obj.checked;
                    }
                }
            },
            EditCustomer: function() {
                var ckList = this.GetCkCustomerList();
                if (ckList.length == 0) {
                    alert("未选择修改项!");
                }
                if (ckList.length > 1) {
                    alert("只能选择一个修改项!");
                }
                if (ckList.length == 1) {
                    var returnUrl = "&returnUrl=" + escape("Default.aspx");
                    window.location.href = "EditCustomer.aspx?EditId=" + ckList[0][0] + returnUrl;
                }
            },
            DeleteCompany: function() {
                var ckList = this.GetCkCustomerList();
                if (ckList.length == 0) {
                    alert("未选择删除项！");
                    return false;
                }
                if (ckList.length > 0) {
                    if (window.confirm("您确认要删除此客户资料吗?\n此操作不可回复!")) {
                        return true;
                    }
                    return false;
                }
            },
            openDialog: function(strurl, strtitle, strwidth, strheight, strdate) {
                Boxy.iframeDialog({ title: strtitle, iframeUrl: strurl, width: strwidth, height: strheight, draggable: true, data: strdate });
            },
            LookContacters: function(CompanyId) {
                CustomerManage.openDialog("AjaxContacters.aspx", "查看公司联系人详细信息", "655", "200", "CompanyId=" + CompanyId);
            }
        };
        $(function() {
            $("#tb_SearchList input").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    CustomerManage.OnSearch();
                    return false;
                }
            });
            $("#img_Search").click(function() {
                CustomerManage.OnSearch();
            });
            if ("<%=EditFlag %>" == "True") {
                $("#a_Edit").show();
                $("#a_Edit").click(function() {
                    CustomerManage.EditCustomer();
                    return false;
                });
            } else {
                $("#a_Edit").hide();
            }
            if ("<%=InsertFlag %>" == "True") {
                $("#a_Add").show();
                $("#a_Add").click(function() {
                    window.location = "EditCustomer.aspx";
                    return false;
                });
            } else {
                $("#a_Add").hide();
            }
        });
    </script>
    
    <script type="text/javascript">
        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF6C7";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
    </script>
    
    </form>
</body>
</html>
