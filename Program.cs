string nombreCliente = "Samuel Reyes";
string numeroCuenta = "0458291046";
decimal saldoDisponible = 10000.00m;
decimal montoOperacion;
int opcionDeMenu;
bool estadoCuenta = true; //false

do
{
    MostrarMenu(nombreCliente, numeroCuenta);

    if (!LeerOpcion(out opcionDeMenu))
    {
        Console.WriteLine("Operación Rechazada: Opción inválida");
        VolverMenu();
    }
    else if (opcionDeMenu > 4)
    {
        Console.WriteLine("Operación Rechazada: Opción inválida");
        VolverMenu();

    }
    else
    {
        switch (opcionDeMenu)
        {
            case 1:
                ConsultarSaldo(saldoDisponible);
                VolverMenu();
                break;

            case 2:
                if (!CuentaEstaActiva(estadoCuenta))
                {
                    Console.WriteLine("Operación Rechazada: su cuenta no está activa");
                    VolverMenu();

                }
                else
                {
                    MostrarEncabezado(nombreCliente, numeroCuenta);

                    if (!LeerMonto("Ingrese el monto a depositar: ", out montoOperacion))
                    {
                        Console.WriteLine("Operación Rechazada: monto inválido");
                        VolverMenu();

                    }
                    else if (!EsMontoValido(montoOperacion))
                    {
                        Console.WriteLine("Operación Rechazada: el monto debe ser mayor que cero");
                        VolverMenu();

                    }
                    else
                    {
                        saldoDisponible = Depositar(saldoDisponible, montoOperacion);

                        MostrarEncabezado(nombreCliente, numeroCuenta);

                        Console.WriteLine("DEPÓSITO EXITOSO");
                        Console.WriteLine($"Saldo Actual: RD$ {saldoDisponible}");

                        VolverMenu();
                    }
                }
                break;


            case 3:
                if (!CuentaEstaActiva(estadoCuenta))
                {
                    Console.WriteLine("Operación Rechazada: su cuenta no está activa");
                    VolverMenu();

                }
                else
                {

                    MostrarEncabezado(nombreCliente, numeroCuenta);

                    Console.WriteLine($"SALDO ACTUAL: {saldoDisponible}");
                    if (!LeerMonto("Ingrese el monto a retirar: ", out montoOperacion))
                    {
                        Console.WriteLine("Operación Rechazada: monto inválido");
                        VolverMenu();

                    }
                    else if (!EsMontoValido(montoOperacion))
                    {
                        Console.WriteLine("Operación Rechazada: el monto debe ser mayor que cero");
                        VolverMenu();

                    }
                    else if (!TieneFondos(saldoDisponible, montoOperacion))
                    {
                        Console.WriteLine("Operación Rechazada: Fondos Insuficientes");
                        VolverMenu();

                    }
                    else
                    {
                        saldoDisponible = Retirar(saldoDisponible, montoOperacion);

                        MostrarEncabezado(nombreCliente, numeroCuenta);

                        Console.WriteLine("RETIRO EXITOSO");
                        Console.WriteLine($"Saldo Actual: RD$ {saldoDisponible}");

                        VolverMenu();

                    }
                }
                break;

            case 4:
                Console.WriteLine("Gracias por preferir nuestro ATM!!");
                Console.WriteLine("Saliendo del programa...");
                break;


            default:
                Console.WriteLine("Operación Rechazada: Opción inválida");
                VolverMenu();

                break;

        }
    }
}
while (opcionDeMenu != 4);

/*===========================
 * ======== MÉTODOS =========
 * =========================*/

static void MostrarMenu(string nombreCliente, string numeroCuenta)
{
    MostrarEncabezado(nombreCliente, numeroCuenta);
    Console.WriteLine("¿Que desea realizar hoy?");
    Console.WriteLine("1. Consultar Saldo");
    Console.WriteLine("2. Depositar saldo");
    Console.WriteLine("3. Retirar saldo");
    Console.WriteLine("4. Salir");
}

static void MostrarEncabezado(string nombreCliente, string numeroCuenta)
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
}

static void ConsultarSaldo(decimal saldoDisponible)
{
    Console.WriteLine($"Saldo Actual: RD$ {saldoDisponible}");
    
}

static void VolverMenu()
{
    Console.WriteLine("Presione una tecla para volver...");
    Console.ReadKey();
    Console.Clear();
}

static bool CuentaEstaActiva(bool estadoCuenta)
{
    return estadoCuenta;
}

static bool LeerMonto(string mensaje, out decimal montoOperacion)
{
    Console.Write(mensaje);
    return decimal.TryParse(Console.ReadLine(), out montoOperacion);
}
static bool LeerOpcion(out int opcionDeMenu)
{
    return int.TryParse(Console.ReadLine(), out opcionDeMenu);
}

static bool EsMontoValido(decimal montoOperacion)
{
    return montoOperacion > 0;
}

static bool TieneFondos(decimal saldoDisponible, decimal montoOperacion)
{
    return saldoDisponible >= montoOperacion;
}

static decimal Depositar(decimal saldoDisponible, decimal montoOperacion)
{
    if (!EsMontoValido(montoOperacion))
    {
        return saldoDisponible;         
    }
    return saldoDisponible + montoOperacion;    
}

static decimal Retirar(decimal saldoDisponible, decimal montoOperacion)
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

