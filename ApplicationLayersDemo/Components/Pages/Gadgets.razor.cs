using BusinessLogic;
using Microsoft.AspNetCore.Components;
using Models;

namespace ApplicationLayersDemo.Components.Pages
{
    public partial class Gadgets : ComponentBase
    {
        [Inject]
        private IGadgetBusinessLogic _gadgetBusinessLogic { get; set; } = default!;

        private IEnumerable<Gadget> Data { get; set; } = Enumerable.Empty<Gadget>();

        protected override void OnInitialized()
        {
            LoadData();
        }

        private void LoadData()
        {
            Data = _gadgetBusinessLogic.GetGadgets();
        }

        private async Task MoveGadgetUp(int id)
        {
            await _gadgetBusinessLogic.MoveGadgetUp(id);
            LoadData();
        }

        private async Task MoveGadgetDown(int id)
        {
            await _gadgetBusinessLogic.MoveGadgetDown(id);
            LoadData();
        }
    }
}
