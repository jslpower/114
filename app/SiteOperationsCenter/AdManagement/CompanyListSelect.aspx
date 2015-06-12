<%@ Page Language="C#" enableEventValidation="false" AutoEventWireup="true" CodeBehind="CompanyListSelect.aspx.cs" Inherits="SiteOperationsCenter.AdManagement.CompanyListSelect" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Src="../usercontrol/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>单位查找</title>
    <link href="<%=EyouSoft.Common.CssManage.GetCssFilePath("main") %>" rel="stylesheet"
        type="text/css" />
            <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#E1E6F1">
        <tr>
            <td>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" class="tab_luand" style="font-size:small">
<%--                    <tr class="lr_hangbg">
                    <td>
                            <div id="divRdoList">
                                <asp:RadioButtonList ID="rdolisttype" runat="server" 
                                    RepeatDirection="Horizontal">
                                </asp:RadioButtonList>
                            </div> 
                    </td>
                    </tr>--%>
                    <tr class="lr_hangbg" > 
                        <td width="12%" align="left" style=" font-size:12px;">
                            <span class="unnamed1">
                                <img src="<%=ImageServerUrl %>/Images/point16.gif" width="21" height="21"></span>
                            
                            <uc1:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />公司类型: 
                            <asp:DropDownList ID="ddlCompanyType" runat="server">
                            </asp:DropDownList>公司名称：
                            <input name="CompanyName" type="text" id="txtCompanyName" value="<%=CompanyName %>" class="textfield" style="width: 80px;" />
                            <input type="button" name="btnSearch" value="查询" id="btnSearch" class="an_tijiaobaocun"  />
                            <%--onclick="return btnSearch_onclick()"--%>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" style="background-color: #EBF4FF; text-align:center; font-size: 12px; line-height: normal;
                            color: #000000;font-size:small">
                  
                            <ul style="width:615px; border:1px solid #4F9DE3; padding-top:10px; list-style-type:none;">
                                <asp:Repeater runat="server" ID="rptQueryTour">
                                    <ItemTemplate>
                                        <li style=" border-bottom:1px solid #4F9DE3; border-right:1px solid #4F9DE3;  width: 200px;
                                            float: left; text-align:left;"><strong style="color: #A94711">
                                                <input name="CompanyID" id="companyId<%#Eval("ID") %>" type="radio" value='<%#Eval("ID") %>'>
                                                <label for="companyId<%#Eval("ID") %>"><span id="<%#Eval("ID") %>"><%# Eval("CompanyName")%></span></label>
                                                <input type="hidden" id="hdfcompanyMq" value="<%#Eval("ContactInfo.MQ") %>" />
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                               <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                            </ul>
                        </td>
                    </tr>                  
                </table>
                <div id="ExportPage" class="F2Back" style="text-align: right; font-size:small" height="40">
                <div id="OrdersAllOutSource_ExportPage">
                    <cc2:ExportPageInfo ID="ExportPageInfo1" 
                        runat="server"></cc2:ExportPageInfo></div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" height="40" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center">
                            &nbsp;<input type="button" name="btnSelect" value="保 存" onclick="GetSelectCompany();"
                                id="btnSelect" class="renyuan_an" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>" cache="true"></script>
    <script type="text/javascript" language="javascript">
        function closeWin(){            
            parent.Boxy.getIframeDialog(parent.Boxy.queryString("iframeId")).hide();            
        }
        function GetSelectCompany(){
            var compId="";
            var comName="";
            var mq="";
            var radios=document.getElementsByName("CompanyID");
            for(var i=0;i<radios.length;i++){
                if(radios[i].checked){
                    compId=radios[i].value;
                    comName=$("#"+compId).html();
                    mq=$("#hdfcompanyMq").val();
                }
            }
            parent.$("#txtBuyUnit").val(comName);
            parent.$("#hdfUnitId").val(compId);
            parent.$("#hdfUnitMQ").val(mq);
            parent.Boxy.getIframeDialog(parent.Boxy.queryString("iframeId")).hide();            
        }
        $(document).ready(function() {
            $("#btnSearch").click(function() {
                var companyName = escape($("#txtCompanyName").val());
                var cityid = $("#ProvinceAndCityList1_ddl_CityList").val();
                var provinceid = $("#ProvinceAndCityList1_ddl_ProvinceList").val();
                var companyType = $("#<%=ddlCompanyType.ClientID %>").val();
                window.location.href = "CompanyListSelect.aspx?CompanyName=" + companyName + "&Province=" + provinceid + "&City=" + cityid+"&companyType="+ encodeURIComponent(companyType);
            });
        });
function show()
{
 //分页控件链接控制
            $("#OrdersAllOutSource_ExportPage a").each(function(){
                $(this).click(function(){   
                      
                $(this).attr("target","mainFrame");
                              window.location.href=$(this).attr("href");
                    return false;
                })
            });
}
    </script>

    </form>
</body>
</html>
