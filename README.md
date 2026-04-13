# XR Components : XR Lab

## 🎥 Gameplay Video
[Watch Demo Video](https://youtu.be/sNB1mZZ82wo)

---

## 🥽 XR Components : XR Lab
A VR/MR model exploration tool built for **Meta Oculus Quest 3**. Five interaction modes — including custom cut plane cross section shader and X-ray (transparent materials) — let users examine 3D models in ways standard viewers can't offer. Built on a **modular scriptable object system** for clean, extendable architecture.

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

<img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/308787c5-3cb6-409e-836c-cf1c00829eda" />

### 🌄 3 Model Scenes
Three independent scenes, one per model:

- **Revolver**
<br><img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/75efc2cd-c793-4a7e-87d8-9de05fc2cff3" />

- **Fantasy Enemy**
<br><img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/61c4f8fa-1576-4211-91a0-dffa188f8b76" />

- **Mech Robot**
<br><img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/f08c60e5-1cb1-42f8-8547-c68d03835c03" />

Each scene has its own **skybox** for a distinct visual atmosphere in VR mode, 
adding polish and a sense of environment to the experience.

---

## 🎯 Five Interaction Modes

### 1. 🤲 Free Mode
- Enables the box collider on the model
- Grab, rotate, and resize the model freely
<br><img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/5cc62d2d-14d2-4ed2-943b-d6e65063c6af" />

- Supports both VR controllers and hand tracking
<br><img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/b561e032-e1d5-4a9c-ad3a-981c01ad53fd" />

### 2. 🩻 X-Ray Mode
- Swaps the model's material to a custom transparent material
- Visualizes internal structure through colored transparency, like looking through tinted glass

<img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/c209e3cb-a7c1-4103-b1e2-ecf86def9dc5" />

### 3. ✂️ Cut Mode
- Uses a custom shader and special scripts for updating cut panel position's parameters
- A moveable cut panel defines a clip plane on the model
- Any part of the model beyond that panel becomes invisible, revealing a clean cross-section

<img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/72c05038-809b-4581-9394-dd9901c3254e" />

### 4. 🌍 Real World View
- Toggles Meta Quest 3 passthrough mode
- The player can see the real world and interact with the models
- Seamlessly switches between full VR and MR overlay

<img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/b66669a8-5d57-41ed-8f54-99ae7bdc4da6" />

### 5. 🐲 Giant Size Mode
- Configured per model via scriptable object settings
- When enabled, the giant size button becomes interactable. When disabled, the button remains visible but cannot be used
- On activation, the model moves away and scales up dramatically
- Especially satisfying to watch alongside animations

<img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/0659ddd7-8df8-47db-8140-8b9edb03f30c" />

---

## 🎬 Animation Control
- Animation options are populated automatically from the scriptable object configuration
- Labels are mapped to animation clips via dictionary at runtime
- Select any available animation from the VR UI panel
- No manual UI wiring needed when adding new animations — just update the scriptable object

<img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/7fc34584-3712-4c00-bb5e-3666103d1aa1" />

---

## 📋 Guide Panel
- An in-scene guide panel explains how to use all interactions
- Can be opened and closed freely during the session

<img width="427" height="240" alt="image" src="https://github.com/user-attachments/assets/9641770d-3422-4e0b-9bb3-a9c259859a84" />

---

## 🧩 Modular Setup
The project is driven by a custom scriptable object system:
- Set the target model and its animations in the scriptable object
- The application reads the configuration at startup and applies it automatically
- Adding a new model requires minimal scene changes
- Animation controller titles are assigned dynamically via dictionary mapping

<img width="365" height="531" alt="image" src="https://github.com/user-attachments/assets/c24ef8e3-6387-4c7e-a498-5a28e33dd1f9" />
<br>
<img width="367" height="203" alt="image" src="https://github.com/user-attachments/assets/df3f378c-9953-4ca2-ac4f-0380f0af4dc9" />

---

## 🧠 Project Focus
- Modular, data-driven XR architecture
- Custom shader development for cut plane cross section visualization
- MR passthrough integration on Meta Quest 3
- Practical design applicable to medical visualization 💊, engineering inspection 🚀, and education 🎓

---

## 🎨 Assets & Credits
- [Fantasy Enemy Model](https://assetstore.unity.com/packages/3d/characters/robots/wooden-robots-sp-316970) by DJINGAREY
- [Giant Mech Model](https://assetstore.unity.com/packages/3d/characters/robots/stylized-sci-fi-mech-robot-asset-355418#description) by RetroStyle Games
- [Revolver Model](https://assetstore.unity.com/packages/3d/props/guns/reichsrevolver-m-1879-63609) by Nikolay Nagornov
- [Sky Boxes](https://assetstore.unity.com/packages/2d/textures-materials/sky/surreal-skyboxes-254634) by igor-lir
