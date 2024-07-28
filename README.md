
# Event Bus Repository

This repository demonstrates the implementation of an Event Bus system in Unity, focusing on avoiding the Singleton pattern and integrating with both MonoBehaviour and ECS (Entity Component System) classes.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Example Use Case](#example-use-case)
- [Contributing](#contributing)
- [License](#license)

## Introduction

This project aims to showcase a robust and flexible Event Bus system that can be used in Unity projects. The Event Bus pattern helps to decouple event senders and receivers, making the system more modular and maintainable. This repository includes examples of using the Event Bus with both traditional MonoBehaviour classes and Unity's ECS framework.

## Features

- Decouples event producers and consumers.
- Supports both MonoBehaviour and ECS systems.
- Avoids the Singleton pattern, promoting better design practices.
- Easy to extend and integrate into existing projects.

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/berkterek/event_bus_repo.git
   ```

2. Open the project in Unity.

3. Ensure you have the necessary packages installed, such as the Entities package for ECS.

## Usage

### MonoBehaviour Example

1. Create an event by inheriting from the `IEvent` interface.
2. Use the `EventBus` class to register listeners and raise events.

### ECS Example

1. Define events as structs implementing the `IEvent` interface.
2. Use ECS systems to raise and handle events via the Event Bus.

## Example Use Case

### Scenario

In this example, the Game Manager raises events that are listened to by both MonoBehaviour and ECS classes. The `PlayerController` class (a MonoBehaviour) and `StateListenerSystem` (an ECS system) listen for these events, while the `StateChangerSystem` (another ECS system) acts upon these events.

For a detailed walkthrough, please refer to the following [YouTube tutorial](https://youtu.be/He66_RMcYWE).

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository.
2. Create your feature branch (`git checkout -b feature/AmazingFeature`).
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`).
4. Push to the branch (`git push origin feature/AmazingFeature`).
5. Open a Pull Request.

## License

Distributed under the MIT License. See `LICENSE` for more information.
