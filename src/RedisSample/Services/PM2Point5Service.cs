using System.Collections.Generic;
using RedisSample.Model.Entity;
using RedisSample.Model.Repository;

namespace RedisSample.Services
{
    public class PM2Point5Service : IPM2Point5Service
    {
        private IPM2Point5Repository _pm2point5Repo;

        public PM2Point5Service(IPM2Point5Repository pm2point5Repo)
        {
            this._pm2point5Repo = pm2point5Repo;
        }

        public List<Station> GeDataByNonth(int month)
        {
            var result = this._pm2point5Repo.GetDataByMonth(month);

            return result;
        }
    }
}