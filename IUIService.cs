using System;

namespace Context
{
    public interface IUIService
    {
        void ShowErrorDialog(Exception exception, string dialogTitle);
        void ShowErrorDialog(string message, Exception exception, string dialogTitle);
    }
}