using Microsoft.AspNetCore.Components;

namespace FoodHub.Blazor.Client.Helper
{
    public class BlazorComponent : ComponentBase
    {
        private  readonly RefreshBroadCast _refreshRequest = RefreshBroadCast.Instance;

        protected override void OnInitialized() {

            _refreshRequest.RefreshRequested += DoRefresh;

            base.OnInitialized();
        
        }

        public void CallRefreshRequest() {

            _refreshRequest.CallRequestRefresh();

        }

        private void DoRefresh() {

            StateHasChanged();
        
        }

    }
}
