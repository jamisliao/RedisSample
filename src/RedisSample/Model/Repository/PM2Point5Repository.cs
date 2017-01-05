using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using RedisSample.Help;
using RedisSample.Model.Entity;
using StackExchange.Redis;

namespace RedisSample.Model.Repository
{
    public class PM2Point5Repository : IPM2Point5Repository
    {
        private readonly IFileHelper _fileHelper;
        private readonly ConnectionMultiplexer _connection;
        private readonly IDatabase _db;

        public PM2Point5Repository(IFileHelper fileHelper)
        {
            this._fileHelper = fileHelper;
            this._connection = ConnectionMultiplexer.Connect("127.0.0.1:6379");
            this._db = this._connection.GetDatabase();
        }

        public List<Station> GetDataByMonth(int month)
        {
            var result = default(List<Station>);
            if(RedisListExist(2015))
            {
                result = ReadHistoryDataFromRedis(2015, month);
            }
            else
            {
                var totalData = ReadHistoryDataFromFile(@"./wwwroot/data/2015-HistoryData.csv");
                PutToRedis(totalData, 2015);
                CreateMonthIndex(totalData, 2015);

                var date = new DateTime(2015, month, 1);
                date = date.AddMonths(1);
                result = totalData.Where(p => p.Date < date).ToList();
            }

            return result;
        }

        private List<Station> ReadHistoryDataFromFile(string path)
        {
            var content = this._fileHelper.GetContent(path).Result;

            var pointPerDayEnties = new List<Station>();
            foreach (var item in content)
            {
                var pointInfo = item.Split(',');
                var tmp = new Station
                {
                    Date = DateTime.Parse(pointInfo[0]),
                    SiteName = pointInfo[1],
                    Item = pointInfo[2],
                    PM2Point5Measurements = new List<double>()
                };

                for (int i = 3; i < 27; i++)
                {
                    var value = default(double);
                    if (double.TryParse(pointInfo[i], out value))
                    {
                        tmp.PM2Point5Measurements.Add(value);
                    }
                    else
                    {
                        tmp.PM2Point5Measurements.Add(-1);
                    }
                }

                pointPerDayEnties.Add(tmp);
            }

            
            return pointPerDayEnties;
        }

        private List<Station> ReadHistoryDataFromRedis(int year, int month)
        {
            var result = new List<Station>();

            var key = $"{year}-{month}-pm2point5history";
            var listRange = this._db.StringGet(key).ToString();
            var tmp = listRange.Split('-');
            var startIndex = Convert.ToInt64(tmp[0]);
            var endIndex = Convert.ToInt64(tmp[1]);

            var listKey = $"{year}-pm2point5history";
            var value = this._db.ListRange(listKey, startIndex, endIndex-1);
            foreach (var item in value)
            {
                var entity = JsonConvert.DeserializeObject<Station>(item);
                result.Add(entity);
            }

            return result;
        }

        private void PutToRedis(List<Station> entities, int year)
        {
            if (RedisListExist(year))
            {
                return;
            }

            foreach (var entity in entities)
            {
                var jsonStr = JsonConvert.SerializeObject(entity);
                this._db.ListRightPush($"{year}-pm2point5history", jsonStr);
            }
        }

        private bool RedisListExist(int year)
        {
            var key = $"{year}-pm2point5history";
            var isExist = this._db.KeyExists(key);

            return isExist;
        }

        private void CreateMonthIndex(List<Station> entities, int year)
        {
            var startDate = new DateTime(year, 1, 1);
            var startIndex = 0;
            var endIndex = 0;
            for (int i = 1; i <= 12; i++)
            {
                startDate = startDate.AddMonths(1);
                endIndex = entities.Count(p => p.Date < startDate);

                var key = $"{year}-{i}-pm2point5history";
                var value = $"{startIndex}-{endIndex}";
                this._db.StringSet(key, value, new TimeSpan(3650, 0, 0, 0));
                startIndex = endIndex;
            }
        }
    }
}