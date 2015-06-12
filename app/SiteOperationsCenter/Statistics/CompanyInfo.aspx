<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyInfo.aspx.cs" Inherits="SiteOperationsCenter.Statistics.CompanyInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            background-color: #EBF4FF;
            margin: 0;
            color: #333333;
            font-family: "宋体" ,Arial,Helvetica,sans-serif;
            font-size: 12px;
        }
        table
        {
            border-collapse: collapse;
        }
        td
        {
            font-size: 12px;
            line-height: 20px;
        }
        .lr_hangbg
        {
            color: #000000;
            font-size: 12px;
            line-height: normal;
        }
        .font12_grean
        {
            color: #008702;
            font-size: 12px;
        }
        .mainbody
        {
            margin-left: auto;
            margin-right: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="mainbody">
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="1">
            <tr>
                <td colspan="2">
                    <strong>单位名称：</strong><span class="font12_grean"><asp:Literal ID="ltrCompanyName" runat="server"></asp:Literal></span>&nbsp;&nbsp;
                    
                </td>               
            </tr>
            <tr>
                <td>                    
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
    </form>
</body>
</html>
