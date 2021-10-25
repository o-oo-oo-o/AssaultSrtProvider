using AssaultSrtProvider.Representation;
using System.Collections.Generic;

namespace AssaultSrtProvider.Interfaces
{
    public interface IDataProvider
    {
        public IEnumerable<Snapshot> Snapshots();
    }
}