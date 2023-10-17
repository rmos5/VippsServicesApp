using System;

namespace Context
{
    public interface IUIService
    {
        void ShowErrorDialog(string message, Exception exception, string dialogTitle);
    }
}