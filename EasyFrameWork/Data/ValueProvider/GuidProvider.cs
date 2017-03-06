/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;

namespace Easy.Data.ValueProvider
{
    public class GuidProvider : IValueProvider
    {

        public object GenerateValue()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}