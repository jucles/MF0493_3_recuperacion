using Practica5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Practica5.Private
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Empresa> lst = (List<Empresa>)Session["empresas"];
            this.lblEmpresas.Text = lst.Count.ToString();
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

    }
}