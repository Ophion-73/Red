```mermaid
classDiagram
    class Inventory {
        -string name
        -int count
        -int maxElements
        +TakeObject()
        +ConsumeObject()
    }

class Run {
    <<enum>>
    Out
    In
   }

  Inventory *-- Run : utiliza

  class Types {
    <<enum>>
    caperuzas
    comida
    transmutations
    trinkets
    charms
    amulet
    weapon
   }

  Inventory *-- Types : utiliza