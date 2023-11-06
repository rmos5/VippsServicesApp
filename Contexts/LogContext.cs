using Context;
using System;
using System.Diagnostics;
using System.IO;

namespace VippsServicesApp.Contexts
{
    public partial class LogContext : ContextBase       
    {
        private sealed class CreateDirectoryCommandImpl : CommandBase<LogContext>
        {
            public CreateDirectoryCommandImpl(LogContext context) : base(context)
            {
            }

            public override bool CanExecute(object parameter)
            {
                return Context.CanExecuteCreateDirectoryCommand(parameter);
            }

            public override void Execute(object parameter)
            {
                Context.ExecuteCreateDirectoryCommand(parameter);
            }
        }

        public ILoggingSettings Settings { get; }

        public string LoggingDirectoryPath => Settings.LoggingDirectoryPath;

        public CommandBase CreateDirectoryCommand { get; }
        
        public LogContext()
        {
            //note: for UI designer
        }

        public LogContext(IUIService uiService, ILoggingSettings settings)
            :base(uiService)
        {
            Settings = settings;
            CreateDirectoryCommand = new CreateDirectoryCommandImpl(this);
        }

        protected override string SetTitle()
        {
            return "Log";
        }

        private bool CanExecuteCreateDirectoryCommand(object parameter)
        {
            return !Directory.Exists(LoggingDirectoryPath);
        }

        private void ExecuteCreateDirectoryCommand(object parameter)
        {
            try
            {
                Directory.CreateDirectory(LoggingDirectoryPath);
            }
            catch (Exception ex)
            {
                ShowErrorDialog("Can't create logging directory.", ex, "Error");
            }
        }

    }
}
