# RabbitMQ Microservices Solution

This repository contains a microservices solution that demonstrates the use of RabbitMQ as an event bus for communication between services.

## Projects Overview

- **EventBus:**  
  Handles the RabbitMQ setup and provides a generic event bus interface with publish/subscribe functionality.

- **ServiceA:**  
  A publisher service that sends numeric messages and publishes events to RabbitMQ.

- **ServiceB:**  
  A consumer service that listens for messages and processes them.

## RabbitMQ Components and Their Roles

### Producer
- **Role:**  
  The service that sends messages to RabbitMQ.
- **Details:**  
  Produces messages and pushes them to an exchange.
- **Example:**  
  A JobPostService creates a job post and publishes a message.

### Exchange
- **Role:**  
  A routing mechanism that determines how messages are delivered to queues.
- **Types of Exchanges:**
  - **Direct:** Routes messages based on a specific key.
  - **Topic:** Routes messages based on a pattern.
  - **Fanout:** Broadcasts messages to all bound queues.
  - **Headers:** Routes messages based on headers.

### Queue
- **Role:**  
  A buffer where messages wait to be processed.
- **Details:**  
  Messages stay in the queue until a consumer retrieves and acknowledges them.

### Binding
- **Role:**  
  The link between an exchange and a queue.
- **Details:**  
  Specifies which messages should be routed to which queues.

### Consumer
- **Role:**  
  The service that receives messages from a queue.
- **Details:**  
  Processes messages and acknowledges them.

### Acknowledgment (ACK/NACK)
- **Role:**  
  Confirms message processing.
- **Details:**  
  - After consuming a message, the consumer sends an ACK if processed successfully.
  - If an error occurs, the consumer can send a NACK, causing the message to be re-queued or moved to a Dead Letter Queue.

### Dead Letter Queue (DLQ)
- **Role:**  
  A queue where failed messages are stored.
- **Purpose:**  
  Used for debugging and retrying message processing.


