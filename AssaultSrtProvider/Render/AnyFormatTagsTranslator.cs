using System;
using System.IO;
using System.Collections.Generic;
using SkiaSharp;

namespace AssaultSrtProvider
{
    class Style
    {
        public Dictionary<string, dynamic> contains = new Dictionary<string, dynamic>();
        public Style()
        {
            this.contains.Add("pos",new int[] { 100, 100 });
            this.contains.Add("text", "text");
            this.contains.Add("size", 100);
            this.contains.Add("font", "C:\\Windows\\Fonts\\arial.ttf");
            this.contains.Add("color", new List<byte> { (byte)225, (byte)225, (byte)225, (byte)225 });
            this.contains.Add("blendmode", null);
            this.contains.Add("style",0);
            this.contains.Add("shader", null);
            this.contains.Add("blurtype",3);
            this.contains.Add("blur",0.0f);
            this.contains.Add("image", null);
            this.contains.Add("resize", null);
        }
    }
    class TagTranslator
    {
        public Dictionary<string, string> binding = new Dictionary<string, string>();
        public TagTranslator(string bindfile)
        {
            var binds = File.OpenText(bindfile).ReadToEnd();
            foreach(var bind in binds)
            {
                var info = bind.ToString().Split("|");
                binding.Add(info[0], info[1]);
            }
            
        }
        
    }
    class StyleCollector
    {
        public TagTranslator tt;
        public StyleCollector(string bindfile = null)
        {
            tt = new TagTranslator(bindfile);
        }
        public Style[] get_sub_style_info(Representation.Snapshot sub)
        {
            var stl = new List<Style>();
            for(int i = 0; i < sub.elc; i++)
            {
                var tempstyle = new Style();
                foreach(var tag in sub.Tags)
                {
                    foreach(var bind in tt.binding.Keys)
                    {
                        if (tag.Text.StartsWith(bind + i.ToString()))
                        {
                            tempstyle.contains[tt.binding[bind]] = (tempstyle.contains[tt.binding[bind]].GetType())(tag.Text.Split(bind + i.ToString()));
                        }else if (tag.Text.StartsWith(bind)){
                            tempstyle.contains[tt.binding[bind]] = (tempstyle.contains[tt.binding[bind]].GetType())(tag.Text.Split(bind));
                        }
                    }
                }
                stl.Add(tempstyle);
            }
            return (stl.ToArray());
        }
    }
}