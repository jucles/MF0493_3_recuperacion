using Practica5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Practica5.Private
{
    public partial class Listar : System.Web.UI.Page
    {
        public List<Empresa> lista;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lista = (List<Empresa>)Session["empresas"];
            this.tabla.DataSource = this.lista;
            this.tabla.DataBind();
            List<string> acc = (List<string>)Session["acciones"];
            acc.Add("Listar empresas");
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

        protected void tabla_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("edit.aspx?pos="+e.NewEditIndex);
        }
    }
}