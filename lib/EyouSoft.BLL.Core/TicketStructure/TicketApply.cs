using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.TicketStructure
{
    /// <summary>
    /// 机票折扣申请
    /// </summary>
    /// Author:张志瑜  2010-10-11
    public class TicketApply : EyouSoft.IBLL.TicketStructure.ITicketApply
    {
        private readonly EyouSoft.IDAL.TicketStructure.ITicketApply idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TicketStructure.ITicketApply>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TicketStructure.ITicketApply CreateInstance()
        {
            EyouSoft.IBLL.TicketStructure.ITicketApply op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.TicketStructure.ITicketApply>();
            }
            return op;
        }

        /// <summary>
        /// 获得机票折扣申请实体
        /// </summary>
        /// <param name="id">申请ID</param>
        /// <returns></returns>
        public EyouSoft.Model.TicketStructure.TicketApply GetModel(string id)
        {
            return idal.GetModel(id);
        }

        /// <summary>
        /// 添加机票折扣申请
        /// </summary>
        /// <param name="model">机票折扣信息实体</param>
        /// <returns></returns>
        public bool Add(EyouSoft.Model.TicketStructure.TicketApply model)
        {
            return idal.Add(model);
        }
        /// <summary>
        /// 删除机票折扣申请
        /// </summary>
        /// <param name="idList">申请的ID串</param>
        /// <returns></returns>
        public bool Delete(params string[] idList)
        {
            return idal.Delete(idList);
        }
        /// <summary>
        /// 修改机票折扣申请
        /// </summary>
        /// <param name="model">机票折扣信息实体</param>
        /// <returns></returns>
        public bool Update(EyouSoft.Model.TicketStructure.TicketApply model)
        {
            return idal.Update(model);
        }
        /// <summary>
        /// 获得申请的列表
        /// </summary>
        /// <param name="query">查询实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总的记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.TicketApply> GetList(EyouSoft.Model.TicketStructure.QueryTicketApply query, int pageSize, int pageIndex, ref int recordCount)
        {
            return idal.GetList(query, pageSize, pageIndex, ref recordCount);
        }

        public void Test()
        {
            return;
            string companyid = "a0e2aceb-4ffe-4f9f-8413-96f9b13cf820";
            EyouSoft.IDAL.CompanyStructure.ICompanyAttachInfo idal5 = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ICompanyAttachInfo>();
            EyouSoft.Model.CompanyStructure.CompanyAttachInfo modelattach = new EyouSoft.Model.CompanyStructure.CompanyAttachInfo();
            modelattach.Bronze = "Bronze";
            modelattach.BusinessCertif.BusinessCertImg = "BusinessCertif.BusinessCertImg";
            modelattach.BusinessCertif.LicenceImg = "BusinessCertif.LicenceImg";
            modelattach.BusinessCertif.TaxRegImg = "BusinessCertif.TaxRegImg";
            modelattach.CommitmentImg = "CommitmentImg";
            modelattach.CompanyCard.ImageLink = "CompanyCard.ImageLink";
            modelattach.CompanyCard.ImagePath = "CompanyCard.ImagePath";
            modelattach.CompanyCard.ThumbPath = "CompanyCard.ThumbPath";
            modelattach.CompanyId = companyid;
            modelattach.CompanyImg.ImageLink = "CompanyImg.ImageLink";
            modelattach.CompanyImg.ImagePath = "CompanyImg.ImagePath";
            modelattach.CompanyImg.ThumbPath = "CompanyImg.ThumbPath";
            modelattach.CompanyLogo.ImageLink = "CompanyLogo.ImageLink";
            modelattach.CompanyLogo.ImagePath = "CompanyLogo.ImagePath";
            modelattach.CompanyLogo.ThumbPath = "CompanyLogo.ThumbPath";
            modelattach.CompanyMQAdv.ImageLink = "CompanyMQAdv.ImageLink";
            modelattach.CompanyMQAdv.ImagePath = "CompanyMQAdv.ImagePath";
            modelattach.CompanyMQAdv.ThumbPath = "CompanyMQAdv.ThumbPath";
            //modelattach.CompanyShopBanner.ImageLink = "CompanyShopBanner.ImageLink";
            modelattach.CompanyShopBanner.ImagePath = "CompanyShopBanner.ImagePath";
            //modelattach.CompanyShopBanner.ThumbPath = "CompanyShopBanner.ThumbPath";
            modelattach.CompanySignet = "CompanySignet";
            modelattach.TradeAward = "TradeAward";
            modelattach.CompanyShopBanner.BannerType = EyouSoft.Model.CompanyStructure.ShopBannerType.Default;
            modelattach.CompanyShopBanner.CompanyLogo = "CompanyLogo.ImagePath";
            modelattach.CompanyShopBanner.ImagePath = "CompanyShopBanner.ImagePath";
            modelattach.CompanyShopBanner.BannerBackground = "CompanyShopBanner.BannerBackground";            
            bool result = idal5.SetCompanyAttachInfo(modelattach);
           
            EyouSoft.Model.CompanyStructure.CardInfo card = new EyouSoft.Model.CompanyStructure.CardInfo();
            card.ImageLink = "card.ImageLink";
            card.ImagePath = "card.ImagePath";
            card.ThumbPath = "card.ThumbPath";
            bool result1 = idal5.SetCompanyCard(companyid, card);
            EyouSoft.Model.CompanyStructure.CompanyImg img = new EyouSoft.Model.CompanyStructure.CompanyImg();
            img.ImageLink = "img.ImageLink";
            img.ImagePath = "img.ImagePath";
            img.ThumbPath = "img.ThumbPath";
            bool result2 = idal5.SetCompanyImage(companyid, img);
            EyouSoft.Model.CompanyStructure.CompanyLogo logo = new EyouSoft.Model.CompanyStructure.CompanyLogo();
            logo.ImageLink = "logo.ImageLink";
            logo.ImagePath = "logo.ImagePath";
            logo.ThumbPath = "logo.ThumbPath";
            bool result3 = idal5.SetCompanyLogo(companyid, logo);
            EyouSoft.Model.CompanyStructure.CompanyMQAdv mqadv = new EyouSoft.Model.CompanyStructure.CompanyMQAdv();
            mqadv.ImageLink = "mqadv.ImageLink";
            mqadv.ImagePath = "mqadv.ImagePath";
            mqadv.ThumbPath = "mqadv.ThumbPath";
            bool result4 = idal5.SetCompanyMQAdv(companyid, mqadv);
            EyouSoft.Model.CompanyStructure.CompanyShopBanner shop = new EyouSoft.Model.CompanyStructure.CompanyShopBanner();
            //shop.ImageLink = "shop.ImageLink";
            shop.ImagePath = "shop.ImagePath";
            //shop.ThumbPath = "shop.ThumbPath";
            shop.BannerType = EyouSoft.Model.CompanyStructure.ShopBannerType.Default;
            shop.CompanyLogo = "shop.CompanyLogo";
            shop.ImagePath = "shop.ImagePath";
            shop.BannerBackground = "shop.BannerBackground";
            bool result5 = idal5.SetCompanyShopBanner(companyid, shop);
            EyouSoft.Model.CompanyStructure.CompanyAttachInfo attachmodel = idal5.GetModel(companyid);
            int i = 0;
            return;
            EyouSoft.IDAL.MQStructure.IIMMessage idal4 = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.MQStructure.IIMMessage>();
            IList<EyouSoft.Model.MQStructure.IMMessage> list4 = idal4.GetTodayLastMessage(0);

            return;
            bool issend = false;
            issend = EyouSoft.BLL.ToolStructure.MsgTipRecord.CreateInstance().IsSendMsgTip(EyouSoft.Model.ToolStructure.MsgType.AddFriend, EyouSoft.Model.ToolStructure.MsgSendWay.SMS, "48075", 362);
            return;
            EyouSoft.IDAL.MQStructure.IIMMember idal1 = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.MQStructure.IIMMember>();
            IList<Model.CompanyStructure.CompanyUserBase> list1 = idal1.GetLongOffLineList(29, 362);

            EyouSoft.IDAL.CompanyStructure.ICompanyUser idal2 = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ICompanyUser>();
            EyouSoft.Model.CompanyStructure.CompanyUser model2 = idal2.GetAdminModel("40e1a851-6057-4dd9-ae0b-c270566303ac");

            EyouSoft.IDAL.ToolStructure.IMsgTipRecord idal3 = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ToolStructure.IMsgTipRecord>();
            EyouSoft.Model.ToolStructure.MsgTipRecord msgmodel = new EyouSoft.Model.ToolStructure.MsgTipRecord();
            msgmodel.Email = "zhangzy@163.com";
            msgmodel.FromMQID = "35967";  //mqlogin
            msgmodel.ToMQID = "48075";   //mqlogin_1
            msgmodel.Mobile = "13777476875";
            msgmodel.MsgType = EyouSoft.Model.ToolStructure.MsgType.AddFriend;
            msgmodel.SendWay = EyouSoft.Model.ToolStructure.MsgSendWay.Email;            
            bool istrue = idal3.Add(msgmodel);

            int count = idal3.GetTodayCount(EyouSoft.Model.ToolStructure.MsgType.AddFriend, EyouSoft.Model.ToolStructure.MsgSendWay.Email, "48075");

           
        }
    }
}
