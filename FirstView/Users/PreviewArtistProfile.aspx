<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PreviewArtistProfile.aspx.cs" Inherits="FirstView.PublicView.PreviewArtistProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Utils/dist/css/lightbox.css" rel="stylesheet" />
    <script src="../Utils/dist/js/lightbox.js"></script>
    <script type='text/javascript'>
        function ShowPopupApprove() {
            $('#myModalApprove').modal('show');
            window.setTimeout(function () {
                window.location = "RegisterVerify.aspx?reg=1";
            }, 3000);
        }
        function ConfirmSubmitApprove() {
            if (confirm("Are you sure you want to submit your page for approval?") == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <style>
        .carousel-caption2 {
            background-color: #FFFFFF;
            position: relative;
            left: auto;
            right: auto;
            bottom: 0px;
            z-index: 10;
            padding-top: 5px;
            padding-bottom: 5px;
            color: #000000;
            text-align: center;
        }

        .carousel-control.left, .carousel-control.right {
            background-image: none !important;
            filter: none !important;
        }

        .carousel-indicators li {
            visibility: hidden;
        }
    </style>
    <div class="container" style="width:100%;">
        <div class="page-header">
            <h4>My Profile - Preview</h4>
            <div class="row">
                <div class="col-md-2 col-lg-3 col-sm-12 col-sx-12">
                    <a href="Menu.aspx" id="butBack" runat="server" class="btn btn-default" role="button" style="width: 80px">Back</a>
                    <asp:Button ID="butSubmitApprove" CssClass="btn btn-success" runat="server" Width="150px" Text="Submit for Approval" ToolTip="Submit for Approval" OnClick="butSubmitApprove_Click"></asp:Button>
                </div>
                <div class="col-md-2 col-lg-6 col-sm-12 col-sx-12">
                    <asp:TextBox ID="txtApprovalComment" runat="server" class="form-control" TextMode="MultiLine" Rows="1" placeholder="Enter your approval comments"></asp:TextBox>
                </div>
                <div class="col-md-2 col-lg-3 col-sm-12 col-sx-12 pull-right">
                    <p id="pGlyph" runat="server"><span class="glyphicon glyphicon-ok"></span>&nbsp;<asp:Label ID="lblApprovalStatus" runat="server" Text="" Visible="false"></asp:Label></p>
                </div>
            </div>
        </div>
        <div class="row" style="border-bottom: 1px solid #eeeeee;">
            <div class="col-md-2">
                <p>
                    <asp:Image ID="imgArtist" runat="server" CssClass="img-thumbnail" />
                </p>
            </div>
            <div class="col-md-10">
                <div>
                    <h4>
                        <asp:Label ID="lblFullname" runat="server" Text=""></asp:Label>(<asp:Label ID="lblArtistType" Font-Italic="true" ForeColor="Gray" Font-Size="10px" runat="server" Text=""></asp:Label>)
                    </h4>
                    <p>
                        <asp:Label ID="lblCV" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hidArtistID" runat="server" />
        <div id="myModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h3 class="modal-title">
                            <span id="lblPieceNameModal"></span>
                        </h3>
                    </div>
                    <div class="modal-body" id="popupbody" runat="server">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

