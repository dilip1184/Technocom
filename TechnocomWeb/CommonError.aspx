<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommonError.aspx.cs" Inherits="TechnocomWeb.CommonError" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Technocom Attendance</title>
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&amp;subset=all" rel="stylesheet" type="text/css" />

    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
     <link href="Content/css/error.min.css" rel="stylesheet" type="text/css" />
   
    <link href="Content/css/eneric-class.css" rel="stylesheet" />

    <script type="text/javascript" src="Scripts/CommonFunctions.js"></script>
</head>
<body class="page-404-full-page">
      <div class="row">
        <div class="col-md-12 page-404">
            <div class="col-md-6 text-right">
                  <asp:Image ID="ImgBranchLogo" runat="server" ImageUrl="~/Content/images/Securitaslogo.png" style="width: 350px; height: 190px; margin-bottom:40px;" />
            </div>
            <div class="col-md-4 text-left">
                <div class="number font-red">404 </div>
            <div class="details">
                <h3 class="text-left font-red f-w-600">Oops! You're lost.</h3>
                <p class="text-left">
                <span style="font-size:13px;">We can not find the page you're looking for <a href="LoginPage.aspx" class="text-primary">Click here</a> to login again.</span> 
                </p>
                <p class="text-left">
                   <asp:Label ID="lblCompanyCopyRight" CssClass="text-black f-12" runat="server" Text="" />
                </p>
            </div>
            </div>
        </div>
    </div>
</body>
</html>
