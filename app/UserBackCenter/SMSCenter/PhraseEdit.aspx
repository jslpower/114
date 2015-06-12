<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhraseEdit.aspx.cs" Inherits="UserBackCenter.SMSCenter.PhraseEdit" %>
<%@ Register Src="/usercontrol/SMSCenter/SmsHeaderMenu.ascx" TagName="shm" TagPrefix="uc" %>
<asp:Content id="PhraseEdit" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<script type="text/javascript">
       
          var param={method:"",catename:"",cateid:"",content:"",phraseid:""};
          var PhraseEdit={
              //添加类别
              addClass:function(){
                  var className=$("#pe_txtPhraseClass").val().replace(/\s+/g,'');
                  if(className!="")
                  { 
                    if(className.length>10)
                    {
                       alert("类别不超过10字!");
                       $("#pe_txtPhraseClass").focus();
                       return false;
                    }
                    param.classname=encodeURIComponent(className);
                    param.method="addClass";
                    $.newAjax({
                        type: "get",
                        dataType: "json",
                        url: "/SMSCenter/PhraseEdit.aspx",
                        data: param,
                        cache: false,
                        success: function(result) {
                          if(result.success=="1")
                          {
                              $("#<%=pe_selPhraseClass.ClientID %>").append("<option value='"+result.message+"'>"+className+"</option");
                              $("#pe_txtPhraseClass").val('');
                              alert("添加成功!");
                          }
                          else if(result.success=="0")
                          {
                            alert(result.message);
                          }
                        },
                        error:function(){
                            alert("操作失败!");
                        }
                    });
                  }
              },
                   //判断是否按回车
                isEnter:function(event){
                 event=event?event:window.event;
                 if(event.keyCode==13)
                 {
                 
                  return false;
                 }
              },
              //显隐添加类别
              onClass:function(){
                 $("#pe_txtPhraseClass,#pe_btnClassAdd").toggle();
              },
              //删除类别
                 deleteClass:function(){
                  var selClass=$("#<%=pe_selPhraseClass.ClientID %>").val();
                  if(selClass=="")
                  {   
                      alert("请选择要删除的类别!");
                      return false;
                  }
                   $.newAjax({
                        type: "GET",
                        dataType: "json",
                        url: "/SMSCenter/PhraseEdit.aspx",
                        data: {method:"delClass",classid:selClass},
                        cache: false,
                        success: function(result) {
                          if(result.success=="1")
                          {
                             $("#<%=pe_selPhraseClass.ClientID %> option:selected").remove();
                             alert("删除成功!");
                          }
                          else if(result.success=="0")
                          {
                            alert(result.message);
                          }
                          else
                          {
                            alert("删除失败!")
                          }
                        },
                        error:function(){
                            alert("操作失败!");
                        }
                    });
                  return false;
              },
              savePhrase:function(){
                    var form=$("#pe_btnSave").closest("form").get(0);
                    if(!ValiDatorForm.validator(form,"span"))
                    {
                     return;
                    }
                    if($.trim($("#<%=pe_txtContent.ClientID %>").val()).length>70)
                    {
                      alert("字数不能超过70字!");
                      return;
                    }
                   param.cateid=$("#<%=pe_selPhraseClass.ClientID %>").val();
                   param.catename=encodeURIComponent($("#<%=pe_selPhraseClass.ClientID %> option:selected").text());
                   param.content=encodeURIComponent($("#<%=pe_txtContent.ClientID %>").val());
                   param.method=$("#pe_btnSave").attr("method");
                   param.phraseid=$("#pe_btnSave").attr("phraseid");
                   $.newAjax({
                            type: "post",
                            dataType: "json",
                            url: "/SMSCenter/PhraseEdit.aspx",
                            data: param,
                            cache: false,
                            success: function(result) {
                               if(result.success=="1")
                               {
                                 if(param.method=="update")
                                 {
                                   alert("修改成功!");
                                 }
                                 if(param.method=="add")
                                 {
                                   alert("添加成功!");
                                 }
                               }
                               else
                               {
                                 alert("操作失败!");
                               }
                               topTab.url(topTab.activeTabIndex,"/SMSCenter/PhraseList.aspx");
                               return false;
                            },
                            error:function(){
                                alert("操作失败!");
                            }
                        });
              },
              //获取当前短语字数
              GetWordNum:function(){
                var num=$.trim($("#<%=pe_txtContent.ClientID %>").val()).length;
                $("#pe_spanWordNum").html(num);
//                if(num==70)
//                return false;
              }
              
             
              
          };
             $(document).ready(function(){
              FV_onBlur.initValid($("#pe_btnSave").closest("form").get(0));
              PhraseEdit.GetWordNum();

               $("#<%=pe_txtContent.ClientID %>").bind("keyup",function(){PhraseEdit.GetWordNum()});
          });
      </script>
<uc:shm id="cl_shm1" runat="server" TabIndex="tab3"></uc:shm>
<table width="100%" border="0" cellspacing="0" cellpadding="4" class="mobilebox" style=" width:99%;  margin-top:10px;">
        <tr>
          <td width="114" align="right">类型：</td>
          <td width="584" align="left">
          <select id="pe_selPhraseClass" runat="server"  valid="required"  errmsg="请选择类别">
           
          </select>
               <span id="errMsg_<%=pe_selPhraseClass.ClientID %>" class="errmsg"></span>
            <img src="<%=ImageServerUrl%>/images/add2.gif" width="13" height="12" /><a href="javascript:;" onclick="return PhraseEdit.onClass()">新增</a>
            <input id="pe_txtPhraseClass" type="text" size="12"  style="display:none"  onkeypress="return PhraseEdit.isEnter(event);"/>
           <input type="button" id="pe_btnClassAdd" value="增加"  onclick="PhraseEdit.addClass();return false;" style="display:none" />
           <a href="javascript:;" onclick="return PhraseEdit.deleteClass()">删除</a>
           </td>
        </tr>
        <tr>
          <td align="right">常用语：</td>
          <td align="left">
          <textarea id="pe_txtContent" cols="50" rows="6" runat="server" valid="required"  errmsg="请填写内容" ></textarea>
		  <span id="errMsg_<%=pe_txtContent.ClientID %>" class="errmsg"></span>
          短语内容：当前短语共有<span id="pe_spanWordNum" style="display:inline-block; width:35px;"> 0 </span>字。<span style="color:#ff0000;">*注:短语内容不能超过70字! </span>
		  
		  
		  
		  </td>
        </tr>
        <tr>
          <td align="right">&nbsp;</td>
          <td align="left"><input type="button" id="pe_btnSave" value="保存" onclick="PhraseEdit.savePhrase();" style="width:60px; height:30px;" method="<%=isAdd%>" phraseid="<%=phraseId%>"/></td>
        </tr>
      </table>
      
      
      </asp:Content>
