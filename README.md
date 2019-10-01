# LokiLogger v3
Log your whole Application with under 10 Lines.

## Getting Started
1. Logging is bullshit, you write Code that do nothing for the .
    So fuck of it, Logging have to be implicit.
2. I want to know which Method has produced the Log. Without writing this in the Log Method Call
    This is implemented with ```[CallerMemberName]``` so there is no Refelection Overhead.
3. Log Levels are nice, Log Types are better.
    Instead of only differentiating different Log Levels, with Log Types searching for specific Logs is much more easy.


How Looks a Log?
```
LogTyp typ,
LogLevel loglvl,
string message,
string className,
string methodName,
int line,
params object[] objects
```
### Define an Adapter (the thing writing to Log Data to anywhere)
It's very simple just add the Adapter
```
    Loki.UpdateAdapter(new BasicLoggerAdapter());
```

Or when an Adapter only should be used by specific Log Levels
```
    Loki.UpdateAdapter(new BasicLoggerAdapter(), new List<LogLevel>{LogLevel.Debug});
```

Remove Adapter with
```
    Loki.RemoveAdapter(adapter);
```
### Vanilla
Write normal Logs with:
```
    Loki.Debug("This is not so important");
    Loki.Information("This is not so important");
    Loki.Warning("This is more important",new object());
    Loki.Error("This is more important");
    Loki.Critical("This is more important");
```
You can also add any object to the Log Call.

#### Write Exception
Exceptions are very important, but somethimes nobody cars about them, you can define this behaviour with LogLevel
```
    Loki.ExceptionDebug("This is not so important");
    Loki.ExceptionInformation("This is not so important");
    Loki.ExceptionWarning("This is more important",new object());
    Loki.ExceptionError("This is more important",exception);
    Loki.ExceptionCritical("This is more important");
```

#### Write Return
Returns are very important.... sometimes ;)
Returns are logged by Default with LogLevel.Debug

```
    Loki.Return(returnValue);
```

### Fuck of bloated Logging Code
Yeah fuck of it, so use Fody to compile in every Method an implicit Loggingcall
Just add [Fody](https://www.nuget.org/packages/Fody/) to your Project.
Then add ```<LokiLogger/>``` in the ```FodyWeavers.xml```.
Thats it!
Then add over every Method or Class you want to log a ```Loki``` Attribute.
Thanks to Fody every Method Call and Exception will be logged. Also the Execution time is logged.

```
[Loki]
public string ILoveLokiLogger(string name){
    return $"I'm {name} and i love LokiLogger";
}
```
**Don't use that in frequent called Method, the Performance impact is normally very low, but on a high frequent used Method this could be problematic**



### Use in ASP Core
Add in the StartUp.cs:

In ```ConfigureServices``` add
```
    services.AddLokiObjectLogger();
```
In ```Configure``` add the follwoing in Order to use LokiReporter 
```

    app.UseLokiLogger(x =>
    {
        x.UseMiddleware = true;
        x.Secret = "1234";
        x.HostName = "<hostname>";
        x.DefaultLevel = LogLevel.Debug;
    });
```
https://gitlab.com/LokiLokus/lokilogger