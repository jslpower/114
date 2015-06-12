<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="UserBackCenter.GeneralShop.TongYeNews.NewsList" ValidateRequest="false" %>
<%@ Register Src="/usercontrol/SingleFileUpload.ascx" TagName="sznb2" TagPrefix="uc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Src="/usercontrol/DocFileUpload.ascx" TagName="DocFileUpload" TagPrefix="ucDoc" %>
<asp:content id="ConNewsList" runat="server" contentplaceholderid="ContentPlaceHolder1">
<script type="text/javascript">
    commonTourModuleData.add({
        ContainerID: '<%=tblID %>',
        ReleaseType: 'NewsList'
    });
</script>
<script type="text/javascript" src="/kindeditor/kindeditor.js" cache="true"></script>
<style type="text/css">
    table .odd	{ background:#F3F7FF;}
    table tr.highlight{ background:#FFF3BF;}
    table tr.selected{ background:#FFF3E7;color:#fff;}
</style>
<script type="text/javascript">
    //初始化编辑器
    KE.init({
        id: "<%=this.txtContent.ClientID%>", //编辑器对应文本框id
        width: '750px',
        height: '200px',
        skinsPath: '/kindeditor/skins/',
        pluginsPath: '/kindeditor/plugins/',
        scriptPath: '/kindeditor/skins/',
        resizeMode: 0, //宽高不可变
        items: keSimple//功能模式(keMore:多功能,keSimple:简易)
    });
</script>
<div class="tablebox">
         <!--列表-->
		 <table  class="<%=tblID %>"  cellspacing="0" cellpadding="0" border="0" align="center" style="width:100%;">
               <tbody><tr style="background:url(<%=ImageServerUrl %>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
                 <td height="30" align="left"><span class="search">&nbsp;</span>关键字(标题)
                   <input type="text" style="width:180px;" size="50" name="txtSearchTitle" id="txtSearchTitle" onkeypress="return NewsList.isEnter(event);">
                    <a href="javascript:void(0);" onclick="NewsList.goSearch();">
                        <img width="62" height="21" style="margin-bottom:-4px;border:0px;" src="<%=ImageServerUrl %>/images/chaxun.gif">
                    </a>
                 </td>
               </tr>
             </tbody>
         </table>
        <table class="<%=tblID %>" cellspacing="0"  cellpadding="1" bordercolor="#9dc4dc" border="1" align="center"  class="liststyle" style="width:100%; margin-top:1px;">
               <tbody><tr class="list_basicbg">
                 <th width="7%" nowrap="nowrap" align="center">分类</th>
                 <th width="64%" nowrap="nowrap" align="center">标题</th>
                 <th width="17%" nowrap="nowrap" align="center"> 发布时间</th>
                 <th width="12%" nowrap="nowrap" align="center"> 操作</th>
               </tr>
               <cc1:CustomRepeater ID="RepList" runat="server">
                    <ItemTemplate>
                       <tr>
                         <td align="center"><%#Eval("TypeId")%></td>
                         <td align="left">

                         <%#this.getInfoAboutHref(Eval("AreaName"), Eval("AreaId"), Eval("ScenicId"), Eval("CompanyId"))%>
                         <a href="javascript:void(0);" onclick="return topTab.url(topTab.activeTabIndex,'/TongYeInfo/InfoShow.aspx?infoId=<%#Eval("NewId")%>&pageFrom=shop')"><%#Eval("Title")%></a>
                         </td>
                         <td align="center"><%#Eval("IssueTime","{0:yyyy-MM-dd}")%></td>
                         <td align="center">
                            <a href="javascript:void(0);" onclick="NewsList.edit('<%#Eval("NewId")%>');"  class="basic_btn"><span>修改</span></a>
                            <a href="javascript:void(0);" onclick="NewsList.del('<%#Eval("NewId")%>');" class="zhuangtai_btn"><span>删除</span></a>
                         </td>
                       </tr>
                    </ItemTemplate>
               </cc1:CustomRepeater>
             </tbody>
         </table>
         <!--翻页-->
         <table  class="<%=tblID %>" width="98%" cellspacing="0" cellpadding="4" border="0" align="center">
               <tbody><tr id="tablePageTr">
                 <td align="right" class="F2Back"> 
                        <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4" runat="server"></cc2:ExportPageInfo>
                  </td>
               </tr>
             </tbody>
         </table>
         <table  class="<%=tblID %>" cellspacing="0" cellpadding="3" bordercolor="#9dc4dc" border="1" align="center" class="liststyle" style="width:100%; margin-top:1px;">
           <tbody><tr>
             <td width="10%" bgcolor="#CCE8F8" align="right"><span style="color:#f00;">*</span>标 题：</td>
             <td width="90%" align="left">
                <input style="width:350px" type="text" name="txtTitle" id="txtTitle" valid="required|limit" min="1" max="100" errmsg="标题不能为空！|标题在100字内！" runat="server"/>
                <span id="errMsg_<%=this.txtTitle.ClientID%>" class="errmsg"></span>(本标题对外公开，请不要放结算价和返佣金额)
             </td>
           </tr>
           <%--相关专线、酒店、景区--%>
           <asp:Literal runat="server" ID="ltSelectTypeInfo"></asp:Literal>
<%--           <tr id="tableTr" runat="server">
             <td bgcolor="#CCE8F8" align="right"><asp:Literal runat="server" id="ltTypeName"></asp:Literal>：</td>
             <td align="left">
                      <input type="text" runat="server" id="txtTypeInfo" name="txtTypeInfo"/>
             </td>
           </tr>--%>
           <tr>
             <td bgcolor="#CCE8F8" align="right">分 类：</td>
             <td align="left">
                      <asp:DropDownList runat="server" id="ddlType"></asp:DropDownList>
                 </td>
           </tr>
           <tr>
             <td bgcolor="#CCE8F8" align="right"><span style="color:#f00;">*</span>内 容：</td>
             <td align="left">
               <textarea cols="30" rows="5" style="width:750px;height:200px;" name="txtContent" id="txtContent" errmsg="内容不能为空！" runat="server"></textarea>
                <span id="errMsg_<%=this.txtContent.ClientID%>" class="errmsg"></span>
             </td>
           </tr>
           <tr>
             <td bgcolor="#CCE8F8" align="right">图 片：</td>
             <td align="left">
                <uc2:sznb2 id="filePic" runat="server"/>（仅限2M以内的[jpg,jpeg,gif,png]格式的文件）
                <asp:Label runat="server" ID="lbPic"></asp:Label>
            </td>
           </tr>
           <tr>
             <td bgcolor="#CCE8F8" align="right">附 件：</td>
             <td align="left">
                <ucDoc:DocFileUpload id="files" runat="server"/>（仅限2M以内的[xls,rar,pdf,doc,swf]格式的文件）
                <asp:Label runat="server" ID="lbFile"></asp:Label>
             </td>
           </tr>
           <tr>
             <td align="center" colspan="2"><input type="button" class="baocun_btn" id="btnSubmit" value="发 布" style="border:0px;cursor:pointer;"/></td>
           </tr>
         </tbody></table>
		 
		 <table width="100%" border="0">
  <tbody><tr>
    <td align="left"><strong>专线商同业通告有什么用？</strong></td>
  </tr>
  <tr>
    <td align="left"><strong>答</strong>：本信息将在组团社登录时在显要位置显示，同时也会在您的站内网店有栏目显示通告标题（内容需要登陆后才能查看）。如专线商有特价和促销信息欢迎发布，不过注意不要把同行价写在标题里面哦！</td>
  </tr>
</tbody></table>
       </div>
       <script type="text/javascript">
           var NewsList = {
                //判断是否按回车
                 isEnter:function(event){
                 event=event?event:window.event;
                 if(event.keyCode==13)
                 {
                 NewsList.goSearch();
                  return false;
                 }
               },
               filePic: <%=this.filePic.ClientID%>,//图片
               files: <%=this.files.ClientID%>,//附件
               //图片上传
               filePicUpload: function() {
                   if (NewsList.filePic.getStats().files_queued > 0) {
                       NewsList.filePic.startUpload();
                       NewsList.filePic.uploadError=function(){alert("图片上传失败！");};
                       NewsList.filePic.customSettings.UploadSucessCallback = NewsList.filesUpload;
                   }
                   else
                   {
                        NewsList.filesUpload();
                   }
               },
               //附件上传
               filesUpload: function() {
                   if (NewsList.files.getStats().files_queued > 0) {
                       NewsList.files.startUpload();
                       NewsList.files.uploadError=function(){"附件上传失败！"};
                        NewsList.files.customSettings.UploadSucessCallback =function(){
                             NewsList.save();
                        };
                   }
                   else
                   {
                        NewsList.save();
                   }
               },
               _getData: function() {
                   return commonTourModuleData.get('<%=this.tblID %>');
               },
               //单击列表上的搜索
               goSearch: function() {
                   $conObj = $("." + NewsList._getData().ContainerID);
                   var kw = $.trim($conObj.find("#txtSearchTitle").val());
                   var searchUrl = "/GeneralShop/TongYeNews/NewsList.aspx?kw=" + encodeURIComponent(kw);
                   topTab.url(topTab.activeTabIndex, searchUrl);
                   return false;
               },
               //单击“相关资讯”搜索
               setKeyWord: function(kw) {
                   $conObj = $("." + NewsList._getData().ContainerID);
                   $conObj.find("#txtSearchTitle").val(kw);
                   NewsList.goSearch();
               },
               //删除
               del: function(ids) {
                   if (confirm("您确定要删除该条记录吗？")) {
                       $.newAjax({
                           url: "/GeneralShop/TongYeNews/NewsList.aspx?type=del&method=ajax",
                           data: { id: ids },
                           type: "GET",
                           success: function(data) {
                               if(data!="")
                               {
                                    alert(data);
                                    topTab.url(topTab.activeTabIndex,"/GeneralShop/TongYeNews/NewsList.aspx?kw="+'<%=Request.QueryString["kw"]%>'+"&v="+Math.random());
                               }
                           },
                           error: function() {
                               alert("操作失败，服务器繁忙！");
                           }
                       });
                   }
                   return false;
               },
               //编辑时删除图片或附件
               delFile:function(obj,type)
               {
                  if(!confirm("您确定要删除此文件吗？"))
                    return;
                  $conObj = $("." + NewsList._getData().ContainerID);
                  switch(type)
                  {
                     case "pic":
                        $("input[name='<%=this.filePic.UniqueID%>$hidFileName']").val("");
                     break;
                     case "file":
                        $("input[name='<%=this.files.UniqueID%>$hidFileName']").val("");
                     break;
                  }
                  $(obj).hide();
                  $(obj).prev("a").hide();
                  return false;
               },
               //修改
               edit: function(id) {
                   topTab.url(topTab.activeTabIndex,"/GeneralShop/TongYeNews/NewsList.aspx?id="+id+"&kw="+'<%=Request.QueryString["kw"]%>');
                   return false;
               },
               //编辑时设置上传隐藏域的value
               setUploadHiddenValue:function(value,type)
               {
                    $conObj = $("." + NewsList._getData().ContainerID);
                    switch(type)
                    {
                        case "pic":
                            $("input[name='<%=this.filePic.UniqueID%>$hidFileName']").val(value);
                        break;
                        case "file":
                            $("input[name='<%=this.files.UniqueID%>$hidFileName']").val(value);
                        break;
                    }
               },
               //初始化编辑器
               initEditor: function(obj) {
                   KE.create($(obj).attr("id")); //创建编辑器
               },
               //提交表单（当文件上传成功后提交）
               save:function(){
                       $conObj = $("." + NewsList._getData().ContainerID);
                       $selObj=$conObj.find("select#selectInfo").find("option:selected");
                       if($selObj.val()!="-1")
                       {
                           $conObj.find("select#selectInfo").next("input[name='selectInfoName']").val($selObj.text());
                       }
                       $saveBtn=$conObj.find("#btnSubmit");
                       var url='/GeneralShop/TongYeNews/NewsList.aspx?type=save&method=ajax&id=<%=Request.QueryString["id"]%>&kw=<%=Request.QueryString["kw"]%>';
                       //提交表单
                       $.newAjax({
                           type: "POST",
                           url: url+"&v="+Math.random(),
                           data: $conObj.find("#btnSubmit").closest("form").serialize(),
                           success: function(data) {
                               $saveBtn.val("发 布").removeAttr("disabled");
                               if(data!=""){
                                alert(data);
                                topTab.url(topTab.activeTabIndex,"/GeneralShop/TongYeNews/NewsList.aspx?kw="+'<%=Request.QueryString["kw"]%>'+"&v="+Math.random());
                                return;
                               }
                           },
                           beforeSend:function(){
                               $saveBtn.val("提交中...").attr({disabled:"disabled"});
                           },
                           error: function() {
                               alert("服务器繁忙!请稍候再进行此操作!");
                               return false;
                           }
                       });
                       return false;
                   }
               };
           $(function() {
               $conObj = $("." + NewsList._getData().ContainerID);
               //搜索框初始化
               $conObj.find("#txtSearchTitle").val('<%=Server.UrlDecode(Request.QueryString["kw"])%>');
               FV_onBlur.initValid($conObj.find("#btnSubmit").closest("form").get(0));
               $conObj.find("#btnSubmit").click(function() {
                   KE.util.setData("<%=this.txtContent.ClientID%>");
                   if ($conObj.find("#" + '<%=this.txtContent.ClientID%>').html() == "") {
                       $conObj.find("#" + '<%=this.txtContent.ClientID%>').attr("valid", "required");
                   }
                   else {
                       $conObj.find("#" + '<%=this.txtContent.ClientID%>').removeAttr("valid");
                   }
                   var validatorshow = ValiDatorForm.validator($conObj.find("#btnSubmit").closest("form").get(0), "span"); //获取提示信息
                   if (!validatorshow) {
                       return false;
                   }
                   $conObj.find("#btnSubmit").val("提交中...").attr({disabled:"disabled"});
                   NewsList.filePicUpload();
                });
               //分页在当前选项卡中加载
               $conObj.find("#tablePageTr a").each(function() {
                   $(this).click(function() {
                       topTab.url(topTab.activeTabIndex, $(this).attr("href"));
                       return false;
                   })
               });
               setTimeout(function(){
                    $objEditor=$conObj.find("#" + '<%=this.txtContent.ClientID%>');
                    if($objEditor.length>0)
                    {
                        NewsList.initEditor($conObj.find("#" + '<%=this.txtContent.ClientID%>')[0]);
                    }
               },100);
               //隔行,滑动,点击 变色.+ 单选框选中的行 变色:
               $($conObj[1]).find("tr:gt(0):even").addClass('odd');
               $($conObj[1]).find("tr:gt(0)").hover(
		            function() { $(this).addClass('highlight'); },
		            function() { $(this).removeClass('highlight'); }
	            );
           })
       </script>
</asp:content>