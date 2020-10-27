<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Shared/default.Master" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="TechnocomWeb.UI.HR.UserLogin" %>

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
            </div>
            <div class="portlet-body" style="padding-top: 18px;">
                <div class="table-responsive">
                    <Technocomctrl:ctlGridView ID="gridViewList" runat="server" Width="100%" AutoGenerateColumns="false"
                        AutoBind="false" DataKeyNames="UserId" CssClass="table table-bordered table-hover table-striped" OnRowCommand="gridViewList_RowCommand"
                        NoCheckCaption="SKU Information" EmptyDataText="No Record Found!">
                        <Columns>
                            <asp:BoundField HeaderText="UserId" DataField="UserId" SortExpression="UserId" Visible="false" />
                            <asp:BoundField HeaderText="LoginId" DataField="LoginId" SortExpression="LoginId" />
                            <asp:BoundField HeaderText="Employee" DataField="UserName" SortExpression="UserName" />
                            <asp:BoundField HeaderText="EmployeeCode" DataField="EmployeeCode" SortExpression="EmployeeCode" />
                            <asp:BoundField HeaderText="Designation" DataField="DesignationTypeName" SortExpression="DesignationTypeName" />
                            <asp:BoundField HeaderText="MobileNumber" DataField="MobileNumber" SortExpression="MobileNumber" />
                            <asp:BoundField HeaderText="NIC No" DataField="NICNo" SortExpression="NICNo" />
                            <asp:BoundField HeaderText="Role" DataField="RoleName" SortExpression="RoleName" />
                            <asp:BoundField HeaderText="Company" DataField="CompanyName" SortExpression="CompanyName" />
                            <asp:BoundField HeaderText="Location" DataField="LocationName" SortExpression="LocationName" />
                            <asp:BoundField HeaderText="ActiveLogin" DataField="IsActivateLogin" SortExpression="IsActivateLogin" />
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
        <div class="portlet-body">
            <div class="row">
                <div class="col-sm-12 no-padding">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label class="required">Employee Name</label>
                            <asp:Label ID="lblEmployeeName" CssClass="form-control" runat="server" Text="" Font-Bold="true" ForeColor="Maroon" Font-Size="14px" />
                        </div>
                        <div class="form-group">
                            <label class="required">Login Id</label>
                            <Technocomctrl:ctlTextBox ID="txtLoginId" CssClass="form-control" runat="server"
                                MaxLength="60" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="Login Id"
                                MetaValidationGroupName="G1" />
                        </div>
                        <div class="form-group">
                            <label class="required">Password</label>
                            <Technocomctrl:ctlTextBox ID="txtPassword" CssClass="form-control" runat="server"
                                MaxLength="60" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="Password"
                                MetaValidationGroupName="G2" />
                        </div>
                        <div class="form-group">
                            <div class="checkbox-color checkbox-primary">
                                <asp:CheckBox ID="chkIsActivateLogin" runat="server" Text="" />
                                <label for="placeHolderBody1_chkIsActivateLogin">
                                    Is Active Login 
                                </label>
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
    </div>
</asp:Content>
