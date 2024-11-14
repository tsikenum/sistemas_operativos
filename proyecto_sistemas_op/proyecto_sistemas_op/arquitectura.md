# Planificación del Proyecto

## Objetivo del Proyecto

El objetivo principal del proyecto es desarrollar un programa en Windows Forms que lea y procese un archivo plano con datos de la proyección de población del Instituto Nacional de Estadísticas y Censos de Costa Rica (INEC) para el año 2025. El programa debe:

1. **Cargar el archivo** en un DataGrid para mostrar datos de edades y cantidades de población segmentadas por sexo y nivel educativo.
2. **Agrupar y calcular distribuciones** de población en diferentes grupos etarios y niveles educativos usando hilos independientes.
3. **Mostrar los resultados** en varios DataGrids, organizados según sexo, grupo etario y nivel educativo.
4. **Utilizar una barra de progreso** para indicar el procesamiento mientras los hilos realizan los cálculos.

Este proyecto implica el uso de concurrencia, manipulación de archivos y organización de datos en una interfaz gráfica de usuario (UI).

---

## Descripción de Clases y su Funcionalidad

### 1. AccesoDatos

- **`LectorArchivo`**
  - **Propósito**: Leer y parsear el archivo plano de datos de población del INEC.
  - **Funcionalidad**:
    - Abre el archivo `Proyeccion_2025.txt` y lee los datos línea por línea.
    - Extrae y transforma los datos en una lista de objetos `DatosPoblacion` para facilitar el procesamiento posterior.
    - Devuelve una lista de `DatosPoblacion` a la capa de lógica de negocio.
  - **Método principal**:
    ```csharp
    public List<DatosPoblacion> LeerArchivo(string filePath)
    ```

### 2. LogicaNegocio

- **`ServicioPoblacion`**
  - **Propósito**: Manejar la lógica de negocios de la aplicación, especialmente los cálculos de distribución por edad y sexo.
  - **Funcionalidad**:
    - Recibe los datos de población desde `LectorArchivo` y los agrupa en distintos rangos etarios y niveles educativos.
    - Coordina la creación y ejecución de hilos para cada grupo de cálculo (por ejemplo, cálculos de grupos etarios y educativos).
    - Comunica los resultados a la capa de presentación para mostrarlos en los DataGrids de la UI.
  - **Métodos principales**:
    ```csharp
    public void CalcularDistribucionPorEdad(List<DatosPoblacion> datos)
    public void CalcularDistribucionPorEducacion(List<DatosPoblacion> datos)
    ```

- **`ServicioAgrupacion`**
  - **Propósito**: Realizar los cálculos de agrupación y clasificación específicos de datos de población.
  - **Funcionalidad**:
    - Agrupa los datos en rangos etarios (0-4, 5-9, 10-14, etc.) y niveles educativos (Primaria Completa, Secundaria Incompleta, Universitaria Incompleta, etc.).
    - Permite el acceso a funciones de agrupación que son llamadas por `ServicioPoblacion`.
  - **Métodos principales**:
    ```csharp
    public Dictionary<string, int> AgruparPorEdad(List<DatosPoblacion> datos)
    public Dictionary<string, int> AgruparPorEducacion(List<DatosPoblacion> datos)
    ```

- **`GestorHilos`**
  - **Propósito**: Gestionar y ejecutar los hilos que procesan las diferentes partes de los datos de población.
  - **Funcionalidad**:
    - Inicia y controla múltiples hilos de procesamiento, asegurando que cada uno ejecute una tarea específica, como calcular la distribución por edad y por educación.
    - Sincroniza el progreso de los hilos y comunica el estado de avance a la capa de presentación para actualizar la barra de progreso.
  - **Métodos principales**:
    ```csharp
    public void EjecutarHiloEdad(Action accion)
    public void EjecutarHiloEducacion(Action accion)
    ```

### 3. Modelos

- **`DatosPoblacion`**
  - **Propósito**: Representar una fila de datos de población extraída del archivo de proyección del INEC.
  - **Propiedades**:
    - `int Edad`
    - `int CantidadHombres`
    - `int CantidadMujeres`
    - `int PrimariaCompleta`
    - `int SecundariaIncompleta`
    - `int UniversitariaCompleta`, etc.

- **`DatosEscolaridad`**
  - **Propósito**: Representar los datos de escolaridad de cada grupo etario.
  - **Propiedades**:
    - `string NivelEducacion`
    - `int Cantidad`
    - `int GrupoEtario`

### 4. Utilidades

- **`Constantes`**
  - **Propósito**: Definir valores constantes y configuraciones usadas en la aplicación.
  - **Contenido**:
    - Rango de edades (por ejemplo, `Rango0_4`, `Rango5_9`, etc.).
    - Niveles educativos (por ejemplo, `PrimariaCompleta`, `SecundariaIncompleta`, etc.).
  - **Ejemplo de uso**:
    ```csharp
    public static readonly int[] RangoEtario0_4 = { 0, 4 };
    public static readonly string PrimariaCompleta = "Primaria Completa";
    ```

- **`AyudaValidacion`**
  - **Propósito**: Proveer métodos de validación para asegurar que los datos leídos y procesados sean correctos.
  - **Funcionalidad**:
    - Validar si los datos de entrada cumplen con los formatos y rangos esperados.
    - Facilitar el manejo de errores en caso de que el archivo plano contenga datos incorrectos.
  - **Métodos principales**:
    ```csharp
    public static bool ValidarEdad(int edad)
    public static bool ValidarCantidad(int cantidad)
    ```

---

## Asignación de Tareas por Integrante

### Integrante 1: Lector de Archivos y Estructura de Datos Básica
- **Responsabilidades**: Implementar la clase `LectorArchivo` en la capa **AccesoDatos**.
- **Tareas específicas**:
  - Crear el método `LeerArchivo` que lee el archivo y convierte los datos en una lista de `DatosPoblacion`.
  - Validar el archivo para asegurar que los datos cumplen con el formato requerido.

### Integrante 2: Modelado de Datos
- **Responsabilidades**: Implementar las clases en la capa **Modelos**.
- **Tareas específicas**:
  - Definir las propiedades en `DatosPoblacion` para representar edad, cantidad de hombres y mujeres, y niveles educativos.
  - Crear `DatosEscolaridad` con propiedades para el nivel educativo, cantidad y grupo etario.

### Integrante 3: Servicio de Cálculo de Distribución por Edad
- **Responsabilidades**: Implementar la clase `ServicioPoblacion` en la capa **LogicaNegocio**.
- **Tareas específicas**:
  - Crear el método `CalcularDistribucionPorEdad` que reciba una lista de `DatosPoblacion` y realice los cálculos de agrupamiento.
  - Colaborar con el integrante encargado del `GestorHilos` para ejecutar los cálculos de forma concurrente.

### Integrante 4: Servicio de Cálculo de Distribución por Educación
- **Responsabilidades**: Implementar la clase `ServicioAgrupacion` en la capa **LogicaNegocio**.
- **Tareas específicas**:
  - Crear el método `CalcularDistribucionPorEducacion` para agrupar datos por niveles educativos.
  - Coordinar la ejecución de los cálculos en hilos junto con el integrante responsable del `GestorHilos`.

### Integrante 5: Gestión de Hilos y Sincronización
- **Responsabilidades**: Implementar la clase `GestorHilos` en la capa **LogicaNegocio**.
- **Tareas específicas**:
  - Desarrollar métodos para iniciar, pausar y monitorear el progreso de los hilos que ejecutan los cálculos.
  - Asegurarse de que todos los cálculos finalicen correctamente.

### Integrante 6: Interfaz Gráfica de Usuario (UI)
- **Responsabilidades**: Diseñar y codificar los formularios en **Formularios/Form_principal.cs**.
- **Tareas específicas**:
  - Configurar el formulario principal (`Form_principal`) con los DataGrids para mostrar los datos agrupados.
  - Implementar el botón para cargar el archivo y mostrar los resultados de los cálculos.

### Integrante 7: Utilidades y Validación de Datos
- **Responsabilidades**: Crear la clase `Constantes` y `AyudaValidacion`.
- **Tareas específicas**:
  - Definir constantes que representen los grupos etarios y los niveles educativos.
  - Desarrollar métodos de validación en `AyudaValidacion`.

---

## Resumen de Roles y Responsabilidades

| Integrante     | Clase(s) Principal(es)             | Responsabilidades                                     |
|----------------|------------------------------------|-------------------------------------------------------|
| Wagner         | `LectorArchivo`                    | Leer archivo y estructurar datos básicos              |
| Greivin        | `DatosPoblacion`, `DatosEscolaridad` | Modelar datos de población y educación                |
| Gerald         | `ServicioPoblacion`                | Calcular distribución por edad                        |
| Integrante 4   | `ServicioAgrupacion`               | Calcular distribución por educación                   |
| Gerardo        | `GestorHilos`                      | Gestión de hilos y sincronización                     |
| Edwin          | `Form_principal` (UI)              | Interfaz gráfica, botones, DataGrids                  |
| Integrante 7   | `Constantes`, `AyudaValidacion`    | Definición de constantes y validación de datos        |
