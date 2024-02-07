using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;

namespace AudioDeviceSwitcher {
    internal class PowerShellScript {
        private readonly InitialSessionState initialSessionState;

        public PowerShellScript() {
            this.initialSessionState = GetInitialSessionState();
        }

        internal InitialSessionState GetInitialSessionState() {
            var iss = InitialSessionState.CreateDefault();
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "", "AudioDeviceCmdlets.dll");
            iss.ImportPSModule(new string[] { path });
            return iss;
        }

        internal Collection<PSObject>? Execute(string powerShellCommand, string parameter, string parameterValue) {
            var command = new Command(powerShellCommand);
            var param = new CommandParameter(parameter, parameterValue);
            command.Parameters.Add(param);
            var results = Execute(command);
            return results;
        }

        internal Collection<PSObject>? Execute(string powerShellCommand, string parameter) {
            var command = new Command(powerShellCommand);
            var param = new CommandParameter(parameter);
            command.Parameters.Add(param);
            var results = Execute(command);
            return results;
        }

        internal Collection<PSObject>? Execute(Command command) {
            var runspace = RunspaceFactory.CreateRunspace(initialSessionState);
            runspace.Open();
            var pipeline = runspace?.CreatePipeline();
            pipeline?.Commands.Add(command);

            var results = pipeline?.Invoke();
            pipeline?.Dispose();
            runspace?.Dispose();
            return results;
        }
    }
}
