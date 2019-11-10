# LokiLogger v3

Log anything within your application with under ten lines of code.

## What's so nice about this logger?

1. **Simplicity:**  
  It's really simple. Use the logger as a method attribute in your C# code. Everything else is already handled by the logger.
2. **Implicit usage:**  
  When you added the method attribute, it's done. Really. Don't worry about logging. It's done.
3. **No performance impact:**  
  The logging calls are compiled into the binary. The overhead is not more than a method call. You won't ever feel the difference.
4. **Additional log types:**  
  There are several different types of events that can be handled and filtered differently.

## How to use?

It's very simple. Just add the adapter (the "target" you're writing your logs to) using the following example:

```csharp
Loki.UpdateAdapter(new BasicLoggerAdapter());
```

If an adapter should only be used by specific log levels:

```csharp
Loki.UpdateAdapter(new BasicLoggerAdapter(), new List<LogLevel>{LogLevel.Debug});
```

You can also remove an adapter:

```csharp
Loki.RemoveAdapter(adapter);
```

### Traditional logs

Of course, normal log messages using traditional explicit logging are supported as well:

```csharp
Loki.Debug("Nobody cares about debug info");
Loki.Information("This may help finding problems");
Loki.Warning("This is relatively important");
Loki.Error("This is more important");
Loki.Critical("This is very important!");
```

You can also add any object to the method all, e.g.:

```csharp
Loki.Warning("Maybe you have a problem:", new object());
```

### Exception handling

Exceptions are very important. However, sometimes nobody cares about them. So, you can define this behavior by using the log levels accordingly.

```csharp
Loki.ExceptionDebug("Nobody cares about debug info");
Loki.ExceptionInformation("This may help finding problems");
Loki.ExceptionWarning("This is relatively important");
Loki.ExceptionError("This is more important");
Loki.ExceptionCritical("This is very important!");
```

### Return capture

Returns are very important. Sometimes... ;)  
The default log level of returns is `Debug`.

```csharp
Loki.Return(returnValue);
```

## How to add this functionality to your project?

### Basic usage

Just add [Fody](https://www.nuget.org/packages/Fody/) to your Project. Then add ```<LokiLogger/>``` in the ```FodyWeavers.xml```. That's it. You're ready to go!

Add the attribute `Loki` over every method or class you want to log. Every method call and exception will be logged, thanks to Fody. The execution time is additionally logged.

```csharp
[Loki]
public int do(int x){
    return x++;
}
```



:warning: **Note:** Don't use that feature in very frequently called methods. The performance impact is normally very low. But you may encounter "problems" when using the attribute on an extremely frequently used method. The Overhead per Methodcall is ~40-80 ms + Executiontime(Adapter).

### Use in ASP Core

Add the following snippet in the `StartUp.cs` file in the method `ConfigureServices`:

```csharp
services.AddLokiObjectLogger();
```

Adapt and add the following part in the method `Configure` in order to use `LokiReporter`:

```csharp
app.UseLokiLogger(
	x => {
	    x.UseMiddleware = true;
	    x.Secret = "1234";
	    x.HostName = "<hostname>";
	    x.DefaultLevel = LogLevel.Debug;
	}
);
```

## Details

By default, the three basic log events are:

- `Invoke` when entering a method
- `Exception` when an exception occurs
- `Rest` when there happens a REST-call
- `Return` when returning a value

We always want to know, where an event happened. Thus, it's implemented using `[CallerMemberName]` to avoid reflection overhead.
