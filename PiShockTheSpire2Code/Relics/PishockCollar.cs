using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.ValueProps;
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
    public override string PackedIconPath => "pishockcollar.png".RelicImagePath();
    public override string PackedIconOutlinePath => "pishockcollar_outline.png".RelicImagePath();
    public override string BigIconPath => "pishockcollar.png".BigRelicImagePath();
    
    private int damageTakenThisTurn = 0;
    
    
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
    
    
    public override Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (CombatManager.Instance.IsInProgress && target == base.Owner.Creature && result.UnblockedDamage > 0)
        {
            damageTakenThisTurn += result.UnblockedDamage;
        }
        else
        {
            if (target == base.Owner.Creature && result.UnblockedDamage > 0)
            {
                _ = TriggerShock(
                    CalculateOperationDuration(result.UnblockedDamage, base.Owner.Creature.MaxHp),
                    CalculateOperationIntensity(result.UnblockedDamage, base.Owner.Creature.MaxHp));
            }
        }

        return Task.CompletedTask;
    }
    
    public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if ( damageTakenThisTurn > 0 )
        {
            if (!base.Owner.Creature.HasPower<Insulation>())
            {
                Flash();
                _ = TriggerShock(
                    CalculateOperationDuration(damageTakenThisTurn, base.Owner.Creature.MaxHp),
                    CalculateOperationIntensity(damageTakenThisTurn, base.Owner.Creature.MaxHp));
            }
        }
        damageTakenThisTurn = 0;

        return Task.CompletedTask;
    }

    public override Task AfterPlayerTurnStartEarly(PlayerChoiceContext choiceContext, Player player)
    {
        int range = player.Creature.MaxHp;
        return Task.CompletedTask;
    }

    // TODO: ¿Does this trigger for everyone or just the player? 
    public async Task TriggerShock(int duration, int intensity)
    {
        await PiShockApiHandler.PostShockerOpAsync(0, duration, intensity);
    }

    public async Task TriggerVibrate(int duration, int intensity)
    {
        await PiShockApiHandler.PostShockerOpAsync(1, duration, intensity);
    }
    
    public async Task TriggerBeep(int duration)
    {
        await PiShockApiHandler.PostShockerOpAsync(2, duration, 0);
    }
    
    public async Task TriggerMultiShock(int instances)
    {
        for (int i = 0; i < instances; i++)
        {
            await TriggerShock((int)Config.MaxDuration, (int)Config.MaxIntensity);
            await Task.Delay((int)Config.MaxDuration + 1000);
        }
    }

    private int CalculateOperationIntensity(int damageTaken, int playerMaxHp)
    {
        if (Config.AlwaysMaxPower) {
            return (int)Config.MaxIntensity;
        }

        float damageIntensity =  ( (float)damageTaken / (float)playerMaxHp ) * (float)Config.MaxIntensity;
        
        if(damageIntensity > Config.MaxIntensity)
            damageIntensity = (float)Config.MaxIntensity;
        else if (damageIntensity < 1.0f)
            damageIntensity = 1.0f;
        
        return (int)damageIntensity;
    }

    private int CalculateOperationDuration(int damageTaken, int playerMaxHp)
    {
        if (Config.AlwaysMaxPower) {
            return (int)Config.MaxDuration;
        }

        float damageIntensity = ( (float)damageTaken / (float)playerMaxHp ) * (float)Config.MaxDuration;
        
        if(damageIntensity > Config.MaxDuration)
            damageIntensity = (float)Config.MaxDuration;
        else if (damageIntensity < 1.0f)
            damageIntensity = 1.0f;
        
        return (int)damageIntensity;
    }





}