<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Practica5._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
    <div class="col-md-10">
    <asp:Login ID="Login1" runat="server" FailureText="Usuario o contraseña incorrecta. Pruebe de nuevo" LoginButtonText="Iniciar sesion" PasswordLabelText="Contraseña:" PasswordRequiredErrorMessage="Debe introducir una contraseña" RememberMeText="Recuerdame" RenderOuterTable="False" TitleText="Iniciar sesion" UserNameLabelText="Correo electronico" UserNameRequiredErrorMessage="Debe escribir su correo electronico" OnAuthenticate="Entrar">
        <LayoutTemplate>
             <br />
            <fieldset>
                <legend>Iniciar sesion</legend>
                <p class="validation-sumary-errors">
                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                </p>                
                <div class="form-group">
                       <asp:Label CssClass="col-md-2 control-label" ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Correo electronico</asp:Label>
                       <div class="col-md-10">
                            <asp:TextBox CssClass="form-control" ID="UserName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="text-danger" ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="Debe escribir su correo electronico" ToolTip="Debe escribir su correo electronico" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                       </div>
                 </div>    
                 <div class="form-group">
                        <asp:Label CssClass="col-md-2 control-label" ID="PasswordLabel" runat="server" AssociatedControlID="Password">Contraseña:</asp:Label>
                     <div class="col-md-10">
                        <asp:TextBox CssClass="form-control" ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="text-danger" ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Debe introducir una contraseña" ToolTip="Debe introducir una contraseña" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                     </div>
                 </div>
                 <div class="form-group">
                     <div class="col-md-offset-2 col-md-10">
                         <div class="checkbox">
                            <asp:CheckBox ID="RememberMe" runat="server" /> Recuerdeme.
                         </div>
                     </div>
                 </div>
                <div class="form-group">
                     <asp:Button CssClass="btn btn-primary" ID="LoginButton" runat="server" CommandName="Login" Text="Iniciar sesion" ValidationGroup="Login1" />
                </div>
           </fieldset>
        </LayoutTemplate>
    </asp:Login>
  </div>
      <div class="col-md-2">
          
      </div>
</div>   

</asp:Content>
