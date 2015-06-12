<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddScenic.aspx.cs" Inherits="SiteOperationsCenter.ScenicManage.AddScenic"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>景区添加</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("json2.js") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <style type="text/css">
        #txt_CnAddress
        {
            width: 365px;
        }
        #txt_EnAddress
        {
            width: 365px;
        }
        #txt_CompanyId
        {
            width: 266px;
        }
        #txt_ScenicName
        {
            width: 361px;
        }
        #txt_EnName
        {
            width: 361px;
        }
        #txt_Telephone
        {
            width: 361px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="80%" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#CCCCCC"
        class="lr_hangbg table_basic">
        <tr>
            <td width="16%" align="right">
                <span class="unnamed1">*</span>公司编号：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_CompanyId" id="txt_CompanyId" runat="server" valid="required"
                    errmsg="请输入公司编号！" />
                <span id="errMsg_txt_CompanyId" class="errmsg"></span>
                <input id="BindScenicContact" runat="server" type="button" value="获取景区联系人" onclick="BindScenicContactf();" />
                <span class="unnamed1">*</span>景区联系人：<asp:DropDownList ID="ScenicContact" runat="server">
                </asp:DropDownList>
                <input id="hid_Operator" type="hidden" runat="server" />
            </td>
        </tr>
        <tr>
            <td width="16%" align="right">
                <span class="unnamed1">*</span>景区名称：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_ScenicName" id="txt_ScenicName" runat="server" valid="required"
                    errmsg="请输入景区名字长度在1-28！" />
                <span id="errMsg_txt_ScenicName" class="errmsg"></span>
            </td>
        </tr>
        <tr>
            <td align="right">
                英文名称：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_EnName" id="txt_EnName" runat="server" errmsg="请输入景区英文名称长度在1-100！"
                    onblur="this.value=ignoreSpaces(this.value);" />
                <%--<span id="errMsg_txt_EnName" class="errmsg"></span>--%>
            </td>
        </tr>
        <tr>
            <td align="right">
                地图信息：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                (<label id="X" runat="server"></label>,<label id="Y" runat="server"></label>)
                <input type="button" name="Setmap" id="Setmap" value="地图选择" />
                <asp:HiddenField runat="server" ID="jingdu" />
                <asp:HiddenField runat="server" ID="weidu" />
            </td>
        </tr>
        <%--        <tr>
            <td align="right">
                联系人：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_ContactOperator" id="txt_ContactOperator" runat="server"
                    errmsg="请输入联系人！" valid="required" />
                <span id="errMsg_txt_ContactOperator" class="errmsg"></span>
            </td>
        </tr>--%>
        <tr>
            <td align="right">
                客服电话：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_Telephone" id="txt_Telephone" runat="server" />
                <%--<span id="errMsg_txt_Telephone" class="errmsg"></span>
                <span id="error_txt_Telephone"  class="errmsg" style="display: none">请输入真确的客服电话</span>--%>
            </td>
        </tr>
        <tr>
            <td align="right">
                国家代码：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input id="ProvinceText" type="hidden" runat="server" />
                <input id="CityText" type="hidden" runat="server" />
                <input id="CountryText" type="hidden" runat="server" />
                <input id="ProvinceId" type="hidden" runat="server" />
                <input id="CityId" type="hidden" runat="server" />
                <input id="CountryId" type="hidden" runat="server" />
                <span class="unnamed1" id="ProvinceRequired" style="display: none">*</span>省份：<asp:DropDownList
                    ID="ddl_ProvinceList" runat="server">
                </asp:DropDownList>
                <span class="unnamed1" id="CityRequired" style="display: none">*</span>城市：<asp:DropDownList
                    ID="ddl_CityList" runat="server">
                </asp:DropDownList>
                <span class="unnamed1" id="CountyRequired" style="display: none">*</span>县区：<asp:DropDownList
                    ID="ddl_CountyList" runat="server">
                </asp:DropDownList>
                <span id="errMsg_Province" style="display: none" class="unnamed1">请选择省份-城市-县区</span>
                <span id="errMsg_City" style="display: none" class="unnamed1">请选择城市</span> <span
                    id="errMsg_CountyList" style="display: none" class="unnamed1">请选择县区</span>
            </td>
        </tr>
        <tr>
            <td align="right">
                地标区域：
            </td>
            <td width="84%" align="left" bgcolor="#FFFFFF" id="Td_ChbLankId">
                <asp:Repeater ID="ChbLankId" runat="server">
                    <ItemTemplate>
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td align="right">
                中文地址：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_CnAddress" id="txt_CnAddress" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                英文地址：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_EnAddress" id="txt_EnAddress" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <span class="unnamed1">*</span>主题：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=strchb%>
                <span id="errMsg_ThemeId" style="display: none; color: Red;">请至少选择一个主题且最多选择3个</span>
                <%--                <asp:Repeater ID="chbThemeId" runat="server">
                    <ItemTemplate>
                        <label for="labThemeId_<%#Eval("ThemeId") %>">
                            <%# Eval("ThemeName") %><input type="checkbox" name="chbTheme" id="chbThemeId_<%#Eval("ThemeId") %>"
                                value="<%#Eval("ThemeId") %>" /></label>
                    </ItemTemplate>
                </asp:Repeater>--%>
            </td>
        </tr>
        <tr>
            <td align="right">
                景区等级：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:DropDownList ID="DdlHotelStar" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                成立年份：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input name="txt_SetYear" id="txt_SetYear" type="text" size="6" runat="server" />
                年
            </td>
        </tr>
        <tr>
            <td align="right">
                日常开放时间：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <textarea name="txt_OpenTime" id="txt_OpenTime" cols="50" rows="3" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">
                景区详细介绍：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <FCKeditorV2:FCKeditor ID="txt_Description" ToolbarSet="Default" Height="420px" runat="server">
                </FCKeditorV2:FCKeditor>
            </td>
        </tr>
        <tr>
            <td align="right">
                交通说明：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <textarea name="txt_Traffic" id="txt_Traffic" cols="50" rows="5" runat="server" onclick="return txt_Traffic_onclick()"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">
                周边设施：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <textarea name="txt_Facilities" id="txt_Facilities" cols="50" rows="5" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">
                B2B显示控制：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:DropDownList ID="DdlB2B" runat="server" errmsg="请选择" valid="required">
                </asp:DropDownList>
                <span class="unnamed1">*</span>
                <input id="txt_B2BOrder" name="txt_B2BOrder" type="text" runat="server" value="50" />
                （1~50）正向排序，默认50（易诺管理员控制）
            </td>
        </tr>
        <tr>
            <td align="right">
                B2C显示控制：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:DropDownList ID="DdlB2C" runat="server">
                </asp:DropDownList>
                <span class="unnamed1">*</span>
                <input id="txt_B2COrder" size="10" name="txt_B2COrder" runat="server" type="text"
                    value="50" />
                （1~50）正向排序，默认50（易诺管理员控制）
            </td>
        </tr>
        <tr>
            <td align="right">
                <span class="unnamed1">*</span>审核：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:DropDownList ID="DdlStatus" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="50" colspan="2" align="center" bgcolor="#FFFFFF">
                <asp:Button ID="btnSave" class="baocun_an" runat="server" Text="保 存" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
    <br />

    <script type="text/javascript">
    
    
    
    //设置公司经纬度
    $("#Setmap").click(function() {
   
            var Y=$("#weidu").val();
            var X=$("#jingdu").val();
            var url = "SetGoogleMap.aspx?weidu="+Y+"&jindu="+X+"";
            var title = "设置地图";
            Boxy.iframeDialog({ title: title, iframeUrl: url, height: 800, width: 900, draggable: false })
            return false;
    });
    
    //绑定地表区域
    function BinLankId()
    {
        $("#Td_ChbLankId").html("");
        var v=$("#ddl_CityList option:selected").val();
        $.ajax({
             url: "AjaxAll.ashx?type=BinLankId&arg="+v,
             cache: false,
             type: "post",
             success: function(result) {
                 if(result=="error")
                 {
                 }
                 else if(result=="NoLogin")
                 {
                    alert("你还未登录");
                 }
                 else{
                     var list = eval(result);
                     
                     for(var i=0;i<list.length;i++)
                     {
                         $("#Td_ChbLankId").append("<input type=\"checkbox\" name=\"chkboxLankid\" value=\""+list[i].Id+"\" id=\"cbxl_"+i+"\" /><label for=\"cbxl_"+i+"\">"+list[i].Por+"</label>"); 
                         if(i%5==0&&i>0)
                         {
                            $("#Td_ChbLankId").append("<br>");
                         }
                    }
                }
             },
             error: function() {
                 alert("操作失败!");
             }    
        });
    }
    
    function SetProvince(ProvinceId) {
        $("#<%=ddl_ProvinceList.ClientID %>").attr("value", ProvinceId);
    }
    function SetCity(CityId) {
        $("#<%=ddl_CityList.ClientID %>").attr("value", CityId);
    }
    function SetCounty(CountyId) {
        $("#<%=ddl_CountyList.ClientID %>").attr("value", CountyId);
    }
    
    
    function BindScenicContactf()
    {
        $("#ScenicContact").html("");
        $("#BindScenicContact").val("正在获取....");
        $("#BindScenicContact").attr("disabled","disabled");
        $("#<%=btnSave.ClientID %>").attr("disabled","disabled");
        var companyid=$("#txt_CompanyId").val();
        $.ajax({
             url: "AjaxAll.ashx?type=BindContact&arg="+companyid,
             cache: false,
             type: "post",
             success: function(result) {
             if(result=="error")
             {
                alert("没有该公司编号的成员，请检查公司编号是否有误");
             }
             else if(result=="NoLogin")
             {
                alert("你还未登录");
             }
             else
             {
                  var argumentlist=result.toString().split('$');
                  $("#hid_Operator").val(argumentlist[1]);
                  var list = eval(argumentlist[0]);
                  for(var i=0;i<list.length;i++)
                  {
                    $("#ScenicContact").append("<option value='"+list[i].UserId+"'>"+list[i].UserName+"</option>"); 
                  }

             }
                  $("#BindScenicContact").val("获取景区联系人");
                  $("#BindScenicContact").attr("disabled","");
                  $("#<%=btnSave.ClientID %>").attr("disabled","");
               
             },
             error: function() {
                 alert("操作失败!");
                 $("#BindScenicContact").val("获取景区联系人");
                 $("#BindScenicContact").attr("disabled","");
                 $("#<%=btnSave.ClientID %>").attr("disabled","");
             }    
        });
    
    }
    
    //修改状态下 绑定景区联系人
   $(function(){
          if('<%=ScenicIdAndComId %>'!=null)
          {
            if('<%=ScenicIdAndComId[0] %>'!="")
                BindScenicContactf();
          }  
    });
   

    
    var isSubmit = false; //区分按钮是否提交过
    //模拟一个提交按钮事件    
    function doSubmit(){
         isSubmit = true;
         $("#<%=btnSave.ClientID%>").click();
    }
    
    $(function(){
        $("#<%=btnSave.ClientID%>").click(function(){
            if(isSubmit){
            //如果按钮已经提交过一次验证，则返回执行保存操作
                return true;
            }
            //验证表单
//            var isValidator = ValiDatorForm.validator($("#form1").get(0), "span");
//            if(!isValidator)
//                return false;
            //验证服务电话
            if($("#<%=ScenicContact.UniqueID %>").val()==null)
            {
                alert("请获取景区联系人");
                return false;
            }
            
            var scenicname=$("#txt_ScenicName").val();
            if(scenicname.length>28&&scenicname.length>0){
                alert("请输入景区名称长度在1-28");
                $("#txt_ScenicName").focus();
                return false;
            }
            
//            var EnName=$("#txt_EnName").val();
//            if(EnName.length>100&&EnName.length>0){
//                alert("请输入英文名称长度在1-100");
//                $("#txt_EnName").focus();
//                return false;
//            }

            
//            var pattern = new RegExp("[^ ' "\!@#\$%^&\*~ ',\. "]");
//            var EnName=$("#txt_EnName").val();
//            alert(EnName);
//            if(!pattern.test(EnName))
//            {
//                alert("英文名称中含有特殊字符");
//                $("#txt_EnName").focus();
//                return false;
//            }
            
            var Telephone=$("#txt_Telephone").val();
            if(Telephone.length>50&&Telephone.length>0){
                alert("请输入客服电话长度在1-50");
                $("#txt_Telephone").focus();
                return false;
            }
            
            
            
            var CnAddress=$("#txt_CnAddress").val();
            if(CnAddress.length>60&&CnAddress.length>0){
                alert("请输入中文地址长度在1-60");
                $("#txt_CnAddress").focus();
                return false;
            }
            
//            var EnAddress=$("#txt_EnAddress").val();
//            if(EnAddress.length>200&&EnAddress.length>0){
//                alert("请输入英文地址长度在1-200");
//                $("#txt_EnAddress").focus();
//                return false;
//            }
            
            
            
            //省份-城市-县区
//            if($("#<%=ddl_ProvinceList.UniqueID %>").val()=="0"||$("#<%=ddl_CityList.UniqueID %>").val()=="0"||$("#<%=ddl_CountyList.UniqueID %>").val()=="0")
//            {
//                $("#errMsg_Province").show();
//                return false;
//            }
//            else
//                $("#errMsg_Province").hide();
            
            
            var OpenTime=$("#txt_OpenTime").val();
            if(OpenTime.length>200&&OpenTime.length>0){
                alert("请输入日常开放时间长度在1-200");
                $("#txt_OpenTime").focus();
                return false;
            }
            
            
            
            
//            var tel=$("#txt_Telephone").val();
//            var IstelNumber=/(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)/; 
//            if(!IstelNumber.test(tel))
//            {
//                $("#error_txt_Telephone").show();
//                return false;
//            }

            //验证主题            
            var arr=$("input[type=checkbox][name='chbTheme']:checked").length
            if (arr == 0||arr>3) {
                $("#errMsg_ThemeId").show();
                return false;
            } else {
                $("#errMsg_ThemeId").hide();
            } 
            //验证成立年份
//            var DdlHotelStar=$("#<%=DdlHotelStar.UniqueID %>").val();
//            if(DdlHotelStar=="0")
//            {
//                $("#errMsg_DdlHotelStar").show();
//                return false;
//            }
//            else
//            $("#errMsg_DdlHotelStar").hide();
            
            var SetyearToInt=parseInt($("#txt_SetYear").val());
            var dat=new Date().getFullYear();
            if($("#txt_SetYear").val()!=""){
            if(!(/^[0-9]+$/.test($("#txt_SetYear").val()))||SetyearToInt>dat)
            {
                alert("请输入正确的成立年份");
                return false;
            }}
            //验证B2BandB2C
            var B2BOrderToInt=parseInt($("#txt_B2BOrder").val());
            var B2COrderToInt=parseInt($("#txt_B2COrder").val());
            if(B2BOrderToInt>99||B2BOrderToInt<1||$("#txt_B2BOrder").val()=="")
            {
                alert("B2B请输入1-99之间的数字");
                return false;
            }
            if(B2COrderToInt>99||B2COrderToInt<1||$("#txt_B2COrder").val()=="")
            {
                alert("B2C请输入1-99之间的数字");
                return false;
            }
            
            if($("#<%=DdlStatus.UniqueID %>").val()=="0")
            {
                $("#errMsg_DdlStatus").show();
                return false;
            }
        });
	    FV_onBlur.initValid($("#form1").get(0));
    });
    function txt_Traffic_onclick() {

    }

    </script>

</body>
</html>
