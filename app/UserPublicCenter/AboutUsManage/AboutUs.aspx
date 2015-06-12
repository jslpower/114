<%@ Page Title="关于我们" Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="UserPublicCenter.AboutUsManage.AboutUs" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register src="AboutUsHeadControl.ascx" tagname="AboutUsHeadControl" tagprefix="uc1" %>

<%@ Register src="AboutUsLeftControl.ascx" tagname="AboutUsLeftControl" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("gongqiu") %>" rel="stylesheet" type="text/css" />
    <div id="header">
       
        <uc1:AboutUsHeadControl ID="AboutUsHeadControl1" runat="server" />
       
    </div>
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="142" valign="top" style="background: #FFF7D7;">
                
                <uc2:AboutUsLeftControl ID="AboutUsLeftControl1" runat="server" />
                
            </td>
            <td width="10">
                &nbsp;
            </td>
            <td width="818" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="background: #F6F6F6; padding: 5px;">
                            <img src="<%=ImageServerPath %>/images/UserPublicCenter/companytu.gif" width="811" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop10">
                    <tr>
                        <td width="10%" style="background: url(<%=ImageServerPath %>/images/UserPublicCenter/companyleft.gif) no-repeat left top;
                            height: 10px;">
                        </td>
                        <td width="90%" style="background: #F6F6F6; border: 1px solid #E5E5E5; border-bottom: 0px;
                            border-left: 0px;">
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="background: #F6F6F6;">
                    <tr>
                          <td  style="padding:10px 38px 18px 38px; text-align:left; line-height:24px;">&nbsp;&nbsp;&nbsp;&nbsp;<span class="chengse16"><strong>同业114</strong></span> <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;同业114致力打造中国最大的旅游B2B同业交易平台，为旅行社、票务、景区、酒店、交通等在内的旅游企业，提供24小时在线即时交流和交易服务。
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;同业114在WEB 2.0网站的基础上，率先在国内推出同业MQ即时通讯软件，实现了“网站+即时通讯软件”相结合的新一代应用模式。方便了用户在业务操作过程中即时洽谈，真正实现旅游业务操作的全程信息化和无纸化，使旅游同行之间的高效畅通交流及交易变为现实。
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;五年以来，我们一直专注于旅游行业信息化、网络化的应用开发和服务，我们始终相信旅游电子商务是未来的发展趋势，我们更坚信“同业114平台+MQ软件”相结合的应用模式将会给旅游行业带来全新变化。我们创造性的电子商务平台和多维立体营销推广模式，必将助力旅游同行进一步开拓市场！

　　<br /><br />

&nbsp;&nbsp;&nbsp;&nbsp;<span class="chengse16"><strong>杭州易诺科技有限公司</strong></span> <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;杭州易诺科技有限公司是一家专业从事于提供互联网应用服务的高科技企业。致力于打造中国最大的旅游B2B同业交易平台，公司主要投资方是国内最大金融软件服务商—上市公司恒生电子（600570.SH）的创始人。
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;易诺公司现有员工100余人，平均年龄28岁，硕士学历占5%、本科学历占85%，公司一直专注旅游行业的信息化服务，拥有40多人的资深IT研发团队。自主开发的“同业114”平台、“同业MQ即时通讯软件” 、“同业联盟分销系统”和“易游通旅行社管理系统” 。在旅行界获得了广泛的好评。 
 <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;易诺公司的管理团队源自恒生，“诚信、务实”的经营理念是易诺的立业根基。成熟、科学的管理机制是易诺的发展保障。服务好“客户的客户” 是每一个易诺人的服务理念。 
<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2005年，杭州易诺科技有限公司成立，并面向全国旅游同业推出“中国之旅”网站；
<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2006年，易诺科技自主研发“易游通”旅行社管理系统成功面市，至今有1,000家客户；
<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2007年，易诺科技自主研发“同业通”旅游分销系统并在旅游同行之间广泛投入使用；
<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2008年，易诺科技开始投入研发“同业114”旅游交易平台 和“同业MQ”即时通讯软件；
<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2009年，易诺科技正式推出“同业114”旅游交易平台和“同业MQ”即时通讯软件，至今注册量已近30,000人，在线人数突破5,000人，且每天在持续快速的增长中…….

<br />
&nbsp;&nbsp;&nbsp;&nbsp;<strong>企业文化</strong> <br />
&nbsp;&nbsp;&nbsp;&nbsp;愿 景 
<br />
&nbsp;&nbsp;&nbsp;&nbsp;中国最大的旅游B2B同业交易平台。
 <br />
&nbsp;&nbsp;&nbsp;&nbsp;使 命 
<br />
&nbsp;&nbsp;&nbsp;&nbsp;网络改变旅游
<br />
&nbsp;&nbsp;&nbsp;&nbsp;价值观<br />
&nbsp;&nbsp;&nbsp;&nbsp;1、客户第一
<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;始终把客户放在第一位，关注客户需求，并关注“客户之客户”的需求 
<br />
&nbsp;&nbsp;&nbsp;&nbsp;2、激情向上
<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 永不放弃，积极进取，日事日毕，日清日高
<br />
&nbsp;&nbsp;&nbsp;&nbsp;3、执行有力
<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 尊重制度，有令必行，有禁必止<br />
&nbsp;&nbsp;&nbsp;
4、团队合作
<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
拥抱团队，共享共担，共同成长
<br /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
