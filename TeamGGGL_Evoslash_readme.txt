Team GGGL Alpha Demo
====================

1. Start Scene File: Assets/Scenes/MenuScene

2. How to play /what parts of the level to observe technology requirements
-   Movement: W,A.S,D and/or Up,Down, Left, Right arrows
-   Attack: Left mouse click
-   Pause: Escape

3. Problem Areas:
-   Yet to implement core gameplay features:
    -   player and enemy damage system
    -   experience gains from killing enemies
    -   gameplay balance
    -   special buffs gained from picking up powerup orbs
-   Clipping issues with the Avatar
-   Merging enemies model and enemy ai to current branch
-   Enhancing environment with physics objects and additional environmental objectives
-   Adding another weapon, skills, or more player movement such as jump

4. Manifest:

Andrew
------
-   Worked on: Enemy asset, enemy AI, NavMesh implementation, spawner conciliation with new enemy assets/AI systems.
-   Assets: Enemy, Spawners, NavMesh
-   Scripts:
    -   Assets/Scripts/Zombie.cs
    -   Assets/Scripts/ZombieSpawner.cs

Gordon
------
-   Worked on: Player IK controls,movement and Katana implementation
-   Assets: 
    -   Player IK controls and movement fluidity with Katana
    -   Katana positioning relative to Player Animator
-   Scripts: 
    -   Assets/Scripts/PlayerSword.cs
    -   Assets/Scripts/Sword.cs

Jin
---
-   Worked on: player input system and player actors for movement and using weapon including animation, game manager for player activation, camera work with cinemachine, level arts
-   Assets
    -   Katana prefab and swing animation
    -   Player model and animations with IK for basic movement and attack motions
    -   Main camera with virtual camera for camera work
    -   Zombie Spawner and zombie
    -   Map design and particle system for fire effects
    -   Baked lighting effects and shadow effects
-   Scripts
    -   Assets/Scripts/GameManager.cs
    -   Assets/Scripts/PlayerInput.cs
    -   Assets/Scripts/PlayerMovement.cs
    -   Assets/Scripts/PlayerSword.cs
    -   Assets/Scripts/Sword.cs
    -   Assets/Scripts/Zombie.cs
    -   Assets/Scripts/ZombieSpawner.cs

Joseph
------
-   Worked on: User interface design (in Figma) and implementation, and core gameplay loop mechanics (waves, breaks between waves, & victory condition)
-   Assets:
    -   Main menu scene & UI (main screen and credits screen)
    -   Pause menu UI & end game screen UI
    -   In-game UI & icons (plus, diamond, and EXP)
    -   Post processing presets & menu screen blur
    -   Imported various Roboto font weights (in Assets/Fonts)
-   Scripts:
    -   Assets/Scripts/EndGameScreen.cs
    -   Assets/Scripts/GameManager.cs
    -   Assets/Scripts/InGameUI.cs
    -   Assets/Scripts/MainMenu.cs
    -   Assets/Scripts/MenuScreenBlur.cs
    -   Assets/Scripts/PauseMenu.cs
    -   (minor edits) Assets/Scripts/ZombieSpawner.cs

Leo:
----
-   Worked on: 
    -   Weapon design, effects, and animation
    -   Powerup design and system
    -   Music and sound effects
-   Assets:
    -   KatanaParent / Katana_LODA
    -   Background Music
    -   Powerup
-   Scripts:
    -   Assets/Scripts/PlayerSword.cs
    -   Assets/Scripts/Powerup.cs
