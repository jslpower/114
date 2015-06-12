using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源
    /// 创建时间：2010-05-11
    /// 描述：基础数据-包含项目实体类
    /// </summary>
    public class ServiceStandard
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceStandard() { }
        #endregion

        #region 属性
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// 类型编号
        /// </summary>
        public ServiceTypes TypeID
        {
            get;
            set;
        }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public string OperatorID
        {
            get;
            set;
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get;
            set;
        }
        #endregion
    }
    #region 包含项目类别枚举
    /// <summary>
    /// 包含项目类别枚举
    /// </summary>
    public enum ServiceTypes
    {
        /// <summary>
        /// 住宿
        /// </summary>
        住宿 = 1,
        /// <summary>
        /// 用餐
        /// </summary>
        用餐,
        /// <summary>
        /// 景点
        /// </summary>
        景点,
        /// <summary>
        /// 用车
        /// </summary>
        用车,
        /// <summary>
        /// 导服
        /// </summary>
        导服,
        /// <summary>
        /// 往返大交通
        /// </summary>
        往返大交通,
        /// <summary>
        /// 其他
        /// </summary>
        其他,
        ///// <summary>
        ///// 不包含项目
        ///// </summary>
        //不包含项目,
        ///// <summary>
        ///// 儿童安排
        ///// </summary>
        //儿童安排,
        ///// <summary>
        ///// 购物安排
        ///// </summary>
        //购物安排,
        ///// <summary>
        ///// 赠送项目
        ///// </summary>
        //赠送项目,
        /// <summary>
        /// 其他说明
        /// </summary>
        其他说明,
        /// <summary>
        /// 备注
        /// </summary>
        备注,
        /// <summary>
        /// 集合方式
        /// </summary>
        集合方式,
        /// <summary>
        /// 接团方式
        /// </summary>
        接团方式

    }
    #endregion
}
