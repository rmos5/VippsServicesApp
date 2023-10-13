using System;
using System.Windows.Input;

namespace Context
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);

        public void RaiseCanExecuteChanged()
        {
            EventHandler h = CanExecuteChanged;
            h?.Invoke(this, EventArgs.Empty);
        }
    }

    public abstract class CommandBase<T> : CommandBase
            where T : ContextBase
    {
        public T Context { get; }

        protected CommandBase(T context) => Context = context ?? throw new ArgumentNullException(nameof(context));

        //public override bool CanExecute(object parameter)
        //{
        //    return Context.HasContext
        //        && Context.Context.IsInitialized
        //        && Context.IsEnabled;
        //}
    }

    public abstract class LoggedInCommandBase<T> : CommandBase<T>
        where T : ContextBase
    {
        protected LoggedInCommandBase(T context)
            : base(context)
        {
        }

        //public override bool CanExecute(object parameter)
        //{
        //    return base.CanExecute(parameter)
        //        && Context.IsLoggedIn;
        //}
    }

    //note: used for a menu item enabled/disabled binding
    public class EmptyCommand : CommandBase<ContextBase>
    {
        public EmptyCommand(ContextBase context) 
            : base(context)
        {
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
        }
    }
    
    //public class CloseDialogCommand : CommandBase<ContextBase>
    //{
    //    protected bool DialogResult { get; }

    //    protected Func<bool> CanExecuteFunc { get; }

    //    public CloseDialogCommand(ContextBase context, bool dialogResult, Func<bool> canExecuteFunc = null)
    //        : base(context)
    //    {
    //        DialogResult = dialogResult;
    //        CanExecuteFunc = canExecuteFunc;
    //    }

    //    public override bool CanExecute(object parameter)
    //    {
    //        return CanExecuteFunc == null || CanExecuteFunc.Invoke();
    //    }

    //    public override void Execute(object parameter)
    //    {
    //        Context.UIService.CloseDialog(DialogResult);
    //    }
    //}
}
