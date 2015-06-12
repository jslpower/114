using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-11
    /// 描述：互动平台信息—互动交流列表
    /// </summary>
    public class ExchangeList
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExchangeList() { }

        #region 属性
       /// <summary>
        /// 编号
       /// </summary>
       public string ID
       {
           get;
           set;
       }
       /// <summary>
       /// 互动栏目类别ID
       /// </summary>
       public int TopicClassID
       {
           get;
           set;
       }
       /// <summary>
       /// 互动栏目类别名称
       /// </summary>
       public string TopicClassName
       {
           get;
           set;
       }
       /// <summary>
       /// 互动标题
       /// </summary>
       public string ExchangeTitle
       {
           get;
           set;
       }
       /// <summary>
       /// 互动内容
       /// </summary>
       public string ExchangeText
       {
           get;
           set;
       }
       /// <summary>
       /// 回复次数(为所有的回复次数)
       /// </summary>
       public int WriteBackCount
       {
           get;
           set;
       }
       /// <summary>
       /// 浏览次数
       /// </summary>
       public int ViewCount
       {
           get;set;
       }
       /// <summary>
       /// 回复和浏览的总次数
       /// </summary>
       public int BackViewCount
       {
           get;set;
       }
       /// <summary>
       /// 发布人公司ID
       /// </summary>
       public string ReleaseCompanyId
       { 
           get; set; 
       }
       /// <summary>
       /// 发布人公司名称
       /// </summary>
       public string ReleaseCompanyName 
       { 
           get; set;
       }
       /// <summary>
       /// 发布人用户ID
       /// </summary>
       public string ReleaseOperatorId 
       { 
           get; set;
       }
       /// <summary>
       /// 发布人姓名
       /// </summary>
       public string ReleaseOperatorName 
       { 
           get; set; 
       }
       /// <summary>
       /// 发布人MQ号码
       /// </summary>
       public string ReleaseOperatorMQ 
       { 
           get; set;
       }
       /// <summary>
       /// 发布时间(默认为getdate())
       /// </summary>
       public DateTime IssueTime 
       { 
           get; set; 
       }
       /// <summary>
       /// 是否置顶(1:置顶  0:非置顶 ,默认为0)
       /// </summary>
       public bool IsTop 
       { 
           get; set;
       }
       /// 置顶时间(默认为null)
       public DateTime topTime 
       { 
           get; set; 
       }
       /// <summary>
       /// 发布到的省份ID
       /// </summary>
       public int ToProvinceId 
       {
           get; set; 
       }
       /// <summary>
       /// 发布到的省份名称
       /// </summary>
       public string ToProvinceName 
       {
           get; set; 
       }
       #endregion
    }
}
