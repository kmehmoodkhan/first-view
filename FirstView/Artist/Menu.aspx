<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="FirstView.Artist.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="page-header">
            <h4>Menu</h4>
            <p class="lead">
            </p>
        </div>
        <br />
        <div class="col-md-3" style="text-align:center">
            <a href="ProfileEdit.aspx" class="link">
            <img src="../Images/Users_100.png" alt="Manage My Page" />
            <p class="text">My Profile</p>
            </a>
        </div>
        <div class="col-md-3" style="text-align:center">
            <a href="../ArtistWork/Main.aspx" class="link">
            <img src="../Images/ArtistWork_100.png" alt="Manage My Work"  />
            <p class="text">Manage My Work</p>
            </a>
        </div>
        <div class="col-md-3" style="text-align:center">
            <a href="ProfileInformation.aspx" class="link">
            <img src="../Images/UAC_100.png" alt="Change My Password" />
            <p class="text">Personal Details</p>
            </a>
        </div>
        <div class="col-md-3" style="text-align:center">
            <a href="Preview.aspx" class="link">
            <img src="../Images/Preview_100.png" alt="Preview My Page" />
            <p class="text">Preview My Page</p>
            </a>
        </div>
    </div>
</asp:Content>
