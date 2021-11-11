namespace AssaultSrtProvider.Representation
{
    public class Tag
    {
        public string Text;
        public Style Style;

        public new string ToString()
        {
            return Text;
        }
        public Tag(string text, Style style)
        {
            Style = style;
            Text = text;
        }

    }
}