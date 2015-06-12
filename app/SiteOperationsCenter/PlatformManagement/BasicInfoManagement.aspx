<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasicInfoManagement.aspx.cs"
    Inherits="SiteOperationsCenter.PlatformManagement.BasicInfoManagement" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="../usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>基本信息</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script language="JavaScript">
  function mouseovertr(o) {
	  o.style.backgroundColor="#FFF9E7";
      //o.style.cursor="hand";
  }
  function mouseouttr(o) {
	  o.style.backgroundColor=""
  }
  //-----------------对区域联系人的管理START-----------------
    var isFirst = true;
    var isTrue = false;
    var   img   =  null;   
    var   s   ="";
    //添加一行
    function AddCententInfo()
    {
        var myTable =$("#tb_Content");
        var s=[];
        s.push("<tr>");
        s.push('<td>负责区域<input name="txt_Area"  type="text" style="width:60px" />&nbsp;类别<select name="sel_ContentType"><option value="0">销售</option><option value="1">客服</option></select>&nbsp;联系人<input  name="txt_ContentPeople" type="text" style="width:50px"/>&nbsp;电话<input name="txt_ContentPhone" type="text" style="width:58px" />&nbsp;手机<input  name="txt_ContentTel" type="text" style="width:58px" />&nbsp;QQ<input  name="txt_ContentQQ" type="text" style="width:58px" />&nbsp;MQ<input  name="txt_ContentMQ" type="text" style="width:58px" />');
        s.push('<input type="button" value="删除" onclick="D(this)" ></td>');
        s.push("</tr>")
        
        myTable.append(s.join(''));
        
    }
    //删除一行
    function D(obj) {
        $(obj).parent().parent().remove();
    }
  
    //-------------------------------对区域联系人的管理END-----------------------------
    </script>

</head>
<body>
    <form id="form1" name="form1" runat="server">
    <table width="100%" border="0" align="center" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <fieldset>
                    <legend><strong>联盟基本信息</strong></legend>
                    <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="lr_bg"
                        style="margin-top: 5px;">
                        <tr class="lr_hangbg">
                            <td class="lr_shangbg" style="width: 13%" align="right">
                                <span class="unnamed1">* </span>平台名称：
                            </td>
                            <td align="left">
                                <input id="txt_SiteOpeName" name="txt_SiteOpeName" maxlength="250" type="text" class="textfield"
                                    valid="required" errmsg="请输入平台名称！" size="40" runat="server" /><span id="errMsg_txt_SiteOpeName"
                                        class="errmsg"></span>
                            </td>
                        </tr>
                        <tr class="lr_hangbg">
                            <td align="right" class="lr_shangbg">
                                平台首页logo：
                            </td>
                            <td align="left">
                                <uc1:SingleFileUpload ID="SingleFileUpload1" runat="server" ImageWidth="170" ImageHeight="70" />
                                <%=img_Path %>
                                <span id="errMsg_hid2" class="errmsg"></span>
                            </td>
                        </tr>
                        <tr class="lr_hangbg">
                            <td align="right" class="lr_shangbg">
                                联系人：
                            </td>
                            <td align="left">
                                <input runat="server" maxlength="50"  id="txt_ContentName" name="txt_ContentName" type="text" class="textfield"
                                    size="40" />
                            </td>
                        </tr>
                        <tr class="lr_hangbg">
                            <td align="right" class="lr_shangbg">
                                电话：
                            </td>
                            <td align="left">
                                <input runat="server"  maxlength="50"  id="txt_ContentPhoneNum" name="txt_ContentPhoneNum" type="text"
                                    class="textfield" size="40" />
                            </td>
                        </tr>
                        <tr class="lr_hangbg">
                            <td align="right" class="lr_shangbg">
                                手机：
                            </td>
                            <td align="left">
                                <input runat="server" maxlength="50" id="txt_ContentTelNum" name="txt_ContentTelNum" type="text"
                                    class="textfield" size="40" />
                            </td>
                        </tr>
                        <tr class="lr_hangbg">
                            <td align="right" class="lr_shangbg">
                                MQ：
                            </td>
                            <td align="left">
                                <input runat="server" maxlength="30"  id="txt_CMQ" name="txt_CMQ" type="text" class="textfield" size="10" />
                            </td>
                        </tr>
                        <tr class="lr_hangbg">
                            <td align="right" class="lr_shangbg">
                                QQ：
                            </td>
                            <td align="left">
                                <input runat="server"  maxlength="30"  id="txt_CQQ" name="txt_CQQ" type="text" class="textfield" size="10" />
                            </td>
                        </tr>
                        <tr class="lr_hangbg">
                            <td align="right" class="lr_shangbg">
                                MSN：
                            </td>
                            <td align="left">
                                <input runat="server" maxlength="40"  id="txt_CMSN" name="txt_CMSN" type="text" class="textfield"
                                    size="10" />&nbsp;
                            </td>
                        </tr>
                        <tr class="lr_hangbg">
                            <td align="right" class="lr_shangbg">
                                平台介绍：
                            </td>
                            <td align="left">
                                <FCKeditorV2:FCKeditor ID="txt_UnionInfo" Height="300px" ToolbarSet="Default" runat="server">
                                </FCKeditorV2:FCKeditor>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <div style="height: 15px;">
                </div>
                <div style="height: 15px;">
                </div>
                <fieldset>
                    <legend><strong>其他信息</strong></legend>
                    <table width="98%" align="center" cellpadding="2" cellspacing="1" class="lr_bg" style="margin-top: 5px;">
                        <tr class="lr_hangbg">
                            <td width="13%" align="right" class="lr_shangbg">
                                区域联系人信息：
                            </td>
                            <td width="87%" align="left">
                                <table id="tb_Content">
                                    <asp:Repeater ID="rpt_ContentPeople" runat="server">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr id="tr_auto">
                                                <td>
                                                    负责区域<input name="txt_Area" type="text" style="width: 60px" value='<%# DataBinder.Eval(Container.DataItem, "SaleArea")%>' />
                                                    <%# GetSalType(DataBinder.Eval(Container.DataItem, "SaleType").ToString())%>
                                                    联系人<input name="txt_ContentPeople" type="text" style="width: 50px" value='<%# DataBinder.Eval(Container.DataItem, "ContactName")%>' />
                                                    电话<input name="txt_ContentPhone" type="text" style="width: 58px" value='<%# DataBinder.Eval(Container.DataItem, "ContactTel")%>' />
                                                    手机<input name="txt_ContentTel" type="text" style="width: 58px" value='<%# DataBinder.Eval(Container.DataItem, "ContactMobile")%>' />
                                                    QQ<input name="txt_ContentQQ" type="text" style="width: 58px" value='<%# DataBinder.Eval(Container.DataItem, "QQ")%>' />
                                                    MQ<input name="txt_ContentMQ" type="text" style="width: 58px" value='<%# DataBinder.Eval(Container.DataItem, "MQ")%>' />
                                                    <input id="btnDel" type="button" value="删除" onclick="D(this)" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <tr id="tr_Add">
                                        <td>
                                            负责区域<input name="txt_Area" type="text" style="width: 60px" />
                                            类别<select name="sel_ContentType"><option value="0">销售</option>
                                                <option value="1">客服</option>
                                            </select>
                                            联系人<input name="txt_ContentPeople" type="text" style="width: 50px" />
                                            电话<input name="txt_ContentPhone" type="text" style="width: 58px" />
                                            手机<input name="txt_ContentTel" type="text" style="width: 58px" />
                                            QQ<input name="txt_ContentQQ" type="text" style="width: 58px" />
                                            MQ<input name="txt_ContentMQ" type="text" style="width: 58px" />
                                            <input id="btnAddCentent" type="button" value="增加" onclick="AddCententInfo()" />
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <span id="errMsg_hid" class="errmsg"></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="lr_hangbg">
                            <td align="right" class="lr_shangbg">
                                首页版权：
                            </td>
                            <td align="left">
                                <FCKeditorV2:FCKeditor ID="txt_IndexInfo" ToolbarSet="Default" runat="server">
                                </FCKeditorV2:FCKeditor>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <table width="25%" height="30" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <asp:Button ID="btnAdd" runat="server" Text="保存" CssClass="baocun_an" OnClick="btnAdd_Click" />
                <input type="hidden" runat="server" id="hdfAgoImgPath" name="hdfAgoImgPath" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript">
        var sfu1 =<%=SingleFileUpload1.ClientID %>;
//      function custom2(){
//        var flag=true;
//        if(sfu1.getStats().files_queued<=0&&$("#SingleFileUpload1_hidFileName").val()=="")
//        {   
//              flag=false;
//              return false;
//        }
//        return flag;
//    };
    var isSubmit = false; //区分按钮是否提交过
    function doSubmit(){
    isSubmit = true;
     $("#<%=btnAdd.ClientID%>").click();
    }
    $(function(){
	    $("#<%=btnAdd.ClientID %>").click(function(){
	     if(isSubmit){
            //如果按钮已经提交过一次验证，则返回执行保存操作
                return true;
            }
	        var a= ValiDatorForm.validator($("#form1").get(0),"span");
	        if(a){
	        if(sfu1.getStats().files_queued>0){
	        //如果验证成功，则提交按钮保存事件
                sfu1.customSettings.UploadSucessCallback = doSubmit;
                sfu1.startUpload();
            }
            else{
                  return true;
              }
            }
            return false;
	    });
	    FV_onBlur.initValid($("#form1").get(0));
	
    });
    </script>

</body>
</html>
