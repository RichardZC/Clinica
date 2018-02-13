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
        public static bool guardarMedico(medico med, persona per)
        {

            using (var scope = new TransactionScope())
            {
                try
                {

                    if (per.PersonaId > 0)
                    {
                        MedicoBL.Guardar(med);
                    }
                    else
                    {
                        PersonaBL.Guardar(per);
                        // se hace una consulta a la bd para consultar el id de la persona guardada
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

        
    }
}
