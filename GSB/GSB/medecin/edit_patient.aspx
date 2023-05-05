<%@ Page Title="" Language="C#" MasterPageFile="~/medecin/medecin.Master" AutoEventWireup="true" CodeBehind="edit_patient.aspx.cs" Inherits="GSB.medecin.edit_patient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="c1" runat="server">
    <div class="col-lg-12">
        <div class="card">
            <div class="card-header">
                <strong class="card-title">Modifier les informations</strong>
            </div>
            <div class="card-body">
                <!-- Credit Card -->
                <div id="pay-invoice">
                    <div class="card-body">

                        
                        <form action="" id="f1" runat="server" method="post" novalidate="novalidate">

                            <div class="form-group">
                                <label>Prénom</label>
                            <asp:TextBox ID="firstname" runat="server" class="form-control" placeholder="First Name"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Nom</label>
                            <asp:TextBox ID="lastname" runat="server" class="form-control" placeholder="Last Name"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Sexe</label>
                            <asp:DropDownList ID="sexeDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="monthDropDownList_SelectedIndexChanged">
                                    <asp:ListItem Text="homme" Value="homme"></asp:ListItem>
                                    <asp:ListItem Text="femme" Value="femme"></asp:ListItem>
                             </asp:DropDownList>
                        </div>
                         <div>
                             <label>Date de naissance</label><br>
                            
                             
                             <asp:Label ID="Labelday" runat="server" Text="Jour :  "></asp:Label>
                             <asp:DropDownList ID="dayDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dayDropDownList_SelectedIndexChanged">
                                 
                             </asp:DropDownList>
                             <asp:Label ID="Labelmonth" runat="server" Text="Mois :  "></asp:Label>
                             <asp:DropDownList ID="monthDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="monthDropDownList_SelectedIndexChanged">
                                    <asp:ListItem Text="Janvier" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Février" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Mars" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Avril" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Mai" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Juin" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Juillet" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Août" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="Septembre" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="Octobre" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="Novembre" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="Décembre" Value="12"></asp:ListItem>
                             </asp:DropDownList>
                             <asp:Label ID="Labelyear" runat="server" Text="Année :   "></asp:Label>
                             <asp:DropDownList ID="yearDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="yearDropDownList_SelectedIndexChanged">
                                 
                             </asp:DropDownList>
                            
                             <asp:TextBox ID="datenaissance" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                            <div class="form-group">
                            <label>Username</label>
                            <asp:TextBox ID="username" runat="server" class="form-control" placeholder="Username"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Password</label>
                            <asp:TextBox ID="password" runat="server" class="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Email</label>
                            <asp:TextBox ID="email" runat="server" class="form-control" placeholder="Email"></asp:TextBox>
                        </div>
                         <div class="form-group">
                            <label>Contact</label>
                            <asp:TextBox ID="contact" runat="server" class="form-control" placeholder="Contact"></asp:TextBox>
                        </div>
                            <div class="form-group">
                                <label for="" class="control-label mb-1">Groupage</label>
                                <asp:DropDownList ID="groupage" runat="server" CssClass="form-control"></asp:DropDownList>
                         </div>
                         <div class="form-group">
                            <label>Photo (Selectionner une Photo) </label>
                                <label for="" class="control-label mb-1">Photo</label><br />
                                <asp:Label ID="photopatientlabel" runat="server"></asp:Label>
                             <asp:FileUpload ID="photopatient" runat="server" />
                        </div>


                            <div>

                                <asp:Button ID="updateButton" runat="server" class="btn btn-lg btn-info btn-block" Text="Confirmer" OnClick="updateButton_Click"/>
                                
                            </div>
                        </form>
                    </div>
                </div>

            </div>
        </div>
        <!-- .card -->

    </div>
    <!--/.col-->
</asp:Content>
