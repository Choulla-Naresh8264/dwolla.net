using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dwolla.net.SerializableTypes
{
    /// <summary>
    /// LINQ-serializable structure for the "Dwolla envelope".
    /// </summary>
    class DwollaResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }
    }
}
