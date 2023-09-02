using AudioDeviceSwitcher;

var results = PsScript.Execute("Get-AudioDevice", "Playback");
var currentDevice = results?.FirstOrDefault()?.Members["ID"].Value as string;
if (string.IsNullOrWhiteSpace(currentDevice)) {
    return;
}
if (currentDevice == Devices.FirstId) {
    PsScript.Execute("Set-AudioDevice", "ID", Devices.SecondId);
    return;
}
PsScript.Execute("Set-AudioDevice", "ID", Devices.FirstId);
