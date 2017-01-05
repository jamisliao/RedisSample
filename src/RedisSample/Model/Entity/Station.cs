using System;
using System.Collections.Generic;

namespace RedisSample.Model.Entity
{
    public class Station
    {
        public string SiteName { get; set; }

        public List<double> PM2Point5Measurements { get; set; }

        public DateTime Date { get; set; }

        public string Item { get; set; }
    }
}