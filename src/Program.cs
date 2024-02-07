using AudioDeviceSwitcher;

var powerShellScript = new PowerShellScript();
var results = powerShellScript.Execute("Get-AudioDevice", "Playback");
var currentDevice = results?.FirstOrDefault()?.Members["ID"].Value as string;
if (string.IsNullOrWhiteSpace(currentDevice)) {
    return;
}
if (currentDevice == Devices.FirstId) {
    powerShellScript.Execute("Set-AudioDevice", "ID", Devices.SecondId);
    return;
}
powerShellScript.Execute("Set-AudioDevice", "ID", Devices.FirstId);
