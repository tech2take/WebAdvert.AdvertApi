using AdvertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Services
{
    public interface IAdverStorageService
    {
        public Task<string> Add(AdvertModel model);
       public  Task Confirm(ConfirmAdvertModel model);
    }
}
