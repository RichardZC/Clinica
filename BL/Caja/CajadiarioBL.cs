using BE;
using Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BL
{
    public class CajadiarioBL : Repositorio<cajadiario>
    {
        public static decimal ObtenerSaldoBoveda()
        {

            var cd = CajadiarioBL.Obtener(x => x.IndAbierto && x.caja.IndBoveda && x.caja.IndAbierto, includeProperties: "Caja");
            return cd == null ? 0 : cd.SaldoFinal;
        }

        public static int AsignarCajero(int pCajaId, int pPersonaId, decimal SaldoInicial)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    cajadiario cd = new cajadiario()
                    {
                        CajaId = pCajaId,
                        PersonaId = pPersonaId,
                        SaldoInicial = SaldoInicial,
                        FechaInicio = DateTime.Now,
                        IndAbierto = true
                    };
                    Guardar(cd);
                    CajaBL.ActualizarParcial(new caja
                    {
                        CajaId = pCajaId,
                        IndAbierto = true
                    }, x => x.IndAbierto);

                    if (SaldoInicial > 0)
                    {
                        var bovedaId = ComunBL.GetBovedaCajaDiarioId();
                        Transferir(bovedaId, cd.CajaDiarioId, cd.SaldoInicial, Constante.OPERACION.INI);
                    }

                    scope.Complete();
                    return cd.CajaDiarioId;
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception(ex.Message);
                }
            }
        }


        public static bool Transferir(int pCajaDiarioOrigenId, int pCajaDiarioDestinoId, decimal pImporte, string Operacion)
        {
            var pCajaDiarioOrigen = CajadiarioBL.Obtener(x => x.CajaDiarioId == pCajaDiarioOrigenId, "Caja");
            var pCajaDiarioDestino = CajadiarioBL.Obtener(x => x.CajaDiarioId == pCajaDiarioDestinoId, "Caja");
            using (var scope = new TransactionScope())
            {
                try
                {
                    if (pImporte > 0)
                    {
                        CajaMovBL.Crear(new cajamov
                        {
                            CajaDiarioId = pCajaDiarioOrigenId,
                            PersonaId = ComunBL.GetPersonaIdSesion(),
                            Monto = pImporte,
                            Operacion = Constante.OPERACION.TRANSFERENCIA,
                            Glosa = "TRANS. " + pCajaDiarioOrigen.caja.Denominacion + " A " + pCajaDiarioDestino.caja.Denominacion,
                            IndEntrada = false,
                            Estado = Constante.CAJADIARIO.Terminado,
                            UsuarioRegId = Comun.SessionHelper.GetUser(),
                            FechaReg = DateTime.Now,
                            CajaDiarioTransId = pCajaDiarioDestinoId
                        });
                        CajaMovBL.Crear(new cajamov
                        {
                            CajaDiarioId = pCajaDiarioDestinoId,
                            PersonaId = ComunBL.GetPersonaIdSesion(),
                            Monto = pImporte,
                            Operacion = Operacion,
                            Glosa = "TRANS. " + pCajaDiarioOrigen.caja.Denominacion + " A " + pCajaDiarioDestino.caja.Denominacion,
                            IndEntrada = true,
                            Estado = Constante.CAJADIARIO.Terminado,
                            UsuarioRegId = Comun.SessionHelper.GetUser(),
                            FechaReg = DateTime.Now,
                            CajaDiarioTransId = pCajaDiarioOrigenId
                        });

                        ActualizarSaldoCajaDiario(pCajaDiarioOrigenId);
                        ActualizarSaldoCajaDiario(pCajaDiarioDestinoId);
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



        public static bool ActualizarSaldoCajaDiario(int pCajaDiarioId)
        {
            decimal ingresoMov = 0, egresoMov = 0;

            using (var bd = new clinicaEntities())
            {
                ingresoMov = bd.cajamov
                    .Where(x => x.CajaDiarioId == pCajaDiarioId && x.IndEntrada && x.Estado == "T" && x.Operacion != "INI")
                    .Select(x => x.Monto).ToList().Sum();
                egresoMov = bd.cajamov
                    .Where(x => x.CajaDiarioId == pCajaDiarioId && x.IndEntrada == false && x.Estado == "T")
                    .Select(x => x.Monto).ToList().Sum();

                var cd = bd.cajadiario.Find(pCajaDiarioId);
                cd.Entradas = ingresoMov;
                cd.Salidas = egresoMov;
                cd.SaldoFinal = cd.SaldoInicial + cd.Entradas - cd.Salidas;
                bd.SaveChanges();
            }

            return true;
        }

        public static bool CrearSaldoInicial(int cajaDiarioId, decimal saldoInicial)
        {

            using (var scope = new TransactionScope())
            {
                try
                {
                    CajadiarioBL.ActualizarParcial(new cajadiario { CajaDiarioId = cajaDiarioId, SaldoInicial = saldoInicial }, x => x.SaldoInicial);
                    CajaMovBL.Crear(new cajamov
                    {
                        CajaDiarioId = cajaDiarioId,
                        PersonaId = ComunBL.GetPersonaIdSesion(),
                        Monto = saldoInicial,
                        Operacion = "INI",
                        Glosa = "TRANS. SALDO INICIAL",
                        IndEntrada = true,
                        Estado = "T",
                        UsuarioRegId = Comun.SessionHelper.GetUser(),
                        FechaReg = DateTime.Now
                    });
                    ActualizarSaldoCajaDiario(cajaDiarioId);

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


        public static cajadiario TranferenciaBovedaBanco(decimal monto, bool indEntrada)
        {
            var b = ComunBL.GetBoveda();
            using (var scope = new TransactionScope())
            {
                try
                {
                    CajaMovBL.Crear(new cajamov
                    {
                        CajaDiarioId = b.CajaDiarioId,
                        PersonaId = ComunBL.GetPersonaIdSesion(),
                        Monto = monto,
                        Operacion = Constante.OPERACION.TRANSFERENCIA,
                        Glosa = indEntrada ? "TRANS. A BOVEDA" : "TRANS. A BANCO",
                        IndEntrada = indEntrada,
                        Estado = Constante.CAJADIARIO.Terminado,
                        UsuarioRegId = Comun.SessionHelper.GetUser(),
                        FechaReg = DateTime.Now
                    });
                    ActualizarSaldoCajaDiario(b.CajaDiarioId);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception(ex.Message);
                }
            }
            return BL.ComunBL.GetBoveda();
        }

    }
}
