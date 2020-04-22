using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Directory
{
    public class FileHelper
    {
        private const int DefaultBufferSize = 4096;
        private const FileOptions DefaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

        public static async Task<string[]> ReadAllLinesAsync(string rootDirectory, string directory, string filename, Encoding encoding)
        {
            var lines = new List<string>();

            var filePath = Path.Combine(rootDirectory, directory, filename);
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, DefaultOptions))
            using (var reader = new StreamReader(stream, encoding))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines.ToArray();
        }
    }
}