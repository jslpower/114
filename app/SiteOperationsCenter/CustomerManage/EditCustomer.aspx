<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="EditCustomer.aspx.cs" Inherits="SiteOperationsCenter.CustomerManage.EditCustomer" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改客户信息</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #txtCompanyRemark
        {
            width: 509px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="lr_bg"
        id="tbCompanyInfo">
        <tr class="lr_hangbg">
            <td width="15%" align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>所在地区：
            </td>
            <td width="85%">
                省份
                <asp:DropDownList ID="dropProvinceId" runat="server">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                    <asp:ListItem Value="1">111</asp:ListItem>
                </asp:DropDownList>
                城市
                <asp:DropDownList ID="dropCityId" runat="server">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                    <asp:ListItem Value="1">222</asp:ListItem>
                </asp:DropDownList>
                <span id="errMsg_Province" style="display: none" class="unnamed1">请选择省份</span> 
                <span id="errMsg_City" style="display: none" class="unnamed1">请选择城市</span>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>客户类型：
            </td>
            <td>
                <asp:CheckBoxList ID="chbCustomerType" runat="server" RepeatColumns="4" >
                </asp:CheckBoxList>
                <span id="errMsg_dropCustomerType" style="display: none" class="unnamed1">请选择客户类型</span>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>单位名称：
            </td>
            <td>
                <input id="txtCompanyName" runat="server" name="txtCompanyName" type="text" class="textfield"
                    size="25" valid="required" errmsg="请输入单位名称" style=" width:300px;" />
                    <span id="errMsg_txtCompanyName" class="unnamed1"></span>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                企业星级：
            </td>
            <td>
                <input id="txtCompanyStar" runat="server" name="txtCompanyStar" type="text" class="textfield" style=" width:300px;"  />
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                公司联系人：
            </td>
            <td>
                <table id="tb_Content" width="100%">
                    <tr>
                        <td>
                            
                            <asp:Panel ID="Panel1" runat="server" Width="98%" Visible="false">
                            <table>
                                <tr>
                                    <td>
                                        <input name="txt_ContactId" type="hidden"  value="0"  />
                                        <span class="unnamed1">*</span>联系人<input name="txt_Name" type="text" style="width: 70px" />
                                     </td>
                                     <td>
                                        <span class="unnamed1">*</span>生日<input name="txt_Birthday" onfocus="WdatePicker()" type="text" style="width: 70px" />
                                     </td>
                                     <td>
                                        纪念日<input name="txt_MemorialDay" onfocus="WdatePicker()" type="text" style="width:70px" />
                                     </td>
                                     <td>
                                        <span class="unnamed1">*</span>职务<input name="txt_Duties" type="text" style="width: 70px" />
                                      </td>
                                     <td>
                                        <span class="unnamed1">*</span>手机<input name="txt_Mobile"  type="text" style="width: 90px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        邮　箱<input name="txt_Email" type="text" style="width: 70px" />
                                    </td>
                                    <td align="right">
                                        Q&nbsp;&nbsp;Q<input name="txt_QQ" type="text" style="width: 70px" />
                                    </td>
                                    <td align="right">
                                        性　格<input name="txt_Character" type="text" style="width: 70px" />
                                     </td>
                                     <td align="right">
                                        爱好<input name="txt_Hobby" type="text" style="width: 70px" />
                                     </td>
                                     <td align="right">
                                        备注<input name="txt_Remarks" type="text" style="width: 90px" />
                                    </td>
                                </tr>
                            </table>
                            </asp:Panel>
                            
                        </td>
                        <td>
                            <input id="btnAddCentent" type="button" value="添加" onclick="AddCententInfo()" />
                        </td>
                    </tr>
                    <asp:Repeater ID="rptContacters" runat="server">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id="tr_auto">
                                <td>
                                    <table>
                                    <tr>
                                        <td>
                                    <input name="txt_ContactId" type="hidden"  value="<%# DataBinder.Eval(Container.DataItem, "ContacterId") %>"  />
                                    <span class="unnamed1">*</span>联系人<input name="txt_Name" type="text" style="width: 70px" value='<%#  DataBinder.Eval(Container.DataItem, "Fullname") %>' />
                                        </td>
                                        <td>
                                    <span class="unnamed1">*</span>生日<input name="txt_Birthday" onfocus="WdatePicker()" type="text" style="width: 70px" value='<%# Convert.ToDateTime( DataBinder.Eval(Container.DataItem, "Birthday")).ToShortDateString() %>' />
                                        </td>
                                        <td>
                                    纪念日<input name="txt_MemorialDay" onfocus="WdatePicker()" type="text" style="width:70px" value='<%# Convert.ToDateTime( DataBinder.Eval(Container.DataItem, "RememberDay")).ToShortDateString() %>' />
                                        </td>
                                        <td>
                                    <span class="unnamed1">*</span>职务<input name="txt_Duties" type="text" style="width: 70px"  value='<%# DataBinder.Eval(Container.DataItem, "JobTitle") %>' />
                                        </td>
                                        <td>
                                    <span class="unnamed1">*</span>手机<input name="txt_Mobile" type="text" style="width: 90px" value='<%# DataBinder.Eval(Container.DataItem, "Mobile") %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                    邮　箱<input name="txt_Email" type="text" style="width: 70px"  value='<%# DataBinder.Eval(Container.DataItem, "Email") %>'/>
                                         </td>
                                         <td align="right">
                                    Q&nbsp;&nbsp;Q<input name="txt_QQ" type="text" style="width: 70px"  value='<%# DataBinder.Eval(Container.DataItem, "QQ") %>'/>
                                         </td>
                                         <td align="right">
                                    性　格<input name="txt_Character" type="text" style="width: 70px" value='<%# DataBinder.Eval(Container.DataItem, "Character") %>' />
                                         </td>
                                         <td align="right">
                                    爱好<input name="txt_Hobby" type="text" style="width: 70px"  value='<%# DataBinder.Eval(Container.DataItem, "Interest") %>' />
                                          </td>
                                         <td align="right">
                                    备注<input name="txt_Remarks" type="text" style="width: 90px"  value='<%# DataBinder.Eval(Container.DataItem, "Remark") %>' /> 
                                        </td>
                                    </tr>
                                    </table>
                                </td>
                                <td>
                                    <input id="btnDel" type="button" value="删除" onclick="D(this,<%# this.rptContacters.Items.Count %>)"  />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
        <tr id="err_Contacter" style=" display:none;" class="lr_hangbg">
            <td class="lr_shangbg">&nbsp;</td>
            <td><span class="unnamed1" id="errMgs_Contacter">请填写完整公司联系人信息</span></td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                地址：
            </td>
            <td>
                <input id="txtAddress" runat="server" name="txtAddress" type="text" class="textfield"
                    style=" width:511px;"  />
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                公司备注：
            </td>
            <td>
                <textarea id="txtCompanyRemark" runat="server" name="txtCompanyNotes" rows="4" style=" font-size:13px;" class="textfield"></textarea>
            </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>适用产品：
            </td>
            <td>
                <asp:CheckBoxList ID="chbSuitProduct" runat="server" RepeatColumns="4">
                </asp:CheckBoxList>
                <span id="errMsg_Suit" style="display: none" class="unnamed1">请选择适用产品</span>
            </td>
        </tr>
    </table>
    <table width="25%" height="30" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" class="baocun_an" Text="保 存" OnClick="btnSave_Click"  />
            </td>
            <td align="center">
            </td>
            <td align="center">
              <input  type="button" value="取 消" onclick="parent.history.back(-1);"/>
            </td>
        </tr>
    </table>
    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    
    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script> 
    
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>
    
    <script type="text/javascript">
        var isFirst = true;
        var isTrue = false;
        var img = null;
        var s = "";
        //添加一行
        function AddCententInfo() {
            var myTable = $("#tb_Content");
            var s = [];
            s.push("<tr>");
            s.push('<td><div style="width: 98%;"><table><tr><td><input name="txt_ContactId" type="hidden"  value="0"  /><span class="unnamed1">*</span>联系人<input name="txt_Name" type="text" style="width: 70px"  /></td><td><span class="unnamed1">*</span>生日<input name="txt_Birthday" onfocus="WdatePicker()" type="text" style="width: 70px"  /></td><td>纪念日<input name="txt_MemorialDay" onfocus="WdatePicker()" type="text" style="width:70px"  /></td><td><span class="unnamed1">*</span>职务<input name="txt_Duties" type="text" style="width: 70px"  /></td><td><span class="unnamed1">*</span>手机<input name="txt_Mobile" type="text" style="width: 90px"  /></td></tr><tr><td align="right">邮　箱<input name="txt_Email" type="text" style="width: 70px" /></td><td align="right">Q&nbsp;&nbsp;Q<input name="txt_QQ" type="text" style="width: 70px" /></td><td align="right">性　格<input name="txt_Character" type="text" style="width: 70px"  /></td><td align="right">&nbsp;爱好<input name="txt_Hobby" type="text" style="width: 70px"   /></td><td align="right">&nbsp;备注<input name="txt_Remarks" type="text" style="width: 90px" /></td></tr></table></td><td><input type="button" value="删除" onclick="D(this,1)"  /></div></td>');
            s.push("</tr>")
            myTable.append(s.join(''));
        }
        //删除一行
        function D(obj, rows) {
            if (rows != 0) {
                $(obj).parent().parent().remove();
            }
        }
    </script>
    
    <script type="text/javascript">
       //验证手机是否存在
        function CheckMoible() {
            var isSure=false;
            $.ajax(
	             {
	                 url: "EditCustomer.aspx",
	                 data: $("input[name='txt_Mobile'],[name='txt_ContactId']").serialize() + "&method=checkMoible",
	                 dataType: "text",
	                 cache: false,
	                 type: "post",
	                 async: false,
	                 success: function(result) {
	                     if (result == "hasMoible") {
	                         if (confirm("电话号码重复，确认保存？"))
	                             isSure = true;
	                     }
	                     else {
	                         isSure = true;
	                     }
	                 },
	                 error: function() {
	                     alert("操作失败!");
	                 }
	             });
	             return isSure;
        }
        function SetProvince(ProvinceId) {
            $("#<%=dropProvinceId.ClientID %>").attr("value", ProvinceId);
        }
        function SetCity(CityId) {
            $("#<%=dropCityId.ClientID %>").attr("value", CityId);
        }
        $(function() {
            $("#<%=btnSave.ClientID%>").click(function() {
                //省份 城市
                var isPass = true;
                if ($("#<%=dropProvinceId.ClientID %>").val() == "0") {
                    $("#errMsg_Province").show();
                    $("#<%=dropProvinceId.ClientID %>").focus();
                    isPass = false;
                }
                if ($("#<%=dropProvinceId.ClientID %>").val() != "0") {
                    $("#errMsg_Province").hide();
                    if ($("#<%=dropCityId.ClientID %>").val() != "0") {
                        $("#errMsg_City").hide();
                    } else {
                        $("#errMsg_City").show();
                        $("#<%=dropCityId.ClientID %>").focus();
                        isPass = false;
                    }
                } else {
                    $("#<%=dropProvinceId.ClientID %>").focus();
                    $("#errMsg_Province").show();
                    isPass = false;
                }
                //客户类型
                var arr = $("#chbCustomerType").find("input:checked").length;
                if (arr == 0) {
                    $("#errMsg_dropCustomerType").show();
                    isPass = false;
                } else {
                    $("#errMsg_dropCustomerType").hide();
                }
                //公司联系人信息
                var trCount = $("#tb_Content").find("table").length;
                var nameCount = 0;
                $("#tb_Content").find("input[name='txt_Name']").each(function() {
                    if ($.trim($(this).val()) != "")
                        nameCount++;
                });
                var birthCount = 0;
                $("#tb_Content").find("input[name='txt_Birthday']").each(function() {
                    if ($.trim($(this).val()) != "")
                        birthCount++;
                });
                var DutiesCount = 0;
                $("#tb_Content").find("input[name='txt_Duties']").each(function() {
                    if ($.trim($(this).val()) != "")
                        DutiesCount++;
                });
                var moibleCount = 0;
                $("#tb_Content").find("input[name='txt_Mobile']").each(function() {
                    if ($.trim($(this).val()) != "")
                        moibleCount++;
                });
                if (nameCount != trCount || birthCount != trCount || DutiesCount != trCount || moibleCount != trCount) {
                    isPass = false;
                    $("#errMgs_Contacter").html("请填写完整公司联系人信息");
                    $("#err_Contacter").show();
                } else {
                    $("#err_Contacter").hide();
                    //公司联系人信息
                    var isMobile = true;
                    $("#tb_Content").find("input[name='txt_Mobile']").each(function() {
                        var patrn = /^(13|15|18|14)\d{9}$/;
                        if (!patrn.exec($.trim($(this).val()))) {
                            isPass = false;
                            isMobile = false;
                        }
                    });
                    if (!isMobile) {
                        $("#errMgs_Contacter").html("手机格式不对");
                        $("#err_Contacter").show();
                    }
                    else {
                        $("#err_Contacter").hide();
                    }
                }
                //适用产品
                var arr = $("#chbSuitProduct").find("input:checked").length;
                if (arr == 0) {
                    $("#errMsg_Suit").show();
                    isPass = false;
                } else {
                    $("#errMsg_Suit").hide();
                }
                var isValidator = ValiDatorForm.validator($("#form1").get(0), "span");
                if (isValidator && isPass) {
                    if (isPass) {
                        return CheckMoible();
                    }
                    return isPass;
                }
                else
                    isPass = false;
                if (isPass)
                    return CheckMoible();

                return isPass;

            });
            FV_onBlur.initValid($("#form1").get(0));
        });
    
    </script>
    
    
    
    </form>
</body>
</html>
