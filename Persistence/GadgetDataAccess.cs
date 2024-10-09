using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Persistence
{
    /// <summary>
    /// Access the Gadget datastore
    /// </summary>
    public class GadgetDataAccess : IGadgetDataAccess
    {
        private readonly GadgetDbContext _gadgetDbContext;

        public GadgetDataAccess(GadgetDbContext gadgetDbContext) 
        { 
            _gadgetDbContext = gadgetDbContext;
        }

         #region Gadget CRUD

        public IEnumerable<Gadget> GetGadgets()
        {
            return _gadgetDbContext.Gadgets.ToList();
        }

        public async Task<Gadget?> GetGadget(int id)
        {
            return await _gadgetDbContext.Gadgets.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task UpsertGadget(Gadget gadget)
        {
            var gadgetToUpdate = await _gadgetDbContext.Gadgets.FirstOrDefaultAsync(f => f.Id == gadget.Id);

            if (gadgetToUpdate != null)
            {
                gadgetToUpdate.GadgetType = gadget.GadgetType;
                gadgetToUpdate.UsageInstructions = gadget.UsageInstructions;
            }
            else
            {
                await _gadgetDbContext.Gadgets.AddAsync(gadget);
            }

            await _gadgetDbContext.SaveChangesAsync();
        }

        public async Task DeleteGadget(int id)
        {
            var GadgetToDelete = await _gadgetDbContext.Gadgets.FirstOrDefaultAsync(f => f.Id == id);

            if (GadgetToDelete != null)
            {
                _gadgetDbContext.Gadgets.Remove(GadgetToDelete);
                await _gadgetDbContext.SaveChangesAsync();
            }
        }

        #endregion
    }
}
