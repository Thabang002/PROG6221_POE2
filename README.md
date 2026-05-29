# CybersecurityChatbot

A WPF-based cybersecurity awareness chatbot built in .NET 10.

## Overview

This project is a Windows desktop application using WPF. It includes:

- a simple chatbot UI in `MainWindow.xaml`
- conversational logic in `Services/ChatbotService.cs`
- user profile and conversation tracking
- optional Windows speech support via `Services/VoiceService.cs`

## Requirements

- Windows 10 / 11
- .NET 10 SDK and Desktop Runtime
- Visual Studio 2022/2023 or `dotnet` CLI

## Build & Run

From the project root:

```bash
dotnet build
dotnet run --project CybersecurityChatbot.csproj
```

> Note: This application targets `net10.0-windows` and requires the Windows desktop runtime. It is not runnable as a WPF app on Linux.

## Project files

- `CybersecurityChatbot.csproj` — project configuration for WPF
- `App.xaml` / `App.xaml.cs` — WPF application startup
- `MainWindow.xaml` / `MainWindow.xaml.cs` — main chat UI
- `Services/ChatbotService.cs` — chatbot response engine
- `Services/VoiceService.cs` — optional Windows speech support
- `Models/ChatMessage.cs`, `Models/UserProfile.cs` — chat model classes

## Notes

If you want to use this repository on Linux, the app must be ported to a cross-platform UI framework such as Avalonia or MAUI. Currently it is only configured as a Windows WPF app.