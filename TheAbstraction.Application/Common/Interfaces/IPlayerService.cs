using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAbstraction.Application.Common.Interfaces
{
    public interface IPlayerService
    {
        Task<int> Create(string userId, string namePlayer);
        Task<int> Delete(string playerId); // Thêm phương thức xóa người chơi
    }
}

