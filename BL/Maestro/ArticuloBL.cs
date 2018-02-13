using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ArticuloBL : Repositorio<articulo>
    {
        public static List<articulo> ListarArticulos(string clave = "")
        {
            var lista = clave.Split(char.Parse(" "));
            string s1 = string.Empty, s2 = string.Empty, s3 = string.Empty, s4 = string.Empty;
            switch (lista.Length)
            {
                case 1: s1 = lista[0]; break;
                case 2: s1 = lista[0]; s2 = lista[1]; break;
                case 3: s1 = lista[0]; s2 = lista[1]; s3 = lista[2]; break;
                case 4: s1 = lista[0]; s2 = lista[1]; s3 = lista[2]; s4 = lista[3]; break;
            }

            using (var db = new clinicaEntities())
            {
                if (string.IsNullOrEmpty(clave))
                    return db.articulo.OrderByDescending(x => x.ArticuloId).Take(10).ToList();
                //int dni = Int32.Parse(clave);
                //if (clave.Length == 8)
                //{
                //    return db.persona.Where(x => x.DNI.Contains(s1)).OrderByDescending(x => x.Paterno).Take(15).ToList();
                //}

                if (lista.Length == 2)
                {

                    return db.articulo.Where(x => x.Denominacion.Contains(s1) && x.Denominacion.Contains(s2)).OrderByDescending(x => x.ArticuloId).Take(15).ToList();
                }
                if (lista.Length == 3)
                    return db.articulo.Where(x => x.Denominacion.Contains(s1) && x.Denominacion.Contains(s2) && x.Denominacion.Contains(s3)).OrderByDescending(x => x.ArticuloId).Take(15).ToList();
                if (lista.Length == 4)
                    return db.articulo.Where(x => x.Denominacion.Contains(s1) && x.Denominacion.Contains(s2) && x.Denominacion.Contains(s3) && x.Denominacion.Contains(s4)).OrderByDescending(x => x.ArticuloId).Take(15).ToList();

                
                return db.articulo.Where(x => x.Denominacion.Contains(clave) || x.Codigo.Contains(clave))
                    .OrderByDescending(x => x.ArticuloId)
                    .Take(15).ToList();

            }
        }
    }
}
