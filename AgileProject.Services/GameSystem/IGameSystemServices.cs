using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileProject.Models.GameSystem;

namespace AgileProject.Services.GameSystem
{
    public interface IGameSystemService
    {
        Task<bool> InputGameSystemAsync(GSRegister request);
        
        Task<bool> RemoveGameSystemAsync(string name);

    }
}