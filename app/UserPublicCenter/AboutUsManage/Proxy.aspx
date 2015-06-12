<%@ Page Title="代理合作" Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    AutoEventWireup="true" CodeBehind="Proxy.aspx.cs" Inherits="UserPublicCenter.AboutUsManage.Proxy" %>

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
                                <td  style="padding:10px 38px 18px 38px; text-align:left; line-height:24px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td><img src="<%=ImageServerPath %>/images/UserPublicCenter/hezuo1.gif" width="217" height="29" /></td>
            </tr>
            <tr>
              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;同业114致力打造中国最大的旅游B2B同业交易平台，为旅行社、票务、景区、酒店、交通等在内的旅游企业,提供24小时的即时在线交流和交易服务。
　　<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;同业114在WEB 2.0的网站基础上，率先在国内推出同业MQ即时通讯软件，实现了“网站+即时通讯软件”相结合的新一代应用模式。方便了客户在业务操作过程相互即时聊天洽谈的需要。真正实现了旅游业务操作的全程信息化和无纸化。让旅游业同行之间高效畅通的交流和交易成为了现实。
<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;现今，同业114正逢良好的发展趋势，前景光明，前进的道路上充满了机遇，也伴随着挑战，同业114诚邀您的加盟！让我们共同的迎接这美好的时刻，一起实现持续共赢、持续获利，共创辉煌！<br />
<br /></td>
            </tr>
          </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td><img src="<%=ImageServerPath %>/images/UserPublicCenter/hezuo2.gif" width="217" height="29" /></td>
              </tr>
              <tr>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;<span class="hui14"><strong>产品优势</strong></span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;公司用5年的时间专注于旅游行业的信息化、网络化的应用开发和服务，通过强有力的技术支持和产品设计，开发出了四款旅游市场强有力的管理和市场软件：同业114旅游平台、企业MQ软件、易游通软件以及同业通软件。
 	<br />
 	&nbsp;&nbsp;&nbsp;&nbsp;<img src="<%=ImageServerPath %>/images/UserPublicCenter/hezuoyuandian.gif" width="17" height="17" />同业114旅游平台：致力于打造中国最大的旅游B2B同业交易平台，为旅行社、票务、  景区、酒店、交通等在内的旅游企业，提供24小时在线即时交流和交易服务。
 	<br />
 	&nbsp;&nbsp;&nbsp;&nbsp;<img src="<%=ImageServerPath %>/images/UserPublicCenter/hezuoyuandian.gif" width="17" height="17" />企业MQ软件：目前国内领先的旅游即时通讯软件，集同行分销、在线预订、即时聊天、办公管理为一体，丰富供应商产品。
 	<br />
 	&nbsp;&nbsp;&nbsp;&nbsp;<img src="<%=ImageServerPath %>/images/UserPublicCenter/hezuoyuandian.gif" width="17" height="17" />易游通软件：使得软件业务流程清晰、易学、易用；业务管理细化、精简，适合不同类型的旅行社应用；功能模块多项选择，信息化一步到位，也可分步实施等。
 	<br />
 	&nbsp;&nbsp;&nbsp;&nbsp;<img src="<%=ImageServerPath %>/images/UserPublicCenter/hezuoyuandian.gif" width="17" height="17" />同业通软件：有助于快速构建联合收客圈子，实现市场成本共担、资源共享，迅速将市场做大做强。<br />
 	<br />
 	<span class="hui14"><strong>&nbsp;&nbsp;&nbsp;&nbsp;服务优势</strong></span><br />
 	&nbsp;&nbsp;&nbsp;&nbsp;<img src="<%=ImageServerPath %>/images/UserPublicCenter/hezuoyuandian.gif" width="17" height="17" />完善的技术保障服务：提供稳定的技术服务、对系统进行及时的维护和升级；对于代理商提出的合理技术需求，<br />
 	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;提供技术解决方案等。<br />
&nbsp;&nbsp;&nbsp;&nbsp;<img src="<%=ImageServerPath %>/images/UserPublicCenter/hezuoyuandian.gif" width="17" height="17" />至诚的代理咨询服务：提供产品的相关的技术指导和人员培训；指定相关的销售和客服人员配合代理商市场推动工作等。<br />
 	&nbsp;&nbsp;&nbsp;&nbsp;<img src="<%=ImageServerPath %>/images/UserPublicCenter/hezuoyuandian.gif" width="17" height="17" />良好的资金推广支持：提供人员支持和业务推广费用，为代理商更好地开拓全国市场提供资金支持。<br />
 	<br />
 	&nbsp;&nbsp;&nbsp;&nbsp;<span class="hui14"><strong>运营优势</strong></span><br />
 	&nbsp;&nbsp;&nbsp;&nbsp;<img src="<%=ImageServerPath %>/images/UserPublicCenter/hezuoyuandian.gif" width="17" height="17" />5年的旅游行业经验，具备成熟的渠道运营经验，给予渠道更多的指导和建议。<br />
 	&nbsp;&nbsp;&nbsp;&nbsp;<img src="<%=ImageServerPath %>/images/UserPublicCenter/hezuoyuandian.gif" width="17" height="17" />灵活多样的市场推广活动，针对代理商在市场宣传方面的需求给予相应的支持，并将不遗余力地帮助代理商业务领域中发展壮大。<br />
 	<br /></td>
              </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td><img src="<%=ImageServerPath %>/images/UserPublicCenter/hezuo3.gif" width="217" height="29" /></td>
              </tr>
              <tr>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;1、	成为代理商后，无论是新客户签约还是老客户续费，代理商均可持续获得相应利润<br />
&nbsp;&nbsp;&nbsp;&nbsp;2、	代理商享受作为专线商三年期的同业114平台和MQ软件免费功能的使用权；以及规定区域免费三年的显著LOGO展示权。
  <br />
  &nbsp;&nbsp;&nbsp;&nbsp;3、	代理商营销业绩出色，可以在原先分利益分成上，将收益扩大化。<br />
  <br /></td>
              </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td><img src="<%=ImageServerPath %>/images/UserPublicCenter/hezuo4.gif" width="217" height="29" /></td>
              </tr>
              <tr>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;1、	热爱旅游网络化的发展，认同易诺公司的经营思想和合作模式，认可公司产品和服务以及其发展方向。
                  <br />
                  &nbsp;&nbsp;&nbsp;&nbsp;2、	有着强烈的创业愿望、资深的旅游从业经历和丰富的销售经验。
                  <br />
                  &nbsp;&nbsp;&nbsp;&nbsp;3、	根据当地的市场情况制订切实可行的“市场拓展计划”，并积极主动的开发当地市场。
                  <br />
                  &nbsp;&nbsp;&nbsp;&nbsp;4、	重视并开展对公司品牌的宣传，不可代理（或经销）同行业同类产品。
                  <br />
                  &nbsp;&nbsp;&nbsp;&nbsp;5、	接受公司相关市场、技术指导；遵守公司的市场营销政策和其他管理规定。
                  <br />
                &nbsp;&nbsp;&nbsp;&nbsp;6、	在合作期间完成（或超额完成）协议规定的最低任务。<br />
                <br /></td>
              </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td><img src="<%=ImageServerPath %>/images/UserPublicCenter/hezuo5.gif" width="217" height="29" /></td>
              </tr>
              <tr>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;1、	易诺市场部直接或间接地与当地行业客户（以下称意向合作伙伴）取得联系，双方探计合作的可能性。 
                  <br />
                  &nbsp;&nbsp;&nbsp;&nbsp;2、	意向合作伙伴按照公司要求详细填写“意向合作申请表”（表格附后），并以传真或邮寄的方式发至公司（公司保留对当地市场及意向合作伙伴进行实地综合考察的权利）；其间，意向合作伙伴需提交公司简介、营业执照及相关资质证明等材料。 
下载 “意向合作申请表” 


                  <br />
                &nbsp;&nbsp;&nbsp;&nbsp;3、	易诺公司在对意向合作伙伴的经营条件和实力进行综合评估后，达到公司加盟条件的，对意向合作伙伴宣传应遵守的市场政策和确定应完成的销售任务，然后根据当地市场情况，确定双方进行合作的形式，签定一段时期的，“合作协议书”；最后公司对合作伙伴展开市场、技术培训指导，双方合作正式开始。
                <br />
                <br /></td>
              </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td><img src="<%=ImageServerPath %>/images/UserPublicCenter/hezuo6.gif" width="217" height="29" /></td>
              </tr>
              <tr>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;更多前景分析、行业分析、合作利润保证、经营策略，详询招商热线。
                  <br />
                  &nbsp;&nbsp;&nbsp;&nbsp;电　话：0571-56892803     （项先生）
                  <br />
                  &nbsp;&nbsp;&nbsp;&nbsp;传　真：0571-56893768<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; QQ：569995625<br />
&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp; MQ：23209<br />
&nbsp;&nbsp;&nbsp;
E-mail：xiangjf@enowinfo.com</td>
              </tr>
            </table></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
