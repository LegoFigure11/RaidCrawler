namespace pkNX.Structures.FlatBuffers.Gen9;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class RaidEnemyInfo
{
    public void SerializePKHeX(
        BinaryWriter bw,
        byte star,
        sbyte rate,
        RaidSerializationFormat format
    )
    {
        BossPokePara.SerializePKHeX(bw, CaptureLv, format);
        BossPokeSize.SerializePKHeX();
        bw.Write(DeliveryGroupID);

        // Append RNG details.
        bw.Write(star);
        bw.Write(rate);
    }

    public void SerializeDistribution(BinaryWriter bw)
    {
        var b = BossPokePara;
        if (b.TalentType > 2)
            throw new InvalidDataException($"Invalid talent type for {nameof(SerializeDistribution)}.");

        bw.Write(b.Seikaku == 0 ? (byte)25 : (byte)(b.Seikaku - 1));
        bw.Write((byte)b.TalentValue.HP);
        bw.Write((byte)b.TalentValue.ATK);
        bw.Write((byte)b.TalentValue.DEF);
        bw.Write((byte)b.TalentValue.SPE);
        bw.Write((byte)b.TalentValue.SPA);
        bw.Write((byte)b.TalentValue.SPD);
        bw.Write((byte)(b.TalentType == 2 ? 1 : 0));
        bw.Write((byte)b.ScaleType);
        bw.Write((byte)b.ScaleValue);
    }

    public void SerializeMight(BinaryWriter bw)
    {
        // Fixed Nature, fixed IVs, fixed Scale
        var b = BossPokePara;
        if (b.TalentType > 2)
            throw new InvalidDataException($"Invalid talent type for {nameof(SerializeMight)}.");

        bw.Write(b.Seikaku == 0 ? (byte)25 : (byte)(b.Seikaku - 1));
        bw.Write((byte)b.TalentValue.HP);
        bw.Write((byte)b.TalentValue.ATK);
        bw.Write((byte)b.TalentValue.DEF);
        bw.Write((byte)b.TalentValue.SPE);
        bw.Write((byte)b.TalentValue.SPA);
        bw.Write((byte)b.TalentValue.SPD);
        bw.Write((byte)(b.TalentType == 2 ? 1 : 0));
        bw.Write((byte)b.ScaleType);
        bw.Write((byte)b.ScaleValue);
    }
}
