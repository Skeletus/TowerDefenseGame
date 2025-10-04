# PathGuard TD

A modern 3D tower-defense game built with **Unity** and **C#**. Place and upgrade towers, route swarms of varied enemies, and survive escalating waves across tile-based maps with seamless level transitions and a polished game loop.

---

## 🎮 Overview

PathGuard TD focuses on **readable architecture** and **extendable systems**. It ships with a robust build mode, advanced targeting, diverse enemy archetypes (including bosses and fliers), and a clean UI with feedback, audio, and post-processing polish. The codebase emphasizes **clean C#**, **SOLID** so you can grow the game without rewrites.

---

## ✨ Core Features

* **Enemy & Wave Management**

  * NavMesh movement, waypoints, respawn points
  * Wave lists with timers, scaling, and dynamic difficulty
* **Diverse Enemy Types**

  * Runners, swarm units, shielded tanks, stealth/buffed units, flying enemies, and boss encounters
* **Build System**

  * Ghost previews, radius visualization, hotkeys, blocked cells, and unlocks
* **Towers**

  * Cannons, machine guns, lasers, drones (expandable via ScriptableObjects)
* **Targeting & Damage**

  * Priorities: by progress, proximity, group density; dynamic retargeting
* **Levels & 3D Tile System**

  * Editor tools for fast map layout; on-the-fly NavMesh updates
* **Polished Game Loop**

  * Win/Loss conditions, pacing, and seamless level transitions
* **UI/UX**

  * Main/menu flow, in-game HUD (HP, currency), feedback (hover, shake, fades)
* **Audio**

  * Centralized audio manager for SFX, music, and volume settings
* **Camera**

  * Smooth pan/rotate/zoom, transitions, and screen shake
* **Post-Processing & Optimization**

  * Lightweight effects, pooling, culling, and profiler-guided tweaks

---

## 🧪 Skills C# & Game Dev

**C# / Engineering**

* Object-Oriented Design, **SOLID**, composition over inheritance
* **ScriptableObjects** for data-driven content (towers, enemies, waves)
* Events & decoupling (C# events/Actions), state & lifecycle management
* Clean architecture: separation of concerns, dependency boundaries
* Writing maintainable, testable gameplay code

**Unity / Gameplay**

* **NavMesh** agents & dynamic navigation
* Custom editor tooling & 3D tile workflows
* Targeting systems, damage & status effects
* Object pooling, performance profiling & micro-optimizations
* UI Toolkit/UGUI workflows, animation & feedback design
* Audio pipelines and camera feel (shake, damping, limits)

---

## 🧭 Systems at a Glance

* **Build System** – Grid-aware placement with ghost previews, radius circles, and obstruction checks.
* **Towers** – Modular behaviors (target acquisition, fire controllers, projectile/beam/drone emitters).
* **Targeting** – Select by “most progressed,” “closest,” or “densest cluster,” with live switching.
* **Enemies & Waves** – Waypoint runners, shielded tanks (layered health), stealth buffs, fliers with custom paths, and bosses with phased mechanics.
* **Levels** – 3D tiles + editor tools to paint paths/blocks; quick iteration with automated NavMesh rebuilds.
* **UI & Feedback** – Currency, health, cooldowns; hover/press animations, screen shake on hits, and clean transitions.
* **Audio** – Centralized manager for mixing, categories, and runtime volume changes.
* **Optimization** – Object pooling, baked data, minimal GC allocations, and profiler-driven fixes.

---

## 🚀 Getting Started

### Prerequisites

* **Unity 2020 or later** (LTS recommended)
* A PC/Mac capable of running Unity
* Basic C# knowledge is helpful but not required

---

## 🗺️ Roadmap

* Endless mode & leaderboards
* Elemental/status effects (slow, burn, stun)
* In-game tower upgrade trees & sell/refund
* More biomes and dynamic path events
* Save system for profiles and progress

---

## 🤝 Contributing

Issues and PRs are welcome! Please keep code **modular**, follow **SOLID** principles, and prefer **data-driven** additions (ScriptableObjects) over hard-coding.
