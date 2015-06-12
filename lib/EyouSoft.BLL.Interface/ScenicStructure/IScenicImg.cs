using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ScenicStructure;

namespace EyouSoft.IBLL.ScenicStructure
{
    /// <summary>
    /// 景区图片
    /// 创建者：郑付杰
    /// 创建时间：2011/10/28
    /// </summary>
    public interface IScenicImg
    {
        /// <summary>
        /// 添加景区图片
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Add(MScenicImg item);
        /// <summary>
        /// 修改图片
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Update(MScenicImg item);

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="ImgId">图片编号</param>
        /// <returns></returns>
        bool Delete(string ImgId);

        /// <summary>
        /// 获取景区图片
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <returns></returns>
        IList<MScenicImg> GetList(string scenicId);

        /// <summary>
        /// 获取景区图片
        /// </summary>
        /// <param name="Id">景区自增编号</param>
        /// <returns></returns>
        IList<MScenicImg> GetList(long Id);

        /// <summary>
        /// 获取景区形象图片(前台)
        /// </summary>
        /// <param name="topNum"></param>
        /// <returns></returns>
        IList<MScenicImg> GetList(int topNum);

        /// <summary>
        /// 指定条数获取景区图片
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        IList<MScenicImg> GetList(int topNum, string companyId, MScenicImgSearch search);

        /// <summary>
        /// 获取景区图片
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        IList<MScenicImg> GetList(int pageSize, int pageIndex, ref int recordCount,
            string companyId, MScenicImgSearch search);
    }
}
