# Task 2, AR Chess Interaction (Unity)
### CSU44057 Extended Reality Final Project, Group 7
**Author:** Diya Monica Mathew | **GitHub:** khaleesi123got

---

## What This Task Does

This Unity project implements an AR chess interaction system where:
- A chess board spawns on a real flat surface (table/floor) when the user taps
- Individual chess pieces can be grabbed and moved using pinch gestures
- A reset button returns all pieces to their original positions
- Designed for the Meta Quest 3 headset (Android)

---

## Tools & Software Installed

| Tool | Version | Purpose |
|------|---------|---------|
| Unity Hub | 3.x | Launcher for Unity Editor |
| Unity Editor | 6.3 LTS (6000.3.11f1) | Game engine used to build the project |
| Git Bash | Latest | Terminal for running Git commands on Windows |
| Visual Studio Code | Latest | Code editor for editing my C# scripts |


---

## Unity Modules Installed

These were installed via Unity Hub → Installs → Manage on Unity 6.3 LTS:

| Module | Purpose |
|--------|---------|
| Android Build Support | Required to run on Meta Quest headset (which runs Android) |
| Android SDK & NDK Tools | Android development tools needed for building |
| OpenJDK | Java runtime required for Android builds |
| Android SDK Platform Tools | Device communication tools |
| Android SDK Build Tools | Compilation tools for Android |
| Android NDK | Development kit for Android |
| CMake | Build system for code |
| Microsoft Visual Studio Community 2022 | C# development environment |

---

## Unity Packages Installed

Installed via Window → Package Manager → Unity Registry:

| Package | Purpose |
|---------|---------|
| AR Foundation | To handle plane detection, raycasting onto real surfaces |
| XR Hands | Hand tracking support to detect hand joints and poses for gesture recognition |
| XR Plugin Management | Connects Unity to the headset/device XR runtime |
| OpenXR Plugin | Connects Unity to OpenXR-compatible devices including Meta Quest |
| XR Interaction Toolkit | Handles XR interactions like grabbing and moving objects |

---

## OpenXR Configuration

Settings configured via Edit → Project Settings → XR Plugin Management → OpenXR:

| Setting | Why |
|---------|-----|
| Hand Interaction Profile | Allows hand tracking input |
| Hand Interaction Poses |Provides pose data for pinch/grab gestures |
| Hand Tracking Subsystem | Enables full hand tracking |
| Mock Runtime | Allows testing in Unity Editor without a physical headset |
| Interaction Profiles | Registers hand input with OpenXR |

---

## Scene Setup (Hierarchy)

Objects in the Unity scene (`SampleScene`):

| Object | Purpose |
|--------|---------|
| Directional Light | Lights the scene |
| Global Volume | Post-processing effects |
| XR Origin (Mobile AR) | The AR camera rig represents the user's viewpoint in AR |
| Camera Offset | Adjusts camera height |
| Main Camera | The AR camera that shows the real world |
| Trackables | Tracks detected AR planes (flat surfaces) |
| AR Session | Manages the AR lifecycle starts/stops AR tracking |
| Canvas | UI container |
| Button | The Reset button visible on screen |
| EventSystem | Handles UI input events |

---

## Components on XR Origin

These components were added to the XR Origin GameObject:

| Component | Purpose |
|-----------|---------|
| XR Origin (Script) | Built-in, manages the AR camera rig |
| AR Plane Manager | Detects flat real-world surfaces (tables, floors) using the camera |
| AR Raycast Manager | Casts rays from screen touch position to detect where on a surface the user tapped |
| Cube Spawner (Script) | Script pawns chess board when surface is tapped, manages piece positions |
| Piece Grabber (Script) | Script to detects pinch gesture and moves grabbed chess pieces |

---

## Scripts Written

### CubeSpawner.cs
**Location:** `Assets/Task2/Scripts/CubeSpawner.cs`

**What it does:**
- Listens for a screen tap
- Uses `ARRaycastManager` to detect where on a real surface the user tapped
- Spawns the chess board prefab at that position
- Stores the default positions of all chess pieces for the reset function
- `ResetPieces()`: public method called by the Reset button to return all pieces to starting positions

**Important variables:**
- `chessBoardPrefab`: drag the chess board prefab here in Inspector
- `chessPiecePrefabs[]`: array of all 6 chess piece prefabs

---

### PieceGrabber.cs
**Location:** `Assets/Task2/Scripts/PieceGrabber.cs`

**What it does:**
- Detects a two-finger pinch gesture (two touches close together)
- Raycasts from the midpoint between the two fingers
- If the ray hits a chess piece (identified by name containing King/Queen/Rook/Bishop/Knight/Pawn), grabs it
- Moves the grabbed piece with the hand until the pinch is released
- Only grabs pieces, not the board (board name doesn't contain piece keywords)

**Important variables:**
- `grabDistance`: how close fingers need to be to a piece to grab it

---

## Inspector Assignments (XR Origin)

After attaching scripts, these slots were filled in the Inspector:

| Script | Slot | Assigned To |
|--------|------|-------------|
| Cube Spawner | Chess Board Prefab | CHessBoardModel2 prefab |
| Cube Spawner | Chess Piece Prefabs (6) | Bishop, King, Knight, Pawn, Queen, Rook prefabs |

---

## Reset Button Setup

- Created via: GameObject > UI > Button > TextMeshPro
- Button text changed to "Reset"
- On Click event wired to: XR Origin > CubeSpawner > ResetPieces()

---


## Task 2 Completion Status

| Objective | Status |
|-----------|--------|
| Chess board spawns on real surface when tapped | ✅ Done |
| Individual chess pieces can be grabbed by pinch | ✅ Written, untested |
| Pieces can be moved and placed elsewhere | ✅ Written, untested |
| Reset button returns pieces to starting positions | ✅ Done |
| Tested on real device | ❌ Pending |

**Overall: 70% complete. Remaining work is testing and fixing grab interaction on the headset after Albertina configures it.**

**Continuing README to explain AR Mobile trial since Albertina could not get headset to work**

### Moving to AR Mobile template
The original project (that i did using Unity project's 3D template) had a yellow screen issue on Ane's Samsung phone the camera feed wasn't showing because the render wasn't configured for mobile AR. Rather than spending hours fixing the settings, we created a new project using Unity's **AR Mobile template** which has everything configured correctly out of the box (ARCore, URP with AR background renderer, plane detection, etc.).

We copied our `Task2` assets folder into the new project (`ARChess`) and wired up our `chessModel` prefab to the template's OBJECT SPAWNER.

### Building for Android
- Build platform: Android
- Graphics API: OpenGLES3 only (removed Vulkan because it caused a renderPassIndex crash on Ane's Samsung phone when she tried opening the apk file I sent her on Samsung)
- XR Plugin: Google ARCore (not OpenXR)
- Package name: com.group7.archess because the previous name was not letting me build
- We built the APK and sent it via WhatsApp since USB ADB wasn't cooperating when we tried the "Build and Run" option

---

## What works

- AR camera feed showing the real room (no more yellow screen)
- Tap a flat surface → chessboard appears with all 32 pieces in correct starting positions thanks to Ane's ChessSetup.cs script
- White pieces on one side, black on the other, properly coloured, not magenta anymore since Ane helped configure the colouring
- Board and pieces are correctly scaled relative to each other

## What still needs work

- Piece grab/pinch on device needs testing and tuning
- Board scale needs adjusting so the full board fits in the phone screen
- Multiple spawns on tap needs to be locked to one board per session
- Black pieces materials weren't showing correctly at one point. Needs a check.

---
