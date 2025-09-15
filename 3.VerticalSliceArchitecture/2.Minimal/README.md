
# Vertical Slice Minimal Template

[![Vertical Slice Minimal Architecture](https://github.com/entelect-incubator/.NET-Template/actions/workflows/min-vertical-slice.yml/badge.svg)](https://github.com/entelect-incubator/.NET-Template/actions/workflows/min-vertical-slice.yml)

---

This template provides a minimal, production-ready starting point for building .NET applications using the Vertical Slice Architecture. It is designed for rapid feature delivery, clear separation of concerns, and easy extensibility. Ideal for teams adopting Domain-Driven Design, microservices, or simply seeking a modern, maintainable .NET backend foundation.

## What is Vertical Slice Architecture?

Vertical Slice Architecture advocates building end-to-end slices of functionality across all layers of the application stack. Each feature is implemented as a complete, independent unit, rather than splitting code into horizontal layers (e.g., controllers, services, repositories). This approach:

- Delivers working features quickly
- Encourages cross-functional collaboration
- Reduces integration pain
- Enables rapid feedback and iteration

## Technologies Used

- .NET (latest)
- Entity Framework Core (latest)
- MediatR
- FeatureManagement
- Rate limiting
- FluentValidation
- Polly
- NSwag
- Endpoint routing
- Serilog
- NUnit

## Solution Structure

- **Api/**: Minimal ASP.NET Core API project. Hosts endpoints and composes features. Depends on Core.
- **Core/**: Domain objects, business logic, feature commands/queries, and data access. Each feature is a vertical slice.
- **Common/**: Shared extensions, middleware, helpers, logging, and types. Can be extracted to a NuGet package.
- **Test/**: Unit and integration tests for the application.
- **Benchmark.Testing/**: Performance benchmarks for critical code paths.
- **LoadTesting/**: Load testing scripts and instructions (see [LoadTesting/README.md](./LoadTesting/README.md)).

## Getting Started

1. **Install Prerequisites**
	- [.NET SDK (latest)](https://dotnet.microsoft.com/download)
	- [Node.js](https://nodejs.org/) (for some tooling, if needed)
	- [K6](https://k6.io/docs/get-started/installation/) (for load testing)

2. **Clone the Repository**

```sh
git clone https://github.com/entelect-incubator/.NET-Template.git
cd .NET-Template/3.VerticalSliceArchitecture/2.Minimal
```

1. **Restore and Build**

```sh
dotnet restore
dotnet build
```

1. **Run the API**

```sh
dotnet run --project Api/Api.csproj
```

1. **Run Tests**

```sh
dotnet test
```

1. **Run Benchmarks**

```sh
dotnet run --project Benchmark.Testing/Benchmark.Testing.csproj
```

1. **Run Load Tests**
	See [LoadTesting/README.md](./LoadTesting/README.md) for details.

## Customization & Usage

- Add new features by creating a folder in `Core/` and implementing your command/query, handler, and related logic. Register endpoints in `Api/Endpoints/`.
- Use MediatR for in-process messaging and CQRS patterns.
- Configure logging, validation, and middleware in `Common/` and `Api/Startup.cs`.
- Update `appsettings.json` for environment-specific configuration.

## Contributing

Contributions are welcome! Please see [CONTRIBUTING.md](../../CONTRIBUTING.md) and [CODE_OF_CONDUCT.md](../../CODE_OF_CONDUCT.md).

## Resources

- [Vertical Slice Architecture (Jimmy Bogard)](https://jimmybogard.com/vertical-slice-architecture/)
- [CQRS Pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [Test Coverage Report](https://entelect-incubator.github.io/.NET-Template/)

---

_This template is maintained by [Entelect Incubator](https://github.com/entelect-incubator)._
