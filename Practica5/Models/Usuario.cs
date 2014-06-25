using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Practica5.Models
{
    public class Usuario
    {
        private string _email;
        private string _home;

        /// <summary>
        /// 
        /// </summary>
        public string DirectorioPersonal
        {
            get { return this._home; }
            set
            {
                this._home = value;
                //Renombramos el directorio
                if (!Directory.Exists(value))
                {
                    try
                    {
                        //Directory.Move(this._home, value);
                    }
                    catch { }
                }
            }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        /*private string _foto;

        public string Foto
        {
            get { return _foto; }
            set { _foto = value; }
        }*/
        public string EncryptPassword
        {
            set
            {
                this._passwd = value;
            }
        }
        private string _passwd;

        public string Passwd
        {
            set
            {
                this._passwd = this.getMD5(value + this._salt);
            }
        }
        private string _salt;

        private bool _locked;

        public bool Locked
        {
            get { return _locked; }
            set { _locked = value; }
        }
        private bool _activo;

        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }

        public Usuario(string email, string pass)
        {
            this._email = email;
            this._nombre = "Anonimo";
            this._home = email;
            this._locked = false;
            this._activo = true;
            //this._foto = "~/privada/fotos/blank.png";

            Random r = new Random((int)DateTime.Now.Ticks);
            this._salt = this.getMD5(r.Next(10000000, 99999999).ToString());
            this._passwd = this.getMD5(pass + this._salt);

        }
        public Usuario(string email, string pass, string salt)
            : this(email, pass)
        {
            this._salt = salt;
            this._passwd = pass;
        }

        private string getMD5(string datos)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding enco = new ASCIIEncoding();
            byte[] cadena = null;
            StringBuilder sb = new StringBuilder();

            cadena = md5.ComputeHash(enco.GetBytes(datos));
            for (int i = 0; i < cadena.Length; i++)
            {
                sb.AppendFormat("{0:x2}", cadena[i]);
            }
            return sb.ToString();
        }
        public bool validar(string pass)
        {
            string hash = this.getMD5(pass + this._salt);
            return this._passwd == hash;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this._email + ";");
            sb.Append(this._nombre + ";");
            sb.Append(this._salt + ";");
            sb.Append(this._passwd + ";");
            //sb.Append(this._foto + ";");
            sb.Append(this._locked + ";");
            sb.Append(this._activo + ";");
            sb.Append(this._home);

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Usuario)
            {
                Usuario u = (Usuario)obj;
                return this._email.Equals(u._email);
            }
            return false;
        }
    }
}