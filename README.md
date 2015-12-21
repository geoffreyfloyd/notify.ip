# Install

installutil path/to/build/IpChangeNotifyService.exe

When prompted for user and password, it must be in format domain/username

# Uninstall

installutil /u path/to/build/IpChangeNotifyService.exe

# Debug

Use the Console Application with Debug Config in order to debug the `OnStart()` and `OnStop()` methods.

For debugging other code, you can attach to the Windows Service Process in `Debug > Attach to Process...`