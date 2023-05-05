<%@ Page Title="" Language="C#" MasterPageFile="~/medecin/medecin.Master" AutoEventWireup="true" CodeBehind="edit_ordonnance.aspx.cs" Inherits="GSB.medecin.edit_ordonnance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="c1" runat="server">

    <div class="col-lg-12">
        <div class="card">
            <div class="card-header">
                <strong class="card-title">Modifier Ordonnance</strong>
            </div>
            <div class="card-body">
                <!-- Credit Card -->
                <div id="pay-invoice">
                    <div class="card-body">

                        
                        <form action="" id="f1" runat="server" method="post" novalidate="novalidate">

                           <div class="form-group">
                                <label for="" class="control-label mb-1">ID Patient</label>
                                <asp:DropDownList ID="patientid" runat="server" CssClass="form-control" Enabled="false" ></asp:DropDownList>
                                <asp:Label ID="Label1" runat="server" Text="Prénom : "></asp:Label>
                                <asp:TextBox ID="patientfirstname" runat="server" Enabled ="false"></asp:TextBox>
                               <asp:Label ID="Label2" runat="server" Text="Nom : "></asp:Label> 
                               <asp:TextBox ID="patientlastname" runat="server" Enabled ="false"></asp:TextBox>
                               <asp:Label ID="Label3" runat="server" Text="Age : "></asp:Label> 
                               <asp:TextBox ID="patientage" runat="server" Enabled ="false"></asp:TextBox>
                           </div>


                            <div class="form-group">
                                <label for="" class="control-label mb-1">Le nombre des médicaments ： </label>
                                <asp:TextBox ID="medicinsNumber" runat="server" class="form-control" AutoPostBack ="true" OnTextChanged="medicinsNumber_TextChanged"></asp:TextBox>
                            </div>
                            <div>
                            <asp:Panel ID="medicinsPanel" runat="server" Height="350px" ScrollBars="Vertical">
                            </asp:Panel>
                            </div>
                       
                            <div>

                                <asp:Button ID="modifierdonnance" runat="server" class="btn btn-lg btn-info btn-block" Text="Confirmer" OnClick="modifierdonnance_Click" />
                                <div class="alert alert-success" id="msg" runat="server" style="margin-top: 10px; display: none">
                                    <strong>Confirmée !</strong>
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
