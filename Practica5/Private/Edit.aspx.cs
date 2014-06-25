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
    public partial class Edit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            List<Empresa> lst = (List<Empresa>)Session["empresas"];
            int pos = -1;
            if (this.IsPostBack)
            {
                    this.Validate();
                    if (this.IsValid)
                    {
                        pos = Int32.Parse(Request.QueryString["pos"]);
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
                        try
                        {
                            lst[pos].Foto = this.foto.ImageUrl;
                            lst[pos].NIF = this.txtDni.Text;
                            lst[pos].Nombre = this.txtNombre.Text;
                            lst[pos].Email = this.txtEmail.Text;
                            lst[pos].Fnac = f;
                            lst[pos].Direccion = this.txtDireccion.Text;
                            lst[pos].Poblacion = this.txtPoblacion.Text;
                            lst[pos].Representante = this.txtRepresentate.Text;
                            lst[pos].Pyme = this.txtPyme.Checked;
                            lst[pos].Telefono = this.txtTlf.Text;
                         //   Empresa empresa = new Empresa(this.txtDni.Text, this.txtNombre.Text, this.txtEmail.Text, f, this.txtDireccion.Text, this.txtPoblacion.Text, this.txtRepresentate.Text, this.txtPyme.Checked, this.txtTlf.Text);

                            //si es solo un archivo
                            if (this.file_u.HasFile)
                            {
                                if (!Directory.Exists(Server.MapPath("~/Private/" + this.txtNombre.Text + "/Logo")))
                                    Directory.CreateDirectory(Server.MapPath("~/Private/" + this.txtNombre.Text + "/Logo"));
                                
                                //cogemos el nombre del archivo dada la ruta
                                string archivo = Path.GetFileName(this.file_u.FileName);
                                //guardamos en el servidor, en la ruta completa
                                this.file_u.SaveAs(Server.MapPath("~/Private/" + this.txtNombre.Text + "/Logo/" + archivo));
                                lst[pos].Foto = "~/Private/" + this.txtNombre.Text + "/Logo/" + archivo;

                            }

                            //si tenemos varios archivos
                            if (this.fu_archivos.HasFiles)
                            {
                                if (!Directory.Exists(Server.MapPath("~/Private/" + this.txtNombre.Text + "/Archivos")))
                                    Directory.CreateDirectory(Server.MapPath("~/Private/" + this.txtNombre.Text + "/Archivos"));
                                for (int i = 0; i < this.fu_archivos.PostedFiles.Count; i++)
                                {
                                    string archivo = Path.GetFileName(this.fu_archivos.PostedFiles[i].FileName);
                                    this.fu_archivos.PostedFiles[i].SaveAs(Server.MapPath("~/Private/" + this.txtNombre.Text + "/Archivos/" + archivo));
                                }
                                lst[pos].Carpeta = "~/Private/" + this.txtNombre.Text + "/Archivos";
                            }

                            guardarEmpresaArchivo(lst[pos]);
                            List<string> acc = (List<string>)Session["acciones"];
                            acc.Add("Empresa modificada");
                        }
                        catch (Exception ex) { Response.Write("<script>alert('Error al crear empresa: '" + ex + ")</script>"); }
                        Response.Redirect("Default.aspx");
                    }
                }else
                {
                    try
                    {
                        pos = Int32.Parse(Request.QueryString["pos"]);

                        if (pos != -1)
                        {
                            this.foto.ImageUrl = lst[pos].Foto;
                            this.txtDni.Text = lst[pos].NIF;
                            this.txtNombre.Text = lst[pos].Nombre;
                            this.txtEmail.Text = lst[pos].Email;
                            this.txtFnac.Text = lst[pos].Fnac.ToString("dd/MM/yyyy");
                            this.txtDireccion.Text = lst[pos].Direccion;
                            this.txtPoblacion.Text = lst[pos].Poblacion;
                            this.txtRepresentate.Text = lst[pos].Representante;
                            this.txtPyme.Checked = lst[pos].Pyme;
                            this.txtTlf.Text = lst[pos].Telefono;

                            string[] archivos = Directory.GetFiles(Server.MapPath("~/Private/") + lst[pos].Nombre + "/Archivos");
                            long total = 0; //nos da el tamaño de la carpeta, o la suma de todos sus archivos

                            for (int i = 0; i < archivos.Length; i++)
                            {
                                //para cada elemento encontrado (archivo)
                                TableRow fila = new TableRow(); //creamos una fila
                                TableCell nombre = new TableCell(); //creamos primera celda
                                nombre.Text = Path.GetFileName(archivos[i]);
                                fila.Cells.Add(nombre);

                                //ahora creariamos otra celda y la agregariamos a la fila
                                TableCell tamanio = new TableCell();
                                FileInfo f = new FileInfo(archivos[i]);
                                tamanio.Text = UnidadesHumanas(f.Length);
                                total += f.Length;
                                fila.Cells.Add(tamanio);
                                TableCell borrar = new TableCell();
                                LinkButton lnk = new LinkButton();
                                lnk.CssClass = "glyphicon glyphicon-remove";
                                lnk.ToolTip = "Borrar Archivo";
                                lnk.Attributes.Add("href", "#");  //hay que rellenar porque los linkbutton por defecto hacen postback
                                lnk.OnClientClick = "BorrarArchivo('" + pos + "','" + Path.GetFileName(archivos[i]) + "')";  //llama a una funcion javascript
                                borrar.Controls.Add(lnk);
                                fila.Cells.Add(borrar);

                                TableCell des = new TableCell();
                                LinkButton lnk2 = new LinkButton();
                                lnk2.CssClass = "glyphicon glyphicon-save";
                                lnk2.ToolTip = "Descargar Archivo";
                                lnk2.Attributes.Add("href", lst[pos].Nombre + "/Archivos/" + nombre.Text);  //hay que rellenar porque los linkbutton por defecto hacen postback
                                //lnk.OnClientClick = "BorrarArchivo('" + Path.GetFileName(archivos[i]) + "')";  //llama a una funcion javascript
                                des.Controls.Add(lnk2);
                                fila.Cells.Add(des);

                                //añadimos la fila a la tabla
                                this.tabla.Rows.Add(fila);
                            }//fin for

                            //añadimos al final de la tabla el tamaño total
                            TableRow filaU = new TableRow();
                            TableCell celdatotal = new TableCell();
                            celdatotal.ColumnSpan = 3;
                            celdatotal.Text = this.UnidadesHumanas(total);
                            celdatotal.HorizontalAlign = HorizontalAlign.Right;
                            filaU.Cells.Add(celdatotal);
                            this.tabla.Rows.Add(filaU);
                        }
                    }
                    catch { }
                }
            MostrarAcciones();
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

        public void guardarEmpresaArchivo(Empresa e)
        {
            List<Empresa> lst = (List<Empresa>)Session["empresas"];

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
            TextWriter tw = new StreamWriter(archivo_usuarios);
            for (int i = 0; i < lst.Count; i++)
            {
                tw.WriteLine(lst[i].ToString());
            }
            tw.Close();

        }
        public void MostrarAcciones() {
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
            args.IsValid = rgxEmail.IsMatch(args.Value);
        }
    }
}