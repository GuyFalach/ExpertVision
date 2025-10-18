# ExpertVision

**ExpertVision** is a VR Interactive Smart Class, where both the student and the teacher can join together to explore and learn about server hardware in an immersive environment.

---

## Overview

The goal of ExpertVision is to assist teachers in explaining technical material and to help students better understand theoretical concepts through realistic, hands-on virtual interaction.  
It aims to make the learning process more engaging, allowing students to “touch” and explore server components safely and intuitively.

**Target Audience:**  
Server technicians, IT students, and anyone interested in understanding how servers look and function.

---

## Features

- **Interactive Server Model** – Open server doors for a better view, grab and inspect internal components.  
- **Scalable Parts** – Scale up or down individual parts for detailed examination.  
- **Informational Overlays** – Access information about specific components.  
- **Educational Video Integration** – Watch videos explaining server concepts.  
- **Smart Class Connectivity** – Join a shared VR classroom with another user, where the teacher can observe and guide the student’s actions in real-time.  
- **Full Interaction Support** – All interactions built using the Meta All-In-One SDK.  

**Experience Type:** Standing, room-scale VR.

---

## Technical Details

| Setting | Value |
|----------|-------|
| **Unity Version** | 2022.3 |
| **Render Pipeline** | Built-in |
| **Target Platform** | Oculus Quest 2 (Android) |
| **SDKs Used** | Meta All-In-One SDK (formerly Oculus SDK), Avatar SDK |
| **Unity Packages** | Oculus XR Plugin, Meta XR Interaction, TextMeshPro |

---

## Project Structure

- **Scenes/**
  - `Room.unity` – Main scene containing the Smart Class environment.
- **Assets/**
  - Project assets, models, and interactable components.
- **Scripts/**
  - Logic for interactivity and connectivity (via Meta SDK).

---

## How to Build and Run

1. **Open the project in Unity (2022.3)**  
2. Go to **File → Build Settings**  
3. **Switch platform** to **Android**  
4. Make sure your **Oculus Quest 2** is selected under **Run Device**  
5. In **Project Settings → XR Plug-in Management**, enable **XR Plugin** and check **Oculus** under **Plug-in Providers**  
6. **Build the APK** and install it on your Oculus Quest 2 headset  

---

## Dependencies

- [Meta All-In-One SDK](https://developers.meta.com/horizon/downloads/package/meta-xr-sdk-all-in-one-upm/)
- [Meta Avatar SDK](https://developers.meta.com/horizon/downloads/package/meta-avatars-sdk/)
- [Oculus XR Plugin](https://docs.unity3d.com/Packages/com.unity.xr.oculus@latest/)
- [TextMeshPro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@latest/)

---

## Author

Developed by **Guy Falach**  
*Solo developer and creator of ExpertVision.*

---
