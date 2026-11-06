# Mini ATM — Sistema de Cajero Automático en consola

Prototipo académico de un cajero automático desarrollado en C# como parte del **Programa FullStack .NET Cloud Developer**, Módulo 1: Fundamentos con C#. El proyecto evoluciona por versiones; cada una agrega capacidades sobre la anterior siguiendo su documento de requerimientos (BRD).

> **Versión actual: Mini ATM V2 — Modularización e historial de movimientos (Semana 4)** · 🚧 *En desarrollo*

## Descripción

El sistema permite a un cliente realizar varias operaciones consecutivas en una misma sesión: consultar el saldo, depositar y retirar dinero de una cuenta precargada en memoria, y consultar el historial de movimientos de la sesión. El menú se repite hasta que el usuario elige **Salir**, y el saldo se mantiene actualizado durante toda la ejecución.

La V2 conserva las funcionalidades y reglas de la V1, organiza el código en clases según su responsabilidad e incorpora un historial de depósitos y retiros exitosos con `List<string>`.

## Funcionalidades

| Opción | Operación | Validaciones |
|---|---|---|
| 1 | Consultar saldo | — |
| 2 | Depositar dinero | Cuenta activa, monto numérico, monto mayor que cero |
| 3 | Retirar dinero | Cuenta activa, monto numérico, monto mayor que cero, fondos suficientes |
| 4 | Consultar movimientos | 🚧 En desarrollo |
| 5 | Salir | Finaliza la sesión de forma controlada |

### Comportamiento ante entradas inválidas

| Entrada | Ejemplo | Comportamiento | Referencia |
|---|---|---|---|
| Opción no numérica o fuera de rango | `abc`, `9` | Mensaje de error y retorno al menú | RF-02, RN-08 |
| Monto **no numérico** | `abc` | Mensaje de error y **se solicita el monto nuevamente** | RF-07, CP-06 |
| Monto numérico **no válido** | `0`, `-500` | Se rechaza la operación y el sistema vuelve al menú | RN-02, RN-03, CP-04 |
| Retiro mayor al saldo | `8000` con saldo `5000` | Se rechaza por fondos insuficientes y el sistema vuelve al menú | RN-03, CP-05 |

En ningún caso se modifica el saldo ni se cierra el programa.

## Arquitectura

El código se organiza en clases `static` según su responsabilidad (**separación de responsabilidades**). La lógica de negocio no depende de la consola: solo recibe datos, aplica reglas y devuelve resultados.

```
MiniATM/
├── Program.cs                       # Main: datos de la cuenta y ciclo del menú (coordina el flujo)
├── UI/
│   └── ConsolaATM.cs                # Presentación y lectura de datos (usa Console)
├── Logica/
│   └── OperacionesBancarias.cs      # Reglas de negocio y operaciones (no usa Console)
├── MiniATM.csproj
├── MiniATM.slnx
├── README.md
└── docs/
    ├── pseudocodigo_4.5.md          # Pseudocódigo de la actividad 4.5 (histórico)
    ├── trazabilidad_4.5.md          # Matriz de trazabilidad de la actividad 4.5 (histórico)
    └── evidencias/
        ├── Lab_3.10/                # Capturas de V1 (laboratorio 3.10)
        └── Lab_4.5/                 # Capturas de la regresión 4.5
```

### `UI/ConsolaATM`

| Método | Retorno | Responsabilidad |
|---|---|---|
| `MostrarEncabezado(nombreCliente, numeroCuenta)` | `void` | Limpia la pantalla y muestra el título y los datos de la cuenta |
| `MostrarMenu(nombreCliente, numeroCuenta)` | `void` | Muestra el encabezado y las cinco opciones |
| `ConsultarSaldo(saldo)` | `void` | Muestra el saldo recibido sin modificarlo |
| `VolverMenu()` | `void` | Pausa hasta presionar una tecla y limpia la pantalla |
| `LeerOpcion(out opcion)` | `bool` | Lee la opción del menú e indica si es numérica |
| `LeerMonto(mensaje)` | `decimal` | Solicita un monto y lo repite hasta recibir un valor numérico |

### `Logica/OperacionesBancarias`

| Método | Retorno | Responsabilidad |
|---|---|---|
| `CuentaEstaActiva(estado)` | `bool` | Indica si la cuenta permite operar |
| `EsMontoValido(monto)` | `bool` | Indica si el monto es mayor que cero |
| `TieneFondos(saldo, monto)` | `bool` | Indica si el saldo cubre el monto |
| `Depositar(saldo, monto)` | `decimal` | Devuelve el saldo aumentado, o el anterior si el monto es inválido |
| `Retirar(saldo, monto)` | `decimal` | Devuelve el saldo disminuido, o el anterior si el monto es inválido o no hay fondos |

### 🚧 Historial de movimientos (en desarrollo)

| Método | Responsabilidad |
|---|---|
| `RegistrarMovimiento(...)` | Agregar una operación exitosa al historial |
| `MostrarMovimientos(...)` | Recorrer y presentar el historial en orden |

## Conceptos aplicados

- Clases `static` organizadas por responsabilidad, con `namespace` por carpeta
- Métodos con parámetros por valor, parámetros `out` y valores de retorno
- Modificadores de acceso: clases `internal`, métodos `public`
- Programación defensiva: `Depositar` y `Retirar` validan por sí mismos
- Ciclos `do-while` para el menú y para la lectura del monto
- `switch`, `if / else`, `TryParse` y `decimal`
- Colección `List<string>` para el historial *(en desarrollo)*

## Cómo ejecutar

Requisito: [.NET SDK 10](https://dotnet.microsoft.com/download).

```bash
git clone https://github.com/DHDanaurysHiraldo/MiniATM.git
cd MiniATM
dotnet run
```

## Datos de prueba

| Dato | Valor |
|---|---|
| Cliente | Samuel Reyes |
| Número de cuenta | 0458291046 |
| Saldo inicial | RD$5,000.00 |
| Estado de cuenta | Activa |

El saldo y el historial viven solo en memoria: al cerrar el programa se pierden (RN-06). Para probar una cuenta inactiva (CP-09), cambiar `estadoCuenta` a `false` en `Program.cs`.

## Reglas de negocio

| ID | Regla |
|---|---|
| RN-01 | El saldo no podrá ser negativo. |
| RN-02 | Los depósitos deberán ser mayores que cero. |
| RN-03 | Los retiros deberán ser mayores que cero y menores o iguales al saldo disponible. |
| RN-04 | Una cuenta inactiva no podrá realizar depósitos ni retiros. |
| RN-05 | Solo se registrarán depósitos y retiros completados exitosamente. |
| RN-06 | Los movimientos se conservarán únicamente durante la ejecución de la aplicación. |
| RN-07 | El historial conservará el orden en que se realizaron las operaciones. |
| RN-08 | Los errores de entrada no deberán finalizar inesperadamente el programa. |
| RN-09 | La refactorización no deberá alterar las funcionalidades válidas del Mini ATM V1. |

## Pruebas

Saldo inicial RD$5,000.00 y cuenta activa, salvo indicación contraria.

| Prueba | Escenario | Resultado esperado | Estado |
|---|---|---|---|
| CP-01 | Consultar saldo inicial | RD$5,000.00 | ⬜ |
| CP-02 | Depositar RD$1,000 | Saldo RD$6,000; registrar depósito | ⬜ |
| CP-03 | Retirar RD$500 | Saldo RD$5,500; registrar retiro | ⬜ |
| CP-04 | Depositar RD$0 | Rechazar; saldo sin cambios | ⬜ |
| CP-05 | Retirar RD$8,000 | Rechazar por fondos insuficientes | ⬜ |
| CP-06 | Introducir letras como monto | Mostrar error y solicitar nuevamente | ⬜ |
| CP-07 | Consultar historial sin operaciones | Informar que no existen movimientos | ⬜ |
| CP-08 | Consultar historial después de depósito y retiro | Mostrar ambos en orden | ⬜ |
| CP-09 | Intentar operar con cuenta inactiva | Rechazar la operación | ⬜ |
| CP-10 | Realizar una operación inválida | No registrar movimiento | ⬜ |
| CP-11 | Ejecutar múltiples operaciones | Mantener saldo e historial coherentes | ⬜ |
| CP-12 | Seleccionar Salir | Finalizar correctamente | ⬜ |

## Historial de versiones

| Versión | Actividad | Capacidad principal |
|---|---|---|
| V0 | 3.5 · Semana 3, Día 1 | Menú de una sola ejecución con validaciones |
| V1 | 3.10 · Semana 3, Día 2 | Menú repetitivo con `do-while`; saldo acumulado en la sesión |
| V1 modular | 4.5 · Semana 4, Día 1 | Refactorización en métodos con parámetros y valores de retorno |
| **V2** | **Semana 4, Día 2** | **Clases por responsabilidad, menú de cinco opciones, relectura de montos no numéricos e historial con `List<string>`** |

## Alcance

**Incluye:** consulta de saldo, depósitos, retiros, validaciones, registro de movimientos exitosos, consulta de historial y salida controlada.

**No incluye:** bases de datos, autenticación bancaria real, transferencias, múltiples cuentas ni persistencia entre ejecuciones.

## Observaciones

- **Entradas no numéricas vs. montos no válidos.** Las entradas no numéricas se solicitan nuevamente (CP-06). Los montos numéricos no válidos (cero, negativos o mayores al saldo) se rechazan y el sistema vuelve al menú (CP-04, CP-05), conservando el comportamiento de V1 (RN-09). La separación es intencional: `LeerMonto` (interfaz) solo garantiza que el valor sea numérico, mientras que `EsMontoValido` y `TieneFondos` (lógica) deciden si el monto es aceptable para el negocio.
- **Cambio de firma de `LeerMonto`.** En la actividad 4.5 era `bool LeerMonto(string, out decimal)`. En V2 pasa a ser `decimal LeerMonto(string)`: como repite la solicitud hasta recibir un número, ya no puede fallar y el valor de retorno `bool` dejó de tener sentido.
- **Plataforma.** El BRD indica .NET 8; el proyecto usa .NET 10, compatible con todo lo requerido.
- `Depositar` y `Retirar` validan nuevamente el monto aunque el flujo principal ya lo haya hecho. Es intencional: el flujo principal valida para mostrar el mensaje correcto, y los métodos validan para garantizar las reglas aunque se reutilicen desde otro lugar.

## Autor

**DHDanaurysHiraldo** — [GitHub](https://github.com/DHDanaurysHiraldo)