<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftList.ascx.cs" Inherits="UserPublicCenter.WebControl.LeftList" %>
<%@ Import Namespace="EyouSoft.Common" %>
<div class="box">
    <div class="box-l">
        <div class="box-r">
            <div class="box-c">
                <h3 class="add add-icon">
                    热点咨询排行</h3>
            </div>
        </div>
    </div>
    <div class="box-main">
        <div class="box-content">
            <ul class="list">
                <%=HotLists%>
            </ul>
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
                    供求信息</h3>
            </div>
        </div>
    </div>
    <div class="box-main">
        <div class="box-content">
            <ul class="list">
                <asp:Repeater ID="star" runat="server">
                    <ItemTemplate>
                        <li><a title='<%#Eval("ExchangeTitle")%>' href='<%#EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(Eval("ID").ToString(), CityId)%>'>
                            <%#EyouSoft.Common.Utils.GetText2(Eval("ExchangeTitle").ToString(), 16, true)%></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
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
                    最新行业新闻</h3>
            </div>
        </div>
    </div>
    <div class="box-main">
        <div class="box-content">
            <ul class="list">
                <asp:Repeater ID="news" runat="server">
                    <ItemTemplate>
                        <li newid='<%#Eval("ID")%>'><a title='<%#Eval("AfficheTitle")%>' href='<%#Eval("GotoUrl").ToString().Length > 0 ? Eval("GotoUrl") : EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(Convert.ToInt32(Eval("AfficheClass")),Convert.ToInt32(Eval("Id")))%>''>
                            <%#EyouSoft.Common.Utils.GetText2(Eval("AfficheTitle").ToString(),16,true)%></a>
                        </li>
                        
                        <li class='first' style="display: none">
                            <div class='img'>
                                <a title='<%#Eval("AfficheTitle")%>' href='<%#Eval("GotoUrl").ToString().Length > 0 ? Eval("GotoUrl") : EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(Convert.ToInt32(Eval("AfficheClass")),Convert.ToInt32(Eval("Id")))%>'>
                                    <img style="width:76px;height:69px" alt='' src='<%#Utils.GetNewImgUrl(Eval("PicPath").ToString(), 3)%>'></a>
                            </div>
                            <div class='text'>
                                <h4>
                                    <a href="<%#Eval("GotoUrl").ToString().Length > 0 ? Eval("GotoUrl") : EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(Convert.ToInt32(Eval("AfficheClass")),Convert.ToInt32(Eval("Id")))%>">
                                        <%#EyouSoft.Common.Utils.GetText2(Eval("AfficheTitle").ToString(),12,true)%>
                                    </a>
                                </h4>
                                <p>
                                    <a href="<%#Eval("GotoUrl").ToString().Length > 0 ? Eval("GotoUrl") : EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(Convert.ToInt32(Eval("AfficheClass")),Convert.ToInt32(Eval("Id")))%>">
                                        <%#Utils.GetText2(Eval("AfficheDesc").ToString(),30,true)%>
                                    </a>
                                </p>
                            </div>
                            <div class="clearBoth"></div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
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
                    最新发布线路</h3>
            </div>
        </div>
    </div>
    <div class="box-main">
        <div class="box-content">
            <ul class="list">
                <%=RouteList%>
            </ul>
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
                    旅游企业</h3>
            </div>
        </div>
    </div>
    <div class="box-main">
        <div class="box-content">
            <ul class="list">
                <asp:Repeater ID="company" runat="server">
                    <ItemTemplate>
                        <li><a title='<%#Eval("CompanyName")%>' href='<%#EyouSoft.Common.Utils.GetDomainByCompanyId(Eval("ID").ToString(), CityId)%>'>
                            <%#EyouSoft.Common.Utils.GetText2(Eval("CompanyName").ToString(),16,true)%></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</div>
<!--列表 end-->

<script type="text/javascript">
    $(function() {

        $("li[newid]").bind("mouseover", function() {

                       $("li[class='first']").hide();
                        $(this).next().show();
                        $("li[newid]").show();
                       $(this).hide();

        });

    });
</script>

