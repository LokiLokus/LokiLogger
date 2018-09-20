# LokiLogger

Most Logging Frameworks like Serilog are quiet complex and so slow.
LokiLogger is a more than simple Alternative. With under 200 LoC it's
very small and easy to extend.

## Why it's much better than other Logger?
Other Logger are f*** slow, LokiLogger is small and fast.
Also the most Logger don't safe the Class, Method or LineNumber
where the Logger is called. But this is obviously a nice Feature
for Debugging (most Logger have this Feature, but the use Refelection
=> more than slow => mostly disabled in Production).


## Usage
More than simple just:
```
Log.SetWriter(new ConsoleWriter());


Log.Verbose("Text");
Log.Debug("Text");
Log.Info("Text");
Log.Warn("Text");
Log.Crit("Text");
Log.SysCrit("Text");

//Not needed e.g. for the ConsoleWriter but for the FileWriter and some others to close Stream/cleanup
Log.StopLog();

```

## Architecture
The Libary is divided in Models, Writers and the Main Log Class.
 
Models is mainly the Log Class,
a DTO for all Logged Events and the Logtype enum, just a simple Enum
to identitfy the type of a Log Event.

Writers provide the Possibility to write the Logs to a custom output
per default the ConsoleWriter is used. All of them must Implement the
the IWriter Interface.

The Log Class provides the kernl Features like changing the Writer and
or Log Events.

## Features
Fast Logging.
The Log Events are divided in:
 - Information => Nothing Special just an Information
 - Warning => Possible Issues
 - Critical => Failure in the System, expected Functionality cannot be provided
 - System Critical => System is unstable, Writers should send an Alert,
 when this Event happens

On every Log Event the Calling Class, Method and LineNumber is saved
=> it's quite easy to find the Error.


## Performance
The Focus on this Project is performance. LokiLogger is e.g. with Console
Output about 40% faster then Serilog (in some Scenarios about 60 % faster). It's thread safe and build for fast
Logging.

## Licence
This Libary is under MIT Lincense published.

https://www.nuget.org/packages/LokiLogger/