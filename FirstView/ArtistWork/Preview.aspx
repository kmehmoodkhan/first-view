<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Preview.aspx.cs" Inherits="FirstView.ArtistWork.Preview" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="page-header">
            <h4>Artist Page - Preview</h4>
            <div class="row">
                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                    <asp:Button ID="butBack" Text="Back" runat="server" CssClass="btn btn-default" Width="80px" OnClick="butBack_Click" />

                </div>
            </div>
        </div>
        <div class="row" style="border-bottom: 1px solid #eeeeee;">
            <div class="col-md-1">
                <p>
                    <asp:Image ID="imgArtist" runat="server" CssClass="img-thumbnail" />
                </p>
            </div>
            <div class="col-md-11">
                <div>
                    <h4>
                        <asp:Label ID="lblFullname" runat="server" Text=""></asp:Label>
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
