<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Shared/default.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="TechnocomWeb.UI.HR.ChangePassword" %>

<%@ Register Assembly="TechnocomControl" Namespace="TechnocomControl" TagPrefix="Technocomctrl" %>

<asp:Content ID="ContentBody1" ContentPlaceHolderID="placeHolderBody1" runat="server">
    <div class="portlet light">
        <div class="portlet-body">
            <div class="row">
                <div class="col-sm-12 no-padding">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label class="required">Employee Name</label>
                            <asp:Label ID="lblEmployeeName" CssClass="form-control" runat="server" Text="" Font-Bold="true" ForeColor="Maroon" Font-Size="14px" />
                        </div>
                        <div class="form-group">
                            <label class="required">New Password</label>
                            <Technocomctrl:ctlTextBox ID="txtNewPassword" CssClass="form-control" runat="server" TextMode="Password"
                                MaxLength="60" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="New Password"
                                MetaValidationGroupName="G1" />
                        </div>
                        <div class="form-group">
                            <label class="required">Confirm Password</label>
                            <Technocomctrl:ctlTextBox ID="txtConfirmPassword" CssClass="form-control" runat="server" TextMode="Password"
                                MaxLength="60" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="Confirm Password"
                                MetaValidationGroupName="G2" />
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
