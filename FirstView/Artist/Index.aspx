<%@ Page Title="Artist Index" Language="C#" MasterPageFile="~/PublicMaster.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="FirstView.Artist.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .page-header{
            border-bottom:none!important;
            padding-bottom: 0px!important; 
        }
    </style>
    <script type='text/javascript'>
        $(document).ready(function () {
            $('#MainContent_txtSearchName').keypress(function (e) {
                if (e.keyCode == 13) {
                    $('#MainContent_butSearch').click();
                    return false;
                }
            });
        });

    </script>
    <div class="container">
        <div class="page-header">
            <h4>Artist - Index</h4>
            <p class="info-bar text ">
                <img src="../Images/Info.png" />
                    Please use the Search Filter or the Artist Index to find a specific Artist. Click on an Artist to see more details of their work."
            </p>
        </div>
        <br />
        <div class="panel panel-default">
            <div class="panel-heading">Search Filters</div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label1" runat="server" Text="Artist Type"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlArtistType" runat="server" CssClass="form-control" AutoPostBack="true" CausesValidation="false" OnSelectedIndexChanged="ddlArtistType_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label2" runat="server" Text="Search Name"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtSearchName" CssClass="form-control" Width="100%" runat="server"></asp:TextBox>
                    </div>
                    <div>
                        <button type="button" id="butSearch" class="btn btn-primary" runat="server" style="width: 80px" onserverclick="butSearch_ServerClick">
                            <span class="glyphicon glyphicon-search"></span>Search
                        </button>
                        <button type="button" id="butClearSearch" class="btn btn-default" runat="server" style="width: 80px" onserverclick="butClearSearch_ServerClick">
                            <span class="glyphicon glyphicon-repeat"></span>Clear
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <asp:Label ID="lblStatus" runat="server" Text="" CssClass="text-danger" Visible="false"></asp:Label>
        <br />
        <asp:Repeater ID="rptAlphabets" runat="server">
            <ItemTemplate>
                <a href="Index.aspx?sAlpha=<%#Eval("SurnameStart")%>" class="btn-sm  btn btn-info"><%#Eval("SurnameStart")%></a>
            </ItemTemplate>
        </asp:Repeater>
        <br />
        <br />
        <div id="artistindex" runat="server">
        </div>
    </div>

    <script type="text/javascript">
        function ShowArtistDetail(artistId) {
            var url = 'IndexView.aspx?ArtistID=' + artistId;
            window.open(url, '_blank');
            return false;
        }
    </script>
</asp:Content>
