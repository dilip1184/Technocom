<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Shared/default.Master"
    AutoEventWireup="true" CodeBehind="DeviceList.aspx.cs" Inherits="TechnocomWeb.UI.Device.DeviceList"
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
                                <label>Device</label>
                                <Technocomctrl:ctlTextBox ID="txtDeviceNameSearch" runat="server" class="form-control"
                                    MaxLength="100" />
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
                    <span class="caption-subject font-dark bold uppercase">Device List</span>
                </div>
                <div class="pull-right">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Device"
                        OnClick="btnAdd_Click" />
                </div>
            </div>
            <div class="portlet-body" style="padding-top: 18px;">
                <div class="table-responsive">
                    <Technocomctrl:ctlGridView ID="gridViewList" runat="server" Width="100%" AutoGenerateColumns="false"
                        AutoBind="false" DataKeyNames="DeviceId" CssClass="table table-bordered table-hover table-striped" OnRowCommand="gridViewList_RowCommand"
                        NoCheckCaption="Device" EmptyDataText="No Record Found!">
                        <Columns>
                            <asp:BoundField HeaderText="DeviceId" DataField="DeviceId" SortExpression="DeviceId" Visible="false" />
                            <asp:BoundField HeaderText="SN" DataField="SN" SortExpression="SN" />
                            <asp:BoundField HeaderText="Device" DataField="DeviceAliasName" SortExpression="DeviceAliasName" />
                            <asp:BoundField HeaderText="IPAddress" DataField="IPAddress" SortExpression="IPAddress" />
                            <asp:BoundField HeaderText="ServerPort" DataField="ServerPort" SortExpression="ServerPort" />
                            <asp:BoundField HeaderText="AttLogStamp" DataField="AttLogStamp" SortExpression="AttLogStamp" />
                            <asp:BoundField HeaderText="Status" DataField="MachineStatus" SortExpression="MachineStatus" />
                            <asp:BoundField HeaderText="LogTime" DataField="LogDateTime" SortExpression="LogDateTime" />
                            <asp:BoundField HeaderText="Status" DataField="IsActive" SortExpression="IsActive" />

                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" CommandName="EditRow" Text="" CssClass="btn btn-sm btn-success btn-refrsh"
                                        CommandArgument='<%# Eval("DeviceId")%>'></asp:Button>&nbsp;&nbsp;
                                     <asp:Button ID="btnDelete" runat="server" CommandName="DeleteRow" Text="" CssClass="btn btn-sm red-mint btn-del"
                                         CommandArgument='<%# Eval("DeviceId")%>'></asp:Button>
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
                <span class="caption-subject txt-theme bold uppercase">Device Details</span>
            </div>
        </div>
        <div class="portlet-body">
            <div class="row">
                <div class="col-sm-12 no-padding">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label class="required">SN</label>
                            <asp:Label ID="lblSN" runat="server" class="form-control"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label class="required">Device Alias Name</label>
                            <Technocomctrl:ctlTextBox ID="txtDeviceAliasName" CssClass="form-control" runat="server"
                                MaxLength="100" MetaMandatory="true" MetaValidationRequired="true" MetaCaption="Alias Name"
                                MetaValidationGroupName="G1" />
                        </div>
                        
                        <div class="form-group">
                            <label class="required">Status</label>
                            <Technocomctrl:ctlDropDownList ID="ddlIsActive" runat="server" CssClass="form-control"
                                DataTextField="StatusTypeName" DataValueField="StatusTypeId" MetaMandatory="true" MetaValidationRequired="true"
                                MetaCaption="Status" MetaValidationGroupName="G2" />
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Online Status</label>
                            <asp:Label ID="lblOnlineStatus" runat="server" class="form-control"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label>IP Address</label>
                            <asp:Label ID="lblIPAddress" runat="server" class="form-control"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label>Server Port</label>
                            <asp:Label ID="lblServerPort" runat="server" class="form-control"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label>Att Log Stamp</label>
                           <asp:Label ID="lblAttLogStamp" runat="server" class="form-control"></asp:Label>
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
