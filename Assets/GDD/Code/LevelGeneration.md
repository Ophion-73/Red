# Algoritmo de Generación de Nievel

## FASE 1: Preparación

### Paso 1: Creación de la Escena
* Crear una nueva escena en Unity llamada `Level`.
* Crear un GameObject vacío en la jerarquía llamado `GeneratorManager`. Este será el cerebro del sistema.

### Paso 2: Sistema de Datos (ScriptableObjects)
* Crear un script llamado `EscenarioData.cs`. 
* Este archivo permitirá crear archivos de datos (uno para cada mapa que se necesite).
* **Campos necesarios:**
    * `Nombre del Escenario` (String)
    * `Prefab Sala Inicial` (GameObject)
    * `Pool de Salas de Combate` (Lista de GameObjects)
    * `Prefab Sala de Jefe` (GameObject)

### Paso 3: Preparación de los Prefabs (Salas)
* Diseñar las salas en 2.5D.
* Cada sala debe tener un objeto vacío de entrada y salida.

---

## FASE 2: Programación del Algoritmo (C#)

### Paso 4: Crear la clase `LevelGenerator`
Crear un script de C# y definir las variables principales:
* Referencia al `EscenarioData` actual.
* Contador de salas generadas (int).
* Límite de salas.

### Paso 5: Lógica de Conexión
El algoritmo debe realizar las siguientes acciones:

1.  **Instanciar Inicio:** Colocar la sala inicial en la posición (0,0,0).
2.  **Bucle de Generación:**
    * Tomar la salida de la última sala colocada.
    * Elegir una sala aleatoria de la lista de Salas de Combate.
    * Instanciar la nueva sala.
    * **Alineación de la nueva sala:** Mover la nueva sala para que su entrada coincida exactamente en posición y rotación con la salida de la sala anterior.
3.  **Colocar Boss:** Al llegar a la ultima sala, usar la sala del Boss.

---

## FASE 3: Contenido Aleatorio y Navegación

### Paso 6: Spawning del contenido de la Sala
* Dentro de cada prefab de sala, crear puntos vacíos llamados `SpawnPoint` (estos objetos se deben de colocar como hijos en un empty para mantener el orden).
* Crear un script `RoomContent.cs` que se ejecute al ser instanciada la sala:
    * Elegir aleatoriamente entre instanciar: Un enemigo, un cofre de recompensa o un obstáculo destructible.

### Paso 7: Generación de NavMesh
* Como el mapa cambia cada vez, la malla de navegación debe ser dinámica.
* Al finalizar el bucle de generación que se haga el bake del NavMesh.

---

## FASE 4: Flujo de Juego

### Paso 8: Transición de Escenarios
1.  Crear un `GameManager` que lleve el conteo de los escenarios.
2.  Cuando el jugador derrota al jefe, caminar a la salida de la sala (estilo Mario Bros.).
3.  Al pasar por la salida, el `GameManager` cambia el `EscenarioData` al siguiente en la lista y reinicia el proceso de generación.