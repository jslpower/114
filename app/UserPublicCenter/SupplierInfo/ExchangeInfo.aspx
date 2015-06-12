<%@ Page Title="" Language="C#" MasterPageFile="~/SupplierInfo/SupplierNew.Master"
    AutoEventWireup="true" CodeBehind="ExchangeInfo.aspx.cs" Inherits="UserPublicCenter.SupplierInfo.ExchangeNewInfo" %>
<%@ Register Src="~/WebControl/GongQiuPublish.ascx" tagname="gongqiu" tagprefix="ucgq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SupplierHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Show" runat="server">
<ucgq:gongqiu ID="Gongqiu1" runat="server"></ucgq:gongqiu>
    <!--列表 end-->
    <div class="hr-10">
    </div>
    <a target="_blank" href="http://im.tongye114.com" title="同业MQ"><img src="<%=ImageServerUrl + "/images/news/news-list_19.gif"%>" alt="同业MQ" /></a>
    <div class="hr-10">
    </div>
    <!--列表 start-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SupplierBody" runat="server">
    <form id="form5" runat="server">
    <div style="width: 68.59%" id="supply-right" class="addVg">
        <!--供求详细信息 开始-->
        <div class="news-read">
            <div class="title">
                <h2><%=emodel.ExchangeTitle %></h2>
                <p>
                    <span><%=emodel.IssueTime.ToString("yyyy年MM月dd日 HH:mm") %></span>&nbsp;&nbsp; <span><%=emodel.CompanyName %>
                    </span> <span><% =EyouSoft.Common.Utils.GetMQ(emodel.OperatorMQ) %></span>&nbsp;&nbsp; 
                    <span class="b">已有<em><%=emodel.WriteBackCount %></em>评论</span><span class="c">/<em><%=emodel.ViewCount %></em>预览 &nbsp;
                    <a href="javascript:void(0);" onclick="AddFavor('<%= id %>');return false;"><img src="<%= ImageServerPath %>/images/UserPublicCenter/20090716hf.gif" width="69" height="22" /></a>
                    </span>
                </p>
            </div>
            <div class="content" id="jcontent">
                <%= UserPublicCenter.SupplierInfo.SupplierCom.ConvertExchangeListContent(emodel.ExchangeText)%>
                <p><asp:Literal runat="server" ID="ltrDownLoad"></asp:Literal></p>
            </div>
        </div>
        <!--供求详细信息结束-->
        <!--列表 start-->
        <div class="box" id="divComment">
        </div>
        <div class="hr-10">
        </div>
        <!--分页 开始-->
        <div class="digg" id="DivPage">
            
        </div>
        <!--分页 结束-->
        <!--列表 end-->
        <div class="hr-10">
        </div>
        <!--列表 start-->
        <div class="box">
            <div class="box-l">
                <div class="box-r">
                    <div class="box-c">
                        <h3 class="add">
                            发表评论</h3>
                    </div>
                </div>
            </div>
            <div class="box-main">
                <div class="box-content">
                    <div class="reviewT">
                        <span class="l">用户名： 理智评论文明上网，拒绝恶意谩骂</span> <span class="R">[评论内容不能超过500个字符]</span>
                    </div>
                    <div class="reviewC">
                        <input type="hidden" id="hidCommentId" name="hidCommentId" value="" />
                        <textarea runat="server" id="txtCommentInfo" name="txtCommentInfo" style="width:640px; height:140px;" cols="10" rows="10" ></textarea>
                        <%--<input value="我要发布" type="button">--%>
                        <asp:Button runat="server" ID="ibtnSave"  OnClick="ibtnSave_Click" OnClientClick="return ClientSaveComment();"/>
                        <input id="isLogin" type="hidden" value="<%=GetIsLogin() %>" />
                    </div>
                </div>
            </div>
        </div>
        <!--列表 end-->
        <div class="hr-10">
        </div>
        <!--列表 start-->
        <div class="box">
            <div class="box-l">
                <div class="box-r">
                    <div class="box-c">
                        <h3 class="add">
                            最新供需
                        </h3>
                    </div>
                </div>
            </div>
            <div class="box-main">
                <div class="box-content">
                    <table class="reList reList1" border="0" width="643">
                        <tbody>
                            <%if (lastlist != null && lastlist.Count > 0)
                              {
                                  foreach (EyouSoft.Model.CommunityStructure.ExchangeList el in lastlist)
                                  { %>
                            <tr>
                                <%if (el.ExchangeTag == EyouSoft.Model.CommunityStructure.ExchangeTag.无)
                                  { %>
                                <td style="background: #fff" width="0"></td>
                                <%}
                                  else
                                  { %>
                                <td style="background: #fff" width="57">
                                    <div><% =UserPublicCenter.SupplierInfo.SupplierCom.GetTagUrl(el.ExchangeTag, ImageServerUrl, CityId, CityModel.ProvinceId)%></div>
                                </td>
                                <%} %>
                                <td class="regin" width="94">
                                    <a target="_blank" href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,0,el.ProvinceId,0,CityId) %>">【<%= GetProvNameById(el.ProvinceId) %>】</a>
                                </td>
                                <td class="title" width="400" align="left">
                                    <a target="_blank" href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(el.ID,CityId) %>"><strong><%=el.ExchangeTitle.Length>20?el.ExchangeTitle.Substring(0,20)+"...":el.ExchangeTitle %></strong></a>
                                </td>
                                <td class="pubTime" width="35">
                                    <%=el.IssueTime.ToString("MM.dd") %>
                                </td>
                                <td class="supplier" width="128">
                                    <a  href="javascript:;"><%=el.CompanyName %></a>
                                </td>
                                <td width="46">
                                    <%=EyouSoft.Common.Utils.GetMQ(el.OperatorMQ) %>
                                </td>
                            </tr>
                            <%}
                              } %>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <!--列表 end-->
        <div class="hr-10">
        </div>
        <!--列表 start-->
        <div class="box">
            <div class="box-l">
                <div class="box-r">
                    <div class="box-c">
                        <h3 class="add">
                            同类其他供需
                        </h3>
                    </div>
                </div>
            </div>
            <div class="box-main">
                <div class="box-content">
                    <table class="reList reList2" border="0" width="643">
                        <tbody>
                            <%if (otherlist != null && otherlist.Count > 0)
                              {
                                  foreach (EyouSoft.Model.CommunityStructure.ExchangeList el in otherlist)
                                  { %>
                            <tr>
                                <%if (el.ExchangeTag == EyouSoft.Model.CommunityStructure.ExchangeTag.无)
                                  { %>
                                  <td   style="background: #fff" width="0"></td>
                                <%}
                                  else
                                  { %>
                                <td style="background: #fff" width="57">
                                    <div><% =UserPublicCenter.SupplierInfo.SupplierCom.GetTagUrl(el.ExchangeTag, ImageServerUrl, CityId, CityModel.ProvinceId)%></div>
                                </td>
                                <%} %>
                                <td class="regin" width="94">
                                    <a target="_blank" href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.GetSuplierUrl("",0,0,0,el.ProvinceId,0,CityId) %>">【<%= GetProvNameById(el.ProvinceId) %>】</a>
                                </td>
                                <td class="title" width="400" align="left">
                                    <a target="_blank" href="<%= EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(el.ID,CityId) %>" ><strong><%=el.ExchangeTitle.Length>20?el.ExchangeTitle.Substring(0,20)+"...":el.ExchangeTitle %></strong></a>
                                </td>
                                <td class="pubTime" width="35">
                                    <%=el.IssueTime.ToString("MM.dd") %>
                                </td>
                                <td class="supplier" width="128">
                                    <a  href="javascript:;"><%=el.CompanyName %></a>
                                </td>
                                <td width="46">
                                    <%=EyouSoft.Common.Utils.GetMQ(el.OperatorMQ) %>
                                </td>
                            </tr>
                            <%}
                              } %>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <!--列表 end-->
    </div>
    
        <%--盖楼回复开始--%>
    <table id="CommentInfo" style="display: none;" width="100%" border="0" cellspacing="0"
        cellpadding="0">
        <tr>
            <td>
                <textarea runat="server" id="txtCommentInfo1" name="txtCommentInfo1" style="height: 150px;
                    width: 630px; border: 1px solid #666;"></textarea>
            </td>
        </tr>
        <tr>
            <td height="40" align="left" style="padding-left: 50px;">
                <img alt="提交" id="save" src="<%= EyouSoft.Common.Domain.ServerComponents %>/images/UserPublicCenter/20090716tijiao.gif"
                    width="69" height="22" onclick="ClientSaveComment1()" />
                <span class="huise">[评论内容不能超过500个字符]</span>
            </td>
        </tr>
    </table>
    <%--盖楼回复结束--%>
</form>
    <textarea rows="1" cols="1" style="position:absolute;left:-9999px;top:-9999px;z-index:-999" id="hack">hacked</textarea>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SupplierBottom" runat="server">

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("ajaxpagecontrols") %>"></script>

<script type="text/javascript">


        function show() {
            Boxy.iframeDialog({ title: "马上登录同业114", iframeUrl: "<%=GetReturnUrl() %>", width: "400px", height: "250px", modal: true });
        }

        function AddFavor(infoid) {
            var isLogin = $("#isLogin").val();
            if (isLogin == "True") {
                if (infoid != "") {
                    $.ajax({
                        type: "GET",
                        cache: false,
                        url: "/SupplierInfo/Ashx/FavorExchange.ashx",
                        data: "Id=" + infoid,
                        success: function(msg) {
                            var strErr = "";
                            if (msg == "1")
                                strErr = "已设为关注！";
                            else if (msg == "2")
                                strErr = "操作失败！";
                            else if (msg == "0")
                                strErr = "请先登录！";
                            alert(strErr);
                        }
                    });
                }
            } else {
                show();
            }
        }

        function LoadCommentList(infoid, pageIndex) {
            var cityid = "<%= CityId %>";
            $("#divComment").html("<img id=\"img_loading\" src='\<%= ImageServerPath %>/images/loadingnew.gif\' border=\"0\" /><br />&nbsp;正在加载...&nbsp;");
            $.ajax({
                type: "GET",
                cache: false,
                async: false,
                url: "/SupplierInfo/Ashx/AjaxCommentPage.ashx",
                data: "cityid=" + cityid + "&TopicType=1&TopicId=" + encodeURI(infoid) + "&pageIndex=" + pageIndex,
                success: function(CommentHTML) {
                    $("#divComment").html(CommentHTML);
                }
            });
            var config = {
                pageSize: parseInt($("#hSize").val()),
                pageIndex: parseInt($("#hIndex").val()),
                recordCount: parseInt($("#hRecordCount").val()),
                pageCount: 0,
                gotoPageFunctionName: 'AjaxPageControls.gotoPage',
                showPrev: true,
                showNext: true
            };
            AjaxPageControls.replace("DivPage", config);
            AjaxPageControls.gotoPage = function(pIndex) {
                LoadCommentList(infoid, pIndex);
            }
        }
        $(document).ready(function() {
            showDialog();
            function showDialog() {
                var boolIsLogin = $("#isLogin").val();
                if (boolIsLogin == "False") {
                    show();
                }
            }
            LoadCommentList("<%= id %>", "1");
        });

        function ClientSaveComment1() {
            var strInfo = $("#<%= txtCommentInfo1.ClientID %>").val();
            var strErr = "";
            if (strInfo == "" || strInfo.length > 500)
                strErr += "回复内容不能为空且应在500字符以内！\n";
            if (strErr != "") {
                alert(strErr);
                return false;
            }
            else {
                $("#<%= txtCommentInfo.ClientID %>").val(strInfo);
                $("#save").hide();
                $("#<%= ibtnSave.ClientID %>").click();
                return false;
            }
        }

        function ClientSaveComment() {
            var strInfo = $("#<%= txtCommentInfo.ClientID %>").val();
            var strErr = "";
            var boolIsLogin = $("#isLogin").val();
            if (boolIsLogin == "False") {
                show();
            } else {
                if (strInfo == "" || strInfo.length > 500)
                    strErr += "回复内容不能为空且应在500字符以内！\n";
                if (strErr != "") {
                    alert(strErr);
                    return false;
                }
                else {
                    $("#<%= ibtnSave.ClientID %>").hide();
                    return true;
                }
            }
            return false;
        }
    
    
        function gotoComment(CommentId, num) {
            var IsLogin = $("#isLogin").val();
            if (IsLogin == "True") {
                if (CommentId == "")
                    return false;
                $("#hidCommentId").val(CommentId);
                var boxy = new Boxy($("#CommentInfo"), { title: "回复" + num + "楼：", width: "800", height: "180", draggable: false, modal: true,
                    afterHide: function() { $("#hidCommentId").val(""); $("#<%= txtCommentInfo1.ClientID %>").val(""); }
                });
            } else {
                show();
            }
            return false;
        }

        var hack = document.getElementById('hack');

        function clear(node) {
            var style = node.style;
            if ((style.position != "" && style.position != "static")
                ||
                style.display == "none"
                || style.visibility == "hidden"
                ) {
                node.parentNode.removeChild(node);
                return true;
            }
            return false;
        }
        function makeArray(o) {
            if (o.item && document.all) {
                var ret = [], i = 0, len = o.length;
                for (; i < len; ++i) {
                    ret[i] = o[i];
                }
                return ret;
            }
            return [].slice.call(o);
        }

        function clearAll(node) {
            if (!clear(node)) {
                var c = makeArray(node.childNodes);
                for (var i = 0; i < c.length; i++) {
                    var n = c[i]
                    n.nodeType == 1 && clearAll(n);
                }
            }
        }

        function getWrap(html) {
            var div = document.createElement("div");
            div.innerHTML = html;
            return div;
        }
        function wrap(frag) {
            var div = document.createElement("div");
            div.appendChild(frag);
            return div;
        }

        document.getElementById("jcontent").oncopy = function(e) {
            var h, w;
            if (document.documentElement && document.documentElement.scrollTop) {
                h = document.documentElement.scrollTop;
                w = document.documentElement.scrollLeft;
            } else if (document.body) {
                h = document.body.scrollTop;
                w = document.body.scrollLeft;
            }

            var evt = e || window.event,
                        userSelection, selectedDom, savedRange;
            if (window.getSelection) {
                userSelection = window.getSelection();
                savedRange = userSelection.getRangeAt(0);

            } else if (document.selection) { // should come last; Opera!
                userSelection = document.selection.createRange();
            }

            selectedDom = userSelection.htmlText ? getWrap(userSelection.htmlText)
                        :
                        wrap(savedRange.cloneContents());
            clearAll(selectedDom);
            hack.value = selectedDom.innerText || selectedDom.textContent;
            hack.value += "\r\n信息来自：同业114平台(http://www.tongye114.com)\r\n实时掌握供求信息请下载同业MQ：http://im.tongye114.com";
            hack.focus();
            hack.select();
            setTimeout(function() {
                if (userSelection.select) userSelection.select();
                else
                    userSelection.addRange(savedRange);
            }, 10);

            window.scrollTo(w, h);
        }
    </script>
</asp:Content>
