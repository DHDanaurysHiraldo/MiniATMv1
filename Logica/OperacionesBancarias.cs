
namespace MiniATM.Logica
{
    internal class OperacionesBancarias
    {
        public static bool CuentaEstaActiva(bool estadoCuenta)
        {
            return estadoCuenta;
        }

        public static bool EsMontoValido(decimal montoOperacion)
        {
            return montoOperacion > 0;
        }

        public static bool TieneFondos(decimal saldoDisponible, decimal montoOperacion)
        {
            return saldoDisponible >= montoOperacion;
        }

        public static decimal Depositar(decimal saldoDisponible, decimal montoOperacion)
        {
            if (!EsMontoValido(montoOperacion))
            {
                return saldoDisponible;
            }
            return saldoDisponible + montoOperacion;
        }

        public static decimal Retirar(decimal saldoDisponible, decimal montoOperacion)
        {
            if (!EsMontoValido(montoOperacion))
            {
                return saldoDisponible;
            }

            if (!TieneFondos(saldoDisponible, montoOperacion))
            {
                return saldoDisponible;
            }
            return saldoDisponible - montoOperacion;
        }

    }
}
