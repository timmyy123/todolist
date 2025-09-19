# TODO List App

A simple TODO list application with Angular frontend and .NET Web API backend.

## Prerequisites

- Node.js (latest)
- .NET 10 SDK
- npm

## Backend Setup

```bash
cd backend/backend
dotnet restore
dotnet dev-certs https --trust
dotnet run
```

Backend runs on:
- HTTP: http://localhost:5235
- HTTPS: https://localhost:7175

## Frontend Setup

```bash
cd frontend
npm install
ng serve
```

Frontend runs on: http://localhost:4200

## Usage

1. Start backend first
2. Start frontend
3. Open http://localhost:4200
4. Add, view, and delete todos

## API Endpoints

- GET /api/todos - List all todos
- POST /api/todos - Create todo
- DELETE /api/todos/{id} - Delete todo