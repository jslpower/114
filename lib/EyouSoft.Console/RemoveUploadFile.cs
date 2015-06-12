using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;
namespace EyouSoft.ConsoleApp
{
    /// <summary>
    /// 清除待删除文件清除
    /// </summary>
    public class RemoveUploadFile
    {
        public static void Run()
        {
            DeleteFileService service = new DeleteFileService();
            service.LoadDeleteFile();
        }
    }

    public class DeleteFileService : EyouSoft.Common.DAL.DALBase
    {
        string UploadFolder = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString(null, "UploadFolder");

        private void SaveState(string FilePath, int FileState)
        {
            DbCommand dc = this.SystemStore.GetSqlStringCommand("UPDATE tbl_SysDeletedFileQue SET FileState=@FileState WHERE FilePath=@FilePath");
            this.SystemStore.AddInParameter(dc, "FileState", DbType.Int32, FileState);
            this.SystemStore.AddInParameter(dc, "FilePath", DbType.String, FilePath);
            this.SystemStore.ExecuteNonQuery(dc);
        }

        public void LoadDeleteFile()
        {
            DataSet ds = new DataSet();
            DbCommand dc = this.SystemStore.GetSqlStringCommand("DELETE FROM view_DeletedFileQue_Repeat WHERE row_index>1;SELECT * FROM tbl_SysDeletedFileQue WHERE FileState=0");
            this.SystemStore.LoadDataSet(dc, ds, "DeletedFileQue");
            string FilePath = string.Empty;
            string FullPath = string.Empty;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int FileState = 0;
                if (dr["FilePath"] != DBNull.Value)
                {
                    FilePath = dr["FilePath"].ToString();
                    FullPath = UploadFolder + FilePath.Replace("/", "\\");
                    try
                    {
                        if (File.Exists(FullPath))
                        {
                            File.Delete(FullPath);
                            FileState = 1;
                        }
                        else
                        {
                            FileState = 3;
                        }
                    }
                    catch
                    {
                        FileState = 2;
                    }
                    finally
                    {
                        SaveState(FilePath, FileState);
                    }
                }
            }
        }
    }
}
