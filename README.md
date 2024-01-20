# Store Microservices

A system of microservices communicating asynchronously with RabbitMQ. This project was built using ASP.NET and MongoDB. 

## Catalog Service

The catalog service is a REST API that supports basic CRUD operations on catalog items. This service publishes an event to RabbitMQ on modification of catalog items.

## Inventory Service

The inventory service is a REST API that manages peoples' bought items. To get updated references of catalog items, it consumes messages from RabbitMQ and stores a compressed version of the catalog item in its own database.
