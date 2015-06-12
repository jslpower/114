using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CommunityStructure
{
    /// <summary>
    /// 顾问团队实体
    /// </summary>
    /// 周文超 2010-07-16
    public class CommunityAdvisor
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommunityAdvisor() { }

        /// <summary>
        /// 顾问团队编号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// 性别 true or 1为男,false or 0为女
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string ContactTel { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// 照片上传
        /// </summary>
        public string ImgPath { get; set; }

        /// <summary>
        /// 主要成就
        /// </summary>
        public string Achieve { get; set; }

        /// <summary>
        /// 重要荣誉
        /// </summary>
        public string Honour { get; set; }

        /// <summary>
        /// 是否审核 1为审核通过
        /// </summary>
        public bool IsCheck { get; set; }

        /// <summary>
        /// 是否前台显示 1为显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 发布人编号
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 后台操作员编号
        /// </summary>
        public int SysOperatorId { get; set; }
    }
}
