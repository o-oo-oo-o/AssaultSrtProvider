using System;
using System.IO;
using System.Collections.Generic;
using SkiaSharp;
using MediaToolkit;
using MediaToolkit.Model;
namespace AssaultSrtProvider.Representation
{
    class Render
    {
        public Snapshot[] Subslist;
        public Dictionary<string, string> files;
        public Engine MediatoolEngine = new Engine();
        public MediaFile video;
        public Render(Snapshot[] sublist, string fontpath = @"C:\\Windows\\Fonts\\")
        {
            this.Subslist = sublist;
            this.files["fonts"] = fontpath;
        }
        public Render(Snapshot[] sublist, Dictionary<string, string> files)
        {
            this.Subslist = sublist;
            this.files = files;
        }
        public void load_video(string Video)
        {
            video = new MediaFile(Video);
            MediatoolEngine.GetMetadata(video);
            var i = 0;
            for (int i = 0; i < video.Metadata.Duration.Seconds;i++)
            {
                var options = new ConversionOptions { Seek = TimeSpan.FromSeconds(i) };
                var outputFile = new MediaFile { Filename = string.Format("{0}\\image-{1}.jpeg", outputPath, i) };
                engine.GetThumbnail(mp4, outputFile, options);
            }
        }
        public void rendFrame(dynamic time)
        {
            //get alife subs
            var rendlist = new List<Snapshot>();
            foreach (var i in Subslist)
            {
                if (i.Start <= time <= i.End)
                {
                    rendlist.Add(i);
                }
            }
            //get frame from video
            

            //initialize rendering
            var surface = SKSurface.Create()
        }
    }
}