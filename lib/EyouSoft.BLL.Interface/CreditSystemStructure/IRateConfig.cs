using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.CreditSystemStructure
{
    /// <summary>
    /// 创建人:张志瑜 2010-07-02
    /// 描述:诚信体系-诚信体系的配置接口层
    /// </summary>
    public interface IRateConfig
    {
        /// <summary>
        /// 获得诚信体系的配置信息
        /// </summary>
        EyouSoft.Model.CreditSystemStructure.RateConfig GetRateConfig();
    }
}
