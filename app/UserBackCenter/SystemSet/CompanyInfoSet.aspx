<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="CompanyInfoSet.aspx.cs" Inherits="UserBackCenter.SystemSet.CompanyInfoSet" %>
<%@ Register Src="/usercontrol/SingleFileUpload.ascx" TagName="sznb2" TagPrefix="uc2" %>
<%@ Register Src="/usercontrol/szindexNavigationbar.ascx" TagName="sznb" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>
<asp:Content id="CompanyInfoSet" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<style type="text/css">
    .errmsg{
        color:#FF0000;
    }
</style>
<script type="text/javascript" src="/kindeditor/kindeditor.js" cache="true" ></script>
<script type="text/javascript">
    //初始化编辑器
    KE.init({
        id: 'cis_companyRemak', //编辑器对应文本框id
        width: '625px',
        height: '250px',
        skinsPath: '/kindeditor/skins/',
        pluginsPath: '/kindeditor/plugins/',
        scriptPath: '/kindeditor/skins/',
        resizeMode: 0, //宽高不可变
        items: keSimple //功能模式(keMore:多功能,keSimple:简易)
    });
</script>
<div class="right">
       <table cellspacing="0" cellpadding="3" bordercolor="#9dc4dc" border="1" align="center" style="margin-top:1px; width:99%;" class="liststyle">
         <tbody><tr>
           <td width="132" align="right"><%--<span class="ff0000">*</span>--%>所在区域： </td>
           <td width="412" align="left">
<%--                   省份：<asp:DropDownList id="ProvinceList" runat="server"></asp:DropDownList>
                   城市：<asp:DropDownList id="CityList" runat="server"></asp:DropDownList>
                   县区：<asp:DropDownList runat="server" id="CountyList"></asp:DropDownList>--%>
                   <span id="spanProvinceInfo" runat="server"></span>
           </td>
           <td width="335" bgcolor="#f1fafe" align="left"></td>
         </tr>
         <tr>
           <td align="right">单位名称： </td>
           <td align="left"><%=companyName%></td>
           <td bgcolor="#f1fafe" align="left"></td>
         </tr>
         <tr>
           <td nowrap="nowrap" align="right" class="bgcolor_dm_color">公司简称：</td>
           <td align="left" class="bgcolor_dm_color">
                <input type="text" id="txtSimpleName" name="txtSimpleName" valid="limit" min="0" max="6" errmsg="六个字以内！" runat="server"/>
                <span id="errMsg_<%=this.txtSimpleName.ClientID%>" class="errmsg"></span>
           </td>
           <td bgcolor="#f1fafe" align="left">六个字以内！</td>
         </tr>
         <tr>
           <td nowrap="nowrap" align="right" class="bgcolor_dm_color">公司规模：</td>
           <td align="left" class="bgcolor_dm_color">
               <asp:DropDownList runat="server" ID="ddlScale"></asp:DropDownList>
           </td>
           <td bgcolor="#f1fafe" align="left"> </td>
         </tr>
         <tr>
           <td nowrap="nowrap" align="right">许可证号： </td>
           <td align="left"><%=licenese%></td>
           <td bgcolor="#f1fafe" align="left"></td>
         </tr>
         <tr>
           <td nowrap="nowrap" align="right">管理员帐户： </td>
           <td align="left">
                <input type="text" id="txtAdminCode" name="txtAdminCode" runat="server" disabled="disabled"/>
           </td>
           <td bgcolor="#f1fafe" align="left"> </td>
         </tr>         
         <tr>
           <td align="right">联系人： </td>
           <td align="left"> 
                    <input name="cis_txtAdmin" id="cis_txtAdmin" type="text" runat="server" maxlength="20" class="bitian" size="20" valid="required"   errmsg="请填写总负责人" />
                     <span id="errMsg_<%=cis_txtAdmin.ClientID %>" class="errmsg"></span></td>
           <td bgcolor="#f1fafe" align="left">请填写您的真实姓名。</td>
         </tr>
         <tr>
           <td align="right">客服电话： </td>
           <td align="left"><input name="cis_txtTel" id="cis_txtTel" runat="server" type="text" maxlength="45" class="shurukuang" size="20" /></td>
           <td bgcolor="#f1fafe" align="left">请填写含区号的完整格式</td>
         </tr>
         <tr>
           <td align="right">客服手机： </td>
           <td align="left">                    <input name="cis_txtMobile" id="cis_txtMobile"  runat="server" type="text" maxlength="20" class="shurukuang" size="20"  valid="isMobile"  errmsg="请填写有效的手机格式" />
                    <span id="errMsg_<%=cis_txtMobile.ClientID %>" class="errmsg"></span></td>
           <td bgcolor="#f1fafe" align="left">订单信息将通过手机通知</td>
         </tr>
         <tr>
           <td align="right">客服传真： </td>
           <td align="left"><input name="cis_txtFax" id="cis_txtFax"  runat="server" maxlength="50" type="text" class="shurukuang"  size="20" /></td>
           <td bgcolor="#f1fafe" align="left">对外公开的客服传真号码</td>
         </tr>
         <tr>
           <td align="right">客服Email： </td>
           <td align="left"><input type="text" id="txtEmail" name="txtEmail" runat="server" valid="isEmail" errmsg="Email格式不正确！"/>
                <span id="errMsg_<%=this.txtEmail.ClientID%>" class="errmsg"></span>
           </td>
           <td bgcolor="#f1fafe" align="left">电子邮箱</td>
         </tr>
         <tr>
           <td align="right">公司网站： </td>
           <td align="left">http://<input type="text" id="txtWebUrl" name="txtWebUrl" runat="server" valid="custom" custom="CompanyInfoSet.checkUrl" errmsg="网址格式不正确！"/>
            <span id="errMsg_<%=this.txtWebUrl.ClientID%>" class="errmsg"></span>
           </td>
           <td bgcolor="#f1fafe" align="left">公司网站</td>
         </tr>
         <tr>
           <td align="right" class="bgcolor_dm_color">旅行社资质：</td>
           <td align="left" class="bgcolor_dm_color"><span id="spanCompetence" runat="server"></span> </td>
           <td bgcolor="#f1fafe" align="left">如需要修改，请联系客服</td>
         </tr>
                  <tr>
           <td align="right" class="bgcolor_dm_color">用户等级：</td>
           <td align="left" class="bgcolor_dm_color">
                <span id="spanUserLevel" runat="server"></span>
           </td>
           <td bgcolor="#f1fafe" align="left">用户等级</td>
         </tr>
                           <tr>
           <td align="right" class="bgcolor_dm_color">资料完整度：</td>
           <td align="left" class="bgcolor_dm_color">
                <span id="spanInfoLevel" runat="server"></span>
           </td>
           <td bgcolor="#f1fafe" align="left">资料完整度的百分比</td>
         </tr>
         <tr>
           <td align="right" class="bgcolor_dm_color">签约时间：</td>
           <td align="left" class="bgcolor_dm_color">
                有效日期从
                <input type="text" id="txtStartTime" name="txtStartTime" runat="server" disabled="disabled"  onfocus="CompanyInfoSet.checkTime('startTime')"/>
                 至
                 <input type="text" id="txtEndTime" name="txtEndTime"  runat="server" disabled="disabled"   onfocus="CompanyInfoSet.checkTime('endTime')"/>
           </td>
           <td bgcolor="#f1fafe" align="left">签约时间</td>
         </tr>    
          <tr>
           <td align="right" class="bgcolor_dm_color">业务优势：</td>
           <td align="left" class="bgcolor_dm_color">
                <textarea runat="server" id="txtOperation" name="txtOperation" disabled="disabled" cols="35" rows="2"></textarea>
           </td>
           <td bgcolor="#f1fafe" align="left">业务优势</td>
         </tr>     
         <tr>
           <td align="right">主要品牌名称： </td>
           <td align="left"><input name="txtBrandName" size="40" id="txtBrandName" type="text" runat="server" valid="limit" min="0" max="10" errmsg="十个字以内！"/>
           <span id="errMsg_<%=this.txtBrandName.ClientID%>" class="errmsg"></span>
           </td>
           <td bgcolor="#f1fafe" align="left">请输入产品品牌名称</td>
         </tr>
         <tr>
           <td align="right">品牌LOGO：</td>
           <td align="left" class="OtherPicHidden">
          <uc2:sznb2 id="uc_logo_fileUp" runat="server" ImageWidth="170" ImageHeight="50" />
                     <span class="cis_imgSize">(图片长宽比为4：3)</span>&nbsp;<%=GetLogo()%></td>
           <td bgcolor="#f1fafe" align="left"></td>
         </tr>
         <tr>
           <td align="right">办公地点： </td>
           <td align="left"><input name="cis_txtCompanyAddress" maxlength="250" runat="server" id="cis_txtCompanyAddress" type="text" class="shurukuang" size="55"  value="公司地址1"/></td>
           <td bgcolor="#f1fafe" align="left">&nbsp;</td>
         </tr>
         <tr>
           <td align="right">证书管理： </td>
           <td align="left" colspan="2"><table width="100%" cellspacing="0" cellpadding="1" bordercolor="#9dc4dc" border="1">
               <tbody>
                 <tr>
                   <td width="19%" align="right">营 业 执照： </td>
                   <td width="39%" align="left"  class="OtherPicHidden"><uc2:sznb2 id="uc_Licence_fileUp" runat="server" />
                    <span class="cis_imgSize"></span><%=GetLicence()%></td>
                   <td width="42%" align="left">扫描或者照相皆可以</td>
                 </tr>
                 <tr>
                   <td align="right">经营许可证： </td>
                   <td align="left"   class="OtherPicHidden"><uc2:sznb2 id="uc_Cert_fileUp" runat="server" />
                    <span class="cis_imgSize"></span><%=GetCer() %></td>
                   <td align="left">&nbsp;</td>
                 </tr>
                 <tr>
                   <td align="right">税务登记证： </td>
                   <td align="left"  class="OtherPicHidden"><uc2:sznb2 id="uc_Tax_fileUp" runat="server" />
                    <span class="cis_imgSize"></span><%=GetTax()%></td>
                   <td align="left">&nbsp;</td>
                 </tr>
<%--                 <tr>
                    <td align="right">公司电子章：</td>
                    <td align="left">
                        <uc2:sznb2 id="uc_Sign_fileUp" runat="server" />
                        <span class="cis_imgSize"></span><%=GetSign()%>
                    </td>
                    <td align="left">&nbsp;</td>
                  </tr>--%>
                 <tr>
                   <td align="right" class="bgcolor_dm_color">授权证书：</td>
                   <td align="left">
                           <table border="0" class="setRightPicAndIDCard">
                                <td><uc2:sznb2 id="setRightPic" runat="server" /></td>
                                <td><span id="spanSetRightPic" runat="server"></span></td>
                           </table>
                    </td>
                   <td nowrap="nowrap" align="left">专线授权书，营业部授权书</td>
                 </tr>
                 <tr>
                   <td align="right" class="bgcolor_dm_color">负责人身份证：</td>
                   <td align="left">
                          <table border="0" class="setRightPicAndIDCard">
                                <td><uc2:sznb2 id="setIDCard" runat="server" /></td>
                                <td><span id="spanSetIDCard" runat="server"></span></td>
                           </table>
                    </td>
                   <td nowrap="nowrap" align="left">&nbsp;</td>
                 </tr>
               </tbody>
           </table></td>
         </tr>
         <tr>
           <td align="right">公司介绍： </td>
           <td align="left"><textarea id="cis_companyRemak"  name="cis_CompanyDetail" style="width:600px;height:250px;"></textarea>
            <textarea id="cis_companyRemak2" style=" display:none;"><%=companyDetail%></textarea>
           </td>
           <td align="left"></td>
         </tr>
         <tr>
           <td align="right" rowspan="3">公司照片：</td>
           <td align="left">
                  <table border="0" class="tableCompanyPic">
                        <tr>
                               <td><uc2:sznb2 id="CompanyPic1" runat="server" /></td>
                               <td align="left"><asp:Literal runat="server" ID="LtComPic1"></asp:Literal></td>
                        </tr>
                  </table>
            </td>
           <td align="left" rowspan="3">可以是门面、办公场景、前台等(建议图片尺寸：1024*768)</td>
         </tr>
         <tr>
           <td align="left">
                  <table border="0" class="tableCompanyUploadPic">
                        <tr>
                               <td><uc2:sznb2 id="CompanyPic2" runat="server" /></td>
                               <td align="left"><asp:Literal runat="server" ID="LtComPic2"></asp:Literal></td>
                        </tr>
                  </table>
           </td>
         </tr>
         <tr>
           <td align="left">
                  <table border="0" class="tableCompanyPic">
                        <tr>
                               <td><uc2:sznb2 id="CompanyPic3" runat="server" /></td>
                               <td align="left"><asp:Literal runat="server" ID="LtComPic3"></asp:Literal></td>
                        </tr>
                  </table>
          </td>
         </tr>
         <tr>
           <td align="right">电子地图位置：</td>
           <td align="left" colspan="2">
            <input type="button" value="地图标注" name="txtMapBtn" id="txtMapBtn" onclick="CompanyInfoSet.setMap(this);"/>
            <span id="spanMapXY" runat="server"></span>
            <input type="hidden" runat="server" id="hiddenMapXY" />
           </td>
         </tr>
         <tr>
           <td align="right">销售城市：</td>
           <td align="left" colspan="2"><span id="txtSaleCity" runat="server"></span></td>
         </tr>
         <tr class="bgcolor_dm_color">
           <td align="right"><font class="ff0000">同业联系方式</font>：
           &nbsp;</td>
           <td align="left"><textarea name="txtTongYeContact" runat="server" cols="60" rows="4" id="txtTongYeContact" valid="limit" min="0" max="500" errmsg="500字以内！"></textarea>
           <span id="errMsg_<%=this.txtTongYeContact.ClientID%>"></span>
           </td>
           <td align="left"><p>用于同业间的联系，避免散客骚扰，可以把计调和专线负责人资料写入。</p></td>
         </tr>
         <tr>
           <td align="right">经营范围：</td>
           <td align="left" colspan="2">
           
                    <table style="width:100%;border-bottom:dashed 1px #999;">
                            <tr>
                                    <td align="middle" width="8%" bgcolor="#D1E7F4">国内长线</td>
                                    <td onmouseover="CompanyInfoSet.mouseovertr(this)" onmouseout="CompanyInfoSet.mouseouttr(this)">
                                        <ul>
                                               <asp:CustomRepeater id="pis_rpt_LongAreaList" runat="server"  >
                                                        <ItemTemplate>
                                                                <li style="width:20%;float:left;">
                                                                    <input id="ckLongArea<%#Container.ItemIndex%>"   name="ckLongArea"  disabled="disabled"  type="checkbox" <%#GetChecked(Convert.ToInt32(Eval("AreaId"))) %> value="<%#Eval("AreaId")%>"/>
                                                                    <label for="ckLongArea<%#Container.ItemIndex%>"><%# Eval("areaName") %></label>
                                                                </li>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <%
                                                                if (this.pis_rpt_LongAreaList.Items.Count == 0)
                                                                {
                                                                    Response.Write("<li>暂无信息！</li>");
                                                                }
                                                                 %>
                                                       </FooterTemplate>
                                                </asp:CustomRepeater>
                                        </ul>
                                    </td>
                            </tr>
                      </table>
                      <table style="width:100%;border-bottom:dashed 1px #999;">
                          <tr>
                                    <td align="middle" width="8%" bgcolor="#D1E7F4">国际线</td>
                                    <td onmouseover="CompanyInfoSet.mouseovertr(this)" onmouseout="CompanyInfoSet.mouseouttr(this)">
                                        <ul>
                                               <asp:CustomRepeater id="pis_rpt_ExitAreaList" runat="server"  >
                                                        <ItemTemplate>
                                                                <li style="width:20%;float:left;">
                                                                    <input id="ckExitAreaList<%#Container.ItemIndex%>"   name="ckExitAreaList"  disabled="disabled"  type="checkbox" <%#GetChecked(Convert.ToInt32(Eval("AreaId"))) %> value="<%#Eval("AreaId")%>"/>
                                                                    <label for="ckExitAreaList<%#Container.ItemIndex%>"><%# Eval("areaName") %></label>
                                                                </li>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <%
                                                                if (this.pis_rpt_ExitAreaList.Items.Count == 0)
                                                                {
                                                                    Response.Write("<li>暂无信息！</li>");
                                                                }
                                                                 %>
                                                       </FooterTemplate>
                                                </asp:CustomRepeater>
                                        </ul>
                                    </td>
                            </tr>
                       </table>
                       <table style="width:100%;">
                           <tr>
                                    <td align="middle" width="8%" bgcolor="#D1E7F4">国内短线</td>
                                    <td onmouseover="CompanyInfoSet.mouseovertr(this)" onmouseout="CompanyInfoSet.mouseouttr(this)">
                                        <ul>
                                               <asp:CustomRepeater id="pis_rpt_ShortAreaList" runat="server"  >
                                                        <ItemTemplate>
                                                                <li style="width:20%;float:left;">
                                                                    <input id="ShortAreaList<%#Container.ItemIndex%>"   name="ShortAreaList"  disabled="disabled"  type="checkbox" <%#GetChecked(Convert.ToInt32(Eval("AreaId"))) %> value="<%#Eval("AreaId")%>"/>
                                                                    <label for="ShortAreaList<%#Container.ItemIndex%>"><%# Eval("areaName") %></label>
                                                                </li>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <%
                                                                if (this.pis_rpt_ShortAreaList.Items.Count == 0)
                                                                {
                                                                    Response.Write("<li>暂无信息！</li>");
                                                                }
                                                                 %>
                                                       </FooterTemplate>                                                        
                                                </asp:CustomRepeater>
                                        </ul>
                                    </td>                        
                           </tr>
                      </table>
                
           </td>
         </tr>
         <tr id="trAreas" runat="server">
           <td align="right">经营区域： </td>
           <td align="left" colspan="2">
                <span id="spanAreas" runat="server"></span>
           </td>
         </tr>
          <tr>
           <td align="right" class="bgcolor_dm_color">支付宝账户：</td>
           <td align="left" class="bgcolor_dm_color">
                <input type="text" runat="server" id="txtAliPay" name="txtAliPay"/>
           </td>
           <td bgcolor="#f1fafe" align="left">用于网站支付</td>
         </tr>
         <tr id="tr_CompanyBank">
           <td nowrap="nowrap" align="right">公司银行账户： </td>
         <td align="left" colspan="2"><table>
               <tbody>
                 <tr>
                   <td>公司全称
                     <input name="cis_Company1" maxlength="100" id="cis_Company1" runat="server" type="text" class="shurukuang" size="20" value="" /></td>
                   <td>开户行
                     <input name="cis_CompanyBank1" maxlength="100" id="cis_CompanyBank1" runat="server" type="text" class="shurukuang" size="20" value="" /></td>
                   <td>帐号
                     <input name="cis_CompanyAccount1" maxlength="250" id="cis_CompanyAccount1" runat="server" type="text" class="shurukuang" size="20" /></td>
                   <td></td>
                 </tr>
               </tbody>
           </table></td>
         </tr>
         <asp:Repeater id="cis_rpt_personBank" runat="server"  onitemdatabound="cis_rpt_personBank_ItemDataBound" >
               <ItemTemplate>
                         <tr id="tr_CompanyUserBank">
                           <td align="right">个人银行账户： </td>
                           <td align="left" colspan="2">
                                <table id="tbBankAdd">
                                    <tbody>
                                     <tr>
                                       <td>户&nbsp; 名
                                         <input name="cis_PeosonName<%#GetPersonBankNo() %>" type="text" maxlength="50"  class="shurukuang" size="20"  value='<%#Eval("BankAccountName") %>' />
                                       </td>
                                       <td>开户行
                                        <input name="cis_PeosonBank<%#GetPersonBankNo() %>" type="text"  maxlength="50"  class="shurukuang" size="20" value='<%#Eval("BankName") %>' />
                                       </td>
                                       <td>帐号
                                         <input name="cis_PeosonAccount<%#GetPersonBankNo() %>" type="text" maxlength="50"  class="shurukuang" size="20"  value='<%#Eval("AccountNumber") %>'/>
                                       </td>
                                       <td>
                                            <input name="cis_btnAddPerson" type="button" style="width:100px;" id="cis_btnAddPerson" value="删除个人账户"  onclick="CompanyInfoSet.personDel(this)"/>
                                       </td>
                                     </tr>
                                   </tbody>
                                </table>
                           </td>
                         </tr>
                 </ItemTemplate>
           </asp:Repeater>
         <tr>
            <td align="right" nowrap="nowrap">个人银行账户：</td>
            <td colspan="2" align="left">
                    户&nbsp;&nbsp;名
                    <input name="cis_PeosonName0" type="text" class="shurukuang" maxlength="50" size="20" />
                    开户行
                    <input name="cis_PeosonBank0" type="text" class="shurukuang" maxlength="50" size="20" />
                    帐号
                    <input name="cis_PeosonAccount0" type="text" class="shurukuang" maxlength="50" size="20" />
                    <input name="cis_btnAddPerson" type="button" value="添加个人账户" style="width:100px;"  onclick="CompanyInfoSet.personAdd(this)"/>
             </td>
          </tr>
         <tr>
           <td align="center" colspan="3"> <a href="javascript:void(0)" id="cis_aSave"  class="baocun_btn" onclick="return CompanyInfoSet.save()" >保 存</a></td>
         </tr>
       </tbody></table>
     </div>
<%--     <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("GetCityList") %>" ></script>--%>
        <script type="text/javascript">
     var CompanyInfoSet=
    { 
            mouseovertr: function(o) {
            o.style.backgroundColor = "#FFF9E7";
        },
        mouseouttr: function(o) {
            o.style.backgroundColor = "";
        },
      setOptions:function(obj,value){
        $ops=$(obj).find("option");
        for(var i=0;i<$ops.length;i++)
        {
            if($($ops[i]).attr("value")==value)
            {
                $($ops[i]).attr("selected",true);
                break;
            }
        }
      },
        InitPlace:function(provinceID,cityID,countryID){
                var objProvince=document.getElementById("<%--this.ProvinceList.ClientID--%>");
                var objCity=document.getElementById("<%--this.CityList.ClientID--%>");
                var objCountry=document.getElementById("<%--this.CountyList.ClientID--%>");
                if(provinceID!="")
                {
                    CompanyInfoSet.setOptions(objProvince, provinceID);
                    SetList("<%--this.CityList.ClientID--%>", $(objProvince).val(), cityID);
                    if(cityID!="")
                    {
                        SetList('<%--this.CountyList.ClientID--%>', 1, $(objCity).val(), countryID);
                    }
                }
      },
      logo_fileUp:<%=uc_logo_fileUp.ClientID %>,
      Licence_fileUp:<%=uc_Licence_fileUp.ClientID %>,
      Cert_fileUp:<%=uc_Cert_fileUp.ClientID %>,
      Tax_fileUp:<%=uc_Tax_fileUp.ClientID %>,
      //Sign_fileUp:uc_Sign_fileUp.ClientID,
      //公司的三张图片
      CompanyPic1:<%=this.CompanyPic1.ClientID%>,
      CompanyPic2:<%=this.CompanyPic2.ClientID%>,
      CompanyPic3:<%=this.CompanyPic3.ClientID%>,
      //授权证书
      setRightPic:<%=this.setRightPic.ClientID%>,
      //负责人身份证
      setIDCard:<%=this.setIDCard.ClientID%>,
      mouseovertr:function(o){
          o.style.backgroundColor="#FFF9E7";
      },
      mouseouttr:function(o){
         o.style.backgroundColor="";
      },
      checkUrl:function(obj)
      {
        var v=$.trim(obj.value);
        if(v=="")return true;
        if(/^http[s]?:\/\/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/.test("http://"+v))
            return true;
        else
            return false;
      },
     //添加个人账户
     personAdd:function(tar_a){
         var id=1;
         var lastPersonBank=$(tar_a).closest("tr").prev("tr").find("input[name*='cis_PeosonName']");
       
         if(lastPersonBank.length>0)
         {
          id=parseInt(lastPersonBank.attr("name").match(/\d*$/g)[0])+1;//产生控件编号
         }
         var personName=$(tar_a).siblings("input[name*='cis_PeosonName']");
         var personBank=$(tar_a).siblings("input[name*='cis_PeosonBank']");
         var personAccount=$(tar_a).siblings("input[name*='cis_PeosonAccount']");
        
         $(tar_a).closest("tr").before("<tr><td align=\"right\">个人银行账户：</td><td colspan=\"2\" align=\"left\">户&nbsp;&nbsp;名 "+
                      "<input name=\"cis_PeosonName"+id+"\" type=\"text\" class=\"shurukuang\" maxlength=\"50\" size=\"20\" value='"+personName.val()+"' /> 开户行 "+
                      "<input name=\"cis_PeosonBank"+id+"\" type=\"text\" class=\"shurukuang\" maxlength=\"50\" size=\"20\" value='"+personBank.val()+"'/> 帐号 "+
                      "<input name=\"cis_PeosonAccount"+id+"\" type=\"text\" class=\"shurukuang\" maxlength=\"50\" size=\"20\" value='"+personAccount.val()+"'/>"+
                      " <input name=\"cis_btnAddPerson\" type=\"button\" id=\"cis_btnAddPerson\" style=\"width:100px;\" value=\"删除个人账户\"  onclick=\"CompanyInfoSet.personDel(this)\"/></tr>");
       personName.val('');
       personBank.val('');
       personAccount.val('');
     },
     checkTime:function(type)
     {
        if(type=="startTime")
        {
            WdatePicker({maxDate:'#F{$dp.$D(\'<%=this.txtEndTime.ClientID%>\')}'});
        }  
        else
        {
            WdatePicker({minDate:'#F{$dp.$D(\'<%=this.txtStartTime.ClientID%>\')}'});
        }
     },
     //删除个人账户
     personDel:function(tar_a){//动态添加的tr没有id。
        $trObj=$(tar_a).closest("tr#tr_CompanyUserBank");
        if($trObj.length==0)
        {
            $(tar_a).closest("tr").remove();
        }
        else
        {
            $trObj.remove();
        }
     },
     //上传营业执照
     Licence_fileUp1:function(){
        if(CompanyInfoSet.Licence_fileUp.getStats().files_queued>0)
	     { 
            CompanyInfoSet.Licence_fileUp.customSettings.UploadSucessCallback = CompanyInfoSet.Cert_fileUp1;
            CompanyInfoSet.Licence_fileUp.startUpload();
         }
        else
         {
             CompanyInfoSet.Cert_fileUp1();
         }
     },
     //上传经营许可证
     Cert_fileUp1:function(){
         if(CompanyInfoSet.Cert_fileUp.getStats().files_queued>0)
	     { 
            CompanyInfoSet.Cert_fileUp.customSettings.UploadSucessCallback = CompanyInfoSet.Tax_fileUp1;
            CompanyInfoSet.Cert_fileUp.startUpload();
         }
        else
         {
            CompanyInfoSet.Tax_fileUp1();
         }
      },
     //上传税务登记证
     Tax_fileUp1:function(){
         if(CompanyInfoSet.Tax_fileUp.getStats().files_queued>0)
	     {  
            CompanyInfoSet.Tax_fileUp.customSettings.UploadSucessCallback = CompanyInfoSet.setRightPicUpload;
            CompanyInfoSet.Tax_fileUp.startUpload();
         }
         else
         {
             CompanyInfoSet.setRightPicUpload();
         }
      },
//      //上传公司电子章
//      Sign_fileUp1:function(){
//       if(CompanyInfoSet.Sign_fileUp1.getStats().files_queued>0)
//	     {  
//            CompanyInfoSet.Sign_fileUp1.customSettings.UploadSucessCallback = CompanyInfoSet.setRightPicUpload;
//            CompanyInfoSet.Sign_fileUp1.startUpload();
//         }
//         else
//         {
//            CompanyInfoSet.setRightPicUpload();
//         }
//      },
      //上传授权证书
      setRightPicUpload:function(){
         if(CompanyInfoSet.setRightPic.getStats().files_queued>0)
	     {  
            CompanyInfoSet.setRightPic.customSettings.UploadSucessCallback = CompanyInfoSet.setIDCardUpload;
            CompanyInfoSet.setRightPic.startUpload();
         }
         else
         {
            CompanyInfoSet.setIDCardUpload();
         }
      },
      //上传负责人身份证
      setIDCardUpload:function(){
         if(CompanyInfoSet.setIDCard.getStats().files_queued>0)
	     {  
            CompanyInfoSet.setIDCard.customSettings.UploadSucessCallback = CompanyInfoSet.uploadCompanyPic1;
            CompanyInfoSet.setIDCard.startUpload();
         }
         else
         {
            CompanyInfoSet.uploadCompanyPic1();
         }
      },
      //上传公司图片开始
      uploadCompanyPic1:function(){
         if(CompanyInfoSet.CompanyPic1.getStats().files_queued>0)
	     {  
            CompanyInfoSet.CompanyPic1.customSettings.UploadSucessCallback = CompanyInfoSet.uploadCompanyPic2;
            CompanyInfoSet.CompanyPic1.startUpload();
         }
         else
         {
             CompanyInfoSet.uploadCompanyPic2();
         }
      },
     uploadCompanyPic2:function(){
         if(CompanyInfoSet.CompanyPic2.getStats().files_queued>0)
	     {  
            CompanyInfoSet.CompanyPic2.customSettings.UploadSucessCallback = CompanyInfoSet.uploadCompanyPic3;
            CompanyInfoSet.CompanyPic2.startUpload();
         }
         else
         {
             CompanyInfoSet.uploadCompanyPic3();
         }
      },
     uploadCompanyPic3:function(){
         if(CompanyInfoSet.CompanyPic3.getStats().files_queued>0)
	     {  
            CompanyInfoSet.CompanyPic3.customSettings.UploadSucessCallback = CompanyInfoSet.save2;
            CompanyInfoSet.CompanyPic3.startUpload();
         }
         else
         {
            CompanyInfoSet.save2();
         }
      },
      //上传公司图片结束
      //编辑公司图片时，删除图片操作
      delFile:function(obj)
      {
            if(!confirm("您确定要删除吗？"))return;
            $(obj).closest("td").prev("td").find("input[name$='hidFileName']").val("");
            $(obj).hide();
            $(obj).prev("a").hide();
      },
      //编辑时初始化公司图片的hidden
      setCompPic:function(index,val)
      {
        $tables=$("table.tableCompanyUploadPic");
        $($tables[index]).find("input[name$='hidFileName']").val("");
      },
      //编辑时初始化授 权证书和负责人身份证hidden
      setRightPicAndIDCard:function(index,val)
      {
         $cons=$("table.setRightPicAndIDCard");
         $($cons[index]).find("input[name$='hidFileName']").val("");
      },
      //编辑时初始化logo,营 业 执照,经营许可证,税务登记证的hidden
      setOtherPicHidden:function(index,val)
      {
         $cons=$("table.OtherPicHidden");
         $($cons[index]).find("input[name$='hidFileName']").val("");
      },
     //保存公司信息前的上传操作
     save:function(){
         if("<%=isCompanyCheck %>"=="False")
         {
           alert("对不起，你还未通过审核，不能进行操作！")
           return false;
         }
         if("<%=haveUpdate %>"=="False")
         {
           alert("对不起，你没有该权限!");
           return false;
         }
         var form = $("#cis_aSave").closest("form").get(0);
		 if(ValiDatorForm.validator(form,"span"))
		 {  
		     $("#cis_aSave").css("color","#aaa").css("cursor","default").get(0).onclick=null;
		     $("#cis_clear").after("<span style='margin-top:10px;float:left;margin-left:10px;color:green'>正在保存中……</span>");
		     if(CompanyInfoSet.logo_fileUp.getStats().files_queued>0)
		     {
                CompanyInfoSet.logo_fileUp.customSettings.UploadSucessCallback = CompanyInfoSet.Licence_fileUp1;
                CompanyInfoSet.logo_fileUp.startUpload();
             }
             else
             {
               CompanyInfoSet.Licence_fileUp1();
             }
          }
		 return false;
     },
     //保存公司信息并提交
     save2:function(){
          var form = $("#cis_aSave").closest("form").get(0);
          $("#cis_companyRemak").val(KE.html('cis_companyRemak'));//获取编辑器内容并赋值到文本框
         
          $.newAjax(
	       { 
	         url:"/SystemSet/CompanyInfoSet.aspx",
	         data:$(form).serialize()+"&method=save",
             dataType:"json",
             cache:false,
             type:"post",
             success:function(result){ 
                 
                 alert(result.message);
                  $("#cis_aSave").css("color","#000").css("cursor","pointer").click(CompanyInfoSet.save);
                  $("#cis_clear").next("span").remove();
                 if(result.success=="1")
                 {
                  var imagePath=$("input[name='ctl00$ContentPlaceHolder1$uc_logo_fileUp$hidFileName']").val();
                  if(imagePath!="")
                  { 
                      DefaultFunction.SetLogoURL("<%=EyouSoft.Common.Domain.FileSystem %>"+imagePath);
                  }
                  else
                  {
                      DefaultFunction.SetLogoURL($("#cis_logo_url").attr("href"));
                  }
                  topTab.url(topTab.activeTabIndex,"/SystemSet/CompanyInfoSet.aspx?method=updatelogo");
                  return false;
                 }
                   
             },
             error:function(){ 
                 $("#cis_aSave").css("color","#000").css("cursor","pointer").click(CompanyInfoSet.save);
                $("#cis_clear").next("span").remove();
               alert("操作失败!");
             }
	       });
      },
     //重置
     clear:function(tar_a){
        $("#cis_aSave").closest("form").get(0).reset();
        return true;
     },
     setMap:function(obj){
           var x = 0, y = 0, xy = [];
           xy = $("#<%=this.hiddenMapXY.ClientID%>").val().split(",");
           if (xy.length == 2) {
               x = parseFloat(xy[0]); //经度
               y = parseFloat(xy[1]); //纬度
           }
            var url="/SystemSet/GetMapXY.aspx?x="+x+"&y="+y+"&callBackFunction=getMapXY";
            parent.Boxy.iframeDialog({ title: "地图标注", iframeUrl: url, height: 410, width: 560, draggable: false });
            return false;
     }
  }
    function getMapXY(x,y){
        $("#<%=this.hiddenMapXY.ClientID%>").val(x+","+y);
        $("#<%=this.spanMapXY.ClientID%>").hide();
     }
      $(document).ready(function()
     {
          FV_onBlur.initValid($("#cis_aSave").closest("form").get(0));
          $(".cis_imgSize").prev("div").css("float","left");
          
          CompanyInfoSet.logo_fileUp=<%=uc_logo_fileUp.ClientID %>;
          CompanyInfoSet.Licence_fileUp=<%=uc_Licence_fileUp.ClientID %>;
          CompanyInfoSet.Cert_fileUp=<%=uc_Cert_fileUp.ClientID %>;
          CompanyInfoSet.Tax_fileUp=<%=uc_Tax_fileUp.ClientID %>;
          //CompanyInfoSet.Sign_fileUp=uc_Sign_fileUp.ClientID;
          setTimeout(
            function(){
                      KE.create('cis_companyRemak',0);//创建编辑器
                     KE.html('cis_companyRemak',htmlDecode($("#cis_companyRemak2").html())); //赋值
            },100);
     });
    </script>
</asp:Content>