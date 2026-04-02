using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.RelicPools;
using PiShockTheSpire2.PiShockTheSpire2Code.Cards;
using PiShockTheSpire2.PiShockTheSpire2Code.Powers;
using PiShockTheSpire2.PiShockTheSpire2Code.Utils;

namespace PiShockTheSpire2.PiShockTheSpire2Code.Relics;

[Pool(typeof(SharedRelicPool))]
public class PishockCollar() : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Starter;
    public override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)]; 
    public override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromCard<Safeword>()];
    
    
    // TODO: Sanitize and generalize this.
    public override string PackedIconPath => "pishockcollar.png".RelicImagePath();
    public override string PackedIconOutlinePath => "pishockcollar_outline.png".RelicImagePath();
    public override string BigIconPath => "pishockcollar.png".BigRelicImagePath();
    
    
    
    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
    {
        if (player == base.Owner && combatState.RoundNumber == 1)
        {
            List<CardModel?> list = new List<CardModel?>();
            list.Add(base.Owner.Creature.CombatState?.CreateCard<Safeword>(base.Owner));
            Flash();
            
            await CardPileCmd.AddGeneratedCardsToCombat(list!, PileType.Hand, addedByPlayer: true);
        }
    }
    
    
    
    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Enemy)
        {
            // Todo: trigger shock damage   
        }
        else
        {
            // Todo: mahyaps trigger shock damage taken on turn? 
        }
    }
    
}