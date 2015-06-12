<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TourContactInfo.ascx.cs"
    Inherits="UserBackCenter.usercontrol.RouteAgency.TourContactInfo" %>
<%@ Import Namespace="EyouSoft.Common" %>
<table width="96%" border="0" align="center" cellpadding="3" cellspacing="0" class="lankuang"
    style="margin-top: 10px;">
    <tr>
        <td width="12%" align="right" class="shenglan">
            <span style="color: #ff0000">*</span>线路负责人：
        </td>
        <td width="20%" align="left" class="zhonglan">
            <input name="<%=ReleaseType %>_TourContact" id="<%=ReleaseType %>_TourContact" type="text"
                valid="required" errmsg="请填写线路负责人!" value='<%=ContactName %>' />
            <input name="<%=ReleaseType %>_TourContactUserName" type="hidden" />
        </td>
        <td width="10%" align="right" class="shenglan">
            电话：
        </td>
        <td width="22%" align="left" class="zhonglan">
            <input name="<%=ReleaseType %>_TourContactTel" type="text" id="<%=ReleaseType %>_TourContactTel"
                value='<%=ContactTel %>' />
        </td>
        <td width="14%" align="right" class="shenglan">
            线路联系MQ号码：
        </td>
        <td width="22%" align="left" class="zhonglan">
            <input name="<%=ReleaseType %>_TourContacMQ" type="text" id="<%=ReleaseType %>_TourContacMQ"
                value='<%=ContactMQID %>' onblur="GetMQId()" />
            <input type="hidden" id="hidTourContactMQID" name="hidTourContactMQID" />
        </td>
    </tr>
</table>

<%--<script type="text/javascript" src="<%=JsManage.GetJsFilePath("autocomplete") %>"></script>--%>

<script type="text/javascript">   

function selectItem(tr){ 
    if (tr.cells.length > 0) {
        $("input[name=<%=ReleaseType %>TourContact]").val($(tr.cells[0]).html());
        $("input[name=<%=ReleaseType %>_TourContactTel]").val($(tr.cells[1]).html());
        $("input[name=<%=ReleaseType %>_TourContacMQ]").val($(tr.cells[2]).html());
    }
}
$(document).ready(function(){
$("#<%=ContainerID %>").find("input[name$=TourContact]").autocomplete("/RouteAgency/AjaxGetContactInfo.aspx?CompanyID=<%=CompanyID %>&rand=" + Math.random(),{
    delay:10,
    minChars:2,
    matchSubset:false,
    cacheLength:1,
    autoFill:false,
    maxItemsToShow:10,
    onItemSelect:selectItem,
    spaceCount:3,
    spaceFlag:"~&&~",
    addWidth:400, 
    IsFocusShow:true,
    extraParams: {flag:'select'}
});
});
function GetMQId()
{
    var MQ_UserName = $("input[name="+ releasetype +"_TourContacMQ]").val();
    $.ajax
    ({
        url:"/RouteAgency/AjaxGetContactInfo.aspx?CompanyID=<%=CompanyID %>&flag=MQ&MQUserName="+encodeURI(MQ_UserName)+"&rnd="+Math.random(),
        cache:false,
        success:function(html)
        {
            $("#hidTourContactMQID").val(html);
        }
    });
}
</script>

