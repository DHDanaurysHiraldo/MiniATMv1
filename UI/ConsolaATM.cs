

namespace MiniATM.UI;

internal static class ConsolaATM
{
    public static void MostrarMenu(string nombreCliente, string numeroCuenta)
    {
        MostrarEncabezado(nombreCliente, numeroCuenta);
        Console.WriteLine("¿Que desea realizar hoy?");
        Console.WriteLine("1. Consultar Saldo");
        Console.WriteLine("2. Depositar saldo");
        Console.WriteLine("3. Retirar saldo");
        Console.WriteLine("4. Consultar movimientos");
        Console.WriteLine("5. Salir");
        Console.WriteLine();
        Console.Write("Seleccione una opción: ");
    }

    public static void MostrarEncabezado(string nombreCliente, string numeroCuenta)
    {
        Console.Clear();
        Console.WriteLine("=================================");
        Console.WriteLine("===== SISTEMA DE CAJERO ATM =====");
        Console.WriteLine("=========================== v.2 =");

        Console.WriteLine();

        Console.WriteLine("INFORMACIÓN DE CUENTA ACTUAL:");
        Console.WriteLine($"Cliente: {nombreCliente}");
        Console.WriteLine($"No. de Cuenta: {numeroCuenta}");
        Console.WriteLine();
    }

    public static void ConsultarSaldo(decimal saldoDisponible)
    {
        Console.WriteLine($"Saldo Actual: RD$ {saldoDisponible}");

    }

    public static void VolverMenu()
    {
        Console.WriteLine("Presione una tecla para volver...");
        Console.ReadKey();
        Console.Clear();
    }
    public static decimal LeerMonto(string mensaje)
    {
        decimal montoOperacion;
        bool esNumerico;

        do
        {
            Console.Write(mensaje);
            esNumerico = decimal.TryParse(Console.ReadLine(), out montoOperacion);

            if (!esNumerico)
            {
                Console.WriteLine("Operación Rechazada: monto inválido, intente nuevamente.");
            }
        } while (!esNumerico);

        return montoOperacion;
    }
    public static bool LeerOpcion(out int opcionDeMenu)
    {
        return int.TryParse(Console.ReadLine(), out opcionDeMenu);
    }
}
