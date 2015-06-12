<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TourServiceStandard.ascx.cs"
    Inherits="UserBackCenter.usercontrol.Linecontrol.TourServiceStandard" %>
<table width="96%" border="0" align="center" cellpadding="3" cellspacing="0" class="lankuang"
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
            <a href="javascript:void(0);" onclick="GoOpenDialog('选择住宿安排','1')">住&nbsp;&nbsp;&nbsp;宿<img
                src="<%=ImageServerPath %>/images/ns-expand.gif" width="11" height="12" border="0" /></a>：
        </td>
        <td align="left" class="zhonglan">
            <textarea name="<%=ReleaseType %>ResideContent" id="<%=ReleaseType %>ResideContent"
                cols="100" rows="3" class="input"><%=tmpResideContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right" class="shenglan_lr">
            <a href="javascript:void(0);" onclick="GoOpenDialog('选择用餐安排','2')">用&nbsp;&nbsp;&nbsp;餐<img
                src="<%=ImageServerPath %>/images/ns-expand.gif" width="11" height="12" border="0" /></a>：
        </td>
        <td align="left" class="zhonglan">
            <textarea name="<%=ReleaseType %>DinnerContent" id="<%=ReleaseType %>DinnerContent"
                cols="100" rows="3" class="input"><%=tmpDinnerContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right" class="shenglan_lr">
            <a href="javascript:void(0);" onclick="GoOpenDialog('选择景点安排','3')">景&nbsp;&nbsp;&nbsp;点<img
                src="<%=ImageServerPath %>/images/ns-expand.gif" width="11" height="12" border="0" /></a>：
        </td>
        <td align="left" class="zhonglan">
            <textarea name="<%=ReleaseType %>SightContent" id="<%=ReleaseType %>SightContent"
                cols="100" rows="3" class="input"><%=tmpSightContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right" class="shenglan_lr">
            <a href="javascript:void(0);" onclick="GoOpenDialog('选择用车安排','4')">用&nbsp;&nbsp;&nbsp;车<img
                src="<%=ImageServerPath %>/images/ns-expand.gif" width="11" height="12" border="0" /></a>：
        </td>
        <td align="left" class="zhonglan">
            <textarea name="<%=ReleaseType %>CarContent" id="<%=ReleaseType %>CarContent" cols="100"
                rows="3" class="input"><%=tmpCarContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right" class="shenglan_lr">
            <a href="javascript:void(0);" onclick="GoOpenDialog('选择导游安排','5')">导&nbsp;&nbsp;&nbsp;游<img
                src="<%=ImageServerPath %>/images/ns-expand.gif" width="11" height="12" border="0" /></a>：
        </td>
        <td align="left" class="zhonglan">
            <textarea name="<%=ReleaseType %>GuideContent" id="<%=ReleaseType %>GuideContent"
                cols="100" rows="3" class="input"><%=tmpGuideContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right" class="shenglan_lr">
            <a href="javascript:void(0);" onclick="GoOpenDialog('选择往返交通安排','6')">往返交通<img src="<%=ImageServerPath %>/images/ns-expand.gif"
                width="11" height="12" border="0" /></a>：
        </td>
        <td align="left" class="zhonglan">
            <textarea name="<%=ReleaseType %>TrafficContent" id="<%=ReleaseType %>TrafficContent"
                cols="100" rows="3" class="input"><%=tmpTrafficContent%></textarea>
        </td>
    </tr>
    <tr>
        <td align="right" class="shenglan_lr">
            <a href="javascript:void(0);" onclick="GoOpenDialog('选择其它安排','7')">其&nbsp;&nbsp;&nbsp;它<img
                src="<%=ImageServerPath %>/images/ns-expand.gif" width="11" height="12" border="0" /></a>：
        </td>
        <td align="left" class="zhonglan">
            <textarea name="<%=ReleaseType %>IncludeOtherContent" id="<%=ReleaseType %>IncludeOtherContent"
                cols="100" rows="3" class="input"><%=tmpIncludeOtherContent%></textarea>
        </td>
    </tr>
</table>

