# Slidely-T2-DesktopApp-Backend
This public repo is of backend implementation on the Frontend Desktop App, it consider GET and POST methods and backend written in Typescript, GET method is used to capture the data.

> [!IMPORTANT]
> node_modules directory is not uploaded due to too many files.

## BACKEND SERVER CONFIGURATION

# Express TypeScript Backend Server

This is a simple backend server built using Express and TypeScript. It includes three endpoints: `/ping`, `/submit`, and `/read`.

## Table of Contents

- [Requirements](#requirements)
- [Installation](#installation)
- [Running the Server](#running-the-server)
- [Endpoints](#endpoints)
  - [`/ping`](#ping)
  - [`/submit`](#submit)
  - [`/read`](#read)

## Requirements

- Node.js (version 14.x or later)
- npm (version 6.x or later) or yarn (version 1.x or later)

## Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/your-username/express-typescript-server.git
    cd express-typescript-server
    ```

2. Install the dependencies:
    ```sh
    npm install
    ```
    Or if you prefer yarn:
    ```sh
    yarn install
    ```

## Running the Server

1. Compile the TypeScript code to JavaScript:
    ```sh
    npm run build
    ```
    Or if you prefer yarn:
    ```sh
    yarn build
    ```

2. Start the server:
    ```sh
    npm start
    ```
    Or if you prefer yarn:
    ```sh
    yarn start
    ```

The server should now be running at `http://localhost:3000`.

## Endpoints

### `/ping`

- **Method**: GET
- **Description**: A simple endpoint to check if the server is running.
- **Response**: 
  ```json
  {
    "message": "pong"
  }


### `/submit`

- **Method**: POST
- **Description**: Accepts data submission.
- **Request Body**: 
  ```json
  {
    "data": "string"
  }
- **Response**:
  ```json
  {
  "status": "success",
  "message": "Data submitted successfully"
  }

### `/read`
- **Method**: GET
- **Description**: Reads the submitted data.
- **Response**:
  ```json
  {
    "data": "string"
  }

### Development
To start the development server with hot reloading:
  ```sh
  npm run dev
  ```
Or prefer Yarn:
  ```sh
  yarn dev
  ```
