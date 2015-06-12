using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 单位区域设置实体类
    /// </summary>
    /// 创建人：张志瑜 2010-05-31
    [Serializable]
    public class CompanyAreaSetting
    {
        private string _companyid = "";
        private int _areaid = 0;
        private string _prefixtext = "";
        private bool _isshowordersite = false;

        /// <summary>
        /// 所属公司编号
        /// </summary>
        public string CompanyID { get { return this._companyid; } set { this._companyid = value; } }
        /// <summary>
        /// 线路区域编号
        /// </summary>
        public int AreaID { get { return this._areaid; } set { this._areaid = value; } }
        /// <summary>
        /// 团号前缀
        /// </summary>
        public string PrefixText { get { return this._prefixtext; } set { this._prefixtext = value; } }
        /// <summary>
        /// 是否显示座位
        /// </summary>
        public bool IsShowOrderSite { get { return this._isshowordersite; } set { this._isshowordersite = value; } }
    }
}
