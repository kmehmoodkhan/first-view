﻿<%@ Page Title="Preview" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Preview.aspx.cs" Inherits="FirstView.Artist.Preview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Utils/dist/css/lightbox.css" rel="stylesheet" />
    <script src="../Utils/dist/js/lightbox.js"></script>
    <script type='text/javascript'>
        function ShowPopupApprove() {
            $('#myModalApprove').modal('show');
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
    <div class="container">
        <div class="page-header">
            <h4>My Page - Preview</h4>           
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
                        <asp:ListView ID="ArtistWork" runat="server" GroupItemCount="4" OnItemDataBound="ArtistWork_ItemDataBound">
                            <EmptyDataTemplate>
                                <h2>There are no artist Pieces</h2>
                            </EmptyDataTemplate>
                            <GroupTemplate>
                                <div class="row">
                                    <div class="col-md-8" runat="server" id="itemPlaceholder"></div>
                                </div>
                            </GroupTemplate>
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hdnfArtistWorkID" Value='<%# Eval("ArtistWorkID") %>' />
                                 <asp:HiddenField runat="server" ID="hiddenApprovalStatus" Value='<%# Eval("ApprovalStatus") %>' />
                                <div class="col-md-3" style="display: <%# Eval("Show")%>; margin-top: 25px;">
                                    <div class="ArtistWorkItemContainer">
                                        <p>
                                            <a href="../Uploads/Resized/<%# Eval("ImageFileName")%>" class="example-image-link" data-lightbox="example-set" data-title="<%# Eval("PreviewTitle")%>">
                                                <img src="../Uploads/Thumbnails/<%# Eval("ImageFileName")%>" title="<%# Eval("WorkName")%>" class="ArtistWorkItem img-thumbnail " />
                                            </a>
                                        </p>
                                    </div>
                                    <h5><%# Eval("TrimWorkName")%>(<%# Eval("IsApprovalText")%>)</h5>
                                    <asp:Panel CssClass="marginTop" Style="border: 1px dashed black; width: 100px;" ID="pnlExhibition" runat="server">
                                        <asp:Label ID="lblExbhition" Text="Exbn No-" runat="server"></asp:Label>
                                        <asp:Label ID="lblExhibitionNo" Width="25px" BackColor="White" ForeColor="Black" Text='<%# Eval("ExhibitionNo")%>' runat="server"></asp:Label>
                                    </asp:Panel>

                                </div>
                                <div class="col-md-3" style="display: <%# Eval("Hide")%>">
                                    <img src="../Uploads/Thumbnails/blank1.png" alt="" />
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
</asp:Content>
