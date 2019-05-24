using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ExamenBL : Repositorio<examen>
    {
        public static bool EliminarExamen(int pAtencionId, int pTablaId)
        {
            using (var bd = new clinicaEntities())
            {
                var qry = bd.examen.Where(x => x.AtencionId == pAtencionId && x.TablaId == pTablaId);
                bd.examen.RemoveRange(qry);
                bd.SaveChanges();
            }
            return true;
        }
        public static List<VmExamenes> ListarExamenes(string pPaciente)
        {
            using (var bd = new clinicaEntities())
            {
                var qry = bd.examen.Where(x => x.ItemId == 0);
                if (!string.IsNullOrEmpty(pPaciente))
                    qry = qry.OrderByDescending(x => x.atencion.Fecha).ThenByDescending(x=>x.Valor)
                        .Where(x => x.atencion.persona.NombreCompleto.Contains(pPaciente));
                else
                    qry = qry.OrderByDescending(x => x.atencion.Fecha).ThenByDescending(x => x.Valor).Take(20);

                return qry.Select(x => new VmExamenes
                {
                    AtencionId = x.AtencionId,
                    TablaId = x.TablaId,
                    Examen = x.Denominacion,
                    Paciente = x.atencion.persona.NombreCompleto,
                    Pendiente = (x.Valor == "P"),
                    Fecha= x.atencion.Fecha
                }).ToList();
               
            }
        }
    }
}

public class VmExamenes
{
    public int AtencionId { get; set; }
    public int TablaId { get; set; }
    public string Examen { get; set; }
    public string Paciente { get; set; }
    public DateTime Fecha { get; set; }
    public bool Pendiente { get; set; }
}

   

