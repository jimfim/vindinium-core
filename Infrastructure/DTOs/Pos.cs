using System.Runtime.Serialization;

namespace vindiniumcore.Infrastructure.DTOs
{
  [DataContract]
  public class Pos
  {
    [DataMember]
    public int x;

    [DataMember]
    public int y;
  }
}