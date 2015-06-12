using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ScenicStructure;

namespace EyouSoft.BLL.ScenicStructure
{
    /// <summary>
    /// 景区图片
    /// 创建者：郑付杰
    /// 创建时间：2011/10/28
    /// </summary>
    public class BScenicImg:EyouSoft.IBLL.ScenicStructure.IScenicImg
    {
        private readonly IDAL.ScenicStructure.IScenicImg dal =
            Component.Factory.ComponentFactory.CreateDAL<IDAL.ScenicStructure.IScenicImg>();

        public static IBLL.ScenicStructure.IScenicImg CreateInstance()
        {
            IBLL.ScenicStructure.IScenicImg op = null;
            if (op == null)
            {
                op= Component.Factory.ComponentFactory.Create<IBLL.ScenicStructure.IScenicImg>();
            }
            return op;
        }

        #region IScenicImg 成员
        /// <summary>
        /// 添加景区图片
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(MScenicImg item)
        {
            bool result = false;
            if (item != null && !string.IsNullOrEmpty(item.CompanyId) && !string.IsNullOrEmpty(item.Address))
            {
                item.ImgId = Guid.NewGuid().ToString();
                result = dal.Add(item);
            }
            return result;
        }
        /// <summary>
        /// 修改图片
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Update(MScenicImg item)
        {
            bool result = false;
            if (item != null && !string.IsNullOrEmpty(item.Address) 
                && !string.IsNullOrEmpty(item.ImgId))
            {
                result = dal.Update(item);
            }
            return result;
        }
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="ImgId">图片编号</param>
        /// <returns></returns>
        public bool Delete(string ImgId)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(ImgId))
            {
                result = dal.Delete(ImgId);
            }
            return result;
        }
        /// <summary>
        /// 获取景区图片
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <returns></returns>
        public IList<MScenicImg> GetList(string scenicId)
        {
            IList<MScenicImg> list = null;
            if (!string.IsNullOrEmpty(scenicId))
            {
                list = dal.GetList(scenicId);
            }
            return list;
        }


         /// <summary>
        /// 获取景区图片
        /// </summary>
        /// <param name="Id">景区自增编号</param>
        /// <returns></returns>
        public IList<MScenicImg> GetList(long Id)
        {
            IList<MScenicImg> list = null;
            if (Id > 0)
            {
                list = dal.GetList(Id);
            }
            return list;
        }

        /// <summary>
        /// 获取景区形象图片(前台)
        /// </summary>
        /// <param name="topNum"></param>
        /// <returns></returns>
        public IList<MScenicImg> GetList(int topNum)
        {
            topNum = topNum < 1 ? 4 : topNum;
            return dal.GetList(topNum);
        }

        /// <summary>
        /// 指定条数获取景区图片
        /// </summary>
        /// <param name="topNum">获取条数(小于0则获取所有)</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public IList<MScenicImg> GetList(int topNum, string companyId, MScenicImgSearch search)
        {
            IList<MScenicImg> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetList(topNum, companyId, search);
            }
            return list;
        }
        /// <summary>
        /// 获取景区图片
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public IList<MScenicImg> GetList(int pageSize, int pageIndex, ref int recordCount,
            string companyId, MScenicImgSearch search)
        {
            IList<MScenicImg> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                list = dal.GetList(pageSize, pageIndex, ref recordCount,companyId, search);
            }
            return list;
        }

        #endregion
    }
}
