using System.Collections.Generic;
using RedisSample.Model.Entity;

namespace RedisSample.Model.Repository
{
    public interface IPM2Point5Repository
    {
        List<PointPerDayEntity> GetPointHistory();
    }
}