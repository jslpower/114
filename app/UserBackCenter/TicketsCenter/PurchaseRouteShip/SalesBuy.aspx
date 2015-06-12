<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesBuy.aspx.cs" Inherits="UserBackCenter.TicketsCenter.PurchaseRouteShip.SalesBuy" %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:content id="SalesBuy" runat="server" contentplaceholderid="ContentPlaceHolder1">

      	<ul class="sub_leftmenu">
        	<li><a href="#"  id="three1" onclick="topTab.url(topTab.activeTabIndex, '/TicketsCenter/PurchaseRouteShip/Default.aspx');return false;">常规购买</a></li>
          <li><a href="#" id="three2" onclick="topTab.url(topTab.activeTabIndex, '/TicketsCenter/PurchaseRouteShip/ComboBuy.aspx');return false;">套餐购买</a></li>
            <li><a href="#" id="three3"  class="book_default">促销购买</a></li>
        </ul>
        <div class="clearboth"></div>
        <div class="contact_text"><span><font color="#FF0000">备注：</font>如果有个性化需求，可在每周一至周五9：00－17：30另行 <a href="#">联系我们</a>。<font color="#FF0000">供应商管理部：</font><a href="#">MQ</a>
以上所有活动按照自然月计算，本活动解释权归属同业114。</span>   </div>
   
<div id="con_three_3" >
        	<table width="835" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#7dabd8" class="admin_tablebox">
              <tr>
                <th width="69" height="30" align="center" bgcolor="#EEF7FF">套餐项目</th>
                <th width="69" align="center" bgcolor="#EEF7FF">类型</th>
                <th width="69" align="center" bgcolor="#EEF7FF">航空公司</th>
                <th width="69" align="center" bgcolor="#EEF7FF">始发地</th>
                <th width="69" align="center" bgcolor="#EEF7FF">目的地</th>
                <th width="69" align="center" bgcolor="#EEF7FF">一月价格（元/条）</th>
                <th width="69" align="center" bgcolor="#EEF7FF">一季价格（元/条）</th>
                <th width="69" align="center" bgcolor="#EEF7FF">半年价格（元/条）</th>
                <th width="69" align="center" bgcolor="#EEF7FF">小计(元)</th>
                <th width="69" align="center" bgcolor="#EEF7FF">开始月份</th>
                <th width="69" align="center" bgcolor="#EEF7FF">结束月份</th>
                <th width="69" align="center" bgcolor="#EEF7FF">购买</th>
              </tr>
              <tr>
                <td height="30" align="center">华东区套餐</td>
                <td align="center">团队</td>
                <td align="center">CA-国际航空</td>
                <td align="center">杭州</td>
                <td align="center">北京</td>
                <td align="center" bgcolor="#E9EDF4"><input name="radio001" type="radio" id="radio10" value="radio001" checked="checked" />
                540</td>
                <td align="center"><input name="radio001" type="radio" id="radio11" value="radio001" />
                780</td>
                <td align="center"><input name="radio001" type="radio" id="radio12" value="radio001" />
                888</td>
                <td align="center">540</td>
                <td align="center"><select name="select16" id="select16">
                  <option value="1">1</option>
                  <option value="2">2</option>
                  <option value="3">3</option>
                  <option value="4">4</option>
                  <option value="5">5</option>
                  <option value="6">6</option>
                  <option value="7">7</option>
                  <option value="8">8</option>
                  <option value="9">9</option>
                  <option value="10">10</option>
                  <option value="11">11</option>
                  <option value="12">12</option>
                </select></td>
                <td align="center">10.1</td>
                <td align="center"><input type="submit" name="button" id="button" value="支付" /></td>
              </tr>
              <tr>
                <td height="30" align="center">华南区套餐</td>
                <td align="center">散客</td>
                <td align="center">CA-国际航空</td>
                <td align="center">上海</td>
                <td align="center">成都</td>
                <td align="center"><input type="radio" name="radio002" id="radio13" value="radio002" />
                300</td>
                <td align="center" bgcolor="#E9EDF4"><input name="radio002" type="radio" id="radio14" value="radio002" checked="checked" />
                450</td>
                <td align="center"><input type="radio" name="radio002" id="radio15" value="radio002" />
                888</td>
                <td align="center">1350</td>
                <td align="center"><select name="select17" id="select17">
                  <option value="1">1</option>
                  <option value="2">2</option>
                  <option value="3">3</option>
                  <option value="4">4</option>
                  <option value="5">5</option>
                  <option value="6">6</option>
                  <option value="7">7</option>
                  <option value="8">8</option>
                  <option value="9">9</option>
                  <option value="10">10</option>
                  <option value="11">11</option>
                  <option value="12">12</option>
                </select></td>
                <td align="center">10.1</td>
                <td align="center"><input type="submit" name="button9" id="button9" value="支付" /></td>
              </tr>
              <tr>
                <td height="30" align="center">华北区套餐</td>
                <td align="center">团队</td>
                <td align="center">CA-国际航空</td>
                <td align="center">北京</td>
                <td align="center">东京</td>
                <td align="center">                <input type="radio" name="radio003" id="radio16" value="radio003" />
                180</td>
                <td align="center"><input type="radio" name="radio003" id="radio17" value="radio003" />                
                300</td>
                <td align="center" bgcolor="#E9EDF4"><input name="radio003" type="radio" id="radio18" value="radio003" checked="checked" />
                888</td>
                <td align="center">5328</td>
                <td align="center"><select name="select18" id="select18">
                  <option value="1">1</option>
                  <option value="2">2</option>
                  <option value="3">3</option>
                  <option value="4">4</option>
                  <option value="5">5</option>
                  <option value="6">6</option>
                  <option value="7">7</option>
                  <option value="8">8</option>
                  <option value="9">9</option>
                  <option value="10">10</option>
                  <option value="11">11</option>
                  <option value="12">12</option>
                </select></td>
                <td align="center">10.1</td>
                <td align="center"><input type="submit" name="button2" id="button2" value="支付" /></td>
              </tr>
        </table>
        </div>
</asp:content>
