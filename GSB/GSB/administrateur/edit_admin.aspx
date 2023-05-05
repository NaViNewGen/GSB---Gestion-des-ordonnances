<%@ Page Title="" Language="C#" MasterPageFile="~/administrateur/admin.Master" AutoEventWireup="true" CodeBehind="edit_admin.aspx.cs" Inherits="GSB.administrateur.edit_admin" %>
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
                            <asp:TextBox ID="firstname" runat="server" class="form-control" placeholder="Prénom"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Nom</label>
                            <asp:TextBox ID="lastname" runat="server" class="form-control" placeholder="Nom"></asp:TextBox>
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
                            <label>Email </label>
                            <asp:TextBox ID="email" runat="server" class="form-control" placeholder="Email"></asp:TextBox>
                        </div>
                         <div class="form-group">
                            <label>Contact</label>
                            <asp:TextBox ID="contact" runat="server" class="form-control" placeholder="Contact"></asp:TextBox>
                        </div>

                            <div>

                                <asp:Button ID="updateAdmin" runat="server" class="btn btn-lg btn-info btn-block" Text="Confirmer" OnClick="updateAdmin_Click" />
                                
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
