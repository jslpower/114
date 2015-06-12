<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddWritingAd.aspx.cs" Inherits="SiteOperationsCenter.AdManagement.AddWritingAd" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register assembly="ControlLibrary" namespace="Adpost.Common.DatePicker" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>文字广告</title>
       <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>    
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />    
    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script> 
    <style type="text/css">      
        .style1
        {  width: 315px;
        }       
        .style2
        {
            width: 468px;
            height: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" name="form1" runat="server">
    <div>
        <table width="100%" border="1" cellpadding="5" cellspacing="0" bordercolor="#B9D3F2">           
          <tr>
            <td align="right" bgcolor="#D7E9FF"><span class="unnamed1">*</span>有效期：</td>
            <td align="left" bgcolor="#F7FBFF">
               <div ID="pnlforver" runat="server">
                       <input type="text" id="DatePicker1" name="DatePicker1" runat="server"/>至
                  <input type="text" id="DatePicker2" name="DatePicker2" runat="server"/>
                  </div>
                       <asp:CheckBox ID="ckbDate" runat="server" Text="是否永久" />
                <span id="errMsg_DatePicker1" class="errmsg"></span>
                <span id="errMsg_DatePicker2" class="errmsg"></span></td>
          </tr>
          <tr>
            <td width="17%" align="right" bgcolor="#D7E9FF"><span class="unnamed1">*</span>投放城市： </td>
            <td width="83%" align="left" bgcolor="#F7FBFF">
                <div id="divadvTypes">
                 <span id="range1"><input  id="rdoCountry" type="radio" name="advrange" value="全国"  onclick="AddPhotoAd.GetAdvRange(1)"/>全国</span>
                    <span id="range2"><input  type="radio" name="advrange" value="全省" onclick="AddPhotoAd.GetAdvRange(2)" id="rdoProvince"/>省份</span>
                    <span id="range3"><input type="radio" name="advrange"value="城市" onclick="AddPhotoAd.GetAdvRange(3)" id="rdoCity"/>城市</span>
                
                <input type="hidden" id="btnreange" custom="custom1"  valid="custom" errmsg="请选择投放范围" />
                <span id="errMsg_btnreange" class="errmsg" ></span>
            </div>
                <div id="defaultProvince" style="display:none; width:500px;">
                    <asp:CheckBoxList ID="ckbDefaultProvince" runat="server" RepeatColumns="10" 
                        RepeatDirection="Horizontal" >
                    </asp:CheckBoxList>
                </div>
                 <div id="spanSellCity">     
                             
                </div>
                <span>
                <a href='javascript:void(0);' style="display:none" onclick='SetOtherSaleCity();return false;' id="selectCity">选择城市</a>&nbsp;&nbsp;<span id="errMsgckSellCity" class="unnamed1" style="display: none"></span></span>
              </td>
          </tr>
          <tr>
            <td align="right" bgcolor="#D7E9FF"><span class="unnamed1">*</span>广告位置：</td>
            <td align="left" bgcolor="#F7FBFF">
                <asp:Label ID="ltr_advPostion" runat="server" Text=""></asp:Label>
            </td>
          </tr>
          <tr>
            <td align="right" bgcolor="#D7E9FF"><span class="unnamed1">*</span>类别：</td>
            <td align="left" bgcolor="#F7FBFF">
                <asp:DropDownList ID="ddlCategory" runat="server"  valid="required" errmsg="请选择类别" >
                </asp:DropDownList>
                <span id="errMsg_ddlCategory" class="errmsg"></span>
            </td>
          </tr>
          <tr>
            <td align="right" bgcolor="#D7E9FF"><span class="unnamed1">*</span>标题：</td>
            <td align="left" bgcolor="#F7FBFF">
              <input type="text" name="txtTitle" id="txtTitle" class="style2" runat="server" 
                    valid="required" errmsg="请填写标题"/>    
                <span id="errMsg_txtTitle" class="errmsg"></span>
            </td>
          </tr>
          <tr>
            <td align="right" bgcolor="#D7E9FF"><span class="unnamed1">*</span>正文：</td>
            <td align="left" bgcolor="#F7FBFF"><FCKeditorV2:FCKeditor ID="fckContent" runat="server" 
                    Width="700px" Height="300px" ToolbarSet="Default" ></FCKeditorV2:FCKeditor>
            <span id="errmess_fckContent" class="errmsg"></span> 
            </td>
          </tr>
          <tr id="divunit1">
            <td align="right" bgcolor="#D7E9FF"><span class="unnamed1">*</span>购买单位：</td>
            <td align="left" bgcolor="#F7FBFF"><input type="text" name="txtBuyUnit" id="txtBuyUnit" runat="server" class="style1"  onfocus="return SetBuyComapny()" value="请点击选择" valid="required|custom" custom="AddPhotoAd.ValidUnitIdName" errmsg="请选择购买单位|请选择购买单位"/>
                    <span id="errMsg_txtBuyUnit" class="errmsg"></span>
                    <input type="hidden" runat="server" id="hdfUnitId" value="" />
                    <input type="hidden" value="" id="hdfUnitMQ" runat="server" runat="server" />
            </td>
          </tr>
          <tr id="divunit2">
            <td align="right" bgcolor="#D7E9FF">联系方式：</td>
            <td align="left" bgcolor="#F7FBFF"><input type="text" name="txtContact" 
                    id="txtContact" runat="server" class="style1"  /></td>
          </tr>
            <asp:Panel ID="Panel1" runat="server">
          <tr  id="tropearid">
            <td align="right" bgcolor="#D7E9FF">操作人：</td>
            <td align="left" bgcolor="#F7FBFF"><input type="text" name="txtOperator" 
                    readonly="readonly" id="txtOperator" runat="server"  /></td>
          </tr>
          <tr id="tropeardate">
            <td align="right" bgcolor="#D7E9FF">操作时间：</td>
            <td align="left" bgcolor="#F7FBFF"><input type="text" name="txtOperTime" readonly="readonly" id="txtOperTime" runat="server" /></td>
          </tr>
            </asp:Panel>
          <tr>
            <td align="right" bgcolor="#D7E9FF">&nbsp;</td>
            <td align="left" bgcolor="#F7FBFF">
                <asp:Button ID="btnSubmit" runat="server" Text="提 交"  OnClientClick="return AddPhotoAd.SubmitData()"
                    onclick="btnSubmit_Click" />             
            </td>
        </tr>
        </table>
        <asp:HiddenField ID="hdfResult" runat="server" />
        <asp:HiddenField ID="hdfAdRange" runat="server" />
        <asp:HiddenField ID="hdfListUrl" runat="server" />
        <input type="hidden" id="hdfProvince" value="" />
    </div>        
    </form>
    <script type="text/javascript">
      
        $(document).ready(function()
        {  
          FV_onBlur.initValid($("#<%=btnSubmit.ClientID%>").closest("form").get(0),"span");
                        
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
                    }else
                    {
                        WdatePicker({
                            onpicked:function(){
                                AddPhotoAd.Dateonpicked(self,$("#form1").get(0));
                            }
                        });
                    }
                }
                else
                {
                    $(this).attr("readonly",true);
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
                }else
                {
                    $(this).attr("readonly",true);
                      this.blur();
                }
            });   
             $("#<%=ckbDate.ClientID %>").click(function()
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
                if($("#<%=ckbDate.ClientID %>").attr("checked")==false)
                {
                    $("#DatePicker1").attr("valid","required");
                    $("#DatePicker1").attr("errmsg"," 请填写结束时间");
                    $("#DatePicker2").attr("valid","required");
                    $("#DatePicker2").attr("errmsg"," 请填写结束时间");
                }
            },
           SubmitData:function()
           {
	           var a= ValiDatorForm.validator($("#form1").get(0),"span"); 
	              //内容验证
	              
	          var oEditor = FCKeditorAPI.GetInstance('fckContent');
              var dataContent= oEditor.GetHTML().replace(/<(?!img).*?>|&nbsp;/g,"").replace(/\s/gi,'');
              if(dataContent.length<1)              
	           {
	                $("#errmess_fckContent").html("请填写正文");	                
                     a=false
	           }
	           else
               {
                        $("#errmess_fckContent").html(""); 
                       AddPhotoAd.GetResultSell();
	            }
               if(!a)
               {
            
                 return false;	            
               }else{
                  return true;
               }
           },
          ValidUnitIdName:function(e,formelements)
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
                         $("#hdfProvince").val($("#defaultProvince").html());
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
            openDialog("CompanyListSelect.aspx", "设置购买单位",document.documentElement.clientWidth-200, "380", null);
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
                    $("#spanSellCity").find("input[type='checkbox'][name='ckSellCity']:checked").each(function() {
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
