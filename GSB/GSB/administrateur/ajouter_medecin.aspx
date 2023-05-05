<%@ Page Title="" Language="C#" MasterPageFile="~/administrateur/admin.Master" AutoEventWireup="true" CodeBehind="ajouter_medecin.aspx.cs" Inherits="GSB.administrateur.ajouter_medecin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="c1" runat="server">


    <div class="col-lg-12">
        <div class="card">
            <div class="card-header">
                <strong class="card-title">Ajouter un Médecin</strong>
            </div>
            <div class="card-body">
                <!-- Credit Card -->
                <div id="pay-invoice">
                    <div class="card-body">

                        
                        <form id="form1" runat="server">
                        <div class="form-group">
                            <label>Prénom</label>
                            <asp:TextBox ID="prenom" runat="server" class="form-control" placeholder="Prénom"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Nom</label>
                            <asp:TextBox ID="nom" runat="server" class="form-control" placeholder="Nom"></asp:TextBox>
                        </div>
                        
                        <div class="form-group">
                                <label for="" class="control-label mb-1">Spécialité</label>
                                <asp:DropDownList ID="specialites" runat="server" CssClass="form-control"></asp:DropDownList>
                         </div>

                        <div class="form-group">
                            <label>Nom d'utilisateur</label>
                            <asp:TextBox ID="username" runat="server" class="form-control" placeholder="Nom d'utilisateur"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Mot de passe</label>
                            <asp:TextBox ID="password" runat="server" class="form-control" placeholder="Mot de passe" TextMode="Password"></asp:TextBox>
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
                            <label>Photo (Selectionner une Photo) </label>
                             <asp:FileUpload ID="photomedecin" runat="server" />
                        </div>
                      <div>
                        <asp:Button ID="ajoutButton" runat="server" class="btn btn-lg btn-info btn-block" Text="Ajouter" OnClick="ajoutButton_Click"/>
                            <div class="alert alert-success" id="msg" runat="server" style="margin-top: 10px; display: none">
                                    <strong>Ajouté(e) avec succès!</strong>
                                </div>
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
