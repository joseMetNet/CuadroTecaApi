using fotoTeca.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Email
{
    public class ValorConfiguracion
    {
        #region MetodosPublicos

        private readonly string _conectionString;
        public int IdValor { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
        #endregion
        public ValorConfiguracion(string nombre, string conectionString) : base()
        {
            _conectionString = conectionString;
            Nombre = nombre;
            CargarInformacion();
        }
        private void CargarInformacion()
        {
            if (Nombre != null && Nombre.Trim().Length > 0)
            {
                Connection db = new Connection(_conectionString);
                try
                {
                    db.TiempoEsperaComando = 0;
                    db.SQLParametros.Clear();
                    db.SQLParametros.Add("@Nombre", SqlDbType.VarChar, 50).Value = Nombre;
                    var reader = db.EjecutarReader("ObtenerInfoValorConfiguracion", CommandType.StoredProcedure);
                    if (reader != null)
                    {
                        if (reader.Read())
                        {
                            if (!Convert.IsDBNull(reader["idValor"])) { IdValor = (int)reader["idValor"]; }
                            if (!Convert.IsDBNull(reader["Nombre"])) { Nombre = reader["Nombre"].ToString(); }
                            if (!Convert.IsDBNull(reader["Valor"])) { Valor = reader["Valor"].ToString(); }

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Se genero un error:" + ex.Message);
                }
                finally { }
                if (db != null)
                {
                    db.Dispose();
                }

            }
        }
        //#endregion

    }
}

