<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteSelect.ascx.cs"
    Inherits="IMFrame.WebControls.SiteSelect" %>

<%@ Import Namespace="EyouSoft.Common" %>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
<script language="javascript" type="text/javascript">
var iFloatProvince=null;
var iFloatSite=null;

function showFloatCategorySite(str)
{
    var oC = document.getElementById(str);
    oC.style.display="block";
    var joc = $(oC);
    var offset = $("#linkCity").offset();
    joc.css("top",offset.top+18).css("left",$("#<%=table_SiteInfo.ClientID %>").offset().left);
    createFrame(oC);
    if(iFloatSite!=null)
        clearInterval(iFloatSite);
}
function hideFloatCategorySite(str)
{
    closeFrame();
    var obj = document.getElementById(str);
    if(obj!=null)
        iFloatSite = setInterval('document.getElementById("'+str+'").style.display="none"',100);
}

function showFloatCategoryProvince(str)
{
    var oP=document.getElementById(str);
    var jop = $(oP);
    oP.style.display="block";
    var offset = $("#linkProvince").offset();
    jop.css("top",offset.top+18).css("left",$("#<%=table_SiteInfo.ClientID %>").offset().left);
    createFrame(oP);
    if(iFloatProvince!=null)
        clearInterval(iFloatProvince);
}
function hideFloatCategoryProvince(str)
{
   closeFrame();
    var obj = document.getElementById(str);
    if(obj!=null){
            iFloatProvince = setInterval('document.getElementById("'+str+'").style.display="none"',100);
        }
}


function ChangeProvinceName(obj,CityId)
{
    $("#SpanProvinceName").html($(obj).text());
    $("#ProvinceDiv").find("a").each(function(){
        if(this == obj)
            this.style.color = 'red';
        else
            this.style.color = '#0E3F70';
    });
    $("#SpanSiteName").html($("#c_"+CityId).text());
    $("#strongProvinceName").html($(obj).text());
    $("#ProvinceDiv").attr("style","display:none;");
    
    $("#td_SiteInfo").children().each(function(){
        if($(this).attr("pid")!=$(obj).attr("id").split('_')[1])
        {
            $(this).hide();
        }
        else
        {
            $(this).show();
        }
    });
    InitInfo(CityId);    
}
$(function(){
    $("#td_SiteInfo").children().each(function(){
        if($(this).attr("pid")!="<%= ProvinceId %>")
        {
            $(this).hide();
        }
    });
    InitInfo("<%= CityId %>");
});
function ChangeSiteName(obj,CityId)
{
    document.getElementById("SiteIdAndCityId").value = CityId;
    $("#SpanSiteName").html($(obj).text());
    $("#SiteDiv").find("a").each(function(){
        if(this == obj)
            this.style.color = 'red';
        else
            this.style.color = '#0E3F70';
    });
    document.getElementById("SiteDiv").style.display="none";
    InitInfo(CityId);
}

function InitInfo(CityId)
{
    ChangeTourArealist(0);
    GetAreaList(CityId);
}
function closeFrame(oDiv){
    var oFrame = document.getElementById("pframe");
    if(oFrame){
        oFrame.style.display="none";
    }
}
function createFrame(oDiv){
    var isIe6=false;
    if ( $.browser.msie && $.browser.version=="6.0" ){
        isIe6=true;
    }
    if(isIe6==false){return;}
    var oFrame = document.getElementById("pframe");
    if(oFrame==null){
        oFrame = document.createElement("iframe");
        oFrame.id="pframe"
        oFrame.style.cssText="display:none;position:absolute;";
        document.body.appendChild(oFrame);
    }
    var offset = $(oDiv).offset();
    oDiv.style.zIndex = 500;
    oFrame.style.top = offset.top+2+"px";
    oFrame.style.left = offset.left+"px";
    oFrame.style.width = $(oDiv).width()+"px";
    oFrame.style.height = $(oDiv).height()+"px";
    oFrame.style.zIndex = 499;
    oFrame.style.display="block";
}
</script>

<style type="text/css">
    #ProvinceDiv
    {
        line-height: 22px;
        display: none;
        position: absolute;
        width: 210px;
        z-index: 9999;
        border: 1px #5794C3 solid;
        background: #F6FCFF;
        left: 0px;
        top: 0px;
        cursor: pointer;
    }
    #SiteDiv
    {
        line-height: 22px;
        display:none;        
        position: absolute;
        width: 210px;        
        z-index: 9999;
        border: 1px #5794C3 solid;
        background: #F6FCFF;
        left: 0px;
        top: 0px;
        cursor: pointer;
    }
</style>
<div id="ProvinceDiv" onmouseover="showFloatCategoryProvince('ProvinceDiv')" onmouseout="hideFloatCategoryProvince('ProvinceDiv')">
        <table width="100%" border="0" cellspacing="0">
            <tr>
                <td width="100%" style="padding: 5px;" align="left">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-bottom: 1px solid #f5f5f5;">
                        <tr>
                            <td width="40%" height="20" align="center" bgcolor="#D9E7F1">
                                请选择省份：
                            </td>
                            <td width="60%" height="20" align="left" bgcolor="#D9E7F1">
                                <%= ProvinceArray[0] %>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <%= ProvinceArray[1] %>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
</div>
<div id="SiteDiv" onmouseover="showFloatCategorySite('SiteDiv');" onmouseout="hideFloatCategorySite('SiteDiv')">
        <table width="100%" border="0" cellspacing="0">
            <tr>
                <td width="100%" style="padding: 5px;" align="left">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-bottom: 1px solid #f5f5f5;">
                        <tr>
                            <td height="20" align="left" bgcolor="#D9E7F1">
                                请选择 <strong id="strongProvinceName">
                                    <%= ProvinceName %></strong> 区域销售城市：
                            </td>
                        </tr>
                        <tr>
                            <td height="20" id="td_SiteInfo">
                                <%= strSiteName %>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
</div>
<input type="hidden" id="SiteIdAndCityId" value="" />
<div id="table_SiteInfo" runat="server" style="border: 1px solid #D5DEEF; padding: 3px;
    background: #F1F5FB; width: 210px">
    当前销售城市为 <a id="linkProvince" href="#" onmouseover="showFloatCategoryProvince('ProvinceDiv');" onmouseout="hideFloatCategoryProvince('ProvinceDiv')">
        <span style="font-size: 14px; font-weight: bold;" id='SpanProvinceName'>
            <%= ProvinceName %></span><img src="<%=ImageServerUrl %>/IM/images/icodown.gif" style="margin-bottom: -1px;" /></a>
    &nbsp;<a id="linkCity" href="#" onmouseover="showFloatCategorySite('SiteDiv');" onmouseout="hideFloatCategorySite('SiteDiv')"><span
        style="font-size: 14px; font-weight: bold;" id="SpanSiteName"><%= SiteName %></span><img
            src="<%=ImageServerUrl %>/IM/images/icodown.gif" style="margin-bottom: -1px;" /></a><br />
    
</div>
