using Models;
using Persistence;

namespace BusinessLogic
{
    public class GadgetBusinessLogic : IGadgetBusinessLogic
    {
        private readonly IGadgetDataAccess _gadgetDataAccess;

        public GadgetBusinessLogic(IGadgetDataAccess applicationDataAccess)
        {
            _gadgetDataAccess = applicationDataAccess;
        }

        #region Gadgets

        /// <summary>
        /// Get the Gadgets and sort by the manual sorting index
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Gadget> GetGadgets() 
        { 
            return _gadgetDataAccess.GetGadgets().OrderBy(f => f.UserDefinedSequenceNumber);
        }

        /// <summary>
        /// Get a single Gadget
        /// </summary>
        /// <param name="id">Gadget Id to retrieve</param>
        /// <returns>Gadget if there is a Gidget with the corresponding Id in the datastore.</returns>
        public async Task<Gadget?> GetGadget(int id)
        {
            return await _gadgetDataAccess.GetGadget(id);
        }

        /// <summary>
        /// Updates the Gadget if a Gadget already exists with the provided Id, otherwise, inserts a new Gadget.
        /// </summary>
        /// <param name="Gadget">Gadget object to update or insert.</param>
        /// <returns></returns>
        public async Task UpsertGadget(Gadget gadget)
        {
            await _gadgetDataAccess.UpsertGadget(gadget);
        }

        /// <summary>
        /// Deletes a Gadget
        /// </summary>
        /// <param name="id">Gadget Id to delete</param>
        /// <returns></returns>
        public async Task DeleteGadget(int id)
        {
            await _gadgetDataAccess.DeleteGadget(id);
        }

        /// <summary>
        /// Simple business logic example that adjusts a manual sorting index of a Gadget to move it up one position
        /// </summary>
        /// <param name="id">Gadget Id to move up</param>
        /// <returns>Updated Gadget after sort index has been updated</returns>
        public async Task<Gadget?> MoveGadgetUp(int id)
        {
            var gadgets = GetGadgets();

            Gadget? previousGadget = null;

            foreach(var gadget in gadgets)
            {
                if (gadget.Id ==  id)
                {
                    if (previousGadget != null)
                    {
                        int previousSequenceNumber = previousGadget.UserDefinedSequenceNumber;

                        previousGadget.UserDefinedSequenceNumber = gadget.UserDefinedSequenceNumber;
                        await _gadgetDataAccess.UpsertGadget(previousGadget);

                        gadget.UserDefinedSequenceNumber = previousSequenceNumber;
                        await _gadgetDataAccess.UpsertGadget(gadget);

                        break;
                    }
                }

                previousGadget = gadget;
            }

            return gadgets.FirstOrDefault(f => f.Id == id);
        }

        /// <summary>
        /// Simple business logic example that adjusts a manual sorting index of a Gadget to move it down one position
        /// </summary>
        /// <param name="id">Gadget Id to move down</param>
        /// <returns>Updated Gadget after sort index has been updated</returns>  
        public async Task<Gadget?> MoveGadgetDown(int id)
        {
            var gadgets = GetGadgets();

            Gadget? previousGadget = null;

            foreach (var gadget in gadgets)
            {
                if (previousGadget != null)
                {
                    var previousGadgetSequenceNumber = previousGadget.UserDefinedSequenceNumber;

                    previousGadget.UserDefinedSequenceNumber = gadget.UserDefinedSequenceNumber;
                    await _gadgetDataAccess.UpsertGadget(previousGadget);

                    gadget.UserDefinedSequenceNumber = previousGadgetSequenceNumber;
                    await _gadgetDataAccess.UpsertGadget(gadget);

                    break;
                }

                if (gadget.Id == id)
                {
                    previousGadget = gadget;
                }
            }

            return gadgets.FirstOrDefault(f => f.Id == id);
        }

        #endregion
    }
}
