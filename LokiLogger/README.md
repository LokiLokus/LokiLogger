# LokiLogger v2

Vendors Lock-In is a common Antipattern, Patterns like Dependency Injection try to remove these Antipatterns,
but not all Platforms support DI like e.g. ASP Core. So usually a Wrapper/Interface is build, so it's easy to exchange 
e.g. the Loging Framework. Also some new nice Feature are not supported, like the ```CallerLineNumber``` to get the LineNumber
in which a Method is called.

So you can build for every Project your on Logging Wrapper, or you can just use LokiLogger, which is basically the same.

## Why it's much better than other Logger?
LokiLogger do not replace other Logger, it just build a wrapper around them, so you can use LokiLogger
as your normal Logging Interface and define on Runtime which Framework you want to use (maybe you want to use more than one).
Maybe you use an Email Notification on Fatal Errors, you can define this in LokiLogger.

## Usage
More than simple just:
```
Loki.UpdateAdapter(new SerilogLoggerAdapter());


Loki.Verbose("Text");
Loki.Debug("Text");
Loki.Information("Text");
Loki.Warning("Text");
Loki.Error("Text");
Loki.Fatal("Text");


```

## Architecture
The Libary is divided in Models, Adapters and the Main Log Class.
 
Models is mainly the Log Class,
a DTO for all Logged Events and the Logtype enum, just a simple Enum
to identitfy the type of a Log Event.

Adapters are just Adapter to other Logging Frameworks, LokiLogger provide only a simple ConsoleLogger for demo, which is used by default.
To build your own LoggerAdapter Implement the ```ILogAdapter``` Interface.

The Loki Class provides the kernel Features like changing the Writer and
or Log Events.

## Features
Fast Logging.
The Log Events are divided in:
 - Information => Nothing Special just an Information
 - Warning => Possible Issues
 - Critical => Failure in the System, expected Functionality cannot be provided
 - System Critical => System is unstable, Writers should send an Alert,
 when this Event happens

### Log Special events
Log Return values:
 - Especially logging Exceptions is very Important so use ```Loki.ExceptionDebug(Exception)```
 - Logging return Values ( very important for Debug) ```Loki.ReturnDebug(object)```

On every Log Event the Calling Class, Method and LineNumber is saved
=> it's quite easy to find the Error.

Filter your Logs by LogLevel to different Loggers:
```
Loki.UpdateAdapter(new SerilogLoggerAdapter(),new List<LogLevel>(){LogLevel.Verbose,LogLevel.Information});

```
Now the Serilog Adapter will not get any Logs which are Verbose or Information Level.
## Adapter

## Licence
This Libary is under MIT Lincense published.

https://www.nuget.org/packages/LokiLogger/