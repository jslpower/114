using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.IBLL;
using EyouSoft.Component.Factory;
using EyouSoft.IDAL;
using EyouSoft.Model;

namespace EyouSoft.BLL.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-07-02
    /// 描述:诚信体系-诚信体系的配置业务逻辑层
    /// </summary>
    public class RateConfig : EyouSoft.IBLL.CreditSystemStructure.IRateConfig
    {
        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CreditSystemStructure.IRateConfig CreateInstance()
        {
            EyouSoft.IBLL.CreditSystemStructure.IRateConfig op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<EyouSoft.IBLL.CreditSystemStructure.IRateConfig>();
            }
            return op;
        }

        /// <summary>
        /// 获得诚信体系的配置信息
        /// </summary>
        public EyouSoft.Model.CreditSystemStructure.RateConfig GetRateConfig()
        {
            EyouSoft.Model.CreditSystemStructure.RateConfig model = (EyouSoft.Model.CreditSystemStructure.RateConfig)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.Company.RateConfig);

            if (model == null)  //缓存中无数据
            {
                //从配置文件中读取
                model = GetWebConfig();
                if (model != null) //写入缓存
                {
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.Company.RateConfig, model);
                }
            }

            return model;
        }

        /// <summary>
        /// 从配置文件中获取配置信息
        /// </summary>
        /// <returns></returns>
        private EyouSoft.Model.CreditSystemStructure.RateConfig GetWebConfig()
        {
            EyouSoft.Model.CreditSystemStructure.RateConfig model = new EyouSoft.Model.CreditSystemStructure.RateConfig();
            string strRateConfig = System.Configuration.ConfigurationManager.AppSettings["RateConfig"];
            if (!string.IsNullOrEmpty(strRateConfig))
            {
                string[] arrVal = strRateConfig.Split(",".ToCharArray());
                if (arrVal != null && arrVal.Length > 0)
                {
                    foreach (string val in arrVal)
                    {
                        if (!string.IsNullOrEmpty(val))
                        {
                            string[] arrItem = val.Split(":".ToCharArray());
                            if (arrItem != null && arrItem.Length > 1)
                            {
                                string ItemValue = arrItem[1];
                                switch (arrItem[0])
                                {
                                    case "Login":
                                        model.LoginScore = double.Parse(ItemValue);
                                        break;
                                    case "HoldUp":
                                        model.HoldUpScore = double.Parse(ItemValue);
                                        break;
                                    case "OrderBuy":
                                        model.OrderBuyScore = double.Parse(ItemValue);
                                        break;
                                    case "Certificate":
                                        model.CertificateScore = double.Parse(ItemValue);
                                        break;
                                    case "RateExpire":
                                        model.RateExpireday = Int32.Parse(ItemValue);
                                        break;
                                    case "RateGood":
                                        model.RateGoodScore = double.Parse(ItemValue);
                                        break;
                                    case "RateMiddle":
                                        model.RateMiddleScore = double.Parse(ItemValue);
                                        break;
                                    case "RateBad":
                                        model.RateBadScore = double.Parse(ItemValue);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            return model;
        }
    }
}
