# ChiHuoLianMeng (FoodLovers)

A console-based food ordering system written in C#, built as an exercise in object-oriented design.

The project was first written as a single procedural file, then rebuilt from scratch with separated responsibilities. This repository holds the refactored version.

## What it does

- Displays a seeded menu of dishes with names, prices and like counts
- Places orders for one or more dishes
- Manages an order queue — confirm an order, delete an order, view pending orders
- Tracks "likes" per dish and reorders the menu accordingly

## Design

The point of this exercise was separating concerns rather than getting the feature list right. Four layers:

| Layer | Type | Responsibility |
| --- | --- | --- |
| Data | `Dish`, `Order` | Plain data holders. No behaviour beyond their own state |
| Collection | `Menu`, `OrderManager` | Owns a collection of items and the operations on that collection |
| Logic | `OrderService` | Coordinates a workflow by talking to both collections |
| Presentation | `MainPage` | Console menu and input loop |

`Program.Main` only assembles the object graph and starts the presentation layer — it holds no business logic, so the flow of control stays readable from top to bottom.

```csharp
// Program.cs — composition only
var menu = new Menu();
var orders = new OrderManager();
var service = new OrderService(menu, orders);
new MainPage(service).Run();
```

## Stack

- C# / .NET Framework 4.8
- Console application
- Visual Studio 2022

## Build

```
msbuild ChiHuoLianMengOOP.csproj
```

Or open `ChiHuoLianMengOOP.csproj` in Visual Studio 2022 and press F5.

Compiles with 0 errors and 0 warnings.

## Notes

Comments in the source are in Chinese — this was coursework, and the commentary documents the reasoning behind each design decision as I worked through it.
