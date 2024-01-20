# Store Microservices

A system of microservices communicating asynchronously with RabbitMQ. This project was built using ASP.NET and MongoDB. 

## Catalog Service

The catalog service is a REST API that supports basic CRUD operations on catalog items. This service publishes an event to RabbitMQ when catalog items are modified.

## Inventory Service

The inventory service is a REST API that manages peoples' bought items. To get updated references of catalog items, it consumes messages from RabbitMQ and stores a compressed version of the catalog item in its own database.

## Common Package

This project introduces a common nuget package that enables the one-line setup of MongoDB repositories and RabbitMQ.
