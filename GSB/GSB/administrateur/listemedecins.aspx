<%@ Page Title="" Language="C#" MasterPageFile="~/administrateur/admin.Master" AutoEventWireup="true" CodeBehind="listemedecins.aspx.cs" Inherits="GSB.administrateur.listemedecins" %>
<asp:Content ID="Content1" ContentPlaceHolderID="c1" runat="server">
    <link href="https://cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css" type="text/css" rel="stylesheet"/>
<script src="https://code.jquery.com/jquery-3.3.1.js"></script>
<script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>





    <div class="col-lg-12">
        <div class="card">
            <div class="card-header">
                <strong class="card-title">Liste des Médecins</strong>
            </div>
            <div class="card-body">
                <asp:Repeater ID="listMedecin" runat="server">
                    <HeaderTemplate>
                        <table class="table" id="pageSearch">
                            <thead class="thead-dark">
                                <tr>
                                    <th scope="col">ID</th>
                                    <th scope="col">Photo</th>
                                    <th scope="col">Prénom</th>
                                    <th scope="col">Nom</th>
                                    <th scope="col">Spécialité</th>
                                    <th scope="col">Username</th>
                                    <th scope="col">Email</th>
                                    <th scope="col">Contact</th>
                                    <th scope="col">Modifier</th>
                                    <th scope="col">Supprimer</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                         <tr>
                             <td><%#Eval("Id") %></td>
                             <td><img src="../<%#Eval("photo_medecin")%>" height ="100" width="100"/></td>
                             <td><%#Eval("firstname") %></td>
                            <td><%#Eval("lastname") %></td>
                            <td><%#Eval("speciality") %></td>
                            <td><%#Eval("username") %></td>
                            <td><%#Eval("email") %></td>
                            <td><%#Eval("contact") %></td>
                             <td><a href ="edit_medecin.aspx?id=<%#Eval("Id")%>">Modifier</a></td>
                             <td><a href ="delete_medecin.aspx?id=<%#Eval("Id")%>" style="color:red">Supprimer</a></td>
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
