using Context;
using System.Windows;
using System.Windows.Controls;

namespace VippsServicesApp.Views
{
    public class ContextViewBase<TContext> : UserControl
        where TContext : ContextBase
    {
        public new TContext DataContext
        {
            get => base.DataContext as TContext;
            set => base.DataContext = value;
        }

        public ContextViewBase()
        {
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            OnDataContextChanged(e.NewValue as TContext);
        }

        protected virtual void OnDataContextChanged(TContext context)
        {
        }
    }
}
