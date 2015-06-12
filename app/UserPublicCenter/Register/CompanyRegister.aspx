<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyRegister.aspx.cs"
    MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" Inherits="UserPublicCenter.CompanyRegister" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/WebControl/RegisterHead.ascx" TagName="RegisterHead" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="Main" ID="Default_ctMain" runat="server">
    <uc1:RegisterHead ID="RegisterHead1" runat="server" />
    <div class="body">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="center">
                    <img id="RegisterHeadimg" src="<%=ImageServerPath %>/images/UserPublicCenter/join-1.gif"
                        width="956" height="30" />
                </td>
            </tr>
        </table>

        <script type="text/javascript">
            function mouseovertr(o) {
                o.style.backgroundColor = "#FFF6C7";
            }
            function mouseouttr(o) {
                o.style.backgroundColor = ""
            }
        </script>

        <div id="divFristRegister">
            <table width="940" border="0" cellspacing="0" cellpadding="5" class="maintop15">
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td height="40" align="right">
                        <span class="ff0000"><strong>*</strong></span><strong>公司名称：</strong>
                    </td>
                    <td align="left">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <input id="txtCompanyName" name="txtCompanyName" type="text" class="bitian" size="50" />
                                </td>
                                <td>
                                    <div class="tist" style="display: none">
                                        <img src="<%=ImageServerPath %>/images/UserPublicCenter/1000716.gif" width="16" height="16" />
                                        请输入您所在单位的全称，如：某某某旅行社。
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td width="27%" height="40" align="right">
                        <span class="ff0000"><strong>*</strong></span><strong>用户名：</strong>
                    </td>
                    <td align="left">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="11%">
                                    <input id="txtUserName" name="txtUserName" type="text" class="bitian" size="20" />
                                </td>
                                <td width="89%">
                                    <div class="tist" style="display: none">
                                        <img src="<%=ImageServerPath %>/images/UserPublicCenter/1000716.gif" width="16" height="16" />
                                        5-20个字符(包括小写字母、数字、下划线、中文)，一个汉字为两个字符。一旦注册成功会员名不能修改，并且 <a href="http://www.tongye114.com"
                                            target="_blank">同业MQ</a> 亦可同步使用</div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td height="40" align="right">
                        <span class="ff0000"><strong>*</strong></span><strong>登录密码：</strong>
                    </td>
                    <td align="left">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="11%">
                                    <input id="txtFristPassWord" name="txtFristPassWord" type="password" class="bitian"
                                        size="20" />
                                </td>
                                <td width="89%">
                                    <div class="tist" style="display: none">
                                        <img src="<%=ImageServerPath %>/images/UserPublicCenter/1000716.gif" width="16" height="16" />
                                        密码由6-16个字符组成，请使用英文字母加数字或符号的组合密码，不能单独使用英文字母、数字或符号作为您的密码。</div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td height="40" align="right">
                        <span class="ff0000"><strong>*</strong></span><strong>确认密码：</strong>
                    </td>
                    <td align="left">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="11%">
                                    <input id="txtSecondPassWord" name="txtSecondPassWord" type="password" class="bitian"
                                        size="20" />
                                </td>
                                <td width="89%">
                                    <div class="tist" id="divSecondPassErr" style="display: none">
                                        <img src="<%=ImageServerPath %>/images/UserPublicCenter/1000716.gif" width="16" height="16" />
                                        请再输入一遍您上面输入的密码。</div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td height="40" align="right">
                        <span class="ff0000"><strong>*</strong></span><strong>验证码：</strong>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtValidateCode" runat="server" CssClass="bitian" size="6" MaxLength="4"></asp:TextBox>
                        <a title="刷新验证码" href="#" onclick="javascript:document.getElementById('<%=imgValidateCode.ClientID %>').src='/ValidateCode.aspx?ValidateCodeName=CompanyRegisterCode&id='+Math.random();$('#spanCodeisNull').hide();return false;">
                            <asp:Image ID="imgValidateCode" runat="server" /></a>

                        <script language="javascript">
                            document.getElementById('<%= imgValidateCode.ClientID %>').src = '/ValidateCode.aspx?id=' + Math.random() + "&ValidateCodeName=CompanyRegisterCode";
                        </script>

                        <span id="spanCodeisNull" style="display: none" class="errmsg">请输入验证码</span> 
                        <span id="spanCodeErr" style="display: none" class="errmsg">请输入正确的验证码</span>
                    </td>
                </tr>
                <tr>
                    <td height="40" align="right">
                        &nbsp;
                    </td>
                    <td align="left">
                        <input type="button" id="btnFristRegister" value="同意以下服务条款，并提交注册" style="background: url(<%=ImageServerPath %>/images/UserPublicCenter/subb.gif);
                            width: 270px; height: 37px; border: none; font-size: 14px; font-weight: bold;
                            color: #ffffff; cursor: pointer" />
                    </td>
                </tr>
            </table>
            <table width="900" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left">
                        <input id="F_ckPact" name="checkbox" type="checkbox" value="checkbox" onclick="Register.ChangeckPoct();"
                            checked="true" />
                        <label for="F_ckPact" style="cursor: pointer">
                            阅读并同意以下条款</label>&nbsp;&nbsp;<span class="errmsg" style="display: none" id="span_PactErr">同意以下条款才能完成注册</span>
                        <br />
                        <textarea name="textfield4" cols="140" rows="6">一、本服务协议双方为杭州易诺科技有限公司（下称“易诺科技”）与旅游同业网用户，本服务协议具有合同效力。

    本服务协议内容包括协议正文及所有易诺科技已经发布的或将来可能发布的各类规则。所有规则为协议不可分割的一部分，与协议正文具有同等法律效力。 
　　在本服务协议中没有以“规则”字样表示的链接文字所指示的文件不属于本服务协议的组成部分，而是其它内容的协议或有关参考数据，与本协议没有法律上的直接关系。 
　　用户在使用易诺科技提供的各项服务的同时，承诺接受并遵守各项相关规则的规定。易诺科技有权根据需要不时地制定、修改本协议或各类规则，如本协议有任何变更，易诺科技将在网站上刊载公告，通知予用户。如用户不同意相关变更，必须停止使用“服务”。经修订的协议 
一经在旅游同业网公布后，立即自动生效。各类规则会在发布后生效，亦成为本协议的一部分。登录或继续使用“服务”将表示用户接受经修订的协议。除另行明确声明外，任何使“服务”范围扩大或功能增强的新内容均受本协议约束。 
　　用户确认本服务协议后，本服务协议即在用户和易诺科技之间产生法律效力。请用户务必在注册之前认真阅读全部服务协议内容，如有任何疑问，可向易诺科技咨询。 1)无论用户事实上是否在注册之前认真阅读了本服务协议，只要用户点击协议正本下方的“确认”按钮并按照易诺科技注册程序成功注册为用户，用户的行为仍然表示其同意并签署了本服务协议。 2)本协议不涉及用户与易诺科技其它用户之间因网上交易而产生的法律关系及法律纠纷。 

二、 定义

旅游同业网上交易平台：有关旅游同业网上交易平台上的术语或图示的含义，详见旅游同业网及关于旅游同业网帮助。 
　　用户及用户注册：用户必须是具备完全民事行为能力的自然人，或者是具有合法经营资格的实体组织。无民事行为能力人、限制民事行为能力人以及无经营或特定经营资格的组织不当注册为易诺科技用户或超过其民事权利或行为能力范围从事交易的，其与易诺科技之间的服务协议自始无效，易诺科技一经发现，有权立即注销该用户，并追究其使用旅游同业网“服务”的一切法律责任。用户注册是指用户登陆旅游同业网，并按要求填写相关信息并确认同意履行相关用户协议的过程。用户因进行交易、获取有偿服务或接触旅游同业网服务器而发生的所有应纳税赋，以及一切硬件、软件、服务及其它方面的费用均由用户负责支付。旅游同业网站仅作为交易地点。旅游同业网仅作为用户物色交易对象，货物和服务的交易进行协商，以及获取各类与贸易相关的服务的地点。易诺科技不能控制交易所涉及的商品、服务的质量、安全或合法性，商贸信息的真实性或准确性，以及交易方履行其在贸易协议项下的各项义务的能力。易诺科技并不作为买家或是卖家的身份参与买卖行为的本身。易诺科技提醒用户应该通过自己的谨慎判断确定登录商品、服务及相关信息的真实性、合法性和有效性。  



三、 用户权利和义务：

用户有权利拥有自己在旅游同业网的用户名及交易密码，并有权利使用自己的用户名及密码随时登陆旅游同业网交易平台。用户不得以任何形式擅自转让或授权他人使用自己的旅游同业网用户名； 
　　用户有权根据本服务协议的规定以及旅游同业网上发布的相关规则利用旅游同业网上交易平台查询商品及服务信息、发布交易信息、登录商品、参加网上商品竞买、与其它用户订立商品买卖合同、评价其它用户的信用、参加易诺科技的有关活动以及有权享受易诺科技提供的其它的有关信息服务； 
　　用户在旅游同业网上交易过程中如与其他用户因交易产生纠纷，可以请求易诺科技从中予以协调。用户如发现其他用户有违法或违反本服务协议的行为，可以向易诺科技进行反映要求处理。如用户因网上交易与其他用户产生诉讼的，用户有权通过司法部门要求易诺科技提供相关资料； 
　　用户有义务在注册时提供自己的真实资料，并保证诸如联系人、电子邮件地址、联系电话、联系地址、邮政编码等内容的有效性及安全性，保证易诺科技及其他用户可以通过上述联系方式与自己进行联系。同时，用户也有义务在相关资料实际变更时及时更新有关注册资料。用户保证不以他人资料在旅游同业网进行注册或认证； 
　　用户应当保证在使用旅游同业网网上交易平台进行交易过程中遵守诚实信用的原则，不在交易过程中采取不正当竞争行为，不扰乱网上交易的正常秩序，不从事与网上交易无关的行为； 
　　用户不应在旅游同业网网上交易平台上恶意评价其他用户，或采取不正当手段提高自身的信用度或降低其他用户的信用度； 
　　用户在旅游同业网网上交易平台上不得发布各类违法或违规信息； 
　　用户在旅游同业网网上交易平台上不得买卖国家禁止销售的或限制销售的商品、不得买卖侵犯他人知识产权或其它合法权益的商品，也不得买卖违背社会公共利益或公共道德的、或是易诺科技认为不适合在旅游同业网上销售的商品。 
　　用户承诺自己在使用旅游同业网时实施的所有行为均遵守国家法律、法规和易诺科技的相关规定以及各种社会公共利益或公共道德。如有违反导致任何法律后果的发生，用户将以自己的名义独立承担所有相应的法律责任； 
　　用户同意，不对旅游同业网上任何数据作商业性利用，包括但不限于在未经易诺科技事先书面批准的情况下，以复制、传播等方式使用在旅游同业网站上展示的任何资料。

四、易诺科技的权利和义务：

易诺科技有义务在现有技术上维护整个网上交易平台的正常运行，并努力提升和改进技术，使用户网上交易活动得以顺利进行； 
　　对用户在注册使用旅游同业网上交易平台中所遇到的与交易或注册有关的问题及反映的情况，易诺科技应及时作出回复； 
　　对于用户在旅游同业网网上交易平台上的不当行为或其它任何易诺科技认为应当终止服务的情况，易诺科技有权随时作出删除相关信息、终止服务提供等处理，而无须征得用户的同意； 
　　因网上交易平台的特殊性，易诺科技没有义务对所有用户的注册数据、所有的交易行为以及与交易有关的其它事项进行事先审查，但如存在下列情况： 
①用户或其它第三方通知易诺科技，认为某个具体用户或具体交易事项可能存在重大问题；
②用户或其它第三方向易诺科技告知交易平台上有违法或不当行为的，易诺科技以普通非专业交易者的知识水平标准对相关内容进行判别，可以明显认为这些内容或行为具有违法或不当性质的；
易诺科技有权根据不同情况选择保留或删除相关信息或继续、停止对该用户提供服务，并追究相关法律责任。
用户在旅游同业网上交易过程中如与其它用户因交易产生纠纷，请求易诺科技从中予以调处，经易诺科技审核后，易诺科技有权通过电子邮件联系向纠纷双方了解情况，并将所了解的情况通过电子邮件互相通知对方； 
　　用户因在旅游同业网上交易与其它用户产生诉讼的，用户通过司法部门或行政部门依照法定程序要求易诺科技提供相关数据，易诺科技应积极配合并提供有关资料； 
　　易诺科技有权对用户的注册数据及交易行为进行查阅，发现注册数据或交易行为中存在任何问题或怀疑，均有权向用户发出询问及要求改正的通知或者直接作出删除等处理； 
　　经国家生效法律文书或行政处罚决定确认用户存在违法行为，或者易诺科技有足够事实依据可以认定用户存在违法或违反服务协议行为的，易诺科技有权在易诺科技交易平台及所在网站上以网络发布形式公布用户的违法行为； 
　　对于用户在易诺科技交易平台发布的下列各类信息，易诺科技有权在不通知用户的前提下进行删除或采取其它限制性措施，包括但不限于以规避费用为目的的信息；以炒作信用为目的的信息；易诺科技有理由相信存在欺诈等恶意或虚假内容的信息；易诺科技有理由相信与网上交易无关或不是以交易为目的的信息；易诺科技有理由相信存在恶意竞价或其它试图扰乱正常交易秩序因素的信息；易诺科技有理由相信该信息违反公共利益或可能严重损害易诺科技和其它用户合法利益的； 
　　许可使用权。 用户以此授予易诺科技独家的、全球通用的、永久的、免费的许可使用权利 (并有权对该权利进行再授权)，使易诺科技有权(全部或部份地) 使用、复制、修订、改写、发布、翻译、分发、执行和展示用户公示于网站的各类信息或制作其派生作品，和/或以现在已知或日后开发的任何形式、媒体或技术，将上述信息纳入其它作品内； 
　　易诺科技会在用户的计算机上设定或取用易诺科技cookies。 易诺科技允许那些在旅游同业网页上发布广告的公司到用户计算机上设定或取用 cookies 。 在用户登录时获取数据，易诺科技使用cookies可为用户用户提供个性化服务。 如果拒绝所有 cookies，用户将不能使用需要登录的易诺科技产品及服务内容。 
　　
五、服务的中断和终止：

用户同意，在易诺科技未向用户收取服务费的情况下，易诺科技可自行全权决定以任何理由 (包括但不限于易诺科技认为用户已违反本协议的字面意义和精神，或以不符合本协议的字面意义和精神的方式行事，或用户在超过90天的时间内未以用户的账号及密码登录网站等) 终止用户的“服务”密码、账户 (或其任何部份) 或用户对“服务”的使用，并删除（不再保存）用户在使用“服务”中提交的任何资料。同时易诺科技可自行全权决定，在发出通知或不发出通知的情况下，随时停止提供“服务”或其任何部份。账号终止后，易诺科技没有义务为用户保留原账号中或与之相关的任何信息，或转发任何未曾阅读或发送的信息给用户或第三方。此外，用户同意，易诺科技不就终止用户接入“服务”而对用户或任何第三者承担任何责任； 
　　如用户向易诺科技提出注销旅游同业网注册用户身份时，经易诺科技审核同意，由易诺科技注销该注册用户，用户即解除与易诺科技的服务协议关系。但注销该用户账号后，易诺科技仍保留下列权利： 


①用户注销后，易诺科技有权保留该用户的注册数据及以前的交易行为记录。
②用户注销后，如用户在注销前在易诺科技交易平台上存在违法行为或违反合同的行为，易诺科技仍可行使本服务协议所规定的权利；
在下列情况下，易诺科技可以通过注销用户的方式终止服务： 
①在用户违反本服务协议相关规定时，易诺科技有权终止向该用户提供服务。易诺科技将在中断服务时通知用户。但如该用户在被易诺科技终止提供服务后，再一次直接或间接或以他人名义注册为易诺科技用户的，易诺科技有权再次单方面终止向该用户提供服务；

②如易诺科技通过用户提供的信息与用户联系时，发现用户在注册时填写的电子邮箱已不存在或无法接收电子邮件的，经易诺科技以其它联系方式通知用户更改，而用户在三个工作日内仍未能提供新的电子邮箱地址的，易诺科技有权终止向该用户提供服务；

③一旦易诺科技发现用户注册数据中主要内容是虚假的，易诺科技有权随时终止向该用户提供服务；

④本服务协议终止或更新时，用户明示不愿接受新的服务协议的；

⑤其它易诺科技认为需终止服务的情况。

服务中断、终止之前用户交易行为的处理因用户违反法律法规或者违反服务协议规定而致使易诺科技中断、终止对用户服务的，对于服务中断、终止之前用户交易行为依下列原则处理： 
①服务中断、终止之前，用户已经上传至旅游同业网的商品尚未交易或尚未交易完成的，易诺科技有权在中断、终止服务的同时删除此项商品的相关信息；
②服务中断、终止之前，用户已经就其它用户出售的具体商品作出要约，但交易尚未结束，易诺科技有权在中断或终止服务的同时删除该用户的相关要约；
③服务中断、终止之前，用户已经与另一用户就具体交易达成一致，易诺科技可以不删除该项交易，但易诺科技有权在中断、终止服务的同时将用户被中断或终止服务的情况通知用户的交易对方。 

六、责任范围：

用户明确理解和同意，易诺科技不对因下述任一情况而导致的任何损害赔偿承担责任，包括但不限于利润、商誉、使用、数据等方面的损失或其它无形损失的损害赔偿 (无论易诺科技是否已被告知该等损害赔偿的可能性)： 
　　使用或未能使用“服务；第三方未经批准的接入或第三方更改用户的传输数据或数据；第三方对“服务”的声明或关于“服务”的行为；或非因易诺科技的原因而引起的与“服务”有关的任何其它事宜，包括疏忽。用户明确理解并同意，如因其违反有关法律或者本协议之规定，使易诺科技遭受任何损失，受到任何第三方的索赔，或任何行政管理部门的处罚，用户应对易诺科技提供补偿，包括合理的律师费用。 

七、隐私权政策：

适用范围： 

①在用户注册旅游同业网账户时，用户根据易诺科技要求提供的个人注册信息；
②在用户使用易诺科技服务，参加易诺科技活动，或访问旅游同业网页时，易诺科技自动接收并记录的用户浏览器上的服务器数值，包括但不限于IP地址等数据及用户要求取用的网页记录；
③易诺科技收集到的用户在易诺科技进行交易的有关数据，包括但不限于出价、购买、商品登录、信用评价及违规记录；
④易诺科技通过合法途径从商业伙伴处取得的用户个人数据。
信息使用： 
①易诺科技不会向任何人出售或出借用户的个人信息，除非事先得到用户得许可。
②易诺科技亦不允许任何第三方以任何手段收集、编辑、出售或者无偿传播用户的个人信息。任何用户如从事上述活动，一经发现，易诺科技有权立即终止与该用户的服务协议，查封其账号。 
③为服务用户的目的，易诺科技可能通过使用用户的个人信息，向用户提供服务，包括但不限于向用户发出产品和服务信息，或者与易诺科技合作伙伴共享信息以便他们向用户发送有关其产品和服务的信息（后者需要用户的事先同意）。

信息披露：

用户的个人信息将在下述情况下部分或全部被披露：

①经用户同意，向第三方披露； 
②如用户是合资格的知识产权投诉人并已提起投诉，应被投诉人要求，向被投诉人披露，以便双方处理可能的权利纠纷；
③根据法律的有关规定，或者行政或司法机构的要求，向第三方或者行政、司法机构披露；
④如果用户出现违反中国有关法律或者网站政策的情况，需要向第三方披露；
⑤为提供你所要求的产品和服务，而必须和第三方分享用户的个人信息；
⑥其它易诺科技根据法律或者网站政策认为合适的披露；
⑦在旅游同业网上创建的某一交易中，如交易任何一方履行或部分履行了交易义务并提出信息披露请求的， 易诺科技有全权可以决定向该用户提供其交易对方的联络方式等必要信息，以促成交易的完成或纠纷的解决。

信息安全： 

①易诺科技账户均有密码保护功能，请妥善保管用户的账户及密码信息； 
②在使用易诺科技服务进行网上交易时，用户不可避免的要向交易对方或潜在的交易对方提供自己的个人信息，如联络方式或者邮政地址。请用户妥善保护自己的个人信息，仅在必要的情形下向他人提供；
③如果用户发现自己的个人信息泄密，尤其是易诺科技账户及密码发生泄露，请用户立即联络易诺科技客服，以便易诺科技采取相应措施。

Cookie的使用： 

①通过易诺科技所设Cookie所取得的有关信息，将适用本政策；
②在易诺科技上发布广告的公司通过广告在用户计算机上设定的Cookies，将按其自己的隐私权政策使用。
编辑和删除个人信息的权限： 用户可以点击我的易诺科技对用户的个人信息进行编辑和删除，除非易诺科技另有规定。
政策修改：易诺科技保留对本政策作出不时修改的权利。 

管辖：

本服务条款之解释与适用，以及与本服务条款有关的争议，均应依照中华人民共和国法律予以处理，并以浙江省杭州市西湖区人民法院为第一审管辖法院。
</textarea>
                    </td>
                </tr>
                <tr>
                    <td height="30" align="left">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div id="divSecondRegister" style="display:none">
            <table width="940" border="0" cellspacing="0" cellpadding="5" class="maintop15">
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td align="right">
                        <strong>公司名称：</strong>
                    </td>
                    <td align="left">
                        <span style="font-size: 16px;">
                            <label id="labCompanyName">
                            </label>
                        </span>
                    </td>
                </tr>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td width="27%" align="right">
                        <strong>许可证号：</strong>
                    </td>
                    <td align="left">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="20%">
                                    <input id="txtLicenseNumber" name="txtLicenseNumber" type="text" class="bitian2"
                                        size="20" />
                                </td>
                                <td width="80%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td align="right">
                        <span class="ff0000"><strong>*</strong></span><strong>品牌名称：</strong>
                    </td>
                    <td align="left">
                        <input id="txtBrandName" name="txtBrandName" type="text" class="bitian1" size="20"
                            valid="required" errmsg="请输入品牌名称" />
                        <span id="errMsg_txtBrandName" class="errmsg"></span>
                    </td>
                </tr>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td align="right">
                        <span class="ff0000"><strong>*</strong></span><strong>所在地：</strong>
                    </td>
                    <td align="left">
                        &nbsp;
                        <asp:DropDownList ID="dropProvinceList" runat="server">
                            <asp:ListItem Value="">省份</asp:ListItem>
                            <asp:ListItem Value="1">1111</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="dropCityList" runat="server" >
                            <asp:ListItem Value="">城市</asp:ListItem>
                             <asp:ListItem Value="2">2222</asp:ListItem>
                        </asp:DropDownList>
                        <span id="errMsg_Province" style="display:none" class="errmsg">请选择省份</span>
                        <span id="errMsg_City" style="display:none" class="errmsg">请选择城市</span>
                    </td>
                </tr>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td align="right">
                        <span class="ff0000"><strong>*</strong></span><strong>办公地点</strong><strong>：</strong>
                    </td>
                    <td align="left">
                        <input id="txtOfficeAddress" name="txtOfficeAddress" type="text" class="bitian1"
                            size="50" valid="required" errmsg="请输入办公地点" />
                        <span id="errMsg_txtOfficeAddress" class="errmsg"></span>
                    </td>
                </tr>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td align="right">
                        <strong>引荐单位：</strong>
                    </td>
                    <td align="left">
                        <input id="txtCommendCompany" name="txtCommendCompany" type="text" class="bitian2"
                            size="50" />
                    </td>
                </tr>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td align="right">
                        <span class="ff0000"><strong>*</strong></span><strong>经营范围：</strong>
                    </td>
                    <td align="left">
                        <table>
                            <tr>
                                <td>
                                    <table width="400" border="0" cellspacing="0" cellpadding="2" style="border: 1px solid #D1E7F4;
                                        background: #F2FAFF">
                                        <tr>
                                            <td width="25%" align="left">                                                <input type="radio" id="CompanyType1" name="radManageArea" value="0" valid="requiredRadioed"
                                                    errmsg="请选择经营范围" errmsgend="radManageArea" /><strong style="color: #3366CC"><label
                                                        style="cursor: pointer" for="CompanyType1">旅行社</label>
                                                    </strong>
                                            </td>
                                            <td width="25%" align="left">
                                                <input type="checkbox" id="CompanyType2" name="ckCompanyType" disabled="disabled"
                                                    value="2" /><label style="cursor: pointer" for="CompanyType2">组团社</label>
                                            </td>
                                            <td width="25%" align="left">
                                                <input type="checkbox" name="ckCompanyType" id="CompanyType3" disabled="disabled"
                                                    value="1" /><label style="cursor: pointer" for="CompanyType3">专线商</label>
                                            </td>
                                            <td width="25%" align="left">
                                                <input type="checkbox" name="ckCompanyType" id="CompanyType4" disabled="disabled"
                                                    value="3" /><label style="cursor: pointer" for="CompanyType4">地接社</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <input type="radio" id="CompanyType5" name="radManageArea" value="4" /><strong style="color: #3366CC">
                                                    <label style="cursor: pointer" for="CompanyType5">
                                                        景区</label></strong>
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <input type="radio" id="CompanyType6" name="radManageArea" value="5" /><strong style="color: #3366CC"><label
                                                    style="cursor: pointer" for="CompanyType6">酒店</label></strong>
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <input type="radio" id="CompanyType7" id="CompanyType6" name="radManageArea" value="6" /><strong
                                                    style="color: #3366CC"><label style="cursor: pointer" for="CompanyType7">车队</label></strong>
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <input type="radio" id="CompanyType8" name="radManageArea" value="7" /><strong style="color: #3366CC"><label
                                                    style="cursor: pointer" for="CompanyType8">旅游用品</label></strong>
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <input type="radio" id="CompanyType9" name="radManageArea" value="8" /><strong style="color: #3366CC"><label
                                                    style="cursor: pointer" for="CompanyType9">购物点</label></strong>
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="100px">
                                    <span class="errmsg" id="errMsg_radManageArea"></span>
                                     <span class="errmsg" id="errMsg_ckCompanyType" style="display:none">请选择旅行社类型</span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td align="right">
                        <strong>真实姓名：</strong>
                    </td>
                    <td align="left">
                        <input id="txtContactName" name="txtContactName" type="text" class="bitian2" size="20" />
                    </td>
                </tr>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td align="right">
                        <span class="ff0000"><strong>*</strong></span><strong>电话：</strong>
                    </td>
                    <td align="left">
                        <input id="txtContactTel" name="txtContactTel" type="text" class="bitian1" size="20"
                            valid="required|isPhone" errmsg="请输入电话号码|请输入正确的电话号码" />
                        <span id="errMsg_txtContactTel" class="errmsg"></span>
                    </td>
                </tr>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td align="right">
                        <span class="ff0000"><strong>*</strong></span><strong>手机：</strong>
                    </td>
                    <td align="left">
                        <input id="txtContactMobile" name="txtContactMobile" type="text" class="bitian1"
                            size="20" valid="required|isMobile" errmsg="请输入手机号码|请输入正确的手机号码" />
                        <span id="errMsg_txtContactMobile" class="errmsg"></span>
                    </td>
                </tr>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td align="right">
                        <strong>传真：</strong>
                    </td>
                    <td align="left">
                        <input name="textfield2233" type="text" class="bitian2" size="20" />
                    </td>
                </tr>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td align="right">
                        <span class="ff0000"><strong>*</strong></span><strong>E-mail：</strong>
                    </td>
                    <td align="left">
                        <input id="txtContactEmail" name="txtContactEmail" type="text" class="bitian1" size="20"
                            valid="required|isEmail" errmsg="请输入Email|请输入正确的Email" />
                        <span id="errMsg_txtContactEmail" class="errmsg"></span>
                    </td>
                </tr>
                <%--  <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td align="right">
                        <strong>MQ：</strong>
                    </td>
                    <td align="left">
                        <span style="font-size: 16px; font-weight: bold;">m123</span> <span style="color: #999999">
                            (注：您的会员账号 <a href="http://im.tongye114.com" target="_blank">同业MQ</a> 亦可同步使用)
                        
                        
                        
                        
                        
                        </span>
                    </td>
                </tr>--%>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td align="right">
                        <strong>QQ：<br />
                        </strong>
                    </td>
                    <td align="left">
                        <input id="txtContactQQ" name="txtContactQQ" type="text" class="bitian2" size="20" />
                    </td>
                </tr>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td align="right">
                        <strong>MSN：</strong>
                    </td>
                    <td align="left">
                        <input id="txtContactMSN" name="txtContactMSN" type="text" class="bitian2" size="20" />
                    </td>
                </tr>
                <tr>
                    <td height="40" align="right">
                        &nbsp;
                    </td>
                    <td align="left">
                        <input type="button" id="btnSecondRegister" value="资料填写完成！提交并进入系统" style="background: url(<%=ImageServerPath %>/images/UserPublicCenter/subb.gif);
                            width: 270px; height: 37px; border: none; font-size: 14px; font-weight: bold;
                            color: #ffffff; cursor: pointer" />
                    </td>
                </tr>
            </table>
            <table width="900" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td height="30" align="left">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("fv_onblur") %>"></script>

    <script type="text/javascript">
        var Register = {
            ckInputisNull: function($obj, isOnclick) { //注册第一步（填写会员信息）时的验证
                var objValue = $.trim($obj.val());
                var isPassck = true;
                $obj.parent("td").next().find("div").hide();
                if (objValue != "") {
                    var objId = $obj.attr("id");
                    if (objId == "txtUserName") {
                        //用户名长度5-20
                        if (objValue.length < 5 || objValue.length > 20) {
                            $obj.parent("td").next().find("div").show();
                            isPassck = false;
                        }
                    } else if (objId == "txtFristPassWord") { //密码
                        //长度6-16
                        if (objValue.length < 6 || objValue.length > 16) {
                            $obj.parent("td").next().find("div").show();
                            isPassck = false;
                        }
                        //不能为纯数字，纯字母，纯符号
                        if ((/^[\d]*$/g.test(objValue)) || (/^[a-z]*$/gi.test(objValue)) || (/^[\W_]*$/gi.test(objValue))) {
                            $obj.parent("td").next().find("div").show();
                            isPassck = false;
                        }
                        if (objValue == $.trim($("#txtSecondPassWord").val())) {
                            $("#divSecondPassErr").hide();
                        }
                    }
                    else if (objId == "txtSecondPassWord") {
                        //确认密码
                        if (objValue != $.trim($("#txtFristPassWord").val())) {
                            $obj.parent("td").next().find("div").show();
                            isPassck = false;
                        }
                    }
                } else {
                    if (isOnclick) {
                        $obj.parent("td").next().find("div").show();
                        isPassck = false;
                    }
                }
                return isPassck;
            },
            ckValidateCode: function() {
                var $CodeObj = $("#<%=txtValidateCode.ClientID %>");
                var CodeisTrue = true;
                if ($.trim($CodeObj.val()) == "") {
                    $("#spanCodeisNull").show();
                    CodeisTrue = false;
                } else {
                    var validResult = false;
                    var arrStrCookie = document.cookie;
                    for (var i = 0; i < arrStrCookie.split(";").length; i++) {
                        var temName = arrStrCookie.split(";")[i].split("=")[0];
                        var temCode = arrStrCookie.split(";")[i].split("=")[1];
                        if (temName == "CompanyRegisterCode") {
                            if (temCode == $.trim($CodeObj.val())) {
                                validResult = true;
                            }
                        }
                    }
                    if (!validResult) {
                        $("#spanCodeErr").show();
                        CodeisTrue = false;
                    }
                }
                return CodeisTrue;
            },
            ChangeckPoct: function() {
                if (!$("#F_ckPact").attr("checked")) {
                    $("#span_PactErr").show();
                } else {
                    $("#span_PactErr").hide();
                }
            }
        };

        $(function() {
            $("#divFristRegister .bitian").focus(function() {
                if ($.trim($(this).val()) == "") {
                    $(this).parent("td").next().find("div").show();
                }
                $("#spanCodeisNull").hide();
                $("#spanCodeErr").hide();
            });
            $("#divFristRegister .bitian").blur(function() {
                Register.ckInputisNull($(this), false);
                if ($(this).attr("id") == "ctl00_Main_txtValidateCode") {
                    Register.ckValidateCode();
                }
            });
            var isPass;
            $("#btnFristRegister").click(function() {
                $("#divFristRegister .bitian").each(function() {
                    if (!Register.ckInputisNull($(this), true)) {
                        isPass = false;
                    } else {
                        isPass = true;
                    }
                });

                isPass = Register.ckValidateCode();

                if (isPass) {
                    if (!$("#F_ckPact").attr("checked")) {
                        $("#span_PactErr").show();
                        isPass = false;
                    } else {
                        isPass = true;
                    }
                }
                //验证通过
                if (isPass) {
                    $("#labCompanyName").html($.trim($("#txtCompanyName").val()));
                    $("#RegisterHeadimg").attr("src", "<%=ImageServerPath %>/images/UserPublicCenter/join-2.gif");
                    $("#divFristRegister").hide();
                    $("#divSecondRegister").show();
                }
            });
            $("#<%=dropProvinceList.ClientID %>").change(function() {
                $("#errMsg_City").hide();
                if ($(this).val() != "") {
                    $("#errMsg_Province").hide();
                } else {
                    $("#errMsg_Province").show();
                }
            });

            $("#<%=dropCityList.ClientID %>").change(function() {
                if ($("#<%=dropProvinceList.ClientID %>").val() != "") {
                    if ($(this).val() != "") {
                        $("#errMsg_City").hide();
                    } else {
                        $("#errMsg_City").show();
                    }
                } else {
                    $("#errMsg_Province").show();
                }
            });

            $("#divSecondRegister").find("input[type='radio']").click(function() {
                $("#errMsg_radManageArea").hide();
                $("#errMsg_ckCompanyType").hide();
                var ckType = $(this).val();
                if (ckType == 0) {//旅行社
                    $("#divSecondRegister").find("input[type='checkbox'][name='ckCompanyType']").attr("disabled", "");
                } else {
                    $("#divSecondRegister").find("input[type='checkbox'][name='ckCompanyType']").attr("disabled", "disabled");
                }
            });
            $("#divSecondRegister").find("input[type='checkbox']").click(function() {
                var ckCompanyType = new Array();
                $("#divSecondRegister").find("input[type='checkbox'][name='ckCompanyType']:checked").each(function() {
                    ckCompanyType.push($(this).val());
                });
                if (ckCompanyType.length == 0) {
                    $("#errMsg_ckCompanyType").show();
                } else {
                    $("#errMsg_ckCompanyType").hide();
                }
            });

            FV_onBlur.initValid($("#btnSecondRegister").closest("form").get(0));
            $("#btnSecondRegister").click(function() {
                var isTrue = true;
                if ($("#<%=dropProvinceList.ClientID %>").val() == "") {
                    $("#errMsg_Province").show();
                    isTrue = false;
                } else {
                    $("#errMsg_Province").hide();
                    if ($("#<%=dropCityList.ClientID %>").val() == "") {
                        $("#errMsg_City").show();
                        isTrue = false;
                    } else {
                        $("#errMsg_City").hide();
                    }

                }
                var form = $(this).closest("form").get(0);
                if (ValiDatorForm.validator(form, "span")) {
                    var ckTypeId = $("#divSecondRegister").find("input[type='radio']:checked").val();
                    if (ckTypeId == 0) { //旅行社
                        var ckCompanyType = new Array();
                        $("#divSecondRegister").find("input[type='checkbox'][name='ckCompanyType']:checked").each(function() {
                            ckCompanyType.push($(this).val());
                        });
                        if (ckCompanyType.length == 0) {
                            isTrue = false;
                            $("#errMsg_ckCompanyType").show();
                        }
                    }
                } else {
                    isTrue = false;
                }
                if (isTrue) {
                    //验证通过，提交表单
                    $.ajax({
                        type: "POST",
                        url: "",
                        cache: false,
                        success: function(html) {
                            
                        }
                    });
                }
            });

        });

  
    </script>

</asp:Content>
