using Models;

namespace Persistence
{
    public interface IWidgetDataAccess
    {
        Task<IEnumerable<Widget>> GetWidgets();
        Task<Widget?> GetWidget(int id);
        Task UpsertWidget(Widget widget);
        Task DeleteWidget(int id);
        Task<Widget?> MoveWidgetUp(int id);
        Task<Widget?> MoveWidgetDown(int id);
    }
}
