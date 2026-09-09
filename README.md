# Culpas

A first-person PS1-inspired horror game set in the Kroosbani catacombs. Explore, scavenge, and survive as the walls close in — while your real-world microphone decides how safe you are.

> 🎮 **Play it on itch.io:** https://duckayyy.itch.io/culpas

> ⚠️ **Content warning:** loud sounds, jumpscares, flashing lights.

---

## Features

### Microphone-driven stealth
The game samples your default input device in real time. `MicListener` reads a rolling window of mic data, computes an RMS volume, and feeds it into `PlayerNoise` — a single noise meter that spikes on sound and decays over time. Real-world noise (talking, a cough, a startled shout) makes you louder in-game, and the monsters that hunt by sound react to it. You can pick a specific input device from the menu.

### Monster AI
Enemies run a four-state machine — **Patrol**, **Chase**, **Search**, **Flee** — layered on Unity's NavMesh:
- Each monster detects the player through either **Sight** or **Audio** (the mic-fed noise level), swappable per enemy via `EnemyData`.
- On losing the player they fall back to **Search**, moving toward the last known position before returning to patrol.
- Random teleport points let a monster reposition around the level so encounters don't get predictable.
- Timed growls and roars telegraph proximity; contact triggers a jumpscare sequence.

### Fragile, degrading survival
The player has 3 HP. Every hit doesn't just count down — it thickens the world fog and impairs your sight (`PlayerHP.ImpairSight`), so each mistake makes the next stretch harder to read. Run out of HP and the game-over flow takes over.

### Journal / lore collection
Journal entries (`JournalSO` scriptable objects) are scattered through the catacombs. Picking one up adds it to an in-game journal you can open and read, building out the story through exploration rather than cutscenes.

### The altar ritual
The ending puzzle is an altar with three slots that cycle through `mem` / `ento` / `mori`. Aligning all three to spell **memento mori** completes the ritual and ends the game. A masked gate blocks the route until you're carrying the mask.

### Inventory & interaction
- Look-based interaction (`LookInteractor`) for picking up and using objects.
- A hotbar UI with item slots, backed by `ItemData` and driven by `ItemEventManager` so picking up key items (like the mask) unlocks world state.
- Dialogue triggers, flickering lights, snack/item respawns, and spatialized sound events fill out the space.

## Design choices

- **PS1-era presentation** — jagged shadows, low-fi textures, and static-lit corners. The look is a deliberate constraint that hides draw distance and makes the fog feel diegetic.
- **Fog as a health bar** — instead of a HUD meter, damage is communicated by the world getting harder to see. It keeps the screen clean and raises tension as you get closer to death.
- **One noise value, many listeners** — mic input, movement, and world events all collapse into a single `PlayerNoise` level with a decay curve. Detection systems just read that number, which keeps monster behavior easy to tune.
- **Behavior split into small classes** — movement (`PatrolMovement`, `ChaseMovement`, `SearchMovement`) and detection (`SightDetection`, `AudioDetection`) are separate strategies the `EnemyController` swaps at runtime, so new enemy types are mostly data.
- **Lore is opt-in** — story lives in collectible journal pages, so players who just want to run and survive can, and players who want context can dig for it.

## Tech

| | |
|---|---|
| Engine | Unity **6000.4.0f1** (Unity 6) |
| Render pipeline | Universal Render Pipeline (URP) 17.4 |
| Input | Unity Input System 1.19 |
| AI | Unity AI Navigation (NavMesh) 2.0 |
| Audio | Unity `Microphone` API for live mic capture |
| Language | C# |
| Platform | Windows (64-bit) |

## Getting Started

### Play

1. Download **UniJam2.zip** (~66 MB) from the [itch.io page](https://duckayyy.itch.io/culpas).
2. Unzip the file.
3. Run `My Project.exe`.

> Grant microphone access when prompted — it's the core mechanic.

### Run from source

1. Install **Unity 6000.4.0f1** via Unity Hub.
2. Clone the repo:
   ```bash
   git clone https://github.com/saahilster/Jam.git
   ```
3. In Unity Hub, **Add** the `My project/` folder and open it.
4. Load a scene from `Assets/Scenes/` (e.g. `StartScreen`) and press Play.

## Project Structure

```
My project/
├── Assets/
│   ├── Scripts/
│   │   ├── Player/      # Movement, camera, mic capture, noise, HP, interaction
│   │   ├── Monsters/    # Enemy controller, state machine, detection, jumpscares
│   │   ├── Journal/     # Collectible lore entries + reader
│   │   ├── Worl/        # World: altar puzzle, gates, dialogue, sound events
│   │   └── UI/          # Menus, hotbar, hover text
│   ├── Scenes/          # Game scenes
│   ├── Audio/  Graphics/  Sprites/  3dModels/  Prefabs/
│   └── ...
├── Packages/
└── ProjectSettings/
```

## Credits

Created for UniJam 2 by:

- Ashton Legaspi - Gameplay Programmer
- Saahil Farook - Gameplay Programmer and Game Design
- Chris Joanne - Art
- Mai Bui - Art
- Jillian Magahilig - Art

Repo: [saahilster/Jam](https://github.com/saahilster/Jam)
