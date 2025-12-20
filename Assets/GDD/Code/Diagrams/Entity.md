```mermaid
classDiagram
    class Entity {
        -float currentHealth
        -float maxHealth
        -float currentSpeed
        -float maxSpeed
        -float currentDamage
        -float maxDamage
        -bool isAlive
        +TakeDamage(receivedDamage)
        +Heal(receivedHealth)
        +Die()
    }