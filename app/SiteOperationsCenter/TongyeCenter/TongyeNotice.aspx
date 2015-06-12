<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TongyeNotice.aspx.cs" Inherits="SiteOperationsCenter.TongyeCenter.TongyeNotice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript">
        //鼠标选中后的背景样式
         var TongyeNotice = {
            GetSelectValue: function() {
                var arraylist = new Array();
                $(":checkbox[name='chkId'][checked='true']").each(function() {
                    arraylist.push($(this).val());
                });
                return arraylist
            },
            AddNotice: function() {
                var params = { NoticeClass:<%=Request.QueryString["NoticeClass"] %> };
                window.location.href = "TongyeNoticeEdit.aspx?" + $.param(params);
                return false;
            },
            DelNotice: function(id) {
            if (confirm("确定要删除该记录!")) {
            if(id.constructor==Array){
            id=id.join(",");
            }
            var params = { ids: id,state: 'Del',TongyeClass:$("#ddlTongyeList").val(),NoticeClass:<%=Request.QueryString["NoticeClass"] %>};
                window.location.href = "TongyeNotice.aspx?" + $.param(params);
                return false;
                }
            },
            EditNotice: function(id) {
                var params = {  id: id,NoticeClass:<%=Request.QueryString["NoticeClass"] %>};
                window.location.href = "TongyeNoticeEdit.aspx?" + $.param(params);
                return false;
            },
            SearchNotice: function() {
                var TongyeClass = $("#ddlTongyeList").val();
                var params = { TongyeClass: TongyeClass,NoticeClass:<%=Request.QueryString["NoticeClass"] %> };
                window.location.href = "TongyeNotice.aspx?" + $.param(params);
                return false;
            },
           mouseovertr: function(o){
	            o.style.backgroundColor="#FFF9E7";
          },
           mouseouttr: function(o){
	            o.style.backgroundColor="";
          }
        }
          
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td height="25" width="23%" background="<%=ImageServerUrl%>/images/gongneng_bg.gif">
                    <table width="51%" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td width="4%" align="right">
                                    <img height="20" width="3" src="<%=ImageServerUrl%>/images/yunying/ge_da.gif" />
                                </td>
                                <td width="23%">
                                    <a href="javascript:" id="AddNotice">
                                        <img height="25" width="50" border="0" src="<%=ImageServerUrl%>/images/yunying/xinzeng.gif" /></a>
                                </td>
                                <td width="4%">
                                    <img height="25" width="2" src="<%=ImageServerUrl%>/images/yunying/ge_hang.gif">
                                </td>
                                <td width="24%">
                                    <a href="javascript:" id="EditNotice">
                                        <img height="25" width="50" border="0" src="<%=ImageServerUrl%>/images/yunying/xiugai.gif"></a>
                                </td>
                                <td width="5%">
                                    <img height="25" width="2" src="<%=ImageServerUrl%>/images/yunying/ge_hang.gif">
                                </td>
                                <td width="23%">
                                    <a href="javascript:" id="DelNotice">
                                        <img height="25" width="51" src="<%=ImageServerUrl%>/images/yunying/shanchu.gif" /></a>
                                </td>
                                <td width="17%">
                                    <asp:DropDownList ID="ddlTongyeList" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td align="left" width="77%" background="<%=ImageServerUrl%>/images/yunying/gongneng_bg.gif"
                    align="center">
                    &nbsp;
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存序号" />
                </td>
            </tr>
        </tbody>
    </table>
    <table width="98%" cellspacing="1" cellpadding="0" border="0" align="center" class="kuang"
        id="NoticeList">
        <tbody>
            <tr background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" class="white">
                <td height="23" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif"
                    align="center">
                    <input type="checkbox" value="<%#Eval("ID")%>" name="chkAll" /><strong>序号</strong>
                </td>
                <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                    <strong>标题</strong>
                </td>
                <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                    <strong>时间</strong>
                </td>
                <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                    <strong>发布人</strong>
                </td>
                <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                    <strong>操作</strong>
                </td>
            </tr>
            <cc1:CustomRepeater ID="repList" runat="server">
                <ItemTemplate>
                    <tr onmouseout="TongyeNotice.mouseouttr(this)" onmouseover="TongyeNotice.mouseovertr(this)" class="baidi">
                        <td height="25" align="center">
                            <input type="checkbox" value="<%#Eval("ID")%>" name="chkId">
                            <input type="text" size="2" maxlength="3" value="<%#Eval("Num")%>" name="sort">
                            <input id="hidid"  name ="hidid" type="hidden" value="<%#Eval("ID")%>"/>
                        </td>
                        <td align="center">
                            <%# Utils.GetText2(Eval("Title").ToString(),16,true)%>
                        </td>
                        <td align="center">
                            <%#Eval("OperateTime")%>
                        </td>
                        <td align="center">
                            <%#Eval("Operater")%>
                        </td>
                        <td align="center">
                            <a href="javascript:" onclick=" return TongyeNotice.EditNotice(<%#Eval("ID")%>)">修改</a>|<a
                                href="javascript:" onclick="return TongyeNotice.DelNotice(<%#Eval("Id")%>);"> 删除</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </cc1:CustomRepeater>
            <tr background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" class="white">
                <td height="23" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif"
                    align="center">
                    <input type="checkbox" value="<%#Eval("ID")%>" name="chkAll" /><strong>序号</strong>
                </td>
                <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                    <strong>标题</strong>
                </td>
                <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                    <strong>时间</strong>
                </td>
                <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                    <strong>发布人</strong>
                </td>
                <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                    <strong>操作</strong>
                </td>
            </tr>
        </tbody>
    </table>
    <table width="100%" cellspacing="0" cellpadding="0" border="0">
        <tbody>
            <tr>
                <td>
                </td>
                <td align="right">
                    <cc2:ExportPageInfo ID="ExportPageInfo1" runat="server" />
                </td>
                <td align="right">
                </td>
            </tr>
        </tbody>
    </table>
    </form>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript">

        $(document).ready(function() {
            $(":checkbox[name='chkAll']").click(function() {
                var isChecked = $(this).attr("checked");
                $(":checkbox[name='chkId']").each(function() {
                    $(this).attr("checked", isChecked)
                });
                $(":checkbox[name='chkAll']").each(function() {
                    $(this).attr("checked", isChecked)
                });
            });

            $("#AddNotice").click(function() {
                TongyeNotice.AddNotice();
                return false;
            });

            $("#EditNotice").click(function() {
                var arraylist = TongyeNotice.GetSelectValue();
                if (arraylist.length > 1) {
                    alert("只能选择一条记录操作!");
                    return false;
                } else if (arraylist.length == 0) {
                    alert('请选择要修改的记录!');
                    return false;
                }
                else {
                    TongyeNotice.EditNotice(arraylist[0]);
                }
                return false;
            });
            $("#btnSave").click(function() {
                var arraylist = TongyeNotice.GetSelectValue();
                if (arraylist.length == 0) {
                    alert('请选择要保存序号的记录!');
                    return false;
                }
            });
            $("#DelNotice").click(function() {
                var arraylist = TongyeNotice.GetSelectValue();
                if (arraylist.length == 0) {
                    alert('请选择要删除的记录!');
                    return false;
                } else {
                    TongyeNotice.DelNotice(arraylist);
                }
                return false;
            });
            $("#ddlTongyeList").change(function() {
                TongyeNotice.SearchNotice();
            });
        });     
    </script>

</body>
</html>
