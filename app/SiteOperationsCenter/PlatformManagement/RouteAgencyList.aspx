<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RouteAgencyList.aspx.cs"
    Inherits="SiteOperationsCenter.PlatformManagement.RouteAgencyList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>批发商列表</title>
    <link href="<%=CssManage.GetCssFilePath("acss") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("ajaxpagecontrols") %>"></script>

    <script type="text/javascript">
         var currindex=0;
         function LoadList(pageIndex) {
            var kw = '';
            kw = $("#<%=txt_CompanyName.ClientID %>").val();
            var urlParams ="Page="+pageIndex+"&RouteAreaId=<%=AreaId %>&CityID=<%=cityID %>&kw=" + encodeURI(kw);
            if(urlParams != '') 
            {
                $("#RAgencyList").html("<img id=\"img_loading\" src='<%= ImageServerUrl %>/images/loadingnew.gif' border=\"0\" /><br />&nbsp;正在加载...&nbsp;");
                $.ajax({
                    type: "GET",
                    url: "/PlatformManagement/AjaxByRouteAgencyList.aspx",
                    data: urlParams,
                    async: false,
                    success: function(msg) {
                        $("#RAgencyList").html(msg);
                    }
                });
                var config = {
                    pageSize:40,
                    pageIndex: pageIndex,
                    recordCount:inputVal,
                    pageCount: 0,
                    gotoPageFunctionName: 'AjaxPageControls.gotoPage',
                    showPrev: true,
                    showNext: true
                }
                AjaxPageControls.replace("DivPage", config);
                AjaxPageControls.gotoPage = function(pIndex) {
                    LoadList(pIndex);
                }
            }
            BindCbkClick();
        }
        function BindCbkClick()
        {
            $("input[type]='checkbox'").each(function(i){
                var obj=this;
                $("#divTag").children().each(function(){
                    if($(obj).attr("value")==$(this).attr("value"))
                    {
                        $(obj).attr("checked",true);
                    }
                });
                $(this).click(function(){
                    if($(this).attr("checked"))
                    {
                        //新选择的批发商数量加上已选择的批发商数量不得超过五家
                        if(( Number(currindex)+Number(<%=selectCompanyCount%>))>5)
                        {
                            alert("最多只能选择6个批发商");
                            $(this).attr("checked",false);
                        }
                        else
                        {
                             $("#divTag").append("<label IsDefault='false' value='"+$(this).attr("value")+"' id='lb_"+$(this).attr("ID")+"'><img src='<%=ImageServerUrl%>/images/gou.gif' width='15' height='14' />"+$(this).attr("Text")+"<a href='javascript:;' onclick='Del(this)'>删除</a><input type='hidden' name='hNewVal' value='"+$(this).attr("value")+"' /></label>");
                             currindex+=1; 
                        }
                    }
                    else
                    {
                        if($(this).attr("isdefault")=="true") //删除已经选择的批发商【真实删除】
                        {
                            var falg=DeleteAgency($(this).attr("value"));
                            $(this).attr("checked",falg?false:true);
                        }
                        else
                        {
                            Del($("#lb_"+$(this).attr("ID")).children(0)); //【虚拟删除】
                        }
                    }
                });
            });
        }
  
        $(document).ready(function(){ 
            $("#btn_Query").click(function() {
                LoadList(1);
            });
            $("#<%=txt_CompanyName.ClientID %>").keydown(function(event) {
                if (event.keyCode == 13) {
                    $("#btn_Query").click();
                    return false;
                }
            });
            LoadList(1);
        });
         //数据真实删除
        function DeleteAgency(AgencyId) {
             if (confirm('您确定要删除此批发商吗？\n\n此操作不可恢复！')) {
             $.ajax
                ({
                    url: "RouteAgencyList.aspx?DeletID=" + AgencyId+"&RouteAreaId=<%=AreaId %>&CityID=<%=cityID %>",
                    cache: false,
                    async:true,
                    success: function(html) {
                    location.href=location.href;
                        alert("删除成功");
                    }
                });
            }
            return false;
        }
        //选中的数据删除（假删除）
        function Del(obj)
        {
           var parentID=$(obj).parent().attr("ID")!=null?$(obj).parent().attr("ID").split('_')[1]:"";
           $(obj).parent().remove();
           if(parentID!="")
           {
                $("#"+parentID).attr("checked",false);
                currindex-=1;
            }
        }  
        
      function mouseovertr(o) {
	      o.style.backgroundColor="#FFF9E7";
          //o.style.cursor="hand";
      }
      function mouseouttr(o) {
	      o.style.backgroundColor="";
      }
    </script>

    <style type="text/css">
        <!
        -- body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-left: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
        -- > DIV.yahoo2
        {
            padding-right: 3px;
            padding-left: 3px;
            padding-bottom: 3px;
            margin: 0px;
            padding-top: 3px;
            text-align: right;
        }
        DIV.yahoo2 A
        {
            border: #ccdbe4 1px solid;
            padding-left: 4px;
            padding-right: 4px;
            background-position: 50% bottom;
            padding-bottom: 2px;
            margin-right: 3px;
            padding-top: 2px;
            text-decoration: none;
            text-align: center;
        }
        DIV.yahoo2 A:hover
        {
            border: #2b55af 1px solid;
            background-image: none;
            background-color: #4690B9;
            text-align: center;
        }
        DIV.yahoo2 A:active
        {
            border: #2b55af 1px solid;
            background-image: none;
            background-color: #4690B9;
            text-align: center;
        }
        DIV.yahoo2 SPAN
        {
            font-size: 12px;
        }
        DIV.yahoo2 SPAN.current
        {
            padding-right: 6px;
            padding-left: 6px;
            font-weight: bold;
            padding-bottom: 2px;
            color: #000;
            margin-right: 3px;
            padding-top: 2px;
            text-align: center;
        }
        DIV.yahoo2 SPAN.disabled
        {
            display: none;
        }
        DIV.yahoo2 A.next
        {
            border: #ccdbe4 2px solid;
            margin: 0px 0px 0px 10px;
        }
        DIV.yahoo2 A.next:hover
        {
            border: #2b55af 2px solid;
        }
        DIV.yahoo2 A.prev
        {
            border: #ccdbe4 2px solid;
            margin: 0px 10px 0px 0px;
        }
        DIV.yahoo2 A.prev:hover
        {
            border: #2b55af 2px solid;
        }
    </style>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <input id="AllPageSelectVal" type="hidden" name="AllPageSelectVal" />
    <table width="100%" border="0" align="center" cellpadding="3" cellspacing="0">
        <tr>
            <td width="5%" align="center" bgcolor="#D5EDFB">
                <img src="<%=ImageServerUrl%>/images/point16.gif" width="21" height="21">
            </td>
            <td width="95%" align="left" bgcolor="#D5EDFB">
                <asp:TextBox ID="txt_CompanyName" runat="server" Width="178px"></asp:TextBox>
                <input type="button" id="btn_Query" class="baocun_an" value="搜 索" style="height: 26px" />
            </td>
        </tr>
        <tr>
            <td colspan="2" bgcolor="#D5EDFB" height="3">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <div id="RAgencyList">
                </div>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <div id="DivPage" class="yahoo2">
                </div>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="3">
        <tr>
            <td align="left" bgcolor="#BEDCFA">
                <strong>已选择：</strong>
            </td>
        </tr>
    </table>
    <table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#D5EDFB">
        <tr>
            <td>
                <div id="divTag">
                </div>
            </td>
        </tr>
    </table>
    <asp:DataList ID="dalSelectList" runat="server" Width="100%" RepeatColumns="3" BorderStyle="None"
        GridLines="Horizontal">
        <ItemTemplate>
            <table border="0" cellspacing="0" cellpadding="3">
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td align="center">
                        <%# DataBinder.Eval(Container.DataItem, "CompanyName")%>
                    </td>
                    <td align="center">
                        <%# CreateOperation(Convert.ToString(DataBinder.Eval(Container.DataItem, "ID")))%>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="40" align="center">
                <table width="25%" height="30" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="baocun_an" OnClick="btnSave_Click">
                            </asp:Button>
                        </td>
                        <td align="center">
                        </td>
                        <td align="center">
                            <input id="btnBack" class="baocun_an" type="button" value="取 消" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide(function(){window.parent.location.reload();});" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
