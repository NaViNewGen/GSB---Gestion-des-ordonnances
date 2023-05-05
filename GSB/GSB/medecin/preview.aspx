<%@ Page Title="" Language="C#" MasterPageFile="~/medecin/medecin.Master" AutoEventWireup="true" CodeBehind="preview.aspx.cs" Inherits="GSB.medecin.preview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="c1" runat="server">
 
    <div class="col-lg-12">
        <div class="card">
            <div class="card-header">
                <strong class="card-title">Aperçu Ordonnance</strong>
            </div>
            <div class="card-body">
                <!-- Credit Card -->
                <div id="pay-invoice">
                    <div class="card-body">

                        
                        <form action="" id="f1" runat="server" method="post" novalidate="novalidate">
                                <asp:TextBox ID="previewTextBox" runat="server" TextMode="MultiLine" Height="530px" Width="756px"></asp:TextBox>


                            <div style="display: flex; justify-content: space-between; width: 100%;">
                                <asp:Button ID="saveButton" runat="server" class="btn btn-lg btn-info" Text="Enregistrer" OnClick="saveButton_Click" style="width: 100%;" />
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
