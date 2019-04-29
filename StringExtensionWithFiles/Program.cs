using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringExtension;

namespace StringExtensionWithFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = ConfigurationManager.AppSettings["path"];
            var fileGenerator = new RandomCharsFileGenerator(path);
           
            StringConverter sc = new StringConverter();
           
            Stopwatch sw = new Stopwatch();
            for (int i = 1, power = 1; i <= 5; i++)
            {
                var contentLength = 10 * power;
                fileGenerator.GenerateFiles(1, contentLength);
                power *= 10;
                string source = File.ReadAllText(path);

                sw.Start();
                sc.Convert(source, Int32.MaxValue);
                sw.Stop();

                Console.WriteLine($"Content length {contentLength}. Time in milliseconds {sw.ElapsedMilliseconds}.");
                sw.Reset();
            }                
            Console.ReadKey();
        }
    }

    public class RandomCharsFileGenerator
    {
        private readonly string path;

        public RandomCharsFileGenerator(string fileName)
        {
            this.path = $"{AppDomain.CurrentDomain.BaseDirectory}{fileName}";
        }

        public void GenerateFiles(int filesCount, int contentLength)
        {
            for (var i = 0; i < filesCount; ++i)
            {
                var generatedFileContent = this.GenerateFileContent(contentLength);

                File.WriteAllBytes(path, generatedFileContent);
            }
        }

        private byte[] GenerateFileContent(int contentLength)
        {
            var generatedString = this.RandomString(contentLength);

            var bytes = Encoding.Unicode.GetBytes(generatedString);

            return bytes;
        }

        private string RandomString(int size)
        {
            var random = new Random();

            const string input = "abcdefghijklmnopqrstuvwxyz0123456789";

            var chars = Enumerable.Range(0, size)
                                  .Select(x => input[random.Next(0, input.Length)]);

            return new string(chars.ToArray());
        }
    }
}
