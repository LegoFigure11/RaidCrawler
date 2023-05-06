using PKHeX.Core;

namespace pkNX.Structures.FlatBuffers.Gen9;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class PokeDataBattle
{
    public void SerializePKHeX(BinaryWriter bw, sbyte captureLv, RaidSerializationFormat format)
    {
        if (format == RaidSerializationFormat.BaseROM)
            AssertRegularFormat();

        // If any PointUp for a move is nonzero, throw an exception.
        if (Waza1.PointUp != 0 || Waza2.PointUp != 0 || Waza3.PointUp != 0 || Waza4.PointUp != 0)
            throw new ArgumentOutOfRangeException(nameof(WazaSet.PointUp), $"No {nameof(WazaSet.PointUp)} allowed!");

        // flag BallId if not none
        if (BallId != 0)
            throw new ArgumentOutOfRangeException(nameof(BallId), BallId, $"No {nameof(BallId)} allowed!");

        bw.Write(SpeciesConverter.GetNational9(DevId));
        bw.Write((byte)FormId);
        bw.Write((byte)Sex);

        bw.Write((byte)Tokusei);
        bw.Write((byte)(TalentType == 1 ? TalentVnum : 0));
        bw.Write((byte)RareType);
        bw.Write((byte)captureLv);

        // Write moves
        bw.Write(Waza1.WazaId);
        bw.Write(Waza2.WazaId);
        bw.Write(Waza3.WazaId);
        bw.Write(Waza4.WazaId);

        // ROM raids with 5 stars have a few entries that are defined as DEFAULT
        // If the type is not {specified}, the game will assume it is RANDOM.
        // Thus, DEFAULT behaves like RANDOM.
        // Let's clean up this mistake and make it explicit so we don't have to program this workaround in other tools.
        var gem = GemType is 0 ? 1 : GemType;
        bw.Write((byte)gem);
    }

    private void AssertRegularFormat()
    {
        if (TalentType != 1)
            throw new ArgumentOutOfRangeException(nameof(TalentType), TalentType, "Invalid talent type.");

        if (TalentVnum == 0 && DevId != (ushort)Species.Pachirisu && Level != 35) // nice mistake gamefreak -- 3star Pachirisu is 0 IVs.
            throw new ArgumentOutOfRangeException(nameof(TalentVnum), TalentVnum, "No min flawless IVs?");

        if (Seikaku != 0)
            throw new ArgumentOutOfRangeException(nameof(Seikaku), Seikaku, $"No {nameof(Seikaku)} allowed!");
    }
}
