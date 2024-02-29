using Microsoft.EntityFrameworkCore;
using SonetexApp.Data;
using SonetexApp.Models;

namespace SonetexApp.Repositories;
public interface IStateRepository
{
    List<State> GetStates(string currentCultureName);
}
public class StateRepository : IStateRepository
{
    private readonly ApplicationContext _context;

    public StateRepository(ApplicationContext context)
    {
        _context = context;
    }
    public List<State> GetStates(string currentCultureName)
    {
        List<State> states = new List<State>();
        var dbStates = _context.States
                            .Include(i => i.Products)
                            .ToList();

        if (currentCultureName == "uz")
        {
            foreach (var dbState in dbStates)
            {
                var state = new State();
                state.Name = dbState.NameUzbek;
                state.Id = dbState.Id;
                state.Products = dbState.Products;

                states.Add(state);
            }
        }
        else if (currentCultureName == "ru")
        {
            foreach (var dbState in dbStates)
            {
                var state = new State();
                state.Name = dbState.NameRussian;
                state.Id = dbState.Id;
                state.Products = dbState.Products;

                states.Add(state);
            }
        }
        else if (currentCultureName == "en")
        {
            foreach (var dbState in dbStates)
            {
                var state = new State();
                state.Name = dbState.NameEnglish;
                state.Id = dbState.Id;
                state.Products = dbState.Products;

                states.Add(state);
            }
        }
        else
        {
            foreach (var dbState in dbStates)
            {
                var state = new State();
                state.Name = dbState.Name;
                state.Id = dbState.Id;
                state.Products = dbState.Products;

                states.Add(state);
            }
        }
        return states;
    }
}