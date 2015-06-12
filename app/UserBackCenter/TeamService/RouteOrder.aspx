<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteOrder.aspx.cs" Inherits="UserBackCenter.TeamService.RouteOrder" %>


<%@ Import Namespace="EyouSoft.Common" %>
<html xmlns="http://www.w3.org/1999/xhtml"  >
<head runat="server">
    <title>无标题页</title>
   <% 
       if (!Page.IsPostBack)
       {
           ro_urlReferrer.Value = Request.UrlReferrer.Host;
       }
       
       if (Request.Url.HostNameType == UriHostNameType.Dns)
       {
           if (!Request.Url.ToString().ToLower().Contains("localhost"))
           {
               if (ro_urlReferrer.Value != Request.Url.Host)
               {%>
           <script type="text/javascript">
             document.domain="tongye114.com";
           </script>
        <%}
           }
  } %>
           

   
      <style type="text/css">
.errmsg{
color:#FF0000;
 }
</style>

<link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" ><div ><input type="hidden" id="ro_urlReferrer"  value="" runat="server"/>
    <table width="100%" border="0" cellspacing="0" cellpadding="6">
  <tr>
    <td align="right" class="tdleft"><strong>线路名称：</strong></td>
    <td width="68%" align="left" class="td2left"><a  href="<%=Utils.GetTeamInformationPagePath(tourId)%>" target="_blank" ><%=routeName %></a> <strong>出发日期：</strong><%= startDate %>&nbsp;<%=weekDay %> &nbsp;<strong>当前剩余空位：<span class="lv14" id="ro_spanRemain"><%=remain %></span></strong></td>
 
    <td width="14%" align="left" class="td2left"><a href="../同行/fax.htm" target="_blank"></td>
    </tr>
  <tr>
    <td width="18%" align="right" class="tdleft"><strong>发布单位：</strong></td>
    <td colspan="2" align="left" class="td2left"><%=company %>&nbsp;&nbsp; <strong><br />
      联系人</strong>：<%= contant %> <strong>电话</strong>：<%=tel%><strong> 联系MQ：</strong> <%=Utils.GetBigImgMQ(MQ)%></td>
  </tr>
  
 <tr>
    <td align="right" class="tdleft"><strong>出港城市：</strong></td>
    <td colspan="2" align="left" class="td2left">
       <label><%=leaveCity %></label>
	</td>
  </tr>
  
  <tr>
    <td align="right" class="tdleft"><strong>交通安排：</strong></td>
    <td colspan="2" align="left" class="td2left"><textarea  id="ro_traffic" cols="60"  rows="2"  runat="server" disabled="disabled"></textarea></td>
  </tr>
  <tr id="ro_tr_price">
    <td align="right" class="tdleft"><strong>价格组成：</strong></td>
    <td colspan="2" align="left" class="td2left">
    <asp:Repeater ID="ro_rpt_priceList" runat="server" 
            onitemdatabound="ro_rpt_priceList_ItemDataBound">
     <ItemTemplate>
    <%--  <span>
	  <input name='ro_rdiPriceStandId' type="radio" id="changgui"  value='<%# Eval("PriceStandId")%>' checked="checked"  onclick="RouteOrder.selectPrice(this)"/>
	  <label for="changgui"><%#Eval("PriceStandName") %></label> 
	  <span style="display:none" class="ro_noselect">成人价￥<label>1600</label>&nbsp; 儿童价￥<label>1400</label>&nbsp;单房差￥<label>360</label></span>
	  <span class="ro_select">
	  成人数<input name="ro_txtManCount1" id="ro_txtManCount1" runat="server" onchange="RouteOrder.changePeopleCount(this,'1')" type="text" sourceCount="6" value="6" size="2" style="border:1px solid #7F9DB9;"/>×单价<input name="ro_txtManPrice1" id="ro_txtManPrice1" type="text" readonly="readonly"  value="1600" size="4" style="border:1px solid #7F9DB9; color:Gray"/><span style="font-size:20px; font-weight:bold; color:#cc0000;">+</span>
	  儿童数<input name="ro_txtChildCount1" id="ro_txtChildCount1" onchange="RouteOrder.changePeopleCount(this,'0')" sourceCount="0" type="text" value="0" size="2" style="border:1px solid #7F9DB9;"/>×单价<input name="ro_txtChildPrice1" id="ro_txtChildPrice1" type="text" value="1200"  readonly="readonly" size="4" style="border:1px solid #7F9DB9;color:Gray"/><span style="font-size:20px; font-weight:bold; color:#cc0000;">+</span>
	  单房差<input name="ro_txtOneRoomCount1" onchange="RouteOrder.ro_change(this)" id="ro_txtOneRoomCount1" type="text" value="2" size="2" style="border:1px solid #7F9DB9;"/>×单价<input name="ro_txtOneRoomPrice1" id="ro_txtOneRoomPrice1" type="text" value="360" size="3" style="border:1px solid #7F9DB9;color:Gray"/>+
	  其它费用<input name="ro_txtOtherPrice1" onchange="RouteOrder.ro_change(this)" id="ro_txtOtherPrice1" type="text" value="2" size="4" style="border:1px solid #7F9DB9;"/></span>
   </span>--%>
     </ItemTemplate>
    </asp:Repeater>
   
<%--	<br/>
	<span>
	  <input name="radiobutton" type="radio" id="changgui2" value="radiobutton"  onclick="RouteOrder.selectPrice(this)"/>
	  <label for="changgui2">三星</label> 
	  <span class="ro_noselect">成人价￥<label>3200</label>&nbsp; 儿童价￥<label>2000</label>&nbsp;单房差￥<label>400</label></span>
	  <span style="display:none" class="ro_select">
	  成人数<input name="ro_txtManCount2"  id="ro_txtManCount2" onchange="RouteOrder.changePeopleCount(this,'1')" sourceCount="6" type="text" value="6" size="2" style="border:1px solid #7F9DB9;"/>×单价<input name="ro_txtManPrice2" id="ro_txtManPrice2" readonly="readonly" type="text" value="3200" size="4" style="border:1px solid #7F9DB9;color:Gray"/><span style="font-size:20px; font-weight:bold; color:#cc0000;">+</span>
	  儿童数<input name="ro_txtChildCount2" id="ro_txtChildCount2" onchange="RouteOrder.changePeopleCount(this,'0')" sourceCount="0" type="text" value="3" size="2" style="border:1px solid #7F9DB9;"/>×单价<input name="ro_txtChildPrice2" id="ro_txtChildPrice2" type="text" readonly="readonly" value="2000" size="4" style="border:1px solid #7F9DB9;color:Gray"/><span style="font-size:20px; font-weight:bold; color:#cc0000;">+</span>
	  单房差<input name="ro_txtOneRoomCount2"  id="ro_txtOneRoomCount2" onchange="RouteOrder.ro_change(this)" type="text" value="2" size="2" style="border:1px solid #7F9DB9;"/>×单价<input name="ro_txtOneRoomPrice2" id="ro_txtOneRoomPrice2" type="text" value="400" size="3" style="border:1px solid #7F9DB9;color:Gray"/>+
	  其它费用<input name="ro_txtOtherPrice2" id="ro_txtOtherPrice2" onchange="RouteOrder.ro_change(this)" type="text" value="2" size="4" style="border:1px solid #7F9DB9;"/></span>
	</span>
	<br/>
	<span>
	<input name="radiobutton" type="radio" id="changgui3" value="radiobutton"  onclick="RouteOrder.selectPrice(this)"/>
	<label for="changgui3">四星</label>
	<span class="ro_noselect">成人价￥<label>6400</label>&nbsp; 儿童价￥<label>5500</label>&nbsp;单房差￥<label>500</label></span>
	<span style="display:none" class="ro_select">
	成人数<input name="ro_txtManCount3" id="ro_txtManCount3" onchange="RouteOrder.changePeopleCount(this,'1')" sourceCount="6" type="text" value="6" size="2" style="border:1px solid #7F9DB9;"/>×单价<input name="ro_txtManPrice3" id="ro_txtManPrice3" readonly="readonly" type="text" value="6400" size="4" style="border:1px solid #7F9DB9;color:Gray"/><span style="font-size:20px; font-weight:bold; color:#cc0000;">+</span>
	儿童数<input name="ro_txtChildCount3" id="ro_txtChildCount3" onchange="RouteOrder.changePeopleCount(this,'0')" type="text" sourceCount="0" value="3" size="2" style="border:1px solid #7F9DB9;"/>×单价<input name="ro_txtChildPrice3" id="ro_txtChildPrice3" type="text" readonly="readonly" value="5500" size="4" style="border:1px solid #7F9DB9;color:Gray"/><span style="font-size:20px; font-weight:bold; color:#cc0000;">+</span>
	单房差<input name="ro_txtOneRoomCount3" id="ro_txtOneRoomCount3" onchange="RouteOrder.ro_change(this)" type="text"  value="2" size="2" style="border:1px solid #7F9DB9;"/>×单价<input name="ro_txtOneRoomPrice3" id="ro_txtOneRoomPrice3" type="text" value="500" size="3" style="border:1px solid #7F9DB9;color:Gray"/>+
	其它费用<input name="ro_txtOtherPrice3" id="ro_txtOtherPrice3" onchange="RouteOrder.ro_change(this)" type="text" value="2" size="4" style="border:1px solid #7F9DB9;"/></span>
	</span>--%>
	
	</td>
  </tr>
  
  
  <tr>
    <td align="right" class="tdleft"><strong>特殊要求说明：</strong></td>
    <td colspan="2" align="left" class="td2left"><textarea name="ro_txtSpecialContent"  cols="80" rows="2">
</textarea></td>
  </tr>
  
  <tr>
    <td align="right" class="tdleft"><strong>总金额：</strong></td>
    <td colspan="2" align="left" class="td2left"><input name="ro_txtSumPrice" id="ro_txtSumPrice" type="text" readonly="readonly" runat="server" style="height:20px; width:100px; text-align:right; font-size:16px; font-weight:bold;" value="0"/></td>
    </tr>
</table>
<table width="100%" border="0" cellpadding="3" cellspacing="0" bgcolor="#FEF7CD" style="border:1px solid #D8BC81; margin-top:8px;">
  <tr>
    <td width="21%" align="right"><strong>操作留言：</strong></td>
    <td width="79%" align="left"><textarea name="ro_txtOperatorContent" cols="60"></textarea></td>
  </tr>
  <tr>
    <td align="right"><strong>批发商操作：</strong></td>
    <td align="left">
	<input type="submit" name="ro_SaveOrder" value="提交预订" onclick="return RouteOrder.bookOrder()"  runat="server" id="ro_SaveOrder" visible="false"/>
	</td>
  </tr>
</table>

<table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td height="30" align="left"><strong>可出机票游客信息</strong></td>
        </tr>
  </table>
   <div id="div_tblCustomerList"></div>
<table id="tblCustomerList" cellspacing="1" cellpadding="0" width="100%" align="center" bgcolor="#93B5D7" border="0" sortcol="5">
        <tbody>
     
        <tr align="middle" bgcolor="#f9f9f4">
            <td width="4%" height="20" bgcolor="#D4E6F7">序号</td>
            
            <td width="7%" bgcolor="#D4E6F7">姓&nbsp;&nbsp; 名</td>
            <td width="12%" bgcolor="#D4E6F7">证件名称</td>
            <td width="15%" bgcolor="#D4E6F7">证件号码</td>
            <td width="6%" bgcolor="#D4E6F7">性别</td>
            <td width="8%" bgcolor="#D4E6F7">类型</td>
            <td width="8%" bgcolor="#D4E6F7">联系电话</td>
            <td width="5%" bgcolor="#D4E6F7">座位号</td>
            <td width="26%" bgcolor="#D4E6F7">备注</td>
          </tr>
      
       <%   
           for (int i = 1; i <= adultNum; i++)
         {%>
       <tr bgcolor="#ffffff">
            <td align="middle" bgcolor="#EDF6FF"><span size="3"><%=customerNo%></span></td>
            <td align="middle" bgcolor="#EDF6FF"><input size="8"  name="CustomerName<%=customerNo%>" value="游客" /></td>
            <td align="middle" bgcolor="#EDF6FF"><select id="CertificateName1" name="CertificateName<%=customerNo%>">
                <option value="0" selected="selected">请选择证件</option>
                <option value="1">身份证</option>
                <option value="2">户口本</option>
                <option value="3">军官证</option>
                <option value="4">护照</option>
                <option value="5">边境通行证</option>
                <option value="6">其他</option>
            </select></td>
            <td align="middle" bgcolor="#EDF6FF"><input name="CertificateNo<%=customerNo%>" id="CertificateNo1" size="20" /></td>
            <td align="middle" bgcolor="#EDF6FF"><select id="CustomerSex1" style="position:relative" name="CustomerSex<%=customerNo%>">
                <option value="1" selected="selected" >男</option>
                <option value="0">女</option>
            </select></td>
            <td align="middle" bgcolor="#EDF6FF">
             <%-- <select id="CustomerType1" name="CustomerType<%=customerNo%>">
                <option value="1" selected="selected">成人</option>
                <option value="0">儿童</option>
             </select>--%>
              <label>成人</label><input type="hidden" name="CustomerType<%=customerNo%>" value="1" />
            </td>
            <td align="middle" bgcolor="#EDF6FF"><input name="CustomerTelphone<%=customerNo%>" id="CustomerTelphone1" size="15" /></td>
            <td align="middle" bgcolor="#EDF6FF"><input name="CustomerSiteNo<%=customerNo%>" id="CustomerSiteNo"   size="5" /></td>
            <td align="middle" bgcolor="#EDF6FF"><input name="CustomerRemark<%=customerNo%>" id="CustomerRemark1"  /></td>
          </tr>
          <%  customerNo++;
         }
         for (int i = 1; i <= childNum; i++)
         {
                  %>
          <tr bgcolor="#ffffff">
            <td align="middle" bgcolor="#EDF6FF"><span size="3"><%=customerNo%></span></td>
            <td align="middle" bgcolor="#EDF6FF"><input size="8"  name="CustomerName<%=customerNo%>" value="游客"/></td>
            <td align="middle" bgcolor="#EDF6FF"><select id="Select1" name="CertificateName<%=customerNo%>">
                <option value="0" selected="selected">请选择证件</option>
                <option value="1">身份证</option>
                <option value="2">户口本</option>
                <option value="3">军官证</option>
                <option value="4">护照</option>
                <option value="5">边境通行证</option>
                <option value="6">其他</option>
            </select></td>
            <td align="middle" bgcolor="#EDF6FF"><input name="CertificateNo<%=customerNo%>" id="Text1"  size="20" /></td>
            <td align="middle" bgcolor="#EDF6FF"><select id="Select2" name="CustomerSex<%=customerNo%>">
                <option value="1" selected="selected">男</option>
                <option value="0">女</option>
            </select></td>
            <td align="middle" bgcolor="#EDF6FF">
            <%--  <select id="Select3"  readonly='readonly' name="CustomerType<%=customerNo%>">
                <option value="1" >成人</option>
                <option value="0" selected="selected">儿童</option>
             </select>--%>
             <label>儿童</label><input type="hidden" name="CustomerType<%=customerNo%>" value="0" />
            </td>
            <td align="middle" bgcolor="#EDF6FF"><input name="CustomerTelphone<%=customerNo%>" id="Text2"  size="15" /></td>
            <td align="middle" bgcolor="#EDF6FF"><input name="CustomerSiteNo<%=customerNo%>" id="Text3"   size="5" /></td>
            <td align="middle" bgcolor="#EDF6FF"><input name="CustomerRemark<%=customerNo%>" id="Text4"  /></td>
          </tr>
          <% customerNo++;
         } %>
    
        </tbody>
      </table></div>
    </form>
      <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
     <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>
      <script type="text/javascript" src="<%=JsManage.GetJsFilePath("select") %>"></script>
        <script type="text/javascript">
         try{ document.execCommand('BackgroundImageCache',false,true); }catch(e){}
        $(document).ready(function(){
          $("#ro_tr_price input[type='text']").focus(function(){
            $(this).select();
          });
         if ( $.browser.msie )
           if($.browser.version=="6.0")
           { 
             window.parent.$('#<%=Request.QueryString["iframeId"] %>').get(0).contentWindow.document.body.onscroll=RouteOrder.scroll1;
             
             
           }
        });

   var RouteOrder=
   { 
     funHandleIeLayout:null,
     isScroll:false,
     handleIELayout:function(){
        if(RouteOrder.isScroll){
            RouteOrder.isScroll = false;
            document.body.style.display="none";
            document.body.style.display="";
            clearInterval(RouteOrder.funHandleIeLayout);
            RouteOrder.funHandleIeLayout = null;
        }
     },
     scroll1:function()
     { 
        RouteOrder.isScroll = true;
        clearInterval(RouteOrder.funHandleIeLayout);
        RouteOrder.funHandleIeLayout=setInterval(RouteOrder.handleIELayout,300);
    },
    changeSelect:function(){
       $("select").each(
       function(){
        if($(this).next("div").length==0)
        $(this).sSelect();
       })
  },
  //计算总价格
  totalCount:function(manText,childText,roomText){
       var manInput=manText?manText:$("#ro_tr_price").find("span.ro_select:visible").children("[id*='ManCount']");
       var childInput=childText?childText:manInput.siblings("[id*='ChildCount']");
       var oneRoom=roomText?roomText:$("#ro_tr_price").find("span.ro_select:visible").children("[id*='OneRoomCount']");
       var totalPrice=manInput.val()*manInput.siblings("[id*='ManPrice']").val()+childInput.val()*childInput.siblings("[id*='ChildPrice']").val()+oneRoom.val()*oneRoom.siblings("[id*='OneRoomPrice']").val()+parseFloat(oneRoom.siblings("[id*='OtherPrice']").val());
       $("#<%=ro_txtSumPrice.ClientID %>").val(parseFloat(totalPrice));
   },
  
  //切换价格组成
  selectPrice:function(tar_rdi){
       var sourceSelect=$(tar_rdi).parent("span").siblings("span").children("span.ro_select:visible");
       var manCount=sourceSelect.children("input:[id*='ManCount']").val();
       var manSourceCount=sourceSelect.children("input:[id*='ManCount']").attr("sourceCount");
       var childCount=sourceSelect.children("input:[id*='ChildCount']").val();
       var childCourceCount=sourceSelect.children("input:[id*='ChildCount']").attr("sourceCount");
       var oneRoom=sourceSelect.children("input:[id*='Room']").val();
       var otherPrice=sourceSelect.children("input:[id*='Other']").val();
       $(tar_rdi).parent("span").find("span.ro_noselect").css("display","none").siblings("span").css("display","").children("input:[id*='ManCount']").val(manCount).attr("sourceCount",manSourceCount).siblings("input:[id*='ChildCount']").val(childCount).attr("sourceCount",childCourceCount).siblings("input:[id*='Room']").val(oneRoom).siblings("input:[id*='Other']").val(otherPrice).removeAttr("disabled").siblings("input").removeAttr('disabled');
       $(tar_rdi).parent("span").siblings("span").children("span.ro_noselect").css("display","");
       $(tar_rdi).parent("span").siblings("span").children("span.ro_select").css("display","none").find("input").attr("disabled","disabled");
       this.totalCount();
  },
  //改变人数
  changePeopleCount:function(tar_txt,p_type){
     var value=$(tar_txt).val().replace(/\s*/g,'');
     if(value=='')
     {
       $(tar_txt).val(0);
       $(tar_txt).change();
       return;
     }
     if(/^\d+$/.test(value))
     {  
        if(parseInt(value)<0)
        {
          alert("请输入有效人数!");
          $(tar_txt).val($(tar_txt).attr("sourceCount"));
          return false;
        }
        var otherCount=0;
        if(/ManCount/.test($(tar_txt).attr("id")))
        {
          otherCount=$(tar_txt).siblings("input[id*='ChildCount']").val();
        }
        else 
         if(/ChildCount/.test($(tar_txt).attr("id")))
         {
           otherCount=$(tar_txt).siblings("input[id*='ManCount']").val();
         }
         if((parseInt(otherCount)+parseInt(value))>parseInt($("#ro_spanRemain").html()))
         {
            alert("人数超过!");
            $(tar_txt).val($(tar_txt).attr("sourceCount"));
            return false;
         }
        var cha=parseInt(value-$(tar_txt).attr("sourceCount"));
        if(cha==0) return;//如果人数没有变化则返回
         document.getElementById("div_tblCustomerList").innerHTML="<span id='span_in'>正在生成游客信息列表，请稍后……</span>";
         setTimeout(function(){
          if(cha>0)
            RouteOrder.peopleListChange(cha,p_type,"add");//增加游客
          else
          {  
           RouteOrder.peopleListChange(Math.abs(cha),p_type,"remove");//减少游客
          }
         $(tar_txt).attr("sourceCount",value);
         var sortNum=0;
         $("#tblCustomerList tbody").children("tr").each(
          function()
          { 
            $(this).children("td:first").children("span").html(sortNum);//修改序号
             sortNum++
          });
          RouteOrder.totalCount();
          document.getElementById("div_tblCustomerList").innerHTML="";},100);
       }
     else
     {
        alert("请输入有效人数!");
        $(tar_txt).val($(tar_txt).attr("sourceCount"));
        return false;
     }
    },
  //修改单房差和其他费用
  ro_change:function(tar_input){
       var value=$(tar_input).val().replace(/\s*/g,'');
       if(value=="")
            $(tar_input).val(0);
       if($(tar_input).attr("name")=="ro_txtOtherPrice")
       {    
           
            if(parseFloat($(tar_input).val()).toString()=="NaN")
            {
              alert("请输入有效数字!");
              $(tar_input).val($(tar_input).attr("sourceCount"));
            }
            else
            {
              this.totalCount();
              $(tar_input).attr("sourceCount",$(tar_input).val());
            }
            
       }
        else
        {
          if(/^\d+$/.test($(tar_input).val()))
          {
             this.totalCount();
             $(tar_input).attr("sourceCount",$(tar_input).val());
          }
          else
          { 
             alert("请输入有效人数!");
             $(tar_input).val($(tar_input).attr("sourceCount"));
          }
        }
     
      return false;
  },
//  //修改人数类型(成人,儿童)
//  customerTypeChanged:function(tar_sel){
//      var manInput=$("#ro_tr_price").find("span.ro_select:visible").children("[id*='ManCount']")
//      var childInput=manInput.siblings("[id*='ChildCount']");
//      if($(tar_sel).val()=="1")
//      {
//          manInput.attr("value",parseInt(manInput.attr("value"))+1).attr("sourceCount",parseInt(manInput.attr("sourceCount"))+1);
//          childInput.attr("value",parseInt(childInput.attr("value"))-1).attr("sourceCount",parseInt(childInput.attr("sourceCount"))-1);
//      }
//      else
//      {  
//         manInput.attr("value",parseInt(manInput.attr("value"))-1).attr("sourceCount",parseInt(manInput.attr("sourceCount"))-1);
//         childInput.attr("value",parseInt(childInput.attr("value"))+1).attr("sourceCount",parseInt(childInput.attr("sourceCount"))+1);
//      }
//      this.totalCount(manInput,childInput);
//  },
  
  bookOrder:function(){
    var adultCount=$("#ro_tr_price").find("span.ro_select:visible").children("[id*='ManCount']").val().replace(/\s*/gi,'');
    var childCount=$("#ro_tr_price").find("span.ro_select:visible").children("[id*='ChildCount']").val().replace(/\s*/gi,'');
   if(!/^\d+$/i.test(adultCount)||!/^\d+$/i.test(childCount))
    { 
     alert("请输入有效人数!");
     return false;
    }
    if(adultCount+childCount<1)
    {
      alert("游客人数必须大于0!");
      return false;
    }
    if(parseInt($("#ro_spanRemain").html())<(parseInt(adultCount)+parseInt(childCount)))
     {
       alert("人数超过!");
       return false;
     }
   if($("#tblCustomerList tbody").find("tr").length!=(parseInt(adultCount)+parseInt(childCount)+1))
   {
    alert("正在生成游客信息,请稍后再提交!");
    return false;
    }
   
  },
  
  //改变预定人员列表
  peopleListChange:function(num,type,method){
    if(method=="remove")
    {  
       var plist=$("#tblCustomerList tbody").find("input[name*='CustomerType'][value='"+type+"']").closest("tr");
       for(var i=0;i<num;i++)
       {
         $(plist.get(i)).remove();
       }
    }
    else if(method="add")
    {  
      
       var plast=$("#tblCustomerList tbody");
       var i=1;
       var i1=plast.find("tr:last").find("[name*='CustomerSiteNo']").attr("name");
       if(i1)
       {
         i=parseInt(i1.substring(14))+1;
       }
       num=num+i-1;
       var strHTML="";
  
       for(i;i<=num;i++)
       { 
           strHTML=strHTML.concat("<tr bgcolor=\"#ffffff\"><td align=\"middle\" bgcolor=\"#EDF6FF\"><span  size=\"3\">"+i+"</span></td><td align=\"middle\" bgcolor=\"#EDF6FF\"><input size=\"8\" value=\"游客\" name=\"CustomerName"+i+"\" /></td><td align=\"middle\" bgcolor=\"#EDF6FF\" ><select id=\"CertificateName\" name=\"CertificateName"+i+"\"><option value=\"0\" selected=\"selected\">请选择证件</option><option value=\"1\">身份证</option><option value=\"2\">户口本</option><option value=\"3\">军官证</option><option value=\"4\">护照</option><option value=\"5\">边境通行证</option><option value=\"6\">其他</option></select></td><td align=\"middle\" bgcolor=\"#EDF6FF\"><input name=\"CertificateNo"+i+"\" id=\"CertificateNo5\"  size=\"20\" /></td><td align=\"middle\" bgcolor=\"#EDF6FF\"><div class='sexclass'><select id=\"CustomerSex\"  name=\"CustomerSex"+i+"\"><option value=\"1\" selected=\"selected\">男</option><option value=\"0\">女</option></select></div></td><td align=\"middle\" bgcolor=\"#EDF6FF\">"+(type=="0"?"<label>儿童</label><input type='hidden' name='CustomerType"+i+"' value='0'/>":"<label>成人</label><input type='hidden' name='CustomerType"+i+"' value='1'/>")+"</td><td align=\"middle\" bgcolor=\"#EDF6FF\"><input name=\"CustomerTelphone"+i+"\" id=\"CustomerTelphone5\" size=\"15\" /></td><td align=\"middle\" bgcolor=\"#EDF6FF\"><input   name=\"CustomerSiteNo"+i+"\" id=\"CustomerSiteNo\" size=\"5\" /></td><td align=\"middle\" bgcolor=\"#EDF6FF\"><input id=\"CustomerRemark"+i+"\" name=\"CustomerRemark"+i+"\" /></td></tr>");
       }
       
       plast.append(strHTML);
    }
  }
}

</script>
</body>
</html>
