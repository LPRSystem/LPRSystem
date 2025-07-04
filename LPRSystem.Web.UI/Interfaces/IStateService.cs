﻿using LPRSystem.Web.UI.Models;

namespace LPRSystem.Web.UI.Interfaces
{
    public interface IStateService
    {
        Task<List<StateDetails>> GetStatesAync();
        Task<StateDetails> GetStateByIdAsync(long stateId);
        Task<State> InsertOrUpdateStateAsync(State state);
        Task<State> UpdateStateAsync(State state);
        Task<bool> DeleteStateAsync(long stateId);
    }
}
