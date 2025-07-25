# Unity Reels

[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=for-the-badge&logo=unity)](https://unity.com)

![Gameplay Demo](./demo.gif)

A classic 3-reel slot machine prototype built in Unity. This project focuses on UI development, asynchronous game flow with coroutines, and robust, decoupled game logic. It was created as a hands-on learning exercise.

## Table of contents

- [About The Project](#about-the-project)
- [Gameplay](#gameplay)
- [Key Features](#key-features)
- [Built With](#️built-with)
- [Getting Started](#getting-started)
- [Future Ideas](#future-ideas)

## About The Project

**Unity Reels** is a from-scratch implementation of a classic casino slot machine. More than just a simple game, this project serves as a deep dive into the Unity UI lifecycle and the challenges of creating perfectly synchronized animations.

This project was developed to learn and demonstrate fundamental Unity concepts, with a special focus on overcoming common development hurdles like race conditions with UI Layout Groups and understanding the critical difference between `localPosition` and `anchoredPosition` for dynamic UI elements.

## Gameplay

1.  **Place Your Bet:** The game starts with a fixed bet amount deducted from your currency.
2.  **Spin:** Press the "SPIN" button to start the reels.
3.  **Watch:** The reels spin and stop sequentially, creating suspense.
4.  **Win:** If the symbols on the payline form a winning combination, your currency is updated!

## Key Features

-   **Decoupled Architecture:** Central `GameManager` dictates game state and makes all random decisions, while individual `ReelController` scripts execute commands for spinning and stopping.
-   **Robust Reel Mechanics:** Reels feature an infinite loop effect created by dynamically re-parenting symbols (`SetAsLastSibling`). The stopping logic is frame-perfect, ensuring symbols always align on the payline.
-   **Asynchronous Game Flow:** The entire game sequence is managed by coroutines (`IEnumerator`) to handle time-based events like spinning durations and sequential reel stops without freezing the game.
-   **Dynamic UI with UGUI:** The interface is built entirely with Unity's UI tools, including the use of a `Mask` component to create the reel "window" effect.
-   **Event-Driven UI:** A clean UI event system using `Button.onClick.AddListener` to trigger the main game loop.

## Built With

-   **[Unity](https://unity.com/)** (Unity 6)
-   **[C#](https://docs.microsoft.com/en-us/dotnet/csharp/)**
-   **[TextMeshPro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html)** for text rendering

## Getting Started

To get a local copy up and running, follow these simple steps:

1.  Clone the repository:
    ```sh
    git clone https://github.com/AngelOfDeath19/Unity-Reels.git
    ```
2.  Open the project in **Unity Hub** (ensure you have a compatible Unity version installed).
3.  Once the project is loaded, open the main scene located in the `Assets/Scenes/` folder.
4.  Press the **Play** button in the editor.

## Future Ideas

While the core prototype is complete, here are a few ideas for future enhancements:

-   **Advanced Betting:** Add buttons to increase or decrease the bet amount.
-   **Visual & Audio Polish:** Implement particle effects for big wins and add sound effects for spinning, stopping, and winning.
-   **Complex Paylines:** Introduce more winning combinations (e.g., two cherries, mixed bars).
-   **Probability Control:** Adjust the symbol distribution on each reel's strip to control the odds of winning.
-   **Data Persistence:** Use `PlayerPrefs` or a JSON file to save the player's currency between game sessions.
