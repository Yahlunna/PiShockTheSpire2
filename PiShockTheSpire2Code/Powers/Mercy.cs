using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Entities.Powers;
using PiShockTheSpire2.PiShockTheSpire2Code.Utils;

namespace PiShockTheSpire2.PiShockTheSpire2Code.Powers;

public class Mercy() : CustomPowerModel
{
    public override string CustomPackedIconPath => "mercy.png".PowerImagePath();
    public override string CustomBigIconPath => "mercy.png".BigPowerImagePath();
    
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
}