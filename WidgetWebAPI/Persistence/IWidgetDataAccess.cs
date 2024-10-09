using WidgetWebAPI.Models;

namespace WidgetWebAPI.Data
{
    public interface IWidgetDataAccess
    {
        IEnumerable<Widget> GetWidgets();
        Task<Widget?> GetWidget(int id);
        Task UpsertWidget(Widget widget);
        Task DeleteWidget(int id);
    }
}
