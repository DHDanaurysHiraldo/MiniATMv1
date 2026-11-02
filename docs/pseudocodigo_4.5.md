# Pseudocódigo — Mini ATM · Actividad 4.5

Algoritmo del cajero automático refactorizado en métodos. Los métodos que no devuelven un valor se representan como `SubProceso` y los que devuelven un valor como `Funcion`. Un parámetro `Por Referencia` equivale al parámetro `out` de C#.

> **Nota:** PSeInt no permite comprobar si una entrada es numérica sin producir un error, por lo que `LeerOpcion` y `LeerMonto` siempre devuelven `Verdadero` en el pseudocódigo. En C#, esa validación la realiza `TryParse`.

## Índice

1. [Algoritmo principal](#1-algoritmo-principal)
2. [Procedimientos de presentación](#2-procedimientos-de-presentación)
3. [Funciones de lectura](#3-funciones-de-lectura)
4. [Funciones de validación](#4-funciones-de-validación)
5. [Funciones de operación](#5-funciones-de-operación)
6. [Resumen de métodos](#6-resumen-de-métodos)

## 1. Algoritmo principal

Inicializa los datos de la cuenta y repite el menú hasta que el usuario elige la opción 4. Cada opción delega el trabajo en los métodos de las secciones siguientes.

```
Algoritmo MiniATM_4_5
    Definir nombreCliente, numeroCuenta Como Cadena
    Definir saldoDisponible, montoOperacion Como Real
    Definir opcionDeMenu Como Entero
    Definir estadoCuenta Como Logico

    nombreCliente <- 'Samuel Reyes'
    numeroCuenta <- '0458291046'
    saldoDisponible <- 10000.00
    estadoCuenta <- Verdadero

    Repetir
        MostrarMenu(nombreCliente, numeroCuenta)

        Si NO LeerOpcion(opcionDeMenu) Entonces
            Escribir 'Operación rechazada: opción inválida'
            VolverMenu
        SiNo
            Segun opcionDeMenu Hacer
                1:
                    ConsultarSaldo(saldoDisponible)
                    VolverMenu

                2:
                    Si NO CuentaEstaActiva(estadoCuenta) Entonces
                        Escribir 'Operación rechazada: su cuenta no está activa'
                        VolverMenu
                    SiNo
                        MostrarEncabezado(nombreCliente, numeroCuenta)
                        Si NO LeerMonto('Ingrese el monto a depositar: ', montoOperacion) Entonces
                            Escribir 'Operación rechazada: monto inválido'
                            VolverMenu
                        SiNo
                            Si NO EsMontoValido(montoOperacion) Entonces
                                Escribir 'Operación rechazada: el monto debe ser mayor que cero'
                                VolverMenu
                            SiNo
                                saldoDisponible <- Depositar(saldoDisponible, montoOperacion)
                                MostrarEncabezado(nombreCliente, numeroCuenta)
                                Escribir 'DEPÓSITO EXITOSO'
                                ConsultarSaldo(saldoDisponible)
                                VolverMenu
                            FinSi
                        FinSi
                    FinSi

                3:
                    Si NO CuentaEstaActiva(estadoCuenta) Entonces
                        Escribir 'Operación rechazada: su cuenta no está activa'
                        VolverMenu
                    SiNo
                        MostrarEncabezado(nombreCliente, numeroCuenta)
                        ConsultarSaldo(saldoDisponible)
                        Si NO LeerMonto('Ingrese el monto a retirar: ', montoOperacion) Entonces
                            Escribir 'Operación rechazada: monto inválido'
                            VolverMenu
                        SiNo
                            Si NO EsMontoValido(montoOperacion) Entonces
                                Escribir 'Operación rechazada: el monto debe ser mayor que cero'
                                VolverMenu
                            SiNo
                                Si NO TieneFondos(saldoDisponible, montoOperacion) Entonces
                                    Escribir 'Operación rechazada: fondos insuficientes'
                                    VolverMenu
                                SiNo
                                    saldoDisponible <- Retirar(saldoDisponible, montoOperacion)
                                    MostrarEncabezado(nombreCliente, numeroCuenta)
                                    Escribir 'RETIRO EXITOSO'
                                    ConsultarSaldo(saldoDisponible)
                                    VolverMenu
                                FinSi
                            FinSi
                        FinSi
                    FinSi

                4:
                    Escribir 'Gracias por preferir nuestro ATM!!'
                    Escribir 'Saliendo del programa...'

                De Otro Modo:
                    Escribir 'Operación rechazada: opción inválida'
                    VolverMenu
            FinSegun
        FinSi
    Hasta Que opcionDeMenu = 4
FinAlgoritmo
```

## 2. Procedimientos de presentación

Muestran información en pantalla. No reciben ni modifican el saldo, salvo `ConsultarSaldo`, que solo lo lee.

### MostrarEncabezado

Limpia la pantalla y muestra el título del cajero y los datos de la cuenta.

```
SubProceso MostrarEncabezado(nombreCliente, numeroCuenta)
    Borrar Pantalla
    Escribir '================================='
    Escribir '===== SISTEMA DE CAJERO ATM ====='
    Escribir '=========================== v.1 ='
    Escribir ''
    Escribir 'INFORMACIÓN DE CUENTA ACTUAL:'
    Escribir 'Cliente: ', nombreCliente
    Escribir 'No. de Cuenta: ', numeroCuenta
    Escribir ''
FinSubProceso
```

### MostrarMenu

Reutiliza `MostrarEncabezado` y agrega las cuatro opciones del menú.

```
SubProceso MostrarMenu(nombreCliente, numeroCuenta)
    MostrarEncabezado(nombreCliente, numeroCuenta)
    Escribir '¿Qué desea realizar hoy?'
    Escribir '1. Consultar saldo'
    Escribir '2. Depositar saldo'
    Escribir '3. Retirar saldo'
    Escribir '4. Salir'
FinSubProceso
```

### ConsultarSaldo

Muestra el saldo recibido por parámetro, sin modificarlo.

```
SubProceso ConsultarSaldo(saldo)
    Escribir 'Saldo actual: RD$', saldo
FinSubProceso
```

### VolverMenu

Pausa la ejecución hasta que el usuario presiona una tecla.

```
SubProceso VolverMenu
    Escribir 'Presione una tecla para volver...'
    Esperar Tecla
    Borrar Pantalla
FinSubProceso
```

## 3. Funciones de lectura

Leen una entrada del usuario y la devuelven por referencia. El valor de retorno indica si la lectura fue válida.

### LeerOpcion

Lee la opción del menú.

```
Funcion exito <- LeerOpcion(opcion Por Referencia)
    Definir exito Como Logico
    Leer opcion
    exito <- Verdadero
FinFuncion
```

### LeerMonto

Muestra el mensaje recibido y lee el monto. El mismo método se usa para depósitos y retiros.

```
Funcion exito <- LeerMonto(mensaje, monto Por Referencia)
    Definir exito Como Logico
    Escribir Sin Saltar mensaje
    Leer monto
    exito <- Verdadero
FinFuncion
```

## 4. Funciones de validación

Cada una implementa una sola regla de negocio y devuelve `Verdadero` o `Falso`. No leen del teclado ni escriben en pantalla.

### CuentaEstaActiva — RN-01

Indica si la cuenta permite operar.

```
Funcion activa <- CuentaEstaActiva(estado)
    Definir activa Como Logico
    activa <- estado
FinFuncion
```

### EsMontoValido — RN-02 / RN-03

Indica si el monto es mayor que cero.

```
Funcion valido <- EsMontoValido(monto)
    Definir valido Como Logico
    valido <- monto > 0
FinFuncion
```

### TieneFondos — RN-04

Indica si el saldo cubre el monto. Usa `>=` para permitir retirar el saldo exacto.

```
Funcion suficiente <- TieneFondos(saldo, monto)
    Definir suficiente Como Logico
    suficiente <- saldo >= monto
FinFuncion
```

## 5. Funciones de operación

Calculan el saldo resultante. Validan el monto por sí mismas y, si la operación no procede, devuelven el saldo anterior (RN-07). El algoritmo principal debe asignar el valor devuelto a `saldoDisponible` (RF-15).

### Depositar — RN-05

```
Funcion nuevoSaldo <- Depositar(saldo, monto)
    Definir nuevoSaldo Como Real
    Si NO EsMontoValido(monto) Entonces
        nuevoSaldo <- saldo
    SiNo
        nuevoSaldo <- saldo + monto
    FinSi
FinFuncion
```

### Retirar — RN-06

```
Funcion nuevoSaldo <- Retirar(saldo, monto)
    Definir nuevoSaldo Como Real
    Si NO EsMontoValido(monto) O NO TieneFondos(saldo, monto) Entonces
        nuevoSaldo <- saldo
    SiNo
        nuevoSaldo <- saldo - monto
    FinSi
FinFuncion
```

## 6. Resumen de métodos

| Método | Tipo | Parámetros | Devuelve | Regla / requerimiento |
|---|---|---|---|---|
| `MostrarEncabezado` | SubProceso | nombreCliente, numeroCuenta | — | RF-01 |
| `MostrarMenu` | SubProceso | nombreCliente, numeroCuenta | — | RF-10 |
| `ConsultarSaldo` | SubProceso | saldo | — | RF-11 |
| `VolverMenu` | SubProceso | — | — | RN-08 |
| `LeerOpcion` | Funcion | opcion (por referencia) | Lógico | RF-09 |
| `LeerMonto` | Funcion | mensaje, monto (por referencia) | Lógico | RF-13 |
| `CuentaEstaActiva` | Funcion | estado | Lógico | RF-14, RN-01 |
| `EsMontoValido` | Funcion | monto | Lógico | RN-02, RN-03 |
| `TieneFondos` | Funcion | saldo, monto | Lógico | RN-04 |
| `Depositar` | Funcion | saldo, monto | Real | RF-12, RN-05, RN-07 |
| `Retirar` | Funcion | saldo, monto | Real | RF-12, RN-06, RN-07 |
