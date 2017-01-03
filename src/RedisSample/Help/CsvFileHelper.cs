using System.Collections.Generic;
using System.Linq;

namespace RedisSample.Help
{
    public class CsvFileHelper : IFileHelper
    {
        public List<string> GetContent(string path)
        {
            var contents = System.IO.File.ReadLines(path).ToList();
            return contents;
        }
    }
}