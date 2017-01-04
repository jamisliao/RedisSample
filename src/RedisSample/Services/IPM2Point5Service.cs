using System.Collections.Generic;
using RedisSample.Model.Entity;

namespace RedisSample.Services
{
    public interface IPM2Point5Service
    {
        List<PointPerDayEntity> GeDataByNonth(int month);
    }
}