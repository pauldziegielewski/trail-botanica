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
- [Special Mentions](#special-mentions)
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

- `GET /api/plantdata/listplants`: List all plants in the database.
- `GET /api/plantdata/findplant/{id}`: Find a specific plant by its ID.
- `POST /api/plantdata/addplant`: Create a new plant in the database.
- `PUT /api/plantdata/updateplant{id}`: Update a plant's information by its ID.
- `DELETE /api/plantdata/deleteplant/{id}`: Delete a plant by its ID.

- `GET /api/designdata/listdesigns`: List all designs in the database.
- `GET /api/designdata/finddesign/{id}`: Find a specific design by its ID.
- `POST /api/designdata/adddesign`: Create a new design in the database.
- `PUT /api/designdata/updatedesign/{id}`: Update a design's information by its ID.
- `DELETE /api/designdata/deletedesign/{id}`: Delete a design by its ID.

- `GET /api/traildata/listtrails`: List all trails in the database.
- `GET /api/traildata/findtrail/{id}`: Find a specific trail by its ID.
- `POST /api/traildata/addtrail`: Create a new trail in the database.
- `PUT /api/traildata/{updatetrail/id}`: Update a trail's information by its ID.
- `DELETE /api/traildata/deletetrail/{id}`: Delete a trail by its ID.

- `GET /api/featuredata/listfeatures`: List all features in the database.
- `GET /api/featuredata/findfeature/{id}`: Find a specific feature by its ID.
- `POST /api/featuredata/addfeature`: Create a new feature in the database.
- `PUT /api/featuredata/updatefeature/{id}`: Update a feature's information by its ID.
- `DELETE /api/featuredata/deletefeature/{id}`: Delete a feature by its ID.

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

## Special Mentions
We would like to thank Dhaval Shah, our classmate for helping us with issues whenever we had to fix. We would also like to thank Christine Bittle, our course co-ordinator for guiding and giving us feedback on building up this project.

## License

This project is licensed under the [MIT License](LICENSE).

---

Happy coding and exploring the botanical world with TrialBotanica!
