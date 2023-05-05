<%@ Page Title="" Language="C#" MasterPageFile="~/pharmacien/pharmacien.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="GSB.pharmacien.search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="c1" runat="server">
    <div class="col-lg-12">
        <div class="card">
            <div class="card-header">
                <strong class="card-title">Recherche</strong>
            </div>
            <div class="card-body">
                <!-- Credit Card -->
                <div id="pay-invoice">
                    <div class="card-body">

                        
                        <form action="" id="f1" runat="server" method="post" novalidate="novalidate">

                            <div class="form-group">
                                <label>Champs de Recherche</label>
                            <asp:TextBox ID="searchfield" runat="server" class="form-control" placeholder="Recherche"></asp:TextBox>
                            </div>
                                <label>Rechercher par</label>
                                <asp:CheckBox ID="patientname" runat="server" Text="Nom" AutoPostBack="true" OnCheckedChanged="patientname_CheckedChanged" />
                                <asp:CheckBox ID="ordonnancenumber" runat="server" Text="ID d'ordonnance" AutoPostBack="true" OnCheckedChanged="ordonnancenumber_CheckedChanged" />
                            <div>
                            </div>

                            <div>
                                <asp:Button ID="searchButton" runat="server" class="btn btn-lg btn-info btn-block" Text="Rechercher" OnClick="searchButton_Click"/>
                                <div class="alert alert-danger" id ="error" runat="server" style="margin-top: 10px;display:none">
                                <strong>Sélectionner Rechercher par !</strong>
                                </div>
                            </div>

               <div class="card-body">
                <asp:Repeater ID="listPatient" runat="server">
                    <HeaderTemplate>
                        <table class="table" id="pageSearch">
                            <thead class="thead-dark">
                                <tr>
                                    <th scope="col">Ord.ID</th>
                                    <th scope="col">Nom Patient</th>
                                    <th scope="col">Prénom Patient</th>
                                    <th scope="col">Nom Méd</th>
                                    <th scope="col">Prénom Méd</th>
                                    <th scope="col">Date</th>
                                    <th scope="col">Satut</th>
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
                            <td><%#Eval("medfirstname") %></td>
                            <td><%#Eval("medlastname") %></td>
                            <td><%#Eval("date_ordonnance") %></td>
                            <td><%#Eval("status") %></td>
                            <td><a href ="show_ordonnance.aspx?id=<%#Eval("Id")%>" style="color:gray">Voir</a></td>
                         </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
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
