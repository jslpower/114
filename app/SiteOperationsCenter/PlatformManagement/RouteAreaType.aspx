<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="RouteAreaType.aspx.cs"
    Inherits="SiteOperationsCenter.PlatformManagement.RouteAreaType" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/AreaList.ascx" TagName="AreaList" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>线路区域分类</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script language="JavaScript">
       var index=0;
       //已选择的线路区域是否刷新标识，但用户取消切换的时候不刷新
       var isIniteInfo=true;
       function mouseovertr(o) {
	      o.style.backgroundColor="#FFF9E7";
          //o.style.cursor="hand";
       }
       function mouseouttr(o) {
	      o.style.backgroundColor=""
       }
           
        //判断用户在切换城市的时候有没有选中线路区域
        function GetChkValue(typeID)
        {
            var obj;
            if(typeID==0)
            {
                obj="dalLongList";
            }
            else
            {
                obj="dalShortList";
            }
            var arr=new Array();
            var jQueryObj=$("#"+obj+" tr input[type='checkbox']");
            jQueryObj.each(function(i){
                if(this.checked)
	            {				
		            arr.push(this);
	            } 
              })
            return arr; 
        }
        //清空选中值
        function ClearChkValue(typeID)
        {
            var obj;
            if(typeID==0)
            {
                obj="dalLongList";
            }
            else
            {
                obj="dalShortList";
            }
            var arr=new Array();
            var jQueryObj=$("#"+obj+" tr input[type='checkbox']");
            jQueryObj.each(function(i){
                if(this.checked)
	            {				
		            this.checked=false;
	            } 
            })
             return arr; 
        }
       //菜单样式切换
       function ChangeClass(obj,typeID)
       {
            var arr = document.getElementsByName(obj.name);
            //当用户准备切换城市设置的时候，给予数据保存的提示
            var arrlength=GetChkValue(typeID);
            if(arrlength.length>0){
                if (confirm('您确定要切换设置吗？\n\n此前选择的数据将不会被保存！')) {
                    ClearChkValue(typeID);//清空选中值
                    if(arr != null && arr.length>0)
                    {
                        for(var i=0; i<arr.length; i++)
                        {
                            arr[i].className = "";
                        }
                    }
                    obj.className = "lion";
                    isIniteInfo=true;
                }else
                {
                    isIniteInfo=false;
                }
            }
            else
            {    //用户没选中线路区域的时候不给予提醒
                if(arr != null && arr.length>0)
                {
                    for(var i=0; i<arr.length; i++)
                    {
                        arr[i].className = "";
                    }
                }
                obj.className = "lion";
            }
        }
        //当添加成功后样式绑定给当前添加的那个省份或城市
        function SetClass(proid,cityid,typeid)
        {        
            //typeid:0-代表长线  1，代表短线
            if(typeid==0){
                    $("#a_Province_"+proid).attr("class", "lion");
                }
             else{
                    $("#<%=ddl_ProvinceList.ClientID %>").attr("value",proid);
                    ChangeList(proid);//根据选中省份显示出城市列表
                    $("#li_"+cityid).attr("class", "lion");
                }
        }
        //显示城市下已经选中的线路区域EditeID:为省份或者城市ID；typeID：0 为国内长线 1 为国内短线
        function ShowAreaAndCompany(ProvinceID,CityID,typeID)
        {
            if(isIniteInfo==true){//如果用户确认切换城市，再刷新城市对应的已选列表
                var url = "AjaxByAreaList.aspx?ProvinceID="+ProvinceID+"&CityID="+CityID+"&TypeID="+typeID+"&rnd="+Math.random();
                $.ajax
                ({
                    url:url,
                    cache:false,
                    success:function(html)
                    {
                        if(typeID==0)
                       {
                            $("#divLongArea").html(html); 
                            $("#hdProvinceID").val(ProvinceID);
                        }
                        else 
                        {
                            $("#divShortArea").html(html);
                            var pcStr=ProvinceID+"|"+CityID;
                            $("#hdCityID").val(pcStr);
                         }   
                    } 
                })
            }
        }
        //当省份城市下拉框改变选中省份时，DivCity显示对应省份下的所有城市
        function ChangeList(ProvinceId)
        {
            var url = "AjaxShowCityByProvince.ashx?ProvinceId="+ProvinceId;
            $.ajax
            ({
                url:url,
                cache:false,
                async: false,
                success:function(html)
                {
                    $("#DivCity").html(html); 
                } 
            })
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="95%" border="0" align="center" cellpadding="3" cellspacing="1" style="margin-top: 12px;">
        <tr>
            <td width="114" height="23" align="right" bgcolor="#C3E3F2">
                国内长线：
            </td>
            <td width="720" align="left" bgcolor="#C3E3F2">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="95%" border="1" align="center" cellpadding="5" cellspacing="0" bordercolor="#C8E0EB">
        <tr>
            <td colspan="4" align="left" bgcolor="#E8F2F7">
                <div id="div_ShowProvince" class="eali" runat="server">
                </div>
            </td>
        </tr>
    </table>
    <table width="95%" border="1" align="center" cellpadding="5" cellspacing="0" bordercolor="#C8E0EB">
        <tr>
            <td colspan="4" align="left" bgcolor="#E8F2F7">
                已选：
                <div id="divLongArea">
                </div>
            </td>
        </tr>
    </table>
    <table width="95%" border="0" align="center" cellpadding="3" cellspacing="0">
        <tr>
            <td align="left">
                <strong>请选择：</strong>
            </td>
        </tr>
    </table>
    <uc2:AreaList ID="AreaList1" runat="server" />
    <table width="25%" height="30" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <asp:Button ID="Button1" runat="server" Text="保存提交" Width="80px" Height="30px" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
    <table width="95%" border="0" align="center" cellpadding="3" cellspacing="1" style="margin-top: 12px;">
        <tr>
            <td width="114" height="23" align="right" bgcolor="#C3E3F2">
                国内短线：
            </td>
            <td width="720" align="left" bgcolor="#C3E3F2">
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <nobr> <asp:DropDownList ID="ddl_ProvinceList" runat="server" ></asp:DropDownList>
<div id="DivCity"  class="eali"></div></nobr>
            </td>
        </tr>
    </table>
    <table width="95%" border="1" align="center" cellpadding="5" cellspacing="0" bordercolor="#C8E0EB">
        <tr>
            <td colspan="4" align="left" bgcolor="#E8F2F7">
                已选：
                <div id="divShortArea">
                </div>
            </td>
        </tr>
    </table>
    <table width="95%" border="0" align="center" cellpadding="3" cellspacing="0">
        <tr>
            <td align="left">
                <strong>请选择：</strong>
            </td>
        </tr>
    </table>
    <uc2:AreaList ID="AreaList2" runat="server" />
    <table width="25%" height="30" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <asp:Button ID="btn_SaveShortArea" runat="server" Text="保存提交" Width="80px" Height="30px"
                    OnClick="btn_SaveShortArea_Click" />
                <input type="hidden" runat="server" id="hdProvinceID" name="hdProvinceID" />
                <input type="hidden" runat="server" id="hdCityID" name="hdCityID" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
