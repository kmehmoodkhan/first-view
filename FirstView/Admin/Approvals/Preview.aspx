<%@ Page Title="Preview" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Preview.aspx.cs" Inherits="FirstView.Admin.Approvals.Preview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Utils/dist/css/lightbox.css" rel="stylesheet" />
    <script src="../../Utils/dist/js/lightbox.js"></script>
    <script type='text/javascript'>
        function ShowPopupApprove() {
            $('#myModalApprove').modal('show');
        }
        function ShowPopupReject() {
            $('#myModalReject').modal('show');
        }
        function ShowPopupReset() {
            $('#myModalReset').modal('show');
        }
        function ConfirmApprove() {
            if (confirm("Are you sure you want to approve this page?") == true) {
                return true;
            }
            else {
                return false;
            }
        }
        function ConfirmReject() {
            if (confirm("Are you sure you want to reject this page?") == true) {
                return true;
            }
            else {
                return false;
            }
        }
        function ConfirmReset() {
            if (confirm("Are you sure you want to reset this page?") == true) {
                return true;
            }
            else {
                return false;
            }
        }
        function UpdateArtistWorkExhibition(elem) {            
            var id = elem.children[0].id;
            var chkExhibitionParticipationId = id.replace("chkWork", "chkExhibitionParticipation");
         
            var hdnfArtistWork = $(elem).parent().find('input:hidden:first');            
            var artistWorkId = $(hdnfArtistWork).val();
            $.ajax({
                type: "POST",
                url: "/FirstViewService.asmx/UpdateArtistWorkExhibitionNo",
                data: JSON.stringify({ ArtistWorkId: artistWorkId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    if (r.d != undefined && r.d != '') {
                        var objJson = jQuery.parseJSON(r.d);
                        if (parseInt(objJson) > 0) {
                            alert('Artist Work added to current exhibition')
                        }
                        if (parseInt(objJson) == 0) {
                            alert('Artist Work removed from current exhibition')
                        }
                    }
                },
                error: function (r) {
                    console.log('error=' + r.responseText);
                },
                failure: function (r) {
                    console.log('failure=' + r.responseText);
                }
            });
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
    <div class="container">
        <div class="page-header">
            <h4>Artist Page - Preview</h4>
            <div class="row">
                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                    <asp:Button ID="butBack" Text="Back" runat="server" CssClass="btn btn-default" Width="80px" OnClick="butBack_Click" />
                    <asp:Button ID="butApprove" CssClass="btn btn-success" runat="server" Width="80px" Text="Approve" ToolTip="Approve Page" OnClientClick="return confirm('Are you sure to approve selected items?');" OnClick="butApprove_Click" CausesValidation="false"></asp:Button>
                    <asp:Button ID="butReject" CssClass="btn btn-danger" runat="server" Width="80px" Text="Reject" ToolTip="Reject Page" OnClientClick="return confirm('Are you sure to reject selected items?');" OnClick="butReject_Click" CausesValidation="false"></asp:Button>
                    <asp:Button ID="butReset" CssClass="btn btn-warning" runat="server" Width="80px" Text="Reset" ToolTip="Reset Page" OnClick="butReset_Click" CausesValidation="false"></asp:Button>

                </div>
                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                    <asp:TextBox ID="txtApprovalComment" runat="server" class="form-control" TextMode="MultiLine" Rows="1" placeholder="Enter your approval comments"></asp:TextBox></td>
                </div>
                <div class="col-lg-4 col-md- col-sm-12 col-xs-12">
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
                        <asp:Label ID="lblFullname" runat="server" Text=""></asp:Label>
                        &nbsp;<asp:Label ID="lblArtistCommission" Visible="false" runat="server"></asp:Label>
                    </h4>
                    <p>
                        <asp:Label ID="lblCV" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <h4>Artist's Work</h4>
            </div>
        </div>

        <asp:ListView ID="ArtistWork" runat="server" GroupItemCount="6" OnItemDataBound="ArtistWork_ItemDataBound" DataKeyNames="ArtistWorkID,IsAllowed">
            <EmptyDataTemplate>
                <h2>There are no Artist Work items avaialble.</h2>
            </EmptyDataTemplate>
            <GroupTemplate>
                <div class="row">
                    <div class="col-md-8" runat="server" id="itemPlaceholder"></div>
                </div>
            </GroupTemplate>
            <ItemTemplate>
                <div class="col-md-2" style="margin-bottom: 25px;">
                    <div class="ArtistWorkItemContainer">
                        <p>
                            <a href="../../Uploads/Resized/<%# Eval("ImageFileName")%>" class="example-image-link" data-lightbox="example-set" data-title="<%# Eval("PreviewTitle")%>">
                                <img src="../../Uploads/Thumbnails/<%# Eval("ImageFileName")%>" title="<%# Eval("WorkName")%>" class="ArtistWorkItem img-thumbnail" />
                            </a>
                        </p>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnfExhibitionNo" Value='<%#Eval("CurrentExhibitionNo") %>' />
                    <asp:HiddenField runat="server" ID="hdnfIsApproved" Value='<%#Eval("IsApproved") %>' />
                    <div style="float: left; margin-top: 5px;">
                        <asp:CheckBox ID="chkWork" runat="server" Text='<%# Eval("TrimWorkName")%>' TextAlign="Right" />
                        <asp:CheckBox ID="chkExhibitionParticipation" Text="Exhibition" onchange="UpdateArtistWorkExhibition(this);" TextAlign="Right" runat="server" />
                        <asp:HiddenField runat="server" ID="hdnfArtistWorkId" Value='<%#Eval("ArtistWorkID") %>' />                        
                    </div>

                </div>
            </ItemTemplate>
        </asp:ListView>

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
        <div id="myModalApprove" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title">Confirmation</h4>
                    </div>
                    <div class="modal-body">
                        <p class="text-success">
                            <span class="glyphicon glyphicon-ok"></span>
                            The artist page has been approved.
                        </p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="myModalReject" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title">Confirmation</h4>
                    </div>
                    <div class="modal-body">
                        <p class="text-danger">
                            <span class="glyphicon glyphicon-ok"></span>
                            The artist page has been rejected.
                        </p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="myModalReset" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title">Confirmation</h4>
                    </div>
                    <div class="modal-body">
                        <p class="text-info">
                            <span class="glyphicon glyphicon-ok"></span>
                            The artist page has been reset.
                        </p>
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
