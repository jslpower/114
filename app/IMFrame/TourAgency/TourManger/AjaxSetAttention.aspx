<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxSetAttention.aspx.cs"
    Inherits="TourUnion.WEB.IM.TourAgency.AjaxSetAttention" %>

<%@ Register Assembly="ExporPage" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<script type="text/javascript">
    //获的己取消关注的批发商列表 将其对应的checkbox不选中
     function NoSettionCompanyList()
     {
         var AgencyId="<%=CompanyId %>";
         var NationType="<%=NationType %>";
         var SiteId=$("#DropSiteList").val().split(",")[0];
         var TourAreaId=$("#hidAreaId").val();
        var strUrl="/ShareWeb/IsAttentionCompany.aspx?GetMyAttetionCompany=1&SiteId="+SiteId+"&TourAreaId="+TourAreaId+"&AgencyId="+AgencyId+"&NationType="+NationType;
        $.ajax
            ({
                url:strUrl,
                cache:false,
                dataType:'json',
                success:function(html)
                {
                    if(html.DataList!=null)
                    {
                         var data=html.DataList;
                         for(var i=0;i<data.length;i++) 
                         {
                            var SiteId=data[i].SiteId;
                            var TourAreaId=data[i].TourAreaId;
                            var CompanyId=data[i].CompanyId;
                            $("#input_"+SiteId+"_"+TourAreaId+"_"+CompanyId).attr("checked","");
                         }
                     }
                }
            });
     }
     
     $(document).ready(function(){NoSettionCompanyList();});
</script>
<asp:repeater runat="server" id="repCompanyList">
        <HeaderTemplate>
            <table width="210" border="0" cellpadding="0" align="center" cellspacing="1" bgcolor="#eeeeee">
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <%#GetstrCompany(Convert.ToInt32(Eval("Id")), Eval("CompanyName").ToString(), Eval("ContactName").ToString(), Eval("ContactTel").ToString(), Eval("ContactMobile").ToString(), Eval("AreaId").ToString(),Eval("TendCompanyId").ToString(), Eval("isTendCompany").ToString())%>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:repeater>
<table width="210" border="0" visible="false" id="NoDate" runat="server" align="center"
    cellpadding="0" cellspacing="1" bgcolor="#A2CFE5">
    <tr>
        <td height="100" colspan="5" align="center" bgcolor="#FFFFFF">
            暂无批发商信息
        </td>
    </tr>
</table>
<table width="210" border="0" align="center" cellspacing="0" cellpadding="0">
    <tr>
        <td height="22" align="right">
            <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" PageStyleType="MostEasyNewButtonStyle"
                HrefType="JsHref"></cc1:ExporPageInfoSelect>
    </tr>
</table>
