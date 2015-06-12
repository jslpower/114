<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPhotoAd.aspx.cs" Inherits="SiteOperationsCenter.AdManagement.AddPhotoAd"
    EnableEventValidation="false" ValidateRequest="false" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新增图片广告</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <style type="text/css">
        .style1
        {
            width: 315px;
        }
        .style2
        {
            width: 470px;
        }
    </style>
</head>
<body>
    <form id="form1" name="form1" runat="server">
    <div>
        <table width="100%" border="1" cellpadding="5" cellspacing="0" bordercolor="#B9D3F2">
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    <span class="unnamed1" style="color: Red;">*</span>有效期：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <div id="pnlforver" runat="server">
                        <input type="text" id="DatePicker1" name="DatePicker1" runat="server" />至
                        <input type="text" id="DatePicker2" name="DatePicker2" runat="server" />
                    </div>
                    <asp:CheckBox ID="ckDate" runat="server" Text="是否永久" />
                    <span id="errMsg_DatePicker1" class="errmsg"></span><span id="errMsg_DatePicker2"
                        class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td width="17%" align="right" bgcolor="#D7E9FF">
                    <span class="unnamed1" style="color: Red;">*</span>广告范围：
                </td>
                <td width="83%" align="left" bgcolor="#F7FBFF">
                    <div id="divadvTypes">
                        <span id="range1">
                            <input id="rdoCountry" type="radio" name="advrange" value="全国" onclick="AddPhotoAd.GetAdvRange(1)" />全国</span>
                        <span id="range2">
                            <input id="rdoProvince" type="radio" name="advrange" value="全省" onclick="AddPhotoAd.GetAdvRange(2)" />省份</span>
                        <span id="range3">
                            <input type="radio" id="rdoCity" name="advrange" value="城市" onclick="AddPhotoAd.GetAdvRange(3)" />城市</span>
                        <input type="hidden" id="btnreange" custom="custom1" valid="custom" errmsg="请选择投放范围" />
                        <span id="errMsg_btnreange" class="errmsg"></span>
                    </div>
                    <div id="defaultProvince" style="display: none; width: 500px;">
                    </div>
                    <div id="spanSellCity">
                    </div>
                    <span><a href='javascript:void(0);' style="display: none" onclick='SetOtherSaleCity();return false;'
                        id="selectCity">选择城市</a>&nbsp;&nbsp;<span id="errMsgckSellCity" class="unnamed1"
                            style="display: none"></span></span>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    <span class="unnamed1" style="color: Red;">*</span>广告位置：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <asp:Label ID="ltr_advPostion" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="TrAdvTitle" runat="server">
                <td align="right" bgcolor="#D7E9FF">
                    <span style="color: Red;">*</span>广告标题：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <input type="text" id="txtAdvTitle" name="txtAdvTitle" runat="server" class="style2"
                        valid="required" errmsg="请填写广告标题" />
                    <span id="errMsg_txtAdvTitle" class="errmsg"></span>
                </td>
            </tr>
            <tr id="TrAdvBrief" runat="server">
                <td align="right" bgcolor="#D7E9FF">
                    <span style="color: Red;">*</span>广告简要：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <textarea type="text" id="txtAdvBrief" runat="server" valid="required" errmsg="请填写广告简要"
                        name="txtAdvBrief" class="style2" rows="10"></textarea>
                    <span id="errMsg_txtAdvBrief" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    <span class="unnamed1" style="color: Red;">*</span>类别：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <asp:DropDownList ID="ddlCategory" runat="server" valid="required" errmsg="请选择类别">
                    </asp:DropDownList>
                    <span id="errMsg_ddlCategory" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    <span class="unnamed1" style="color: Red;">*</span><%=ShowStr%>：<br>
                    （建议大小：565px*250px）
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <uc1:SingleFileUpload ID="sfuPhotoImg" runat="server" ImageWidth="565" ImageHight="250"
                        IsUploadSwf="true" />
                    <asp:Literal ID="ltr_ImgSize" runat="server"></asp:Literal>
                    <asp:Literal ID="ltr_ImagePath" runat="server"></asp:Literal>
                    <span id="errMsgsfuPhotoImg" class="errmsg"></span>
                    <input id="hdfoldimgpath" type="hidden" runat="server" />
                </td>
            </tr>
            <tr id="TrAdvThumb" runat="server">
                <td align="right" bgcolor="#D7E9FF">
                    <span class="unnamed1" style="color: Red;">*</span>上传缩略图：<br />
                    （建议大小：78px*48px）
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <uc1:SingleFileUpload ID="sfuSmallPhotoImg" ImageWidth="78" ImageHight="48" runat="server"
                        IsUploadSwf="true" />
                    <asp:Literal ID="ltr_ImgSizeSmall" runat="server"></asp:Literal>
                    <asp:Literal ID="ltr_ImagePathSmall" runat="server"></asp:Literal>
                    <%if (IsShowMoreCell)
                      { %>
                    <span id="errMsgsfuSmallPhotoImg" class="errmsg"></span>
                    <input id="hdfoldimgpathSmall" type="hidden" runat="server" /><%} %>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    网址URl：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <input name="txturl" runat="server" type="text" id="txtUrl" valid="notHttpUrl" errmsg="网址URL填写错误"
                        class="style2" />
                    <span id="errMsg_txtUrl" class="errmsg"></span>
                </td>
            </tr>
            <tr id="divunit1">
                <td align="right" bgcolor="#D7E9FF">
                    <span class="unnamed1" style="color: Red;">*</span>购买单位：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <input type="text" onfocus="return SetBuyComapny()" name="txtBuyUnit" runat="server"
                        id="txtBuyUnit" class="style1" value="请点击选择" valid="required|custom" custom="AddPhotoAd.ValidUnitId"
                        errmsg="请选择购买单位|请选择购买单位" />
                    <span id="errMsg_txtBuyUnit" class="errmsg"></span>
                    <input type="hidden" value="" id="hdfUnitId" runat="server" />
                    <input type="hidden" value="" id="hdfUnitMQ" runat="server" runat="server" />
                </td>
            </tr>
            <tr id="divunit2">
                <td align="right" bgcolor="#D7E9FF">
                    联系方式：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <input type="text" name="txtLink" id="txtLink" runat="server" class="style1" />
                </td>
            </tr>
            <asp:Panel ID="Panel1" runat="server">
                <tr id="tropearid">
                    <td align="right" bgcolor="#D7E9FF">
                        操作人：
                    </td>
                    <td align="left" bgcolor="#F7FBFF">
                        <input type="text" readonly="readonly" name="txtOpearId" id="txtOpearId" runat="server" />
                    </td>
                </tr>
                <tr id="tropeardate">
                    <td align="right" bgcolor="#D7E9FF">
                        操作时间：
                    </td>
                    <td align="left" bgcolor="#F7FBFF">
                        <input type="text" readonly="readonly" name="txtOpeaDate" id="txtOpearDate" runat="server" />
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    &nbsp;
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdfResult" runat="server" />
        <asp:HiddenField ID="hdfAdRange" runat="server" />
        <asp:HiddenField ID="hdfListUrl" runat="server" />
    </div>
    </form>

    <script type="text/javascript">
        var sfu1,sfusmall=false;
        var isSubmit=false;
        
        sfu1 = <%=sfuPhotoImg.ClientID %>;//大图上传控件
        sfusmall=<%=sfuSmallPhotoImg.ClientID %>//缩略图上传控件
        function doSubmit()
        {
            isSubmit = true;
           $("#<%=btnSubmit.ClientID %>").click();  
        }
        $(document).ready(function()
        {  
            if("<%= IsShowMoreCell %>".toLowerCase()=="false")
            {
               $("#txtAdvBrief").removeAttr("valid");
               $("#txtAdvTitle").removeAttr("valid");
            }
            FV_onBlur.initValid($("#<%=btnSubmit.ClientID%>").closest("form").get(0));
            AddPhotoAd.InitDateControl(); //添加日期控件属性
            
    	    $("#<%=btnSubmit.ClientID %>").click(function()
    	    {  
                if(isSubmit)
                {
                  AddPhotoAd.GetResultSell();
                  $("#errMsgsfuPhotoImg").html("");
                  return true;
                }
	            var result= ValiDatorForm.validator($(this).closest("form").get(0),"span"); 
	            if(sfu1.getStats().files_queued<=0)
                {
                    var img=$("#imgpath").attr("href");                  
                    if(img==undefined || img=="")
                    {
                        $("#errMsgsfuPhotoImg").html("请上传大图片");
                        result=false;
                    }        
                }
                if("<%= IsShowMoreCell %>".toLowerCase()=="true" && sfusmall.getStats().files_queued<=0)
                {
                    var img=$("#smallimgpath").attr("href");                  
                    if(img==undefined || img=="")
                    {
                        $("#errMsgsfuSmallPhotoImg").html("请上传缩略图");
                        result=false;
                    }
                }
	            if(!result){  
                    return false;
                }
                else{
                    if(sfu1.getStats().files_queued>0){
                         if("<%= IsShowMoreCell %>".toLowerCase()=="true" && sfusmall.getStats().files_queued>0){
                            sfu1.customSettings.UploadSucessCallback =UploadThumb;
                         }
                         else{
                            alert("1");
                            sfu1.customSettings.UploadSucessCallback =doSubmit; 
                         }
                         sfu1.startUpload(); 
                         return false; 
                    }
                    else if("<%= IsShowMoreCell %>".toLowerCase()=="true" && sfusmall.getStats().files_queued>0){
                         sfusmall.customSettings.UploadSucessCallback =doSubmit; 
                         sfusmall.startUpload(); 
                         return false; 
                    }
                    else{
                        AddPhotoAd.GetResultSell();
                        $("#errMsgsfuPhotoImg").html("");
                        return true;
                    }
                }                      
            });
            
            $("#DatePicker1").focus(function(){
                if("<%=IsDateUpdate %>".toLowerCase()=="true")
                {
                    var self = this; 
                    if("<%=IsInsert %>".toLowerCase()=="true") 
                    {     
                        WdatePicker({
                            onpicked:function(){
                                AddPhotoAd.Dateonpicked(self,$("#form1").get(0));
                            },
                            maxDate:"#F{$dp.$D(\'DatePicker2\')||\'<%= MaxDate %>\'}",
                            minDate:"<%= NowDate%>"  
                        });                   
                    }
                    else{
                         WdatePicker({
                            onpicked:function(){
                                AddPhotoAd.Dateonpicked(self,$("#form1").get(0));
                            }
                        });
                    }
                }else{
                 $(this).attr("readonly","readonly");
                   this.blur();
                }
            });
            $("#DatePicker2").focus(function(){
                if("<%=IsDateUpdate %>".toLowerCase()=="true")
                {            
                    var self = this;
                    if("<%=IsInsert %>".toLowerCase()=="true") 
                    { 
                        WdatePicker({
                            onpicked:function(){
                                AddPhotoAd.Dateonpicked(self,$("#form1").get(0));
                            },
                           minDate:"#F{$dp.$D(\'DatePicker1\')||\'<%=NowDate %>\'}",
                           maxDate:"<%= MaxDate %>"
                        });
                    }else
                    {
                          WdatePicker({
                            onpicked:function(){
                                AddPhotoAd.Dateonpicked(self,$("#form1").get(0));
                            }
                        });
                    }
                 }
                 else{
                    $(this).attr("readonly","readonly");
                      this.blur();
                
                }
            });  
            $("#<%=ckDate.ClientID %>").click(function()
            { 
                if($(this).attr("checked")==false)
                {
                    $("#DatePicker1").attr("valid","required");
                    $("#DatePicker1").attr("errmsg"," 请填写结束时间");
                    $("#DatePicker2").attr("valid","required");
                    $("#DatePicker2").attr("errmsg"," 请填写结束时间");
                    $("#pnlforver").show();
                }else{
                    $("#DatePicker1").removeAttr("valid");
                    $("#DatePicker1").removeAttr("errmsg");
                    $("#DatePicker2").removeAttr("valid");
                    $("#DatePicker2").removeAttr("errmsg");
                    $("#pnlforver").hide();
                }
            });
        });
        var AddPhotoAd=
        {
            
            InitDateControl:function()
            {
                if($("#<%=ckDate.ClientID %>").attr("checked")==false)
                {
                    $("#DatePicker1").attr("valid","required");
                    $("#DatePicker1").attr("errmsg"," 请填写结束时间");
                    $("#DatePicker2").attr("valid","required");
                    $("#DatePicker2").attr("errmsg"," 请填写结束时间");
                }
                 if($("#txtUrl").val()=="javascript:void(0)")
                {
                    $("#txtUrl").val("");
                }
            },
           ValidUnitId:function(e,formelements)
           {
            if($("#hdfUnitId").val()=="" || $("#hdfUnitId").val()==null)
            {
                return false;
            }
            return true;
           },
           UpdateDefaultProvince:function(ids)
           {
                 var bbb = ids.split(",");      
                 $("#defaultProvince").find("input[type='checkbox']").each(function()
                     {
                        var k=$(this).val();
                        for(j=0;j<bbb.length;j++)
                        {
                            if(k=j)
                            {
                                $(this).attr("checked",true);
                            }
                        }                                     
                  });
           },
           GetAdvRange:function(typeid)
           {
               $("#selectCity").hide();     
               $("#defaultProvince").hide(); //默认省份城市
               $("#spanSellCity").hide();
               if(typeid==3)
               {
                   $("#selectCity").show();
                   $("#spanSellCity").show();
               }else if(typeid==2)
               {
                    $("#defaultProvince").show();
               }
           },
            GetResultSell:function()
            {
               //验证是否选择投放城市，获取结果
               $("#divadvTypes").find("input[type='radio']") .each(function()
               {
                   if($(this).attr("checked"))
                   {
                       var values= $(this).val();
                      
                       if(values=="全国")
                       { 
                        $("#<%=hdfAdRange.ClientID %>").val(values);   
                        $("#<%=hdfResult.ClientID %>").val(values);           
                       }
                       else if(values=="全省")
                       {
                         $("#<%=hdfAdRange.ClientID %>").val(values);
                         var provlist=new Array();                 
                         $("#defaultProvince").find("input[type='checkbox']").each(function()
                         {
                            if($(this).attr("checked")==true)
                            {
                                 provlist.push($(this).val());
                            }                   
                         });
                         if (provlist.length <= 0) {
                               alert("请选择投放范围！");                            
                                return;
                          }
                         $("#<%=hdfResult.ClientID %>").val(provlist.toString());
                      }
                      else
                      {
                         $("#<%=hdfAdRange.ClientID %>").val(values);
                         var citylist=new Array();              
                            $("#spanSellCity").find("input[type='checkbox'][name='ckSellCity']:checked").each(function() {
                                citylist.push($(this).val());
                            });
                         if (citylist.length<= 0) {
                                alert("请选择投放范围！");
                                return ;
                         }
                         $("#<%=hdfResult.ClientID %>").val(citylist.toString());
                      }
                  }
               });              
            },
            Dateonpicked:function(ipt,frm,p){
                if (p==null) p = 'errMsg_';
	            var fv = new FormValid(frm);
	            var formElements = frm.elements;
	            var msgs = ValiDatorForm.fvCheck(ipt,fv,formElements);
	            var errmsgend = ipt.getAttribute("errmsgend");
	            if(errmsgend==null){
	                errmsgend = ipt.id;
	            }
	            if (msgs.length>0) {
		            $("#"+p+errmsgend).html(msgs.join(','));
	            } else {
		            $("#"+p+errmsgend).html('');
	            }
            }
        };
        function UploadThumb()
        {      
              sfusmall.customSettings.UploadSucessCallback = doSubmit;
              sfusmall.startUpload(); 
              
        }
        function openDialog(url, title, width, height) {
        Boxy.iframeDialog({ title: title, iframeUrl: url, width: width, height: height, draggable: true, data: null });
         }
        function SetOtherSaleCity(){
            $("#errMsgckSellCity").html("");
            openDialog("/CompanyManage/OtherAllSaleCity.aspx?title="+escape("已选择的投放城市")+"&isall=yes", "设置投放城市",document.documentElement.clientWidth-400,document.documentElement.clientHeight
-260, null);
        }
        function SetBuyComapny()
        {
            openDialog("CompanyListSelect.aspx", "设置购买单位", document.documentElement.clientWidth-200, "380", null);
        }
        function custom1(e,formelements)
        {
            var range=0;
            var type="";
            $("#divadvTypes").find("input[type='radio']") .each(function()
            {
                if($(this).attr("checked"))
                {
                    range++;
                    type=$(this).val();
                }
            });      
            if(range==0)
                return false;
            else 
            {
                 range=0;
                   if(type=="全国")
                   {
                        range=1;   
                   }
                   else if(type=="全省")
                   {
                     var provlist=new Array();                 
                     $("#defaultProvince").find("input[type='checkbox'][name='ckbProvince']:checked").each(function()
                     {
                       range++;            
                     });             
              }
              else
              {
                $("#spanSellCity").find("input[type='checkbox'][name='ckSellCity']:checked").each(function()                         {
                        range++;
                });                 
              }
              if(range==0)
              {
                    return false;                
              }else
              {
                    return true;
              }
            }
        }
        
       
        
    </script>

</body>
</html>
