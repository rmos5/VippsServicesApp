using Context;
using System;
using System.Windows.Controls;

namespace VippsServicesApp.Views
{
    public partial class ContextView<T> : UserControl
        where T : ContextBase
    {
        public new T DataContext
        {
            get => (T)base.DataContext;
            set => base.DataContext = (T)value;
        }

        protected ContextView(T context)
        {
            DataContext = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
