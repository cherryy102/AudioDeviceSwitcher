namespace AudioDeviceSwitcher {
    internal class Devices {
        internal static string FirstId { get => Environment.GetEnvironmentVariable("Audio:FirstId") ?? string.Empty; }
        internal static string SecondId { get => Environment.GetEnvironmentVariable("Audio:SecondId") ?? string.Empty; }
    }
}
