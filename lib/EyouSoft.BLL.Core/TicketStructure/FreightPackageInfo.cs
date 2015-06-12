using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.TicketStructure
{
    /// <summary>
    /// 运价套餐
    /// </summary>
    /// Author:罗丽娥  2010-10-29
    public class FreightPackageInfo : EyouSoft.IBLL.TicketStructure.IFreightPackageInfo
    {
        private readonly EyouSoft.IDAL.TicketStructure.IFreightPackageInfo idal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TicketStructure.IFreightPackageInfo>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TicketStructure.IFreightPackageInfo CreateInstance()
        {
            EyouSoft.IBLL.TicketStructure.IFreightPackageInfo op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.TicketStructure.IFreightPackageInfo>();
            }
            return op;
        }

        /// <summary>
        /// 添加套餐信息
        /// </summary>
        /// <param name="model">运价套餐实体</param>
        /// <returns>true:成功  false:失败</returns>
        public bool Add(EyouSoft.Model.TicketStructure.TicketFreightPackageInfo model)
        {
            return idal.Add(model);
        }

        /// <summary>
        /// 修改套餐信息
        /// </summary>
        /// <param name="model">运价套餐实体</param>
        /// <returns>true:成功  false:失败</returns>
        public bool Update(EyouSoft.Model.TicketStructure.TicketFreightPackageInfo model)
        {
            return idal.Update(model);
        }

        /// <summary>
        /// 删除套餐信息
        /// </summary>
        /// <param name="Id">套餐Id</param>
        /// <returns>true:成功   false:失败</returns>
        public bool Delete(int Id)
        {
            return idal.Delete(Id);
        }

        /// <summary>
        /// 获取套餐实体
        /// </summary>
        /// <param name="Id">套餐Id</param>
        /// <returns>运价套餐实体</returns>
        public EyouSoft.Model.TicketStructure.TicketFreightPackageInfo GetModel(int Id)
        {
            return idal.GetModel(Id);
        }

        /// <summary>
        /// 获取套餐列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="PackageTypes">套餐类型 =null返回全部</param>
        /// <param name="IsDeleted">是否已删除 =null返回全部</param>
        /// <returns>套餐运价信息列表</returns>
        public IList<EyouSoft.Model.TicketStructure.TicketFreightPackageInfo> GetList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TicketStructure.PackageTypes? PackageTypes, bool? IsDeleted)
        {
            return idal.GetList(pageSize, pageIndex, ref recordCount, PackageTypes, IsDeleted);
        }
    }
}
