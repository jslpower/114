using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CommunityStructure
{
    /// <summary>
    /// 二次整改供求实体基类
    /// </summary>
    /// 创建人：mk 2011-5-10
    public class MSupplyDemandBase
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkAddress
        {
            get;
            set;
        }

        /// <summary>
        /// 类别
        /// </summary>
        public CategoryEnum Category
        {
            get;
            set;
        }

        /// <summary>
        /// 操作人
        /// </summary>
        public int OperateId
        {
            get;
            set;
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 供求规则类
    /// </summary>
    public class MSupplyDemandRule : MSupplyDemandBase
    {
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string NewsTitle
        {
            get;
            set;
        }

        /// <summary>
        /// 新闻内容
        /// </summary>
        public string NewsContent
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 供求图片类
    /// </summary>
    public class MSupplyDemandPic : MSupplyDemandBase
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PicPath
        {
            get;
            set;
        }
    }

    public enum CategoryEnum
    {
        供求规则 = 0,
        供求图片 = 1
    }
}
