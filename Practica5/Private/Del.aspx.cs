using Practica5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Practica5.Private
{
    public partial class Del : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Empresa> lst = (List<Empresa>)Session["empresas"];
            if (this.IsPostBack)
            {
                this.Validate();
                if (this.IsValid)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        if (lst[i].NIF == this.txtDni.Text.ToUpper())
                        {
                            lst.RemoveAt(i);
                            List<string> acc = (List<string>)Session["acciones"];
                            acc.Add("Empresa eliminada");
                            break;
                        } 
                    }

                    Response.Redirect("Default.aspx");
                }
            }
            MostrarAcciones();
        }

        public void MostrarAcciones()
        {
            //mostramos las acciones realizadas
            List<string> acci = (List<string>)Session["acciones"];
            if (acci.Count == 0)
            {
                this.acciones.InnerText = "No ha realizado acciones";
            }
            else
            {
                string res = "<ul>";
                for (int i = 0; i < acci.Count; i++)
                {
                    res += "<li>" + acci[i].ToString() + "</li>";
                }
                res += "</ul>";
                this.acciones.InnerHtml = res;
            }
        }
        public void Nif_Valido(object source, ServerValidateEventArgs args)
        {
            string letras = "TRWAGMYFPDXBNJZSQVHLCKET";
            string nif = args.Value;
            try
            {
                int numero = Int32.Parse(nif.Substring(0, nif.Length - 1));
                string letra = nif.Substring(nif.Length - 1, 1);
                int pos = numero % 23;
                if (letras[pos].ToString() == letra.ToUpper()) args.IsValid = true;
                else args.IsValid = false;

            }
            catch (Exception ex)
            {
                args.IsValid = false;
            }
            //args.IsValid = true;
        }
    }
}