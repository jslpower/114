<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FreightAdd.aspx.cs" Inherits="UserBackCenter.TicketsCenter.FreightManage.FreightAdd"
    EnableEventValidation="false" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="FreightTop.ascx" TagName="FreightTop" TagPrefix="uc1" %>
<asp:content id="FreightAdd" contentplaceholderid="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
    commonTourModuleData.add({
        ContainerID: '<%=tblID %>',
        ReleaseType: 'FreightAdd'
    });
</script>

<!--right start-->

<div class="admin_right">
    <div id="fre_topdiv">
        <ul class="sub_leftmenu">
            <li><a class="book_default" style="text-decoration: none; cursor: default;">运价添加</a></li>
            <li><a href="/ticketscenter/freightmanage/freightmainte.aspx" rel="toptaburl">运价维护</a></li>
        </ul>
        <div class="clearboth">
        </div>
       
            
            <uc1:FreightTop ID="FreightTop1" runat="server" />
            
        
    </div>
    <div id="con_two_1">
        <table id="<%=tblID %>" width="830px" border="0" align="center" cellpadding="0" cellspacing="0"
            bgcolor="#EEF7FF" class="userInfo" id="">
            <tr>
                <td height="30" colspan="4" align="right">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#99CCFF">
                        <tr>
                            <td width="10%" height="40" align="center">
                                &nbsp;
                            </td>
                            <th width="45%" align="left">
                                运价类型:
                                <asp:radiobutton id="fre_rdo_Single" runat="server" text="单程" groupname="freightType"
                                    onclick='FreightPage.ToAndBack("1",this)' value="1" />
                                <asp:radiobutton id="fre_rdo_Back" runat="server" text="来回程" groupname="freightType"
                                    onclick='FreightPage.ToAndBack("2",this)' value="0" />
                                    <input type="hidden" value="<%=tblID %>" />
                            </th>
                            <th width="45%" align="left">
                                &nbsp;
                            </th>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="150" height="30" align="right">
                    <font color="#FF0000">*</font> 产品类型：
                </td>
                <td width="16" align="left">
                    &nbsp;
                </td>
                <td width="318" align="left">
                   
                        <asp:radiobutton id="fre_rdo_Integer" runat="server" text="整团" groupname="proType"
                            value="1" />
                        <asp:radiobutton id="fre_rdo_San" runat="server" text="散拼" groupname="proType" value="0" />
                    
                </td>
                <td width="349" align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="30" align="right">
                    <font color="#FF0000">*</font> 航空公司：
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    <asp:dropdownlist id="fre_AirCompanyList" runat="server" >
                            
                            </asp:dropdownlist>
                </td>
                <td align="left">
                    选择待发布销售运价的航空公司名称
                </td>
            </tr>
            <tr>
                <td height="30" align="right">
                    <font color="#FF0000">*</font> 起始地：
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    <asp:dropdownlist id="fre_StartDdl" runat="server">
                            
                            </asp:dropdownlist>
                </td>
                
                <td align="left">
                    选择该运价所适用的航班起飞机场（如：SHA,PEK）
                </td>
            </tr>
            <tr>
                <td height="30" align="right">
                    <font color="#FF0000">*</font> 目的地：
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left" valign="middle">
                <div runat="server" id="fre_divToLb">
                    <table width="80%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="35%" rowspan="2" align="center">
                                <asp:listbox id="fre_FromLb" runat="server" width="100" height="150" >
                                      
                                        </asp:listbox>
                                        
                            </td>
                            <td width="22%" height="60" align="center">
                                <input type="button" onclick="FreightPage.MoveCityToRight(this)" id="fre_btnToRight" value="&gt;&gt;&gt;" ref="<%=tblID %>" />
                            </td>
                            <td width="43%" rowspan="2" align="left">
                                <asp:listbox id="fre_ToLb" runat="server" width="100" height="150" >
                                
                                       </asp:listbox>
                                       <%--<span id="errMsg_<%=fre_ToLb.ClientID %>" class="errmsg"></span>--%>
                                <asp:hiddenfield runat="server" id="fre_hideToLb"></asp:hiddenfield>
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <input type="button" id="fre_btnToLeft" value="&lt;&lt;&lt;" ref="<%=tblID %>" />
                            </td>
                        </tr>
                    </table>
                    </div>
                    <table height="100%">
                    <tr>
                    <td>
                        <asp:DropDownList ID="fre_ToLbByUpdate" runat="server">

                        </asp:DropDownList>
                    </td>
                    </tr>
                    </table>
                    <br />
                </td>
                <td align="left">
                    选择该政策所适用的航班到达城市（如SHA/PEK，如填多个请用'/'隔开）
                </td>
            </tr>
            <tr>
                <td height="30" align="right">
                    当前是否启用运价：
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    <asp:radiobutton id="fre_rdoPriceEnable" runat="server" text="启用" groupname="fre_Price" value="1"
                        />
                    <asp:radiobutton id="fre_rdoPriceClose" runat="server" text="关闭" groupname="fre_Price" value="0" />
                    
                    <asp:radiobutton id="fre_rdoPriceExpired" runat="server" text="过期关闭" groupname="fre_Price" value="2" />
                </td>
                <td align="left">
                    请选择当前是否启用运价
                </td>
            </tr>
            <tr>
                <td height="30" align="right">
                    是否需要更换PNR：
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    <asp:radiobutton id="fre_rdoPnrYes" runat="server" text="是" groupname="fre_pnr" value="1"/>
                    <asp:radiobutton id="fre_rdoPnrnNo" runat="server" text="否" groupname="fre_pnr" value="0" />
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="30" align="right">
                    运价开始日：
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    <asp:textbox id="fre_txtPriceBegin" runat="server" width="105px" height="20px" onfocus="WdatePicker()" ></asp:textbox>
                   
                    <img src="<%=ImageServerUrl %>/images/jipiao/time1.gif" width="16" height="13" style="position: relative;
                        left: -28px; top: 3px;" /> <%--<span id="errMsg_<%=fre_txtPriceBegin.ClientID %>" class="errmsg"></span>--%>
                </td>
                <td align="left">
                    请选择优惠开始日
                </td>
            </tr>
            <tr>
                <td height="30" align="right">
                    运价结束日：
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    <asp:textbox id="fre_OfferEnd" runat="server" width="105px" height="20px" onfocus="WdatePicker()"></asp:textbox>
                   
                    <img src="<%=ImageServerUrl %>/images/jipiao/time1.gif" width="16" height="13" style="position: relative;
                        left: -28px; top: 3px;" /><%-- <span id="errMsg_<%=fre_OfferEnd.ClientID %>" class="errmsg"></span>--%>
                </td>
                <td align="left">
                    请选择优惠结束日
                </td>
            </tr>
            <!--Test -->
            <tr>
                <td align="right">
                   
                </td>
                <td align="left">
                </td>
                <td align="left">
                   
                </td>
                <td align="left">
                </td>
            </tr>
            <tr>
                <td align="right" colspan="3" rowspan="7" width="650px" height="340px"
                    style="border:solid 0px red">
                    <fieldset style="border: solid 1px gray; text-align: left; width: 210px; padding:0 0 0 5px; float:left;margin-left:70px;">
                        <legend><b>去程</b></legend>
                        <table style="width:215px; border:solid 0px red" >
                            <tr height="40px">
                                <td align="right">
                                    去程适用：</td>
                                <td>
                                 <span id="fre_spanFrom" ref="<%=tblID %>">
                                   <input type="checkbox" value="周一" runat="server" id="fre_FromMonday"/>周一
                                   <input type="checkbox" value="周二" runat="server" id="fre_FromTuesday"/>周二
                                   <input type="checkbox" value="周三" runat="server" id="fre_FromWednesday"/>周三<br />
                                   <input type="checkbox" value="周四" runat="server" id="fre_FromThursday"/>周四
                                   <input type="checkbox" value="周五" runat="server" id="fre_FromFriday"/>周五
                                   <input type="checkbox" value="周六" runat="server" id="fre_FromSaturday"/>周六<br />
                                   <input type="checkbox" value="周日" runat="server" id="fre_FromSunday"/>周日
                                  </span>
                                  <asp:HiddenField ID="fre_FromForDay" runat="server"></asp:HiddenField>
                                </td>
                            </tr>
                            <tr height="40px">
                                <td align="right">
                                   独立日期：
                                </td>
                                <td>
                                     <asp:textbox id="fre_txtIndeDate" runat="server" width="105px" height="20px"
                                        onfocus="WdatePicker()" ></asp:textbox><img src="<%=ImageServerUrl %>/images/jipiao/time1.gif"
                                            width="16" height="13" style="position: relative; left: -28px; top: 3px;" />
                                             <%--<span id="errMsg_<%=fre_txtIndeDate.ClientID %>" class="errmsg"></span>--%>
                                            </td>
                            </tr>
                            <tr height="40px">
                                <td align="right">
                                    参考面价：
                                    </td>
                                <td>
                                    <asp:textbox id="fre_txtRefePrice" runat="server" width="70px" height="20px" errmsg="*|格式不正确!" valid="required|IsDecimalTwo" onblur="FreightPage.SetPrice(this,'1')" ></asp:textbox><span id="errMsg_<%=fre_txtRefePrice.ClientID %>" class="errmsg" ref="<%=tblID %>"></span>
                                </td>
                            </tr>
                            <tr height="40px">
                                <td align="right">
                                    参考扣率：</td>
                                <td>
                                    <asp:textbox id="fre_txtDeduction" runat="server" width="70px" height="20px" errmsg="*|格式不正确!" valid="required|IsDecimalOne" onblur="FreightPage.SetPrice(this,'1')" ></asp:textbox>  %
                               <span id="errMsg_<%=fre_txtDeduction.ClientID %>" class="errmsg" ref="<%=tblID %>"></span>
                                </td>
                            </tr>
                            <tr height="40px">
                                <td align="right">
                                    结算价格：
                                    </td>
                                <td>
                                    <asp:textbox id="fre_txtSettPrice" runat="server" width="70px" height="20px" errmsg="*|格式不正确!" valid="required|IsDecimalTwo" readonly="true"></asp:textbox>
                               <span id="errMsg_<%=fre_txtSettPrice.ClientID %>" class="errmsg"></span>
                                </td>
                            </tr>
                            <tr height="40px">
                                <td align="right">
                                    燃油：</td>
                                <td>
                                    <asp:textbox id="fre_txtFuelPrice" runat="server" width="70px" height="20px" errmsg="*|格式不正确!" valid="required|IsDecimalTwo"></asp:textbox>
                               <span id="errMsg_<%=fre_txtFuelPrice.ClientID %>" class="errmsg"></span>
                                </td>
                            </tr>
                            <tr height="40px">
                                <td align="right">
                                    机建：
                                    </td>
                                <td>
                                    <asp:textbox id="fre_txtMachPrice" runat="server" width="70px" height="20px" errmsg="*|格式不正确!" valid="required|IsDecimalTwo"></asp:textbox>
                               <span id="errMsg_<%=fre_txtMachPrice.ClientID %>" class="errmsg"></span>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                     <fieldset id="fre_weekBack_d" style="border: solid 1px gray; display:none; text-align: left; width: 210px; padding:0 0 0 5px; float:left;  margin-left:10px;" runat="server">
                        <legend><b>回程</b></legend>
                        
                        <table style="width:215px; border:solid 0px red" >
                            <tr height="40px">
                                <td align="right"  >
                                    回程适用：</td>
                                <td>
                                <span id="fre_spanBack" ref="<%=tblID %>">
                                <input type="checkbox" value="周一" runat="server" id="fre_BackMonday"/>周一
                                   <input type="checkbox" value="周二" runat="server" id="fre_BackTuesday"/>周二
                                   <input type="checkbox" value="周三" runat="server" id="fre_BackWednesday"/>周三<br />
                                   <input type="checkbox" value="周四" runat="server" id="fre_BackThursday"/>周四
                                   <input type="checkbox" value="周五" runat="server" id="fre_BackFriday"/>周五
                                   <input type="checkbox" value="周六" runat="server" id="fre_BackSaturday"/>周六<br />
                                   <input type="checkbox" value="周日" runat="server" id="fre_BackSunday"/>周日
                                   </span>
                                   <asp:HiddenField ID="fre_hideToForDay" runat="server" ></asp:HiddenField>
                                   
                                </td>
                            </tr>
                            <tr height="40px">
                                <td align="right">
                                   独立日期：
                                </td>
                                <td>
                                     <asp:textbox id="fre_txtIndeDateBack" runat="server" width="105px" height="20px"
                                        onfocus="WdatePicker()" noauthen="1"></asp:textbox><img src="<%=ImageServerUrl %>/images/jipiao/time1.gif"
                                            width="16" height="13" style="position: relative; left: -28px; top: 3px;" />
                                                  <%--<span id="errMsg_<%=fre_txtIndeDateBack.ClientID %>" class="errmsg"></span>--%></td>
                            </tr>
                            <tr height="40px">
                                <td align="right">
                                    参考面价：
                                    </td>
                                <td>
                                    <asp:textbox id="fre_txtRefePriceBack" runat="server" width="70px" height="20px" errmsg="*|格式不正确!" valid="required|IsDecimalTwo" NoAuthen="1" onblur="FreightPage.SetPrice(this,'2')"></asp:textbox> <span id="errMsg_<%=fre_txtRefePriceBack.ClientID %>" class="errmsg" ref="<%=tblID %>"></span>
                                </td>
                            </tr>
                            <tr height="40px">
                                <td align="right">
                                    参考扣率：</td>
                                <td>
                                    <asp:textbox id="fre_txtDeductionBack" runat="server" width="70px" height="20px" errmsg="*|格式不正确!" valid="required|IsDecimalOne" NoAuthen="1" onblur="FreightPage.SetPrice(this,'2')"></asp:textbox> % 
                                    <span id="errMsg_<%=fre_txtDeductionBack.ClientID %>" class="errmsg" ref="<%=tblID %>"></span>
                                </td>
                            </tr>
                            <tr height="40px">
                                <td align="right">
                                    结算价格：
                                    </td>
                                <td>
                                    <asp:textbox id="fre_txtSettPriceBack" runat="server" width="70px" height="20px" errmsg="*|格式不正确!" valid="required|IsDecimalTwo" NoAuthen="1" readonly="true"></asp:textbox>
                               <span id="errMsg_<%=fre_txtSettPriceBack.ClientID %>" class="errmsg"></span>
                                </td>
                            </tr>
                            <tr height="40px">
                                <td align="right">
                                    燃油：</td>
                                <td>
                                    <asp:textbox id="fre_txtFuelPriceBack" runat="server" width="70px" height="20px" errmsg="*|格式不正确!" valid="required|IsDecimalTwo" noauthen="1"></asp:textbox> 
                               <span id="errMsg_<%=fre_txtFuelPriceBack.ClientID %>" class="errmsg"></span>
                                </td>
                            </tr>
                            <tr height="40px">
                                <td align="right">
                                    机建：
                                    </td>
                                <td>
                                    <asp:textbox id="fre_txtMachPriceBack" runat="server" width="70px" height="20px" errmsg="*|格式不正确!" valid="required|IsDecimalTwo" NoAuthen="1"></asp:textbox> 
                               <span id="errMsg_<%=fre_txtMachPriceBack.ClientID %>" class="errmsg"></span>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <br />
                   
                </td>
                <td align="left" height="40px">
                    
                </td>
            </tr>
            <tr>
                <td align="left" height="50px">
                   
                </td>
            </tr>
            <tr>
                <td align="left" height="30px"> 请输入同行客户折扣率（1至100之间）<br />
                    结算价格 = 参考面价 - （参考面价 * 扣率）
                </td>
            </tr>
            <tr>
                <td align="left" height="50px">
                  
                </td>
            </tr>
            <tr>
                <td align="left">
                   
                </td>
            </tr>
            <tr>
                <td align="left">
                   
                </td>
            </tr>
            <tr>
                <td align="left">
                    </td>
            </tr>
            <!--Test -->
            <tr>
                <td height="30" align="right">
                    人数限制：
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    <asp:textbox id="fre_txtPeopleCount" runat="server" width="70px" height="20px" errmsg="*|格式不正确!" valid="required|isInt" min="1" ></asp:textbox>
               <span id="errMsg_<%=fre_txtPeopleCount.ClientID %>" class="errmsg"></span>
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="30" align="right">
                    上下班时间：
                </td>
                <td align="left">
                </td>
                <td align="left">
                    上班
                    <asp:textbox id="fre_txtToWork" runat="server" width="70px" height="20px" errmsg="*" valid="required"></asp:textbox> <span id="errMsg_<%=fre_txtToWork.ClientID %>" class="errmsg"></span>
                    下班
                    <asp:textbox id="fre_txtBackHome" runat="server" width="70px" height="20px" errmsg="*" valid="required"></asp:textbox>
            <span id="errMsg_<%=fre_txtBackHome.ClientID %>" class="errmsg"></span>
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="100" align="right">
                    备注：
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    <asp:textbox id="fre_txtRemark" runat="server" width="250px" height="100px" textmode="Multiline"
                        rows="5" text="备注固有内容：适用航班MU1234提前出票天数1天 最短停留天数2天 最长停留天数3天" errmsg="*" valid="required"></asp:textbox>
                         <span id="errMsg_<%=fre_txtRemark.ClientID %>" class="errmsg"></span>
                </td>
                <td align="left">
                    请输入备注,如果你想为顾客额外提供优质的服务，请在此填写
                </td>
            </tr>
            <tr>
                <td height="30" colspan="3" align="center">
                    <img id="fre_imgBtnSave" src="<%=ImageServerUrl%>/images/jipiao/baocun_btn.jpg" width="79"
                        height="25" style="cursor: pointer;"
                        alt="保存" onclick="FreightPage.SaveClick('<%=this.fre_hideType.Value %>','<%=tblID %>');" />
                    &nbsp;&nbsp;&nbsp; <span id="fre_spanmsg" style="color: red"></span>
                    <asp:hiddenfield id="fre_hideType" runat="server"></asp:hiddenfield>
                    <asp:hiddenfield id="fre_hideId" runat="server"></asp:hiddenfield>
                </td>
                <td align="left">
                </td>
            </tr>
        </table>
    </div>
    <div class="clearboth">
    </div>
    <div id="con_two_2">
    
    </div>
</div>
<!--right end-->

<script type="text/javascript">
    $(function() {
        //表单验证
        var form = $("#<%=tblID %>").find("img[id$=fre_imgBtnSave]").closest("form").get(0);
        FV_onBlur.initValid(form);

        //设置回程是否显示
        if ("<%=isOpenBack %>" == "0") {
            $("#<%=tblID %>").find("fieldset[id$=<%=fre_weekBack_d.ClientID%>]").css("display", "none");
        } else {
            $("#<%=tblID %>").find("fieldset[id$=<%=fre_weekBack_d.ClientID%>]").css("display", "block");
        }

        //去程
        $("#fre_spanFrom input[type='checkbox']").click(function() {
            var obj = FreightPage._getData($(this).parent().attr("ref"));
            var str = "";
            $("#" + obj.ContainerID).find("span[id$=fre_spanFrom] input[type='checkbox']").each(
            function() {
                if ($(this).attr("checked")) {
                    str += $(this).val() + ",";
                }
            });
            $("#" + obj.ContainerID).find("input[type=hidden][id$=<%=fre_FromForDay.ClientID%>]").val(str);
        });
        //回程
        $("#fre_spanBack input[type='checkbox']").click(function() {
            var obj = FreightPage._getData($(this).parent().attr("ref"));
            var str = "";
            $("#" + obj.ContainerID).find("span[id$=fre_spanBack] input[type='checkbox']").each(
            function() {
                if ($(this).attr("checked")) {
                    str += $(this).val() + ",";
                }
            });
            $("#" + obj.ContainerID).find("input[type=hidden][id$=<%=fre_hideToForDay.ClientID%>]").val(str);
        });

        //目的地删除按钮
        $("#fre_btnToLeft").click(function() {
            var obj = FreightPage._getData($(this).attr("ref"));
            $("#" + obj.ContainerID).find("select[id$=<%=fre_ToLb.ClientID%>] option:selected").remove();
            var str = "";
            $("#" + obj.ContainerID).find("select[id$=<%=fre_ToLb.ClientID%>] option").each(function() {
                str += $(this).val() + "|" + $(this).text() + ",";

            });
            $("#" + obj.ContainerID).find("input[type=hidden][id$=<%=fre_hideToLb.ClientID%>]").val(str);
        });

        //打开新窗口
        $("#fre_topdiv a[rel='toptaburl']").click(function() {
            topTab.open($(this).attr("href"), "运价维护");
            return false;
        });

    });
    
    var FreightPage = {
        ToAndBack: function(type, args) {
            var obj;
            if (type == "1") {
                obj = FreightPage._getData($(args).next().next().next().next().val());
                $("#" + obj.ContainerID).find("fieldset[id$=<%=fre_weekBack_d.ClientID%>]").css("display", "none");
            } else {
                obj = FreightPage._getData($(args).next().next().val());
                $("#" + obj.ContainerID).find("fieldset[id$=<%=fre_weekBack_d.ClientID%>]").css("display", "block");
            }
        },
        _getData: function(id) {
            return commonTourModuleData.get(id);
        },
        CheckForm: function(id) {
            var obj = FreightPage._getData(id);
            var isModified = CheckFormIsChange.isFormChanaged($("#" + obj.ContainerID).closest("form").get(0));
            if (isModified == true) {
                if (obj.ReleaseType == "FreightAdd") {
                    if (!confirm("当前信息尚未保存。\n\n是否舍弃该信息？")) {
                        return false;
                    }
                }
            }
            return false;
        },
        SetPrice: function(o, type) {
            if (type == "1") {
                //去程
                var obj = FreightPage._getData($(o).next().attr("ref"));
                var price = Number($("#" + obj.ContainerID).find("input[type=text][id$=<%=fre_txtRefePrice.ClientID%>]").val());
                var rate = Number($("#" + obj.ContainerID).find("input[type=text][id$=<%=fre_txtDeduction.ClientID%>]").val());
                if (rate >= 100 || rate <= 0 && rate != "") {
                    alert("去程参考扣率必须在0-100之间");
                    $("#" + obj.ContainerID).find("input[type=text][id$=<%=fre_txtDeduction.ClientID%>]").val("")
                    return;
                }

                var ratePrice = price * rate / 100;
                if (ratePrice < 0.01) {
                    ratePrice = 0;
                } else {
                    ratePrice = Number(ratePrice.toString().replace(/^(\d+\.\d{2})\d*$/, "$1 "));
                }

                var val = (Number((price - ratePrice))).toString().replace(/^(\d+\.\d{2})\d*$/, "$1");

                if (Number(val) < 0.01) {
                    val = 0.01;
                }
                if (val.toString() == "NaN") {

                    val = "";
                }
                $("#" + obj.ContainerID).find("input[type=text][id$=<%=fre_txtSettPrice.ClientID%>]").val(val);
            }
            else {
                //回程
                var obj = FreightPage._getData($(o).next().attr("ref"));
                var price = Number($("#" + obj.ContainerID).find("input[type=text][id$=<%=fre_txtRefePriceBack.ClientID%>]").val());
                var rate = Number($("#" + obj.ContainerID).find("input[type=text][id$=<%=fre_txtDeductionBack.ClientID%>]").val());
                if (rate > 100 || rate < 1 && rate != "") {
                    alert("回程参考扣率必须在1-100之间");
                    $("#" + obj.ContainerID).find("input[type=text][id$=<%=fre_txtDeductionBack.ClientID%>]").val("");
                    return;
                }
                var ratePrice = price * rate / 100;
                if (ratePrice < 0.01) {
                    ratePrice = 0;
                } else {
                    ratePrice = Number(ratePrice.toString().replace(/^(\d+\.\d{2})\d*$/, "$1 "));
                }

                var val = (Number((price - ratePrice))).toString().replace(/^(\d+\.\d{2})\d*$/, "$1");

                if (Number(val) < 0.01) {
                    val = 0.01;
                }
                if (val.toString() == "NaN") {
                    val = "";
                }
                $("#" + obj.ContainerID).find("input[type=text][id$=<%=fre_txtSettPriceBack.ClientID%>]").val(val);
            }
        },
        //目的地向右添加按钮事件
        MoveCityToRight: function(obj) {
            var obj = FreightPage._getData($(obj).attr("ref"));
            var fromVal = $("#" + obj.ContainerID).find("select[id$=<%=fre_StartDdl.ClientID%>] option:selected").val()
            var value = $("#" + obj.ContainerID).find("select[id$=<%=fre_FromLb.ClientID%>] option:selected").val();
            var text = $("#" + obj.ContainerID).find("select[id$=<%=fre_FromLb.ClientID%>] option:selected").text();

            if (fromVal == value) {
                alert("目的地不能与始发地相同");
                return;
            }
            if (value != undefined) {
                var bool = false;
                $("#" + obj.ContainerID).find("select[id$=<%=fre_ToLb.ClientID%>] option").each(function() {
                    if ($(this).val() == value) {
                        bool = true;
                        return;
                    }
                });
                if (!bool) {
                    $("<option value='" + value + "'>" + text + "</option>").appendTo($("#" + obj.ContainerID).find("select[id$=<%=fre_ToLb.ClientID%>]"));

                    var str = "";
                    $("#" + obj.ContainerID).find("select[id$=<%=fre_ToLb.ClientID%>] option").each(function() {
                        str += $(this).val() + "|" + $(this).text() + ",";

                    });
                    $("#" + obj.ContainerID).find("input[type=hidden][id$=<%=fre_hideToLb.ClientID%>]").val(str);
                } else {
                    alert(text + " 已经存在!");
                }
            }
        },
        //保存事件
        SaveClick: function(type, id) {
            var ischeck = FreightPage.SaveCheck(type, id);
            if (!ischeck) {
                return;
            }
            FreightPage.AjaxSubmit(type, id);

        },
        //表单验证
        SaveCheck: function(type, id) {
            var isBool = true;
            var obj = FreightPage._getData(id);
            //是否来回程
            var isCheck = $("#" + obj.ContainerID).find("input[type=radio][id$=<%=fre_rdo_Back.ClientID %>]").attr("checked");
            //目的地
            var fre_hideToLb = $("#" + obj.ContainerID).find("input[type=hidden][id$=<%=fre_hideToLb.ClientID%>]");
            //开始时间
            var fre_txtPriceBegin = $("#" + obj.ContainerID).find("input[type=text][id$=<%=fre_txtPriceBegin.ClientID%>]");
            //结束时间
            var fre_OfferEnd = $("#" + obj.ContainerID).find("input[type=text][id$=<%=fre_OfferEnd.ClientID%>]")
            //去程适用
            var fre_FromForDay = $("#" + obj.ContainerID).find("input[type=hidden][id$=<%=fre_FromForDay.ClientID%>]");
            //去程独立日期
            var fre_txtIndeDate = $("#" + obj.ContainerID).find("input[type=text][id$=<%=fre_txtIndeDate.ClientID%>]");
            //回程适用
            var fre_hideToForDay = $("#" + obj.ContainerID).find("input[type=hidden][id$=<%=fre_hideToForDay.ClientID%>]");
            //回程独立日期
            var fre_txtIndeDateBack = $("#" + obj.ContainerID).find("input[type=text][id$=<%=fre_txtIndeDateBack.ClientID%>]");
            if (type == "Add") {
                if ($(fre_hideToLb).val() == "") {
                    alert("请选择至少一个目的地!");
                    return;
                }

            }
            if ($.trim($(fre_txtPriceBegin).val()) != "" && $.trim($(fre_OfferEnd).val()) != "") {
                isBool = true;
            } else {
                if ($.trim($(fre_FromForDay).val()) != "" || $.trim($(fre_txtIndeDate).val()) != "") {
                    isBool == true;
                } else {
                    isBool = false;
                    alert("请选择一个去程日期!");
                    return isBool;
                }
            }
            if (isCheck) {
                if ($.trim($(fre_txtPriceBegin).val()) != "" && $.trim($(fre_OfferEnd).val()) != "") {
                    isBool = true;
                } else {
                    if ($.trim($(fre_hideToForDay).val()) != "" || $.trim($(fre_txtIndeDateBack).val()) != "") {
                        isBool == true;
                    } else {
                        isBool = false;
                        alert("请选择一个回程日期!");
                        return isBool;
                    }
                }
            }
            return isBool;
        },
        AjaxSubmit: function(type, id) {
            if (id == undefined) {
                return;
            }

            var obj = FreightPage._getData(id);

            var form = $("#" + obj.ContainerID).find("img[id$=fre_imgBtnSave]").closest("form").get(0);
            var b = false;

            if ($("#" + obj.ContainerID).find("input[type=radio][id$=<%=fre_rdo_Back.ClientID %>]").attr("checked")) {
                b = ValiDatorForm.validator(form, "span");
            } else {
                b = ValiDatorForm.validator2(form, "span", "noauthen", "1", true);
            }
            if (!b) {
                return;
            }

            $("#" + obj.ContainerID).find("span[id$=fre_spanmsg]").html("正在保存...");

            $("#" + obj.ContainerID).find("img[id$=fre_imgBtnSave]").unbind().css("cursor", "default");

            $.newAjax({
                type: "POST",
                url: "FreightManage/FreightAdd.aspx?type=" + type + "&v=" + Math.random(),
                data: $($("#" + obj.ContainerID).closest("form").get(0)).serializeArray(),
                cache: false,
                success: function(state) {
                    switch (state) {
                        case "AddOk": $("#" + obj.ContainerID).find("span[id$=fre_spanmsg]").html("添加成功!"); break;
                        case "UpdateOk": $("#" + obj.ContainerID).find("span[id$=fre_spanmsg]").html("修改成功!"); break;
                        case "error": $("#" + obj.ContainerID).find("span[id$=fre_spanmsg]").html("保存失败,请稍后在试."); break;
                        case "Count": alert("添加数量超过当月可用数!");
                            $("#" + obj.ContainerID).find("span[id$=fre_spanmsg]").html(""); break;
                        default:
                            $("#" + obj.ContainerID).find("span[id$=fre_spanmsg]").html(state);
                            alert(state);
                            break;
                    }

                    setTimeout(function() {
                        $("#" + obj.ContainerID).find("img[id$=fre_imgBtnSave]").css("cursor", "pointer");
                        $("#" + obj.ContainerID).find("img[id$=fre_imgBtnSave]").unbind().bind("click", function() {
                            var type = $(this).attr("rel");
                            var id = $(this).attr("ref");
                            FreightPage.AjaxSubmit(type, id);
                        });
                    }, 2000);
                }, error: function() {
                    $("#" + obj.ContainerID).find("span[id$=fre_spanmsg]").html("保存失败,请稍后在试.");
                    setTimeout(function() {
                        $("#" + obj.ContainerID).find("img[id$=fre_imgBtnSave]").css("cursor", "pointer");
                        $("#" + obj.ContainerID).find("img[id$=fre_imgBtnSave]").click(function() {
                            var type = $(this).attr("rel");
                            var id = $(this).attr("ref");
                            FreightPage.AjaxSubmit(type, id);
                        });
                    }, 2000);
                }
            });
        }
    };
	
</script>




</asp:content>
