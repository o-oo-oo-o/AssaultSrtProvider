using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AssaultSrtProvider.Interfaces;
using AssaultSrtProvider.Representation;

namespace AssaultSrtProvider.Providers
{
    public class SrtProvider : IDataProvider
    {
        private readonly string _filePath;
        private readonly float _framerate;

        public SrtProvider(string filePath, float framerate)
        {
            _filePath = filePath;
            _framerate = framerate;
        }
        
        public Snapshot GenSnapshot(string text, int start, int end)
        {
            return new Snapshot(new[] {new Tag(text)}, start, end);
        }
        
        public IEnumerable<Snapshot> Snapshots()
        {
            {
                var content = File.OpenText(_filePath).ReadToEnd();
                Console.WriteLine(content.Split("\r\n\r\n").Length);
                foreach (var row in content.Split("\r\n\r\n"))
                {
                    if (row == "") continue;
                    var splitted = row.Split("\r\n");
                    var rowNumber = splitted[0].Trim();
                    var times = splitted[1].Trim();
                    // Match[] style = Regex.Matches(splitted[2], "{.*?}").ToArray() + Regex.Matches(splitted[2]).ToArray();
                    var text = Regex.Replace(splitted[2], @"{.*?}", "").Trim(); 
                    text = Regex.Replace(text, @"<.*?>", "").Trim();
                    var start = Convert.ToInt32(Math.Floor(TimeSpan.ParseExact(times.Split(" --> ")[0], @"hh\:mm\:ss\,fff", null).TotalSeconds*_framerate));
                    var end = Convert.ToInt32(Math.Floor(TimeSpan.ParseExact(times.Split(" --> ")[1], @"hh\:mm\:ss\,fff", null).TotalSeconds*_framerate));
                    yield return GenSnapshot(text, start, end);
                }
            }
        }
    }
}