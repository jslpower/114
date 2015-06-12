<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UserPublicCenter.AirTickets.Login" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/PageHead.ascx" TagName="PageHead" TagPrefix="uc1" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
</head>
<body>
    <form id="form1" runat="server">
    
    <%this.Page.Response.Redirect("http://vipjp.tongye114.com/"); %> 
    
    
    <uc1:PageHead ID="PageHead1" runat="server" />
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" HeadMenuIndex="3" />
    <!--main start-->
    <div class="main">
        <div class="content01">
            <div class="content01_left">
                <ul>
                    <li><span class="inputtext">用 户 名：</span><input tabindex="10" type="text" id="u" value="<%=EyouSoft.Security.Membership.UserProvider.GetCookie_UserName() %>" /></li>
                    <li><span class="inputtext">用户密码：</span><input tabindex="15" type="password" id="p" value="" /><a
                        href="/Register/FindPassWord.aspx" target="_blank">忘记密码</a></li>
                    <li><span class="inputtext">验 证 码：</span><input tabindex="20" type="text" id="ckcode" size="10" />
                        <img align="absmiddle" width="60" height="20" id="imgCheckCode" src="/ValidateCode.aspx?ValidateCodeName=AirLogin" />
                        <a href="javascript:;" onclick="document.getElementById('imgCheckCode').src='/ValidateCode.aspx?ValidateCodeName=AirLogin&id='+Math.random();$('#spanCodeisNull').hide();return false;">
                            看不清？</a></li>
                    <li><a href="javascript:;" id="linkLogin">
                        <img src="<%=ImageServerPath %>/images/jipiao/login_btn.gif" alt="登录" /></a>
							<a style="margin-left: 35px;" href="/Register/CompanyUserRegister.aspx"><img src="<%=ImageServerPath %>/images/jipiao/zhuce_btn.gif"></a><span
                            class="errmsg" id="spanMsg"></span> </li>
                </ul>
            </div>
            <div class="content01_right">
                <!--焦点图-->
                <div class="container" id="idTransformView">
                    <ul class="slider slider2" id="idSlider" style="position: absolute; left: 0px; top: 0pt;">
                        <%= FiveAdvImages %>
                    </ul>
                    <ul class="num" id="idNum">
                        <%=FiveImagesNumber %>
                    </ul>
                </div>
                <!--焦点图-->
            </div>
            <div class="clearboth">
            </div>
        </div>
        <div class="content02">
            <!-- content02_left start-->
            <div class="content02_left">
                <ul class="title_tab">
                    <li><a id="title_tab1" href="/PlaneInfo/PlaneNewsList.aspx?TypeID=0&CityId=<%=CityId %>"
                        class="book_default" target="_blank">团队信息</a> </li>
                  <%--  <li><a id="title_tab2" href="javascript:;">国际信息</a></li>
                    <li><a id="title_tab3" href="javascript:;">特价信息</a></li>--%>
                </ul>
                <div class="clearboth">
                </div>
                <div class="content02_left_body_box" id="title_tab_content1">
                    <div>
                        <img src="<%=ImageServerPath %>/images/jipiao/content02_left_top.jpg" /></div>
                    <ol>
                        <asp:Repeater ID="rptNewList" runat="server">
                            <ItemTemplate>
                                <%# ShowTicketInfo(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ID").ToString()), DataBinder.Eval(Container.DataItem, "AfficheTitle").ToString(),0)%>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ol>
                    <div class="clearboth">
                    </div>
                    <div>
                        <img src="<%=ImageServerPath %>/images/jipiao/content02_left_bottom.jpg" /></div>
                </div>
                <div class="content02_left_body_box_hidden" id="title_tab_content2">
                    <div>
                        <img src="<%=ImageServerPath %>/images/jipiao/content02_left_top.jpg" /></div>
                    <ol>
                        <li>
                            <h1>
                                <a href="#">上海—香港</a></h1>
                            <h2>
                                东方航空 <span>￥1520</span></h2>
                            <h3>
                                元起</h3>
                        </li>
                        <li>
                            <h1>
                                <a href="#">上海—澳门</a></h1>
                            <h2>
                                澳门航空 <span>￥1580</span></h2>
                            <h3>
                                元起</h3>
                        </li>
                    </ol>
                    <div class="clearboth">
                    </div>
                    <div>
                        <img src="<%=ImageServerPath %>/images/jipiao/content02_left_bottom.jpg" /></div>
                </div>
                <div class="content02_left_body_box_hidden" id="title_tab_content3">
                    <div>
                        <img src="<%=ImageServerPath %>/images/jipiao/content02_left_top.jpg" /></div>
                    <ol>
                        <li>
                            <h1>
                                <a href="#">上海—香港</a></h1>
                            <h2>
                                东方航空 <span>￥1520</span></h2>
                            <h3>
                                元起</h3>
                        </li>
                    </ol>
                    <div class="clearboth">
                    </div>
                    <div>
                        <img src="<%=ImageServerPath %>/images/jipiao/content02_left_bottom.jpg" /></div>
                </div>
            </div>
            <!-- content02_left end -->
            <div class="content02_right">
                <div>
                    <img src="<%=ImageServerPath %>/images/jipiao/jpxq_top.jpg" /></div>
                <div class="content02_right_body">
                    <div class="content02_right_body_L">
                        <div class="number">
                            平台用户数: <span>
                                <asp:Literal ID="ltrPlatformUserCount" runat="server"></asp:Literal></span>
                            家 &nbsp;&nbsp;&nbsp;现有运价数: <span>
                                <asp:Literal ID="ltrFreightCount" runat="server"></asp:Literal></span> 条</div>
                        <div>
                            <div>
                                <span>合作供应:</span>
                                <ul> 
                                    <asp:Repeater ID="dal_PlaneAgu" runat="server">
                                      <ItemTemplate>
                                         <%# ShowTicketInfo(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ID").ToString()), DataBinder.Eval(Container.DataItem, "AfficheTitle").ToString(),2)%>
                                      </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="content02_right_body_R">
                        <ul>
                            <li><a href="/Register/CompanyUserRegister.aspx">
                                <img src="<%=ImageServerPath %>/images/jipiao/cg.jpg" /></a></li>
                            <li><a href="#">
                                <img src="<%=ImageServerPath %>/images/jipiao/xs.jpg" /></a></li>
                            <li><a href="/Register/CompanyUserRegister.aspx">
                                <img src="<%=ImageServerPath %>/images/jipiao/gy.jpg" /></a></li>
                            <li><a href="#">
                                <img src="<%=ImageServerPath %>/images/jipiao/contanct_gy.jpg" /></a></li>
                        </ul>
                    </div>
                    <div class="clearboth">
                    </div>
                </div>
                <div>
                    <img src="<%=ImageServerPath %>/images/jipiao/jpxq_bottom.jpg" /></div>
            </div>
        </div>
    </div>
    <!--main end-->
    
    <div class="bottom" style="margin-top: 10px">
        <div class="bottomleft">
            <% EyouSoft.Model.SystemStructure.SystemInfo Model = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSystemModel();
               if (Model != null)
               {
                   Response.Write(Model.AllRight);
               }
               Model = null;
                 %>
        </div>
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("HomeImages") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("blogin") %>"></script>

    <script type="text/javascript">
    function getCheckCode(){
        var c = document.cookie,ckcode="",tenName="";
        for (var i = 0; i < c.split(";").length; i++) {
            tenName = c.split(";")[i].split("=")[0];
            ckcode = c.split(";")[i].split("=")[1];
            if ($.trim(tenName) == "AirLogin") {
                break;
            }else{
                ckcode="";
            }
        }
        return $.trim(ckcode);
    };
    function login(){
        var u = $.trim($("#u").val()),p =$.trim($("#p").val()),ckcode=$.trim($("#ckcode").val());
        if(u==""){
            $("#spanMsg").html("请输入用户名");
            return;
        }
        if(p==""){
            $("#spanMsg").html("请输入密码");
            return;
        }
        if(ckcode!=getCheckCode()){
            $("#spanMsg").html("请输入正确的验证码");
            return;
        }
        //显示登录状态
        $("#spanMsg").html("正在登录中...");
        //防止重复登陆
        $("#linkLogin").unbind().css("cursor","default");
        blogin2(u, p, "", "<%=goToUrl %>", function(m) {
            $("#spanMsg").html(m);
            //重新绑定登录事件
            $("#linkLogin").click(login).css("cursor","pointer");
        });
        
        return false;
    }
    $(function(){
        var pretab="title_tab",precontent="title_tab_content",
        tabOnClass="book_default",tabHiddenClass="",contentOnClass="content02_left_body_box",
        contentHidenClass="content02_left_body_box_hidden";
        $(".title_tab").find("a").mouseover(function(){
            var index = this.id.substr(this.id.length-1,1);
            $(".title_tab ."+tabOnClass).removeClass(tabOnClass).addClass(tabHiddenClass);
            $(this).removeClass(tabHiddenClass).addClass(tabOnClass);
            
            $(".content02_left_body_box").removeClass(contentOnClass).addClass(contentHidenClass);
            $("#"+precontent+index).removeClass(contentHidenClass).addClass(contentOnClass);
        });
        
        blogin.ssologinurl="<%=Domain.PassportCenter %>";
        $("#linkLogin").click(login);
        $(".content01_left input").bind("keypress", function(e) {
            if (e.keyCode == 13) {
               login();
               return false;
            }
        });
        
        //轮换广告初始化
        HomeBigImages.init(640,4);
    });
    </script>

    </form>
</body>
</html>
