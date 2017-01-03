using System;
using RedisSample.Model.Entity;

namespace RedisSample.Model.FormatConvert
{
    public class PointInfoConvert : IConvert
    {
        public T FromJson<T>(string csvStr)
        {
            // var infos = csvStr.Split(',');
            // var data = new PointPerDayEntity {
            //     Date = DateTime.Parse(infos[0]),
            //     SiteName = infos[1]
            // };

            return default(T);
        }
    }
}