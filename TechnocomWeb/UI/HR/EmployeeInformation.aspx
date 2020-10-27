<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Shared/default.Master" AutoEventWireup="true"
    CodeBehind="EmployeeInformation.aspx.cs" Inherits="TechnocomWeb.UI.HR.EmployeeInformation" %>

<%@ Register Assembly="TechnocomControl" Namespace="TechnocomControl" TagPrefix="Technocomctrl" %>

<asp:Content ID="ContentBody" runat="server" ContentPlaceHolderID="placeHolderBody1">
    <div id="DIVList" runat="server">
        <div class="portlet light">
            <div class="portlet-title">
                <div class="caption">
                    <i class="fa fa-search font-dark" aria-hidden="true"></i>
                    <span class="caption-subject font-dark bold uppercase">Filter</span>
                </div>
                <div class="actions">
                </div>
            </div>
            <div class="portlet-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label>User Name</label>
                                <Technocomctrl:ctlTextBox ID="txtUserNameSearch" runat="server" class="form-control" MaxLength="100" />

                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label>Mobile Number</label>
                                <Technocomctrl:ctlTextBox ID="txtMobileNumberSearch" runat="server" class="form-control" MaxLength="20" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label>NIC No</label>
                                <Technocomctrl:ctlTextBox ID="txtNICNoSearch" runat="server" class="form-control" MaxLength="20" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label>Role</label>
                                <Technocomctrl:ctlDropDownList ID="ddlRoleSearch" runat="server" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group" style="padding-top: 20px;">
                                <label>&nbsp;</label>
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-sm yellow" Text="Search"
                                    OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="portlet light">
            <div class="portlet-title">
                <div class="caption">
                    <i class="fa fa-list-alt txt-theme" aria-hidden="true"></i>
                    <span class="caption-subject font-dark bold uppercase">Employee List</span>
                </div>
                <div class="pull-right">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Employee"
                        OnClick="btnAdd_Click" />
                </div>
            </div>
            <div class="portlet-body" style="padding-top: 18px;">
                <div class="table-responsive">
                    <Technocomctrl:ctlGridView ID="gridViewList" runat="server" Width="100%" AutoGenerateColumns="false"
                        AutoBind="false" DataKeyNames="UserId" CssClass="table table-bordered table-hover table-striped" OnRowCommand="gridViewList_RowCommand"
                        NoCheckCaption="SKU Information" EmptyDataText="No Record Found!">
                        <Columns>
                            <asp:BoundField HeaderText="UserId" DataField="UserId" SortExpression="UserId" Visible="false" />
                            <asp:BoundField HeaderText="Employee" DataField="UserName" SortExpression="UserName" />
                            <asp:BoundField HeaderText="EmployeeCode" DataField="EmployeeCode" SortExpression="EmployeeCode" />
                            <asp:BoundField HeaderText="Designation" DataField="DesignationTypeName" SortExpression="DesignationTypeName" />
                            <asp:BoundField HeaderText="MobileNumber" DataField="MobileNumber" SortExpression="MobileNumber" />
                            <asp:BoundField HeaderText="NIC No" DataField="NICNo" SortExpression="NICNo" />
                            <asp:BoundField HeaderText="Role" DataField="RoleName" SortExpression="RoleName" />
                            <asp:BoundField HeaderText="Company" DataField="CompanyName" SortExpression="CompanyName" />
                            <asp:BoundField HeaderText="Location" DataField="LocationName" SortExpression="LocationName" />
                            <asp:BoundField HeaderText="IsActive" DataField="IsActive" SortExpression="IsActive" />
                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" CommandName="EditRow" Text="" CssClass="btn btn-sm btn-success btn-refrsh"
                                        CommandArgument='<%# Eval("UserId")%>'></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Right" />
                    </Technocomctrl:ctlGridView>
                </div>
            </div>
        </div>
    </div>

    <div class="portlet light" id="DIVDetail" runat="server" visible="false">
        <asp:UpdatePanel ID="upnlMain" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="portlet-body">
                    <div class="row">
                        <div class="col-sm-12 no-padding">
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <label class="required">Employee Name</label>
                                    <Technocomctrl:ctlTextBox ID="txtEmployeeName" CssClass="form-control" runat="server"
                                        MaxLength="150" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="Employee Name"
                                        MetaValidationGroupName="G1" />
                                </div>
                                <div class="form-group">
                                    <label class="required">Employee Code</label>
                                    <Technocomctrl:ctlTextBox ID="txtEmployeeCode" CssClass="form-control" runat="server"
                                        MaxLength="50" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="Employee Code"
                                        MetaValidationGroupName="G2" />
                                </div>
                                <div class="form-group">
                                    <label class="required">NIC Number</label>
                                    <Technocomctrl:ctlTextBox ID="txtNICNumber" CssClass="form-control" runat="server"
                                        MaxLength="200" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="NIC Number"
                                        MetaValidationGroupName="G3" />
                                </div>
                                <div class="form-group">
                                    <label class="required">Company</label>
                                    <Technocomctrl:ctlDropDownList ID="ddlCompany" runat="server" CssClass="form-control"
                                        DataTextField="CompanyName" DataValueField="CompanyId" MetaMandatory="true" MetaValidationRequired="true"
                                        MetaCaption="Principal" MetaValidationGroupName="G4" />
                                </div>
                                <div class="form-group">
                                    <label class="required">Role</label>
                                    <Technocomctrl:ctlDropDownList ID="ddlRole" runat="server" CssClass="form-control"
                                        DataTextField="RoleName" DataValueField="RoleId" MetaMandatory="true" MetaValidationRequired="true"
                                        MetaCaption="Role" MetaValidationGroupName="G5" />
                                </div>
                                <div class="form-group">
                                    <label class="required">Designation</label>
                                    <Technocomctrl:ctlDropDownList ID="ddlDesignationType" runat="server" CssClass="form-control"
                                        DataTextField="DesignationTypeName" DataValueField="DesignationTypeId" MetaMandatory="true" MetaValidationRequired="true"
                                        MetaCaption="Designation" MetaValidationGroupName="G6" />
                                </div>
                                <div class="form-group">
                                    <label class="required">Location</label>
                                    <Technocomctrl:ctlDropDownList ID="ddlLocation" runat="server" CssClass="form-control"
                                        DataTextField="LocationName" DataValueField="LocationId" MetaMandatory="true" MetaValidationRequired="true"
                                        MetaCaption="Location" MetaValidationGroupName="G7" />
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <label class="required">Mobile Number</label>
                                    <Technocomctrl:ctlTextBox ID="txtMobileNumber" CssClass="form-control" runat="server"
                                        MaxLength="20" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="Mobile Number"
                                        MetaValidationGroupName="G8" />
                                </div>
                                <div class="form-group">
                                    <label>Email Id</label>
                                    <Technocomctrl:ctlTextBox ID="txtEmailId" CssClass="form-control" runat="server"
                                        MaxLength="150" />
                                </div>
                                <div class="form-group">
                                    <label class="required">Parmanent Address</label>
                                    <Technocomctrl:ctlTextBox ID="txtParmanentAddress" runat="server" TextMode="MultiLine" MetaCaption="Parmanent Address" MaxLength="500"
                                        MetaValidationRequired="true" MetaMandatory="true" MetaValidationGroupName="G9" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label class="required">Present Address</label>
                                    <Technocomctrl:ctlTextBox ID="txtPresentAddress" runat="server" TextMode="MultiLine" MetaCaption="Present Address" MaxLength="500"
                                        MetaValidationRequired="true" MetaMandatory="true" MetaValidationGroupName="G10" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <div class="checkbox-color checkbox-primary">
                                        <asp:CheckBox ID="chkIsActive" runat="server" Text="" />
                                        <label for="placeHolderBody1_chkIsActive">
                                            Is Active 
                                        </label>
                                    </div>
                                </div>
                              
                                <div class="form-group">
                                    <label>User Image (50 x 150)</label>
                                    <asp:Image ID="ImageUserImage" runat="server" CssClass="UserImageUpload" Height="160px" Width="200px" ImageUrl="~/Content/img/defaultphoto.jpg" />
                                </div>
                                <br />
                                <div class="form-group">
                                    <asp:FileUpload ID="txtUserImage" CssClass="form-control" runat="server" />
                                </div>
                           
                            </div>
                            <div class="col-sm-6">
                                <div class="panel panel-default">
                                    <div class="panel-heading text-left">
                                        <b>POC Details</b>
                                    </div>
                                    <div class="panel-body">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="required">POC Name</label>
                                                <Technocomctrl:ctlTextBox ID="txtPOCName" CssClass="form-control" runat="server"
                                                    MaxLength="150" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="POC Name"
                                                    MetaValidationGroupName="G11" />
                                            </div>
                                            <div class="form-group">
                                                <label class="required">Mobile Number</label>
                                                <Technocomctrl:ctlTextBox ID="txtPOCMobileNo" CssClass="form-control" runat="server"
                                                    MaxLength="30" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="POC Mobile Number"
                                                    MetaValidationGroupName="G12" />
                                            </div>
                                            <div class="form-group">
                                                <label>Email Id</label>
                                                <Technocomctrl:ctlTextBox ID="txtPOCEmailId" CssClass="form-control" runat="server" MaxLength="150" />
                                            </div>
                                            <div class="form-group">
                                                <label class="required">Parmanent Address</label>
                                                <Technocomctrl:ctlTextBox ID="txtPOCParmanentAddress" runat="server" TextMode="MultiLine" MetaCaption="Parmanent Address" MaxLength="500"
                                                    MetaValidationRequired="true" MetaMandatory="true" MetaValidationGroupName="G13" CssClass="form-control" />
                                            </div>
                                            <div class="form-group">
                                                <label>Remarks</label>
                                                <Technocomctrl:ctlTextBox ID="txtPOCRemark" runat="server" TextMode="MultiLine" MaxLength="500" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label>CNIC Number</label>
                                                <Technocomctrl:ctlTextBox ID="txtCNICNo" CssClass="form-control" runat="server"
                                                    MaxLength="50" />
                                            </div>
                                            <div class="form-group">
                                                <label>Phone Number</label>
                                                <Technocomctrl:ctlTextBox ID="txtPOCPhoneNo" CssClass="form-control" runat="server"
                                                    MaxLength="30" />
                                            </div>
                                            <div class="form-group">
                                                <label class="required">Relation</label>
                                                <Technocomctrl:ctlTextBox ID="txtPOCRelation" CssClass="form-control" runat="server"
                                                    MaxLength="150" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="POC Relation"
                                                    MetaValidationGroupName="G14" />
                                            </div>
                                            <div class="form-group">
                                                <label class="required">Present Address</label>
                                                <Technocomctrl:ctlTextBox ID="txtPOCPresentAddress" runat="server" TextMode="MultiLine"
                                                    MetaCaption="POC Present Address" MaxLength="500" MetaValidationRequired="true" MetaMandatory="true"
                                                    MetaValidationGroupName="G15" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                        </div>
                        <div class="col-sm-12">&nbsp;</div>
                        <div class="col-sm-12 col-xs-12 col-md-12 text-left">
                            <asp:Button ID="btnSubmit" align="center" runat="server" Text="Submit"
                                CssClass="btn btn-sm blue" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" runat="server" align="center" Text="Cancel"
                                CssClass="btn btn-sm btn-warning" OnClick="btnCancel_Click" />
                        </div>
                        <div class="col-sm-12">&nbsp;</div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
