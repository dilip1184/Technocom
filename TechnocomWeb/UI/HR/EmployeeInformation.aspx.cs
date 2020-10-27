using Microsoft.WindowsAzure.Storage.Blob;
using TechnocomControl;
using TechnocomService;
using TechnocomShared.Entities;
using TechnocomShared.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnocomWeb.UI.HR
{
    public partial class EmployeeInformation : ctlPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            LookupUtility.BindUserRoleLookup(ddlRoleSearch, SessionContext);

            FillGrid();
        }
        private void ClearPageControl()
        {
            ViewState["Add"] = null;
            ViewState["Update"] = null;
            ViewState["UserId"] = null;

            txtUserNameSearch.Text = string.Empty;
            txtMobileNumberSearch.Text = string.Empty;
            txtNICNoSearch.Text = string.Empty;
            ddlRoleSearch.ClearSelection();

            txtEmployeeName.Text = string.Empty;
            txtEmployeeCode.Text = string.Empty;
            txtNICNumber.Text = string.Empty;

            ddlCompany.ClearSelection();
            ddlRole.ClearSelection();
            ddlDesignationType.ClearSelection();
            ddlLocation.ClearSelection();

            txtMobileNumber.Text = string.Empty;
            txtEmailId.Text = string.Empty;
            txtParmanentAddress.Text = string.Empty;
            txtPresentAddress.Text = string.Empty;

            chkIsActive.Checked = false;

            txtPOCName.Text = string.Empty;
            txtPOCMobileNo.Text = string.Empty;
            txtPOCEmailId.Text = string.Empty;
            txtPOCParmanentAddress.Text = string.Empty;
            txtPOCRemark.Text = string.Empty;
            txtCNICNo.Text = string.Empty;
            txtPOCPhoneNo.Text = string.Empty;
            txtPOCRelation.Text = string.Empty;
            txtPOCPresentAddress.Text = string.Empty;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearPageControl();
            NavigateTo("~/UI/HR/EmployeeInformation.aspx");
        }
        private void FillGrid()
        {
            var list = new HRRepository(SessionContext).GetEmployeeInformationList(txtUserNameSearch.Text, txtMobileNumberSearch.Text, txtNICNoSearch.Text, Utility.GetInt(ddlRoleSearch.SelectedValue));
            gridViewList.LoadData(list);

            DIVList.Visible = true;
            DIVDetail.Visible = false;

            ViewState["Add"] = null;
            ViewState["Update"] = null;
            ViewState["UserId"] = null;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ShowErrorMessage(string.Empty);
                FillGrid();
            }
            catch (BaseException be)
            {
                ShowErrorMessage(be.DisplayMessage);
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ClearPageControl();

            ViewState["Add"] = "Add";

            chkIsActive.Checked = true;

            LookupUtility.BindCompanyLookup(ddlCompany, SessionContext);
            LookupUtility.BindUserRoleLookup(ddlRole, SessionContext);
            LookupUtility.BindDesignationTypeLookup(ddlDesignationType, SessionContext);
          //  LookupUtility.BindLocationLookup(ddlLocation, SessionContext);

            DIVList.Visible = false;
            DIVDetail.Visible = true;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateBusinessData("G1");
                ValidateBusinessData("G2");
                ValidateBusinessData("G3");
                ValidateBusinessData("G4");
                ValidateBusinessData("G5");
                ValidateBusinessData("G6");
                ValidateBusinessData("G7");
                ValidateBusinessData("G8");
                ValidateBusinessData("G9");

                ValidateBusinessData("G10");
                ValidateBusinessData("G11");
                ValidateBusinessData("G12");
                ValidateBusinessData("G13");
                ValidateBusinessData("G14");
                ValidateBusinessData("G15");

                byte[] imgbyte = null;
                string getFileName = string.Empty;
                string uploadedFileName = string.Empty;

                if (txtUserImage.HasFile)
                {
                    string completePath = string.Empty;
                    string imgType = string.Empty;

                    string[] stringListArr = System.IO.Path.GetFileName(txtUserImage.PostedFile.FileName).Split('.');
                    imgType = txtUserImage.PostedFile.ContentType;

                    if ((imgType == "image/gif" || imgType == "image/pjpeg" || imgType == "image/jpeg" || imgType == "image/x-png" || stringListArr[1].ToString().Trim().ToLower() == "png"))
                    {
                        int length = txtUserImage.PostedFile.ContentLength;
                        imgbyte = new byte[length];
                        HttpPostedFile imageFile = txtUserImage.PostedFile;
                        imageFile.InputStream.Read(imgbyte, 0, length);

                        MemoryStream stream = new MemoryStream(imgbyte);
                        int kilobytes = Int32.Parse(Convert.ToString(stream.Length)) / 1024; // Get Size In KB

                        if (kilobytes > 200)
                        {
                            ShowPopup("Please Select a image file less than 200 KB in Size.");
                            return;
                        }
                        else
                        {
                            getFileName = txtUserImage.PostedFile.FileName;

                            if (txtUserImage.PostedFile.ContentLength > 0)
                            {
                                var container = Utility.GetStorageContainer();
                                string blobFileName = Guid.NewGuid().ToString("N");
                                string fileExtension = Path.GetExtension(imageFile.FileName);
                                CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobFileName + fileExtension);
                                blockBlob.Properties.ContentType = "image/png";

                                stream.Seek(0, SeekOrigin.Begin);
                                //input.Position = 0;
                                blockBlob.UploadFromStream(stream);

                                uploadedFileName = blockBlob.StorageUri.PrimaryUri.OriginalString;
                            }
                        }
                    }
                    else
                    {
                        ShowPopup("Please Select a valid picture file.");
                        return;
                    }
                }

                UserEntity entity = new UserEntity();

                entity.UserId = Utility.GetInt(ViewState["UserId"]);

                //entity.UserName = txtEmployeeName.Text.Trim();
                //entity.EmployeeCode = txtEmployeeCode.Text.Trim();
                //entity.NICNo = txtNICNumber.Text.Trim();

                //entity.CompanyId = Utility.GetInt(ddlCompany.SelectedValue);
                //entity.RoleId = Utility.GetInt(ddlRole.SelectedValue);
                //entity.DesignationTypeId = Utility.GetInt(ddlDesignationType.SelectedValue);
                //entity.LocationId = Utility.GetLong(ddlLocation.SelectedValue);

                //entity.MobileNumber = txtMobileNumber.Text.Trim();
                //entity.UserEmailId = txtEmailId.Text;
                //entity.ParmanentAddress = txtParmanentAddress.Text.Trim();
                //entity.PresentAddress = txtPresentAddress.Text.Trim();

                //entity.POCName = txtPOCName.Text;
                //entity.POCMobileNo = txtPOCMobileNo.Text;
                //entity.POCEmailId = txtPOCEmailId.Text;
                //entity.POCParmanentAddress = txtPOCParmanentAddress.Text;
                //entity.POCRemark = txtPOCRemark.Text;
                //entity.CNICNo = txtCNICNo.Text;

                //entity.POCPhoneNo = txtPOCPhoneNo.Text;
                //entity.POCRelation = txtPOCRelation.Text;
                //entity.POCPresentAddress = txtPOCPresentAddress.Text;

                //entity.PhotoPath = uploadedFileName;

                //entity.IsActive = chkIsActive.Checked;
                //entity.CreatedBy = SessionClass.LoginUserEntity.UserId;

                OperationStatusEntity c = new HRRepository(SessionContext).UpdateEmployeeInformation(entity);
                if (c.StatusResult == true)
                {
                    ShowInfoMessage(c.InfoMessage);
                    FillGrid();
                }
                else
                {
                    ShowErrorMessage(c.InfoMessage);
                }
            }
            catch (ValidationException ex)
            {
                ShowErrorMessage(ex.Message);
            }
            catch (BaseException be)
            {
                ShowErrorMessage(be.DisplayMessage);
            }
        }
        protected void gridViewList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            if (e.CommandName == "EditRow")
            {
                ViewState["UserId"] = Convert.ToString(arg[0]);
                long UserId = Utility.GetLong(ViewState["UserId"]);

                ViewState["Update"] = "Update";

                LookupUtility.BindCompanyLookup(ddlCompany, SessionContext);
                LookupUtility.BindUserRoleLookup(ddlRole, SessionContext);
                LookupUtility.BindDesignationTypeLookup(ddlDesignationType, SessionContext);
               // LookupUtility.BindLocationLookup(ddlLocation, SessionContext);

                UserEntity entity = new HRRepository(SessionContext).GetEmployeeInformationById(UserId);

                //Utility.SetLookupSelectedValue(ddlCompany, Convert.ToString(entity.CompanyId));
                //Utility.SetLookupSelectedValue(ddlRole, Convert.ToString(entity.RoleId));
                //Utility.SetLookupSelectedValue(ddlDesignationType, Convert.ToString(entity.DesignationTypeId));
                //Utility.SetLookupSelectedValue(ddlLocation, Convert.ToString(entity.LocationId));

                //txtEmployeeName.Text = entity.UserName;
                //txtEmployeeCode.Text = entity.EmployeeCode;
                //txtNICNumber.Text = entity.NICNo;

                //txtMobileNumber.Text = entity.MobileNumber;
                //txtEmailId.Text = entity.UserEmailId;
                //txtParmanentAddress.Text = entity.ParmanentAddress;
                //txtPresentAddress.Text = entity.PresentAddress;

                //txtPOCName.Text = entity.POCName;
                //txtPOCMobileNo.Text = entity.POCMobileNo;
                //txtPOCEmailId.Text = entity.POCEmailId;
                //txtPOCParmanentAddress.Text = entity.POCParmanentAddress; ;
                //txtPOCRemark.Text = entity.POCRemark;
                //txtCNICNo.Text = entity.CNICNo;

                //txtPOCPhoneNo.Text = entity.POCPhoneNo;
                //txtPOCRelation.Text = entity.POCRelation;
                //txtPOCPresentAddress.Text = entity.POCPresentAddress;

                ImageUserImage.ImageUrl = entity.PhotoPath;
                chkIsActive.Checked = entity.IsActive;

                DIVList.Visible = false;
                DIVDetail.Visible = true;
            }
        }
    }
}