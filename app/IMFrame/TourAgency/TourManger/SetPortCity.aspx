<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetPortCity.aspx.cs" EnableEventValidation="false"
    Inherits="TourUnion.WEB.IM.TourAgency.SetPortCity" %>

<%@ Register Src="DropSiteAndCityList.ascx" TagName="DropSiteAndCityList" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>组团栏目</title>
    <style type="text/css">
        BODY
        {
            color: #333;
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            text-align: center;
            background: #fff;
            margin: 0px;
        }
        img
        {
            border: thin none;
        }
        table
        {
            border-collapse: collapse;
            margin: 0px auto;
            padding: 0px auto;
        }
        TD
        {
            font-size: 12px;
            color: #0E3F70;
            line-height: 20px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
        }
        div
        {
            margin: 0px auto;
            text-align: left;
            padding: 0px auto;
            border: 0px;
        }
        textarea
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        select
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        .ff0000
        {
            color: #f00;
        }
        a
        {
            color: #0E3F70;
            text-decoration: none;
        }
        a:hover
        {
            color: #f00;
            text-decoration: underline;
        }
        a:active
        {
            color: #f00;
            text-decoration: none;
        }
        .bar_on_comm
        {
            width: 105px;
            height: 21px;
            float: left;
            border: 1px solid #94B2E7;
            border-bottom: 0px;
            background: #ffffff;
            text-align: center;
        }
        .bar_on_comm a
        {
            color: #cc0000;
        }
        .bar_un_comm
        {
            width: 105px;
            height: 21px;
            float: left;
            text-align: center;
        }
        .bar_un_comm a
        {
            color: #0E3F70;
        }
        a.cliewh
        {
            display: block;
            width: 190px;
            height: 22px;
            overflow: hidden;
        }
        .aun
        {
            background: url(/IM/images/sreach_annui.gif) no-repeat center;
            text-align: center;
        }
        .aun a
        {
            color: #1E446F;
            font-size: 14px;
        }
        .aun a:visited
        {
            color: #1E446F;
            font-size: 14px;
        }
        .aun a:hover
        {
            color: #f00;
            font-size: 14px;
        }
        .aon
        {
            background: url(/IM/images/areabottonon.gif) no-repeat center;
            text-align: center;
        }
        .aon a
        {
            color: #000;
            font-weight: bold;
            font-size: 14px;
        }
    </style>

    <script type="text/javascript">
    function CheckIsNext()
    {
        var ProvinceId=document.getElementById("DropSiteAndCityList1_dropProvince").value;
        var CityId=document.getElementById("DropSiteAndCityList1_dropCity").value;
        var ErrArry=new  Array();
        if(ProvinceId=="0"||CityId=="0")
        {
           alert("请选择出港城市!\n");
            event.returnValue = false;
            return;
        }
        var isCheck=document.getElementById("ckSetCity");
        if(!isCheck.checked)
        {
           alert("请确定是否要将此城市设为首选出港!\n");
            event.returnValue = false;
        }
       
    }
    </script>

</head>
<body oncontextmenu="return false;">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td valign="top" background="/IM/images/ztopbj.gif">
                <table width="210" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 6px;">
                    <tr>
                        <td align="left">
                            <div class="bar_on_comm">
                                <a href="javascript:void(0)" onfocus="blur()">指定供应商的产品</a>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="210" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table width="207" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 8px;">
                    <tr>
                        <td>
                            <img src="/IM/images/zt_top.gif" width="207" height="15" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="border-left: 1px solid #F7E19E; border-right: 1px solid #F7E19E;
                            background: #FFFDF1; color: #333333; line-height: 14px;">
                            &nbsp;<img src="/IM/images/z_r.gif" width="13" height="12" />
                            第一步：设置出港城市，并设为首<br />
                            &nbsp;&nbsp;&nbsp; 选出港。
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="border-left: 1px solid #F7E19E; border-right: 1px solid #F7E19E;
                            background: #FFFDF1; color: #999999; line-height: 14px;">
                            &nbsp;<img src="/IM/images/z_r.gif" width="13" height="12" />
                            第二步：选择您经常交易的专线商<br />
                            &nbsp;&nbsp;&nbsp; (批发商)，完成！
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img src="/IM/images/zt_bottom.gif" width="207" height="4" />
                        </td>
                    </tr>
                </table>
                <table width="200" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 8px;">
                    <tr>
                        <td align="left">
                            <img src="/IM/images/icobu.gif" width="16" height="16" style="margin-bottom: -3px;" />
                            <strong>第一步：设置出港城市</strong>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <img src="/IM/images/lineb.gif" width="210" height="6" />
                        </td>
                    </tr>
                </table>
                <table width="200" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="23" align="left" valign="bottom">
                            出港城市:
                            <uc1:DropSiteAndCityList ID="DropSiteAndCityList1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="left" valign="bottom">
                            <input name="checkbox" id="ckSetCity" type="checkbox" value="checkbox" />设为首选出港！
                        </td>
                    </tr>
                    <tr>
                        <td height="30" align="center" valign="bottom">
                            <asp:Button ID="btnNext" runat="server" Text="下一步" OnClick="btnNext_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td height="60" align="left" valign="bottom" style="border-top: 1px dashed #999999;">
                            <span style="color: #999999; line-height: 14px;">注：出港城市指团队所有成员集合后统一搭乘飞机、火车、汽车或轮船出发旅游的城市</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
