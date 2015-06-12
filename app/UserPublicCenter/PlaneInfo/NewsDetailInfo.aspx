<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsDetailInfo.aspx.cs"
    MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" Inherits="UserPublicCenter.PlaneInfo.NewsDetailInfo" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Register Src="../WebControl/AdveControl.ascx" TagName="AdveControl" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="Main" ID="Default_ctMain" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gongqiu") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
	.addInf_con td,.addInf_con th{
	padding-left:50px;
}
        .style1
        {
            height: 100px; width:300px;
        }      
        #<%=rdoType.ClientID%> label
        {
        	margin-right:13px;
        }
    </style>
    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>  
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td valign="top">
                <table width="970" border="0" cellspacing="0" cellpadding="0" style="margin-right: 10px;">
                    <tr>
                        <td valign="top" style="border: 1px solid #C4C4C4; padding: 1px;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="table-layout: fixed;padding: 10px;
                                background: url(<%=ImageServerPath %>/images/UserPublicCenter/bg_new.gif) repeat-x;">
                                <tr>
                                    <td class="huise1" align="left">
                                        <div id="div_Title" runat="server">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">
                                        <h1>
                                            <asp:Label ID="lbl_Title" runat="server"></asp:Label>
                                        </h1>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="border-bottom: 1px dashed #ccc; text-align: left;" class="huise">
                                                    <asp:Label ID="lbl_Time" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="heise" style="padding-top: 15px;word-break: break-all; white-space: normal;
                        word-wrap: break-word;">
                                        <div id="divShow" style="text-align: center" runat="server">
                                            <img id="img_NewsPic" runat="server" width="600" height="400" /><br /></div>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Literal ID="lit_Content" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <div runat="server" id="divPlaneAppl" visible="false">
                                        <table id="tbPlaneAppl" width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8" bgcolor="#FAFAFA" class="addInf_con" >
                                        <tr>
                                          <th height="30" colspan="2" align="left">航班类型：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:RadioButtonList ID="rdoType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:RadioButtonList>                               
                                            </th>
                                        </tr>
                                        <tr>
                                          <td width="35%" height="30" align="left"><font color="#FF0000">*</font>乘机人数：</td>
                                          <td align="left"><input name="txtPersonCount" type="text" id="txtPersonCount"  size="10" runat="server" errmsg="请填写正整数" custom="NewsDetailInfo.CheckIsNumber"  valid="custom" /><span id="errMsg_ctl00_Main_txtPersonCount" class="errmsg" ></span></td>
                                        </tr>
                                        <tr>
                                          <td width="35%" height="30" align="left"><font color="#FF0000">*</font>出发时间：</td>
                                          <td align="left"><input type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'%y-%M-#{%d}'})" valid="required" errmsg="请填写出发时间" id="DatePicker1" name="DatePicker1" runat="server" onchange="NewsDetailInfo.DateControlChange(this)"/>
                                              <img style="position:relative; left:-24px; top:3px; *top:1px;"  onclick="javascript:$('#ctl00_Main_DatePicker1').focus();"  src="<%=ImageServerPath %>/images/time1.gif" width="16" height="13" /><span id="errMsg_ctl00_Main_DatePicker1" class="errmsg" ></span></td>
                                        </tr>
                                        <tr id="trEndDate">
                                          <td width="35%" height="30" align="left"><font color="#FF0000">*</font>返程时间：</td>
                                          <td align="left"><input type="text" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',minDate:'%y-%M-#{%d}'})" valid="required" errmsg="请填写返程时间" id="DatePicker2" name="DatePicker2" runat="server" onchange="NewsDetailInfo.DateControlChange(this)"/>
                                             <img style="position:relative; left:-24px; top:3px; *top:1px;"  onclick="javascript:$('#ctl00_Main_DatePicker2').focus();"  src="<%=ImageServerPath %>/images/time1.gif" width="16" height="13" /><span id="errMsg_ctl00_Main_DatePicker2" class="errmsg" ></span></td>
                                        </tr>
                                        <tr>
                                          <td width="35%" height="30" align="left">乘客类型：</td>
                                          <td align="left">
                                              <asp:RadioButtonList ID="rdoPassengerType" runat="server" 
                                                  RepeatDirection="Horizontal">
                                              </asp:RadioButtonList></td>
                                        </tr>
                                        <tr>
                                          <td width="35%" height="120" align="left">备注：</td>
                                          <td align="left"><textarea name="txarRemark" id="txarRemark" cols="60"  
                                                  runat="server" class="style1"></textarea></td>
                                        </tr>
                                         <tr>
                                           <th height="40" colspan="2" align="left"><span style="color:#FF0000; font-size:14px;">采购商资料</span></th>
                                           </tr>
                                         <tr>
                                           <td width="35%" height="30" align="left"><font color="#FF0000">*</font>公司名字：</td>
                                           <td align="left"><input type="text" name="txtStockCompanyName" id="txtStockCompanyName" errmsg="请填写公司名字" valid="required" runat="server" /><span id="errMsg_ctl00_Main_txtStockCompanyName" class="errmsg" ></span></td>
                                         </tr>
                                         <tr>
                                           <td width="35%" height="30" align="left"><font color="#FF0000">*</font>联系人：</td>
                                           <td align="left"><input name="txtStockLinkName" type="text" id="txtStockLinkName" size="15" errmsg="请填写联系人" valid="required" runat="server"/><span id="errMsg_ctl00_Main_txtStockLinkName" class="errmsg"></span></td>
                                         </tr>
                                         <tr>
                                           <td width="35%" height="30" align="left"><font color="#FF0000">*</font>联系电话：</td>
                                           <td align="left"><input type="text" name="txtStockPhone" id="txtStockPhone"  errmsg="请填写电话号码|电话号码格式错误" valid="required|isPhone" runat="server" /><span id="errMsg_ctl00_Main_txtStockPhone" class="errmsg"></span></td>
                                         </tr>
                                         <tr>
                                           <td width="35%" height="30" align="left"><font color="#FF0000">*</font>联系地址：</td>
                                           <td align="left"><input name="txtStockAddress" type="text" id="txtStockAddress" size="40" errmsg="请填写联系人" valid="required" runat="server" /><span id="errMsg_ctl00_Main_txtStockAddress" class="errmsg"></span></td>
                                         </tr>
                                        <tr>
                                          <th height="30" colspan="2" align="left"><table width="80%" border="0" cellspacing="0" cellpadding="0">
                                              <tr>
                                                <td align="left"><font color="#FF0000">温馨提醒：</font></td>
                                                <td>&nbsp;</td>
                                              </tr>
                                          </table></th>
                                        </tr>
                                        <tr>
                                          <td colspan="2" align="left"><table width="90%" border="0" align="left" cellpadding="0" cellspacing="0">
                                              <tr>
                                                <td height="30" align="left">1.申请团队时如果没有乘客名单，请拨打  <strong><font color="#FF0000">0571-56893761</font></strong><%=Utils.GetMQ("27440", "廖小姐")%>
                        徐先生  13685750464</td>
                                              </tr>
                                              <tr>
                                                <td height="30" align="left">2.同业114工作时间：周一至周五（工作日）9：00－17：45</td>
                                              </tr>
                                              <tr>
                                                <td height="30" align="left">3.提交申请时请将乘客信息填写完整；</td>
                                              </tr>
                                              <tr>
                                                <td height="30" align="left">4.如有其他要求请在“备注”栏内写明。</td>
                                              </tr>
                                          </table></td>
                                        </tr>
                                      </table>
                                        <table align="center" width="80%" border="0" cellspacing="0" cellpadding="0">
                                          <tr align="center">
                                            <td width="44%" height="40" align="center"><img style="cursor:pointer" id="btnSaveInfo" src="<%=ImageServerPath %>/images/UserPublicCenter/bc_btn.jpg" width="88" height="26" onclick=" return NewsDetailInfo.SaveInfo()" /></td>
                                            <td width="56%" align="center"><a href="PlaneNewsList.aspx?TypeID=<%=TypeID %>&CityId=<%=CityId %>"><img src="<%=ImageServerPath %>/images/UserPublicCenter/qx_btn.jpg" width="88" height="26" /></a></td>
                                          </tr>
                                      </table>
                                      </div>
                <input type="hidden" id="hidTitle" runat="server" />                    
                <input type="hidden" id="hidNewId" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <%--<td valign="top" width="250" >
                <uc2:AdveControl ID="AdveControl1" runat="server" />
            </td>--%>
        </tr>
    </table>
    <script type="text/javascript">
        var SaveCount=0;
        var   alltime=1;  
        var stimer; //计时器
        var htmlstr2="<div id=\"divplaneboxy\" style=\"width:200px; height:80px;\"><span id=\"spnResult\" style=\" font-size:15px; font-weight:bold\">正在保存...</span><br/><br/><a id=\"planeClose\" href=\"javascript:void(0)\" onclick=\"NewsDetailInfo.CloseBoxy(this)\" style=\" bottom:5px; right:5px;\">关闭</a></div>";
        var NewsDetailInfo={
            AlertSaveResult:function(result,errstr)
            {   
                $("#planeClose").show(); //保存结果显示弹出层  关闭按钮
                if(result=="1")
                {
                    $("#spnResult").html("保存成功");
                     $("#ctl00_Main_rdoType_0").attr("checked","checked");
                     $("#<%=txarRemark.ClientID %>").val("");
                     $("#ctl00_Main_rdoPassengerType_0").attr("checked","checked");
                     $("#tbPlaneAppl").find("input[type='text']") .each(function(){
                        $(this).val("");
                     }); 
                    stimer=setInterval("NewsDetailInfo.BoxyBlurSetTime()",1000)                
                }else if(result=="3")
                {
                    $("#spnResult").html(errstr);
                }else{
                     $("#spnResult").html("保存失败");
                }
                
                NewsDetailInfo.DisplayEndDateControl();
            },
          CloseBoxy:function(obj){
                Boxy.get(obj).hide(); 
	            SaveCount=0;    //没有打开了一个窗口
                 $("#btnSaveInfo").bind("click",function(){
                    NewsDetailInfo.SaveInfo();
                });
            },
            BoxyBlurSetTime:function(){
              if   (alltime<1){  
                NewsDetailInfo.CloseBoxy($("#planeClose"));
              clearInterval(stimer);  
              }  
              else
              {  
              alltime--;  
              }    
            },
          SaveInfo:function(){
                var form = $("#btnSaveInfo").closest("form").get(0);       
	            if(ValiDatorForm.validator(form,"span"))
	            { 
	                var startdate=$("#<%=DatePicker1.ClientID%>").val();
                    var enddate=$("#<%=DatePicker2.ClientID%>").val();
                    if(NewsDetailInfo.compareDate(startdate,enddate)==false)
                    {
                         $("#errMsg_<%=DatePicker2.ClientID%>").html("出发或返程时间填写错误");
                         return false;    
                    }                    
	                if(SaveCount==0)  //判断是否已经打开了一个窗口
	                {
	                   new Boxy(htmlstr2, {title: "保存结果",closeable: false,unloadOnHide: true}); 
	                    $("#btnSaveInfo").bind("click",function(){
                            return false;
                        });
                        $("#planeClose").hide(); //正在保存隐藏弹出层  关闭按钮
                        SaveCount++;
	                   $.ajax(
	                   { 
	                     url:"NewsDetailInfo.aspx",
	                     data:$(form).serialize().replace(/&Input=/,'')+"&method=save",
                         cache:false,
                         type:"post",
                         dataType:"json",
                         success:function(msg){    
                            NewsDetailInfo.AlertSaveResult(msg.success,msg.message);                            
                         },
                         error:function(){ 
                            NewsDetailInfo.AlertSaveResult(0,"保存失败");
                         }
	                   });
	                 } 
	             }
	            return false;
          },
         DateControlChange:function(obj){    //出发和返程时间判断
            $("#<%=rdoType.ClientID%>").find("input[type='radio']").each(function(){
                if($(this).attr("checked")==true)
                {
                    if($(this).val()!="单程"){          
                        var spnerr=$(obj).attr("id");
                        var startdate=$("#<%=DatePicker1.ClientID%>").val();
                        var enddate=$("#<%=DatePicker2.ClientID%>").val();
                        var result;
                        var resultstr;
                        if(spnerr=="<%= DatePicker1.ClientID%>")  //第一个时间空间与第二个时间比较
                        {
                           if($("#<%=DatePicker2.ClientID%>").val()!="")
                            {
                                result=NewsDetailInfo.compareDate(startdate,enddate);
                                resultstr="出发时间不能大于返程时间";
                            }
                        }else           //第二个时间空间与第一个时间比较
                        {
                            if($("#<%=DatePicker1.ClientID%>").val()!="")
                            {
                                result= NewsDetailInfo.compareDate(startdate,enddate);
                                resultstr="返程时间不能小于出发时间";
                            }
                        }
                        if(result==false){
                              $("#errMsg_"+spnerr).html(resultstr);       
                        }else{
                              $("#errMsg_<%= DatePicker2.ClientID%>").html("");
                              $("#errMsg_<%= DatePicker1.ClientID%>").html("");
                        }
                    }else{ //清空 错误提示内容
                        if($("#<%=DatePicker1.ClientID%>").val()!=""){
                            $("#errMsg_<%= DatePicker1.ClientID%>").html("");
                        }
                    }
                }
            });
         },
        CheckIsNumber:function(e,formelements)  //验证乘机人数
        {
            var checkvalue=$.trim(e.value);
            if(checkvalue!="")            
            {
                var part=/^[0-9]*[1-9][0-9]*$/  // /^[+]?\d+(\d+)?$/; 
                if(part.exec(checkvalue)){
                    return true;
                }else{
                      return false;
                }
            }else{
                return false;
            }
        },
      compareDate:function(DateOne,DateTwo){
            var OneMonth = DateOne.substring(5,DateOne.lastIndexOf ("-"));
            var OneDay = DateOne.substring(DateOne.length,DateOne.lastIndexOf ("-")+1);
            var OneYear = DateOne.substring(0,DateOne.indexOf ("-"));

            var TwoMonth = DateTwo.substring(5,DateTwo.lastIndexOf ("-"));
            var TwoDay = DateTwo.substring(DateTwo.length,DateTwo.lastIndexOf ("-")+1);
            var TwoYear = DateTwo.substring(0,DateTwo.indexOf ("-"));

            if (Date.parse(OneMonth+"/"+OneDay+"/"+OneYear) >
            Date.parse(TwoMonth+"/"+TwoDay+"/"+TwoYear)) {
                return false;
            }
            else{
                return true;
            }
        },
        DisplayEndDateControl:function()    
        {
            $("#<%=rdoType.ClientID%>").find("input[type='radio']").each(function(){
                $("#<%=DatePicker2.ClientID %>").val("");
                $("#errMsg_<%=DatePicker1.ClientID %>").html("");
                $("#errMsg_<%=DatePicker2.ClientID %>").html("");
                if($(this).attr("checked")==true)
                {
                    if($(this).val()=="单程"){
                       $("#trEndDate").hide();     //隐藏返程时间行
                       $("#<%=DatePicker2.ClientID %>").removeAttr("valid");
                       $("#<%=DatePicker2.ClientID %>").removeAttr("errmsg");
                    }
                    else{  //反之
                         $("#trEndDate").show();
                         $("#<%=DatePicker2.ClientID %>").attr("valid","required");
                         $("#<%=DatePicker2.ClientID %>").attr("errmsg","请填写返程时间"); 
                    }
                }
            });
        },
        IsPlaneAppl:function()      //运价参考，加载表单验证js
        {
            if($("#<%=divPlaneAppl.ClientID %>").length>0)
            {
                 FV_onBlur.initValid( $("#btnSaveInfo").closest("form").get(0));
                 //如果选择单程，返程时间为
                 $("#<%=rdoType.ClientID%>").find("input[type='radio']").each(function(){             //给航班类型单选按钮绑定change事件
                    $(this).bind("click",function(){
                       NewsDetailInfo.DisplayEndDateControl(); 
                    });                    
                 });
                 NewsDetailInfo.DisplayEndDateControl();
            }
        }   
    }
        $(document).ready(function(){
           NewsDetailInfo.IsPlaneAppl();       //表单化验证
        });
    </script>
</asp:Content>
