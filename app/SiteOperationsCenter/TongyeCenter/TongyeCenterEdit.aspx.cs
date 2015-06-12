using System;
using System.Collections.Generic;

using EyouSoft.Common;

namespace SiteOperationsCenter.TongyeCenter
{
    /// <summary>
    /// 同业中心编辑页
    /// 修改记录:
    /// 1. 2011-05-27 曹胡生 创建
    /// </summary>
    public partial class TongyeCenterEdit : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //同业中心编号
            int id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            if (id == 0)
            {
                YuYingPermission[] parms = { YuYingPermission.同业中心_新增, YuYingPermission.同业中心_新增 };
                if (!CheckMasterGrant(parms))
                {
                    Utils.ResponseNoPermit(YuYingPermission.同业中心_新增, true);
                    return;
                }
            }
            else
            {
                YuYingPermission[] parms = { YuYingPermission.同业中心_修改, YuYingPermission.同业中心_修改 };
                if (!CheckMasterGrant(parms))
                {
                    Utils.ResponseNoPermit(YuYingPermission.同业中心_修改, true);
                    return;
                }
            }
            if (!IsPostBack)
            {
                Bind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                //同业中心编号
                int id = Utils.GetInt(Utils.GetQueryStringValue("id"));
                EyouSoft.Model.MQStructure.IMSuperCluster model = new EyouSoft.Model.MQStructure.IMSuperCluster();
                //同业中心名称
                model.Title = txtTongyeCenterName.Value;
                //会员导入方式
                if (selProvince.Checked)
                {
                    //导入的省市
                    model.SelectType = EyouSoft.Model.MQStructure.SelectType.选择省市;
                    model.SelectValue = hidProCityIDs.Value.Trim(',');
                }
                if (radByIds.Checked)
                {
                    //导入的会员ID
                    model.SelectType = EyouSoft.Model.MQStructure.SelectType.选择会员ID;
                    hidMemberIDs.Value=System.Text.RegularExpressions.Regex.Replace(hidMemberIDs.Value, ",+", ",");
                    model.SelectValue = hidMemberIDs.Value.Trim(',');
                }
                //排序号
                model.Num = Utils.GetInt(txtSort.Value);
                //总管理员ID
                model.Master = Utils.GetInt(txtIDs.Value);
                //总管理员密码
                model.PassWord = txtPassword.Value;
                //操作人
                model.Opertor = txtOper.Value;
                //操作时间
                model.OperateTime = Utils.GetDateTime(txtDate.Value);
                
                //修改
                if (id != 0)
                {
                    if (EyouSoft.BLL.MQStructure.IMSuperCluster.CreateInstance().IsExist(model.Title,id))
                    {
                        EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('该同业中心已存在!')");
                        return;
                    }
                    model.Id = id;
                    if (EyouSoft.BLL.MQStructure.IMSuperCluster.CreateInstance().Upd(model))
                    {
                        Utils.ShowAndRedirect("修改成功", "TongyeCenterManager.aspx");
                    }
                    else
                    {
                        Utils.ShowAndRedirect("修改失败", "TongyeCenterManager.aspx");
                    }
                }
                //添加
                else
                {
                    if (EyouSoft.BLL.MQStructure.IMSuperCluster.CreateInstance().IsExist(model.Title,0))
                    {
                        EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('该同业中心已存在!')");
                        return;
                    }
                    if (EyouSoft.BLL.MQStructure.IMSuperCluster.CreateInstance().Add(model))
                    {
                        Utils.ShowAndRedirect("添加成功", "TongyeCenterManager.aspx");
                    }
                    else
                    {
                        Utils.ShowAndRedirect("添加失败", "TongyeCenterManager.aspx");
                    }
                }
            }
        }

        //初始化
        private void Bind()
        {
            int id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            if (id != 0)
            {
                EyouSoft.Model.MQStructure.IMSuperCluster model = EyouSoft.BLL.MQStructure.IMSuperCluster.CreateInstance().GetSuperClusterByID(id);
                if (model != null)
                {
                    //同业中心名称
                    txtTongyeCenterName.Value = model.Title;
                    //会员导入方式
                    if (model.SelectType == EyouSoft.Model.MQStructure.SelectType.选择省市)
                    {
                        selProvince.Checked = true;
                        //导入的省市
                        hidProCityIDs.Value = model.SelectValue;
                    }
                    else if (model.SelectType == EyouSoft.Model.MQStructure.SelectType.选择会员ID)
                    {
                        radByIds.Checked = true;
                        //导入的会员ID
                        hidMemberIDs.Value = model.SelectValue;
                    }
                    //排序号
                    txtSort.Value = model.Num.ToString();
                    //总管理员ID
                    txtIDs.Value = model.Master.ToString();
                    //总管理员密码
                    txtPassword.Value = model.PassWord;
                    //操作人
                    txtOper.Value = MasterUserInfo.ContactName;
                    //操作时间
                    txtDate.Value = model.OperateTime.ToString();
                }
            }
            else
            {
                txtOper.Value = MasterUserInfo.ContactName;
                txtDate.Value = DateTime.Now.ToString();
            }
            //获得所有已选的省市
            IList<EyouSoft.Model.SystemStructure.ProvinceBase> list = null;
            list = EyouSoft.BLL.MQStructure.IMSuperCluster.CreateInstance().GetSelectedProvincesByID(id);
            if (list != null)
            {
                foreach (var item in list)
                {
                    hidProCityIDsEd.Value += item.ProvinceId + ",";
                }
            }
        }

        //检查数据合法性
        private bool CheckData()
        {
            if (string.IsNullOrEmpty(txtTongyeCenterName.Value))
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('请输入同业中心名称!')");
                return false;
            }
            if (selProvince.Checked == false && radByIds.Checked == false)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('请选择会员导入方式!')");
                return false;
            }
            if (selProvince.Checked == true)
            {
                if (string.IsNullOrEmpty(hidProCityIDs.Value))
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('请选择省市!')");
                    return false;
                }
            }
            if (radByIds.Checked == true)
            {
                if (string.IsNullOrEmpty(hidMemberIDs.Value))
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('请输入会员ID!')");
                    return false;
                }
                if (!EyouSoft.Common.Function.StringValidate.IsRegexMatch(hidMemberIDs.Value, @"^\d[,\d]*$"))
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('会员ID只能是数字且以数字开头，英文逗号分隔!')");
                    return false;
                }
            }
            if (Utils.GetInt(txtSort.Value) == 0 && txtSort.Value != "0")
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('请输入有效序号!')");
                return false;
            }
            if (Utils.GetInt(txtIDs.Value) == 0)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('请输入有效总管理员ID!')");
                return false;
            }
           
            return true;
        }
    }
}
