/************************************************************
 * 模块名称：用户关系实体
 * 功能说明：各平台数据同步关系用户关系实体
 * 创建人：周文超  2011-4-2 15:30:56
 * *********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.OpenStructure
{
    #region 用户基本信息业务实体

    /// <summary>
    /// 用户基本信息业务实体
    /// </summary>
    /// <remarks>
    /// 给系统用户编号与平台用户编号赋值按发送指令系统来确定：
    /// 其它系统（非114平台）时为系统用户编号赋值；
    /// 大平台（114平台）时为平台用户编号赋值；
    /// </remarks>
    /// Author:汪奇志 DateTime:2011-04-01
    public class MUserInfo : MBaseCompany
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MUserInfo() { }

        /// <summary>
        /// 系统用户编号
        /// </summary>
        public int SystemUserId { get; set; }

        /// <summary>
        /// 平台用户编号
        /// </summary>
        public string PlatformUserId { get; set; }

    }

    #endregion
}
