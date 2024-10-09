using BusinessLogic;
using Microsoft.AspNetCore.Components;
using Models;

namespace ApplicationLayersDemo.Components.Pages
{
    public partial class Widgets : ComponentBase
    {
        [Inject]
        private IWidgetBusinessLogic _widgetBusinessLogic { get; set; } = default!;

        private IEnumerable<Widget> Data { get; set; } = Enumerable.Empty<Widget>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            Data = await _widgetBusinessLogic.GetWidgets();
        }

        private async Task MoveWidgetUp(int id)
        {
            await _widgetBusinessLogic.MoveWidgetUp(id);
            await LoadData();
        }

        private async Task MoveWidgetDown(int id)
        {
            await _widgetBusinessLogic.MoveWidgetDown(id);
            await LoadData();
        }
    }
}
