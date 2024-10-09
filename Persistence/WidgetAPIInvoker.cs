using Models;

namespace Persistence
{
    /// <summary>
    /// Access the Widget API rather than directly accessing a datastore
    /// </summary>
    public class WidgetAPIInvoker : IWidgetDataAccess
    {
        private readonly HttpClient _httpClient;

        public WidgetAPIInvoker(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private WidgetWebAPI.WidgetWebAPI GetClient()
        {
            return new WidgetWebAPI.WidgetWebAPI("https://localhost:7038", _httpClient);
        }

        #region Transform API objects to Models and vice versa

        // TODO: use Automapper for this
        private Widget WidgetAPIWidgetToWidget(WidgetWebAPI.Widget apiWidget)
        {
            return new Widget()
            {
                Id = apiWidget.Id,
                Name = apiWidget.Name,
                Description = apiWidget.Description,
                Cost = Convert.ToDecimal(apiWidget.Cost),
                UserDefinedSequenceNumber = apiWidget.UserDefinedSequenceNumber
            };
        }

        // TODO: use Automapper for this
        private WidgetWebAPI.Widget WidgetToWidgetAPIWidget(Widget widget)
        {
            return new WidgetWebAPI.Widget()
            {
                Id = widget.Id,
                Name = widget.Name,
                Description = widget.Description,
                Cost = Convert.ToDouble(widget.Cost),
                UserDefinedSequenceNumber = widget.UserDefinedSequenceNumber
            };
        }

        #endregion

        public async Task<IEnumerable<Widget>> GetWidgets()
        {
            var result = new List<Widget>();

            // fetch the data
            var widgets = await GetClient().WidgetAllAsync();

            if (widgets != null)
            {
                widgets.ToList().ForEach(widget =>
                {
                    result.Add(WidgetAPIWidgetToWidget(widget));
                });
            }

            return result;
        }

        public async Task<Widget?> GetWidget(int id)
        {
            // fetch the data
            var widgetAPIWidget = await GetClient().WidgetGETAsync(id);

            if (widgetAPIWidget != null)
            {
                return WidgetAPIWidgetToWidget(widgetAPIWidget);
            }

            return null;
        }

        public async Task UpsertWidget(Widget widget)
        {
            await GetClient().WidgetPUTAsync(WidgetToWidgetAPIWidget(widget));
        }

        public async Task DeleteWidget(int id)
        {
            await GetClient().WidgetDELETEAsync(id);
        }

        public async Task<Widget?> MoveWidgetUp(int id)
        {
            var widgetAPIWidget = await GetClient().MovementAsync(id, new WidgetWebAPI.WidgetMovement() { MovementType = WidgetWebAPI.TypeOfMove.Up });

            if (widgetAPIWidget != null)
            {
                return WidgetAPIWidgetToWidget(widgetAPIWidget);
            }

            return null;
        }

        public async Task<Widget?> MoveWidgetDown(int id)
        {
            var widgetAPIWidget = await GetClient().MovementAsync(id, new WidgetWebAPI.WidgetMovement() { MovementType = WidgetWebAPI.TypeOfMove.Down });

            if (widgetAPIWidget != null)
            {
                return WidgetAPIWidgetToWidget(widgetAPIWidget);
            }

            return null;
        }
    }
}
