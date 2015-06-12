<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxPlanerBuyerQuery.aspx.cs" EnableViewState="false" Inherits="SiteOperationsCenter.TyProductManage.AjaxPlanerBuyerQuery" %>

<fieldset >
 <legend>公司信息</legend>
<div class="mainbody">
        <table width="80%" border="0" align="center" cellpadding="0" cellspacing="1">
            <colgroup align="left"></colgroup>
            <colgroup align="left"></colgroup>
            <tr>
                <td colspan="2" >
                    <strong>单位名称：</strong><span class="font12_grean"><asp:Literal ID="ltrCompanyName" runat="server"></asp:Literal></span>&nbsp;&nbsp;
                </td>               
            </tr>
            <tr>
                <td width="50%">                    
                    <strong>许可证号：</strong><span id="lblLicense" class="font12_grean"><asp:Literal ID="ltrLicense" runat="server"></asp:Literal></span>
                </td>
                <td>
                    <strong>所属地区：</strong><span class="font12_grean"><asp:Literal ID="ltrProvinceName" runat="server"></asp:Literal>
                    </span>
                    <span class="font12_grean">
                    <asp:Literal ID="ltrCityName" runat="server"></asp:Literal>
                        </span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>用户名：</strong><span class="font12_grean">
                    <asp:Literal ID="ltrUserName" runat="server"></asp:Literal></span>
                </td>
                <td>
                    <strong>负责人：</strong><span class="font12_grean">
                    <asp:Literal ID="ltrContactName" runat="server"></asp:Literal></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>电话：</strong><span class="font12_grean">
                        <asp:Literal ID="ltrTel" runat="server"></asp:Literal>
                    </span>
                </td>
                <td>
                    <strong>传真：</strong><span class="font12_grean">
                    <asp:Literal ID="ltrFax" runat="server"></asp:Literal></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>手机：</strong><span class="font12_grean">
                    <asp:Literal ID="ltrMobile" runat="server"></asp:Literal></span>
                </td>
                <td>
                    <strong>引荐人：</strong><span class="font12_grean">
                     <asp:Literal ID="ltrCommendPeople" runat="server"></asp:Literal>   
                    </span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong>办公地点：</strong><span class="font12_grean">
                     <asp:Literal ID="ltrCompanyAddress" runat="server"></asp:Literal>   
                    </span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong>经营范围： </strong><span id="lblManageArea" class="font12_grean">
                      <asp:Literal ID="ltrShortRemark" runat="server"></asp:Literal>   
                    </span>
                   
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong>公司介绍：</strong><span>
                    <asp:Literal ID="ltrRemark" runat="server"></asp:Literal>   
                        </span>
                </td>
            </tr>
        </table>
    </div>
</fieldset>

<fieldset>
<legend>用户信息</legend>
<table width="80%" border="0" align="center" cellpadding="0" cellspacing="1">
            <colgroup align="left"></colgroup>
            <colgroup align="left"></colgroup>
            <tr>
                <td width="50%">                    
                    <strong>姓名：</strong><span id="Span1" class="font12_grean"><asp:Literal ID="ltrUserContactName" runat="server"></asp:Literal></span>
                </td>
                <td width="50%">                    
                    <strong>性别：</strong><span id="Span3" class="font12_grean"><asp:Literal ID="ltrUserSex" runat="server"></asp:Literal></span>
                </td>
            </tr>
            <tr>
                <td width="50%">                    
                    <strong>是否管理员：</strong><span id="Span4" class="font12_grean"><asp:Literal ID="ltrIsAdmin" runat="server"></asp:Literal></span>
                </td>
                
                <td>
                    <strong>用户名：</strong><span class="font12_grean">
                    <asp:Literal ID="ltrUserName1" runat="server"></asp:Literal></span>
                </td>
            </tr>
            <tr>
                 <td>
                    <strong>MQ：</strong><span class="font12_grean">
                    <asp:Literal ID="ltrUserMQ" runat="server"></asp:Literal></span>
                </td>
                <td>
                    <strong>手机：</strong><span class="font12_grean">
                    <asp:Literal ID="ltrUserMobile" runat="server"></asp:Literal></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>电话：</strong><span class="font12_grean">
                        <asp:Literal ID="ltrUserTel" runat="server"></asp:Literal>
                    </span>
                </td>
                <td>
                    <strong>传真：</strong><span class="font12_grean">
                    <asp:Literal ID="ltrUserFax" runat="server"></asp:Literal></span>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>邮箱：</strong><span class="font12_grean">
                        <asp:Literal ID="ltrUserEmail" runat="server"></asp:Literal>
                    </span>
                </td>
                <td>
                    <strong>MSN：</strong><span class="font12_grean">
                    <asp:Literal ID="ltrUserMSN" runat="server"></asp:Literal></span>
                </td>
            </tr>
            <tr>
            <td colspan="2">
                    <strong>所属地区：</strong><span class="font12_grean"><asp:Literal ID="ltrUserProvinceName" runat="server"></asp:Literal>
                    </span>
                    <span class="font12_grean">
                    <asp:Literal ID="ltrUserCityName" runat="server"></asp:Literal>
                        </span>
                </td>
            </tr>
        </table>
</fieldset>
