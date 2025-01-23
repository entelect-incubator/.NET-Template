# Vertical Slice Minimal Template

[![Vertical Slice Minimal Architecture](https://github.com/entelect-incubator/.NET-Template/actions/workflows/min-vertical-slice.yml/badge.svg)](https://github.com/entelect-incubator/.NET-Template/actions/workflows/min-vertical-slice.yml)

<br/>

Vertical Slice Minimal Architecture, often associated with Domain-Driven Design and microservices, advocates for building end-to-end slices of functionality across all layers of the application stack. Unlike traditional horizontal layering, this approach emphasizes delivering a full, working feature set through the entire technology stack. Vertical Slice Architecture fosters a holistic understanding of business features, encourages cross-functional collaboration, and accelerates iterative development by providing a complete user experience with each implemented slice. This methodology promotes rapid feedback loops, enabling teams to iterate quickly and deliver value incrementally.

## Technologies

The solution template incorporates the following technologies:

-   Latest .NET
-   Latest Entity Framework Core
-   MediatR
-   FeatureManagement
-   Rate limiting
-   FluentValidation
-   Polly
-   NSwag
-   Endpoints
-   Serilog
-   NUnit

## Getting Started

### **Apis**

This layer is dedicated to hosting Apis or other ASP.NET Core projects. It depends on the Core Layer.

### **Core**

This layer encompasses domain objects, business logic, specific feature-related command and query operations, and data access, thereby providing a comprehensive vertical slice for the specified feature.

### **Common**

The Common layer contains extensions, middleware, helpers, logging, and types that are shared across different layers of the application. It promotes code reuse and consistency. This also can be moved to a Nuget Package for reuse.

### **Tests**

The Tests layer contains all the unit tests for the application. It ensures the correctness and reliability of the codebase through automated testing.

Also have included Benchmark testing and Load Testing using K6.io under the `LoadTesting` folder.

[View Test Coverage Report](https://entelect-incubator.github.io/.NET-Template/)
