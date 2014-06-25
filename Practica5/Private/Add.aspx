<%@ Page Title="" Language="C#" MasterPageFile="~/Private/Maestra.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="Practica5.Private.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/bootstrap-datepicker3.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/bootstrap-datepicker.js"></script>


    <script type="text/javascript">

    </script>
    <style type="text/css">
        #boton {
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
    <br />

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
        <legend>Nueva Empresa</legend>
        <div class="row">
            <div class="col-md-3">
                <asp:Image ID="foto" runat="server" Height="150px" Width="150px" ImageUrl="~/Private/img/bank.png" />
                <br />
                <br />
                <div id="listado_archivos">
                    <asp:Table CssClass="table table-striped table-bordered table-hover" ID="tabla" runat="server"></asp:Table>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <asp:Label CssClass="form-label" for="txtDni" runat="server" Font-Bold="True">NIF: </asp:Label>
                    <asp:TextBox CssClass="form-control" ID="txtDni" ToolTip="NIF de la empresa" runat="server" CausesValidation="True"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valNIF" runat="server" ErrorMessage="Debe de escribir un NIF" ControlToValidate="txtDni" CssClass="label label-danger" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="valNifCustom" runat="server" ErrorMessage="El NIF escrito no es valido" ControlToValidate="txtDni" CssClass="label label-danger" OnServerValidate="Nif_Valido" SetFocusOnError="True" Display="Dynamic"></asp:CustomValidator>
                </div>
                <div class="form-group">
                    <asp:Label CssClass="form-label" for="txtNombre" runat="server" Font-Bold="True">Nombre: </asp:Label>
                    <asp:TextBox CssClass="form-control" ID="txtNombre" ToolTip="Nombre del alumno" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ValNombre" runat="server" ErrorMessage="Debe de escribir un nombre" ControlToValidate="txtNombre" CssClass="label label-danger" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:Label CssClass="form-label" for="txtEmail" runat="server" Font-Bold="True">Email: </asp:Label>
                    <asp:TextBox CssClass="form-control" ID="txtEmail" ToolTip="Email de la empresa" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valEmail" runat="server" ErrorMessage="Debe escribir un email" ControlToValidate="txtEmail" CssClass="label label-danger" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="valEmailCustom" runat="server" ErrorMessage="El email no es correcto" ControlToValidate="txtEmail" CssClass="label label-danger" OnServerValidate="val_email" SetFocusOnError="True" Display="Dynamic"></asp:CustomValidator>
                </div>
                <div class="form-group">
                    <asp:Label CssClass="form-label" for="txtFnac" runat="server" Font-Bold="True">Fecha de fundación: </asp:Label>
                    <asp:TextBox CssClass="form-control" ID="txtFnac" ToolTip="Fecha de fundación" runat="server" data-provide="datepicker" data-date-format="dd/mm/yyyy"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label CssClass="form-label" for="txtDireccion" runat="server" Font-Bold="True">Direccion: </asp:Label>
                    <asp:TextBox CssClass="form-control" ID="txtDireccion" ToolTip="Dirección de la empresa" runat="server"></asp:TextBox>

                </div>
            </div>
            <div class="col-md-5">
                <div class="form-group">
                    <asp:Label CssClass="form-label" for="txtPoblacion" runat="server" Font-Bold="True">Poblacion: </asp:Label>
                    <asp:TextBox CssClass="form-control" ID="txtPoblacion" ToolTip="Poblacion de la empresa" runat="server"></asp:TextBox>

                </div>
                <div class="form-group">
                    <asp:Label CssClass="form-label" for="txtRepresentate" runat="server" Font-Bold="True">Representante: </asp:Label>
                    <asp:TextBox CssClass="form-control" ID="txtRepresentate" ToolTip="Representante de la empresa" runat="server"></asp:TextBox>

                </div>
                <div class="form-group">
                    <asp:Label CssClass="form-label" for="txtPyme" runat="server" Font-Bold="True">Pyme: </asp:Label>
                    <asp:CheckBox ID="txtPyme" ToolTip="Si es pyme o no" runat="server" />
                </div>
                <div class="form-group">
                    <asp:Label CssClass="form-label" for="txtTlf" runat="server" Font-Bold="True">Telefono: </asp:Label>
                    <asp:TextBox CssClass="form-control" ID="txtTlf" ToolTip="Telefono de la empresa" runat="server"></asp:TextBox>
                    <asp:CustomValidator ID="valTelefono" runat="server" ErrorMessage="El telefono no es valido" ControlToValidate="txtTlf" CssClass="label label-danger" OnServerValidate="val_tel" SetFocusOnError="True" Display="Dynamic"></asp:CustomValidator>
                </div>
                <div class="form-group">
                    <asp:Label CssClass="control-label" ID="Label4" runat="server" Text="Foto" AssociatedControlID="file_u"></asp:Label>
                    <asp:FileUpload CssClass="fileupload-new" ID="file_u" runat="server" />
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="file_u" CssClass="text-danger" ErrorMessage="CustomValidator" OnServerValidate="ValidarArchivo" ValidationGroup="archivo"></asp:CustomValidator>
                </div>
                <div class="form-group">
                    <asp:Label CssClass="control-label" ID="Label5" runat="server" Text="Subir Archivos:" AssociatedControlID="fu_archivos"></asp:Label>
                    <asp:FileUpload CssClass="fileupload-new" ID="fu_archivos" runat="server" AllowMultiple="True" />

                </div>

            </div>
            </div>

        <div class="row">
            <div class="col-md-3">
            </div>
            <div class="col-md-4">
                <div class="form-group" id="boton">
                    <asp:Button CssClass="btn btn-primary" Text="Guardar" ID="btnGuardar" runat="server" />
                </div>
            </div>
            <div class="col-md-5">
            </div>
        </div>
        
    </fieldset>

</asp:Content>
