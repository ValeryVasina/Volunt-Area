using System;
using System.Collections.Generic;
using System.Text;

namespace VoluntArea
{
    // один источник
    public class Factory
    {
        private static Factory instance;
        private Factory() { }
        public static Factory Instance => instance ?? (instance = new Factory()); 

    }
}
