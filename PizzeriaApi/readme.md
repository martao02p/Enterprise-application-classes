# Pizzeria App

A full-stack web application for managing a pizza restaurant – including pizzas, ingredients, pricing, and customization templates.

## Technologies Used

### Backend – .NET 8 (ASP.NET Web API)
- Entity Framework Core + SQLite
- Clean separation of `Pizza`, `Ingredient`, and their many-to-many relationship
- System-defined vs editable ingredients
- Validations (e.g., each pizza must include sauce and cheese)
- RESTful API (`/api/Pizzas`, `/api/Ingredients`)
- Cross-Origin Resource Sharing (CORS) enabled

### Frontend – Angular
- Standalone components (no NgModules)
- Angular Material UI with custom pizza-themed styling
- Forms with validation and custom styles
- Dialog-based modals for creating, editing, and deleting
- Responsive design
- Dynamic templates (Margherita, Pepperoni, Funghi)
- Ingredient filtering, rename, deletion with backend validation

## Features

### Pizzas
- View full pizza list with filtering
- Create new pizza using template or from scratch
- Edit existing pizza
- Delete pizza
- System ingredients (e.g., cheese, sauce) are always included

### Ingredients
- View full ingredient list
- Add new ingredients
- Rename ingredients
- Delete ingredients (only if not in use)
- System ingredients cannot be edited or removed

## Setup

### Backend
```bash
cd PizzeriaApi
dotnet restore
dotnet ef database update
dotnet run
```

## Film


https://github.com/user-attachments/assets/578b2e4f-9d9c-4625-bd09-db321299c581



