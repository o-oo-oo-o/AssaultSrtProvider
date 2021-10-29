using System;
using System.IO;
using System.Collections.Generic;
using SkiaSharp;


namespace AssaultSrtProvider
{
    class Render
    {
        public TagTranslator td;
        public Render(string bindfilepath = null)
        {
            this.td = new TagTranslator(bindfilepath);
        }
        private void rendsub(SKCanvas canvas,Representation.Snapshot sub,int x,int y)
        {
            var style = td.get_sub_style_info(sub);
            if (style.ContainsKey("rendtext")){
                for (int i = 0; i < style["rendtext"]; i++)
                {
                    var lvl = (i).ToString();
                    var font = SKTypeface.FromFile(style["fontpath" + lvl] + style["fontname"+lvl]);
                    SKPaint paint = new SKPaint(font.ToFont());
                    paint.Color = new SKColor(style["color" + lvl]);
                    paint.BlendMode = style["blendmode" + lvl];
                    paint.MaskFilter = SKMaskFilter.CreateBlur(style["blurtype" + lvl], style["blur" + lvl]);
                    paint.TextSize = style["textsize" + lvl];
                    paint.Shader = style["shader" + lvl];
                    canvas.DrawText(style["text" + lvl], style["position" + lvl][0]+x, style["position" + lvl][1]+y, paint);
                }
            }
            if (style.ContainsKey("rendimage"))
            {
                for(int i = 0; i < style["rendimage"]; i++)
                {
                    var lvl = (i).ToString();
                    SKBitmap bitm = SKBitmap.Decode(style["imagefile"+lvl]);
                    canvas.DrawBitmap(bitm, style["imageposition" + lvl][0]+x, style["imageposition" + lvl][1]+y);
                }
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