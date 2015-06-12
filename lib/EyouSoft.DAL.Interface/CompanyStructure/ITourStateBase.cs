using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace EyouSoft.IDAL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-12
    /// 描述：推广类型接口
    /// </summary>
    public interface ITourStateBase
    {
        /// <summary>
        /// 修改推广类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns>false:失败 true:成功</returns>
        bool Update(int id, string text);
        /// <summary>
        /// 获取推广类型实体
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>推广类型实体</returns>
        EyouSoft.Model.CompanyStructure.TourStateBase GetModel(int id);
        /// <summary>
        /// 验证指定公司是否存在指定的推广类型
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="text"></param>
        /// <returns>true:存在 false:不存在</returns>
        bool Exists(string CompanyID, string text);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        bool Delete(int id);

        /// <summary>
        /// 获取指定公司的所有推广类型
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.CompanyStructure.TourStateBase> GetList(string CompanyID);
    }
}
