using System.Collections.Generic;

namespace RedisSample.Help
{
    public interface IFileHelper
    {
        List<string> GetContent(string path);
    }
}