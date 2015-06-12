using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.TicketStructure
{
    /// <summary>
    /// 特价机票业务处理类
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public class SpecialFares:EyouSoft.IBLL.TicketStructure.ISpecialFares
    {
        #region 私有变量
        private readonly EyouSoft.DAL.TicketStructure.SpecialFares dal = new EyouSoft.DAL.TicketStructure.SpecialFares();
        #endregion
        #region 构造函数
        public SpecialFares() { }
        #endregion
        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TicketStructure.ISpecialFares CreateInstance()
        {
            EyouSoft.IBLL.TicketStructure.ISpecialFares op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.TicketStructure.ISpecialFares>();
            }
            return op;
        }

        #region ISpecialFares 成员

        /// <summary>
        /// 添加特价机票信息
        /// </summary>
        /// <param name="item">特价机票实体对象</param>
        /// <returns>true:成功 false:失败</returns>
        public bool AddSpecialFares(EyouSoft.Model.TicketStructure.SpecialFares item)
        {
            if (item == null)
                return false;
            return dal.AddSpecialFares(item);
        }
        /// <summary>
        /// 修改特价机票信息
        /// </summary>
        /// <param name="item">特价机票实体对象</param>
        /// <returns>true:成功 false:失败</returns>
        public bool ModifySpecialFares(EyouSoft.Model.TicketStructure.SpecialFares item)
        {
            if (item == null)
                return false;
            return dal.ModifySpecialFares(item);
        }
        /// <summary>
        /// 单个/批量删除特价机票信息
        /// </summary>
        /// <param name="ids">特价机票编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool DeleteSpecialFares(params int[] ids)
        {
            StringBuilder idsText = new StringBuilder();
            if (ids == null || ids.Length < 1)
                return false;
            for (int i = 0; i < ids.Length; i++)
            {
                idsText.Append(ids[i]);
                if (i + 1 < ids.Length)
                    idsText.Append(",");
            }

            return dal.DeleteSpecialFares(idsText.ToString());
        }
        /// <summary>
        /// 根据编号获取特价机票信息
        /// </summary>
        /// <param name="id">特价机票编号</param>
        /// <returns>特价机票实体对象</returns>
        public EyouSoft.Model.TicketStructure.SpecialFares GetSpecialFare(int id)
        {
            return dal.GetSpecialFare(id);
        }
        /// <summary>
        /// 指定条数获取特价机票信息
        /// </summary>
        /// <param name="topNum">获取数量</param>
        /// <returns>特价机票集合</returns>
        public IList<EyouSoft.Model.TicketStructure.SpecialFares> GetTopSpecialFares(int topNum)
        {
            if (topNum < 1)
                return null;
            return dal.GetTopSpecialFares(topNum);
        }
        /// <summary>
        /// 分页获取特价机票信息
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>特价机票集合</returns>
        public IList<EyouSoft.Model.TicketStructure.SpecialFares> GetSpecialFares(int pageSize, int pageIndex, ref int recordCount)
        {
            if (pageSize < 1 || pageIndex < 1)
                return null;
            return dal.GetSpecialFares(pageSize, pageIndex, ref recordCount);
        }

        #endregion
    }
}
