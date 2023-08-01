# trail-botanica
Find hiking trails and plants

# TrialBotanica Collaborative Project

TrialBotanica is an application that provides data on plants along with their designs and trails. This project aims to build a C# WebAPI for TrialBotanica, utilizing Entity Framework Code-First for data modeling and storage.

## Table of Contents

- [Introduction](#trialbotanica-collaborative-project)
- [Table of Contents](#table-of-contents)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Project Structure](#project-structure)
- [Usage](#usage)
  - [API Endpoints](#api-endpoints)
  - [Data Models](#data-models)
  - [Associations](#associations)
- [Contributing](#contributing)
- [License](#license)

## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) (at least version 5.0)
- [Visual Studio](https://visualstudio.microsoft.com/) or any C# IDE of your choice

### Installation

1. Clone the repository to your local machine:

2. Open the project in your C# IDE.

3. Create and apply the initial database migration:


## Project Structure

The project is structured as follows:

- **TrialBotanica.WebAPI:** Contains the WebAPI controllers and startup configurations.
- **TrialBotanica.Data:** Contains the data models and DbContext for Entity Framework.
- **TrialBotanica.Tests:** Contains unit tests for the application.

## Usage

### API Endpoints

The API exposes the following endpoints:

- `GET /api/plants`: List all plants in the database.
- `GET /api/plants/{id}`: Find a specific plant by its ID.
- `POST /api/plants`: Create a new plant in the database.
- `PUT /api/plants/{id}`: Update a plant's information by its ID.
- `DELETE /api/plants/{id}`: Delete a plant by its ID.

- `GET /api/designs`: List all designs in the database.
- `GET /api/designs/{id}`: Find a specific design by its ID.
- `POST /api/designs`: Create a new design in the database.
- `PUT /api/designs/{id}`: Update a design's information by its ID.
- `DELETE /api/designs/{id}`: Delete a design by its ID.

- `GET /api/trails`: List all trails in the database.
- `GET /api/trails/{id}`: Find a specific trail by its ID.
- `POST /api/trails`: Create a new trail in the database.
- `PUT /api/trails/{id}`: Update a trail's information by its ID.
- `DELETE /api/trails/{id}`: Delete a trail by its ID.

- `GET /api/features`: List all features in the database.
- `GET /api/features/{id}`: Find a specific feature by its ID.
- `POST /api/features`: Create a new feature in the database.
- `PUT /api/features/{id}`: Update a feature's information by its ID.
- `DELETE /api/features/{id}`: Delete a feature by its ID.

### Data Models

The data models are defined in the `TrialBotanica.Data` project. These models represent the database tables and include navigation properties to handle associations between tables.

### Associations

The following associations are defined between the tables:

- "Plants" and "Designs" have a one-to-many relationship.
- "Plants" and "Trails" have a one-to-many relationship.
- "Designs" and "Trails" have a one-to-many relationship.
- "Features" and "Trails" have a one-to-many relationship.

## Contributing

We welcome contributions to this project! To contribute, please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature/bugfix.
3. Commit your changes and push to your fork.
4. Create a pull request to the main repository.

Please ensure that your code follows the coding standards and includes appropriate unit tests.

## License

This project is licensed under the [MIT License](LICENSE).

---

Happy coding and exploring the botanical world with TrialBotanica!
