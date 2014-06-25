<%@ Page Title="" Language="C#" MasterPageFile="~/Private/Maestra.Master" AutoEventWireup="true" CodeBehind="Listar.aspx.cs" Inherits="Practica5.Private.Listar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style type="text/css">
        .paginar td table td{
            width: 50px;
            padding: 5px 10px;
            border: 1px solid #ccc;
            margin: 0 10px;
            text-align:center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="zona2" runat="server">
    <div class="panel panel-primary">
        <div class="panel-heading">Menu de opciones</div>
        <div class="panel-body">
            <a class="btn btn-default btn-block" id="lnkNuevo" href="Add.aspx">Nuevo</a>
            <a class="btn btn-default btn-block" id="lnkEdit" href="EmpEdit.aspx">Editar</a>
            <a class="btn btn-default btn-block" id="lnkBorrar" href="Del.aspx">Borrar</a>
            <a class="btn btn-default btn-block" id="lnkMostrar" href="Listar.aspx">Mostrar</a>
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
        
        <div class="panel panel-default">
            <div class="panel-heading">Listado de empresas</div>
            <asp:GridView CssClass="table table-responsive" ID="tabla" runat="server" AllowPaging="True" BorderStyle="None"
                EnableSortingAndPagingCallbacks="True" GridLines="None" PageSize="4" ShowHeaderWhenEmpty="True" Width="100%" AutoGenerateColumns="True" AutoGenerateEditButton="True" OnRowEditing="tabla_RowEditing" >
                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" />
                <PagerStyle CssClass="paginar" />
            </asp:GridView>
        </div>
    
</asp:Content>
