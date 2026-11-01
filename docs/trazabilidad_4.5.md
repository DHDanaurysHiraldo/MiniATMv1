# Matriz de trazabilidad — Actividad 4.5: Refactorización mediante métodos (BRD ATM Semana 4)

Este documento conecta los requerimientos funcionales, las reglas de negocio, los casos de uso y los casos de prueba con los métodos que los implementan.

Archivo de referencia: [`Program.cs`](../Program.cs), rama `feature/4.5-metodos`. Los números de línea corresponden al commit `fix: TieneFondos permite retirar el saldo exacto (RN-04)`.

## 1. Trazabilidad general (BRD sección 10)

| Necesidad | Requerimientos | Reglas | Caso de uso | Métodos involucrados | Pruebas |
|---|---|---|---|---|---|
| Operación de menú | RF-01, RF-02, RF-09, RF-10 | RN-08, RN-09 | CU-04, CU-05 | `MostrarMenu`, `LeerOpcion`, `VolverMenu` | CP-07, CP-08 |
| Consulta de saldo | RF-03, RF-11 | RN-10 | CU-01 | `ConsultarSaldo` | CP-01 |
| Depósitos seguros | RF-04, RF-06, RF-07, RF-12 a RF-15 | RN-01, RN-02, RN-05, RN-07 | CU-02 | `CuentaEstaActiva`, `LeerMonto`, `EsMontoValido`, `Depositar` | CP-02, CP-04, CP-06 |
| Retiros seguros | RF-05 a RF-08, RF-12 a RF-15 | RN-01, RN-03, RN-04, RN-06, RN-07 | CU-03 | `CuentaEstaActiva`, `LeerMonto`, `EsMontoValido`, `TieneFondos`, `Retirar` | CP-03, CP-05, CP-06, CP-11 |
| Sin regresiones | RF-01 a RF-15 | RN-10 | CU-01 a CU-05 | Todos | CP-01 a CP-11 |

## 2. Métodos: responsabilidad y contrato

| Método | Parámetros | Retorno | Responsabilidad | Líneas (definición) | Se invoca en |
|---|---|---|---|---|---|
| `MostrarEncabezado` | `string nombreCliente`, `string numeroCuenta` | `void` | Limpia la pantalla y muestra el título y los datos de la cuenta | L147–160 | L41, L59, L80, L105, L139 |
| `MostrarMenu` | `string nombreCliente`, `string numeroCuenta` | `void` | Muestra el encabezado y las 4 opciones; no cambia el saldo | L137–145 | L10 |
| `ConsultarSaldo` | `decimal saldoDisponible` | `void` | Muestra el saldo recibido; no modifica datos | L162–166 | L28 |
| `VolverMenu` | — | `void` | Pausa hasta que el usuario presione una tecla y limpia la pantalla | L168–173 | 13 llamadas |
| `CuentaEstaActiva` | `bool estadoCuenta` | `bool` | Indica si la cuenta permite operar | L175–178 | L33, L71 |
| `LeerOpcion` | `out int opcionDeMenu` | `bool` | Lee la opción del menú; indica si la entrada es numérica | L185–188 | L12 |
| `LeerMonto` | `string mensaje`, `out decimal montoOperacion` | `bool` | Muestra el mensaje, lee el monto e indica si es numérico | L180–184 | L43, L83 |
| `EsMontoValido` | `decimal montoOperacion` | `bool` | Indica si el monto es mayor que cero | L190–193 | L49, L89, L202, L211 |
| `TieneFondos` | `decimal saldoDisponible`, `decimal montoOperacion` | `bool` | Indica si el saldo cubre el monto (permite retirar el saldo exacto) | L195–198 | L95, L216 |
| `Depositar` | `decimal saldoDisponible`, `decimal montoOperacion` | `decimal` | Devuelve el saldo aumentado, o el saldo anterior si el monto es inválido | L200–207 | L57 |
| `Retirar` | `decimal saldoDisponible`, `decimal montoOperacion` | `decimal` | Devuelve el saldo disminuido, o el saldo anterior si el monto es inválido o no hay fondos | L209–221 | L103 |

Todos los métodos son `static`: solo trabajan con los datos que reciben por parámetro y no acceden directamente a las variables del programa principal.

## 3. Requerimientos funcionales → código

| ID | Requerimiento | Origen | Implementación | Líneas |
|---|---|---|---|---|
| RF-01 | Mostrar un menú de consultar saldo, depositar, retirar y salir | 3.5 | `MostrarMenu` | L10, L137–145 |
| RF-02 | Permitir múltiples operaciones hasta elegir salir | 3.10 | `do { ... } while (opcionDeMenu != 4)` | L8, L131 |
| RF-03 | Consultar el saldo actual | 3.5 | `case 1` → `ConsultarSaldo` | L27–30 |
| RF-04 | Depositar un monto válido y actualizar el saldo | 3.5 | `case 2` → `Depositar` | L32–67 |
| RF-05 | Retirar un monto válido y actualizar el saldo | 3.5 | `case 3` → `Retirar` | L70–114 |
| RF-06 | Rechazar depósitos y retiros si la cuenta está inactiva | 3.5 | `CuentaEstaActiva` | L33, L71 |
| RF-07 | Rechazar montos no numéricos, cero o negativos | 3.5 | `LeerMonto` y `EsMontoValido` | L43, L49, L83, L89 |
| RF-08 | Rechazar retiros superiores al saldo | 3.5 | `TieneFondos` | L95 |
| RF-09 | Rechazar opciones inválidas sin finalizar la sesión | 3.10 | `LeerOpcion`, `else if (opcionDeMenu > 4)`, `default` | L12, L17, L122 |
| RF-10 | Separar la presentación del menú en `MostrarMenu()` | 4.5 | `MostrarMenu` (reutiliza `MostrarEncabezado`) | L137–145 |
| RF-11 | Separar la consulta de saldo en `ConsultarSaldo(saldo)` | 4.5 | `ConsultarSaldo` | L162–166 |
| RF-12 | `Depositar(saldo, monto)` y `Retirar(saldo, monto)` con retorno del saldo | 4.5 | `Depositar`, `Retirar` | L200–221 |
| RF-13 | Centralizar lectura y validación numérica en `LeerMonto(mensaje, out monto)` | 4.5 | `LeerMonto` | L180–184 |
| RF-14 | Verificar la cuenta activa mediante `CuentaEstaActiva(estado)` | 4.5 | `CuentaEstaActiva` | L175–178 |
| RF-15 | Mantener el saldo de sesión actualizado tras invocar los métodos | 4.5 | El retorno se asigna a `saldoDisponible` | L57, L103 |

## 4. Reglas de negocio → código

| ID | Regla | Implementación | Líneas |
|---|---|---|---|
| RN-01 | Una cuenta inactiva no puede depositar ni retirar | `if (!CuentaEstaActiva(estadoCuenta))` | L33, L71 |
| RN-02 | El depósito debe ser numérico y mayor que cero | `LeerMonto` + `EsMontoValido` en `case 2` | L43, L49 |
| RN-03 | El retiro debe ser numérico y mayor que cero | `LeerMonto` + `EsMontoValido` en `case 3` | L83, L89 |
| RN-04 | El retiro no puede exceder el saldo | `TieneFondos`: `saldoDisponible >= montoOperacion` | L95, L197 |
| RN-05 | Un depósito válido aumenta el saldo exactamente en el monto | `return saldoDisponible + montoOperacion` | L206 |
| RN-06 | Un retiro válido disminuye el saldo exactamente en el monto | `return saldoDisponible - montoOperacion` | L220 |
| RN-07 | Toda operación rechazada mantiene el saldo | Las ramas de rechazo no llaman a `Depositar`/`Retirar`; estos además devuelven el saldo anterior si la validación falla | L202–205, L211–219 |
| RN-08 | Una opción inválida no termina la sesión | Error + `VolverMenu()`; el ciclo continúa | L12–22, L122–126 |
| RN-09 | El menú permanece disponible hasta seleccionar Salir | Condición `opcionDeMenu != 4` | L131 |
| RN-10 | La refactorización no modifica reglas ni resultados | Regresión de los casos CP-01 a CP-11 | Sección 5 |

## 5. Casos de prueba de regresión

Precondición: cada caso inicia con el programa recién abierto, saldo RD$10,000 y cuenta activa, salvo indicación distinta. En las pruebas secuenciales, el saldo se mantiene dentro de la misma ejecución.

| Prueba | Escenario | Resultado esperado | Métodos ejercitados | Resultado obtenido | Estado | Evidencia |
|---|---|---|---|---|---|---|
| CP-01 | Consultar saldo | Mostrar RD$10,000 | `ConsultarSaldo` | ⬜ | ⬜ | ⬜ |
| CP-02 | Depositar RD$2,000 | Saldo RD$12,000 | `LeerMonto`, `EsMontoValido`, `Depositar` | ⬜ | ⬜ | ⬜ |
| CP-03 | Retirar RD$3,000 | Saldo RD$7,000 | `LeerMonto`, `EsMontoValido`, `TieneFondos`, `Retirar` | ⬜ | ⬜ | ⬜ |
| CP-04 | Depositar RD$0 o -100 | Rechazar; saldo RD$10,000 | `EsMontoValido` | ⬜ | ⬜ | ⬜ |
| CP-05 | Retirar RD$15,000 | Rechazar por fondos insuficientes; saldo sin cambio | `TieneFondos` | ⬜ | ⬜ | ⬜ |
| CP-06 | Cuenta inactiva: intentar depósito y retiro | Rechazar ambas operaciones; saldo sin cambio | `CuentaEstaActiva` | ⬜ | ⬜ | ⬜ |
| CP-07 | Elegir opción 9 o ingresar letras | Mensaje de error y retorno al menú | `LeerOpcion`, `VolverMenu` | ⬜ | ⬜ | ⬜ |
| CP-08 | Elegir opción 4 | Terminar ejecución | — (`case 4`) | ⬜ | ⬜ | ⬜ |
| CP-09 | Depositar 2,000 y luego retirar 3,000 | Saldo final RD$9,000 en la misma sesión | `Depositar`, `Retirar` (RF-15) | ⬜ | ⬜ | ⬜ |
| CP-10 | Ingresar letras como monto | Rechazar sin excepción ni cambio de saldo | `LeerMonto` | ⬜ | ⬜ | ⬜ |
| CP-11* | Retirar el saldo exacto (RD$10,000) | Retiro exitoso; saldo RD$0 | `TieneFondos`, `Retirar` | ⬜ | ⬜ | ⬜ |

\* **CP-11 es una prueba adicional** de valor límite para RN-04. No figura en el BRD; se agregó porque durante la refactorización `TieneFondos` usó inicialmente `>` en lugar de `>=`, lo que rechazaba retirar el saldo completo (comportamiento distinto a V1). El defecto se corrigió en el commit `fix: TieneFondos permite retirar el saldo exacto (RN-04)`.

Las capturas se guardan en [`docs/evidencias/4.5/`](evidencias/4.5/), separadas de las evidencias de V1.

**Nota sobre CP-06:** la cuenta está precargada, por lo que la prueba se realiza cambiando temporalmente `estadoCuenta` a `false` (L6) y restableciéndolo después.

## 6. Nota sobre el pseudocódigo

En [`MiniATM_4.5.psc`](MiniATM_4.5.psc), los métodos `void` se representan como `SubProceso` y los que devuelven un valor como `Funcion`. El parámetro `out` de C# se representa con `Por Referencia`. PSeInt no permite comprobar si una entrada es numérica sin producir un error, por lo que `LeerOpcion` y `LeerMonto` siempre devuelven `Verdadero` en el pseudocódigo; en C#, esa validación la realiza `TryParse`.
