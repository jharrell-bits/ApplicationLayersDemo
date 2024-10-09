using Models;

namespace BusinessLogic
{
    public interface IGadgetBusinessLogic
    {
        IEnumerable<Gadget> GetGadgets();
        Task<Gadget?> GetGadget(int id);
        Task UpsertGadget(Gadget gadget);
        Task DeleteGadget(int id);
        Task<Gadget?> MoveGadgetUp(int id);
        Task<Gadget?> MoveGadgetDown(int id);
    }
}
