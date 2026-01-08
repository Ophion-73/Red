# Level Generation Algorithm

## PHASE 1: Preparation

### Step 1: Scene Creation
* Create a new scene in Unity named `Level`.
* Create an empty GameObject in the hierarchy named `GeneratorManager`. This will be the brain of the system.

### Step 2: Data System (ScriptableObjects)
* Create a script named `ScenarioData.cs`. 
* This file will allow for the creation of data files (one for each map needed).
* **Required fields:**
    * `Scenario Name` (String)
    * `Initial Room Prefab` (GameObject)
    * `Combat Room Pool` (List of GameObjects)
    * `Boss Room Prefab` (GameObject)

### Step 3: Prefab Preparation (Rooms)
* Design the rooms in 2.5D.
* Each room must have an empty object for entry and exit.

---

## PHASE 2: Algorithm Programming (C#)

### Step 4: Create the `LevelGenerator` class
Create a C# script and define the main variables:
* Reference to the current `ScenarioData`.
* Generated room counter (int).
* Room limit.

### Step 5: Connection Logic
The algorithm must perform the following actions:

1.  **Instantiate Start:** Place the initial room at position (0,0,0).
2.  **Generation Loop:**
    * Take the exit of the last placed room.
    * Choose a random room from the Combat Room list.
    * Instantiate the new room.
    * **New room alignment:** Move the new room so that its entrance exactly matches the position and rotation of the previous room's exit.
3.  **Place Boss:** Upon reaching the last room, use the Boss room.

---

## PHASE 3: Random Content and Navigation

### Step 6: Room Content Spawning
* Inside each room prefab, create empty points called `SpawnPoint` (these objects should be placed as children in an empty to maintain order).
* Create a script `RoomContent.cs` that executes when the room is instantiated:
    * Randomly choose between instantiating: An enemy, a reward chest, or a destructible obstacle.

### Step 7: NavMesh Generation
* Since the map changes every time, the navigation mesh must be dynamic.
* Once the generation loop is finished, perform the NavMesh bake.

---

## PHASE 4: Gameplay Flow

### Step 8: Scenario Transition
1.  Create a `GameManager` that keeps track of the scenarios.
2.  When the player defeats the boss, walk to the room's exit (Mario Bros. style).
3.  Upon passing through the exit, the `GameManager` changes the `ScenarioData` to the next one in the list and restarts the generation process.