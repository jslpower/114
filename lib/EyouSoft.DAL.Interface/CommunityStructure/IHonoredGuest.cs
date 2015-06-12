using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-14
    /// 描述：嘉宾访谈数据层接口
    /// </summary>
    public interface IHonoredGuest
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">嘉宾访谈实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(EyouSoft.Model.CommunityStructure.HonoredGuest model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">嘉宾访谈实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Update(EyouSoft.Model.CommunityStructure.HonoredGuest model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids">主键编号字符串（，分割）</param>
        /// <returns>true:成功 false:失败</returns>
        bool Delete(params string[] Ids);
        /// <summary>
        /// 获取最新一期的嘉宾访谈
        /// </summary>
        /// <returns>嘉宾访谈实体</returns>
        EyouSoft.Model.CommunityStructure.HonoredGuest GetNewInfo();
        /// <summary>
        /// 获取嘉宾访谈实体
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        EyouSoft.Model.CommunityStructure.HonoredGuest GetModel(string Id);
        /// <summary>
        /// 获取指定条数的嘉宾访谈列表集合
        /// </summary>
        /// <param name="topNumber">需要返回的记录数 =0返回全部</param>
        /// <returns>嘉宾访谈列表集合</returns>
        IList<EyouSoft.Model.CommunityStructure.HonoredGuest> GetTopNumList(int topNumber);
        /// <summary>
        /// 分页获取嘉宾访谈列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <returns>嘉宾访谈列表集合</returns>
        IList<EyouSoft.Model.CommunityStructure.HonoredGuest> GetPageList(int pageSize, int pageIndex, ref int recordCount,string KeyWord);
    }
}
