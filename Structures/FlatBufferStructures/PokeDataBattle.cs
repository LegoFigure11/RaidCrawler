using FlatSharp.Attributes;
using System.ComponentModel;
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedType.Global
#nullable disable

namespace RaidCrawler.Structures;

[FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
public class WazaSet
{
    [FlatBufferItem(0)] public ushort WazaId { get; set; }
    [FlatBufferItem(1)] public sbyte PointUp { get; set; }
}

[FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
public class ParamSet
{
    [FlatBufferItem(0)] public int HP { get; set; }
    [FlatBufferItem(1)] public int ATK { get; set; }
    [FlatBufferItem(2)] public int DEF { get; set; }
    [FlatBufferItem(3)] public int SPA { get; set; }
    [FlatBufferItem(4)] public int SPD { get; set; }
    [FlatBufferItem(5)] public int SPE { get; set; }
}

[FlatBufferTable, TypeConverter(typeof(ExpandableObjectConverter))]
public class PokeDataBattle
{
    [FlatBufferItem(00)] public ushort DevId { get; set; }
    [FlatBufferItem(01)] public short FormId { get; set; }
    [FlatBufferItem(02)] public int Sex { get; set; }
    [FlatBufferItem(03)] public int Item { get; set; }
    [FlatBufferItem(04)] public int Level { get; set; }
    [FlatBufferItem(05)] public int BallId { get; set; }
    [FlatBufferItem(06)] public int WazaType { get; set; }
    [FlatBufferItem(07)] public WazaSet Waza1 { get; set; }
    [FlatBufferItem(08)] public WazaSet Waza2 { get; set; }
    [FlatBufferItem(09)] public WazaSet Waza3 { get; set; }
    [FlatBufferItem(10)] public WazaSet Waza4 { get; set; }
    [FlatBufferItem(11)] public int GemType { get; set; }
    [FlatBufferItem(12)] public int Seikaku { get; set; }
    [FlatBufferItem(13)] public int Tokusei { get; set; }
    [FlatBufferItem(14)] public int TalentType { get; set; }
    [FlatBufferItem(15)] public ParamSet TalentValue { get; set; }
    [FlatBufferItem(16)] public sbyte TalentVnum { get; set; }
    [FlatBufferItem(17)] public ParamSet EffortValue { get; set; }
    [FlatBufferItem(18)] public int RareType { get; set; }
    [FlatBufferItem(19)] public int ScaleType { get; set; }
    [FlatBufferItem(20)] public short ScaleValue { get; set; }

    public void SerializePKHeX(BinaryWriter bw, sbyte captureLv)
    {
        //if (TalentType != 1)
        //    throw new ArgumentOutOfRangeException(nameof(TalentType), TalentType, "No min flawless IVs?");
        //if (TalentVnum == 0 && DevId != 417 && Level != 35) // DEV_PATIRISU = 417
        //    throw new ArgumentOutOfRangeException(nameof(TalentVnum), TalentVnum, "No min flawless IVs?");

        //if (Seikaku != 0)
        //    throw new ArgumentOutOfRangeException($"No {Seikaku} allowed!");

        // If any PointUp for a move is nonzero, throw an exception.
        if (Waza1.PointUp != 0 || Waza2.PointUp != 0 || Waza3.PointUp != 0 || Waza4.PointUp != 0)
            throw new ArgumentOutOfRangeException($"{Waza1.PointUp} | {Waza2.PointUp} | {Waza3.PointUp} | {Waza4.PointUp}");

        // flag BallId if not none
        if (BallId != 0)
            throw new ArgumentOutOfRangeException($"No {BallId} allowed!");

        bw.Write(DevId);
        bw.Write((byte)FormId);
        bw.Write((byte)Sex);

        bw.Write((byte)Tokusei);
        bw.Write((byte)TalentVnum);
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
}

public enum RaidSerializationFormat
{
    /// <summary>
    /// Base ROM Raids
    /// </summary>
    BaseROM,

    /// <summary>
    /// Regular Distribution Raids
    /// </summary>
    Type2,

    /// <summary>
    /// 7 Star Distribution Raids
    /// </summary>
    Type3,
}