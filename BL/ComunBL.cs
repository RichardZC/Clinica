using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Transactions;

namespace BL
{
    public class ComunBL
    {
        public static int GetPersonaIdSesion()
        {
            return UsuarioBL.Obtener(Comun.SessionHelper.GetUser()).PersonaId.Value;
        }
        public static persona GetPersonaSesion()
        {
            int id = Comun.SessionHelper.GetUser();
            return UsuarioBL.Obtener(x => x.UsuarioId == id, includeProperties: "persona")
                .persona;
        }
        public static int GetCajaDiarioId()
        {
            using (var bd = new clinicaEntities())
            {
                var personaid = bd.usuario.Find(Comun.SessionHelper.GetUser()).PersonaId;
                var cd = bd.cajadiario
                    .FirstOrDefault(x => x.PersonaId == personaid && x.IndAbierto && x.caja.IndBoveda == false && x.caja.IndAbierto);

                if (cd == null)
                    return 0;

                return cd.CajaDiarioId;
            }
        }
        public static cajadiario GetCajaDiario()
        {
            using (var bd = new clinicaEntities())
            {
                var personaid = bd.usuario.Find(Comun.SessionHelper.GetUser()).PersonaId;
                return bd.cajadiario.Include(t => t.caja).Include(x => x.persona).Include(x => x.cajamov).Include("cajamov.persona")
                    .FirstOrDefault(x => x.PersonaId == personaid && x.IndAbierto && x.caja.IndBoveda == false && x.caja.IndAbierto);
            }
        }
        public static int GetBovedaCajaDiarioId()
        {
            using (var bd = new clinicaEntities())
            {
                return bd.cajadiario
                    .First(x => x.IndAbierto && x.caja.IndBoveda && x.caja.IndAbierto)
                    .CajaDiarioId;
            }
        }
        public static cajadiario GetBoveda()
        {
            var bovedaDiario = CajadiarioBL.Obtener(x => x.IndAbierto && x.caja.IndBoveda && x.caja.IndAbierto);
            if (bovedaDiario != null) {
                return bovedaDiario;
            }
            else
            {
                using (var scope = new TransactionScope())
                {
                    try
                    {
                        var cbvda = new caja { Denominacion = "BOVEDA", IndBoveda = true, Estado = true, IndAbierto = true };
                        CajaBL.Crear(cbvda);

                        bovedaDiario = new cajadiario()
                        {
                            CajaId = cbvda.CajaId,
                            PersonaId = null,
                            SaldoInicial = 0,
                            FechaInicio = DateTime.Now,
                            IndAbierto = true
                        };
                        CajadiarioBL.Crear(bovedaDiario);

                        var caja = new caja { Denominacion = "CAJA 1", IndBoveda = false, Estado = true, IndAbierto = false };
                        CajaBL.Crear(caja);
                        
                        scope.Complete();

                    }
                    catch (Exception ex)
                    {
                        scope.Dispose();
                        throw new Exception(ex.Message);
                    }
                }
                return CajadiarioBL.Obtener(x => x.IndAbierto && x.caja.IndBoveda && x.caja.IndAbierto, "caja");
            }

        }

        public static medico GetMedicoObj()
        {
            var personaid = GetPersonaIdSesion();
            using (var bd = new clinicaEntities())
            {
                var med = bd.medico.Include(t => t.persona).FirstOrDefault(x => x.PersonaId == personaid);
                return med;
            }
        }
        
        //public static List<ItemCombo> BuscarPersonaAutoComplete(string pClave)
        //{
        //    using (var db = new clinicaEntities())
        //    {
        //        var qry = from p in db.persona
        //                  where (p.NombreCompleto.Contains(pClave) || p.DNI.Contains(pClave))
        //                  orderby p.NombreCompleto
        //                  select new ItemCombo { id = p.PersonaId, value = p.DNI + " " + p.NombreCompleto };
        //        return qry.ToList();
        //    }
        //}

    }
}
