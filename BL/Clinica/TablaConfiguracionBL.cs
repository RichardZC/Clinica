using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TablaExamenBL : Repositorio<tablaexamen>
    {
        public static int ObtenerNuevoId(int pTablaId)
        {
            using (var bd = new clinicaEntities())
            {
                if (bd.tablaexamen.Count(x => x.TablaId == pTablaId) == 0)
                    return 1;
                return bd.tablaexamen.Where(x => x.TablaId == pTablaId).Max(x => x.ItemId) + 1;
            }
        }
        public static int ObtenerNuevoTablaId()
        {
            using (var bd = new clinicaEntities())
            {
                if (bd.tablaexamen.Count(x => x.ItemId == 0)==0)
                    return 1;
                return bd.tablaexamen.Where(x => x.ItemId == 0).Max(x => x.TablaId) + 1;
            }
        }
    }
}
