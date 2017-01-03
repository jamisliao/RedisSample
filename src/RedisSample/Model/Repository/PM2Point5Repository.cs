using System.Collections.Generic;
using Newtonsoft.Json;
using RedisSample.Help;
using RedisSample.Model.Entity;
using RedisSample.Model.FormatConvert;

namespace RedisSample.Model.Repository
{
    public class PM2Point5Repository : IPM2Point5Repository
    {
        IFileHelper _fileHelper;

        private IConvert _convert;

        public PM2Point5Repository(IConvert convert, IFileHelper fileHelper)
        {
            this._convert = convert;
            this._fileHelper = fileHelper;
        }

        public List<PointPerDayEntity> GetPointHistory()
        {
            var content = this._fileHelper.GetContent(@"./wwwroot/data/HistoryData.csv");

            //// remove csv header
            content.RemoveAt(0);
            var pointPerDayEnties = new List<PointPerDayEntity>();
            foreach (var item in content)
            {
                var sourceStr = this._convert.FromJson<PointPerDayEntity>(item);
                //var tmp = JsonConvert.DeserializeObject<PointPerDayEntity>(sourceStr);
                pointPerDayEnties.Add(sourceStr);
            }

            return pointPerDayEnties;
        }
    }
}