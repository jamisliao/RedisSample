using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedisSample.Help
{
    public interface IFileHelper
    {
        Task<List<string>> GetContent(string path);
    }
}