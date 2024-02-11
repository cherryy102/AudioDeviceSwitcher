using AudioDeviceSwitcher;
using System.Reflection;

var powerShellScript = new PowerShellScript();
var audioDeviceLibPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "", "AudioDeviceCmdlets.dll");
powerShellScript.ImportPowerShellModule(audioDeviceLibPath);
var results = powerShellScript.Execute("Get-AudioDevice", "Playback");
var currentDeviceId = results?.FirstOrDefault()?.Members["ID"].Value.ToString();
if (string.IsNullOrWhiteSpace(currentDeviceId)) {
    return;
}
if (currentDeviceId == Devices.FirstId) {
    powerShellScript.Execute("Set-AudioDevice", "ID", Devices.SecondId);
    return;
}
powerShellScript.Execute("Set-AudioDevice", "ID", Devices.FirstId);
