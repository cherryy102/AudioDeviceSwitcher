using AudioDeviceSwitcher;

var script = new Script();
script.Init();
var results = script.Execute("Get-AudioDevice", "Playback");
var currentDevice = results?.FirstOrDefault()?.Members["ID"].Value as string;
if (string.IsNullOrWhiteSpace(currentDevice))
{
    return;
}

if (currentDevice == Devices.SpeakersId)
{
    script.Execute("Set-AudioDevice", "ID", Devices.HeadphonesId);
    return;
}
script.Execute("Set-AudioDevice", "ID", Devices.SpeakersId);

script.Stop();