using Models;

namespace Persistence
{
    public interface IGadgetDataAccess
    {
        IEnumerable<Gadget> GetGadgets();
        Task<Gadget?> GetGadget(int id);
        Task UpsertGadget(Gadget gadget);
        Task DeleteGadget(int id);
    }
}
