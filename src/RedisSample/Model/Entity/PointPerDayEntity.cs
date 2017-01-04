using System;
using System.Collections.Generic;

namespace RedisSample.Model.Entity
{
    public class PointPerDayEntity
    {
        public string SiteName { get; set; }

        public List<double> PM2Point5Values { get; set; }

        public DateTime Date { get; set; }

        public string Item { get; set; }
    }
}