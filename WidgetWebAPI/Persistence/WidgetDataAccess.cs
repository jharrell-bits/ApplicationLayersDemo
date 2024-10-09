using Microsoft.EntityFrameworkCore;
using WidgetWebAPI.Models;

namespace WidgetWebAPI.Data
{
    public class WidgetDataAccess : IWidgetDataAccess
    {
        private readonly WidgetDbContext _widgetDbContext;

        public WidgetDataAccess(WidgetDbContext widgetDbContext)
        {
            _widgetDbContext = widgetDbContext;
        }

        #region Widget CRUD

        public IEnumerable<Widget> GetWidgets()
        {
            return _widgetDbContext.Widgets.ToList();
        }

        public async Task<Widget?> GetWidget(int id)
        {
            return await _widgetDbContext.Widgets.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task UpsertWidget(Widget widget)
        {
            var widgetToUpdate = await _widgetDbContext.Widgets.FirstOrDefaultAsync(f => f.Id == widget.Id);

            if (widgetToUpdate != null)
            {
                widgetToUpdate.Name = widget.Name;
                widgetToUpdate.Description = widget.Description;
                widgetToUpdate.Cost = widget.Cost;
                widgetToUpdate.UserDefinedSequenceNumber = widget.UserDefinedSequenceNumber;
            }
            else
            {
                await _widgetDbContext.Widgets.AddAsync(widget);
            }

            await _widgetDbContext.SaveChangesAsync();
        }

        public async Task DeleteWidget(int id)
        {
            var widgetToDelete = await _widgetDbContext.Widgets.FirstOrDefaultAsync(f => f.Id == id);

            if (widgetToDelete != null)
            {
                _widgetDbContext.Widgets.Remove(widgetToDelete);
                await _widgetDbContext.SaveChangesAsync();
            }
        }

        #endregion
    }
}
