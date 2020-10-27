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

namespace TechnocomWeb.UI.Configuration
{
    public partial class Company : ctlPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            FillGrid();
        }
        private void ClearPageControl()
        {
            ViewState["Add"] = null;
            ViewState["Update"] = null;
            ViewState["CompanyId"] = null;

            txtCompanyName.Text = string.Empty;
            txtCompanyPrefix.Text = string.Empty;
            txtCINNumber.Text = string.Empty;

            txtGSTNumber.Text = string.Empty;
            ddlTown.ClearSelection();
            txtCompanyAddress1.Text = string.Empty;
            txtCompanyAddress2.Text = string.Empty;

            txtCompanyContactPerson.Text = string.Empty;
            txtCompanyContactMobile.Text = string.Empty;
            txtCompanyPhone.Text = string.Empty;
            txtCompanyZipcode.Text = string.Empty;
            txtEmailId.Text = string.Empty;
            txtWebsite.Text = string.Empty;

            chkIsActive.Checked = false;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearPageControl();
            NavigateTo("~/UI/Configuration/Company.aspx");
        }
        private void FillGrid()
        {
            var list = new ConfigrationRepository(SessionContext).GetCompanyList();
            gridViewList.LoadData(list);

            DIVList.Visible = true;
            DIVDetail.Visible = false;

            ViewState["Add"] = null;
            ViewState["Update"] = null;
            ViewState["CompanyId"] = null;
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

            //LookupUtility.BindTownLookup(ddlTown, SessionContext);

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

                byte[] imgbyte = null;
                string getFileName = string.Empty;
                string uploadedFileName = string.Empty;

                if (txtCompanyLogo.HasFile)
                {
                    string completePath = string.Empty;
                    string imgType = string.Empty;

                    string[] stringListArr = System.IO.Path.GetFileName(txtCompanyLogo.PostedFile.FileName).Split('.');
                    imgType = txtCompanyLogo.PostedFile.ContentType;

                    if ((imgType == "image/gif" || imgType == "image/pjpeg" || imgType == "image/jpeg" || imgType == "image/x-png" || stringListArr[1].ToString().Trim().ToLower() == "png"))
                    {
                        int length = txtCompanyLogo.PostedFile.ContentLength;
                        imgbyte = new byte[length];
                        HttpPostedFile imageFile = txtCompanyLogo.PostedFile;
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
                            getFileName = txtCompanyLogo.PostedFile.FileName;

                            if (txtCompanyLogo.PostedFile.ContentLength > 0)
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
                        ShowPopup("Please Select a valid logo file.");
                        return;
                    }
                }

                CompanyEntity entity = new CompanyEntity();

                entity.CompanyId = Utility.GetInt(ViewState["CompanyId"]);

                entity.CompanyName = txtCompanyName.Text.Trim();
                entity.CompanyPrefix = txtCompanyPrefix.Text.Trim();
                entity.CIN = txtCINNumber.Text;
                entity.GSTNumber = txtGSTNumber.Text;
                entity.CompanyAddress1 = txtCompanyAddress1.Text.Trim();
                entity.CompanyAddress2 = txtCompanyAddress2.Text;
                entity.TownId = Utility.GetLong(ddlTown.SelectedValue);

                entity.CompanyContactPerson = txtCompanyContactPerson.Text.Trim();
                entity.CompanyMobile = txtCompanyContactMobile.Text.Trim();
                entity.CompanyPhone = txtCompanyPhone.Text;
                entity.CompanyZipcode = txtCompanyZipcode.Text.Trim();
                entity.EmailId = txtEmailId.Text;
                entity.Website = txtWebsite.Text;
                entity.FinancialYearId = SessionClass.FinancialYearId;

                entity.CompanyLogo = uploadedFileName;

                entity.IsActive = chkIsActive.Checked;
                entity.CreatedBy = SessionClass.LoginUserEntity.UserId;

                OperationStatusEntity c = new ConfigrationRepository(SessionContext).UpdateCompany(entity);
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
                ViewState["CompanyId"] = Convert.ToString(arg[0]);
                int CompanyId = Utility.GetInt(ViewState["CompanyId"]);

                ViewState["Update"] = "Update";

                //LookupUtility.BindTownLookup(ddlTown, SessionContext);

                CompanyEntity entity = new ConfigrationRepository(SessionContext).GetCompanyById(CompanyId);

                Utility.SetLookupSelectedValue(ddlTown, Convert.ToString(entity.TownId));

                txtCompanyName.Text = entity.CompanyName;
                txtCompanyPrefix.Text = entity.CompanyPrefix;
                txtCINNumber.Text = entity.CIN;
                txtGSTNumber.Text = entity.GSTNumber;
                txtCompanyAddress1.Text = entity.CompanyAddress1;
                txtCompanyAddress2.Text = entity.CompanyAddress2;
                
                txtCompanyContactPerson.Text = entity.CompanyContactPerson;
                txtCompanyContactMobile.Text = entity.CompanyMobile;
                txtCompanyPhone.Text = entity.CompanyPhone;
                txtCompanyZipcode.Text = entity.CompanyZipcode;
                txtEmailId.Text = entity.EmailId;
                txtWebsite.Text = entity.Website;

                ImageUserImage.ImageUrl = entity.CompanyLogo;
                chkIsActive.Checked = entity.IsActive;

                DIVList.Visible = false;
                DIVDetail.Visible = true;
            }
        }
    }
}