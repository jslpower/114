<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateLineArea.aspx.cs"
    Inherits="SiteOperationsCenter.PlatformManagement.UpdateLineArea" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>通用专线区域维护-修改</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("ext-all") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("GetCityList") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript">
    
    //选择国家或者城市
    function SelectDomesticOrWord(v)
    {
        if(v=="0")//国内
        {   
            BindProvinceList('<%=dropCountryOrCity.ClientID %>');
            $("#<%=dropCountryOrCity.ClientID %> option").each(function(){
                if($(this).val()=="0")
                {
                    $(this).remove();                    
                }
            })
        }
        else if(v=="1")//国际
        {
            $("#<%=dropCountryOrCity.ClientID %> option").remove();
        }
    }
    
    $(function(){
        SelectDomesticOrWord("0");
    })
    
    //移动选择项专线主要区域
    function movetToRight()
    {
        var bool1=false;//判断是不是已经选择了一个
        var selcedvalue=$("#<%=dropCountryOrCity.ClientID %> option:selected").val();
        var selcedtext=$("#<%=dropCountryOrCity.ClientID %> option:selected").text();
        if(selcedvalue!=undefined)
        {
            if($("#<%=dropSecled.ClientID %>")[0].options.length>0)
            {                
                $("#<%=dropSecled.ClientID %> option").each(function(){
                    if($(this).val()==selcedvalue)
                    {
                        bool1=true;
                    }
                })
                if(!bool1)
                {
                    $("<option value='"+selcedvalue+"'>"+selcedtext+"</option>").appendTo ($("#<%=dropSecled.ClientID %>"));
                }
            }
            else
            {
                $("<option value='"+selcedvalue+"'>"+selcedtext+"</option>").appendTo ($("#<%=dropSecled.ClientID %>"));
            }
        }
        else
        {
            alert("请选择一项");
            return false;
        }
    }
    
    //删除专线主要区域
    function DropOptionRemove()
    {
        var selectd=$("#<%=dropSecled.ClientID %>").val();
        $("#<%=dropSecled.ClientID %> option").each(function(){
            if(selectd!=undefined)
            {
                if($(this).val()==selectd)
                {
                    $(this).remove();
                }
            }
            else
            {
                alert("请选择一项数据");
                return false;
            }
        })
    }
    
    </script>

    <script type="text/javascript">
    
    function btnSave()
    {
        $.ajax({
            url: "UpdateLineArea.aspx?type=Save",
             cache: false,
             type: "post",
             success: function(result) {
                    if(result=="success")
                    {
                        alert("修改成功");
                    }
                    else
                        alert("修改失败");
             },
             error: function() {
                 alert("操作失败!");
             }   
        
        })
    }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="99%" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#cccccc"
        class="lr_hangbg table_basic">
        <tr>
            <td width="17%" align="right" bgcolor="#f2f9fe">
                专线名：
            </td>
            <td width="83%" align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_LineName" id="txt_LineName" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                专线主要区域：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:DropDownList ID="dropDomesticOrWord" Size="5" onchange="SelectDomesticOrWord(this.value)"
                    runat="server">
                    <asp:ListItem Value="0">国内</asp:ListItem>
                    <asp:ListItem Value="1">国际</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="dropCountryOrCity" Size="5" runat="server">
                </asp:DropDownList>
                <input type="button" name="btnmove" id="btnmove" value="=&gt;" onclick="movetToRight()" />
                <asp:DropDownList ID="dropSecled" Size="5" runat="server">
                </asp:DropDownList>
                (选择项)
                <input type="button" name="btndelete" id="btndelete" value="删除" onclick="DropOptionRemove()" />
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                分类：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:RadioButtonList ID="RadClassic" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">国内</asp:ListItem>
                    <asp:ListItem Value="1">国际</asp:ListItem>
                    <asp:ListItem Value="2">周边</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                所属分网站：<br />
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input id="txt_Subsite" runat="server" name="txt_Subsite" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" bgcolor="#f2f9fe">
                <input type="button" name="btnsave" id="btnsave" value="保存修改" onclick="btnSave()" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
