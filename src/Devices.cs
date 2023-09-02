namespace AudioDeviceSwitcher {
    internal class Devices {
        internal static string FirstId => Environment.GetEnvironmentVariable("Audio:FirstId") ?? string.Empty;
        internal static string SecondId => Environment.GetEnvironmentVariable("Audio:SecondId") ?? string.Empty;
    }
}
