<%@ Page Title="Home" Language="C#" MasterPageFile="~/PublicMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FirstView._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<br />
<center>
<div id="myCarousel" class="carousel slide" data-ride="carousel" style="width:600px">
  <!-- Indicators -->
  <ol class="carousel-indicators">
    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
    <li data-target="#myCarousel" data-slide-to="1"></li>
    <li data-target="#myCarousel" data-slide-to="2"></li>
    <li data-target="#myCarousel" data-slide-to="3"></li>
    <li data-target="#myCarousel" data-slide-to="4"></li>
    <li data-target="#myCarousel" data-slide-to="5"></li>
  </ol>

  <!-- Wrapper for slides -->
  <div class="carousel-inner" role="listbox">
    <div class="item active">
      <img src="Images/imageJQ-1.jpg" alt="">
    </div>
    <div class="item">
      <img src="Images/imageJQ-2.jpg" alt="">
    </div>
    <div class="item">
      <img src="Images/imageJQ-3.jpg" alt="">
    </div>
    <div class="item">
      <img src="Images/imageJQ-4.jpg" alt="">
    </div>
    <div class="item">
      <img src="Images/imageJQ-5.jpg" alt="">
    </div>
    <div class="item">
      <img src="Images/imageJQ-6.jpg" alt="">
    </div>
  </div>

  <!-- Left and right controls -->
  <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
    <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
    <span class="sr-only">Previous</span>
  </a>
  <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
    <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
    <span class="sr-only">Next</span>
  </a>
</div>
<br />
<div class="row">
    <div class="col-md-12" style="text-align:center">
        <p class="text-info">First-View Gallery is on the National Trust's Stourhead Estate. We show work of all disciplines from artists who, largely, have a connection with the Wessex region.</p>
        <p class="text-info">We are open every day, except Christmas Day, from 12 - 4pm until February 29th</p>
        <p class="text-info">From 1st March 11am - 5pm</p>
    </div>
</div>
</center>
</asp:Content>
