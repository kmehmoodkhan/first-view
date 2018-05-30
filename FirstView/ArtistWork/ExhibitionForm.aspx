<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExhibitionForm.aspx.cs" Inherits="FirstView.ArtistWork.ExhibitionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function ShowPopup() {
            $('#myModal').modal('show');
        }

        function ConfirmDelete() {
            if (confirm("Are you sure you want to delete this item?") == true) {
                return true;
            }
            else {
                return false;
            }
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "/FirstViewService.asmx/GetCurrentExhibition",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    if (r.d != undefined && r.d != '') {
                        var objJson = jQuery.parseJSON(r.d);
                        $("#dvExhibition").html(objJson[0].CurrentExhibition);
                    }
                },
                error: function (r) {
                    console.log('error=' + r.responseText);
                },
                failure: function (r) {
                    console.log('failure=' + r.responseText);
                }
            });
        });
        </script>
    <div class="container">
        <div class="page-header">
            <table width="100%">
                <tr>
                    <td align="left" width="20%">
                        <h4>Exhibition Entry Form</h4>
                        <p class="lead">
                            <a href="../Artist/Menu.aspx" class="btn btn-default" style="width: 80px" role="button">Back</a>
                            <asp:Button ID="btnSubmit" class="btn btn-default" runat="server" Width="80px" Text="Submit" OnClick="btnSubmit_Click" />
                        </p>
                    </td>
                    <td align="right" valign="middle">
                        <div style="font-size: 18px;" id="dvExhibition"></div>                  
                        Saturday 9th June - Sunday 1st July
                        <br />
                        Preview Friday 8th June 6PM--8PM
                    </td>
                </tr>
            </table>

        </div>
        <div class="row marginTop" style="width: 100%;">
            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                <label for="grid">Entries</label>
                <div class="input-group" style="width: 100%;">
                    <div style="width: 100%; max-height: 250px; overflow-y: scroll;">
                        <table width="100%" class="table table-bordered table-striped table-hover" cellspacing="0" align="left" rules="all" border="1" style="width: 100%; border-collapse: collapse; margin-bottom: 0px!important;">
                            <tr>
                                <th>Work Name</th>
                                <th width="10%">Is Framed</th>
                                <th width="10%">Medium</th>
                                <th width="10%">Width</th>
                                <th width="10%">Height</th>
                                <th width="10%">Artist Price</th>
                                <th width="10%">Wall Price</th>
                                <th width="15%">Select Item to add to exhibition</th>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 100%; max-height: 180px; overflow-y: scroll;">
                        <asp:GridView ID="gvArtistWorks" runat="server" Width="100%" HorizontalAlign="Left" OnRowDataBound="gvArtistWorks_RowDataBound"
                            AutoGenerateColumns="false" AllowPaging="true" ShowHeader="false"
                            DataKeyNames="ArtistWorkID,ArtistID,ImageFileName,IsDeleted,Width,Height,ExhibitionNo,Commission" CssClass="table table-bordered table-striped table-hover" OnPageIndexChanging="gvArtistWorks_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="WorkName" HeaderText="Work Name" />
                                <asp:BoundField DataField="FramedText" HeaderText="Is Framed" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Medium" HeaderText="Medium" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Width" HeaderText="Width" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Height" HeaderText="Height" ItemStyle-Width="10%" />
                                <asp:TemplateField ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtArtistPrice" Width="75px" CssClass="inputbox" runat="server" Text='<%#Eval("Price") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWallPrice" CssClass="inputbox" Width="75px" runat="server" Text='<%#Eval("Price") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Include in Exhibition" ItemStyle-Width="15%">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkInclude" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                    <hr />
                </div>
            </div>
        </div>
        <div class="row marginTop">
            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                <label for="subject">Comment</label>
                <div class="input-group" style="width: 100%;">
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="3" Width="98%" CssClass="inputbox"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row marginTop">
            <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                <label for="subject">Note</label>
                <div class="input-group" style="width: 100%;">
                    Entry fees may be paid by:-Credit or Debit Card at the Gallery or by telephone to 01747840747; by BCAs to Sort Code 40-45-23 account 81384937.
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
                        <p class="text-success">
                            <span class="glyphicon glyphicon-ok"></span>
                            The item has been deleted successfully.
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
