# LokiObject Adpater

Adapter for LokiLogger (https://www.nuget.org/packages/LokiLogger)
written for ASP .Net Core >= 2.0


## How to Use

In ```Startup.cs``` add
```services.AddLokiObjectLogger("http://localhost:5000/api/Logging/Log", "LokiTestLogger");```

Writes the Logs to the Web interface