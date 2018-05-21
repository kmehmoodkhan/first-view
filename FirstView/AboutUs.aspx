<%@ Page Title="About Us" Language="C#" MasterPageFile="~/PublicMaster.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="FirstView.AboutUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="page-header">
            <h4>About Us</h4>
        </div>
        <br />
        <div class="row">
            <div class="col-md-6">
                Situated on the National Trust's famous Stourhead Estate the Gallery was opened in 2002 and is owned by Jane Parfitt.
                <br />
                The Gallery is divided into 4 sections one of which is the Studio which may be hired for art and craft related activities.
                The Gallery is located in the Spread Eagle Courtyard, a favourite place for visitors to relax. 
                <br />
                We are always anxious to meet new artists and this is best arranged by making an initial contact using email or the <a href="ContactUs.aspx">contact form</a>.
            </div>
            <div class="col-md-2">
                <img src="Images/AboutUs_01_beamgallery.jpg" class="img-responsive" />
                <p class="text">Beam Gallery</p>
            </div>
            <div class="col-md-2">
                <img src="Images/AboutUs_02_archway_gallery.jpg" class="img-responsive"/>
                <p class="text">Archway Gallery</p>
            </div>
            <div class="col-md-2">
                <img src="Images/AboutUs_03_arch_window_gallery.jpg" class="img-responsive" />
                <p class="text">Arch Window Gallery</p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6"></div>
            <div class="col-md-2">
                <img src="Images/AboutUs_04_studio.jpg" class="img-responsive"/>
                <p class="text">The Studio</p>
            </div>
            <div class="col-md-2"> 
                <img src="Images/AboutUs_05_spread_eagle_courtyard.jpg" class="img-responsive"/>
                <p class="text">Spread Eagle Courtyard</p>
            </div>
        </div>
    </div>
</asp:Content>
