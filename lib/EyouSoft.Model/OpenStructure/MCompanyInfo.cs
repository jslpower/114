/************************************************************
 * 模块名称：公司关系实体
 * 功能说明：各平台数据同步关系公司关系实体
 * 创建人：周文超  2011-4-2 15:20:08
 * *********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.OpenStructure
{
    /// <summary>
    /// 基本信息业务实体
    /// </summary>
    public class MBaseCompany
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MBaseCompany() { }

        /// <summary>
        /// 系统类型
        /// </summary>
        public int SystemType { get; set; }

        /// <summary>
        /// 平台公司编号
        /// </summary>
        public string PlatformCompanyId { get; set; }
    }


    #region 公司基本信息业务实体

    /// <summary>
    /// 公司基本信息业务实体
    /// </summary>
    /// <remarks>
    /// 给系统公司编号与平台公司编号赋值按发送指令系统来确定：
    /// 其它系统（非114平台）时为系统公司编号赋值；
    /// 大平台（114平台）时为平台公司编号赋值；
    /// </remarks>
    /// Author:汪奇志 DateTime:2011-04-01
    public class MCompanyInfo : MBaseCompany
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MCompanyInfo() { }

        /// <summary>
        /// 系统公司编号，其它系统（非114平台）时赋值
        /// </summary>
        public int SystemCompanyId { get; set; }

        /// <summary>
        /// 系统公司类型
        /// </summary>
        public int SystemCompanyType { get; set; }
    }

    #endregion
}
