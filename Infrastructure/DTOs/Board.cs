using System.Runtime.Serialization;

namespace vindiniumcore.Infrastructure.DTOs
{
  [DataContract]
    class Board
    {
        [DataMember]
        internal int size;

        [DataMember]
        internal string tiles;
    }
}
