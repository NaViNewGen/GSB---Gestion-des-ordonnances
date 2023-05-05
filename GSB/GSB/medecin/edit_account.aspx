<%@ Page Title="" Language="C#" MasterPageFile="~/medecin/medecin.Master" AutoEventWireup="true" CodeBehind="edit_account.aspx.cs" Inherits="GSB.medecin.edit_account" %>
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
                                <label for="" class="control-label mb-1">Spécialité</label>
                                <asp:DropDownList ID="specialites" runat="server" CssClass="form-control"></asp:DropDownList>
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
                            <label>Photo (Selectionner une Photo) </label>
                                <label for="" class="control-label mb-1"> Photo</label><br />
                                <asp:Label ID="photomedecinlabel" runat="server" Text=""></asp:Label>
                             <asp:FileUpload ID="photomedecin" runat="server" />
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
