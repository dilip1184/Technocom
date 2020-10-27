<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="TechnocomWeb.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Technocom Attendance</title>
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&amp;subset=all" rel="stylesheet" type="text/css" />

    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="Content/css/ap-scroll-top.css" rel="stylesheet" />
    <link href="Content/css/metisMenu.css" rel="stylesheet" />
    <link href="Content/css/style.css" rel="stylesheet" />
    <link href="Content/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="Content/css/_all-skins.min.css" rel="stylesheet" />
    <link href="Content/css/skin-black.min.css" rel="stylesheet" />

    <script type="text/javascript" src="Scripts/CommonFunctions.js"></script>

    <style>
        .login-error {
            font-family: Verdana, Geneva, sans-serif;
            font-size: 13px;
            font-weight: normal;
            color: #c20000;
        }
    </style>

    <link rel="icon" href="favicon.ico" />

</head>
<body class="hold-transition login-page">
    <form id="form1" runat="server" defaultbutton="btnSignIn" defaultfocus="txtUserId"
        autocomplete="off">
        <ajax:ToolkitScriptManager ID="ScriptManager" EnablePartialRendering="true" runat="server"
            ScriptMode="Release" EnableViewState="false" CombineScripts="true" />
        <script type="text/javascript">

            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequest);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
            var ModalProgress = '<%=mpeSearching.ClientID %>';
            function beginRequest(sender, args) {
                // show the popup
                $find(ModalProgress).show();
            }

            function endRequest(sender, args) {
                $find(ModalProgress).hide();
            }
        </script>

        <ajax:ModalPopupExtender ID="mpeSearching" runat="server" TargetControlID="panSearching"
            PopupControlID="panSearching" BackgroundCssClass="progressBackground" DropShadow="false"
            RepositionMode="RepositionOnWindowScroll" />
        <asp:Panel runat="server" ID="panSearching" CssClass="loading-overlay" Style="display: none;">
            <asp:Image runat="server" ID="imgProgress" ImageUrl="Content/images/block-loading.gif" />
        </asp:Panel>
        <asp:UpdatePanel ID="upnlMain" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>

                <div class="login-box">
                    <div class="login-logo">
                        <a href="#"><b><span style ="font-size:28px;">Technocom</span></b> <span style ="font-size:28px;">Attendance</span></a>
                    </div>
                    <!-- /.login-logo -->
                    <div class="login-box-body">
                        <p class="login-box-msg">Sign In to Your Account</p>

                        <asp:Label ID="lblmsg" runat="server" CssClass="login-error"></asp:Label>
                        <br />
                        <br />

                        <div class="form-group has-feedback">
                            <asp:TextBox ID="txtUserId" runat="server" class="form-control form-control-solid placeholder-no-fix form-group" autocomplete="off" placeholder="Username" />
                            <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                        </div>
                        <div class="form-group has-feedback">
                            <asp:TextBox ID="txtPassword" runat="server" class="form-control form-control-solid placeholder-no-fix form-group" TextMode="Password" autocomplete="off" placeholder="Password" />
                            <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                        </div>
                        <div class="row login-m-t">
                            <div class="col-xs-8">
                                <input type="checkbox" />
                                Remember Me
                            </div>
                            <!-- /.col -->
                            <div class="col-xs-4">
                                <asp:Button ID="btnSignIn" runat="server" CssClass="btn btn-primary btn-block btn-flat" Text="Sign In" OnClick="btnSignIn_Click" />
                            </div>
                            <div class="col-sm-12 text-center forget-password"><a href="javascript:;">I Forgot My Password</a></div>
                        </div>


                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
