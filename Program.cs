string nombreCliente = "Samuel Reyes";
string numeroCuenta = "0458291046";
decimal saldoDisponible = 10000.00m;
decimal montoOperacion;
int opcionDeMenu;
bool estadoCuenta = true; //false

do
{
    MostrarMenu(nombreCliente, numeroCuenta);

    if (!int.TryParse(Console.ReadLine(), out opcionDeMenu))
    {
        Console.WriteLine("Operación Rechazada: Opción inválida");
        Console.WriteLine("Presione una tecla para volver...");
        Console.ReadKey();
        Console.Clear();
    }
    else if (opcionDeMenu > 4)
    {
        Console.WriteLine("Operación Rechazada: Opción inválida");
        Console.WriteLine("Presione una tecla para volver...");
        Console.ReadKey();
        Console.Clear();
    }
    else
    {
        switch (opcionDeMenu)
        {
            case 1:
                Console.WriteLine($"Saldo Actual: RD$ {saldoDisponible}");
                // VOLVER AL MENU ===
                Console.WriteLine("Presione una tecla para volver...");
                Console.ReadKey();
                Console.Clear();
                break;
            // ==================
            case 2:
                if (estadoCuenta == false)
                {
                    Console.WriteLine("Operación Rechazada: su cuenta no está activa");
                    // VOLVER AL MENU ===
                    Console.WriteLine("Presione una tecla para volver...");
                    Console.ReadKey();
                    Console.Clear();
                    //break;
                    // ==================

                }
                else
                {
                    // INTERFAZ ===========
                    Console.Clear();
                    Console.WriteLine("=================================");
                    Console.WriteLine("===== SISTEMA DE CAJERO ATM =====");
                    Console.WriteLine("=========================== v.1 =");

                    Console.WriteLine();

                    Console.WriteLine("INFORMACIÓN DE CUENTA ACTUAL:");
                    Console.WriteLine($"Cliente: {nombreCliente}");
                    Console.WriteLine($"No. de Cuenta: {numeroCuenta}");

                    Console.WriteLine();
                    //=====================

                    Console.Write("Ingrese el monto a depositar: ");
                    if (!decimal.TryParse(Console.ReadLine(), out montoOperacion))
                    {
                        Console.WriteLine("Operación Rechazada: monto inválido");
                        Console.WriteLine("Presione una tecla para volver...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else if (montoOperacion <= 0)
                    {
                        Console.WriteLine("Operación Rechazada: el monto debe ser mayor que cero");
                        Console.WriteLine("Presione una tecla para volver...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        saldoDisponible = saldoDisponible + montoOperacion;
                        // INTERFAZ ===========
                        Console.Clear();
                        Console.WriteLine("=================================");
                        Console.WriteLine("===== SISTEMA DE CAJERO ATM =====");
                        Console.WriteLine("=========================== v.1 =");

                        Console.WriteLine();

                        Console.WriteLine("INFORMACIÓN DE CUENTA ACTUAL:");
                        Console.WriteLine($"Cliente: {nombreCliente}");
                        Console.WriteLine($"No. de Cuenta: {numeroCuenta}");

                        Console.WriteLine();
                        //=====================

                        Console.WriteLine("DEPÓSITO EXITOSO");
                        Console.WriteLine($"Saldo Actual: RD$ {saldoDisponible}");

                        // VOLVER AL MENU ===
                        Console.WriteLine("Presione una tecla para volver...");
                        Console.ReadKey();
                        Console.Clear();
                        //break;
                        // ==================
                    }
                }
                break;


            case 3:
                if (estadoCuenta == false)
                {
                    Console.WriteLine("Operación Rechazada: su cuenta no está activa");
                    //Console.WriteLine("Saliendo del programa...");

                    // VOLVER AL MENU ===
                    Console.WriteLine("Presione una tecla para volver...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                    // ==================
                }
                else
                {
                    // INTERFAZ ===========
                    Console.Clear();
                    Console.WriteLine("=================================");
                    Console.WriteLine("===== SISTEMA DE CAJERO ATM =====");
                    Console.WriteLine("=========================== v.1 =");

                    Console.WriteLine();

                    Console.WriteLine("INFORMACIÓN DE CUENTA ACTUAL:");
                    Console.WriteLine($"Cliente: {nombreCliente}");
                    Console.WriteLine($"No. de Cuenta: {numeroCuenta}");

                    Console.WriteLine();
                    //=====================

                    Console.WriteLine($"SALDO ACTUAL: {saldoDisponible}");
                    Console.Write("Ingrese el monto a retirar: ");
                    if (!decimal.TryParse(Console.ReadLine(), out montoOperacion))
                    {
                        Console.WriteLine("Operación Rechazada: monto inválido");
                        Console.WriteLine("Presione una tecla para volver...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else if (montoOperacion <= 0)
                    {
                        Console.WriteLine("Operación Rechazada: el monto debe ser mayor que cero");
                        Console.WriteLine("Presione una tecla para volver...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else if (montoOperacion > saldoDisponible)
                    {
                        Console.WriteLine("Operación Rechazada: Fondos Insuficientes");
                        Console.WriteLine("Presione una tecla para volver...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        saldoDisponible = saldoDisponible - montoOperacion;

                        // INTERFAZ ===========
                        Console.Clear();
                        Console.WriteLine("=================================");
                        Console.WriteLine("===== SISTEMA DE CAJERO ATM =====");
                        Console.WriteLine("=========================== v.1 =");

                        Console.WriteLine();

                        Console.WriteLine("INFORMACIÓN DE CUENTA ACTUAL:");
                        Console.WriteLine($"Cliente: {nombreCliente}");
                        Console.WriteLine($"No. de Cuenta: {numeroCuenta}");

                        Console.WriteLine();
                        //=====================

                        Console.WriteLine("RETIRO EXITOSO");
                        Console.WriteLine($"Saldo Actual: RD$ {saldoDisponible}");
                        // VOLVER AL MENU ===
                        Console.WriteLine("Presione una tecla para volver...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                        // ==================
                    }
                }
                break;

            case 4:
                Console.WriteLine("Gracias por preferir nuestro ATM!!");
                Console.WriteLine("Saliendo del programa...");
                break;


            default:
                Console.WriteLine("Operación Rechazada: Opción inválida");
                // VOLVER AL MENU ===
                Console.WriteLine("Presione una tecla para volver...");
                Console.ReadKey();
                Console.Clear();
                break;
                // ==================
        }
    }
}
while (opcionDeMenu != 4);

static void MostrarMenu(string nombreCliente, string numeroCuenta)
{
    Console.Clear();
    Console.WriteLine("=================================");
    Console.WriteLine("===== SISTEMA DE CAJERO ATM =====");
    Console.WriteLine("=========================== v.1 =");

    Console.WriteLine();

    Console.WriteLine("INFORMACIÓN DE CUENTA ACTUAL:");
    Console.WriteLine($"Cliente: {nombreCliente}");
    Console.WriteLine($"No. de Cuenta: {numeroCuenta}");
    Console.WriteLine();
    Console.WriteLine("¿Que desea realizar hoy?");
    Console.WriteLine("1. Consultar Saldo");
    Console.WriteLine("2. Depositar saldo");
    Console.WriteLine("3. Retirar saldo");
    Console.WriteLine("4. Salir");
}