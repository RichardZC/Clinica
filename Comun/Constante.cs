using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun
{
    public class Constante
    {
       public enum PERSONA
        {
            CLIENTE = 0,
            MEDICO = 1,
            PROVEEDOR = 2
        }
        public static class SEGURIDAD {
            public static string CLAVE_DEFAULT = "202cb962ac59075b964b07152d234b70";//123
            public static int ROL_MEDICO = 3;
            public static int ROL_CAJERO = 4;

        }
        public static class OPERACION
        {
            public static string INI = "INI";
            public static string TRANSFERENCIA = "TRA";
        }
        public static class CAJADIARIO
        {
            public static string Pendiente = "P";
            public static string Cobrado = "C";
            public static string Terminado = "T";
            public static string Anulado = "X";
        }
    }
}
