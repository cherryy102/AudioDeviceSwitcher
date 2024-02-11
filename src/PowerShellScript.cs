using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace AudioDeviceSwitcher {
    internal class PowerShellScript {
        private readonly InitialSessionState initialSessionState;

        public PowerShellScript() {
            this.initialSessionState = InitialSessionState.CreateDefault();
        }

        internal void ImportPowerShellModule(params string[] path) {
            initialSessionState.ImportPSModule(path);
        }

        internal Collection<PSObject>? Execute(string powerShellCommand, string parameter, string parameterValue) {
            var command = new Command(powerShellCommand);
            var commandParameter = new CommandParameter(parameter, parameterValue);
            command.Parameters.Add(commandParameter);
            var results = Execute(command);
            return results;
        }

        internal Collection<PSObject>? Execute(string powerShellCommand, string parameter) {
            var command = new Command(powerShellCommand);
            var commandParameter = new CommandParameter(parameter);
            command.Parameters.Add(commandParameter);
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
