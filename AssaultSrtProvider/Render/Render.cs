using System;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.Collections.Generic;
using SkiaSharp;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
namespace AssaultSrtProvider.Representation
{
    class Render
    {
        public Snapshot[] Subslist;
        public Dictionary<string, string> files;
        public Engine MediatoolEngine = new Engine();
        public MediaFile video;
        public Render(Snapshot[] sublist,string tempfpath, string fontpath = @"C:\\Windows\\Fonts\\")
        {
            this.Subslist = sublist;
            this.files["fonts"] = fontpath;
            this.files["temp"] = tempfpath;
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
            var rawframe = new MediaFile(string.Format("{0}\\{1}.png", files["temp"], "tempframe"));
            MediatoolEngine.GetThumbnail(video, rawframe, new ConversionOptions { Seek = TimeSpan.FromSeconds(time) });
            var bitmap = new Bitmap(string.Format("{0}\\{1}.png", files["temp"], "tempframe"));
            var bmpd = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //initialize rendering
            var info = new SKImageInfo(bitmap.Width, bitmap.Height);
            var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            SKBitmap frame = new SKBitmap(info, (byte)bmpd.Scan0);
            canvas.DrawBitmap(frame, new SKPoint(info.Width, info.Height));
            //render subs
            foreach(var i in rendlist)
            {

            }
        }
    }
}