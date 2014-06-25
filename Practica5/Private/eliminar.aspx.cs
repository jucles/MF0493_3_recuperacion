
using Practica5.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EjemploLoginArchivo.privada
{
    public partial class eliminar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.Headers == null || Request.Headers["X-Requested-With"] == "XMLHttpRequest") { 
            List<Empresa> lista = (List<Empresa>)Session["empresas"];
            //Usuario user = lista[lista.IndexOf(new Em(st_user, ","))];
            string pos = Request.QueryString["param2"];
            Empresa emp = lista[Int32.Parse(pos)];
            try
            {
                string archivo = Request.QueryString["param"];
                File.Delete(Server.MapPath("~/Private/" + emp.Nombre + "/Archivos/" + archivo));
                //Response.Redirect("perfil.aspx");
                HttpContext.Current.Response.ContentType = "application/json";
                string[] archivos = Directory.GetFiles(Server.MapPath("~/Private/") + emp.Nombre + "/Archivos");
                long total = 0; 

                for (int i = 0; i < archivos.Length; i++)
                {
                    FileInfo f = new FileInfo(archivos[i]);
                    total += f.Length;
                }
                string t = UnidadesHumanas(total);
                string[] res = { "OK", archivo, t };
                JavaScriptSerializer serial = new JavaScriptSerializer();
                HttpContext.Current.Response.Write(serial.Serialize(res));
                HttpContext.Current.Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                HttpContext.Current.Response.SuppressContent = true;
            }
            catch (Exception err)
            {
                HttpContext.Current.Response.ContentType = "application/json";
                string[] res = { "KO", err.Message };
                JavaScriptSerializer serial = new JavaScriptSerializer();
                HttpContext.Current.Response.Write(serial.Serialize(res));
                HttpContext.Current.Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                HttpContext.Current.Response.SuppressContent = true;
            }
            //}
            //Response.StatusCode = 401;
            //Response.Write("KO");
        }

        private string UnidadesHumanas(double tama)
        {
            double res = tama;
            int div = 0;
            string unidad = "Bytes";
            while (res >= 1000)
            {
                res = res / 1024;
                div++;
            }

            switch (div)
            {
                case 1: unidad = "KB"; break;
                case 2: unidad = "MB"; break;
                case 3: unidad = "GB"; break;
                case 4: unidad = "TB"; break;
                case 5: unidad = "PB"; break;
                case 6: unidad = "EB"; break;
                case 7: unidad = "ZB"; break;
                case 8: unidad = "YB"; break;
                default: unidad = "Bytes"; break;
            }

            return (((int)res) + 1).ToString() + " " + unidad;
        }
    }

}