using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Xml;
using EyouSoft.Common.DAL;
using EyouSoft.Model.VisaStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.VisaStructure
{
    /// <summary>
    /// 创建人：郑知远 2011-05-06
    /// 描述：旅游签证数据层
    /// </summary>
    public class VisaDal : DALBase, IDAL.VisaStructure.IVisaDal
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public VisaDal()
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region 前台运用成员方法
        /// <summary>
        /// 获取所有国家信息
        /// </summary>
        /// <returns>国家实体</returns>
        public IList<Country> GetCountryList()
        {
            IList<Country> lst = new List<Country>();                                                   // 国家实体
            var strSql = new StringBuilder();                                                           // SQL编辑器

            strSql.Append("	SELECT");
            strSql.Append("		ID");                                                                   // 国家编号
            strSql.Append("		,Areas");                                                               // 洲名
            strSql.Append("		,CountryCn");                                                           // 国家中文名
            strSql.Append("		,CountryEn");                                                           // 国家英文名
            strSql.Append("		,CountryJp");                                                           // 国家简拼
            strSql.Append("		,CountryCd");                                                           // 国家代码
            strSql.Append("		,FlagPath");                                                            // 国家国旗
            strSql.Append("	FROM");
            strSql.Append("		tbl_Country");

            DbCommand cmd = this._database.GetSqlStringCommand(strSql.ToString());                      // SQL执行

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._database))
            {
                while (dr.Read())
                {
                    // 国家实体
                    var model = new Country
                                    {
                                        Id = dr.GetInt32(dr.GetOrdinal("ID")),                          // 国家编号
                                        Areas = (Areas)dr.GetInt32(dr.GetOrdinal("Areas")),             // 洲名
                                        CountryCn = dr.GetString(dr.GetOrdinal("CountryCn")),           // 国家中文名
                                        CountryEn = dr.GetString(dr.GetOrdinal("CountryEn")),           // 国家英文名
                                        CountryJp = dr.GetString(dr.GetOrdinal("CountryJp")),           // 国家简拼
                                        CountryCd = dr.GetString(dr.GetOrdinal("CountryCd")),           // 国家代码
                                        FlagPath = dr.GetString(dr.GetOrdinal("FlagPath"))              // 国家国旗
                                    };

                    // 国家实体列表
                    lst.Add(model);
                }
            }

            // 返回国家信息实体
            return lst;
        }

        /// <summary>
        /// 根据热点国家中文名获取国家信息列表实体
        /// </summary>
        /// <param name="strHotCountrys">热点国家中文名</param>
        /// <returns>国家信息列表实体</returns>
        public IList<EyouSoft.Model.VisaStructure.Country> GetHotCountryListByName(string[] strHotCountrys)
        {
            IList<Country> lst = new List<Country>();                                                   // 国家实体
            var strSql = new StringBuilder();                                                           // SQL编辑器

            strSql.Append("	SELECT");
            strSql.Append("		ID");                                                                   // 国家编号
            strSql.Append("		,CountryCn");                                                           // 国家中文名
            strSql.Append("	FROM");
            strSql.Append("		tbl_Country");
            strSql.Append("	WHERE");
            strSql.Append("	    1=0");
            foreach (var s in strHotCountrys)
            {
                strSql.Append("		OR CountryCn LIKE '%" + s + "%'");
            }

            DbCommand cmd = this._database.GetSqlStringCommand(strSql.ToString());                      // SQL执行

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._database))
            {
                while (dr.Read())
                {
                    // 国家实体
                    var model = new Country
                                    {
                                        Id = dr.GetInt32(dr.GetOrdinal("ID")),                          // 国家编号
                                        CountryCn = dr.GetString(dr.GetOrdinal("CountryCn")),           // 国家中文名
                                    };

                    // 国家实体列表
                    lst.Add(model);
                }
            }

            // 返回国家信息实体
            return lst;
        }

        /// <summary>
        /// 根据国家ID获取旅游签证信息
        /// </summary>
        /// <param name="countryId">国家ID</param>
        /// <returns>旅游签证信息实体</returns>
        public Visa GetVisaInfoByCountry(int countryId)
        {
            var model = new Visa();                                                                     // 签证信息实体
            var strSql = new StringBuilder();                                                           // SQL编辑器

            strSql.Append(" SELECT");
            strSql.Append("     A.EmbassyInfo");                                                        // 使馆信息
            strSql.Append("     ,A.HintInfo");                                                          // 特别提示
            strSql.Append("     ,A.FilePath");                                                          // 申请书下载地址
            strSql.Append("	    ,VisaFile = (SELECT");                                                  // 签证资料
            strSql.Append("					    FileType");
            strSql.Append("					    ,FileInfo");
            strSql.Append("				     FROM");
            strSql.Append("					    tbl_VisaSub");
            strSql.Append("				     WHERE");
            strSql.Append("					    CountryID = A.CountryID FOR XML PATH('File'),ROOT('Files'))");
            strSql.Append("	    ,Link = (SELECT");                                                      // 站点信息
            strSql.Append("				    LinkTile");
            strSql.Append("				    ,LinkUrl");
            strSql.Append("			     FROM");
            strSql.Append("				    tbl_VisaLink");
            strSql.Append("			     WHERE");
            strSql.Append("				    CountryID = A.CountryID FOR XML PATH('Link'),ROOT('Links'))");
            strSql.Append(" FROM");
            strSql.Append("     tbl_Visa AS A");
            strSql.Append(" WHERE");
            strSql.Append("     A.CountryID = @CountryID");

            DbCommand cmd = this._database.GetSqlStringCommand(strSql.ToString());                      // SQL执行
            this._database.AddInParameter(cmd, "@CountryID", DbType.Int32, countryId);                  // 国家ID

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._database))
            {
                while (dr.Read())
                {
                    // 使馆信息
                    model.EmbassyInfo = dr.IsDBNull(dr.GetOrdinal("EmbassyInfo"))
                                            ? string.Empty
                                            : dr.GetString(dr.GetOrdinal("EmbassyInfo"));

                    // 特别提示
                    model.HintInfo = dr.IsDBNull(dr.GetOrdinal("HintInfo"))
                                         ? string.Empty
                                         : dr.GetString(dr.GetOrdinal("HintInfo"));

                    // 申请书下载地址
                    model.FilePath = dr.IsDBNull(dr.GetOrdinal("FilePath"))
                                         ? string.Empty
                                         : dr.GetString(dr.GetOrdinal("FilePath"));

                    // 签证资料
                    model.FileInfos = new List<File>();
                    if (!dr.IsDBNull(dr.GetOrdinal("VisaFile")))
                    {
                        // 实例化xml
                        var xml = new XmlDocument();

                        // 读取xml文件
                        xml.LoadXml(dr.GetString(dr.GetOrdinal("VisaFile")));

                        foreach (XmlNode nodeP in xml.ChildNodes[0].ChildNodes)
                        {
                            // 所需签证资料实体
                            var cls = new File();

                            foreach (XmlNode nodeT in nodeP.ChildNodes)
                            {
                                switch (nodeT.Name)
                                {
                                    case "FileType":
                                        cls.FileType = (FileType)Enum.Parse(typeof(FileType), nodeT.InnerText);
                                        break;
                                    case "FileInfo":
                                        cls.FileInfo = nodeT.InnerText;
                                        break;
                                }
                            }

                            // 所需签证资料信息追加
                            model.FileInfos.Add(cls);
                        }
                    }

                    // 站点信息
                    model.Links = new List<Link>();
                    if (!dr.IsDBNull(dr.GetOrdinal("Link")))
                    {
                        // 实例化xml
                        var xml = new XmlDocument();

                        // 读取xml文件
                        xml.LoadXml(dr.GetString(dr.GetOrdinal("Link")));

                        foreach (XmlNode nodeP in xml.ChildNodes[0].ChildNodes)
                        {
                            // 站点信息实体
                            var cls = new Link();

                            foreach (XmlNode nodeT in nodeP.ChildNodes)
                            {
                                switch (nodeT.Name)
                                {
                                    case "LinkTile":
                                        cls.LinkTile = nodeT.InnerText;
                                        break;
                                    case "LinkUrl":
                                        cls.LinkUrl = nodeT.InnerText;
                                        break;
                                }
                            }

                            // 站点信息实体追加
                            model.Links.Add(cls);
                        }
                    }
                }
            }

            // 返回签证资料信息实体
            return model;
        }
        #endregion
    }
}
