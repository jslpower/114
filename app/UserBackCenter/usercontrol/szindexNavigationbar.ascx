<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="szindexNavigationbar.ascx.cs" Inherits="Demo1.usercontrol.szindexNavigationbar" %>
<script type="text/javascript">

$(document).ready(function()
{  
  
   var szindexNavigationBarTab=$("#szindexNavigationBar_<%=TabIndex%>");
   if(szindexNavigationBarTab)
   {
     szindexNavigationBarTab.attr("class","tianon2");
   }
 
});

</script>
<table id="szindexNavigationBar" border="0" cellpadding="0" cellspacing="0" height="33" width="100%">
          <tbody><tr>
<%--              <td style="border-bottom: 1px solid rgb(171, 201, 217); width: 30px;">&nbsp;</td>
          <td class="tianup2" id="szindexNavigationBar_tab1"><a href="/SystemSet/PersonInfoSet.aspx" rel="toptaburl">个人设置 </a></td>
            <td style="border-bottom: 1px solid rgb(171, 201, 217); width: 10px;">&nbsp;</td>
            <td class="tianup2" id="szindexNavigationBar_tab2"><a href="/SystemSet/CompanyInfoSet.aspx" rel="toptaburl" >单位信息管理 </a></td>--%>
            <%--<td style="border-bottom: 1px solid rgb(171, 201, 217); width: 10px;">&nbsp;</td>
			<td class="tianup2" id="szindexNavigationBar_tab3"><a href="/SystemSet/RouteAreaManage.aspx" rel="toptaburl">线路区域管理</a></td>--%>
			<td style="border-bottom: 1px solid rgb(171, 201, 217); width: 10px;">&nbsp;</td>
			<td class="tianup2" id="szindexNavigationBar_tab4"><a href="/SystemSet/DepartManage.aspx" rel="toptaburl">部门设置 </a></td>
			<td style="border-bottom: 1px solid rgb(171, 201, 217); width: 10px;">&nbsp;</td>
            <td class="tianup2" id="szindexNavigationBar_tab5"><a href="/SystemSet/SonUserManage.aspx" rel="toptaburl">子帐户管理 </a></td>
			<td style="border-bottom: 1px solid rgb(171, 201, 217); width: 10px;">&nbsp;</td>
            <td class="tianup2" id="szindexNavigationBar_tab6"><a href="/SystemSet/PermitManage.aspx" rel="toptaburl">权限管理</a></td>
			<%--<td style="border-bottom: 1px solid rgb(171, 201, 217); width: 10px;">&nbsp;</td>
            <td class="tianup2" id="szindexNavigationBar_tab7"><a href="/SystemSet/BasicInfoSet.aspx" rel="toptaburl">基础信息维护</a></td>--%>
            <td style="border-bottom: 1px solid rgb(171, 201, 217);">&nbsp;</td>
          </tr>
        </tbody></table>
<script type="text/javascript">
    $(function() {
        $("#szindexNavigationBar a[rel='toptaburl']").click(function() {
            topTab.url(topTab.activeTabIndex, $(this).attr("href"));
            return false;
        });


    });

</script>