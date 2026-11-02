# Mini ATM — Sistema de Cajero Automático en consola

Prototipo académico de un cajero automático desarrollado en C# como parte del **Programa FullStack .NET Cloud Developer**, Módulo 1: Fundamentos con C#. El proyecto evoluciona por versiones; cada una agrega capacidades sobre la anterior siguiendo su documento de requerimientos (BRD).

> **Versión actual: Actividad 4.5 — Refactorización mediante métodos (Semana 4)**

## Descripción

El sistema permite a un cliente realizar varias operaciones consecutivas en una misma sesión: consultar el saldo, depositar y retirar dinero de una cuenta precargada en memoria. El menú se repite hasta que el usuario elige **Salir**, y el saldo se mantiene actualizado durante toda la sesión.

En esta versión, el código de la V1 se reorganizó en **métodos con responsabilidades definidas**, sin cambiar el comportamiento ni las reglas de negocio. El objetivo es reducir la duplicación y facilitar la comprensión, las pruebas y el mantenimiento del programa.

## Funcionalidades

| Opción | Operación | Validaciones |
|---|---|---|
| 1 | Consultar saldo | — |
| 2 | Depositar | Cuenta activa, monto numérico, monto mayor que cero |
| 3 | Retirar | Cuenta activa, monto numérico, monto mayor que cero, fondos suficientes |
| 4 | Salir | Finaliza la sesión de forma controlada |

Una opción inexistente o un valor no numérico muestran un mensaje de error y el programa vuelve al menú, sin modificar el saldo.

## Métodos

| Método | Retorno | Responsabilidad |
|---|---|---|
| `MostrarEncabezado(nombreCliente, numeroCuenta)` | `void` | Limpia la pantalla y muestra el título y los datos de la cuenta |
| `MostrarMenu(nombreCliente, numeroCuenta)` | `void` | Muestra el encabezado y las cuatro opciones |
| `ConsultarSaldo(saldo)` | `void` | Muestra el saldo recibido sin modificarlo |
| `VolverMenu()` | `void` | Pausa hasta presionar una tecla y limpia la pantalla |
| `CuentaEstaActiva(estado)` | `bool` | Indica si la cuenta permite operar |
| `LeerOpcion(out opcion)` | `bool` | Lee la opción del menú e indica si es numérica |
| `LeerMonto(mensaje, out monto)` | `bool` | Solicita un monto e indica si es numérico |
| `EsMontoValido(monto)` | `bool` | Indica si el monto es mayor que cero |
| `TieneFondos(saldo, monto)` | `bool` | Indica si el saldo cubre el monto |
| `Depositar(saldo, monto)` | `decimal` | Devuelve el saldo aumentado, o el anterior si el monto es inválido |
| `Retirar(saldo, monto)` | `decimal` | Devuelve el saldo disminuido, o el anterior si el monto es inválido o no hay fondos |

Todos los métodos son `static`: trabajan solo con los datos que reciben por parámetro. El detalle de contratos y líneas de código está en la [matriz de trazabilidad](docs/trazabilidad_4.5.md).

## Conceptos aplicados

- Métodos `void` y métodos con valor de retorno (`bool`, `decimal`)
- Parámetros por valor y parámetros `out`
- Alcance de variables con métodos `static`
- Programación defensiva: `Depositar` y `Retirar` validan por sí mismos y devuelven el saldo anterior si la operación no procede
- Ciclo `do-while`, `switch`, `if / else`, `TryParse` y `decimal` (heredados de V1)

## Cómo ejecutar

Requisito: [.NET SDK 10](https://dotnet.microsoft.com/download).

```bash
git clone https://github.com/DHDanaurysHiraldo/MiniATMv1.git
cd MiniATMv1
dotnet run
```

## Datos de prueba

| Dato | Valor |
|---|---|
| Cliente | Samuel Reyes |
| Número de cuenta | 0458291046 |
| Saldo inicial | RD$10,000.00 |
| Estado de cuenta | Activa |

Para probar una cuenta inactiva (CP-06), cambiar `estadoCuenta` a `false` en `Program.cs`.

## Reglas de negocio

| ID | Regla |
|---|---|
| RN-01 | Una cuenta inactiva no puede depositar ni retirar. |
| RN-02 | El depósito debe ser numérico y mayor que cero. |
| RN-03 | El retiro debe ser numérico y mayor que cero. |
| RN-04 | El retiro no puede exceder el saldo disponible. |
| RN-05 | Un depósito válido aumenta el saldo exactamente en el monto depositado. |
| RN-06 | Un retiro válido disminuye el saldo exactamente en el monto retirado. |
| RN-07 | Toda operación rechazada mantiene el saldo sin cambios. |
| RN-08 | Una opción inválida no termina la sesión. |
| RN-09 | El menú permanece disponible hasta seleccionar Salir. |
| RN-10 | La refactorización no modifica las reglas RN-01 a RN-09 ni los resultados esperados. |

## Pruebas de regresión

| Prueba | Escenario | Resultado esperado | Estado |
|---|---|---|---|
| CP-01 | Consultar saldo | Mostrar RD$10,000 | ✅ Pasó |
| CP-02 | Depositar RD$2,000 | Saldo RD$12,000 | ✅ Pasó |
| CP-03 | Retirar RD$3,000 | Saldo RD$7,000 | ✅ Pasó |
| CP-04 | Depositar RD$0 o -100 | Rechazar; saldo RD$10,000 | ✅ Pasó |
| CP-05 | Retirar RD$15,000 | Rechazar por fondos insuficientes | ✅ Pasó |
| CP-06 | Cuenta inactiva: depósito y retiro | Rechazar ambas; saldo sin cambio | ✅ Pasó |
| CP-07 | Opción 9 o letras en el menú | Error y retorno al menú | ✅ Pasó |
| CP-08 | Opción 4 | Terminar ejecución | ✅ Pasó |
| CP-09 | Depositar 2,000 y retirar 3,000 | Saldo final RD$9,000 | ✅ Pasó |
| CP-10 | Letras como monto | Rechazo sin excepción ni cambio de saldo | ✅ Pasó |
| CP-11* | Retirar el saldo exacto (RD$10,000) | Retiro exitoso; saldo RD$0 | ✅ Pasó |

\* Prueba adicional de valor límite para RN-04, no incluida en el BRD.

**Resultado: 11 de 11 pruebas aprobadas.** La refactorización conserva el comportamiento de V1 (RN-10).

Las capturas están en [`docs/evidencias/Lab_4.5/`](docs/evidencias/Lab_4.5/).

## Estructura del proyecto

```
├── Program.cs                     # Código fuente (flujo principal + métodos)
├── MiniATMv1.csproj               # Proyecto .NET
├── MiniATMv1.slnx                 # Solución
├── README.md
└── docs/
    ├── pseudocodigo_4.5.md        # Pseudocódigo 4.5 documentado
    ├── trazabilidad_4.5.md        # Matriz de trazabilidad 4.5
    └── evidencias/
        ├── Lab_3.10/              # Capturas de V1 (laboratorio 3.10)
        └── Lab_4.5/               # Capturas de la regresión 4.5
```

## Historial de versiones

| Versión | Actividad | Capacidad principal |
|---|---|---|
| V0 | 3.5 · Semana 3, Día 1 | Menú de una sola ejecución con validaciones |
| V1 | 3.10 · Semana 3, Día 2 | Menú repetitivo con `do-while`; saldo acumulado en la sesión |
| **V1 modular** | **4.5 · Semana 4** | **Refactorización en métodos con parámetros y valores de retorno** |
| V2 *(próxima)* | 4.10 · Semana 4 | Historial de movimientos con colecciones |

## Alcance

**Incluye:** menú interactivo, operaciones consecutivas, validaciones heredadas, métodos con parámetros y retorno, y pruebas de regresión.

**No incluye:** base de datos, archivos, autenticación, múltiples cuentas, historial de transacciones, clases de dominio, interfaces ni servicios externos. Las colecciones de movimientos se incorporarán en la actividad 4.10 (ATM V2).

## Observaciones

- Durante la refactorización se detectó y corrigió un defecto de valor límite en `TieneFondos` (usaba `>` en lugar de `>=`), que rechazaba retirar el saldo completo. Se agregó la prueba CP-11 para cubrir ese caso.
- `Depositar` y `Retirar` validan nuevamente el monto aunque el flujo principal ya lo haya hecho. Es intencional: el flujo principal valida para mostrar el mensaje correcto, y los métodos validan para garantizar la RN-07 aunque se reutilicen desde otro lugar.

## Autor

**DHDanaurysHiraldo** — [GitHub](https://github.com/DHDanaurysHiraldo)