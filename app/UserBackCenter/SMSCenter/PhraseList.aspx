<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhraseList.aspx.cs" Inherits="UserBackCenter.SMSCenter.PhraseList" %>

<%@ Register Src="/usercontrol/SMSCenter/SmsHeaderMenu.ascx" TagName="shm" TagPrefix="uc" %>

<asp:Content id="PhraseList" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<uc:shm id="pl_shm1" runat="server" TabIndex="tab3"></uc:shm>
 <table width="100%" border="0" cellspacing="0" cellpadding="0" class="mobilebox" style="margin-bottom:10px;margin-top:10px; width:99%;">
        <tr>
          <td align="left">查询：关键字
            <input id="pl_txtKeyWord" type="text" size="30" onkeypress="return PhraseList.isEnter(event);" />
            类型
            <select id="pl_selPhraseClass" runat="server">
            </select>
            
         <input type="button" id="pl_btnSearch"value="搜 索" onclick="PhraseList.search();" /></td>
          <td align="center">&nbsp;</td>
        </tr>
      </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-bottom:5px;" class="tablewidth">
        <tr>
          <td align="left" background="<%=ImageServerUrl%>/images/gongneng_bg.gif"><img src="<%=ImageServerUrl%>/images/ge_da.gif" width="3" height="20" /><a href="javascript:void(0);" onclick="return PhraseList.addPhrase();"><img src="<%=ImageServerUrl%>/images/xinzeng.gif" width="50" height="25" border="0" /></a><a href="javascript:void(0);" onclick="return PhraseList.updatePhrase();"><img src="<%=ImageServerUrl%>/images/xiugai.gif" width="50" height="25" /></a><a href="javascript:void(0);" onclick="return PhraseList.delPhrase();"><img src="<%=ImageServerUrl%>/images/shanchu.gif" width="51" height="25" /></a></td>
        </tr>
      </table>
     <div id="pl_PhraseDiv"></div>
 
 </table>

<script type="text/javascript">
         
          //查询参数
          var searchParam={keyword:"",phraseclass:"",Page:"",method:""};
          var PhraseList={
             
                //点击查询操作
                search:function(){
                   searchParam.method="";
                   searchParam.keyword=encodeURIComponent($("#pl_txtKeyWord").val());
                   searchParam.phraseclass=encodeURIComponent($("#<%=pl_selPhraseClass.ClientID %>").val());
                   PhraseList.getPhraseList();
                },
                //调用Ajax获取数据
                getPhraseList:function(){
                   $.newAjax({
                            type: "post",
                            dataType: "html",
                            url: "/SMSCenter/AjaxPhraseList.aspx",
                            data: searchParam,
                            cache: false,
                            success: function(result) {
                              if(result=="{success:'0',message:''}")
                              {
                                alert("对不起,你尚未审核通过!")
                                return false;
                              }
                              $("#pl_PhraseDiv").html(result);
                              $("#apl_ExportPage a").click(function(){
                                  return PhraseList.loadData($(this));
                              });
                              if(searchParam.method=="delete")
                              {
                                  alert("删除成功!");
                              }
                            },
                            error:function(){
                                alert("操作失败!");
                            }
                        });
                        return false;
                },
                //点击分页时获取数据
                loadData:function(tar_a){
                    searchParam.Page=tar_a.attr("href").match(/Page=\d+/)[0].match(/\d+/)[0];
                    searchParam.method="";
                    PhraseList.getPhraseList();
                    return false;
                },
                updatePhrase:function(){
                    var selCheckBox=$("#pl_PhraseDiv").find(":checkbox:checked");
                    if(selCheckBox.length!=1)
                    {
                        alert("请选择要修改的1条短语!");
                        return false;
                    }
                     topTab.url(topTab.activeTabIndex,"/SMSCenter/PhraseEdit.aspx?phraseid="+selCheckBox.val());
                     return false;
                    
                },
                 //判断是否按回车
                isEnter:function(event){
                 event=event?event:window.event;
                 if(event.keyCode==13)
                 {
                  PhraseList.search();
                  return false;
                 }
              },
                //删除短语
                delPhrase:function(){
                    var selCheckBox=$("#pl_PhraseDiv").find(":checkbox:checked");
                    if(selCheckBox.length==0)
                    {
                        alert("请选择要删除的短语!");
                        return false;
                    }
                    if(!confirm("你确定要删除所选的短语吗?"))
                    {
                        return false;
                    }
                    searchParam.method="delete";
                    var pIds="";
                    selCheckBox.each(function(){
                        pIds+=$(this).val()+",";
                    });
                    searchParam.pids=pIds;
                    PhraseList.getPhraseList();
                    
                },
                chkAll:function(tar_chk){
                  $("#pl_PhraseDiv").find("input[name='pl_chk']").attr("checked",$(tar_chk).attr("checked"));
                },
                addPhrase:function(){
                     topTab.url(topTab.activeTabIndex,"/SMSCenter/PhraseEdit.aspx");
                     return false;
                }
          }
           $(document).ready(function(){
              PhraseList.getPhraseList();
          });
      </script>
</asp:Content>
