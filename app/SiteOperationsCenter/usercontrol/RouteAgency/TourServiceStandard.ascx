<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TourServiceStandard.ascx.cs"
    Inherits="SiteOperationsCenter.usercontrol.RouteAgency.TourServiceStandard" %>
<%--<table width="96%" border="0" align="center" cellpadding="3" cellspacing="0" class="lankuang"
    style="margin-top: 15px;">
    <tr>
        <td width="10%" align="center" class="shenglan_lr">
            <strong>包含项目</strong>
        </td>
        <td width="80%" align="center" class="shenglan_lr">
            <strong>内容</strong>
        </td>
    </tr>
    <tr>
        <td align="right" class="shenglan_lr">
            <a href="javascript:void(0);" onclick="TourModule.OpenDialog('选择住宿安排','/RouteAgency/ServiceStandardList.aspx?Type=1&ReleaseType=<%=ReleaseType %>&ContainerID=<%=ContainerID %>&rnd='+ Math.random(),550,400);return false;">
                住&nbsp;&nbsp;&nbsp;宿<img src="<%=ImageServerPath %>/images/ns-expand.gif" width="11"
                    height="12" border="0" /></a>：
        </td>
        <td align="left" class="zhonglan">
            <textarea name="<%=ReleaseType %>ResideContent" id="<%=ReleaseType %>ResideContent" cols="100" rows="3" class="input"><%=tmpResideContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right" class="shenglan_lr">
            <a href="javascript:void(0);" onclick="TourModule.OpenDialog('选择用餐安排','/RouteAgency/ServiceStandardList.aspx?Type=2&ReleaseType=<%=ReleaseType %>&ContainerID=<%=ContainerID %>&rnd='+ Math.random(),550,400);return false;">
                用&nbsp;&nbsp;&nbsp;餐<img src="<%=ImageServerPath %>/images/ns-expand.gif" width="11"
                    height="12" border="0" /></a>：
        </td>
        <td align="left" class="zhonglan">
            <textarea name="<%=ReleaseType %>DinnerContent" id="<%=ReleaseType %>DinnerContent" cols="100" rows="3" class="input"><%=tmpDinnerContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right" class="shenglan_lr">
            <a href="javascript:void(0);" onclick="TourModule.OpenDialog('选择景点安排','/RouteAgency/ServiceStandardList.aspx?Type=3&ReleaseType=<%=ReleaseType %>&ContainerID=<%=ContainerID %>&rnd='+ Math.random(),550,400);return false;">
                景&nbsp;&nbsp;&nbsp;点<img src="<%=ImageServerPath %>/images/ns-expand.gif" width="11"
                    height="12" border="0" /></a>：
        </td>
        <td align="left" class="zhonglan">
            <textarea name="<%=ReleaseType %>SightContent" id="<%=ReleaseType %>SightContent" cols="100" rows="3" class="input"><%=tmpSightContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right" class="shenglan_lr">
            <a href="javascript:void(0);" onclick="TourModule.OpenDialog('选择用车安排','/RouteAgency/ServiceStandardList.aspx?Type=4&ReleaseType=<%=ReleaseType %>&ContainerID=<%=ContainerID %>&rnd='+ Math.random(),550,400);return false;">
                用&nbsp;&nbsp;&nbsp;车<img src="<%=ImageServerPath %>/images/ns-expand.gif" width="11"
                    height="12" border="0" /></a>：
        </td>
        <td align="left" class="zhonglan">
            <textarea name="<%=ReleaseType %>CarContent" id="<%=ReleaseType %>CarContent" cols="100" rows="3" class="input"><%=tmpCarContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right" class="shenglan_lr">
            <a href="javascript:void(0);" onclick="TourModule.OpenDialog('选择导游安排','/RouteAgency/ServiceStandardList.aspx?Type=5&ReleaseType=<%=ReleaseType %>&ContainerID=<%=ContainerID %>&rnd='+ Math.random(),550,400);return false;">
                导&nbsp;&nbsp;&nbsp;游<img src="<%=ImageServerPath %>/images/ns-expand.gif" width="11"
                    height="12" border="0" /></a>：
        </td>
        <td align="left" class="zhonglan">
            <textarea name="<%=ReleaseType %>GuideContent" id="<%=ReleaseType %>GuideContent" cols="100" rows="3" class="input"><%=tmpGuideContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right" class="shenglan_lr">
            <a href="javascript:void(0);" onclick="TourModule.OpenDialog('选择往返交通安排','/RouteAgency/ServiceStandardList.aspx?Type=6&ReleaseType=<%=ReleaseType %>&ContainerID=<%=ContainerID %>&rnd='+ Math.random(),550,400);return false;">
                往返交通<img src="<%=ImageServerPath %>/images/ns-expand.gif" width="11" height="12"
                    border="0" /></a>：
        </td>
        <td align="left" class="zhonglan">
            <textarea name="<%=ReleaseType %>TrafficContent" id="<%=ReleaseType %>TrafficContent" cols="100" rows="3" class="input"><%=tmpTrafficContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right" class="shenglan_lr">
            <a href="javascript:void(0);" onclick="TourModule.OpenDialog('选择其它安排','/RouteAgency/ServiceStandardList.aspx?Type=7&ReleaseType=<%=ReleaseType %>&ContainerID=<%=ContainerID %>&rnd='+ Math.random(),550,400);return false;">
                其&nbsp;&nbsp;&nbsp;它<img src="<%=ImageServerPath %>/images/ns-expand.gif" width="11"
                    height="12" border="0" /></a>：
        </td>
        <td align="left" class="zhonglan">
            <textarea name="<%=ReleaseType %>IncludeOtherContent" id="<%=ReleaseType %>IncludeOtherContent" cols="100" rows="3" class="input"><%=tmpIncludeOtherContent%></textarea>
        </td>
    </tr>
</table>--%>
<table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#CCCCCC">
    <tr>
        <td width="25%" align="right">
            <a href="/RouteAgency/ServiceStandardList.aspx?Type=1&ControlID=txt_zs&ReleaseType=<%=ReleaseType %>&ContainerID=<%=ContainerID %>&rnd="
                onclick="return ShowBoxy(this)">住 宿</a>：
        </td>
        <td width="75%" align="left">
            <textarea id="txt_zs" cols="65" name="txt_zs" rows="3"><%=tmpResideContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right">
            <a href="/RouteAgency/ServiceStandardList.aspx?Type=2&ControlID=txt_yc&ControlID=txt_qt&ReleaseType=<%=ReleaseType %>&ContainerID=<%=ContainerID %>&rnd="
                onclick="return ShowBoxy(this)">用 餐</a>：
        </td>
        <td>
            <textarea id="txt_yc" cols="65" name="txt_yc" rows="3"><%=tmpDinnerContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right">
            <a href="/RouteAgency/ServiceStandardList.aspx?Type=3&ControlID=txt_jd&ReleaseType=<%=ReleaseType %>&ContainerID=<%=ContainerID %>&rnd="
                onclick="return ShowBoxy(this)">景 点：</a>
        </td>
        <td>
            <textarea id="txt_jd" cols="65" name="txt_jd" rows="3"><%=tmpSightContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right">
            <a href="/RouteAgency/ServiceStandardList.aspx?Type=4&ControlID=txt_car&ControlID=txt_qt&ReleaseType=<%=ReleaseType %>&ContainerID=<%=ContainerID %>&rnd="
                onclick="return ShowBoxy(this)">用 车：</a>
        </td>
        <td>
            <textarea id="txt_car" cols="65" name="txt_car" rows="3"><%=tmpCarContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right">
            <a href="/RouteAgency/ServiceStandardList.aspx?Type=5&ControlID=txt_dy&ReleaseType=<%=ReleaseType %>&ContainerID=<%=ContainerID %>&rnd="
                onclick="return ShowBoxy(this)">导 游：</a>
        </td>
        <td>
            <textarea id="txt_dy" cols="65" name="txt_dy" rows="3"><%=tmpGuideContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right">
            <a href="/RouteAgency/ServiceStandardList.aspx?Type=6&ControlID=txt_fhjt&ReleaseType=<%=ReleaseType %>&ContainerID=<%=ContainerID %>&rnd="
                onclick="return ShowBoxy(this)">往返交通：</a>
        </td>
        <td>
            <textarea id="txt_fhjt" name="txt_fhjt" cols="65" rows="3"><%=tmpTrafficContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right">
            <a href="/RouteAgency/ServiceStandardList.aspx?Type=7&ControlID=txt_qt&ReleaseType=<%=ReleaseType %>&ContainerID=<%=ContainerID %>&rnd="
                onclick="return ShowBoxy(this)">其它包含：</a>
        </td>
        <td>
            <textarea id="txt_qt" name="txt_qt" cols="65" rows="3"><%=tmpIncludeOtherContent%></textarea>
        </td>
    </tr>
</table>

<script type="text/javascript">
    var ShowBoxy = function(obj) {
        Boxy.iframeDialog({
            iframeUrl: $(obj).attr("href") + Math.random(),
            title: "选 择 " + $(obj).text() + " 安 排",
            modal: true,
            width: "550",
            height: "520"
        });
        //        var obj = $(obj);
        //        new Controls.Dialog(obj.attr("href") + Math.random(), "选 择 " + obj.text() + " 安 排", BoxyData);
        return false;
    }
    
</script>

