# ASP.NET Core Microservices Study Project

## Overview

This repository is a study project for building a modern microservices architecture using ASP.NET Core. It demonstrates how to develop, containerize, and integrate multiple services with a mix of REST APIs, gRPC, message-based communication, and API gateway patterns.

## Description

This study project is a comprehensive microservices example built on the .NET platform. It is inspired by a community-verified repository and includes e-commerce modules for Catalog, Basket, Discount, Ordering, and API Gateway services.

The solution demonstrates how to build microservices using ASP.NET Core Web API, Docker, RabbitMQ, MassTransit, gRPC, Ocelot API Gateway, MongoDB, Redis, PostgreSQL, SQL Server, Dapper, Entity Framework Core, CQRS, and Clean Architecture.

Explore NoSQL databases (MongoDB, Redis) and relational databases (PostgreSQL, SQL Server), with inter-service communication through gRPC and RabbitMQ event-driven messaging. The project also includes a web UI with Razor pages and Bootstrap and a fully containerized environment using Docker Compose.

## Microservices and features

### Catalog microservice

- ASP.NET Core Web API application
- REST API principles and CRUD operations
- MongoDB database connection and containerization
- Repository pattern implementation
- Swagger OpenAPI implementation

### Basket microservice

- ASP.NET Core Web API application
- REST API principles and CRUD operations
- Redis database connection and containerization
- Consuming Discount gRPC service for inter-service sync pricing
- Publishing BasketCheckout queue with MassTransit and RabbitMQ

### Discount microservice

- ASP.NET gRPC server application
- High-performance inter-service gRPC communication with Basket microservice
- Exposing gRPC services using Protobuf messages
- Dapper micro-ORM implementation for high performance data access
- PostgreSQL database connection and containerization

### Ordering microservice

- Implementing DDD, CQRS, and Clean Architecture using best practices
- Developing CQRS with MediatR, FluentValidation, and AutoMapper
- Consuming RabbitMQ BasketCheckout event queue with MassTransit
- SQL Server database connection and containerization
- Entity Framework Core ORM with auto migration on startup

### API Gateway (Ocelot)

- API Gateway implementation with Ocelot
- Sample microservices rerouted through the API Gateway
- Gateway aggregation pattern in Shopping.Aggregator
- Calling Ocelot APIs with HttpClientFactory

### Web UI ShoppingApp microservice

- ASP.NET Core web application with Bootstrap 4 and Razor templates
- Using view components, partial views, tag helpers, model binding, validation, and Razor sections
- Calling backend services through Ocelot API Gateway

### Ancillary containers

- Portainer for lightweight Docker management UI
- pgAdmin for PostgreSQL administration and development
- Docker Compose setup for all microservices and databases
- Containerization of microservices and databases
- Environment variable overrides

## Docker and containerization

The solution includes Docker Compose files to build and run all microservices, databases, and supporting tools in containers. This makes it easy to run the entire system locally with consistent environments.

## Notes

This project is intended for study and learning purposes, focused on microservices architecture, integration patterns, and modern .NET development practices.