using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Posology.Core
{
    public class FileHelper
    {
        private const int DefaultBufferSize = 4096;
        private const FileOptions DefaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

        public static async Task<IEnumerable<string>> AsyncGetFiles(string directory)
        {
            return Directory.GetFiles(directory);
        }


        public static async Task<string[]> ReadAllLinesAsync(string directory, string filename, Encoding encoding)
        {
            var lines = new List<string>();

            // Open the FileStream with the same FileMode, FileAccess
            // and FileShare as a call to File.OpenText would've done.
            var baseDirectory = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(baseDirectory, directory, filename);
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