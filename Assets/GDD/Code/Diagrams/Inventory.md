```mermaid
classDiagram
    class Inventory {
        -enum run {out, in}
        -string name
        -enum types {caperuzas, comida, ransmutations, trinkets, charms,  amulet, weapon}
        -int count
        -int maxElements
        +TakeObject()
        +ConsumeObject()
    }