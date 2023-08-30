using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;

namespace AudioDeviceSwitcher
{
    internal class Script
    {
        private Runspace? runspace;

        internal void Init()
        {
            InitialSessionState iss = InitialSessionState.CreateDefault();
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "", "AudioDeviceCmdlets.dll");
            iss.ImportPSModule(new string[]
            {
                path
            });

            runspace = RunspaceFactory.CreateRunspace(iss);
            runspace.Open();
        }

        internal Collection<PSObject>? Execute(string command, string parameter, string? parameterValue = null)
        {
            var pipeline = runspace?.CreatePipeline();
            var command_set = new Command(command);
            var param_set = new CommandParameter(parameter, parameterValue);
            command_set.Parameters.Add(param_set);
            pipeline?.Commands.Add(command_set);

            var results = pipeline?.Invoke();
            return results;
        }

        internal void Stop()
        {
            runspace?.Close();
        }
    }
}
