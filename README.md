# XR Components : XR Lab

## 🎥 Gameplay Video
[Watch Demo Video]()

---

## 🥽 XR Components : XR Lab
An interactive XR application built in Unity for VR/MR model exploration and advanced visualization.  
Designed for **Meta Oculus Quest 3**, this project lets users grab, inspect, and explore 3D models 
through five distinct interaction modes — including X-ray, cut plane visualization, and real-world passthrough.

Built around a **modular scriptable object system**, adding or swapping models requires minimal setup, 
making the architecture clean, reusable, and easy to extend.

---

## ⚙️ Technical Highlights
- Engine: Unity 6 (6000.2.9f1)
- Platform: Meta Oculus Quest 3 (Android APK)
- Programming Language: C#
- Modular scriptable object-driven model for animation configuration, spawn point offset, giant size option, model prefab
- Custom cut plane shader for internal cross-section visualization
- X-ray transparency materials
- MR passthrough toggle via headset API
- Custom VR UI for interaction controls
- Per-scene skybox environments
- Supports both VR controllers and hand tracking input

---

## 🗂️ Structure

### 🏠 Main Menu
- A dedicated main menu scene for selecting which model to explore
- Each model launches its own dedicated scene

### 🌄 3 Model Scenes
Three independent scenes, one per model:

- **Revolver**
- **Fantasy Enemy**
- **Mech Robot**

Each scene has its own **skybox** for a distinct visual atmosphere in VR mode, 
adding polish and a sense of environment to the experience.

---

## 🎯 Five Interaction Modes

### 1. 🤲 Free Mode
- Enables the box collider on the model
- Grab, rotate, and resize the model freely using VR controllers or Hands Tracking

### 2. 🔭 X-Ray Mode
- Swaps the model's material to a custom transparent materials
- Visualizes internal structure through colored transparency, like looking through tinted glass

### 3. ✂️ Cut Mode
- Uses a custom shader and special scripts for updating cut panel position's parameters
- A moveable cut panel defines a clip plane on the model
- Any part of the model beyond that panel becomes invisible, revealing a clean cross-section

### 4. 🌍 Real World View
- Toggles Meta Quest 3 passthrough mode
- The player can see the real world and interact with the models
- Seamlessly switches between full VR and MR overlay

### 5. 🔭 Giant Size Mode
- Configured per model via scriptable object settings
- When enabled, the giant size button becomes interactable. When disabled, the button remains visible but cannot be used
- On activation, the model moves away and scales up dramatically
- Especially satisfying to watch alongside animations

---

## 🎬 Animation Control
- Animation options are populated automatically from the scriptable object configuration
- Labels are mapped to animation clips via dictionary at runtime
- Select any available animation from the VR UI panel
- No manual UI wiring needed when adding new animations — just update the scriptable object

---

## 📋 Guide Panel
- An in-scene guide panel explains how to use all interactions
- Can be opened and closed freely during the session

---

## 🧩 Modular Setup
The project is driven by a custom scriptable object system:
- Set the target model and its animations in the scriptable object
- The application reads the configuration at startup and applies it automatically
- Adding a new model requires minimal scene changes
- Animation controller titles are assigned dynamically via dictionary mapping

---

## 🧠 Project Focus
- Modular, data-driven XR architecture
- Custom shader development for cut plane and X-ray visualization
- MR passthrough integration on Meta Quest 3
- Practical design applicable to medical visualization, engineering inspection, and education
