using WidgetWebAPI.Models;

namespace WidgetWebAPI.BusinessLogic
{
    public interface IWidgetBusinessLogic
    {
        IEnumerable<Widget> GetWidgets();
        Task<Widget?> GetWidget(int id);
        Task UpsertWidget(Widget Widget);
        Task DeleteWidget(int id);
        Task<Widget?> MoveWidgetUp(int id);
        Task<Widget?> MoveWidgetDown(int id);
    }
}
