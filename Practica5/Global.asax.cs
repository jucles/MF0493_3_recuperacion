using Practica5;
using Practica5.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Practica5
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Leemos archivo de usuarios.
            List<Usuario> lista = new List<Usuario>();
            string archivo_usuarios = "~/App_Data/usuarios.txt";
            string archivo_en_disco = Server.MapPath(archivo_usuarios);
            StreamReader sr = new StreamReader(archivo_en_disco);
            while (!sr.EndOfStream)
            {
                string linea = sr.ReadLine();
                string[] datos = linea.Split(';');
                if (datos.Length == 7)
                {
                    Usuario usr = new Usuario(datos[0], datos[3], datos[2]);
                    usr.Nombre = datos[1];
                    //usr.Foto = datos[4];
                    usr.Locked = datos[4] == "True";
                    usr.Activo = datos[5] == "True";
                    usr.DirectorioPersonal = datos[6];
                    lista.Add(usr);
                }
            }
            sr.Close();
            //CREAMOS 3 USUARIOS SI LA LISTA ESTA VACIA
            if (lista.Count == 0)
            {
                Usuario u = new Usuario("Admin", "admin");
                u.Nombre = "Admin";
                Directory.CreateDirectory(Server.MapPath("~/Private/"+ u.DirectorioPersonal));
                lista.Add(u);
                u = new Usuario("Supervisor", "super");
                u.Nombre = "Supervisor";
                Directory.CreateDirectory(Server.MapPath("~/Private/" + u.DirectorioPersonal));
                lista.Add(u);
                this.GuardarUsuarios(lista);
            }
            Application.Add("ListaUsuarios", lista);
        }
        void Application_End(object sender, EventArgs e)
        {

        }
        private void GuardarUsuarios(List<Usuario> lista)
        {
            //List<Usuario> lista = (List<Usuario>)Application["ListaUsuarios"];
            string archivo_usuarios = "~/App_Data/usuarios.txt";
            string archivo_en_disco = Server.MapPath(archivo_usuarios);
            StreamWriter wr = new StreamWriter(archivo_en_disco);
            foreach (Usuario us in lista)
            {
                wr.WriteLine(us.ToString());
            }
            wr.Flush();
            wr.Close();
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            List<Empresa> empresas = new List<Empresa>();

            string archivo_usuarios = "~/App_Data/empresas.csv";
            string archivo_en_disco = Server.MapPath(archivo_usuarios);
            if (System.IO.File.Exists(archivo_en_disco))
            {
                StreamReader sr = new StreamReader(archivo_en_disco);
                while (!sr.EndOfStream)
                {
                    string linea = sr.ReadLine();
                    string[] datos = linea.Split(',');
                    if (datos.Length == 11)
                    {
                        DateTime dt = Convert.ToDateTime(datos[3]);
                        Empresa emp = new Empresa(datos[9], datos[10], datos[1], datos[0], datos[2], dt, datos[4], datos[5], datos[6], Boolean.Parse(datos[7]), datos[8]);
                        empresas.Add(emp);
                    }
                }
                sr.Close();
            }
            else {
            };

            Session.Add("empresas", empresas);

            List<string> acciones = new List<string>();
            Session.Add("acciones", acciones);
        }
    }
}