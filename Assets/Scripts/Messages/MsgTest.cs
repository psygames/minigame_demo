using MessagePack;

[MessagePackObject]
public class MsgTest
{
    [Key(0)]
    public int num { get; set; }

    [Key(1)]
    public string date { get; set; }

    [IgnoreMember]
    public string info { get { return $"{num}:{date}"; } }
}