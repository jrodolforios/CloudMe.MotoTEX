using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries { 
    public class EntryBase
    {
        public bool IsDeleted { get; set; } // logical deletion flag
        public bool ForceDelete { get; set; } = false; // forces 'physical' deletion
    }
}
