# Planificaci�n del Proyecto

## Objetivo del Proyecto

El objetivo principal del proyecto es desarrollar un programa en Windows Forms que lea y procese un archivo plano con datos de la proyecci�n de poblaci�n del Instituto Nacional de Estad�sticas y Censos de Costa Rica (INEC) para el a�o 2025. El programa debe:

1. **Cargar el archivo** en un DataGrid para mostrar datos de edades y cantidades de poblaci�n segmentadas por sexo y nivel educativo.
2. **Agrupar y calcular distribuciones** de poblaci�n en diferentes grupos etarios y niveles educativos usando hilos independientes.
3. **Mostrar los resultados** en varios DataGrids, organizados seg�n sexo, grupo etario y nivel educativo.
4. **Utilizar una barra de progreso** para indicar el procesamiento mientras los hilos realizan los c�lculos.

Este proyecto implica el uso de concurrencia, manipulaci�n de archivos y organizaci�n de datos en una interfaz gr�fica de usuario (UI).

---

## Descripci�n de Clases y su Funcionalidad

### 1. AccesoDatos

- **`LectorArchivo`**
  - **Prop�sito**: Leer y parsear el archivo plano de datos de poblaci�n del INEC.
  - **Funcionalidad**:
    - Abre el archivo `Proyeccion_2025.txt` y lee los datos l�nea por l�nea.
    - Extrae y transforma los datos en una lista de objetos `DatosPoblacion` para facilitar el procesamiento posterior.
    - Devuelve una lista de `DatosPoblacion` a la capa de l�gica de negocio.
  - **M�todo principal**:
    ```csharp
    public List<DatosPoblacion> LeerArchivo(string filePath)
    ```

### 2. LogicaNegocio

- **`ServicioPoblacion`**
  - **Prop�sito**: Manejar la l�gica de negocios de la aplicaci�n, especialmente los c�lculos de distribuci�n por edad y sexo.
  - **Funcionalidad**:
    - Recibe los datos de poblaci�n desde `LectorArchivo` y los agrupa en distintos rangos etarios y niveles educativos.
    - Coordina la creaci�n y ejecuci�n de hilos para cada grupo de c�lculo (por ejemplo, c�lculos de grupos etarios y educativos).
    - Comunica los resultados a la capa de presentaci�n para mostrarlos en los DataGrids de la UI.
  - **M�todos principales**:
    ```csharp
    public void CalcularDistribucionPorEdad(List<DatosPoblacion> datos)
    public void CalcularDistribucionPorEducacion(List<DatosPoblacion> datos)
    ```

- **`ServicioAgrupacion`**
  - **Prop�sito**: Realizar los c�lculos de agrupaci�n y clasificaci�n espec�ficos de datos de poblaci�n.
  - **Funcionalidad**:
    - Agrupa los datos en rangos etarios (0-4, 5-9, 10-14, etc.) y niveles educativos (Primaria Completa, Secundaria Incompleta, Universitaria Incompleta, etc.).
    - Permite el acceso a funciones de agrupaci�n que son llamadas por `ServicioPoblacion`.
  - **M�todos principales**:
    ```csharp
    public Dictionary<string, int> AgruparPorEdad(List<DatosPoblacion> datos)
    public Dictionary<string, int> AgruparPorEducacion(List<DatosPoblacion> datos)
    ```

- **`GestorHilos`**
  - **Prop�sito**: Gestionar y ejecutar los hilos que procesan las diferentes partes de los datos de poblaci�n.
  - **Funcionalidad**:
    - Inicia y controla m�ltiples hilos de procesamiento, asegurando que cada uno ejecute una tarea espec�fica, como calcular la distribuci�n por edad y por educaci�n.
    - Sincroniza el progreso de los hilos y comunica el estado de avance a la capa de presentaci�n para actualizar la barra de progreso.
  - **M�todos principales**:
    ```csharp
    public void EjecutarHiloEdad(Action accion)
    public void EjecutarHiloEducacion(Action accion)
    ```

### 3. Modelos

- **`DatosPoblacion`**
  - **Prop�sito**: Representar una fila de datos de poblaci�n extra�da del archivo de proyecci�n del INEC.
  - **Propiedades**:
    - `int Edad`
    - `int CantidadHombres`
    - `int CantidadMujeres`
    - `int PrimariaCompleta`
    - `int SecundariaIncompleta`
    - `int UniversitariaCompleta`, etc.

- **`DatosEscolaridad`**
  - **Prop�sito**: Representar los datos de escolaridad de cada grupo etario.
  - **Propiedades**:
    - `string NivelEducacion`
    - `int Cantidad`
    - `int GrupoEtario`

### 4. Utilidades

- **`Constantes`**
  - **Prop�sito**: Definir valores constantes y configuraciones usadas en la aplicaci�n.
  - **Contenido**:
    - Rango de edades (por ejemplo, `Rango0_4`, `Rango5_9`, etc.).
    - Niveles educativos (por ejemplo, `PrimariaCompleta`, `SecundariaIncompleta`, etc.).
  - **Ejemplo de uso**:
    ```csharp
    public static readonly int[] RangoEtario0_4 = { 0, 4 };
    public static readonly string PrimariaCompleta = "Primaria Completa";
    ```

- **`AyudaValidacion`**
  - **Prop�sito**: Proveer m�todos de validaci�n para asegurar que los datos le�dos y procesados sean correctos.
  - **Funcionalidad**:
    - Validar si los datos de entrada cumplen con los formatos y rangos esperados.
    - Facilitar el manejo de errores en caso de que el archivo plano contenga datos incorrectos.
  - **M�todos principales**:
    ```csharp
    public static bool ValidarEdad(int edad)
    public static bool ValidarCantidad(int cantidad)
    ```

---

## Asignaci�n de Tareas por Integrante

### Integrante 1: Lector de Archivos y Estructura de Datos B�sica
- **Responsabilidades**: Implementar la clase `LectorArchivo` en la capa **AccesoDatos**.
- **Tareas espec�ficas**:
  - Crear el m�todo `LeerArchivo` que lee el archivo y convierte los datos en una lista de `DatosPoblacion`.
  - Validar el archivo para asegurar que los datos cumplen con el formato requerido.

### Integrante 2: Modelado de Datos
- **Responsabilidades**: Implementar las clases en la capa **Modelos**.
- **Tareas espec�ficas**:
  - Definir las propiedades en `DatosPoblacion` para representar edad, cantidad de hombres y mujeres, y niveles educativos.
  - Crear `DatosEscolaridad` con propiedades para el nivel educativo, cantidad y grupo etario.

### Integrante 3: Servicio de C�lculo de Distribuci�n por Edad
- **Responsabilidades**: Implementar la clase `ServicioPoblacion` en la capa **LogicaNegocio**.
- **Tareas espec�ficas**:
  - Crear el m�todo `CalcularDistribucionPorEdad` que reciba una lista de `DatosPoblacion` y realice los c�lculos de agrupamiento.
  - Colaborar con el integrante encargado del `GestorHilos` para ejecutar los c�lculos de forma concurrente.

### Integrante 4: Servicio de C�lculo de Distribuci�n por Educaci�n
- **Responsabilidades**: Implementar la clase `ServicioAgrupacion` en la capa **LogicaNegocio**.
- **Tareas espec�ficas**:
  - Crear el m�todo `CalcularDistribucionPorEducacion` para agrupar datos por niveles educativos.
  - Coordinar la ejecuci�n de los c�lculos en hilos junto con el integrante responsable del `GestorHilos`.

### Integrante 5: Gesti�n de Hilos y Sincronizaci�n
- **Responsabilidades**: Implementar la clase `GestorHilos` en la capa **LogicaNegocio**.
- **Tareas espec�ficas**:
  - Desarrollar m�todos para iniciar, pausar y monitorear el progreso de los hilos que ejecutan los c�lculos.
  - Asegurarse de que todos los c�lculos finalicen correctamente.

### Integrante 6: Interfaz Gr�fica de Usuario (UI)
- **Responsabilidades**: Dise�ar y codificar los formularios en **Formularios/Form_principal.cs**.
- **Tareas espec�ficas**:
  - Configurar el formulario principal (`Form_principal`) con los DataGrids para mostrar los datos agrupados.
  - Implementar el bot�n para cargar el archivo y mostrar los resultados de los c�lculos.

### Integrante 7: Utilidades y Validaci�n de Datos
- **Responsabilidades**: Crear la clase `Constantes` y `AyudaValidacion`.
- **Tareas espec�ficas**:
  - Definir constantes que representen los grupos etarios y los niveles educativos.
  - Desarrollar m�todos de validaci�n en `AyudaValidacion`.

---

## Resumen de Roles y Responsabilidades

| Integrante     | Clase(s) Principal(es)             | Responsabilidades                                     |
|----------------|------------------------------------|-------------------------------------------------------|
| Wagner         | `LectorArchivo`                    | Leer archivo y estructurar datos b�sicos              |
| Greivin        | `DatosPoblacion`, `DatosEscolaridad` | Modelar datos de poblaci�n y educaci�n                |
| Gerald         | `ServicioPoblacion`                | Calcular distribuci�n por edad                        |
| Integrante 4   | `ServicioAgrupacion`               | Calcular distribuci�n por educaci�n                   |
| Gerardo        | `GestorHilos`                      | Gesti�n de hilos y sincronizaci�n                     |
| Edwin          | `Form_principal` (UI)              | Interfaz gr�fica, botones, DataGrids                  |
| Integrante 7   | `Constantes`, `AyudaValidacion`    | Definici�n de constantes y validaci�n de datos        |
