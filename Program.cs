using MiniATM.Logica;
using MiniATM.UI;
namespace MiniATM;


internal class Program
{
    static void Main(string[] args)
    {
        string nombreCliente = "Samuel Reyes";
        string numeroCuenta = "0458291046";
        decimal saldoDisponible = 5000.00m;
        decimal montoOperacion;
        int opcionDeMenu;
        bool estadoCuenta = true; //false

        do
        {
            ConsolaATM.MostrarMenu(nombreCliente, numeroCuenta);

            if (!ConsolaATM.LeerOpcion(out opcionDeMenu))
            {
                Console.WriteLine("Operación Rechazada: Opción inválida");
                ConsolaATM.VolverMenu();
            }
            else if (opcionDeMenu > 5)
            {
                Console.WriteLine("Operación Rechazada: Opción inválida");
                ConsolaATM.VolverMenu();

            }
            else
            {
                switch (opcionDeMenu)
                {
                    case 1:
                        ConsolaATM.ConsultarSaldo(saldoDisponible);
                        ConsolaATM.VolverMenu();
                        break;

                    case 2:
                        if (!OperacionesBancarias.CuentaEstaActiva(estadoCuenta))
                        {
                            Console.WriteLine("Operación Rechazada: su cuenta no está activa");
                            ConsolaATM.VolverMenu();

                        }
                        else
                        {
                            montoOperacion = ConsolaATM.LeerMonto("Ingrese el monto a depositar: ");

                            if (!OperacionesBancarias.EsMontoValido(montoOperacion))
                            {
                                Console.WriteLine("Operación Rechazada: el monto debe ser mayor que cero");
                                ConsolaATM.VolverMenu();

                            }
                            else
                            {
                                saldoDisponible = OperacionesBancarias.Depositar(saldoDisponible, montoOperacion);

                                ConsolaATM.MostrarEncabezado(nombreCliente, numeroCuenta);

                                Console.WriteLine("DEPÓSITO EXITOSO");
                                Console.WriteLine($"Saldo Actual: RD$ {saldoDisponible}");

                                ConsolaATM.VolverMenu();
                            }
                        }
                        break;


                    case 3:
                        if (!OperacionesBancarias.CuentaEstaActiva(estadoCuenta))
                        {
                            Console.WriteLine("Operación Rechazada: su cuenta no está activa");
                            ConsolaATM.VolverMenu();

                        }
                        else
                        {

                            ConsolaATM.MostrarEncabezado(nombreCliente, numeroCuenta);

                            ConsolaATM.ConsultarSaldo(saldoDisponible);

                            montoOperacion = ConsolaATM.LeerMonto("Ingrese el monto a retirar: ");

                            if (!OperacionesBancarias.EsMontoValido(montoOperacion))
                            {
                                Console.WriteLine("Operación Rechazada: el monto debe ser mayor que cero");
                                ConsolaATM.VolverMenu();

                            }
                            else if (!OperacionesBancarias.TieneFondos(saldoDisponible, montoOperacion))
                            {
                                Console.WriteLine("Operación Rechazada: Fondos Insuficientes");
                                ConsolaATM.VolverMenu();

                            }
                            else
                            {
                                saldoDisponible = OperacionesBancarias.Retirar(saldoDisponible, montoOperacion);

                                ConsolaATM.MostrarEncabezado(nombreCliente, numeroCuenta);

                                Console.WriteLine("RETIRO EXITOSO");
                                Console.WriteLine($"Saldo Actual: RD$ {saldoDisponible}");

                                ConsolaATM.VolverMenu();

                            }
                        }
                        break;

                    case 4:
                        Console.WriteLine("Próximamente...");
                        ConsolaATM.VolverMenu();
                        break;



                    case 5:
                        Console.WriteLine("Gracias por preferir nuestro ATM!!");
                        Console.WriteLine("Saliendo del programa...");
                        break;

                    default:
                        Console.WriteLine("Operación Rechazada: Opción inválida");
                        ConsolaATM.VolverMenu();

                        break;

                }
            }
        }

        while (opcionDeMenu != 5);
    }


}