using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDC.IService
{
    public interface ICharacterService
    {
        Task<string> GetDefaultCharacterImage();
        Task<bool> UpdateCharacterImage(string username, byte[] imageData); // Optional für später
    }
}

