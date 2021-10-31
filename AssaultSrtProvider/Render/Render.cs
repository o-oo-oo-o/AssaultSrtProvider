using System;
using System.IO;
using System.Collections.Generic;
using SkiaSharp;


namespace AssaultSrtProvider
{
    class Render
    {
        public StyleCollector styleCollector;
        public Render(string bindfilepath = null)
        {
            this.styleCollector = new StyleCollector(bindfilepath);
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

        public SKSurface rend(Representation.Snapshot[] subs,SKBitmap frame, int x = 0,int y = 0)
        {
            //Create surface&canvas 4 render
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
    }
}