using AudioDeviceSwitcher;

var script = new Script();
script.Init();
var results = script.Execute("Get-AudioDevice", "Playback");
var currentDevice = results?.FirstOrDefault()?.Members["ID"].Value as string;
if (string.IsNullOrWhiteSpace(currentDevice))
{
    return;
}
if (currentDevice == Devices.FirstId) {
    script.Execute("Set-AudioDevice", "ID", Devices.SecondId);
    return;
}
script.Execute("Set-AudioDevice", "ID", Devices.FirstId);
script.Stop();