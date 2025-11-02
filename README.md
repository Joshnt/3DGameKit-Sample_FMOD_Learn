
# 🎵 Unity + FMOD Lessons

## General Concept
Learn Game-Audio in FMOD - without having to bother about integrating/ coding those connections in Unity.
With that, aspiring Game-Audio-Designers can work directly in FMOD and learn/ teach the Middleware FMOD working in a readily build game!
See setup in [this Video](https://youtu.be/BaJWNOkKOzg).



## ⚙️ General Handling
- Open Game Build 
-  Open FMOD Session and Start Live Updates
➡️That's it!

*Some additional notes:*
- **Loop handling:** Events with the addition "Loop" in it are actively stopped by Unity - make sure they loop inside of FMOD
- **Reload scenes**: Reload the scene via the Pause Menu (by pressing escape) when you insert a new Sound on an Event which is a loop OR when you add/ remove a spatializer on an event, so FMOD calculates those correctly.
- **Notes on events**: all events, parameters, snapshots and folders in the FMOD Project have short notes on them to explain when they are triggered  
- **Organize as you like**: Events and Snapshots can be renamed, re-colored and moved in the hierachy to match your personal preference – parameters **cannot!!!!**  

---
![enter image description here](https://learn.unity.com/_next/image?url=https://unity-connect-prd.storage.googleapis.com/20190219/learn/images/e421863d-d0b9-4fdf-9134-2f55c51287e9_Project_and_Tutorials_3D_Game_Kit_1920x1080_Tutorial_2.jpg&w=384&q=75)
---

## 📂 Structure
- Builds, screen captures, and Unity + FMOD session are in **separate repos**  
	- [Unity and FMOD Project](https://github.com/Joshnt/3DGameKit-Sample_FMOD_Learn)
	- [Builds](https://github.com/Joshnt/3DGameKit_Build_FMOD_Learn)
	- [Screencaptures](https://github.com/Joshnt/3DGameKit_Screencaptures)
- FMOD session available in **English and German**  

---

## 🎮 In-Game Options (ESC)
- Restart Level  
- Change Language (relevant only for info dialog)  
- Teleport to end of Level
- Change Level (Level1, Level2, Level with Enemies only, Level limited to start platform of level 1)

---

## 📚 Tags & Lesson Concept
Additionally, I added Tags to all Events, by which you can filter the Events. Those include so called "Lesson-Tags", in which I tried to group the Events with increasing difficulty:
- **Lesson 1** – No fight, no parameters (except for spaceship 3D)  
- **Lesson 2** – Main character fight mechanics, some enemy events with 3D  
- **Lesson 3** – Parameters for steps, hits, landings (surfaces, music, …)  
- **Lesson 4** – Detail work, environmental animations  
- **Lesson 5** – Comparison, snapshots, mixer  

---
## 🎮Re-Exporting the game with your own sounds
To export/ build the game with your own sounds (without having to connect via Live-Update), you have to take two seperate steps:
- 1) In your FMOD Project with all the sounds you have setup, choose File -> Build (F7). Keep in mind, that Unity will now always refer this state of your project for playing back sound until you connect via Live-Update.
- 2) Open the Unity Project in the correct Editor Version **2022.3.51f1** (if you're doing that for the first time, that might take a while). Keep in mind, that you need to [install the Build Support Modules in the Unity Hub](https://docs.unity3d.com/hub/manual/AddModules.html) for the operating systems for which you want to export to (e.g. Windows Build support if you are on Mac). In the Unity Project, press File -> Build Settings... Then select the Target Platform for your game and press Build!

---
## 📦Other FMOD Learning Projects
- [Platformer (Easy)](https://github.com/Joshnt/Platformer_FMOD-Learn)
- [John Lemon (Easy)](https://github.com/Joshnt/JohnLemon_FMOD-Learn) *(Currently WIN only & Unity Version 6)*
- [Doctor FPS (Intermediate)](https://github.com/Joshnt/DoctorFPS_FMOD-Learn)
- 	The Explorer (Advanced)
	- [Unity and FMOD Project](https://github.com/Joshnt/3DGameKit-Sample_FMOD_Learn)
	- [Builds](https://github.com/Joshnt/3DGameKit_Build_FMOD_Learn)
	- [Screencaptures](https://github.com/Joshnt/3DGameKit_Screencaptures)

*All the Demo-Games use FMOD 2.03.09 and Unity 2022.3.51f1*

*(All those games are originally designed and distributed by Unity Technologies as Learning Ressources and can be found on their website under the [Standard Unity Asset Store EULA](https://unity.com/legal/as-terms).)*
