<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Shared/default.Master" AutoEventWireup="true" CodeBehind="RoleManagement.aspx.cs" Inherits="TechnocomWeb.UI.HR.RoleManagement" %>

<%@ Register Assembly="TechnocomControl" Namespace="TechnocomControl" TagPrefix="Technocomctrl" %>

<asp:Content ID="ContentBody" runat="server" ContentPlaceHolderID="placeHolderBody1">

    <style type="text/css">
        .checkbox {
            width: auto !important;
            float: left;
        }

            .checkbox tr {
                background-color: Transparent !important;
            }

            .checkbox input {
                width: auto !important;
                float: left !important;
            }

            .checkbox label {
                width: 300px !important;
                float: left !important;
                font-family: Tahoma, Geneva, sans-serif;
                font-size: 13px;
                font-weight: bold;
                color: #333;
                text-align: left;
                text-indent: 22px;
                padding-left: 0px !important;
            }

            .checkbox label, .radio label {
                padding-left: 0px !important;
            }

            .checkbox input[type=checkbox], .checkbox-inline input[type=checkbox], .radio input[type=radio], .radio-inline input[type=radio] {
                margin-left: 0px !important;
            }
    </style>

    <div id="DIVList" runat="server">
        <div class="portlet light">
            <div class="portlet-title">
                <div class="caption">
                    <i class="fa fa-list-alt txt-theme" aria-hidden="true"></i>
                    <span class="caption-subject font-dark bold uppercase">Role List</span>
                </div>
                <div class="pull-right">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Role"
                        OnClick="btnAdd_Click" />
                </div>
            </div>
            <div class="portlet-body" style="padding-top: 18px;">
                <div class="table-responsive">
                    <Technocomctrl:ctlGridView ID="gridViewList" runat="server" Width="100%" AutoGenerateColumns="false"
                        AutoBind="false" DataKeyNames="RoleId" CssClass="table table-bordered table-hover table-striped" OnRowCommand="gridViewList_RowCommand"
                        NoCheckCaption="SKU Information" EmptyDataText="No Record Found!">
                        <Columns>
                            <asp:BoundField HeaderText="RoleId" DataField="RoleId" SortExpression="RoleId" Visible="false" />
                            <asp:BoundField HeaderText="RoleName" DataField="RoleName" SortExpression="RoleName" />
                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" CommandName="EditRow" Text="" CssClass="btn btn-sm btn-success btn-refrsh"
                                        CommandArgument='<%# Eval("RoleId")%>'></asp:Button>&nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CommandName="DeleteRow" Text="" CssClass="btn btn-sm red-mint btn-del"
                                    CommandArgument='<%# Eval("RoleId")%>'></asp:Button>
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
                    <div class="col-sm-4">
                        <div class="form-group">
                            <label class="required">Role Name</label>
                            <Technocomctrl:ctlTextBox ID="txtRoleName" CssClass="form-control" runat="server"
                                MaxLength="60" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="Role Name"
                                MetaValidationGroupName="G1" />
                        </div>
                    </div>
                </div>
                <div class="col-sm-12">&nbsp;</div>
                <div class="col-sm-12"><span style="color: maroon; font-size: 15px;">Menu Items:</span></div>
                <div class="col-sm-12">&nbsp;</div>
                <div class="col-sm-12">
                    <asp:Repeater ID="gvMenuList" runat="server" OnItemDataBound="gvMenuList_ItemDataBound">
                        <ItemTemplate>
                            <div class="col-sm-12" style="border: 1px dashed; margin-top: 10px; margin-bottom: 10px; padding-top: 3px; padding-bottom: 3px;">
                                <div class="col-sm-2" style="border-right: 1px solid; min-height: 175px;">
                                    <div class="form-group" style="padding-top:80px;">
                                        <asp:Label ID="lblParentId" runat="server" CssClass="label-new" Visible="false" Text='<%# Eval("NavigationId")%>' />
                                        <asp:Label ID="lblMenuName" runat="server" CssClass="label-new" Text='<%# Eval("MenuName")%>' Font-Size="13px" />
                                    </div>
                                </div>
                                <div class="col-sm-9">
                                    <asp:CheckBoxList ID="chkChildMenuList" runat="server" RepeatColumns="3" CssClass="checkbox tabele table-bordered"
                                        RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
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
