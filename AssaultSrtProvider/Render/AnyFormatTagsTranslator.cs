using System;
using System.IO;
using System.Collections.Generic;
using SkiaSharp;

namespace AssaultSrtProvider
{
    class TagTranslator
    {
        public Dictionary<string, string> binding = new Dictionary<string, string>();
        public TagTranslator(string bindfile = null)
        {
            if (bindfile != null)
            {
                var binds = File.OpenText(bindfile);
                foreach (var i in binds.ReadToEnd().Split("\n"))
                {
                    
                    var j = i.Trim((" ").ToCharArray()).Split("|");
                    binding.Add(j[0], j[1]);
                }
            }
        }
        public Dictionary<string,dynamic> get_sub_style_info(Representation.Snapshot sub)
        {
            
        }
    }
}