using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SystemStructure;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-12
    /// 描述：首页轮换广告业务逻辑接口
    /// </summary>
    public interface ISysIndexAdv
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns>false:失败 true:成功</returns>
        bool Add(SysIndexAdv model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">首页广告实体</param>
        /// <returns>false:失败 true:成功</returns>
        bool Update(SysIndexAdv model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns>false:失败 true:成功</returns>
        bool Delete(int id);

        /// <summary>
        /// 运营后台分页获取列表
        /// </summary>
        /// <param name="siteId">所属分站ID >0时返回该分站下的下广告 =0时返回全部</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordeCount">总记录数</param>
        /// <returns></returns>
        IList<SysIndexAdv> GetList(int siteId, int pageSize, int pageIndex, ref int recordeCount);
        ///<summary>
        /// 获取指定条数的前台轮换广告列表
        /// </summary>
        /// <param name="siteId">分站ID >0时返回该分站下的轮换广告信息 =0时返回全部</param>
        /// <param name="topnumber">指定返回条数 >0时返回指定条数记录 =0返回全部</param>
        /// <returns></returns>
        IList<SysIndexAdv> GetTopList(int siteId, int topnumber);
        /// <summary>
        /// 获取广告实体
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>返回广告实体</returns>
        SysIndexAdv GetModel(int id);
        /// <summary>
        /// 设置排序
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <param name="sortnumber">排序值</param>
        /// <returns>false:失败 true:成功</returns>
        bool SetSort(int id, int sortnumber);
    }
}
