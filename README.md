# MindPebbles

MindPebbles is a minimalist F# web application built with Giraffe. It delivers short motivational "pebbles" (thoughts) to help you reflect and focus throughout your day.

![Screenshot](screenshots/demo.png)

- Screenshot of the app running in dark mode, showing a motivational pebble and the interactive UI.

## Project Motivation

The goal of this project was to explore functional web development using F#.  
MindPebbles is a simple and user-friendly application that demonstrates:

- Server-side rendering using Giraffe and HTML ViewEngine
- Handling routes, form inputs, and user state
- Managing in-memory state without a database
- Building clean and interactive UIs with minimal JavaScript

This project was created as part of a university assignment to experiment with F# in a creative and meaningful way.

## Features

- Random motivational thoughts on load
- Dark/Light mode toggle
- Save favorite thought (stored client-side)
- View statistics at `/stats`
- API: `/api/all`, `/api/random`

## Live Demo

The MindPebbles application is live and hosted on Azure!

[Open it here](https://mindpebbles-app2025-d6e5gsgmcchhdwd8.northeurope-01.azurewebsites.net)

The live version includes full functionality:

- Random motivational pebbles on each load  
- Dark/Light mode toggle    
- View statistics on `/stats`  
- Public API endpoints (`/api/all`, `/api/random`) 

## How to Run Locally

You need the [.NET 9 SDK](https://dotnet.microsoft.com/) and a terminal.

```bash
dotnet restore
dotnet build
dotnet run