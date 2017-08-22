using System.Runtime.Serialization;

namespace vindiniumcore.Infrastructure.DTOs
{
  [DataContract]
  public class Hero
  {
    [DataMember]
    public int id;

    [DataMember]
    public string name;

    [DataMember]
    public int elo;

    [DataMember]
    public Pos pos;

    [DataMember]
    public int life;

    [DataMember]
    public int gold;

    [DataMember]
    public int mineCount;

    [DataMember]
    public Pos spawnPos;

    [DataMember]
    public bool crashed;
  }
}