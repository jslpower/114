<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="UserPublicCenter.AirTickets.OrderManage.OrderDetails" MasterPageFile="~/MasterPage/AirTicket.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage/AirTicket.Master" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ContentPlaceHolderID="c1" ID="cntOrderDetail" runat="server">
<!-- sidebar02_con start-->
<style type="text/css">
    .errmsg{ color:Red}
</style>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>
 <div class="sidebar02_con"> 
  <div class="sidebar02_con_table02">
  	<table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tbody><tr align="left" bgcolor="#e0f4fd">
        <td bgcolor="#e0f4fd" height="30"><span class="title">航班信息</span></td>
      </tr>
      <tr>
        <td height="35"><table width="100%" align="center" border="1" bordercolor="#dcd8d8" cellpadding="0" cellspacing="0">

          <tbody><tr>
            <td><table class="search_results" width="100%" border="0" cellpadding="0" cellspacing="0">
                <tbody>
                <tr>
                  <th width="19%" align="center"><span style="font-size: 14px;"><asp:Literal ID="ltr_gysCompanyName" runat="server"></asp:Literal></span></th>
                  <td width="8%" align="center" height="25">去程：</td>
                  <td width="20%" align="left"><asp:Literal ID="ltr_gysGoRun" runat="server"></asp:Literal></td>
                  <td width="16%" align="left">出发时间：<asp:Literal ID="ltr_gysGoDate" runat="server"></asp:Literal></td>
                
                <td width="15%" align="center">航班号：<asp:Literal ID="ltr_gysPlaneNum" runat="server"></asp:Literal></td>
                  <td width="30%" align="left">旅客类型：<asp:Literal ID="ltr_gyslkType" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                  <td width="19%" align="center">航班类型：<asp:Literal ID="ltr_gyshbType" runat="server"></asp:Literal></td>
                
                  <%=BackRunInfo %>
                </tr>
            </tbody></table></td>
          </tr>
          <tr>
            <td><table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                <tbody><tr>

                  <td align="center" height="30"><table width="100%" align="center" border="1" bordercolor="#dcd8d8" cellpadding="0" cellspacing="0">
                    <tbody><tr bgcolor="#edf8fc">
                      <th width="12%" align="center" height="30">订单号</th>
                      <th width="12%" align="center">PNR</th>
                      <th width="12%" align="center">更换PNR</th>
                      <th width="12%" align="center">代理商名称</th>

                      <th width="12%" align="center">类型</th>
                    </tr>
                    <tr>
                      <td align="center" height="25">
                          <asp:Label ID="ltr_OrderNo" runat="server" Text="Label"></asp:Label>
                      </td>
                      <td align="center"><asp:Literal ID="ltr_OrderPnr" runat="server"></asp:Literal></td>
                      <td align="center"><asp:Literal ID="ltr_IsChangePnr" runat="server"></asp:Literal></td>
                      <td align="center"><asp:Literal runat="server" ID="ltr_gysCompanyName1"></asp:Literal></td>
                      <td align="center"><asp:Literal runat="server" ID="ltr_TeamType" ></asp:Literal></td>
                    </tr>
                  </tbody></table></td>
                  </tr>
                <tr>
                  <td align="center" height="30"><table width="100%" border="1" bordercolor="#cccccc" cellpadding="0" cellspacing="0">
                    <tbody><tr bgcolor="#edf8fc">

                      <th width="5%" align="center" height="30">&nbsp;</th>
                      <th width="10%" align="center">面价</th>
                      <th width="10%" align="center">参考扣率</th>
                      <th width="14%" align="center">运价有效期</th>
                      <th width="18%" align="center">结算价（不含税）</th>
                      <th width="10%" align="center">人数上限</th>

                      <th width="14%" align="center">燃油/机建</th>
                    </tr>
                    <%=OrderRateInfo%>
                  </tbody></table></td>
                </tr>
            </tbody></table></td>
          </tr>
          <tr>
            <td><table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tbody><tr>
                  <td rowspan="2" width="16%" align="center"><%=Utils.GetMQ(GysContactMQ)%></td>

                  <td width="19%" align="left" height="25">联系人：<asp:Literal ID="ltr_gysLinkPerson" runat="server"></asp:Literal></td>
                  <td width="19%" align="left">上下班时间：<asp:Literal ID="ltr_gysWorkDate" runat="server"></asp:Literal></td>
                  <td width="13%" align="left" colspan="2" style="width: 46%">供应商主页：<asp:Literal ID="ltr_gysMainPage" runat="server"></asp:Literal></td>
                </tr>
                <tr>

                  <td align="left" height="25">联系电话：<asp:Literal ID="ltr_gysLinkPhone" runat="server"></asp:Literal></td>
                  <td align="left">出票成功率：<asp:Literal ID="ltr_gysOutTickets" runat="server"></asp:Literal></td>
                  <td align="left">代理级别：<asp:Literal ID="ltr_gysdllevel" runat="server"></asp:Literal></td>
                  <td align="left">退票平均时间：<asp:Literal ID="ltr_gysBackTicAgvDate" runat="server"></asp:Literal>（小时）</td>
                </tr>
            </tbody>
                </table>
              </td>
          </tr>

          <tr>
            <td align="left"><span class="zhechebz">！供应商备注：<asp:Literal ID="ltr_gysRemark" runat="server"></asp:Literal>
              </td>
          </tr>
        </tbody></table></td>
      </tr>
    </tbody></table>
  </div>
  <div class="sidebar02_con_table03">

    <table width="100%" bgcolor="#fafafa" border="0" cellpadding="0" cellspacing="0">
      <tbody><tr align="left">
        <td bgcolor="#e0f4fd" height="30"><span class="title">旅客信息</span></td>
      </tr>
      <tr>
        <td align="left"><table width="100%" border="1" bordercolor="#dcd8d8" cellpadding="0" cellspacing="0" id="tbPassengersInfo">
            <tbody><tr>
              <td colspan="9" align="left" height="25"><table width="45%" border="0" cellpadding="0" cellspacing="0">

                  <tbody><tr>
                    <th width="15%" align="center"><font color="#ff6600">旅客人数：<asp:Literal runat="server" ID="ltr_perCount"></asp:Literal>位</font></th>
                    <th width="15%" align="center"><font color="#ff6600">购买保险：[<%=BuySafetyCount.ToString()%>]份</font></th>
                    <th width="15%" align="center"><font color="#ff6600">购买行程单：[<%=BuySafetyCount.ToString() %>]份</font></th>
                  </tr>
              </tbody></table></td>
            </tr>
                
            <tr id="trTrCell">
                <th align="center" width="6%"><input type="checkbox" id="cbxAllPass" onclick="OrderDetails.AllCheckControl(this)" name="cbxAllPass" />全选</th>
              <th align="center" height="30" width="13%">
                 乘客姓名</th>
              <th align="center" width="60px;">乘客类型</th>
              <th width="8%" align="center">证件类型</th>
              <th align="center">证件号码</th>
              <th align="center" style="width: 12%">票号</th>
                <th align="center" style="width: 10%">旅客状态</th>
              <th align="center" width="9%">购买行程单</th>
              <th width="8%" align="center">购买保险</th>
            </tr>
            <cc1:CustomRepeater ID="crptPassengersList" runat="server">
                <ItemTemplate>
                     <tr>
                          <td width="3%" align="center"><input type="checkbox" name="cbxPassId" value="<%#Eval("TravellerId")%>" /></td>
                          <td align="center" height="8"><%#Eval("TravellerName")%></td>
                          <td width="8%" align="center"><%#Eval("TravellerType")%></td>
                          <td align="center" width="11%"><%#Eval("CertType") %></td>
                          <td align="center"  width="23%"><%#Eval("CertNo")%></td>
                          <td width="11%" align="center"><%#Eval("TicketNumber")%></td>
                          <td width="8%" align="center"><%# GetTravellerState(Eval("TravellerState").ToString())%>                          
                          </td>
                          <td width="8%" align="center"><%#Convert.ToBoolean(Eval("IsBuyItinerary"))==true?"是":"否"%></td>
                          <td width="10%" align="center"><%#Convert.ToBoolean(Eval("IsBuyIns")) == true ? "是" : "否"%></td>
                    </tr>
                </ItemTemplate>                
            </cc1:CustomRepeater>        
        </tbody></table></td>
      </tr>
    </tbody></table>

  </div>
  <div class="sidebar02_con_table03">
    <table width="100%" bgcolor="#fafafa" border="0" bordercolor="#dcd8d8" cellpadding="0" cellspacing="0">
      <tbody><tr align="left">
        <td bgcolor="#e0f4fd" height="30"><span class="title">支付方式和支付金额</span></td>
      </tr>
      <tr>
        <td align="left"><table width="100%" border="1" bordercolor="#dcd8d8" cellpadding="0" cellspacing="0">

            <tbody><tr>
              <td colspan="2" align="left" height="25"><span style="padding-left: 40px; color: rgb(255, 102, 0); font-weight: bold;">乘客人数:
                <asp:Literal ID="ltr_PassCount" runat="server"></asp:Literal>位</span></td>
            </tr>
            <tr>
              <td width="18%" align="center" height="30">订单总金额：</td>
              <td width="80%" align="left">&nbsp;共计：<font color="#ff0000" size="+2"><asp:Literal
                  ID="ltr_TotalPrice" runat="server"></asp:Literal></font>元</td>

            </tr>
            <tr>
              <td colspan="2" align="left" height="30">&nbsp;&nbsp;<font color="#ff0000">注：（结算价格+燃油/机建）*人数+保险*人数+行程单*人数+快递费 &nbsp;&nbsp;</font> </td>
            </tr>
        </tbody></table></td>
      </tr>

    </tbody></table>
  </div>
  <div class="sidebar02_con_table03">
  	<table width="100%" bgcolor="#fafafa" border="0" cellpadding="0" cellspacing="0">
      <tbody><tr align="left">
        <td bgcolor="#e0f4fd" height="30"><span class="title">订单处理状态</span></td>
      </tr>
      <tr>

        <td align="left"><table  width="100%" border="1" bordercolor="#dcd8d8" cellpadding="0" cellspacing="0">
            <tbody>
             <tr>
                  <td width="20%" align="center" height="25">订单状态</td>
                  <td width="15%" align="center">操作时间</td>
                  <td width="15%" align="center">操作人</td>
                  <td width="60%" align="center">备注</td>
            </tr>
            <cc1:CustomRepeater ID="crptOrdState" runat="server">
                <ItemTemplate>
                    <tr>
                      <td width="20%" align="center" height="25"><%#Eval("State")%></td>
                      <td width="15%" align="center"><%#Eval("Time","{0:yyyy-MM-dd HH:mm}")%></td>
                      <td width="15%" align="center"><%#Eval("UserName")%></td>
                      <td width="60%" ><%#Eval("Remark")%></td>
                    </tr>
                </ItemTemplate>
            </cc1:CustomRepeater>
          </tbody></table>
        </td>

      </tr>
    </tbody></table>
  </div>
  <div class="sidebar02_con_table03" id="divcgsInfo" runat="server">
    <table width="100%" bgcolor="#fafafa" border="0" cellpadding="0" cellspacing="0">
      <tbody><tr align="left">
        <td bgcolor="#e0f4fd" height="30"><span class="title">联系方式<%#Eval("TravellerName")%></span></td>
      </tr>

      <tr>
        <td align="left"><table class="sidebar02_con_lianxi" width="100%" border="1" bordercolor="#dcd8d8" cellpadding="0" cellspacing="0">
            <tbody><tr>
              <td width="25%" align="left" height="25">联系公司：</td>
              <td width="74%" align="left"><input name="txtcgsComapanyName" id="txtcgsComapanyName" type="text" runat="server" valid="required" errmsg="联系公司不能为空"/>
              <span id="errMsg_ctl00_c1_txtcgsComapanyName" class="errmsg" ></span></td>
            </tr>
            <tr>
              <td align="left" height="25">联系人：</td>

              <td align="left"><input name="txtcgsLinkName" runat="server" id="txtcgsLinkName" type="text" valid="required" errmsg="联系人不能为空"/>
              <span class="errmsg" id="errMsg_ctl00_c1_txtcgsLinkName"></span></td>
            </tr>
            <tr>
              <td align="left" height="25">手机：</td>
              <td align="left"><input name="txtcgsTel" id="txtcgsTel"  runat="server" type="text" valid="required|isMobile" errmsg="手机号码不能为空|手机号码格式错误"/>
                <span id="errMsg_ctl00_c1_txtcgsTel" class="errmsg"></span>
              </td>
            </tr>
            <tr>
              <td align="left" height="25">地址：</td>

              <td align="left"><input name="txtcgsAddress" id="txtcgsAddress" type="text" runat="server" valid="required" errmsg="地址不能为空" />
                <span class="errmsg" id="errMsg_ctl00_c1_txtcgsAddress"></span>
              </td>
            </tr>
            
            <tr>
              <td colspan="2" align="center"><img alt="修改" src="<%=ImageServerPath %>/images/jipiao/update_btn.jpg" id="imgModifOrder" style=" cursor:pointer" onclick="OrderDetails.ModifCgsInfo()" /></td>
            </tr>
            
        </tbody></table></td>
      </tr>
    </tbody></table>
</div>
  </div>
  <div class="sidebar02_con_table03" id="divOperateOrder"  runat="server" >
  	<table width="100%" bgcolor="#fafafa" border="1" bordercolor="#cccccc" cellpadding="0" cellspacing="0">
      <tbody><tr align="left" bgcolor="#e0f4fd">
        <td height="30"><span class="title">订单处理</span></td>
      </tr>
      <tr>
        <td align="left">

        	<div class="add_travellerInfo" id="divOperate">
                 <ul>
                    <li><a class="book_default" href="javascript:void(0)" id="five1" onmousemove="OrderDetails.setTab('five',1,5)">申请退票</a></li>
                    <li><a class="" href="javascript:void(0)" id="five2" onmousemove="OrderDetails.setTab('five',2,5)">申请作废</a></li>
                    <li><a class="" href="javascript:void(0)" id="five3" onmousemove="OrderDetails.setTab('five',3,5)">申请改期</a></li>
                    <li><a class="" href="javascript:void(0)" id="five4" onmousemove="OrderDetails.setTab('five',4,5)">申请改签</a></li>
                    <li><a class="" href="javascript:void(0)" id="five5" onmousemove="OrderDetails.setTab('five',5,5)">服务备注</a></li>

                 </ul>
              <div class="clearboth"></div>
              <div style="display: block;" class="add_travellerInfo_cont" id="con_five_1">
                  <table width="98%" align="center" bgcolor="#fafafa" border="1" bordercolor="#cccccc" cellpadding="0" cellspacing="0">
                    <tbody><tr>
                      <th width="20%" align="center" bgcolor="#e0f4fd" height="30">申请退票：</th>
                      <td width="30%" align="center"><select name="sltBlack" id="sltBlack" runat="server">  </select>
                      </td>
                      <td width="50%" align="left">&nbsp;&nbsp;
                          <input name="btnBackTicekts" id="btnBackTicekts" onclick="OrderDetails.OrderOperateType(1,this)" value="退票" type="button"/>
                         <span id="errMsg_btnBackTicekts" class="errmsg"></span></td>
                    </tr>
                </tbody></table>
                </div>
                <div class="add_travellerInfo_cont" id="con_five_2" style="display: none;">
                  <table width="98%" align="center" bgcolor="#fafafa" border="1" bordercolor="#cccccc" cellpadding="0" cellspacing="0">
                    <tbody><tr>
                      <th width="20%" align="center" bgcolor="#e0f4fd" height="30">申请作废：</th>
                      <td width="30%" align="center"><select name="sltBlankOut" id="sltBlankOut" runat="server">
                        </select>
                      </td>
                      <td width="50%" align="left">&nbsp;&nbsp;
                          <input name="btnBlankOut" id="btnBlankOut" value="作废" type="button" onclick="OrderDetails.OrderOperateType(2,this)"/>
                         <span id="errMsg_btnBlankOut" class="errmsg"></span></td>
                    </tr>
                  </tbody></table>
                </div>
                <div class="add_travellerInfo_cont" id="con_five_3" style="display: none;">
                  <table width="98%" align="center" bgcolor="#fafafa" border="1" bordercolor="#cccccc" cellpadding="0" cellspacing="0">
                    <tbody><tr>
                      <th width="18%" align="center" bgcolor="#e0f4fd" height="80">申请改期：</th>
                      <td width="44%" align="center"><textarea name="txaChangeDate" id="txaChangeDate" cols="50" rows="5" runat="server"></textarea></td>

                      <td width="38%" align="left">&nbsp;&nbsp;
                          <input name="btnChangeDate" id="btnChangeDate" value="改期" type="button" onclick="OrderDetails.OrderOperateType(3,this)" runat="server"/>
                         <span id="errMsg_btnChangeDate" class="errmsg"></span></td>
                    </tr>
                  </tbody></table>
                </div>

                <div class="add_travellerInfo_cont" id="con_five_4" style="display: none;">
                  <table width="98%" align="center" bgcolor="#fafafa" border="1" bordercolor="#cccccc" cellpadding="0" cellspacing="0">
                    <tbody><tr>
                      <th width="18%" align="center" bgcolor="#e0f4fd" height="80">申请改签：</th>
                      <td width="44%" align="center"><textarea name="txaChangeTack" id="txaChangeTack" cols="50" rows="5" runat="server"></textarea></td>
                      <td width="38%" align="left">&nbsp;&nbsp;
                          <input name="button" id="btnChangeTack" value="改签" type="button" onclick="OrderDetails.OrderOperateType(4,this)"/>
                         <span id="errMsg_btnChangeTack" class="errmsg"></span></td>
                    </tr>
                  </tbody></table>
                </div>
                <div class="add_travellerInfo_cont" id="con_five_5" style="display: none;">
                  <table width="98%" bgcolor="#fafafa" border="1" bordercolor="#cccccc" cellpadding="0" cellspacing="0">
                    <tbody><tr>

                      <th width="18%" align="center" bgcolor="#e0f4fd" height="80">服务备注：</th>
                      <td width="44%" align="center"><textarea name="txaServiceRemark" id="txaServiceRemark" cols="50" rows="5" runat="server"></textarea></td>
                      <td width="38%" align="left">&nbsp;&nbsp;
                          <input name="btnServiceRemark" id="btnServiceRemark" value="修改" type="button" onclick="OrderDetails.OrderOperateType(5,this)"/>
                         <span id="errMsg_btnServiceRemark" class="errmsg"></span></td>
                    </tr>
                  </tbody></table>
                </div>
          </div>
          </td>
      </tr>
    </tbody></table>
  </div>
  <div id="divSpecRemark" runat="server">
    <table style="margin-top: 10px;" width="100%" border="1" bordercolor="#cccccc" cellpadding="0" cellspacing="0">
      <tbody><tr>
        <th width="20%" align="center" height="100">特殊要求备注</th>
        <td align="center">
                <textarea name="txaExpressRemark" id="txaExpressRemark" cols="60" rows="5" runat="server"></textarea>&nbsp;&nbsp;&nbsp;&nbsp;
                <input name="btnSpecRemark" id="btnSpecRemark" onclick="OrderDetails.ModifSpecRemark(6)" value="修改" type="button"/>
                <span id="spn_txaExpressRemark" class="errmsg"></span>    
        </td>
      </tr>
    </tbody></table>
  </div><br />
  <div runat="server" id="divPayType" visible="false">
       <table width="90%" align="center" border="0" cellpadding="0" cellspacing="0">
      <tbody><tr>
        <td  align="center" height="10"><%=PayHtmlText %></td>
      </tr>
    </tbody ></table>

  </div>
 
    <input type="hidden" id="hidOrderId" runat="server" /> 
<!-- sidebar02_con end-->
<script type="text/javascript">
 var form1 ;   //表单
 var ParmsOrd={OrderId:"", OpearType:"",Content:"",CheckperIds:"",OrderNo:""};  //修改备注和订单操作参数
 var ParmCompany={OrderId:"",OpearType:"",CompanyName:"",LinkName:"",Mobile:"",Address:""};    //公司信息参数
 
var OrderDetails={
    setTab:function(name,cursel,n){
       for(i=1;i<=n;i++){
          var menu=document.getElementById(name+i);
          var con=document.getElementById("con_"+name+"_"+i);
          menu.className=i==cursel?"book_default":"";
          con.style.display=i==cursel?"block":"none";
//          this.ClearOperateErrMsg(cursel);
       }
    },
    AllCheckControl:function(obj){   //全选和反选乘客
        $("#tbPassengersInfo").find("input[type='checkbox'][name='cbxPassId']").each(function(){
            $(this).attr("checked",$(obj).attr("checked"));
        });
    },
   OrderOperateType:function(type,obj){  
        var ckids = new Array();
        if(type!=5)
        {
        
            var OrderStateLog="<%= OrderStateLog%>";
            if(OrderStateLog.length>1)
            {
                alert("订单当前状态为"+OrderStateLog+",不能再次申请退票、作废、改期和改签操作！");
                return;
            }
            $("#tbPassengersInfo").find("input[type='checkbox'][name!='cbxAllPass']:checked").each(function() {
                ckids.push($(this).val());//获取所选中的乘客id
            });        
            if (ckids.length == 0) {
                alert("请选择乘客!");
                return false;
            }
            //判断某旅客状态是否已经退票成功或者作废成功，如果两种状态有一成功，改旅客就不能在申请两种状态。
//            if(type==1 || type==2)
//            {
//                for(var i=0;i<ckids.length;i++)
//                {
//                    var TravelState=$("#hidTState"+ckids[i]).val();         //获取该旅客状态
//                        alert(TravelState);
//                    if(TravelState=="退票成功" || TravelState=="作废成功")
//                    {
//                        alert("选择的旅客中存在旅客状态为退票接受或者作废接受的旅客，这类旅客不能再次申请退票和申请作废！");
//                        return false;
//                    }
//                }
//            }
            ParmsOrd.CheckperIds=ckids;  
        }
        var cause="";
        var errinfo;
        switch(type){          //申请操作原因验证
            case 1:         //申请退票              
                errinfo="请选择退票原因";
                cause=$("#<%=sltBlack.ClientID %>").val();
                if(cause=="0"){
                    cause="";    
                }
                break;
            case 2:          //申请作废   
                errinfo="请选择作废原因"
                 cause=$("#<%=sltBlankOut.ClientID %>").val();
                if(cause=="0"){
                    cause="";    
                }
                break; 
            case 3:         //申请改期
                errinfo="改期原因不能为空";
                cause=$("#<%=txaChangeDate.ClientID%>").val();
                break;
            case 4:         //申请改期
                errinfo="改签原因不能为空"
                cause=$("#<%=txaChangeTack.ClientID%>").val();
                break;
            case 5:         //服务备注
                errinfo="服务备注不能为空";
                cause=$("#<%=txaServiceRemark.ClientID%>").val();
                break;
        }
        if(cause==""){
             $("#errMsg_"+$(obj).attr("id")).html(errinfo);
             return false;
        }else{
            $("#errMsg_"+$(obj).attr("id")).html("");
        }
        ParmsOrd.OpearType=type;
        ParmsOrd.Content=cause;  
        ParmsOrd.OrderNo=$.trim($("#<%=ltr_OrderNo.ClientID %>").html());    //获取订单编号
        this.SubmitFrom(ParmsOrd);        //ajax执行操作
        this.ClearOperateErrMsg(type);    
   },
   ClearOperateErrMsg:function(num){     //清楚文本框值
     switch(num){
            case 1:         //申请退票
                   $("#<%=sltBlack.ClientID%>").val("0");
                   $("#errMsg_btnBackTicekts").html("");
                break;
            case 2:          //申请作废   
                  $("#<%=sltBlankOut.ClientID%>").val("0");
                  $("#errMsg_btnBlankOut").html("");
                break; 
            case 3:         //申请改期
                $("#<%=txaChangeDate.ClientID %>").val("");
                  $("#errMsg_btnChangeDate").html("");
                break;
            case 4:         //申请改签
                $("#<%= txaChangeTack.ClientID%>").val("");
                $("#errMsg_btnChangeTack").html("");
                break;
            case 5:         //服务备注
                $("#<%=txaServiceRemark.ClientID%>").val(""); 
                $("#errMsg_btnBackTicekts").html("");
                break;
        }
   },
   ModifCgsInfo:function(){    //订单修改
     if(ValiDatorForm.validator(form1,"span"))
	 {
        var companyName= $("#<%=txtcgsComapanyName.ClientID %>").val();
        var linkName=$("#<%= txtcgsLinkName.ClientID%>").val();
        var tel=$("#<%=txtcgsTel.ClientID %>").val();        
        var address=$("#<%= txtcgsAddress.ClientID %>").val();
        ParmCompany.Address=address;
        ParmCompany.CompanyName=companyName;
        ParmCompany.LinkName=linkName;
        ParmCompany.Mobile=tel;
        ParmCompany.OpearType=0;
        this.SubmitFrom(ParmCompany);        //修改操作
     }else{
        return false;
     }
   },
   LoadControl:function(){   //不能支付时隐藏全选列  
    if($("#<%=divcgsInfo.ClientID %>").length>0)     //获取表单对象
    {
       form1=$("#<%=divcgsInfo.ClientID %>").closest("form").get(0)   
    FV_onBlur.initValid(form1);
    }
        var payvisable=$("#<%=divPayType.ClientID %>").length;
        var opearOdrvisable=$("#<%=divOperateOrder.ClientID %>").length;
        if(opearOdrvisable<1){  
            $("#tbPassengersInfo tr:gt(1)").each(function () {           //【隐藏旅客信息列表第2行以后的第一个单元格 】
                $(this).children().eq(0).hide();
            });
        }else{
             $("#tbPassengersInfo tr:gt(1)").each(function () {           //【隐藏旅客信息列表第2行以后的第一个单元格 】
                var TravelState=$.trim($(this).children().eq(6).html());      //获取该旅客状态
                if(TravelState=="退票成功" || TravelState=="作废成功")
                {
                    $(this).children().eq(0).html("");
                }
            });
        }
    },
    ModifSpecRemark:function(typnum){  //修改特殊备注
        var remark=$.trim($("#<%=txaExpressRemark.ClientID %>").val())   ;
        if(typnum==6 && remark!="")
        {
            $("#spn_txaExpressRemark").html("");
            ParmsOrd.OpearType=typnum;
            ParmsOrd.Content=remark;
            this.SubmitFrom(ParmsOrd);  
        }else{
          $("#spn_txaExpressRemark").html("特殊备注要求不能为空");
        }
    },
    InsertAccount:function(paytype) {  
        var OrderId=$("#<%=hidOrderId.ClientID %>").val()
        var result1=false;
          $.ajax(
             {
                 url: "OrderDetails.aspx",
                 data: {orderId:OrderId,orderNo:"<%=OrderNo %>",sellCId:"<%=SupplierCId %>",total:"<%=TotalAmount %>",OpearType:"InsertAccount",PayType:paytype},
                 dataType: "json",
                 cache: false,
                 type: "post",
                 async:false,
                 success: function(result) {
                     if (result.success == "1") { 
                      result1=true;
                     }
                     else {
                        alert( result.message);
                     }
                 },
                 error: function() {
                     alert("操作失败");
                 }
             });
             return result1;
    },
   SubmitFrom:function(urlData){ 
      var orderId=$("#<%=hidOrderId.ClientID %>").val()
      ParmCompany.OrderId=orderId;
      ParmsOrd.OrderId=orderId;
        $.ajax({
             url:"/AirTickets/OrderManage/OrderDetails.aspx",
             data:urlData,
             dataType:"json",
             cache:false,
             type:"post",
             success:function(msg){     
	            alert(msg.message);       
                if(msg.success=="1")
                {
                    if(urlData==ParmsOrd){	
                       if( ParmsOrd.OpearType!=6 && ParmsOrd.OpearType!=5){   
                            window.location.href="OrderDetails.aspx?OrderId="+orderId; 
                         }
                    }
                 }
	            ParmCompany.CompanyName="";
	            ParmCompany.Address="";
	            ParmCompany.OpearType="";
	            ParmCompany.LinkName="";
	            ParmCompany.Mobile="";
	            ParmCompany.OrderId="";
	            ParmsOrd.OpearType="";
	            ParmsOrd.Content="";
	            ParmsOrd.CheckperIds="";
	            ParmsOrd.OrderId="";	          
             },
             error:function(){
               alert("操作失败，请稍候！");
             }
        });
   }
}
$(document).ready(function(){  
    OrderDetails.LoadControl();
});
</script>
</asp:Content>