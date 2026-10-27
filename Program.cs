string nombreCliente = "Samuel Reyes";
string numeroCuenta = "0458291046";
decimal saldoDisponible = 10000.00m;
decimal montoOperacion;
int opcionDeMenu;
bool estadoCuenta = false; //false

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

if (!int.TryParse(Console.ReadLine(), out opcionDeMenu))
{
    Console.WriteLine("Operación Rechazada: Opción inválida");
}
else if (opcionDeMenu > 4)
{
    Console.WriteLine("Operación Rechazada: Opción inválida");
}
else
{
    switch (opcionDeMenu)
    {
        case 1:
            Console.WriteLine($"Saldo Actual: RD$ {saldoDisponible}");
            break;
        case 2:
            if (estadoCuenta == false)
            {
                Console.WriteLine("Operación Rechazada: su cuenta no está activa");
                Console.WriteLine("Saliendo del programa...");
            }
            else
            {
                Console.Write("Ingrese el saldo a depositar: ");
                if (!decimal.TryParse(Console.ReadLine(), out montoOperacion))
                {
                    Console.WriteLine("Operación Rechazada: monto inválido");
                }
                else if (montoOperacion <= 0)
                {
                    Console.WriteLine("Operación Rechazada: el monto debe ser mayor que cero");
                }
                else
                {
                    saldoDisponible = saldoDisponible + montoOperacion;
                    Console.WriteLine("DEPÓSITO EXITOSO");
                    Console.WriteLine($"Saldo Actual: RD$ {saldoDisponible}");
                }
            }
            break;

            //AGREGAR LOS CASOS QUE FALTAN...
        case 3:
            if (estadoCuenta == false)
            {
                Console.WriteLine("Operación Rechazada: su cuenta no está activa");
                Console.WriteLine("Saliendo del programa...");
            }
            else
            {
                Console.WriteLine($"SALDO ACTUAL: {saldoDisponible}");
                Console.WriteLine("Ingrese el saldo a retirar: ");
                if (!decimal.TryParse(Console.ReadLine(), out montoOperacion))
                {
                    Console.WriteLine("Operación Rechazada: monto inválido");
                }
                else if (montoOperacion <= 0)
                {
                    Console.WriteLine("Operación Rechazada: el monto debe ser mayor que cero");
                }
                else if (montoOperacion > saldoDisponible)
                {
                    Console.WriteLine("Operación Rechazada: Fondos Insuficientes");
                }
                else
                {
                    saldoDisponible = saldoDisponible - montoOperacion;
                    Console.WriteLine("RETIRO EXITOSO");
                    Console.WriteLine($"Saldo Actual: RD$ {saldoDisponible}");
                }
            }
            break;

        case 4:
            Console.WriteLine("Saliendo del programa...");
            break;

            
        default:
            Console.WriteLine("Ingrese una opción válida");
            break;
    }
}
