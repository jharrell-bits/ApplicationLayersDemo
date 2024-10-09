using Models;
using Persistence;

namespace BusinessLogic
{
    public class WidgetBusinessLogic : IWidgetBusinessLogic
    {
        private readonly IWidgetDataAccess _widgetDataAccess;

        public WidgetBusinessLogic(IWidgetDataAccess widgetDataAccess)
        {
            _widgetDataAccess = widgetDataAccess;
        }

        #region Widgets

        public async Task<IEnumerable<Widget>> GetWidgets()
        {
            return (await _widgetDataAccess.GetWidgets()).OrderBy(f => f.UserDefinedSequenceNumber);
        }

        public async Task<Widget?> GetWidget(int id)
        {
            return await _widgetDataAccess.GetWidget(id);
        }

        public async Task UpsertWidget(Widget Widget)
        {
            await _widgetDataAccess.UpsertWidget(Widget);
        }

        public async Task DeleteWidget(int id)
        {
            await _widgetDataAccess.DeleteWidget(id);
        }

        public async Task MoveWidgetUp(int id)
        {
            await _widgetDataAccess.MoveWidgetUp(id);
        }

        public async Task MoveWidgetDown(int id)
        {
            await _widgetDataAccess.MoveWidgetDown(id);
        }

        #endregion
    }
}
