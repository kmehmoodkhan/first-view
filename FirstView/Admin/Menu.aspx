<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="FirstView.Admin.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="page-header">
            <h4>Administration</h4>
            <p class="lead">
            </p>
        </div>
        <br />
         <div class="col-md-3" style="text-align:center">
            <a href="Users/PersonalDetails.aspx" class="link">
            <img src="../Images/Users_100.png" alt="Manage Users" />
            <p class="text">Personal Details</p>
            </a>
        </div>
        <div class="col-md-3" style="text-align:center">
            <a href="Users/List.aspx" class="link">
            <img src="../Images/Users_100.png" alt="Manage Users" />
            <p class="text">Manage Users</p>
            </a>
        </div>
        <div class="col-md-3" style="text-align:center">
            <a href="Artists/List.aspx" class="link">
            <img src="../Images/Artists_100.png" alt="Manage Artists"  />
            <p class="text">Manage Artists</p>
            </a>
        </div>
        <div class="col-md-3" style="text-align:center">
            <a href="ArtistWork/List.aspx" class="link">
            <img src="../Images/ArtistWork_100.png" alt="Manage Artist Work" />
            <p class="text">Manage Artist Work</p>
            </a>
        </div>
        <div class="col-md-3" style="text-align:center">
            <a href="Approvals/List.aspx" class="link">
            <img src="../Images/Preview_100.png" alt="Manage Approvals" />
            <p class="text">Manage Approvals</p>
            </a>
        </div>
        <div class="col-md-3" style="text-align:center">
            <a href="Settings/List.aspx" class="link">
            <img src="../Images/Settings_100.png" alt="Manage Settings" />
            <p class="text">Manage Settings</p>
            </a>
        </div>
        <div class="col-md-3" style="text-align:center">
            <a href="ArtistType/List.aspx" class="link">
            <img src="../Images/ArtistType_100.png" alt="Manage Artist Types" />
            <p class="text">Manage Artist Types</p>
            </a>
        </div>
         <div class="col-md-3" style="text-align:center">
            <a href="Events/List.aspx" class="link">
            <img src="../Images/ArtistType_100.png" alt="Manage Events" />
            <p class="text">Manage Events</p>
            </a>
        </div>
    </div>
</asp:Content>
