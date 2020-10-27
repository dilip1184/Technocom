<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListBoxControl.ascx.cs" Inherits="TechnocomWeb.UI.UserControls.ListBoxControl" %>



    <div class="col-sm-5 m-t-20">
        <div class="panel panel-primary panel-checkbox-list panel-simple-list">
            <div class="panel-heading">
                <h3 class="panel-title"><asp:Label ID="Label1" runat="server" Text="Unassigned"></asp:Label>
                </h3>
            </div>
            <div class="panel-body checkBoxCtrl p-0">
                <div class="mt-checkbox-list checkBoxCtrl">
                    <asp:ListBox ID="listBox1" runat="server" SelectionMode="Multiple" ></asp:ListBox>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-1 m-t-20">
        <div class="col-sm-12 p-0 m-t-70">
            <asp:Button ID="btnForward" runat="server" Text=">" Font-Bold="true" OnClick="btnForward_Click" CssClass="btn btn-block btn-gery m-b-10" />
            <asp:Button ID="btnForwardAll" runat="server" Text=">>" Font-Bold="true" OnClick="btnForwardAll_Click" CssClass="btn btn-block btn-primary m-b-10" />
            <asp:Button ID="btnBackward" runat="server" Text="<" Font-Bold="true" OnClick="btnBackward_Click" CssClass="btn btn-block btn-warning m-b-10" />
            <asp:Button ID="btnBackwardAll" runat="server" Text="<<" Font-Bold="true" OnClick="btnBackwardAll_Click" CssClass="btn btn-block btn-info m-b-10" />
        </div>

    </div>
    <div class="col-sm-5 m-t-20">
        <div class="panel panel-warning panel-checkbox-list panel-simple-list">
            <div class="panel-heading">
                <h3 class="panel-title"><asp:Label ID="Label2" runat="server" Text="Assigned"></asp:Label>
                </h3>
            </div>
            <div class="panel-body checkBoxCtrl p-0">
                <div class="mt-checkbox-list checkBoxCtrl">
                    <asp:ListBox ID="listBox2" runat="server" SelectionMode="Multiple"></asp:ListBox>
                </div>
            </div>
        </div>
    </div>

