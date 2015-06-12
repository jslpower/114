<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="Default.aspx.cs" Inherits="TourUnion.WEB.IM.Search.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc2" %>
<%@ Register src="../TourAgency/TourManger/DropSiteAndCityList.ascx" tagname="DropSiteAndCityList" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>搜索</title>
   
    <style>
<!--
BODY { 	color:#333;	font-size:12px;	font-family:"宋体",Arial, Helvetica, sans-serif;	text-align: center;	background:#fff;margin:0px;}
img {	border-style: none;
            border-color: inherit;
            border-width: thin;
            }
table{	border-collapse:collapse; margin:0px auto; padding:0px auto; }
TD {	FONT-SIZE: 12px; COLOR: #0E3F70; line-height: 20px;  FONT-FAMILY:"宋体",Arial, Helvetica, sans-serif; }
div {	margin: 0px auto;	text-align: left;	padding:0px auto;	border:0px;}
textarea {	font-size:12px;	font-family:"宋体",Arial, Helvetica, sans-serif;	COLOR: #333;}
select {	font-size:12px;	font-family:"宋体",Arial, Helvetica, sans-serif;	COLOR: #333;}
.ff0000 {color:#f00;}
a {COLOR:#0E3F70; TEXT-DECORATION: none;}
a:hover {COLOR:#f00; TEXT-DECORATION: underline;}
a:active {color:#f00; TEXT-DECORATION: none;}	

a.red { color:#cc0000;}
a.red:visited { color:#cc0000;}
a.red:hover { color:#ff0000;}

a.cs { color:#723B00;}
a.cs:visited { color:#723B00;}
a.cs:hover { color:#ff0000;}
.bar_on_comm {width:100px; height:21px; float:left; border:1px solid #94B2E7; border-bottom:0px; background:#ffffff; text-align:center;}
.bar_un_comm {width:100px; height:21px; float:left; text-align:center;}

.inputk {border:1px solid #4592BF; height:17px; background:url(/IM/images/searchfrombj.gif) top repeat-x;}
.inputtime { background:#fff url(/IM/images/dateico.gif) no-repeat 96%; height:17px; border:1px solid #4592BF; height:17px;}
-->
</style>
<script type="text/javascript">
    function getstyle(str)
    {   
        if (str==1)
        {
            line.style.display="block";
            company.style.display="none";
            titline.className="bar_on_comm"
            titcompany.className="bar_un_comm"
        }
        else
        {
            line.style.display="none";
            company.style.display="block";
            titline.className="bar_un_comm"
            titcompany.className="bar_on_comm"
        }
        
    }
    
    
        
   function Query()
    {
        var parsUserName = "<%= parsUserName %>";
    var parsMD5Password = "<%= parsMD5Password %>";
    
    var MQId = "<%= MQId %>";
    var MD5Password = "<%= MD5Password %>";
    var url="<%=lineurl %>"
    var  strCityId = document.getElementById("DropSiteAndCityList1_dropCity").value; 
    var SiteId="0";
    var CityId="0";
    if (strCityId != "0")
    {
        SiteId = strCityId.split('|')[1];
        CityId = strCityId.split('|')[0];
    }
    else
    {
        var dropProvince = document.getElementById("DropSiteAndCityList1$dropProvince").value;
        if(dropProvince == "0")
        {
            alert("请选择省份!");
            return;
        }
        else
        {
            alert("请选择出港城市!");
            return;
        }
    }
    
        var StartDate=document.getElementById("txtLeaveDate_dateTextBox").value; 
        var EndDate=document.getElementById("txtReturnDate_dateTextBox").value; 
        var RouteName=document.getElementById("<%= txtRouteName.ClientID %>").value;
        var CompanyName=document.getElementById("<%= txtCompanyName.ClientID %>").value;       
       
        window.open("/IM/LoginWeb.aspx?"+parsUserName+"="+MQId+"&"+parsMD5Password+"="+MD5Password+"&CsToBsRedirectUrl="+escape(url+"?StartDate="+StartDate+"&EndDate="+EndDate+"&RouteName="+RouteName+"&CompanyName="+CompanyName+"&SiteId="+SiteId+"&CityId="+CityId));
    }
    function Query1()
    {
        var parsUserName = "<%= parsUserName %>";
    var parsMD5Password = "<%= parsMD5Password %>";
    var MQId = "<%= MQId %>";
    var MD5Password = "<%= MD5Password %>";
    var url="<%=lineurl %>"
    var  strCityId = document.getElementById("DropSiteAndCityList1_dropCity").value; 
    var SiteId="0";
    var CityId="0";
    if (strCityId != "0")
    {
        SiteId = strCityId.split('|')[1];
        CityId = strCityId.split('|')[0];
    }
    else
    {
        var dropProvince = document.getElementById("DropSiteAndCityList1$dropProvince").value;
        if(dropProvince == "0")
        {
            alert("请选择省份!");
            return;
        }
        else
        {
            alert("请选择出港城市!");
            return;
        }
    }
    
        var CompanyName=document.getElementById("<%= txtRouteProvider.ClientID %>").value;
        var BandName=document.getElementById("<%= txtBandName.ClientID %>").value;
        window.open("/IM/LoginWeb.aspx?"+parsUserName+"="+MQId+"&"+parsMD5Password+"="+MD5Password+"&CsToBsRedirectUrl="+escape("/TourAgency/RouteProvider/RouteProvider.aspx?CompanyName="+CompanyName+"&BandName="+BandName+"&SiteId="+SiteId+"&CityId="+CityId));
    }
     function Check()
     {
//        var Province=document.getElementById("DropSiteAndCityList1$dropProvince");
//        var City=document.getElementById("DropSiteAndCityList1$dropCity");
//        var errorArr = new Array();
//        if(Province.value == "0")
//        {
//             errorArr.push("请选择省份!\n");

//         }
//        if(City.value== "0")
//        {
//             errorArr.push("请选择城市!\n");

//         }
//        if(errorArr.length>0)
//        {
//            alert(errorArr.join(""));
//            event.returnValue=false;
//        }
     }
</script>
</head>
<body oncontextmenu="return false;">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0"> 
  <tr>
    <td height="57" align="center" valign="bottom" background="/IM/images/ztopbj.gif" bgcolor="#E0E9FC"><table width="210" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top:6px;">
        <tr>
          <td align="center"><div class="bar_on_comm" id="titline"><a href="javascript:void(0)" onclick="getstyle(1)" onfocus="blur()">找线路</a></div>
            <div  id="titcompany"  class="bar_un_comm"><a href="javascript:void(0)" onfocus="blur()" onclick="getstyle(0)">找公司</a> </div></td>
        </tr>
        </table></td>
  </tr>
  <tr>
    <td><font color="red">*</font>出港城市: <uc1:DropSiteAndCityList ID="DropSiteAndCityList1" runat="server" />
        </td>
  </tr>
  <tr>
  
    <td align="center">
    
    <div id="line" style="display:block">
        <table width="210" border="0" cellspacing="0" cellpadding="2">
      <tr>
        <td width="70" align="right">线路名称:</td>
        <td width="130" align="left">
            <asp:TextBox ID="txtRouteName" runat="server" CssClass="inputk" Width="115px"></asp:TextBox>
                            </td>
      </tr>
      <tr>
        <td align="right">专线供应商:</td>
        <td align="left">
            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="inputk" Width="115px"></asp:TextBox>
                            </td>
      </tr>
      <tr>
        <td align="right">发班时间:</td>
        <td align="left"><cc2:DatePicker ID="txtLeaveDate" runat="server" Width="80px"  CssClass="inputtime"></cc2:DatePicker>&nbsp;至<cc2:DatePicker ID="txtReturnDate" runat="server" Width="80px" CssClass="inputtime"></cc2:DatePicker></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td align="left"><img src="/IM/images/btn1.gif" id="imgQuery" style="cursor:pointer" onclick="Query()" /><%--<asp:ImageButton ID="BtnTourQuery" OnClientClick="Check()" CssClass="inputnoline" runat="server" ImageUrl="/IM/images/btn1.gif"
                                OnClick="BtnTourQuery_Click"></asp:ImageButton>--%></td>
      </tr>
    </table> 
    </div>
    <div id="company" style="display:none">   
        <table width="210" border="0" cellspacing="0" cellpadding="3">
        <tr>
          <td width="70" align="right">公司名称:</td>
          <td width="130" align="left">
              <asp:TextBox ID="txtRouteProvider" runat="server" CssClass="inputk" 
                  Width="115px"></asp:TextBox>
                            </td>
        </tr>
        <tr>
          <td align="right">品牌名称:</td>
          <td align="left">
              <asp:TextBox ID="txtBandName" runat="server" CssClass="inputk" Width="115px"></asp:TextBox>
                            </td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="left"><img src="/IM/images/btn1.gif" id="imgQuery1" style="cursor:pointer" onclick="Query1()" /><%--<asp:ImageButton ID="BtnCompanyQuery" CssClass="inputnoline" 
                  runat="server" ImageUrl="/IM/images/btn1.gif" onclick="BtnCompanyQuery_Click"></asp:ImageButton>--%></td>
        </tr>
      </table>
     </div> 
      </td>
  </tr>
</table>

    </form>
    <script >
        getstyle(1);
    </SCRIPT>
</body>
</html>
