using Practica5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Practica5
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button b = new Button();

        }

        protected void Entrar(object sender, AuthenticateEventArgs e)
        {
            //autentifica el usuario y el password con los del archivo de configuracion
            //valido = FormsAuthentication.Authenticate(this.Login1.UserName,this.Login1.Password);
            //autentifica el usuario y el password con los datos de la lista que tenemos en la variable de aplicacion
            List<Usuario> lista = (List<Usuario>)Application["ListaUsuarios"];
            //busco la posicion de mi usuario en la lista. Para ello creo uno con mi email (que es lo que usa para comparar) y el resto vacio
            int pos = lista.IndexOf(new Usuario(this.Login1.UserName, "pass"));

            if (pos != -1)
            {

                if (!(lista[pos].Activo) || lista[pos].Locked)
                {
                    e.Authenticated = false;
                    this.Login1.FailureText = "El usuario esta bloqueado, conctacte con el administrador";
                }
                else
                {
                    if (lista[pos].validar(this.Login1.Password))
                    {
                        //te envia a la pagina que quieres entrar
                        FormsAuthentication.RedirectFromLoginPage(this.Login1.UserName, false);
                        e.Authenticated = true;
                        Response.Redirect("Private/Default.aspx");
                    }
                    else
                    {
                        e.Authenticated = false;
                        this.Login1.FailureText = "Contraseña Incorrecta";
                    }

                }

            }
            else
            {
                e.Authenticated = false;
            }

        }
    }
}