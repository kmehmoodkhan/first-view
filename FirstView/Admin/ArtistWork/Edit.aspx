<%@ Page Title="Artist Work Edit" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" EnableEventValidation="false" Inherits="FirstView.Admin.ArtistWork.Edit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="HeaderContent" ID="head" runat="server">
    <script src="../Scripts/dropzone.js" type="text/javascript"></script>
    <style>
        .RadioButtonWidth label {
            width: 95px;
            font-weight: normal !important;
        }

        .requiredF {
            color: orangered;
            font-size: 15px;
        }

        .imgcontainer {
            position: relative;
            margin-top: 0 auto;
            width: 200px;
            height: 180px;
        }

        .overlay {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 0);
            transition: background 0.5s ease;
        }

        .imgcontainer:hover .overlay {
            display: block;
            background: rgba(0, 0, 0, .3);
        }

        .iimg {
            position: absolute;
            width: 200px;
            height: 180px;
            left: 0;
        }

        .button {
            position: absolute;
            width: 80px;
            left: 30px;
            top: 30px;
            text-align: center;
            opacity: 0;
            transition: opacity .35s ease;
        }

            .button a {
                width: 80px;
                padding: 12px 20px;
                text-align: center;
                color: white;
                z-index: 1;
            }

        .imgcontainer:hover .button {
            opacity: 1;
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
        function openModalNoImage() {
            $('#myModalNoImage').modal('show');
        }
        function ShowError() {
            $('#modalError').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="page-header">
            <h4>Artist Work - Edit</h4>
            <p class="lead">
                <a href="Main.aspx" runat="server" id="btnBack" class="btn btn-default" style="width: 80px" role="button">Back</a>
                <asp:Button ID="butSave" ValidationGroup="Submit" class="btn btn-primary" Width="80px" runat="server" Text="Save" OnClick="butSave_Click" />
            </p>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="row marginTop">
                    <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 align-middle">
                        <label for="subject">Artist Name<span class="requiredF">*</span></label>
                        <div class="input-group">
                            <asp:Label ID="lblArtistName" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row marginTop">
                    <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 align-middle">
                        <label for="subject">Work Name<span class="requiredF">*</span></label>
                        <div class="input-group">
                            <asp:TextBox ID="txtWorkName" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ValidationGroup="Submit" ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtWorkName" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Work Name is a required field"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4 marginTop" id="divArtistPrice" runat="server">
                        <label for="subject">Artist Price<span class="requiredF">*</span></label>
                            <div class="input-group">
                                <asp:TextBox ID="txtArtistPrice" Width="78px" runat="server" class="form-control" MaxLength="100" AutoPostBack="true" OnTextChanged="txtArtistPrice_TextChanged"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ValidationGroup="Submit" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtArtistPrice" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Price is a required field"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterMode="ValidChars" ValidChars="0,1,2,3,4,5,6,7,8,9,." runat="server" TargetControlID="txtArtistPrice">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                    </div>
                    <div class="col-md-4 marginTop" id="divWallPrice" runat="server">
                        <label for="subject">Wall Price<span class="requiredF">*</span></label>
                            <div class="input-group">
                                <asp:TextBox ID="txtPrice" Width="78px" runat="server" class="form-control" MaxLength="100" AutoPostBack="true" OnTextChanged="txtPrice_TextChanged"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ValidationGroup="Submit" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPrice" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Price is a required field"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="txtPriceFTBE" FilterMode="ValidChars" ValidChars="0,1,2,3,4,5,6,7,8,9,." runat="server" TargetControlID="txtPrice">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                    </div>
                     <div class="col-md-4 marginTop" id="divCommission" runat="server">
                        <label for="subject">Commission %</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtCommission" Width="78px" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                            </div>
                    </div>
                </div>

                <div class="row marginTop" runat="server" id="divApproximateWallPrice" visible="false">
                    <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 align-middle">
                        <label for="subject">Approximate Wall Price</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtApproximateWallPrice" ReadOnly="true" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row marginTop">
                    <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 align-middle">

                        <label for="subject">Width (cms)<span class="requiredF">*</span></label>
                        <div class="input-group">
                            <asp:TextBox ID="txtWidth" runat="server" class="form-control" MaxLength="3"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ValidationGroup="Submit" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtWidth" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Width is a required field"></asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="txtWidthFTBE" FilterMode="ValidChars" ValidChars="0,1,2,3,4,5,6,7,8,9" runat="server" TargetControlID="txtWidth">
                            </cc1:FilteredTextBoxExtender>
                        </div>
                    </div>
                </div>
                <div class="row marginTop">
                    <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 align-middle">
                        <label for="subject">Height (cms)<span class="requiredF">*</span></label>
                        <div class="input-group">
                            <asp:TextBox ID="txtHeight" runat="server" class="form-control" MaxLength="9"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ValidationGroup="Submit" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtHeight" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Height is a required field"></asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="txtHeightFTBE" FilterMode="ValidChars" ValidChars="0,1,2,3,4,5,6,7,8,9" runat="server" TargetControlID="txtHeight">
                            </cc1:FilteredTextBoxExtender>
                        </div>
                    </div>
                </div>
                <div class="row marginTop">
                    <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 align-middle">
                        <label for="subject">Note</label>
                        <div class="input-group">
                            <asp:TextBox ID="txtNote" runat="server" class="form-control" TextMode="MultiLine" Rows="3" Width="280px"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <asp:UpdatePanel ID="upnRadio" runat="server">
                    <ContentTemplate>
                        <div class="row marginTop">
                            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 align-middle">
                                <label for="subject">&nbsp;</label>
                                <div class="input-group">
                                    &nbsp;
                                </div>
                            </div>
                        </div>
                        <div class="row marginTop">
                            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 align-middle">
                                <label for="subject">Medium</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtMedium" runat="server" class="form-control"></asp:TextBox>
                                    <br />
                                    <asp:RequiredFieldValidator ID="rfvtxtMedium" ValidationGroup="Submit" runat="server" ControlToValidate="rbltPresenation" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Medium is a required field"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row marginTop">
                            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 align-middle">
                                <label for="subject">Presentation<span class="requiredF">*</span></label>
                                <div class="input-group" style="width: 100% !important;">
                                    <asp:RadioButtonList ID="rbltPresenation" CssClass="RadioButtonWidth" runat="server" RepeatColumns="4" Font-Bold="false" RepeatDirection="Horizontal" TextAlign="Left" Width="100%"
                                        RepeatLayout="Table">
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfvrbltPresenation" ValidationGroup="Submit" runat="server" ControlToValidate="rbltPresenation" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Presentation is a required field"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row marginTop">
                            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 align-middle">
                                <label for="subject">Type<span class="requiredF">*</span></label>
                                <div class="input-group" style="width: 100% !important;">
                                    <asp:RadioButtonList ID="rbltEditionType" CssClass="RadioButtonWidth" OnSelectedIndexChanged="rbltEditionType_SelectedIndexChanged" AutoPostBack="true" runat="server" Font-Bold="false" RepeatColumns="4" RepeatDirection="Horizontal" TextAlign="Left" Width="100%"
                                        RepeatLayout="Table">
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfvrbltEditionType" ValidationGroup="Submit" runat="server" ControlToValidate="rbltEditionType" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Type is a required field."></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row marginTop" id="rowNum" runat="server" visible="false">
                            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 align-middle">
                                <label for="subject">Num<span class="requiredF">*</span></label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtEditionNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtEditionNumber_FilteredTextBoxExtender" FilterMode="ValidChars" ValidChars="0,1,2,3,4,5,6,7,8,9" runat="server" TargetControlID="txtEditionNumber">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                        <div class="row marginTop" id="rowEdition" runat="server" visible="false">
                            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 align-middle">
                                <label for="subject">Edition<span class="requiredF">*</span></label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtHeighestEdition" CssClass="form-control" runat="server"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtHeighestEdition_FilteredTextBoxExtender" FilterMode="ValidChars" ValidChars="0,1,2,3,4,5,6,7,8,9" runat="server" TargetControlID="txtHeighestEdition">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                        <div class="row marginTop" id="addexhitionrow" runat="server">
                            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 align-middle">
                                <label for="subject">Current Exhibition</label>
                                <div class="input-group">
                                    <div style="float: left;">
                                        <asp:CheckBox ID="chkIsParticipating" runat="server" />
                                    </div>
                                    <div style="float: left;">
                                        <asp:Label ID="lblExhibitionName" runat="server"></asp:Label>
                                    </div>
                                    <asp:HiddenField ID="hdnfExhibitionNo" runat="server" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-4">
                <div class="row marginTop">
                    <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 align-middle">
                        <label for="subject">&nbsp;</label>
                        <div class="input-group">
                            &nbsp;
                        </div>
                    </div>
                </div>
                <div class="row marginTop">
                    <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12 align-middle">
                        <label for="subject">Image</label>
                        <div class="input-group">
                            <iframe id="ifrmUpload" runat="server" style="border: none;"></iframe>
                            <br />
                            <div class="imgcontainer">
                                <asp:Image ID="imgArtistWork" runat="server" CssClass="img-thumbnail" />
                                <div class="overlay"></div>
                                <div class="button">
                                    <a href="#" id="lnk" runat="server" onserverclick="lnk_ServerClick">
                                        <img style="width: 100px; height: auto;" id="imgDownload" src="../../Images/download.png" alt="Download" />
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hidUniqueID" runat="server" />
            </div>
        </div>
        <div class="row marginTop">
            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                <div style="float: right;">
                    <asp:Button ID="btnSaveBottom" class="btn btn-primary" Width="80px" ValidationGroup="Submit" runat="server" Text="Save" OnClick="butSave_Click" />
                    <asp:Button ID="btnReset" class="btn btn-secondary" Width="80px" runat="server" Text="Reset" OnClick="btnReset_Click" />
                </div>
            </div>
        </div>
    </div>
    <div id="myModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">Confirmation</h4>
                </div>
                <div class="modal-body">
                    <p class="text-success">The data has been saved successfully.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="modalError" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">Error</h4>
                </div>
                <div class="modal-body">
                    <p class="text-danger">
                        <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalNoImage" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">Error</h4>
                </div>
                <div class="modal-body">
                    <p class="text-danger"><span class="glyphicon glyphicon-remove"></span>&nbsp;Please select a valid image file to upload.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
