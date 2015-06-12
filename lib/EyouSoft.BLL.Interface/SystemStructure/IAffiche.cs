using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SystemStructure;
using EyouSoft.AOPHandler;

namespace EyouSoft.IBLL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-12
    /// 描述：公告信息业务逻辑接口
    /// </summary>
    public interface IAffiche
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">公告实体</param>
        /// <returns>false:失败 true:成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Affiche_Add_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Affiche_Add,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""AfficheClass"",""AttributeType"":""class""},{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Affiche_Add_CODE)]
        bool Add(Affiche model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">公告实体</param>
        /// <returns>false:失败 true:成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Affiche_Edit_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Affiche_Edit,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""AfficheClass"",""AttributeType"":""class""},{""Index"":0,""Attribute"":""ID"",""AttributeType"":""class""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Affiche_Edit_CODE)]
        bool Update(Affiche model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">公告实体</param>
        /// <returns>false:失败 true:成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Affiche_Del_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Affiche_Del,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""id"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Affiche_Del_CODE)]
        bool Delete(int id);

        /// <summary>
        /// 获取指定行数的新闻信息
        /// </summary>
        /// <param name="topNumber">需要返回的行数 =0返回全部</param>
        /// <param name="affichType">新闻类别 =null返回全部</param>
        /// <returns></returns>
        IList<Affiche> GetTopList(int topNumber, EyouSoft.Model.SystemStructure.AfficheType? affichType);
        /// <summary>
        /// 获取新闻实体
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        EyouSoft.Model.SystemStructure.Affiche GetModel(int ID);
        /// <summary>
        /// 分页获取新闻列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="affichType">新闻类别 =null返回全部</param>
        /// <returns></returns>
        IList<Affiche> GetList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.SystemStructure.AfficheType? affichType);

        /// <summary>
        /// 置顶
        /// </summary>
        /// <returns>false:失败 true:成功</returns>
        [CommonLogHandler(
            LogTitle = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Affiche_SetIsTop_Title,
            LogMessage = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Affiche_SetIsTop,
            LogWriterType = typeof(EyouSoft.BusinessLogWriter.WebMasterLog),
            LogAttribute = @"[{""Index"":0,""Attribute"":""id"",""AttributeType"":""val""}]",
            EventCode = EyouSoft.BusinessLogWriter.WebMasterLog.LOG_Affiche_SetIsTop_CODE)]
        bool SetTop(int id, bool istop);

        /// <summary>
        /// 更新浏览数
        /// </summary>
        /// <param name="id"></param>
        /// <returns>false:失败 true:成功</returns>
        bool UpdateReadCount(int id);
    }
}
