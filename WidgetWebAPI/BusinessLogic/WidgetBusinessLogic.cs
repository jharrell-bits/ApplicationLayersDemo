using WidgetWebAPI.Data;
using WidgetWebAPI.Models;

namespace WidgetWebAPI.BusinessLogic
{
    public class WidgetBusinessLogic : IWidgetBusinessLogic
    {
        private readonly IWidgetDataAccess _widgetDataAccess;

        public WidgetBusinessLogic(IWidgetDataAccess widgetDataAccess)
        {
            _widgetDataAccess = widgetDataAccess;
        }

        #region Widgets

        /// <summary>
        /// Get the Widgets and sort by the manual sorting index
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Widget> GetWidgets()
        {
            return _widgetDataAccess.GetWidgets().OrderBy(f => f.UserDefinedSequenceNumber);
        }

        /// <summary>
        /// Get a single Widget
        /// </summary>
        /// <param name="id">Widget Id to retrieve</param>
        /// <returns>Widget if there is a Widget with the corresponding Id in the datastore.</returns>
        public async Task<Widget?> GetWidget(int id)
        {
            return await _widgetDataAccess.GetWidget(id);
        }

        /// <summary>
        /// Updates the Widget if a Widget already exists with the provided Id, otherwise, inserts a new Widget.
        /// </summary>
        /// <param name="Widget">Widget object to update or insert.</param>
        /// <returns></returns>
        public async Task UpsertWidget(Widget Widget)
        {
            await _widgetDataAccess.UpsertWidget(Widget);
        }

        /// <summary>
        /// Deletes a Widget
        /// </summary>
        /// <param name="id">Widget Id to delete</param>
        /// <returns></returns>
        public async Task DeleteWidget(int id)
        {
            await _widgetDataAccess.DeleteWidget(id);
        }

        /// <summary>
        /// Simple business logic example that adjusts a manual sorting index of a Widget to move it up one position
        /// </summary>
        /// <param name="id">Widget Id to move up</param>
        /// <returns>Updated Widget after sort index has been updated</returns>
        public async Task<Widget?> MoveWidgetUp(int id)
        {
            var widgets = GetWidgets();

            Widget? previousWidget = null;

            foreach (var widget in widgets)
            {
                if (widget.Id == id)
                {
                    if (previousWidget != null)
                    {
                        int previousSequenceNumber = previousWidget.UserDefinedSequenceNumber;

                        previousWidget.UserDefinedSequenceNumber = widget.UserDefinedSequenceNumber;
                        await _widgetDataAccess.UpsertWidget(previousWidget);

                        widget.UserDefinedSequenceNumber = previousSequenceNumber;
                        await _widgetDataAccess.UpsertWidget(widget);

                        break;
                    }
                }

                previousWidget = widget;
            }

            return widgets.FirstOrDefault(f => f.Id == id);
        }

        /// <summary>
        /// Simple business logic example that adjusts a manual sorting index of a Widget to move it down one position
        /// </summary>
        /// <param name="id">Widget Id to move down</param>
        /// <returns>Updated Widget after sort index has been updated</returns>
        public async Task<Widget?> MoveWidgetDown(int id)
        {
            var widgets = GetWidgets();

            Widget? previousWidget = null;

            foreach (var widget in widgets)
            {
                if (previousWidget != null)
                {
                    var previousWidgetSequenceNumber = previousWidget.UserDefinedSequenceNumber;

                    previousWidget.UserDefinedSequenceNumber = widget.UserDefinedSequenceNumber;
                    await _widgetDataAccess.UpsertWidget(previousWidget);

                    widget.UserDefinedSequenceNumber = previousWidgetSequenceNumber;
                    await _widgetDataAccess.UpsertWidget(widget);

                    break;
                }

                if (widget.Id == id)
                {
                    previousWidget = widget;
                }
            }

            return widgets.FirstOrDefault(f => f.Id == id);
        }

        #endregion
    }
}
