using System.ComponentModel;

namespace VippsServicesApp.Properties
{
    internal sealed partial class Settings
    {
        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Save();
        }
    }
}
