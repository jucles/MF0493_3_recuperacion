<%@ Page Title="" Language="C#" MasterPageFile="~/Private/Maestra.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Practica5.Private.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="zona2" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">Menu de opciones</div>
        <div class="panel-body">
            <a class ="btn btn-default btn-block" id="lnkNuevo" href="Add.aspx">Nuevo</a>
            <a class ="btn btn-default btn-block" id="lnkEdit" href="EmpEdit.aspx">Editar</a>
            <a class ="btn btn-default btn-block" id="lnkBorrar" href="Del.aspx">Borrar</a>
            <a class ="btn btn-default btn-block" id="lnkMostrar" href="Listar.aspx">Mostrar</a>
        </div>
    </div>
        <!--acciones que realiza el usuario -->
    <div class=" panel panel-primary">
        <div class="panel-heading">
            Acciones realizadas
        </div>
        <div id="acciones" runat="server" style="text-align: center"></div>
    </div>
    <!-- mostramos mensaje de informacion del error -->
    <div id="error" runat="server" style="color: red"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="zona1" runat="server">
    <h3>Menu principal</h3>
    <h5>Numero de empresas registradas</h5>
    <asp:Label ID="lblEmpresas" runat="server" Text="0"></asp:Label>
</asp:Content>
