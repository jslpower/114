<%@ Page Title="" Language="C#" MasterPageFile="~/master/T4.Master" AutoEventWireup="true"
    CodeBehind="TeamCustomization.aspx.cs" Inherits="SeniorOnlineShop.template4.TeamCustomization" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />
    <style>
        .errmsg
        {
            color: Red;
        }
    </style>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="linetjtk">
        <div class="linetjth">
            团队定制
        </div>
        <div class="linetjxx">
            <table width="665" id="tbl_TeamCustomization" border="0" align="center" cellpadding="0"
                cellspacing="0" style="margin: 20px 5px;">
                <tbody>
                    <tr>
                        <td width="132" align="right" class="jiange2">
                            <strong><font color="red">*</font>计划日期：</strong>
                        </td>
                        <td width="527" align="left" class="jiange2">
                            <input name="PlanDate" id="txtPlanDate" onfocus="WdatePicker()" class="boder" size="25" />
                            <span id="errMsg_txtPlanDate" class="errmsg"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="jiange2">
                            <strong><font color="red">*</font>计划人数：</strong>
                        </td>
                        <td align="left" class="jiange2">
                            <input name="PlanPeopleNum" id="txtPlanPeopleNum" valid="required|RegInteger" errmsg="计划人数不能为空!|请输入整数！"
                                class="boder" size="25" />
                            <span id="errMsg_txtPlanPeopleNum" class="errmsg"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="jiange2">
                            <strong>行程要求：</strong>
                        </td>
                        <td align="left" class="jiange2">
                            <uc1:SingleFileUpload ID="SingleFileUpload1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="jiange2">
                            <strong>住宿要求：</strong>
                        </td>
                        <td align="left" class="jiange2">
                            <textarea name="ResideContent" cols="60"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="jiange2">
                            <strong>用餐要求：</strong>
                        </td>
                        <td align="left" class="jiange2">
                            <textarea name="DinnerContent" cols="60"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="jiange2">
                            <strong>用车要求：</strong>
                        </td>
                        <td align="left" class="jiange2">
                            <textarea name="CarContent" cols="60"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="jiange2">
                            <strong>导游要求：</strong>
                        </td>
                        <td align="left" class="jiange2">
                            <textarea name="GuideContent" cols="60"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="jiange2">
                            <strong>购物要求：</strong>
                        </td>
                        <td align="left" class="jiange2">
                            <textarea name="ShoppingInfo" cols="60"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="jiange2">
                            <strong>其它个性要求：</strong>
                        </td>
                        <td align="left" class="jiange2">
                            <textarea name="OtherContent" cols="60"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="jiange2">
                            <strong><font color="red">*</font>联系人：</strong>
                        </td>
                        <td align="left" class="jiange2">
                            <input name="ContactName" id="txtContactName" valid="required" errmsg="联系人不能为空!"
                                class="boder" size="25" />
                            <span id="errMsg_txtContactName" class="errmsg"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="jiange2">
                            <strong><font color="red">*</font>单位名称：</strong>
                        </td>
                        <td align="left" class="jiange2">
                            <input name="ContactCompanyName" id="txtContactCompanyName" valid="required" errmsg="单位名称不能为空!"
                                class="boder" size="40" />
                            <span id="errMsg_txtContactCompanyName" class="errmsg"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="jiange2">
                            <strong><font color="red">*</font>电话：</strong>
                        </td>
                        <td align="left" class="jiange2">
                            <input name="ContactTel" id="txtContactTel" valid="required|isTel" errmsg="电话不能为空!|电话号码格式不正确！"
                                class="boder" size="40" />
                            <span id="errMsg_txtContactTel" class="errmsg"></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="jiange2">
                            &nbsp;
                        </td>
                        <td align="left" class="jiange2">
                            <div class="tijiao" style="width: 133px;">
                                &nbsp;
                                <input type="button" style="width: 100%;" id="btnAdd" /></div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("T4.line.js") %>"></script>    

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript" language="javascript">
        var TeamCustomization = {
            SingleFileUpload1:<%=SingleFileUpload1.ClientID %>,
            CompanyId:"<%=CompanyId %>",                             
            add: function() { 
                var dataArr= $($("#tbl_TeamCustomization").closest("form").get(0)).serializeArray();
                $("#btnAdd").attr("disabled","disabled");                                     
                $.ajax({
                    type: "POST",
                    data:dataArr,
                    url: "TeamCustomization.aspx?action=add&cid="+TeamCustomization.CompanyId,
                    success: function(html) {
                        var returnMsg=eval(html);                        
                         if(returnMsg)
                         {     
                            alert(returnMsg[0].Message)
                            var _href=window.location.href;
                            window.location.href=_href;
                         }else{                                    
                            alert('对不起，保存失败！')
                         }       
                        $("#btnAdd").removeAttr("disabled"); 
                    },error:function(){                    
                        //$("#btnAdd").attr("disabled",""); 
                    }
                });
            },
            upFile_function:function(fileObj,callBack){                                                       
                 if(fileObj.getStats().files_queued>0)
                 {                                        
                    fileObj.customSettings.UploadSucessCallback = callBack;
                    fileObj.startUpload();
                 }
                 else
                 {
                     callBack();
                 }
            }       
        };
        $(function() {
            FV_onBlur.initValid($("#btnAdd").closest("form").get(0));
            $("#btnAdd").click(function() {
                if($("#txtPlanDate").val()==""){
                    alert("计划日期不能为空！");
                    return;                    
                }
                if(ValiDatorForm.validator($("#btnAdd").closest("form").get(0),"alertspan")){     
                    if(confirm("你确定要提交团队定制信息吗？"))
                        TeamCustomization.upFile_function(TeamCustomization.SingleFileUpload1,TeamCustomization.add);
                }
            });
        })
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="GuidebookPlaceHolder" runat="server">
</asp:Content>
