using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ProgramacionBL:Repositorio<programacion>
    {
        public static List<Object> Horas()
        {
            List<Object> listaHoras = new List<object>();
            for (int i = 6; i <= 12; i++)
            {
                listaHoras.Add(new
                {
                    Id=i+":00:00",
                    Valor= i + ":00 A.M."

                });
            }
            for (int i = 13; i <= 24; i++)
            {
                listaHoras.Add(new
                {
                    Id = i + ":00:00",
                    Valor = i + ":00 P.M."

                });
            }
                return listaHoras;
        }
    }
}
