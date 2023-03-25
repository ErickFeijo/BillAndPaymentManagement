# Bill and Payment Management

This is a microservice project to facilitate my personal control of payment accounts. It uses .NET 7.0, RabbitMQ, MySQL Database, Docker Compose.

## Frameworks

- [Microsoft.EntityFrameworkCore](https://docs.microsoft.com/en-us/ef/core/)
- [Swashbuckle.AspNetCore (Swagger)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [RabbitMQ.Client](https://www.rabbitmq.com/dotnet-api-guide.html)
- [Google.Apis.Gmail.v1](https://developers.google.com/gmail/api)

## MVP Todo List

- [ ] Work better in this Readme file :)
- [ ] Separete the projects into different repositories
- [ ] Finish basic implementation of SeekService (Implement the EmailBillReaders and test GmailApi)
- [ ] Finish basic implementation of RegisterServiceAPI (Implement subscribe to queue on RabbitMQ to get and sabe new items)
- [ ] Improve error handling and logging

## Getting Started

To run this project, you will need to have the following tools installed:

- Docker
- .NET 7.0

Once you have those tools installed, follow these steps:

1. Clone this repository: `git clone https://github.com/ErickFeijo/BillAndPaymentManagement.git`
2. Navigate to the project directory: `cd BillAndPaymentManagement`
3. Run the Docker Compose command: `docker-compose up`
4. Open your web browser and go to Swagger Page

You should now see the Swagger UI, which provides documentation and a user interface for testing the API.
