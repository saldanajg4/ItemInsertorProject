using System;
using System.Collections.Generic;
using DeskBooker.Core.Domain;

namespace DeskBooker.Core.DataInterface
{
    public interface IDeskRepository
    {
        //get available desks for specific date
         IEnumerable<Desk> GetAvailableDesks(DateTime date);
    }
}