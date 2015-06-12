<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TouristsList.aspx.cs" Inherits="UserBackCenter.TeamService.TouristsList" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%=EyouSoft.Common.CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("jquery") %>"></script>
    <style type="text/css">
    a.baocun_btn,a.tianjia_btn,a.basic_btn,a.zhuangtai_btn{ display:inline-block; text-align:center; text-decoration:none;}
    a.baocun_btn{ background:url(<%=ImageServerUrl%>/images/baocun_btn.jpg) no-repeat; width:79px; height:25px; line-height:25px;color:#000; font-size:16px; font-weight:bold;}
    </style>
    <script type="text/javascript">
        function mouseovertr(obj) {
            obj.style.backgroundColor = "#D8E5FF";
        }
        function mouseouttr(obj) {
            obj.style.backgroundColor = "";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="96%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="50" align="left">
                    <img src="<%=ImageServerUrl %>/images/point16.gif" width="21" height="21" />关键字：
                    <label>
                        <input  id="SearchKey"  runat="server" type="text" size="15" />
                    </label>
                    <input type="submit" name="Submit" value=" 搜索" />
                </td>
            </tr>
        </table>
        <table width="96%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#AED7EE"
            class="padd5" id="tblCustomerList">
            <tr bgcolor="#ffffff">
                <td height="26" align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />张三</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />李四</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        王五</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox2" value="checkbox" />
                        张丽</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox12" value="checkbox" />
                        李潇</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox13" value="checkbox" />
                        祝辉</span>18757595859
                </td>
            </tr>
            <tr bgcolor="#ffffff">
                <td height="26" align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox29" value="checkbox" />
                        张三 </span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        李四</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        王五</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox3" value="checkbox" />
                        张丽</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox14" value="checkbox" />
                        李潇</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox21" value="checkbox" />
                        祝辉</span>18757595859
                </td>
            </tr>
            <tr bgcolor="#ffffff">
                <td height="26" align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox30" value="checkbox" />
                        张三 </span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        李四</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        王五</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox4" value="checkbox" />
                        张丽</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox15" value="checkbox" />
                        李潇</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox22" value="checkbox" />
                        祝辉</span>18757595859
                </td>
            </tr>
            <tr bgcolor="#ffffff">
                <td height="26" align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox31" value="checkbox" />
                        张三 </span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        李四</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        王五</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox5" value="checkbox" />
                        张丽</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox16" value="checkbox" />
                        李潇</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox23" value="checkbox" />
                        祝辉</span>18757595859
                </td>
            </tr>
            <tr bgcolor="#ffffff">
                <td height="26" align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox32" value="checkbox" />
                        张三 </span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        李四</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        王五</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox6" value="checkbox" />
                        张丽</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox17" value="checkbox" />
                        李潇</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox24" value="checkbox" />
                        祝辉</span>18757595859
                </td>
            </tr>
            <tr bgcolor="#ffffff">
                <td height="26" align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox33" value="checkbox" />
                        张三 </span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        李四</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        王五</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox7" value="checkbox" />
                        张丽</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox18" value="checkbox" />
                        李潇</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox25" value="checkbox" />
                        祝辉</span>18757595859
                </td>
            </tr>
            <tr bgcolor="#ffffff">
                <td height="26" align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox34" value="checkbox" />
                        张三 </span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        李四</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        王五</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox8" value="checkbox" />
                        张丽</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox19" value="checkbox" />
                        李潇</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox26" value="checkbox" />
                        祝辉</span>18757595859
                </td>
            </tr>
            <tr bgcolor="#ffffff">
                <td height="26" align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox35" value="checkbox" />
                        张三 </span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        李四</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        王五</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox9" value="checkbox" />
                        张丽</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox20" value="checkbox" />
                        李潇</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox27" value="checkbox" />
                        祝辉</span>18757595859
                </td>
            </tr>
            <tr bgcolor="#ffffff">
                <td height="26" align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox36" value="checkbox" />
                        张三 </span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        李四</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox" value="checkbox" />
                        王五</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox10" value="checkbox" />
                        张丽</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox11" value="checkbox" />
                        李潇</span>18757595859
                </td>
                <td align="left" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <span class="ce">
                        <input type="checkbox" name="checkbox28" value="checkbox" />
                        祝辉</span>18757595859
                </td>
            </tr>
        </table>
        <table id="Table1" cellspacing="0" cellpadding="4" width="96%" align="center" border="0">
            <tr>
                <td class="F2Back" align="right">
                    <cc1:ExportPageInfo  ID="ExportPageInfo1"  runat="server"  CurrencyPageCssClass="RedFnt" LinkType="6"/>
                </td>
            </tr>
       </table>
       <table id="Table2" cellspacing="0" cellpadding="4" width="96%" align="center" border="0">
            <tr>
                <td class="F2Back" align="center">
                   <asp:LinkButton class="baocun_btn" id="btnSave" runat="server" >保 存</asp:LinkButton>
                   <a  class="baocun_btn" style="cursor:pointer" onclick="parent.Boxy.getIframeDialog('<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>').hide();">关 闭</a>
                </td>
            </tr>
       </table>       
    </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function() {
            $("input[name='Submit']").click(function() {
                var SearchKey = $("#<%=SearchKey.ClientID %>").val();
                window.location.href = "/TeamService/TouristsList.aspx?SearchKey=" + SearchKey;
                return false;
            });
            $("#<%=btnSave.ClientID %>").click(function() {
                var Count = '<%=EyouSoft.Common.Utils.GetQueryStringValue("Count") %>';
                var CheckLength = parseInt($("#tblCustomerList tr").find("input[type='checkbox']:checked").length);
                if (parseInt(Count) == 0) {
                    alert("请输入成人数和儿童数！");
                    parent.Boxy.getIframeDialog('<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>').hide();
                }
                if (CheckLength > parseInt(Count)) {
                    alert("对不起,你只能选择" + Count + "名游客！");
                    return false;
                } else {
                    //选择游客
                }
                return false;
            });

        });
    </script>
</body>
</html>
