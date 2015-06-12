<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetFriendLinkManage.aspx.cs" Inherits="UserBackCenter.EShop.SetFriendLinkManage" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>添加友情链接</title>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("backalertbody") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   <div style="width: 100%;" id="FriendLinkList">
    </div>     
    </form>
     <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

       <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>   
       
    <script language="javascript" src="/js/jquery-1.3.2.min.js" type="text/javascript"></script>

    

    <script language="javascript" type="text/javascript">
        $(document).ready(function(){
            initPage();           
        });
        function newAjax(contentId,Url,dataArr,callBack)
        {  
          $.ajax
            ({  
                url:Url+"?"+dataArr,
                cache:false,
                async:false,
                success:function(html)
                {
                 if(contentId) {
                        $("#"+contentId).html(html);
                     }
                     var type= typeof(callBack);
                     switch(type){
                        case 'function':
                            callBack(html);
                            break;
                        case 'string':
                            eval(callBack);
                        default:
                            break;             
                     }
                    }              
            });  
        }
        function initPage()
        {            
        var compid="<%=CompanyId %>";
            newAjax("FriendLinkList","AjaxFriendLinkManage.aspx","CompanyId="+compid,deleteFriendLink);
            
        }
        function checkUrl(urlValue)
        {
            var checkValue=$.trim(urlValue);
            var result=null;
            if(checkValue!=""){
                var patrn=/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\\\"\\\"])*$/;               
                result=patrn.exec(checkValue);                                         
            }   
            return result;  
        }
        //删除链接
        function deleteFriendLink()
        {
            $(".a_dellink").each(function(){
                $(this).click(function(){
                    if(window.confirm("你确定要删除该链接吗，此操作不可恢复！"))
                    {
                        var linkId=$(this).attr("linkId");
                        $.ajax
                        ({  
                            url:"SetFriendLinkManage.aspx?action="+"delete"+"&FriendLinkId="+linkId,
                            cache:false,
                            async:false,
                            success:function(html)
                            {
                                $("#FriendLinkList").html(html);
                                var type= typeof(callBack);
                                 switch(type){
                                    case 'function':
                                        callBack(html);
                                        break;
                                    case 'string':
                                        eval(callBack);
                                    default:
                                        break;
                                  }      
                               if(html=="1"){
                                    initPage();
                                    alert("删除链接成功！")
                                }else{
                                    alert("删除链接失败！")
                                }  
                              },            
                            error:function()
                             {
                                alert("操作失败");
                             }
                        }); 
                    }
                });
            });
             $("#addFriendLink").click(function(){                 
                 var LinkAddress=$("#txtLinkAddress").val(); 
                var SortId=$("#txtSortId").val();
                 if($("#txtLinkName").val()==""){ 
                    alert("链接名称不能为空！")
                    return false;
                 }
                 if(!checkUrl(LinkAddress))
                 {
                    alert("链接地址输入错误！")
                    return false;
                 }   
                 if(!checkIsNumber(SortId)){ 
                    alert("排序值应为整数！")
                    return false;
                 }                       
                $.ajax
                ({  
                    url:"SetFriendLinkManage.aspx?action="+"addFriendLink"+"&LinkAddress="+LinkAddress+"&LinkName="+escape($("#txtLinkName").val())+"&SortId="+SortId,
                    cache:false,
                    async:false,
                    success:function(html)
                    {
                        $("#FriendLinkList").html(html);                           
                        var type= typeof(callBack);
                         switch(type){
                            case 'function':
                                callBack(html);
                                break;
                            case 'string':
                                eval(callBack);
                            default:
                                break;   
                          }   
                         if(html=="1"){
                            initPage();
                            alert("添加链接成功！")
                            //$("#txtLinkAddress").val('http://');
                            //$("#txtLinkName").val('');
                            //$("#txtSortId").val('0');
                        }else
                        {
                            initPage();                          
                            if(eval(html)){
                                alert(eval(html))
                            }else{
                                alert("添加链接失败！")
                            }
                        }   
                    },            
                    error:function()
                    {
                        alert("操作失败");
                    }
                });               
            });
            
            $(".a_updatelink").each(function(i){
                $(this).click(function(){                
                        var linkId=$(this).attr("linkId");
                        var LinkAddress=$("input[name='LinkAddress']").eq(i).val();
                        var LinkName=escape($("input[name='LinkName']").eq(i).val());
                         var SortId=$("input[name='linkSort']").eq(i).val();    
                        if(LinkName==""){ 
                            alert("链接名称不能为空！")
                            return false;
                        }
                        if(!checkUrl(LinkAddress))
                        {
                            alert("链接输入错误！")
                            return false;
                        } 
                        if(!checkIsNumber(SortId)){ 
                            alert("排序值应为整数！")
                            return false;
                        }
                       
                    $.ajax
                    ({  
                        url:"SetFriendLinkManage.aspx?action="+"update"+"&LinkAddress="+LinkAddress+"&LinkName="+LinkName+"&SortId="+SortId+"&FriendLinkId="+linkId,
                        cache:false,
                        async:false,
                          success:function(html)
                            {
                                $("#FriendLinkList").html(html);
                                 var type= typeof(callBack);
                                 switch(type){
                                    case 'function':
                                        callBack(html);
                                        break;
                                    case 'string':
                                        eval(callBack);
                                    default:
                                        break; 
                                 }     
                                  if(html=="1"){
                                        initPage();
                                        alert("修改链接成功！");
                                }else{
                                     initPage();
                                        if(eval(html)){
                                            alert(eval(html))
                                        }else{
                                            alert("修改链接失败！")
                                        }
                                    }   
                           },            
                        error:function()
                         {
                            alert("操作失败");
                         }
                    });              
                });
            })
        }
        function checkUrl(urlValue)
        {
            var checkValue=$.trim(urlValue);
            var result=null;
            if(checkValue!=""){
                var patrn=/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\\\"\\\"])*$/;               
                result=patrn.exec(checkValue);                                         
            }   
            return result;  
        }
        function checkIsNumber(sortvalue)
        {
            var checkvalue=$.trim(sortvalue);
            var result=null;
            if(sortvalue!="")            
            {
                var part= /^[-\+]?\d+(\.\d+)?$/;
                result=part.exec(sortvalue);
            }   
            return result;
        }

       
    </script>
</body>
</html>
