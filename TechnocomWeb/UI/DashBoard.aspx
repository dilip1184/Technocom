<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Shared/default.Master" AutoEventWireup="true"
    CodeBehind="DashBoard.aspx.cs" Inherits="TechnocomWeb.UI.DashBoard" %>

<%@ Register Assembly="TechnocomControl" Namespace="TechnocomControl" TagPrefix="Technocomctrl" %>
  
<asp:Content ID="ContentBody1" ContentPlaceHolderID="placeHolderBody1" runat="server">
    
    <div class="col-sm-12">
        <div class="row">
            <div class="col-lg-3 col-xs-6">
                <div class="small-box bg-aqua">
                    <div class="inner">
                        <h3>14300</h3>
                        <p>Today Attendance</p>
                    </div>
                    <div class="icon">
                        <i class="fa fa-briefcase"></i>
                    </div>
                    <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                </div>
            </div>
            <div class="col-lg-3 col-xs-6">
                <div class="small-box bg-green">
                   <div class="inner">
                        <h3>9100</h3>
                        <p>Attendance By Mobile</p>
                    </div>
                    <div class="icon">
                        <i class="fa fa-bar-chart"></i>
                    </div>
                    <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                </div>
            </div>
            <div class="col-lg-3 col-xs-6">
                <div class="small-box bg-yellow">
                    <div class="inner">
                        <h3>15000</h3>
                        <p>Registered Employees</p>
                    </div>
                    <div class="icon">
                        <i class="fa fa-user"></i>
                    </div>
                    <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                </div>
            </div>
            <%--<div class="col-lg-3 col-xs-6">
                <div class="small-box bg-red">
                    <div class="inner">
                        <h3>65</h3>
                        <p>Unique Visitors</p>
                    </div>
                    <div class="icon">
                        <i class="fa fa-pie-chart"></i>
                    </div>
                    <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                </div>
            </div>--%>
        </div>
       

    </div>
  
</asp:Content>
