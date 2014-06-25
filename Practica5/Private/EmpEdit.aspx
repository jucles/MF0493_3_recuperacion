<%@ Page Title="" Language="C#" MasterPageFile="~/Private/Maestra.Master" AutoEventWireup="true" CodeBehind="EmpEdit.aspx.cs" Inherits="Practica5.Private.EmpEdit" %>
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

       <fieldset>
        <legend>Buscar Empresa</legend>
        <div class="form-group">                          
            <asp:label CssClass="form-label" for="txtDni" runat="server" Font-Bold="True">NIF: </asp:label>
            <asp:TextBox CssClass="form-control" ID="txtDni" ToolTip="NIF de la empresa" runat="server" CausesValidation="True" ></asp:TextBox>    
            <asp:RequiredFieldValidator ID="valNIF" runat="server" ErrorMessage="Debe de escribir un NIF" ControlToValidate="txtDni" CssClass="label label-danger" SetFocusOnError="True"  Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="valNifCustom" runat="server" ErrorMessage="El NIF escrito no es valido" ControlToValidate="txtDni" CssClass="label label-danger" OnServerValidate="Nif_Valido" SetFocusOnError="True"  Display="Dynamic"></asp:CustomValidator>
        </div>

        <div class="form-group">
            <asp:Button CssClass="btn btn-primary" Text="Buscar" ID="btnBuscar" runat="server" />
           
        </div>
       </fieldset>

</asp:Content>