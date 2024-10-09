using Models;

namespace BusinessLogic
{
    public interface IWidgetBusinessLogic
    {
        Task<IEnumerable<Widget>> GetWidgets();
        Task<Widget?> GetWidget(int id);
        Task UpsertWidget(Widget widget);
        Task DeleteWidget(int id);
        Task MoveWidgetUp(int id);
        Task MoveWidgetDown(int id);
    }
}
