<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNews.aspx.cs" Inherits="UserBackCenter.SupplyManage.AddNews" %>

<asp:content id="AddNews" runat="server" contentplaceholderid="ContentPlaceHolder1">
<script type="text/javascript" src="/kindeditor/kindeditor.js"  cache="true" ></script>
<script type="text/javascript">
 //初始化编辑器
 KE.init({
	id : 'txt_addnews_AfficheInfo',//编辑器对应文本框id
	width : '550px',
	height : '270px',
	skinsPath:'/kindeditor/skins/',
	pluginsPath:'/kindeditor/plugins/',
	scriptPath:'/kindeditor/skins/',
    resizeMode : 0,//宽高不可变
	items:keSimple //功能模式(keMore:多功能,keSimple:简易)
});
</script>
<table id="tbl_SupplyManage_AddNews" width="99%" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
        <tr>
            <td valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <table width="99%" border="0" cellspacing="0" cellpadding="0" style="border-bottom: 1px solid #ABC9D9;
                                border-left: 1px solid #ABC9D9; border-right: 1px solid #ABC9D9; height: 470px;">
                                <tr>
                                    <td align="left" valign="top">
                                   
                                                     <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td height="28" background="<%=ImageServerUrl %>/images/managertopbjmm.gif">
                                                                &nbsp;<strong>新闻管理</strong>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table border="0" cellspacing="0" cellpadding="0" class="cgbj">
                                                        <tr>
                                                            <td>
                                                                <a href="/supplymanage/newslist.aspx" rel="newslist">返回新闻列表</a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                     
                                        <table width="100%" border="1" align="center" cellpadding="2" cellspacing="0" bordercolor="#D0E8F8"
                                            style="border-bottom: 2px dashed #ccc; margin-bottom: 15px;">
                                            <tr>
                                                <td width="16%" align="right" bgcolor="#F1F9FF">
                                                    <span class="ff0000">*</span>标题：
                                                </td>
                                                <td width="84%" align="left">
                                                    <input name="addnews_AfficheTitle"  class="bitian" id="txt_addnews_AfficheTitle"  valid="required" errmsg="标题不能为空！"  value="<%=AfficheTitle%>" type="text" maxlength="50" style="width:490px;" />&nbsp;&nbsp;<span>(标题字数小于等于50字)</span>
                                                    <span id="errMsg_txt_addnews_AfficheTitle" class="errmsg" style="position:absolute;"></span>
                                                </td>
                                            </tr>                                          
                                            <tr>
                                                <td align="right" bgcolor="#F1F9FF">
                                                    <span class="ff0000">*</span>新闻内容：
                                                </td>
                                                <td align="left">
                                                <textarea name="addnews_AfficheInfo" id="txt_addnews_AfficheInfo" cols="1" rows="1"  valid="required" errmsg="内容不能为空！" style="display:none;"><%=AfficheInfo%></textarea>   
                                                                                                    
                                                  <span id="errMsg_txt_addnews_AfficheInfo" class="errmsg"></span>                                                         
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="22" align="right" bgcolor="#F1F9FF">
                                                    时间：
                                                </td>
                                                <td align="left">
                                                    <input name="addnews_IssueTime"  class="shurukuang" style="width:100px;" onfocus="WdatePicker()"  valid="required|isDate" errmsg="发布时间不能为空！|发布时间格式不正确！" id="txt_addnews_IssueTime" type="text" class="bitian1" value="<%=DateTime.Now.ToShortDateString() %>" size="35" />
                                                    <span id="errMsg_txt_addnews_IssueTime" class="errmsg"></span>   
                                                </td>
                                            </tr>                                           
                                            <tr>
                                                <td align="right" bgcolor="#F1F9FF">
                                                    &nbsp;
                                                </td>
                                                <td align="left" height="50">
                                                    <a href="javascript:void(0)" class="xiayiye" id="btn_addnews_Save">保存</a><a href="javascript:void(0)" class="xiayiyec" id="btn_addnews_Continue" style="width:150px;">保存并继续发布</a>
                                                    <div id="SaveUploadFile" style="color:Red;"></div>             
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
            
            <script language="javascript" type="text/javascript">
                var SM_AddNews={
                    AfficheID:"<%=AfficheID %>",
                    save:function(callBack){                      
                        $.newAjax({
                           type: "POST",
                           url: "/SupplyManage/AddNews.aspx?action=Save&AfficheID="+SM_AddNews.AfficheID,
                           data: $($("#txt_addnews_AfficheTitle").closest("form").get(0)).serializeArray(),
                           success: function(msg){                             
                             callBack(msg); 
                           },error:function(){
                                $("#SaveUploadFile").hide();
                                alert("对不起，保存失败！");
                           }
                        });  
                    },
                    cancel:function(){
                        $("#txt_addnews_AfficheTitle").val('');
                        $("#txt_addnews_AfficheInfo").val('');
                    }
                };
                $(document).ready(function(){
                    $("#tbl_SupplyManage_AddNews a[rel='newslist']").eq(0).click(function(){
                        topTab.open($(this).attr("href"),"公司资讯",{isRefresh:true});
                        return false;
                    })
                    $("#tbl_SupplyManage_AddNews #btn_addnews_Save").eq(0).click(function(){
                        $("#txt_addnews_AfficheInfo").val(encodeURIComponent(KE.html('txt_addnews_AfficheInfo')));//获取编辑器内容并赋值到文本框
                        if(ValiDatorForm.validator($("#btn_addnews_Continue").closest("form").get(0),"alertspan")){
                            $("#btn_addnews_Save,#btn_addnews_Continue").hide();
                            SM_AddNews.save(function(msg){                             
                                 var returnMsg=eval(msg);
                                 if(returnMsg)
                                 {
                                    $("#SaveUploadFile").html("保存成功...").hide(2000).html("")
                                    alert("保存成功！")
                                    topTab.url(topTab.activeTabIndex,"/supplymanage/newslist.aspx");                                                                                          
                                 }else{
                                    $("#SaveUploadFile").html("保存失败...").hide()
                                    alert('对不起，保存失败！')
                                 }       
                            });
                        }
                        return false;
                    });
                    $("#tbl_SupplyManage_AddNews #btn_addnews_Continue").eq(0).click(function(){
                        $("#txt_addnews_AfficheInfo").val(encodeURIComponent(KE.html('txt_addnews_AfficheInfo')));//获取编辑器内容并赋值到文本框
                        if(ValiDatorForm.validator($("#btn_addnews_Continue").closest("form").get(0),"alertspan")){
                            $("#btn_addnews_Save,#btn_addnews_Continue").hide();
                            SM_AddNews.save(function(msg){
                                 var returnMsg=eval(msg);
                                 if(returnMsg)
                                 {
                                    $("#SaveUploadFile").html("保存成功...").hide(2000).html("")
                                    alert("保存成功！");
                                    $("#btn_addnews_Save,#btn_addnews_Continue").show();
                                    SM_AddNews.cancel();                                                                                             
                                 }else{
                                    $("#SaveUploadFile").html("保存失败...").hide()
                                    alert('对不起，保存失败！')
                                 }          
                            });
                        }
                        return false;
                    })
                    setTimeout(function(){
                          KE.create('txt_addnews_AfficheInfo',0);//创建编辑器
                          KE.html('txt_addnews_AfficheInfo',htmlDecode($("#txt_addnews_AfficheInfo").html())) //赋值
                        },50);
                });
            </script>
</asp:content>
