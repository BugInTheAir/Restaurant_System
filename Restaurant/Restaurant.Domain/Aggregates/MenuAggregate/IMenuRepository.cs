using Restaurant.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Aggregates.MenuAggregate
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Menu Add(Menu menu);
        Menu Update(Menu menu);
        Task<Menu> FindByIdAsync(string id);
        Task<Menu> FindByNameAsync(string name);
    }
}
