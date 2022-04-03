```
Config config = GetConfig();

if (config is null)
{
    Exit.Error("Config file not found (config.json)");
    return;
}

if (!ConfigConfigured(config))
{
    Exit.Error("Config file not configured properly");
    return;
}

FileWatcher fileWatcher = new(config);
fileWatcher.WatchFile();
```
