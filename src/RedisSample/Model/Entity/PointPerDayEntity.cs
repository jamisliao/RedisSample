using System;
using System.Collections.Generic;

namespace RedisSample.Model.Entity
{
    public class PointPerDayEntity
    {
        public string SiteName { get; set; }

        public string County { get; set; }

        public Dictionary<int, int> PM2Point5Dict { get; set; }

        public DateTime Date { get; set; }

        public string PublishTime { get; set; }
    }
}