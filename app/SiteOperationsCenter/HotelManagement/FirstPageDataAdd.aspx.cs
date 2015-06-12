using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SiteOperationsCenter.HotelManagement
{
    /// <summary>
    /// 酒店管理：首页数据添加
    /// 2010-12-02，袁惠
    /// </summary>
    public partial class FirstPageDataAdd :EyouSoft.Common.Control.YunYingPage
    {
        protected List<EnumObj> showtype = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckMasterGrant(YuYingPermission.酒店后台管理_首页板块数据管理))
                {
                    Utils.ResponseNoPermit(YuYingPermission.酒店后台管理_首页板块数据管理, true);
                    return;
                }

                string methodtype = Utils.GetFormValue("method");
                if (string.IsNullOrEmpty(methodtype))
                {
                    BindControlData();
                }
                else
                {
                    OpearMothed(methodtype);
                }

            }
        }
        /// <summary>
        /// 绑定控件
        /// </summary>
        private void BindControlData()
        {
            //IList<EyouSoft.Model.HotelStructure.HotelCity> cityList = EyouSoft.BLL.HotelStructure.HotelCity.CreateInstance().GetList();

            //foreach (var item in cityList)
            //{
            //    sltCity.Items.Add(new ListItem(item.SimpleSpelling+item.CityName,item.CityCode));
            //}        
            //sltCity.Items.Insert(0,new ListItem("-请选择-","-1"));
            HotelLevelInit(null);
            //绑定板块酒店类型
            showtype = EnumObj.GetList(typeof(EyouSoft.Model.HotelStructure.HotelShowType));
            //cityList = null;
        }
        /// <summary>
        /// 添加，删除操作
        /// </summary>
        /// <param name="mothedType">操作类型（add,delete）</param>
        private void OpearMothed(string mothedType)
        {
            string[] HotelCodesOrId = StringValidate.Split(Utils.GetFormValue("HotelCodes"), ",");  //接口酒店Code,或者本地酒店ID
          
            if (mothedType == "add")
            {
                int HotelShowType = Utils.GetInt(Request.Form["HotelShowType"], 0);
                string Interjson=Server.HtmlDecode( Utils.GetFormValue("InterList"));  //获取接口列表数据
                if (string.IsNullOrEmpty(Interjson))
                {
                    Utils.ResponseMeg(false, "请重新查询数据，再进行添加！");
                    return;
                }
                EyouSoft.Model.HotelStructure.HotelLocalInfo hotelLocal = null;  //本地酒店实体
                IList<EyouSoft.Model.HotelStructure.HotelLocalInfo> list = new List<EyouSoft.Model.HotelStructure.HotelLocalInfo>();
                string topcodes = Utils.GetFormValue("HotelTopCodes");
                string[] SetTopCodes =string.IsNullOrEmpty(topcodes)==true?null: StringValidate.Split(topcodes, ",");  //置顶的酒店Code
                IList<EyouSoft.Model.HotelStructure.HotelInfo> interList = JsonConvert.DeserializeObject<IList<EyouSoft.Model.HotelStructure.HotelInfo>>(Interjson);  //将json序列化为集合对象
                foreach (string item in HotelCodesOrId)
                {
                    EyouSoft.Model.HotelStructure.HotelInfo hodelinfo = null;
                    foreach (EyouSoft.Model.HotelStructure.HotelInfo interitem in interList)
                    {
                        if (interitem == null) continue;
                        if (interitem.HotelCode == item)
                        {
                            hodelinfo = interitem;
                            break;
                        }
                    }
                    if (hodelinfo != null)  //将接口Hotel值赋给HotelLocal实体
                    {
                        hotelLocal = new EyouSoft.Model.HotelStructure.HotelLocalInfo();
                        hotelLocal.HotelCode = hodelinfo.HotelCode;
                        hotelLocal.HotelName = hodelinfo.HotelName;
                        hotelLocal.IssueTime = DateTime.Now;
                        hotelLocal.MarketingPrice = hodelinfo.MinRate; //暂定
                        if (hodelinfo.HotelImages != null && hodelinfo.HotelImages.Count > 0)
                            hotelLocal.HotelImg = hodelinfo.HotelImages[0].ImageURL;
                        hotelLocal.Rank = hodelinfo.Rank;
                        hotelLocal.ShortDesc = hodelinfo.ShortDesc;
                        hotelLocal.ShowType = (EyouSoft.Model.HotelStructure.HotelShowType)HotelShowType;
                        hotelLocal.CityCode = hodelinfo.CityCode;
                        if (SetTopCodes != null && SetTopCodes.Length > 0)   //判断是否选中置顶
                        {
                            for (int i = 0; i < SetTopCodes.Length; i++)
                            {
                                if (item == SetTopCodes[i])
                                {
                                    hotelLocal.IsTop = true;
                                }
                            }
                        }
                        list.Add(hotelLocal);
                        hodelinfo = null;
                    }
                }

                int row = EyouSoft.BLL.HotelStructure.HotelLocalInfo.CreateInstance().Add(list); //添加操作结果
                if (row==0)
                    Utils.ResponseMeg(false, "该酒店数据已经存在");
                else if (row < list.Count)
                    Utils.ResponseMeg(false, "部分酒店已经存在，其它的添加成功");
                else
                    Utils.ResponseMeg(true, "操作成功");

                list = null;
            }
            if (mothedType == "delete")   //删除本地数据
            {
                if(EyouSoft.BLL.HotelStructure.HotelLocalInfo.CreateInstance().Delete(HotelCodesOrId)>0)
                {
                    Utils.ResponseMeg(true, "操作成功");
                }
                else
                {
                    Utils.ResponseMeg(true, "操作失败");
                }
            }
        }

        /// <summary>
        /// 酒店星级初始化
        /// </summary>
        /// <param name="hotelLevel"></param>
        private void HotelLevelInit(string hotelLevel)
        {
            IList<ListItem> list = EyouSoft.Common.Function.EnumHandle.GetListEnumValue(typeof(EyouSoft.HotelBI.HotelRankEnum));
            this.sltStartNum.Items.Clear();
            string str = "一二三四五";
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    ListItem item = new ListItem();
                    item.Value = list[i].Value;
                    if (i == 0)
                    {
                        item.Text = "--不限--";
                    }
                    if (i > 0 && i < 6)
                    {
                        item.Text = str.Substring(i - 1, 1) + "星级";
                    }
                    if (i >= 6)
                    {
                        item.Text = "准" + str.Substring(i - 6, 1) + "星级";
                    }
                    this.sltStartNum.Items.Add(item);
                }
            }
            list = null;
        }
    }
}
