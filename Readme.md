# Order book monitor
This is an app that keeps track of an order book and calculates the depth of market 

## Prerequisites

This project requires .NET 8 SDK to be built

You can download and install the .NET 8 sdk from this link for your machine
https://dotnet.microsoft.com/download/dotnet/8.0

In order to verify that you have it installed, you can run the following command in your terminal

  ```bash
    dotnet --version
  ```

To run the project, execute these commands from the project directory

  ```bash
    dotnet restore
  ```  
```bash
    dotnet build
  ```  
```bash
    dotnet run --project OrderBookMonitor --launch-profile "https"
  ```
The running app UI should look like this

![Project Logo](/project_ui.png)

