using System;
using System.IO;
using System.Collections.Generic;
using SkiaSharp;


namespace AssaultSrtProvider
{
    class Render
    {
        public StyleCollector styleCollector;
        public Representation.Snapshot[] subs;
        public Slicer slicer;
        public Render(Representation.Snapshot[] subs,Slicer slicer,string bindfilepath = null)
        {
            this.subs = subs;
            this.styleCollector = new StyleCollector(bindfilepath);
            this.slicer = slicer;
        }
        public void rendsub(SKCanvas canvas,Representation.Snapshot sub,int x,int y)
        {
            var styles = styleCollector.get_sub_style_info(sub);
            foreach(var style in styles)
            {
                var contains = style.contains;
                var font = SKTypeface.FromFile(contains["font"]);
                SKPaint paint = new SKPaint(font.ToFont());
                paint.Color = new SKColor(contains["color"]);
                paint.BlendMode = contains["blendmode"];
                paint.MaskFilter = SKMaskFilter.CreateBlur(contains["blurtype"], contains["blur"]);
                paint.TextSize = contains["size"];
                paint.Shader = contains["shader"];
                if (contains["image"])
                {
                    SKBitmap bmp = SKBitmap.Decode(contains["image"]);
                    canvas.DrawBitmap(bmp, contains["pos"][0] + x, contains["pos"][1] + y);
                }
                canvas.DrawText(contains["text"], contains["pos"][0] + x, contains["pos"][1] + y, paint);
            }
        }

        public SKSurface rend(Representation.Snapshot[] subs, string framefile, int x = 0, int y = 0)
        {
            //Create surface&canvas 4 render
            var frame = SKBitmap.Decode(framefile);
            SKImageInfo info = new SKImageInfo(frame.Width,frame.Height);
            SKSurface surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            //Draw frame
            canvas.DrawBitmap(frame, x, y);
            //Rens subs
            foreach(var sub in subs)
            {
                rendsub(canvas,sub,x,y);
            }

            return surface;
        }
        public SKSurface rend(Representation.Snapshot[] subs, int time, int x = 0, int y = 0)
        {
            //select subs to rend
            var substorend = new List<Representation.Snapshot>();
            foreach(var i in subs)
            {
                if (i.Start < time&time < i.End)
                {
                    substorend.Add(i);
                }
            }
            //get frame
            var framefile = slicer.get_frame(time);
            //Create surface&canvas 4 render
            var frame = SKBitmap.Decode(framefile);
            SKImageInfo info = new SKImageInfo(frame.Width, frame.Height);
            SKSurface surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            //Draw frame
            canvas.DrawBitmap(frame, x, y);
            //Rens subs
            foreach (var sub in substorend)
            {
                rendsub(canvas, sub, x, y);
            }

            return surface;
        }
    }
}