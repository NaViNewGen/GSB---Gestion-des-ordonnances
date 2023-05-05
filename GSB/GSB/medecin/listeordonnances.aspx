﻿<%@ Page Title="" Language="C#" MasterPageFile="~/medecin/medecin.Master" AutoEventWireup="true" CodeBehind="listeordonnances.aspx.cs" Inherits="GSB.medecin.listeordonnances" %>
<asp:Content ID="Content1" ContentPlaceHolderID="c1" runat="server">

    <link href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css" type="text/css" rel="stylesheet"/>
<script src="https://code.jquery.com/jquery-3.3.1.js"></script>
<script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>

    <div class="col-lg-12">
        <div class="card">
            <div class="card-header">
                <strong class="card-title">Liste des patients</strong>
            </div>
            <div class="card-body">
                <asp:Repeater ID="listPatient" runat="server">
                    <HeaderTemplate>
                        <table class="table" id="pageSearch">
                            <thead class="thead-dark">
                                <tr>
                                    <th scope="col">ID</th>
                                    <th scope="col">Prénom</th>
                                    <th scope="col">Nom</th>
                                    <th scope="col">Age</th>
                                    <th scope="col">Date</th>
                                    <th scope="col">Statut</th>
                                    <th scope="col">Délivrée</th>
                                    <th scope="col">Annuler/Soumettre</th>
                                    <th scope="col">Modifier</th>
                                    <th scope="col">Supprimer</th>
                                    <th scope="col">Voir</th>

                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                         <tr>
                            <td><%#Eval("Id") %></td>
                            <td><%#Eval("firstname") %></td>
                            <td><%#Eval("lastname") %></td>
                            <td><%#Eval("patient_age") %></td>
                            <td><%#Eval("date_ordonnance") %></td>
                            <td><%#Eval("status") %></td>
                            <td><%#Eval("date_delivree") %></td>
                            <td><%# GetStatusLink(Eval("status")) %></td>
                            <td><%# GetStatusLinkMod(Eval("status")) %></td>
                            <td><%# GetStatusLinkSup(Eval("status")) %></td>
                             <td><a href ="show_ordonnance.aspx?id=<%#Eval("Id")%>" style="color:gray">Voir</a></td>
                            
                       </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

     

<script type="text/javascript">
    $(document).ready(function () {
        $('#pageSearch').DataTable({
            "pagingType": "full_numbers"
        });
    });
</script>

</asp:Content>