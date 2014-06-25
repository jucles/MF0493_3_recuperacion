using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practica5.Models
{
    public class Empresa
    {
        private string nif;
        private string nombre;
        private string email;
        private DateTime fecha;
        private string direccion;
        private string poblacion;
        private string representante;
        private bool pyme;
        private string telefono;
        private string foto;

        /// <summary>
        /// Propiedades GET Y SET por defecto.
        /// </summary>

        private string carpeta;

        //es la ruta de la carpeta, donde se guardaran todos sus archivos
        /// <summary>
        /// la ruta es la ruta completa en disco. 
        /// </summary>
        public string Carpeta
        {
            get
            {
                return this.carpeta;
            }
            set
            {
                this.carpeta = value;
            }
        }
        public string Foto
        {
            get { return foto; }
            set { foto = value; }
        }
        public string NIF
        {
            get { return this.nif; }
            set { this.nif = value; }
        }
        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }
        public DateTime Fnac
        {
            get { return this.fecha; }
            set { this.fecha = value; }
        }
        public string Direccion
        {
            get { return this.direccion; }
            set { this.direccion = value; }
        }
        public string Poblacion
        {
            get { return this.poblacion; }
            set { this.poblacion = value; }
        }
        public string Representante
        {
            get { return this.representante; }
            set { this.representante = value; }
        }
        public bool Pyme
        {
            get { return this.pyme; }
            set { this.pyme = value; }
        }
        public string Telefono
        {
            get { return this.telefono; }
            set { this.telefono = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Empresa()
        {
            NIF = "11111111T";
            Nombre = "Sin nombre";
            Email = "Sin email";
            Fnac = new DateTime();
            Direccion = "Sin direccion";
            Poblacion = "";
            Representante = "Sin representante";
            Pyme = true;
            Telefono = "";
            Foto = "";
            Carpeta = "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nif"></param>
        /// <param name="nombre"></param>
        /// <param name="email"></param>
        /// <param name="fnac"></param>
        /// <param name="direccion"></param>
        /// <param name="poblacion"></param>
        /// <param name="representante"></param>
        /// <param name="pyme"></param>
        /// <param name="telefono"></param>
        public Empresa(string foto, string carpeta, string nif, string nombre, string email, DateTime fnac, string direccion, string poblacion, string representante, bool pyme, string telefono)
        {
            NIF = nif.ToUpper();
            Nombre = nombre;
            Email = email;
            if (fnac != null)
            {
                Fnac = new DateTime(fnac.Ticks);
            }
            else
                Fnac = new DateTime();

            Direccion = direccion;
            Poblacion = poblacion;
            Representante = representante;
            Pyme = pyme;
            Telefono = telefono;
            Foto = foto;
            Carpeta = carpeta;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public Empresa(Empresa e)
        {
            NIF = e.NIF.ToUpper();
            Nombre = e.Nombre;
            Email = e.Email;
            if (e.Fnac != null)
            {
                Fnac = new DateTime(e.Fnac.Ticks);
            }
            else
                Fnac = new DateTime();

            Direccion = e.Direccion;
            Poblacion = e.Poblacion;
            Representante = e.Representante;
            Pyme = e.Pyme;
            Telefono = e.Telefono;
            Foto = e.foto;
            Carpeta = e.Carpeta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string empresa = "";
            empresa = this.Nombre +",";
            empresa += this.NIF.ToUpper() + ",";
            empresa += this.Email + ",";
            empresa += this.Fnac.ToString() + ",";
            empresa += this.Direccion + ",";
            empresa += this.Poblacion + ",";
            empresa += this.Representante + ",";
            empresa += this.Pyme + ",";
            empresa += this.Telefono + ",";
            empresa += this.Foto + ",";
            empresa += this.Carpeta;
            return empresa;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != this.GetType()) return false;
            if (obj is Empresa)
            {
                Empresa e = (Empresa)obj;
                return this.NIF == e.NIF && this.Nombre == e.Nombre;
            }
            return false;
        }

        //OPERADORES
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Empresa a, Empresa b)
        {
            if (a == null && b == null) return true;
            if (a == null && b != null) return false;
            if (a != null && b == null) return false;
            return a.Equals(b);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Empresa a, Empresa b)
        {
            return !a.Equals(b);
        }

        
    }
}