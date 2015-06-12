using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Function;
using EyouSoft.Common;

namespace SiteOperationsCenter.NewsCenterControl
{
    /// <summary>
    ///  资讯类别管理
    ///  author:zhengfj date:2011-4-1
    /// </summary>
    public partial class Category : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        private static EyouSoft.Model.NewsStructure.NewsType newsTypeModel = new EyouSoft.Model.NewsStructure.NewsType();
        private readonly EyouSoft.BLL.NewsStructure.NewsType newsTypeBLL = new EyouSoft.BLL.NewsStructure.NewsType();
        #endregion

        #region page_load

        protected void Page_Load(object sender, EventArgs e)
        {

            YuYingPermission[] parms = { YuYingPermission.平台管理_管理该栏目, YuYingPermission.新闻类别管理_管理该栏目 };
            if (!CheckMasterGrant(parms))
            {
                Utils.ResponseNoPermit(YuYingPermission.新闻类别管理_管理该栏目, true);
                return;
            }
            if (!IsPostBack)
            {
                btnAdd.CommandName = "Add"; 
                btnSchoolAdd.CommandName = "Add";
                switch (Request.QueryString["type"])
                {
                    case "edit":   //资讯类别编辑操作
                        EditCategory();
                        break;
                    case "sedit":  //同业学堂编辑操作
                        EditSchoolCategory();
                        break;
                    default:
                        break;
                }
                Databind();
                SchoolDatabind();
            }
        }

        #endregion

        #region 获取行业资讯分类

        public void Databind()
        {
            IList<EyouSoft.Model.NewsStructure.NewsType> list = newsTypeBLL.GetInformationType();
            repList.DataSource = list;
            repList.DataBind();
        }

        #endregion

        #region 同业学堂

        public void SchoolDatabind()
        {
            IList<EyouSoft.Model.NewsStructure.NewsType> list = newsTypeBLL.GetSchoolType();
            repSchooolList.DataSource = list;
            repSchooolList.DataBind();
        }

        #endregion

        #region 编辑

        private void EditCategory()
        {
            txtCateName.Text = Request.QueryString["name"];
            btnAdd.CommandName = "Edit";
        }

        private void EditSchoolCategory()
        {
            txtSchoolName.Text = Request.QueryString["name"];
            btnSchoolAdd.CommandName = "Edit";
        }
        #endregion

        /// <summary>
        /// 添加/编辑行业资讯类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            string cateName =  txtCateName.Text.Replace(" ", "");
            try
            {
                bool result = false;
                newsTypeModel = new EyouSoft.Model.NewsStructure.NewsType()
                {
                    ClassName = cateName,
                    Category = EyouSoft.Model.NewsStructure.NewsCategory.行业资讯,
                    OperatorId = base.MasterUserInfo.ID
                };
                if (btnAdd.CommandName.Equals("Add"))  //添加操作
                {
                    if (newsTypeBLL.IsExists(cateName, 0))  //检测类别是否存在
                    {
                        MessageBox.Show(this, "该资讯类别已经存在，请重新输入!!!");
                        return;
                    }
                    result = newsTypeBLL.Add(newsTypeModel);
                }
                else if (btnAdd.CommandName.Equals("Edit"))
                {
                    int ID;
                    result = int.TryParse(Request.QueryString["id"], out ID);
                    if (result)
                    {
                        if (newsTypeBLL.IsExists(cateName, ID))  //检测类别是否存在
                        {
                            MessageBox.Show(this, "该资讯类别已经存在，请重新输入!!!");
                            return;
                        }
                        newsTypeModel.Id = ID;
                        result = newsTypeBLL.Update(newsTypeModel);
                    }
                }
                if (result)
                {
                    MessageBox.ShowAndRedirect(this, "行业资讯类别操作成功", "Category.aspx");
                }
                else
                {
                    MessageBox.Show(this, "操作失败");
                }
                

            }
            catch (Exception)
            {
                MessageBox.Show(this,"操作失败");
            }
        }

        /// <summary>
        /// 添加/编辑同业学堂类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSchoolAdd_Click(object sender, EventArgs e)
        {
            string cateName = txtSchoolName.Text.Replace(" ", "");
            
            try
            {
                bool result = false;
                newsTypeModel = new EyouSoft.Model.NewsStructure.NewsType()
                {
                    ClassName = cateName,
                    Category = EyouSoft.Model.NewsStructure.NewsCategory.同业学堂,
                    OperatorId = base.MasterUserInfo.ID
                };
                if (btnSchoolAdd.CommandName.Equals("Add"))  //添加操作
                {
                    if (newsTypeBLL.SchoolExists(cateName, 0))  //检测类别是否存在
                    {
                        MessageBox.Show(this, "该同业学堂类别已经存在，请重新输入!!!");
                        return;
                    }
                    result = newsTypeBLL.Add(newsTypeModel);
                }
                else if (btnSchoolAdd.CommandName.Equals("Edit"))
                {
                    int ID;
                    result = int.TryParse(Request.QueryString["id"], out ID);
                    if (result)
                    {
                        if (newsTypeBLL.SchoolExists(cateName, ID))  //检测类别是否存在
                        {
                            MessageBox.Show(this, "该同业学堂类别已经存在，请重新输入!!!");
                            return;
                        }
                        newsTypeModel.Id = ID;
                        result = newsTypeBLL.Update(newsTypeModel);
                    }
                }
                if (result)
                {
                    MessageBox.ShowAndRedirect(this, "同业学堂类别操作成功", "Category.aspx");
                }
                else
                {
                    MessageBox.Show(this, "操作失败");
                }


            }
            catch (Exception)
            {
                MessageBox.Show(this, "操作失败");
            }
        }

        /// <summary>
        /// 处理repList编辑/删除操作.行业资讯
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //操作结果。默认为false
            bool result = false;
            try
            {
                if (e.CommandName.Equals("Del"))
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    result = newsTypeBLL.Delete(new int[] { id });
                    if (result)
                    {
                        MessageBox.ShowAndRedirect(this, "删除成功", "Category.aspx");
                    }
                }
                //else if (e.CommandName.Equals("Edit"))
                //{
                //    btnAdd.CommandName = "Edit";
                //    newsTypeModel = newsTypeBLL.GetInformationType().Where(item => item.Id == id) as EyouSoft.Model.NewsStructure.NewsType;
                //    txtCateName.Text = newsTypeModel.ClassName;
                //}
            }
            catch (Exception)
            {
                MessageBox.Show(this,"操作失败");
            }
            if (result)
            {
                Databind();
            }
        }

        /// <summary>
        /// 处理repSchoolList编辑/删除操作.同业学堂
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repSchoolList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //操作结果。默认为false
            bool result = false;
            try
            {
                if (e.CommandName.Equals("Del"))
                {
                    int id = int.Parse(e.CommandArgument.ToString());
                    result = newsTypeBLL.Delete(new int[] { id });
                    if (result)
                    {
                        MessageBox.ShowAndRedirect(this, "删除成功", "Category.aspx");
                    }
                }
                //else if (e.CommandName.Equals("Edit"))
                //{
                //    btnAdd.CommandName = "Edit";
                //    newsTypeModel = newsTypeBLL.GetInformationType().Where(item => item.Id == id) as EyouSoft.Model.NewsStructure.NewsType;
                //    txtCateName.Text = newsTypeModel.ClassName;
                //}
            }
            catch (Exception)
            {
                MessageBox.Show(this, "操作失败");
            }
            if (result)
            {
                Databind();
            }
        }

    }
}
