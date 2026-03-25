using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using PiShockTheSpire2.PiShockTheSpire2Code.Utils;

namespace PiShockTheSpire2.PiShockTheSpire2Code.Powers;

public class Insulation() : CustomPowerModel
{
    
    public override string CustomPackedIconPath => "mercy.png".PowerImagePath();
    public override string CustomBigIconPath => "mercy.png".BigPowerImagePath();
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    //Todo: should scale in multiplayer? i genuinely have no idea of what does this mean.
    //
    // nani dafuck is public override IEnumerable<DynamicVar> CanonicalVars => new global::_003C_003Ez__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Decrement", 1m));
    // HoverTip ???
    
    // TODO: Call this countdown at the beginning of player turn, not at the end of enemy turn.
    // AfterTurnEnd is great to call the PiShock API, TODO: use this in the collar relic.
    // I can now consistently trigger discharges by taking damage after the end of BOTH the player and the enemy turn.
    // This is fine :)
    /*public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Enemy)
        {
            if (base.Owner.Side == CombatSide.Enemy)
            {
                await PowerCmd.ModifyAmount(this, -base.DynamicVars["Decrement"].BaseValue, null, null);
            }
            else
            {
                await PowerCmd.Decrement(this);
            }
        }
    }*/
    
    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side == base.Owner.Side)
        {
            Flash();
            await PowerCmd.Decrement(this);
        }
    }
    
    
    
}