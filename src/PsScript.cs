using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;

namespace AudioDeviceSwitcher {
    internal class PsScript {
        internal static Collection<PSObject>? Execute(string psCommand, string parameter, string? parameterValue = null) {
            var iss = InitialSessionState.CreateDefault();
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "", "AudioDeviceCmdlets.dll");
            iss.ImportPSModule(new string[]{ path });
            var runspace = RunspaceFactory.CreateRunspace(iss);
            runspace.Open();
            var pipeline = runspace?.CreatePipeline();
            var command = new Command(psCommand);
            var param = new CommandParameter(parameter, parameterValue);
            command.Parameters.Add(param);
            pipeline?.Commands.Add(command);

            var results = pipeline?.Invoke();
            pipeline?.Dispose();
            runspace?.Dispose();
            return results;
        }
    }
}
