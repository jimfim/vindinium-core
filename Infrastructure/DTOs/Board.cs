using System.Runtime.Serialization;

namespace vindiniumcore.Infrastructure.DTOs
{
  [DataContract]
    public class Board
    {
        [DataMember]
        public int size;

        [DataMember]
        public string tiles;
    }
}
