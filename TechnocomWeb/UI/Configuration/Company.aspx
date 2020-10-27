<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Shared/default.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="TechnocomWeb.UI.Configuration.Company" %>

<%@ Register Assembly="TechnocomControl" Namespace="TechnocomControl" TagPrefix="Technocomctrl" %>

<asp:Content ID="ContentBody" runat="server" ContentPlaceHolderID="placeHolderBody1">
    <div id="DIVList" runat="server">
        <div class="portlet light">
            <div class="portlet-title">
                <div class="caption">
                    <i class="fa fa-list-alt txt-theme" aria-hidden="true"></i>
                    <span class="caption-subject font-dark bold uppercase">Company List</span>
                </div>
                <div class="pull-right">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Company"
                        OnClick="btnAdd_Click" />
                </div>
            </div>
            <div class="portlet-body" style="padding-top: 18px;">
                <div class="table-responsive">
                    <Technocomctrl:ctlGridView ID="gridViewList" runat="server" Width="100%" AutoGenerateColumns="false"
                        AutoBind="false" DataKeyNames="CompanyId" CssClass="table table-bordered table-hover table-striped" OnRowCommand="gridViewList_RowCommand"
                        NoCheckCaption="SKU Information" EmptyDataText="No Record Found!">
                        <Columns>
                            <asp:BoundField HeaderText="CompanyId" DataField="CompanyId" SortExpression="CompanyId" Visible="false" />
                            <asp:BoundField HeaderText="Company" DataField="CompanyName" SortExpression="CompanyName" />
                            <asp:BoundField HeaderText="Prefix" DataField="CompanyPrefix" SortExpression="CompanyPrefix" />
                            <asp:BoundField HeaderText="GST Number" DataField="GSTNumber" SortExpression="GSTNumber" />
                            <asp:BoundField HeaderText="Address1" DataField="CompanyAddress1" SortExpression="CompanyAddress1" />
                            <asp:BoundField HeaderText="ContactPerson" DataField="CompanyContactPerson" SortExpression="CompanyContactPerson" />
                            <asp:BoundField HeaderText="Mobile" DataField="CompanyMobile" SortExpression="CompanyMobile" />
                            <asp:BoundField HeaderText="Website" DataField="Website" SortExpression="Website" />
                            <asp:BoundField HeaderText="IsActive" DataField="IsActive" SortExpression="IsActive" />
                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" CommandName="EditRow" Text="" CssClass="btn btn-sm btn-success btn-refrsh"
                                        CommandArgument='<%# Eval("CompanyId")%>'></asp:Button>
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
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="required">Company Name</label>
                                    <Technocomctrl:ctlTextBox ID="txtCompanyName" CssClass="form-control" runat="server"
                                        MaxLength="350" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="Company Name"
                                        MetaValidationGroupName="G1" />
                                </div>
                                <div class="form-group">
                                    <label class="required">Company Prefix</label>
                                    <Technocomctrl:ctlTextBox ID="txtCompanyPrefix" CssClass="form-control" runat="server"
                                        MaxLength="20" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="Company Prefix"
                                        MetaValidationGroupName="G2" />
                                </div>
                                <div class="form-group">
                                    <label class="required">CIN Number</label>
                                    <Technocomctrl:ctlTextBox ID="txtCINNumber" CssClass="form-control" runat="server"
                                        MaxLength="100" />
                                </div>
                                <div class="form-group">
                                    <label class="required">GST Number</label>
                                    <Technocomctrl:ctlTextBox ID="txtGSTNumber" CssClass="form-control" runat="server"
                                        MaxLength="100" />
                                </div>
                                <div class="form-group">
                                    <label class="required">Town</label>
                                    <Technocomctrl:ctlDropDownList ID="ddlTown" runat="server" CssClass="form-control"
                                        DataTextField="TownName" DataValueField="TownId" MetaMandatory="true" MetaValidationRequired="true"
                                        MetaCaption="Town" MetaValidationGroupName="G3" />
                                </div>
                                <div class="form-group">
                                    <label class="required">Address1</label>
                                    <Technocomctrl:ctlTextBox ID="txtCompanyAddress1" runat="server" TextMode="MultiLine" MetaCaption="Address1" MaxLength="400"
                                        MetaValidationRequired="true" MetaMandatory="true" MetaValidationGroupName="G4" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label class="required">Address2</label>
                                    <Technocomctrl:ctlTextBox ID="txtCompanyAddress2" runat="server" TextMode="MultiLine" MaxLength="400"
                                        CssClass="form-control" />
                                </div>

                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="required">Contact Person</label>
                                    <Technocomctrl:ctlTextBox ID="txtCompanyContactPerson" CssClass="form-control" runat="server"
                                        MaxLength="100" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="Company Contact Person"
                                        MetaValidationGroupName="G5" />
                                </div>
                                <div class="form-group">
                                    <label class="required">Contact Mobile</label>
                                    <Technocomctrl:ctlTextBox ID="txtCompanyContactMobile" CssClass="form-control" runat="server"
                                        MaxLength="50" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="Company Contact Mobile"
                                        MetaValidationGroupName="G6" />
                                </div>
                                <div class="form-group">
                                    <label class="required">Company Phone</label>
                                    <Technocomctrl:ctlTextBox ID="txtCompanyPhone" CssClass="form-control" runat="server"
                                        MaxLength="50" />
                                </div>

                                <div class="form-group">
                                    <label class="required">Zipcode</label>
                                    <Technocomctrl:ctlTextBox ID="txtCompanyZipcode" CssClass="form-control" runat="server"
                                        MaxLength="20" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="Company Zipcode"
                                        MetaValidationGroupName="G7" />
                                </div>

                                <div class="form-group">
                                    <label>Email Id</label>
                                    <Technocomctrl:ctlTextBox ID="txtEmailId" CssClass="form-control" runat="server" MaxLength="150" />
                                </div>
                                <div class="form-group">
                                    <label class="required">Website</label>
                                    <Technocomctrl:ctlTextBox ID="txtWebsite" CssClass="form-control" runat="server"
                                        MaxLength="150" />
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
                                    <label>Logo Image (50 x 150)</label>
                                    <asp:Image ID="ImageUserImage" runat="server" CssClass="UserImageUpload" Height="160px" Width="200px" ImageUrl="~/Content/images/Securitaslogo.png" />
                                </div>
                                <br />
                                <div class="form-group">
                                    <asp:FileUpload ID="txtCompanyLogo" CssClass="form-control" runat="server" />
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
