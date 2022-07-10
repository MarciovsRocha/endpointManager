using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace endpointManager
{
    public interface ILogicLayer
    {
        void Insert(string serialNumber, string model, int number, string firmwareVersion, string state);
        void Edit(string serialNumber, string state);
        void Delete(string serialNumber);
        string List();
        Endpoint Search(string serialNumber);
    }
}