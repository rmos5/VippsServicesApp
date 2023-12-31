using LoggingHelper;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Context
{
    public abstract partial class ContextBase : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected IUIService UIService { get; }

        protected string title;
        public string Title
        {
            get => title;
            set
            {
                if (value == null
                    && title == null)
                    return;
                if (!value.Equals(title))
                {
                    title = value;
                    OnPropertyChanged();
                }
            }
        }

        protected ContextBase()
        {
            title = SetTitle();
        }

        protected ContextBase(IUIService uiService)
        {
            UIService = uiService;
            title = SetTitle();
        }

        protected abstract string SetTitle();

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Log.Debug(this, $"{nameof(OnPropertyChanged)}:{propertyName}");
            if (PropertyChanged == null)
                throw new ArgumentNullException(nameof(propertyName));
            if (!GetType().GetProperties().Any(o => o.Name.Equals(propertyName)))
                throw new ArgumentOutOfRangeException($"Not existing properety '{propertyName}' change.", nameof(propertyName));
            PropertyChangedEventHandler e = PropertyChanged;
            e?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void ShowErrorDialog(string message, Exception error, string title)
        {
            UIService.ShowErrorDialog(message, error, title);
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //DisposeInternal();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PdPosViewModelBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
