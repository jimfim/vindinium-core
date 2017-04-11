using System.Runtime.Serialization;

namespace vindiniumcore.Infrastructure.DTOs
{
  [DataContract]
  internal class Pos
  {
    [DataMember]
    internal int x;

    [DataMember]
    internal int y;
  }
}