<%@ Page Title="Artist View" Language="C#" MasterPageFile="~/PublicMaster.Master" AutoEventWireup="true" CodeBehind="IndexView.aspx.cs" Inherits="FirstView.Artist.IndexView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Utils/dist/css/lightbox.css" rel="stylesheet" />
    <script src="../Utils/dist/js/lightbox.js"></script>
    <div class="container">
        <div class="page-header">
            <h4>Artist - View</h4>
            <p class="lead">
                <a href="Index.aspx" class="btn btn-default" style="width: 80px" role="button">Back</a>
                <asp:Button ID="butLoadImages" runat="server" Text="" CausesValidation="false" Style="display: none" OnClick="butLoadImages_Click" />
                <asp:HiddenField ID="hidSelectedImage" runat="server" />
            </p>
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
        <table width="100%">
            <tr>
                <td class="col-md-12" style="vertical-align: top">
                    <div>
                        <asp:ListView ID="ArtistWork" runat="server" DataKeyNames="ArtistWorkID" GroupItemCount="6">
                            <EmptyDataTemplate>
                                <h2>There are no Artist Work items avaialble.</h2>
                            </EmptyDataTemplate>
                            <GroupTemplate>
                                <div class="row">
                                    <div class="col-md-8" runat="server" id="itemPlaceholder"></div>
                                </div>
                            </GroupTemplate>
                            <ItemTemplate>
                                <div class="col-md-2" style="margin-top:25px;">
                                    <div class="ArtistWorkItemContainer">
                                        <p>
                                            <a href="../Uploads/Resized/<%# Eval("ImageFileName")%>" class="example-image-link" data-lightbox="example-set" data-title="<%# Eval("PreviewTitle")%>">
                                                <img src="../Uploads/Thumbnails/<%# Eval("ImageFileName")%>" title="<%# Eval("WorkName")%>" class="ArtistWorkItem img-thumbnail" style="max-height:200px" />
                                            </a>
                                        </p>
                                    </div>
                                    <h5><%# Eval("TrimWorkName")%></h5>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </td>
            </tr>
        </table>
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('.img-thumbnail').css("height", "170px");
        });
    </script>
</asp:Content>
