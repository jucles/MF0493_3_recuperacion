using Practica5.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Practica5.Private
{
    public partial class Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Empresa> lst = (List<Empresa>)Session["empresas"];
            if (this.IsPostBack)
            {
                this.Validate();
                if (this.IsValid)
                {
                    DateTime f;
                    string[] fecha = this.txtFnac.Text.Split('/');
                    if (fecha.Length == 3)
                    {
                        int dia = Int32.Parse(fecha[0]);
                        int mes = Int32.Parse(fecha[1]);
                        int anio = Int32.Parse(fecha[2]);
                        f = new DateTime(anio, mes, dia);

                    }
                    else f = DateTime.Now;


                    if (!Directory.Exists(Server.MapPath("~/Private/" + this.txtNombre.Text))) {

                        try
                        {

                            Empresa empresa = new Empresa("", "", this.txtDni.Text, this.txtNombre.Text, this.txtEmail.Text, f, this.txtDireccion.Text, this.txtPoblacion.Text, this.txtRepresentate.Text, this.txtPyme.Checked, this.txtTlf.Text);
                            //add(empresa);

                            //si es solo un archivo
                            if (this.file_u.HasFile)
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Private/" + this.txtNombre.Text + "/Logo"));
                                //cogemos el nombre del archivo dada la ruta
                                string archivo = Path.GetFileName(this.file_u.FileName);
                                //guardamos en el servidor, en la ruta completa
                                this.file_u.SaveAs(Server.MapPath("~/Private/" + this.txtNombre.Text + "/Logo/" + archivo));
                                empresa.Foto = "~/Private/" + this.txtNombre.Text + "/Logo/" + archivo;
                                
                            }

                            //si tenemos varios archivos
                            if (this.fu_archivos.HasFiles)
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Private/" + this.txtNombre.Text + "/Archivos"));
                                for (int i = 0; i < this.fu_archivos.PostedFiles.Count; i++)
                                {
                                    string archivo = Path.GetFileName(this.fu_archivos.PostedFiles[i].FileName);
                                    this.fu_archivos.PostedFiles[i].SaveAs(Server.MapPath("~/Private/" + this.txtNombre.Text + "/Archivos/" + archivo));
                                }
                                empresa.Carpeta = "~/Private/" + this.txtNombre.Text + "/Archivos";
                            }
                            
                            guardarEmpresaArchivo(empresa);

                            List<string> acc = (List<string>)Session["acciones"];

                            //guardamos la accion en la variable de sesion
                            acc.Add("Empresa guardada");
                        }
                        catch (Exception ex)
                        {
                            this.error.InnerText = "Error al crear la empresa";
                            //Response.Write("<script>alert('Error al crear empresa: '"+ex+")</script>"); 
                        }


                    }


                    Response.Redirect("Default.aspx");
                }
            }
            else
            {

            }
            MostrarAcciones();
        }
        //funciones de validacion
        #region
        /// <summary>
        /// calcula el tamaño de la informacion en su unidad sin miles
        /// </summary>
        /// <param name="tama">tamaño en bytes a calcular</param>
        /// <returns>tamaño sin miles</returns>    

        protected void ValidarArchivo(object source, ServerValidateEventArgs args)
        {
            if (this.file_u.HasFile)
            {
                //no permitimos mas de 5MB de tamaño de archivo
                if (this.file_u.PostedFile.ContentLength >= (5000 * 1024))
                {
                    args.IsValid = false;
                    this.CustomValidator1.ErrorMessage = "El tamaño del archivo excede el maximo del tamaño";
                }
                else
                {
                    string ext = Path.GetExtension(this.file_u.PostedFile.FileName);
                    string[] validas = { ".jpg", ".png", ".gif" };
                    if (!validas.Contains(ext))
                    {
                        args.IsValid = false;
                        this.CustomValidator1.ErrorMessage = "El formato del archivo no es valido";
                    }
                    else
                    {
                        args.IsValid = true;
                    }
                }
            }
        }//fin validar
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
        public void add(Empresa e)
        {
            if (e != null)
            {
                List<Empresa> lst = (List<Empresa>)Session["empresas"];
                lst.Add(e);
            }
            else throw new Exception("Empresa null");

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
        public void val_fecha(object source, ServerValidateEventArgs args)
        {
            int edad;
            string[] fecha = args.Value.Split('/');
            int dia = Int32.Parse(fecha[0]);
            int mes = Int32.Parse(fecha[1]);
            int anio = Int32.Parse(fecha[2]);

            DateTime fnac = new DateTime(anio, mes, dia);
            TimeSpan dif = new TimeSpan(DateTime.Now.Ticks - fnac.Ticks);

            edad = (int)(dif.Days / 356);
            args.IsValid = edad >= 18;
        }
        public void val_tel(object source, ServerValidateEventArgs args)
        {
            string tel = args.Value;
            args.IsValid = (tel[0] == '6' || tel[0] == '9');
        }
        public void val_email(object source, ServerValidateEventArgs args)
        {
            Regex rgxEmail = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            args.IsValid  = rgxEmail.IsMatch(args.Value);
        }

        public void guardarEmpresaArchivo(Empresa e) {
            try
            {
                List<Empresa> lst;
                if (e != null)
                {
                    lst = (List<Empresa>)Session["empresas"];
                    lst.Add(e);

                }
                else throw new Exception("Empresa null");

                string archivo_usuarios = "~/App_Data/empresas.csv";
                string archivo_en_disco = Server.MapPath(archivo_usuarios);

                if (System.IO.File.Exists(archivo_en_disco))
                {
                    try
                    {
                        System.IO.File.Delete(archivo_en_disco);
                    }
                    catch (System.IO.IOException ex)
                    {
                        this.error.InnerText = "Error al borrar archivo de empresas";
                    }
                }

                //File.Create(archivo_usuarios);
                TextWriter tw = new StreamWriter(archivo_en_disco);
                for (int i = 0; i < lst.Count; i++)
                {
                    tw.WriteLine(lst[i].ToString());
                }
                tw.Close();
            }catch(Exception ex){}
        
        }
#endregion

    }
}