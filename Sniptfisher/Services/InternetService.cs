using Microsoft.Phone.Net.NetworkInformation;

namespace Sniptfisher.Services
{
    public class InternetService : Interfaces.IInternetService
    {
        public bool IsNetworkAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
    }
}
