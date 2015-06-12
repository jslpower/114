<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportNumFromSystem.aspx.cs" Inherits="UserBackCenter.SMSCenter.ImportNumFromSystem" %>
<%@ Register Src="/usercontrol/ProvinceAndCityList.ascx" TagName="pc" TagPrefix="uc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <style>
        body
        {
            font-size: 12px;
        }
        table
        {
          border-collapse:collapse;
        }
        a
        {
          color:#0048A3;
           text-decoration:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    	<table width="100%" border="0" cellspacing="0" cellpadding="0" class="ztlistsearch">
              <tr>
                <td width="4%" align="center"><img src="<%=ImageServerUrl%>/images/searchico2.gif" width="23" height="24" /></td>
                <td width="96%" align="left">
			      <uc1:pc id="infs_pc" runat="server" ></uc1:pc>
				  公司名称<input id="infs_txtCompanyName" type="text" size="15" onkeypress="return infs.isEnter(event);" />
				  总负责人<input  id="infs_txtAdmin"  type="text" size="15" onkeypress="return infs.isEnter(event);" />
				 
				  <input type="image"  id="infs_btnSearch" onclick="return infs.search()"  value="提交" src="<%=ImageServerUrl%>/images/chaxun.gif" style="width:62px; height:21px; border:none; margin-bottom:-5px;" />                  </td>
              </tr>
            </table>
            <div id="infs_customerdiv" style="margin-top:5px; text-align:center;"></div>
            <input type="button" value="导入" onclick="infs.addCustomer()" />
    </div>
    </form>
    </body>
      <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
      <script type="text/javascript">
        //查询参数
        var searchParam={companyname:"",admin:"",province:"",city:"",Page:"",method:"",brand:""};
        $(document).ready(function(){
             infs.search();
           });
        var infs={
               //点击查询操作
                search:function(){
                   searchParam.companyname=encodeURIComponent($("#infs_txtCompanyName").val());
                   searchParam.admin=encodeURIComponent($("#infs_txtAdmin").val());
                  
                   searchParam.province=$("#infs_pc_ddl_ProvinceList").val();
                   searchParam.city=$("#infs_pc_ddl_CityList").val();
                   searchParam.method="";
                   infs.getCustomerList();
                   return false;
                },
                //调用Ajax获取数据
                getCustomerList:function(){
                   $.ajax({
                            type: "GET",
                            dataType: "html",
                            url: "/SMSCenter/AjaxSystemCustomer.aspx",
                            data: searchParam,
                            cache: false,
                            success: function(result) {
                              $("#infs_customerdiv").html(result);
                              $("#asc_ExportPage a").click(function(){
                                  return infs.loadData($(this));
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
                    infs.getCustomerList();
                    return false;
                },
                   //判断是否按回车
                isEnter:function(event){
                 event=event?event:window.event;
                 if(event.keyCode==13)
                 {
                  infs.search();
                  return false;
                 }
              },
                //添加客户
                addCustomer:function(){
                    var selCheckBox=$("#infs_customerdiv").find(":checkbox:checked");
                    if(selCheckBox.length==0)
                    {
                        alert("请选择要导入的客户!");
                        return false;
                    }
                    var datas={method:"add"};
                    var customerList="[";
                    selCheckBox.each(function(){
                       customerList+="{CustomerCompanyName:\""+$(this).parent().next().next().html()+"\",CustomerContactName:\""+$(this).parent().next().next().next().html()+"\",Mobile:\""+$(this).parent().next().html()+"\"},";
                    });
                    datas.custs=customerList.replace(/,$/,'')+"]";
                    
                     $.ajax({
                            type: "post",
                            dataType: "json",
                            url: "/SMSCenter/ImportNumFromSystem.aspx",
                            data: datas,
                            cache: false,
                            success: function(result) {
                               if(result.success=="0")
                               {
                                 alert(result.message);
                                 return false;
                               }
                               alert("导入完成,如果号码已经存则将被过滤掉!");
                               window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                               window.parent.CustomerList.search();
                               return false;
                             
                            },
                            error:function(){
                                alert("操作失败!");
                            }
                        });
                        return false;
                  
                    
                    
                }
      }
 </script>

</html>
