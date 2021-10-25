namespace AssaultSrtProvider.Representation
{
    public class Snapshot
    {
        public readonly Tag[] Tags;
        public readonly int Start;
        public readonly int End;
        
        public Snapshot(Tag[] tags, int start, int end)
        {
            Tags = tags;
            Start = start;
            End = end;
        }
    }
}