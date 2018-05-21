<%@ Page Title="Studio" Language="C#" MasterPageFile="~/PublicMaster.Master" AutoEventWireup="true" CodeBehind="Studio.aspx.cs" Inherits="FirstView.Studio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="page-header">
            <h4>Studio</h4>
        </div>
        <br />
        <div class="row">
            <div class="col-md-6">
                This space is available for hire for art, craft, photography and similar events.
                <br />
                Workshops are another popular use.
                <br />
                First-View use this space for their regular "Emporia" when a collection of interesting and diverse range of items are offered for sale.
                <br />
                When not used for any of these activities the Studio becomes a natural extension of the Gallery
                <br />
                If you are interested in further information please use the <a href="ContactUs.aspx">contact form</a>.
            </div>
            <div class="col-md-2">
                <img src="Images/studio_01.jpg" class="img-responsive" />
                <p class="text">The Studio is used for a variety of events</p>
            </div>
            <div class="col-md-2">
                <img src="Images/studio_02.jpg" class="img-responsive"/>
                <p class="text">An exhibition space</p>
            </div>
            <div class="col-md-2">
                <img src="Images/studio_03.jpg"  class="img-responsive" />
                <p class="text">Selling</p>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6"></div>
            <div class="col-md-2">
                <img src="Images/AboutUs_04_studio.jpg" class="img-responsive"/>
                <p class="text">Fairs</p>
            </div>
            <div class="col-md-2"> 
                <img src="Images/studio_05.jpg" class="img-responsive"/>
                <p class="text">Mixed</p>
            </div>
        </div>
    </div>
</asp:Content>
