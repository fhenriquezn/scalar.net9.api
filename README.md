# API Key Authentication and Scalar Implementation in .NET 9 and more

This project demonstrates how to implement API Key authentication, modify endpoint metadata, utilize Scalar in .NET 9, and versioning. Hope you find this useful.



## Technologies Used

**.NET 9**

**Visual Studio 2022 (version 17.12.0)**

**Scalar.AspNetCore 1.2.39**

**APIWeaver.OpenApi**


## Features

- API Key authentication
- [Endpoint metadata modification](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/include-metadata?view=aspnetcore-9.0&tabs=minimal-apis)
- API versioning
- [Scalar implementation](https://github.com/scalar/scalar/tree/main/packages/scalar.aspnetcore)
## Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
## API Versions Documentation

- /scalar/v1
- /scalar/v2


---

## New: .NET 10 Implementation

A .NET 10 implementation has been added alongside the existing .NET 9 project. The .NET 9 implementation above is left unchanged. The .NET 10 project lives in the `scalar.net10.api` folder and demonstrates the same patterns (API Key authentication, OpenAPI metadata modification, Scalar UI) updated for .NET 10.

Technologies Used (for .NET 10)
- **.NET 10**
- **Visual Studio 2026**
- **Scalar.AspNetCore** (used in `scalar.net10.api`)

Features
- API Key authentication (same approach as .NET 9, updated for the .NET 10 project)
- Endpoint metadata modification for OpenAPI
- API versioning with versioned endpoints
- Scalar UI integration and configuration

Requirements (for .NET 10)
- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- [Visual Studio 2026](https://visualstudio.microsoft.com/) (or use the SDK with CLI)

Quick start for the .NET 10 project
- Build:
  - `dotnet build ./scalar.net10.api`
- Run:
  - `dotnet run --project ./scalar.net10.api`
- Or open the solution/project in __Visual Studio 2026__ and run/debug from the IDE.

Notes
- Both implementations expose API documentation for the same versioned endpoints (for example `/scalar/#api-v1` and `/scalar/#api-v2`). The .NET 10 project follows the same routing/versioning conventions and adds any project-level updates required for .NET 10 and C# 13.