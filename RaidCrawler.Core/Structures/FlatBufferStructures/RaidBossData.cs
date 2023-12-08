namespace pkNX.Structures.FlatBuffers.Gen9;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class RaidBossData
{
    public void SerializePKHeX(BinaryWriter bw)
    {
        bw.Write(ExtraAction1.Wazano);
        bw.Write(ExtraAction2.Wazano);
        bw.Write(ExtraAction3.Wazano);
        bw.Write(ExtraAction4.Wazano);
        bw.Write(ExtraAction5.Wazano);
        bw.Write(ExtraAction6.Wazano);
    }
}
