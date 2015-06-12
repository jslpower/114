<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FeedBack.aspx.cs" Inherits="SiteOperationsCenter.FeedbackManage.FeedBack" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>无标题文档</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" name="form1" method="post" action="" runat="server"  defaultbutton="ImgBtn">
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" bgcolor="#E2ECFE">
                <span style=" float:left; margin-left:1px;">
                    <asp:ImageButton ID="ImgBtnDel" runat="server" OnClientClick="return CheckIsOne()"
                        OnClick="ImgBtnDel_Click" ImageAlign="AbsMiddle" />
                </span>
                <asp:DropDownList ID="ddlType" runat="server" Visible="false">
                <asp:ListItem Value="1" Text="个人中心" Selected="True"></asp:ListItem>
                <asp:ListItem Value="2" Text="个人中心报价标准" ></asp:ListItem>
                </asp:DropDownList>
                &nbsp;
                单位名称
                <input id="cName" type="text" class="textfield" size="10" runat="server" />
                负责人
                <input type="text" id="uName" class="textfield" size="10" runat="server" />
                内容
                <input type="text" class="textfield" size="10" id="searchVal" runat="server" />
                <asp:ImageButton ID="ImgBtn" runat="server" Width="60px" Height="21px" OnClick="ImgBtn_Click"
                    ImageAlign="AbsMiddle" />
            </td>
        </tr>
    </table>
    <div id="divList" style="width: 98%" runat="server">
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
            <tr background="<%=ImageServerUrl %>/images/yunying/yunying/hangbg.gif" class="white"
                height="23">
                <td width="8%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <input type="checkbox" id="cbxAll" class="checkbox" /><strong>序号</strong>
                </td>
                <td width="18%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>单位名称<br />
                    </strong>
                </td>
                <td width="12%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>负责人<br />
                    </strong>
                </td>
                <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>电话<br />
                    </strong>
                </td>
                <td width="9%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>手机<br />
                    </strong>
                </td>
                <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>MQ/QQ<br />
                    </strong>
                </td>
                <td width="25%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>意见内容</strong>
                </td>
                 <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>反馈时间</strong>
                </td>
            </tr>
            <asp:Repeater ID="rptList" runat="server">
                <ItemTemplate>
                    <%# (Container.ItemIndex+1)%2==1? "<tr class='baidi' onmouseover=\"mouseovertr(this)\" onmouseout=\"mouseouttr(this)\">" : "<tr bgcolor='#f3f7ff' onmouseover=\"mouseovertr(this)\" onmouseout=\"mouseouttr(this)\">"%>
                    <td height="25" align="center">
                        <asp:CheckBox ID="Delbox" runat="server" CssClass='<%#Eval("SuggestionId")%>' />
                        <strong>
                            <%#((pageIndex - 1) * pageSize) + Container.ItemIndex + 1%>
                        </strong>
                    </td>
                    <td height="25" align="center">
                        <a>
                            <%#Eval("CompanyName")%></a><br />
                    </td>
                    <td align="center">
                        <%#Eval("ContactName")%>
                    </td>
                    <td width="8%" align="center">
                        <%#Eval("ContactTel")%>
                    </td>
                    <td align="center">
                        <a href="javascript:void(0)" onmouseover="wsug(event, '联系人:<%#Eval("ContactName")%></br>')"
                            onmouseout="wsug(event, 0)">
                            <%#Eval("ContactMobile")%><br />
                        </a>
                    </td>
                    <td align="center">
                        <%#Eval("MQ")%>
                        /<%#Eval("QQ")%>
                    </td>
                    <td align="center">
                        <%#Eval("ContentText")%>
                    </td>
                    <td align="center">
                        <%#Eval("IssueTime")%>
                    </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
               
            <tr >
                <td  align="center" valign="middle" 
                     colspan="7">
                      <asp:Label ID="lblFristMsg" runat="server" Text="没有相关数据" ></asp:Label></td>
            </tr>

            <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                <td width="8%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <input type="checkbox" id="Checkbox1" class="checkbox" /><strong>序号</strong>
                </td>
                <td width="18%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>单位名称<br />
                    </strong>
                </td>
                <td width="12%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>负责人<br />
                    </strong>
                </td>
                <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>电话<br />
                    </strong>
                </td>
                <td width="9%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>手机<br />
                    </strong>
                </td>
                <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>MQ/QQ<br />
                    </strong>
                </td>
                <td width="25%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>意见内容</strong>
                </td>
                  <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>反馈时间</strong>
                </td>
            </tr>

        </table>
    </div>
    <div id="divListSecond" runat="server">
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
            <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                <td width="8%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <input type="checkbox" id="Checkbox2" class="checkboxSecond" />
                    <strong>序号 </strong>
                </td>
                <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>性别</strong>
                </td>
                <td width="16%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>单位名称<br />
                    </strong>
                </td>
                <td width="8%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>姓名</strong>
                </td>
                <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>手机<br />
                    </strong>
                </td>
                <td width="7%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>职务<br />
                    </strong>
                </td>
                <td width="9%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>照片</strong>
                </td>
                <td width="30%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>主要成就</strong>
                </td>
                <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>反馈时间</strong>
                </td>
            </tr>
           <tr >
                <td align="center" valign="middle" 
                     colspan="8">
                      <asp:Label ID="lblMsgSecond" runat="server" Text="没有相关数据" ></asp:Label></td>
            </tr>
            <asp:Repeater ID="rptListScond" runat="server">
                <ItemTemplate>
                    <%# (Container.ItemIndex + 1) % 2 == 1 ? "<tr class='baidi' onmouseover=\"mouseovertr(this)\" onmouseout=\"mouseouttr(this)\">" : "<tr bgcolor='#f3f7ff' onmouseover=\"mouseovertr(this)\" onmouseout=\"mouseouttr(this)\">"%>
                    <td height="23" align="center">
                        <asp:CheckBox ID="deleteBox"  runat="server" CssClass='<%#Eval("id")%>'/>
                        <strong>
                            <%# ((pageIndex-1) *20) + Container.ItemIndex+1%></strong>
                    </td>
                    <td align="center">
                        <%#Eval("sex").ToString().Trim() == "True" ? "男":"女" %>
                    </td>
                    <td height="23" align="center">
                        <%#Eval("CompanyName")%><br />
                    </td>
                    <td align="left">
                        <%#Eval("CompanyName")%>
                    </td>
                    <td align="center">
                        <%#Eval("ContactTel")%>
                    </td>
                    <td align="center">
                        <%#Eval("job")%>
                    </td>
                    <td align="center">
                        <a href="<%#Domain.FileSystem + Eval("ImgPath")%>" title="点击查看大图" target="_blank"><img src="<%#Domain.FileSystem + Eval("ImgPath")%>" width="100px" height="50px" /></a>
                    </td>
                    <td align="center">
                        <%#Eval("Achieve")%>
                    </td>
                     <td align="center">
                        <%#Eval("IssueTime")%>
                    </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr background="images/hangbg.gif" class="white" height="23">
                <td width="8%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <input type="checkbox" id="Checkbox3" class="checkboxSecond" />
                    <strong>序号 </strong>
                </td>
                <td width="5%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>性别</strong>
                </td>
                <td width="16%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>单位名称<br />
                    </strong>
                </td>
                <td width="8%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>姓名</strong>
                </td>
                <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>手机<br />
                    </strong>
                </td>
                <td width="7%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>职务<br />
                    </strong>
                </td>
                <td width="9%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>照片</strong>
                </td>
                <td width="30%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>主要成就</strong>
                </td>
                 <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                    <strong>反馈时间</strong>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hiddenType" runat="server" />
    <table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="right">
                <cc1:ExportPageInfo ID="ExportPageInfo1" runat="server" />
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

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script language="JavaScript" type="text/javascript">

        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF9E7";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = "";
        }

        var list = {
            type: 0,
            GetList: function() {
                var ckList = new Array();

                $("#divList").find("input[type='checkbox'][name!='ckAll']:checked").each(function() {
                    if ($(this).attr("class") != "checkbox") {
                        ckList.push($(this).val());
                    }
                });

                $("#divListSecond").find("input[type='checkbox'][name!='ckAll']:checked").each(function() {
                    if ($(this).attr("class") != "checkboxSecond") {
                        ckList.push($(this).val());
                    }
                });

                return ckList;
            }

        }

        function CheckIsOne() {
            if (list.GetList().length <= 0) {
                alert("请选择至少一行数据!");
                return false;
            }
            if (confirm("确定删除吗？该操作不可恢复！")) {
                return true;
            }
            return false;
        }

        $(function() {
            $(".checkbox").click(function() {
                if ($(this).attr("checked") == true) {
                    $("#divList input[type=checkbox]").attr("checked", true);
                } else {
                    $("#divList input[type=checkbox]").attr("checked", false);
                }
            })

            $(".checkboxSecond").click(function() {
                if ($(this).attr("checked") == true) {
                    $("#divListSecond input[type=checkbox]").attr("checked", true);
                } else {
                    $("#divListSecond input[type=checkbox]").attr("checked", false);
                }
            })

        });
        
    </script>

    <script>

        //鼠标跟随代码//
        function wsug(e, str) {
            var oThis = arguments.callee;
            if (!str) {
                oThis.sug.style.visibility = 'hidden';
                document.onmousemove = null;
                return;
            }
            if (!oThis.sug) {
                var div = document.createElement('div'), css = 'top:0; left:-30px;text-align:left;color:#2C709F;position:absolute; z-index:100; visibility:hidden';
                div.style.cssText = css;
                div.setAttribute('style', css);
                var sug = document.createElement('div'), css = 'font:normal 12px/16px "宋体"; white-space:nowrap; color:#666; padding:3px; position:absolute; left:-30px; top:0; z-index:10; background:#f9fdfd; border:1px solid #629BC7;text-align:left;color:#2C709F;';
                sug.style.cssText = css;
                sug.setAttribute('style', css);
                var dr = document.createElement('div'), css = 'position:absolute; top:3px; left:-27px; background:#333; filter:alpha(opacity=30); opacity:0.3; z-index:9';
                dr.style.cssText = css;
                dr.setAttribute('style', css);
                var ifr = document.createElement('iframe'), css = 'position:absolute; left:0; top:-10; z-index:8; filter:alpha(opacity=0); opacity:0';
                ifr.style.cssText = css;
                ifr.setAttribute('style', css);
                div.appendChild(ifr);
                div.appendChild(dr);
                div.appendChild(sug);
                div.sug = sug;
                document.body.appendChild(div);
                oThis.sug = div;
                oThis.dr = dr;
                oThis.ifr = ifr;
                div = dr = ifr = sug = null;
            }
            var e = e || window.event, obj = oThis.sug, dr = oThis.dr, ifr = oThis.ifr;
            obj.sug.innerHTML = str;

            var w = obj.sug.offsetWidth, h = obj.sug.offsetHeight, dw = document.documentElement.clientWidth || document.body.clientWidth; dh = document.documentElement.clientHeight || document.body.clientHeight;
            var st = document.documentElement.scrollTop || document.body.scrollTop, sl = document.documentElement.scrollLeft || document.body.scrollLeft;
            var left = e.clientX + sl + 17 + w < dw + sl && e.clientX + sl + 15 || e.clientX + sl - 8 - w, top = e.clientY + st + 17;
            obj.style.left = left + 10 + 'px';
            obj.style.top = top + 10 + 'px';
            dr.style.width = w + 'px';
            dr.style.height = h + 'px';
            ifr.style.width = w + 3 + 'px';
            ifr.style.height = h + 3 + 'px';
            obj.style.visibility = 'visible';
            document.onmousemove = function(e) {
                var e = e || window.event, st = document.documentElement.scrollTop || document.body.scrollTop, sl = document.documentElement.scrollLeft || document.body.scrollLeft;
                var left = e.clientX + sl + 17 + w < dw + sl && e.clientX + sl + 15 || e.clientX + sl - 8 - w, top = e.clientY + st + 17 + h < dh + st && e.clientY + st + 17 || e.clientY + st - 5 - h;
                obj.style.left = left + 'px';
                obj.style.top = top + 'px';
            }
        }
    </script>

    </form>
</body>
</html>
