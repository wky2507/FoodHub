using System;
namespace FoodHub.Blazor.Client.Helper
{
    internal sealed class RefreshBroadCast
    {
        private readonly static Lazy<RefreshBroadCast> _instance = new Lazy<RefreshBroadCast>(() => new RefreshBroadCast());

        public static RefreshBroadCast Instance => _instance.Value;
        private RefreshBroadCast() { }
        
        public event Action? RefreshRequested;

       
        public void CallRequestRefresh() {
            RefreshRequested?.Invoke();
        }

    }
}
