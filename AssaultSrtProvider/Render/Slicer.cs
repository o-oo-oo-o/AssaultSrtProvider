using System;
using System.IO;
using System.Collections.Generic;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;

namespace AssaultSrtProvider
{
    class Slicer
    {
        private string tempPath;
        private Engine MediaToolkitEngine = new Engine();
        public MediaFile video;
        public Slicer(string videofile,string tempPath)
        {
            this.tempPath = tempPath;
            video = new MediaFile(videofile);
            MediaToolkitEngine.GetMetadata(video);
        }
        public string get_frame(int time)
        {
            var options = new ConversionOptions { Seek = TimeSpan.FromSeconds(time)};
            var tempfile = new MediaFile { Filename = $"{tempPath}\\frame"};
            MediaToolkitEngine.GetThumbnail(video, tempfile, options);
            return (tempfile.Filename);
        }
    }
}