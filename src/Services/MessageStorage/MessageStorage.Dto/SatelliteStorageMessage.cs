using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageStorage.Dto
{
    public class SatelliteStorageMessage : StorageMessage
    {
        public string Name { get; set; }

        public StorageMessage GetBase()
        {
            return this;
        }
    }
}
