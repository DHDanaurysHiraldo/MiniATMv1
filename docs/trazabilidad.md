# Matriz de trazabilidad — Mini ATM V1 (BRD-ATM-3.10)

Este documento conecta cada necesidad del negocio con sus requerimientos, reglas, casos de uso, el código que los implementa y las pruebas que lo demuestran.

Archivo de referencia: [`Program.cs`](../Program.cs). Los números de línea corresponden a la versión entregada del laboratorio 3.10 (commit `style: mensaje de despedida en opción 4...`).

## 1. Trazabilidad general (BRD sección 12)

| Necesidad | Requisitos | Reglas | Casos de uso | Implementación | Pruebas |
|---|---|---|---|---|---|
| RB-01 Varias operaciones sin reiniciar | RF-01, RF-02, RF-10, RF-13 | RN-09 | CU-01 a CU-04 | `do { ... } while (opcionDeMenu != 4)` (L8, L230) | CP-01, CP-07, CP-09 |
| RB-02 El saldo refleja la secuencia | RF-03, RF-04, RF-05, RF-11 | RN-05, RN-06, RN-12 | CU-01 a CU-03 | `saldoDisponible` declarado fuera del ciclo (L3); se actualiza en L99 y L184 | CP-02, CP-03, CP-09 |
| RB-03 Lo inválido no altera el saldo ni cierra la sesión | RF-06, RF-07, RF-08, RF-12 | RN-01 a RN-04, RN-07, RN-08, RN-11 | CU-02 a CU-04 | Validaciones en L27, L34, L54, L83, L90, L130, L161, L168, L175 y `default` (L219) | CP-04 a CP-08, CP-10, CP-12 |
| RB-04 La sesión termina solo con Salir | RF-09, RF-10 | RN-09, RN-10 | CU-05 | `case 4` (L213) + condición del ciclo (L230) | CP-11 |
| RB-05 Todo es comprobable con pruebas | RF-01 a RF-13 | RN-01 a RN-12 | CU-01 a CU-05 | Secciones 2 a 4 de este documento | CP-01 a CP-12 |

## 2. Requerimientos funcionales → código

| ID | Requerimiento | Origen | Implementación | Líneas |
|---|---|---|---|---|
| RF-01 | Mostrar información de cuenta y menú de cuatro opciones | 3.5 | Bloque de `Console.WriteLine` al inicio de cada vuelta | L10–25 |
| RF-02 | Identificar la operación mediante `switch` | 3.5 | `switch (opcionDeMenu)` | L43 |
| RF-03 | Consultar saldo actual | 3.5 | `case 1` | L45–51 |
| RF-04 | Depositar un monto válido | 3.5 | `case 2` | L53–126 |
| RF-05 | Retirar un monto válido sin exceder el saldo | 3.5 | `case 3` | L129–211 |
| RF-06 | Impedir depósitos y retiros si la cuenta está inactiva | 3.5 | `if (estadoCuenta == false)` | L54, L130 |
| RF-07 | Mostrar el motivo de rechazo sin modificar el saldo | 3.5 | Mensajes `"Operación Rechazada: ..."` | L56, L85, L92, L132, L163, L170, L177 |
| RF-08 | Rechazar opciones inexistentes | 3.5 | `else if (opcionDeMenu > 4)` y `default` | L34–36, L219–220 |
| RF-09 | Finalizar al seleccionar la opción 4 | 3.5 | `case 4` | L213–216 |
| RF-10 | Mostrar el menú nuevamente tras cada operación distinta de Salir | 3.10 | `do-while` con condición `opcionDeMenu != 4` | L8, L230 |
| RF-11 | Conservar el saldo actualizado durante la sesión | 3.10 | `saldoDisponible` declarado fuera del ciclo | L3 |
| RF-12 | Ante una opción no numérica, informar y volver al menú | 3.10 | `int.TryParse` | L27–33 |
| RF-13 | Permitir secuencias consecutivas de operaciones | 3.10 | El ciclo repite el menú con el saldo acumulado | L8–230 |

## 3. Reglas de negocio → código

| ID | Regla | Implementación | Líneas |
|---|---|---|---|
| RN-01 | La cuenta debe estar activa para depósitos y retiros | `if (estadoCuenta == false)` → rechazo | L54, L130 |
| RN-02 | El depósito debe ser mayor que cero | `else if (montoOperacion <= 0)` en `case 2` | L90 |
| RN-03 | El retiro debe ser mayor que cero | `else if (montoOperacion <= 0)` en `case 3` | L168 |
| RN-04 | El retiro no puede superar el saldo | `else if (montoOperacion > saldoDisponible)` | L175 |
| RN-05 | El depósito válido aumenta el saldo | `saldoDisponible = saldoDisponible + montoOperacion` | L99 |
| RN-06 | El retiro válido disminuye el saldo | `saldoDisponible = saldoDisponible - montoOperacion` | L184 |
| RN-07 | Una operación rechazada no modifica el saldo | El saldo solo se modifica en la última rama `else` de cada operación | L99, L184 |
| RN-08 | Una opción no reconocida genera un mensaje de error | `else if (opcionDeMenu > 4)` y `default` | L34, L219 |
| RN-09 | El menú se repite mientras no se seleccione la opción 4 | `while (opcionDeMenu != 4)` | L230 |
| RN-10 | La opción 4 finaliza la sesión de manera controlada | `case 4` + la condición del ciclo se vuelve falsa | L213–216, L230 |
| RN-11 | Las entradas no numéricas no producen excepciones no controladas | `int.TryParse` (menú) y `decimal.TryParse` (montos) | L27, L83, L161 |
| RN-12 | El saldo de una operación es la base de la siguiente | Variable única `saldoDisponible` fuera del ciclo | L3, L99, L184 |

## 4. Casos de prueba

Condición inicial de cada prueba independiente: cuenta activa y saldo RD$10,000, salvo que se indique otra cosa. En las pruebas de secuencia, las operaciones se ejecutan **sin reiniciar** el programa.

| Prueba | Escenario / secuencia | Resultado esperado | Líneas involucradas | Resultado obtenido | Estado | Evidencia |
|---|---|---|---|---|---|---|
| CP-01 | `1` | Muestra RD$10,000 y regresa al menú | L46, L230 | Muestra RD$ 10000.00 y regresa al menú | ✅ Pasó | [CP-01](evidencias/CP-01.png) |
| CP-02 | `2` → `2000` → `1` | Muestra RD$12,000 | L99, L46 | Muestra RD$ 12000.00 | ✅ Pasó | [CP-02_1](evidencias/CP-02_1.png), [CP-02_2](evidencias/CP-02_2.png) |
| CP-03 | `3` → `3000` → `1` | Muestra RD$7,000 | L184, L46 | Muestra RD$ 7000.00 | ✅ Pasó | [CP-03_1](evidencias/CP-03_1.png), [CP-03_2](evidencias/CP-03_2.png) |
| CP-04 | `2` → `0` | Rechaza y conserva RD$10,000 | L90–92 | Rechazo; consulta posterior RD$ 10000.00 | ✅ Pasó | [CP-04_1](evidencias/CP-04_1.png), [CP-04_2](evidencias/CP-04_2.png) |
| CP-05 | `3` → `15000` | Rechaza y conserva RD$10,000 | L175–177 | Rechazo por fondos insuficientes; consulta posterior RD$ 10000.00 | ✅ Pasó | [CP-05_1](evidencias/CP-05_1.png), [CP-05_2](evidencias/CP-05_2.png) |
| CP-06 | `3` → `-500` | Rechaza y conserva RD$10,000 | L168–170 | Rechazo; consulta posterior RD$ 10000.00 | ✅ Pasó | [CP-06_1](evidencias/CP-06_1.png), [CP-06_2](evidencias/CP-06_2.png) |
| CP-07 | `9` → `1` | Error y luego consulta normal | L34–36, L46 | Mensaje de opción inválida y luego consulta normal | ✅ Pasó | [CP-07_1](evidencias/CP-07_1.png), [CP-07_2](evidencias/CP-07_2.png) |
| CP-08 | `ABC` → `1` | Error controlado y menú operativo | L27–29, L46 | Mensaje de opción inválida; menú operativo | ✅ Pasó | [CP-08_1](evidencias/CP-08_1.png), [CP-08_2](evidencias/CP-08_2.png) |
| CP-09 | `2` → `2000`; `3` → `3000`; `1` | Muestra RD$9,000 | L99, L184, L46 | Muestra RD$ 9000.00 | ✅ Pasó | [CP-09_1](evidencias/CP-09_1.png), [CP-09_2](evidencias/CP-09_2.png), [CP-09_3](evidencias/CP-09_3.png), [CP-09_4](evidencias/CP-09_4.png) |
| CP-10 | `estadoCuenta = false` → `2` o `3` | Rechaza sin cambiar el saldo | L54–56, L130–132 | Rechazo por cuenta inactiva; saldo sin cambios | ✅ Pasó | [CP-10_1](evidencias/CP-10_1.png), [CP-10_2](evidencias/CP-10_2.png), [CP-10_3](evidencias/CP-10_3.png) |
| CP-11 | `4` | Despedida y terminación | L213–216, L230 | Mensaje de salida y fin del programa | ✅ Pasó | [CP-11](evidencias/CP-11.png) |
| CP-12 | `2` → `ABC` o `3` → `ABC` | Rechazo sin cierre ni modificación del saldo | L83–85, L161–163 | Rechazo por monto inválido; sin cierre; saldo sin cambios | ✅ Pasó | [CP-12_1](evidencias/CP-12_1.png), [CP-12_2](evidencias/CP-12_2.png), [CP-12_3](evidencias/CP-12_3.png) |

**Resultado: 12 de 12 pruebas aprobadas.** En las pruebas de rechazo (CP-04, 05, 06, 10 y 12) se realizó una consulta de saldo al final para demostrar que el saldo no cambió. Cada prueba se ejecutó desde un programa recién iniciado; las capturas de una misma prueba están numeradas en orden (`CP-02_1.png`, `CP-02_2.png`, ...).

**Nota sobre CP-09:** el BRD escribe la secuencia como "+2,000" y "-3,000" para indicar un depósito y un retiro. En el programa se ingresan los montos positivos `2000` y `3000`; si se escribiera `-3000` en el retiro, la regla RN-03 lo rechazaría.

**Nota sobre CP-10:** como la cuenta está precargada, esta prueba se realiza cambiando temporalmente `estadoCuenta` a `false` (L6). Después se restablece a `true`.

## 5. Depuración

Evidencia del uso de breakpoints e inspección de variables (BRD sección 14).

> ⏳ **Pendiente:** las capturas de depuración se agregarán en un próximo commit.

| Punto de interrupción | Línea | Variables inspeccionadas (Locals / Watch) | Qué se verifica | Evidencia |
|---|---|---|---|---|
| Lectura de la opción | L27 | `opcionDeMenu` | Valor de la opción en cada vuelta; queda en 0 cuando la entrada no es numérica | ⏳ Pendiente |
| Depósito autorizado | L99 | `saldoDisponible`, `montoOperacion` | El saldo aumenta exactamente en el monto ingresado | ⏳ Pendiente |
| Retiro autorizado | L184 | `saldoDisponible`, `montoOperacion` | El saldo disminuye y parte del valor dejado por la operación anterior (RN-12) | ⏳ Pendiente |
| Condición del ciclo | L230 | `opcionDeMenu` | El ciclo continúa con valores distintos de 4 y termina con 4 | ⏳ Pendiente |

Secuencia recomendada para la depuración: ejecutar CP-09 (depositar 2,000, retirar 3,000, consultar) con *Step Over* (F10), observando cómo `saldoDisponible` pasa de 10,000 a 12,000 y luego a 9,000.