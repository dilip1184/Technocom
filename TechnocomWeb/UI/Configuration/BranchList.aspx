<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Shared/default.Master"
    AutoEventWireup="true" CodeBehind="BranchList.aspx.cs" Inherits="TechnocomWeb.UI.Configuration.BranchList"
    MaintainScrollPositionOnPostback="true" %>

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
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label>Region</label>
                                <Technocomctrl:ctlDropDownList ID="ddlRegionSearch" runat="server" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlRegionSearch_SelectedIndexChanged" AutoPostBack="true"
                                    DataTextField="RegionName" DataValueField="RegionId" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label>Zone</label>
                                <Technocomctrl:ctlDropDownList ID="ddlZoneSearch" runat="server" CssClass="form-control"
                                    DataTextField="ZoneName" DataValueField="ZoneId" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label>Branch</label>
                                <Technocomctrl:ctlTextBox ID="txtBranchNameSearch" runat="server" class="form-control"
                                    MaxLength="100" />
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group" style="padding-top: 20px;">
                                <label>&nbsp;</label>
                                <asp:Button ID="btnSearchBranch" runat="server" CssClass="btn btn-sm yellow" Text="Search"
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
                    <span class="caption-subject font-dark bold uppercase">Branch List</span>
                </div>
                <div class="pull-right">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Branch"
                        OnClick="btnAdd_Click" />
                </div>
            </div>
            <div class="portlet-body" style="padding-top: 18px;">
                <div class="table-responsive">
                    <Technocomctrl:ctlGridView ID="gridViewList" runat="server" Width="100%" AutoGenerateColumns="false"
                        AutoBind="false" DataKeyNames="BranchId" CssClass="table table-bordered table-hover table-striped" OnRowCommand="gridViewList_RowCommand"
                        NoCheckCaption="Location" EmptyDataText="No Record Found!">
                        <Columns>
                            <asp:BoundField HeaderText="BranchId" DataField="BranchId" SortExpression="BranchId" Visible="false" />
                            <asp:BoundField HeaderText="Region" DataField="RegionName" SortExpression="RegionName" />
                            <asp:BoundField HeaderText="Zone" DataField="ZoneName" SortExpression="ZoneName" />
                            <asp:BoundField HeaderText="Branch" DataField="BranchName" SortExpression="BranchName" />
                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" CommandName="EditRow" Text="" CssClass="btn btn-sm btn-success btn-refrsh"
                                        CommandArgument='<%# Eval("BranchId")%>'></asp:Button>&nbsp;&nbsp;
                                     <asp:Button ID="btnDelete" runat="server" CommandName="DeleteRow" Text="" CssClass="btn btn-sm red-mint btn-del"
                                         CommandArgument='<%# Eval("BranchId")%>'></asp:Button>
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
        <div class="portlet-title">
            <div class="caption">
                <i class="fa fa-list-alt txt-theme" aria-hidden="true"></i>
                <span class="caption-subject txt-theme bold uppercase">Branch Details</span>
            </div>
        </div>
        <div class="portlet-body">
            <div class="row">
                <div class="col-sm-12 no-padding">
                    <div class="col-sm-12">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label class="required">Region</label>
                                <Technocomctrl:ctlDropDownList ID="ddlRegion" runat="server" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" AutoPostBack="true"
                                    DataTextField="RegionName" DataValueField="RegionId" MetaMandatory="true" MetaValidationRequired="true"
                                    MetaCaption="Region" MetaValidationGroupName="G1" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label class="required">Zone</label>
                                <Technocomctrl:ctlDropDownList ID="ddlZone" runat="server" CssClass="form-control"
                                    DataTextField="ZoneName" DataValueField="ZoneId" MetaMandatory="true" MetaValidationRequired="true"
                                    MetaCaption="Zone" MetaValidationGroupName="G2" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label class="required">Branch Name</label>
                                <Technocomctrl:ctlTextBox ID="txtBranchName" CssClass="form-control" runat="server"
                                    MaxLength="100" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="Branch Name"
                                    MetaValidationGroupName="G3" />
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12">&nbsp;</div>
                    <div class="col-sm-12 col-xs-12 col-md-12 text-left">
                        <asp:Button ID="btnSubmit" align="center" runat="server" Text="Save"
                            CssClass="btn btn-sm blue" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" align="center" Text="Cancel"
                            CssClass="btn btn-sm btn-warning" OnClick="btnCancel_Click" />
                    </div>
                    <div class="col-sm-12">&nbsp;</div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
