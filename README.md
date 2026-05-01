Interactive Book Reading Application

This is my university capstone project — a visual novel-style interactive reading app built in Unity. The idea came from wanting to make classic literature more engaging, especially for people who struggle to get through dense texts.

We picked *The Apology of Socrates* as the first book since it's public domain and works well in a dialogue format. I handled all the coding side of the project while my teammates worked on music selection and visual design.

What It Does

- Users read through a book in a visual novel format — click to advance, characters appear with portraits and background scenes
- Each scene has matching background music that transitions automatically
- Progress is saved so users can pick up where they left off
- A quiz at the end tests comprehension and awards a badge based on score
- Pause menu with volume control and mute option
- Support for multiple books (Socrates + Three Musketeers scenes implemented)

---

Tech Stack

- Unity 6.0** — game engine
- C#** — all scripting
- JSON — dialogue data storage and management
- TextMeshPro** — UI text rendering
- Unity SceneManagement** — scene transitions
- PlayerPrefs** — save/load progress

Scripts Overview

| Script | What it does |
|---|---|
| `DialogueManager.cs` | Core script — reads JSON, renders dialogue, handles portraits and backgrounds |
| `AudioManager.cs` | Singleton audio controller — handles scene-specific music playlists and transitions |
| `GameManager.cs` | Singleton state manager — tracks selected book and saved progress across scenes |
| `BookSelectionManager.cs` | Handles book selection, continue game, and quiz navigation |
| `SaveManager.cs` | Saves and loads dialogue index using PlayerPrefs |
| `QuizManager.cs` | Loads quiz questions from JSON, handles answer selection and scoring |
| `PauseMenu.cs` | Pause/resume, volume slider, mute toggle, save and quit |
| `BackgroundLibrary.cs` | Maps background names from JSON to Unity sprites |
| `DialogueLine.cs` | Data classes for deserializing dialogue JSON |
| `JsonHelper.cs` | Helper for parsing JSON arrays into Unity-compatible objects |
| `PageTurner.cs` | Handles page turn animations |
| `MainMenuManager.cs` | Main menu scene controller |

Architecture Notes

All dialogue content is stored in JSON files under `StreamingAssets/` — this keeps content completely separate from code, making it easy to add new books without touching any scripts.

The `DialogueManager` supports two JSON formats (original and alternate structure) with a fallback parser, so different books can have slightly different data shapes.

`AudioManager` and `GameManager` both use the Singleton pattern with `DontDestroyOnLoad` to persist across scene changes.

My Role

I was responsible for the entire software side of the project:
- Designed and implemented all C# scripts from scratch
- Built the JSON data pipeline for dialogue management
- Implemented the save/load system, quiz engine, audio management and pause menu
- Debugged NullReferenceException and JSON loading errors throughout development

My teammates handled music curation and visual/UI design.

What I'd Add Next

- More books
- Bookmarks and annotation system
- A proper save slot system instead of single-slot PlayerPrefs
- Replacing copyrighted music with original compositions
