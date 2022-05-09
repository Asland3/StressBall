using System;
using System.Collections.Generic;
using System.Linq;

namespace StressBall.Manager;

public class StressBallDBManager
{
    private readonly StressBallContext _stressBallContext;

    /// <summary>
    /// Dependency injection
    /// </summary>
    /// <param name="context"></param>
    public StressBallDBManager(StressBallContext context)
    {
        _stressBallContext = context;
    }

    /// <summary>
    /// Returns a list of stress balls if acceleration and date time is null otherwise it returns a filtered list
    /// </summary>
    /// <param name="accelerationFilter"></param>
    /// <param name="dateTimeFilter"></param>
    /// <returns></returns>
    public List<StressBallData> GetAll(double? accelerationFilter, DateTime? dateTimeFilter)
    {
        List<StressBallData> result = new List<StressBallData>(_stressBallContext.StressBall);

        /*if (!string.IsNullOrWhiteSpace(accelerationFilter))
        {
            result = result.FindAll(filterItem => filterItem.Speed.Contains(accelerationFilter, StringComparison.OrdinalIgnoreCase));
        }*/

        if (dateTimeFilter != null)
        {
            result = result.FindAll(filterItem => filterItem.DateTimeNow.Equals(dateTimeFilter));
        }

        return result.ToList();
    }

    /// <summary>
    /// Creates a new stress ball object and adds it to the list of stress balls 
    /// </summary>
    /// <param name="newStressBall"></param>
    /// <returns></returns>
    public StressBallData Add(StressBallData newStressBall)
    {
        newStressBall.Id = 0;
        _stressBallContext.StressBall.Add(newStressBall);
        _stressBallContext.SaveChanges();
        return newStressBall;
    }

    /// <summary>
    /// Gets a single stress ball from Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public StressBallData GetById(int id)
    {
        return _stressBallContext.StressBall.Find(id);
    }

    /// <summary>
    /// Gets a single stress ball from Id an updates it
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updates"></param>
    /// <returns></returns>
    public StressBallData Update(int id, StressBallData updates)
    {
        StressBallData stressBall = _stressBallContext.StressBall.Find(id);
        stressBall.Speed = updates.Speed;
        stressBall.DateTimeNow = updates.DateTimeNow;
        _stressBallContext.SaveChanges();
        return stressBall;
    }

    /// <summary>
    /// Removes a stress ball from list & returns the removed stress ball
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public StressBallData Delete(int id)
    {
        StressBallData stressball = _stressBallContext.StressBall.Find(id);
        _stressBallContext.StressBall.Remove(stressball);
        _stressBallContext.SaveChanges();
        return stressball;
    }

}

