# Cooking Duck – 3D Cooking Simulation Game

A 3D arcade-style cooking simulation game built with **Unity** and **C#**, inspired by games like [Overcooked](https://en.wikipedia.org/wiki/Overcooked). This project demonstrates clean code architecture, decoupled game logic, and modern C# design patterns in Unity.

---

## 1. Technical Requirements & Architecture

### Objective

The primary goal is to build a fully functional, modular 3D game while adhering to production-level software engineering practices:

* Decoupling UI and visual presentation from underlying game logic using C# events and interfaces.
* Implementing robust design patterns (Singleton, State Machines, ScriptableObjects).
* Utilizing Unity's New Input System for flexible control handling and key rebinding.

### Storyline & Concept

You play as an ambitious chef rising through the ranks of the culinary world. Your goal is to work your way through a series of increasingly demanding kitchens, each presenting unique layout challenges, tighter time limits, higher target scores, and more complex menu recipes.

* Level Progression: Each level represents a new culinary challenge or restaurant venue. To clear a level and unlock the next stage, the player must meet or exceed a target score within the time limit by successfully fulfilling customer orders.

* Core Gameplay Loop: In every level, you must take incoming orders from the order queue, retrieve raw ingredients, process them at cooking and cutting workstations (e.g., slicing vegetables, frying meat to the correct cooked state), assemble completed meals onto plates, and deliver them to the service counter before time expires.

* Failing & Retrying: If you fail to reach the level's target score before the timer runs out, you must retry the level; clearing the target unlocks the next level with new recipes and faster-paced mechanics.

### Class Diagram Overview

```mermaid
classDiagram
    class Player {
        +holdingItem
        +move()
        +pickUp()
        +drop()
        +interact()
    }
    class Item {
        +name
    }
    class Ingredient {
        +state
    }
    class IngredientState {
        <<enumeration>>
        raw
        chopped
        cooked
        burnt
    }
    class Plate {
        +ingredients
        +addIngredient()
    }
    class Station {
        +interact(player)
    }
    class Counter {
        +currentItem
    }
    class IngredientBox {
        +ingredientType
    }
    class PlateDispenser {
        +givePlate()
    }
    class WorkStation {
        +currentItem
        +progress
        +isDone
        +isBurning
        +startWork()
        +update()
    }
    class CuttingBoard {
        +cuttingProgress
        +cuttingProgressMax
        +cut()
        +testCuttingProgress()
    }
    class Stove {
        +cookingTimer
        +burningTimer
        +currentState
        +startCooking()
        +updateCookingTimer()
    }
    class ServeStation {
        +deliveryManager
        +deliverPlate(plate)
    }
    class Trash {
        +interact(player)
    }
    class GameManager {
        +score
        +timer
        +createOrder()
        +checkOrder()
    }
    class Level {
        +levelNumber
        +targetScore
        +timeLimit
        +availableRecipes
        +isUnlocked
    }
    class LevelManager {
        +currentLevelIndex
        +levelsList
        +loadLevel(levelIndex)
        +completeLevel()
        +loadNextLevel()
        +checkLevelCompletion()
    }
    class Order {
        +timeLeft
    }
    class Recipe {
        +name
    }
    class RecipeIngredient {
        +name
        +requiredState
    }
    class OrderBoard {
        +showOrders()
    }

    Player "1" --> "0..1" Item : holds
    Item <|-- Ingredient
    Item <|-- Plate
    Ingredient --> IngredientState
    Plate o-- Ingredient

    Station <|-- Counter
    Station <|-- IngredientBox
    Station <|-- PlateDispenser
    Station <|-- WorkStation
    Station <|-- ServeStation
    Station <|-- Trash
    WorkStation <|-- CuttingBoard
    WorkStation <|-- Stove

    Counter --> Item : keeps
    WorkStation --> Ingredient : works on
    IngredientBox ..> Ingredient : creates
    PlateDispenser ..> Plate : creates

    ServeStation --> GameManager : sends plate
    GameManager o-- Order
    GameManager --> LevelManager : reports progress
    LevelManager o-- Level : manages
    Level --> Recipe : contains available
    Order --> Recipe
    Recipe *-- RecipeIngredient
    OrderBoard --> GameManager : reads orders
```

### Flowchart Diagram

```mermaid
graph TD
    %% Phase 1: Foundation
    subgraph Phase1 [1 Foundation]
        F1[Project and assets] --> F2[Player movement]
        F2 --> F3[Animation, camera]
        F3 --> F4[New input system]
        F4 --> F5[Collision detection]
    end

    %% Phase 2: Kitchen gameplay
    subgraph Phase2 [2 Kitchen gameplay]
        K1[Clear counter] --> K2[Kitchen objects]
        K2 --> K3[Container, cutting]
        K3 --> K4[Stove, trash]
        K4 --> K5[Plates, plate UI]
    end

    %% Phase 3: Game flow
    subgraph Phase3 [3 Game flow]
        G1[Delivery and orders] --> G2[Sound and music]
        G2 --> G3[Start, game over]
        G3 --> G4[Menus and options]
        G4 --> G5[Polish and ship]
    end

    %% Connections between phases
    F5 ==> K1
    K5 ==> G1
```
