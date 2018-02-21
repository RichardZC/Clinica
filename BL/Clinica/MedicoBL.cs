using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BL
{
    public class MedicoBL : Repositorio<medico>
    {
<<<<<<< HEAD
        public static bool guardarMedico(medico med, persona per)
        {

            using (var scope = new TransactionScope())
            {
                try
                {

                    if (per.PersonaId > 0)
                    {
                        PersonaBL.Guardar(per);
                        MedicoBL.Guardar(med);
                    }
                    else
                    {
                        PersonaBL.Guardar(per);
                        med.PersonaId = PersonaBL.mostrarIdMedico(per.DNI).PersonaId;
                        MedicoBL.Guardar(med);

                    }
                    scope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception(ex.Message);
                }
            }
        }

        public static List<persona> listarMedico()
        {
            List<persona> listaMedico = new List<persona>();
            using (var db = new clinicaEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ValidateOnSaveEnabled = false;

                listaMedico = (from a in db.persona
                             join b in db.medico on a.PersonaId equals b.PersonaId
                             select a ).ToList();
            }
            return listaMedico;
        }
=======
        
        
>>>>>>> 45dfb445e923491f43e086cbdaad52dc38153f6c
    }
}
