using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;

namespace RedisSample.Help
{
    public class CsvFileHelper : IFileHelper
    {
        private IFileProvider _fileProvider;
        
        public CsvFileHelper(IFileProvider fileProvider)
        {
            this._fileProvider = fileProvider;
        }

        public async Task<List<string>> GetContent(string path)
        {
            var tmp = await this.GetContentAsync(path);
            var result = new List<string>();
            var pm2point5 = tmp.Split(new string[] { "\n" }, StringSplitOptions.None).ToList().Where(p => p.Contains("PM2.5")).ToList();
            result.AddRange(pm2point5);

            return result;
        }

        private async Task<string> GetContentAsync(string path)
        {
            byte[] buffer;
            using (Stream readStream = this._fileProvider.GetFileInfo(path).CreateReadStream())
            {
                buffer = new byte[readStream.Length];
                await readStream.ReadAsync(buffer, 0, buffer.Length);
            }

            return Encoding.UTF8.GetString(buffer);
        }
    }
}