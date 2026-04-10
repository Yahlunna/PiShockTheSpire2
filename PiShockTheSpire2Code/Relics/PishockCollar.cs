using System.Diagnostics;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;
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
    
    private int _damageTakenThisTurn = 0;
    private int _piShockTheSpire2ActiveAct = -1;
    [SavedProperty]
    public int PiShockTheSpire2_ActiveAct {
        get { return _piShockTheSpire2ActiveAct; }
        set { AssertMutable(); _piShockTheSpire2ActiveAct = value; }
    }
    
    public override Task AfterObtained()
    {
        PiShockTheSpire2_ActiveAct = base.Owner.RunState.CurrentActIndex;
        if (Config.HealingVibrates) {
            int midRangeDuration = (int)( (Config.MaxDuration + Config.MinDuration)/2 );
            int midRangeIntensity= (int)( (Config.MaxIntensity + Config.MinIntensity)/2 );;

            _ = TriggerVibrate(midRangeDuration, midRangeIntensity);
        }
        return Task.CompletedTask;
    }
    
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
        if (CombatManager.Instance.IsInProgress && target == base.Owner.Creature && result.UnblockedDamage > 0) {
            _damageTakenThisTurn += result.UnblockedDamage;
        }
        else if (target == base.Owner.Creature && result.UnblockedDamage > 0) {
            _ = TriggerShock(
                CalculateOperationDuration(result.UnblockedDamage, base.Owner.Creature.MaxHp),
                CalculateOperationIntensity(result.UnblockedDamage, base.Owner.Creature.MaxHp));
        }
        return Task.CompletedTask;
    }
    
    public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == base.Owner.Creature.Side && !Config.TriggerSelfDamage) {
            _damageTakenThisTurn = 0;
            return Task.CompletedTask;
        }
        if ( _damageTakenThisTurn > 0 ) {
            if (!base.Owner.Creature.HasPower<Insulation>()) {
                Flash();
                
                _ = TriggerShock(
                    CalculateOperationDuration(_damageTakenThisTurn, base.Owner.Creature.MaxHp),
                    CalculateOperationIntensity(_damageTakenThisTurn, base.Owner.Creature.MaxHp));
            }
        }
        _damageTakenThisTurn = 0;
        return Task.CompletedTask;
    }

    public override Task AfterCombatVictoryEarly(CombatRoom room)
    {
        if (_damageTakenThisTurn > 0)
        {
            if (!base.Owner.Creature.HasPower<Insulation>())
            {
                Flash();
                
                _ = TriggerShock(
                    CalculateOperationDuration(_damageTakenThisTurn, base.Owner.Creature.MaxHp),
                    CalculateOperationIntensity(_damageTakenThisTurn, base.Owner.Creature.MaxHp));
            }
        }
        _damageTakenThisTurn = 0;

        return Task.CompletedTask;;
    }

    public override Task AfterRestSiteHeal(Player player, bool isMimicked)
    {
        if (Config.HealingVibrates && player == base.Owner)
        {
            int midRangeDuration = (int)( (Config.MaxDuration + Config.MinDuration)/2 );
            int midRangeIntensity= (int)( (Config.MaxIntensity + Config.MinIntensity)/2 );;

            _ = TriggerVibrate(midRangeDuration, midRangeIntensity);
        }
        return Task.CompletedTask;
    }
    
    public override Task AfterRoomEntered(AbstractRoom ar)
    {
        if (Config.HealingVibrates && PiShockTheSpire2_ActiveAct != base.Owner.RunState.CurrentActIndex)
        {
            PiShockTheSpire2_ActiveAct = base.Owner.RunState.CurrentActIndex;
            
            int midRangeDuration = (int)( (Config.MaxDuration + Config.MinDuration)/2 );
            int midRangeIntensity= (int)( (Config.MaxIntensity + Config.MinIntensity)/2 );;

            _ = TriggerVibrate(midRangeDuration, midRangeIntensity);
        }
        return Task.CompletedTask;
    }
    
    public async Task TriggerShock(int duration, int intensity)
    {
        if (LocalContext.IsMe(base.Owner)){
            DebugDump(intensity, duration);
            await PiShockApiHandler.PostShockerOpAsync(0, duration, intensity);
        }
    }

    public async Task TriggerVibrate(int duration, int intensity)
    {
        if (LocalContext.IsMe(base.Owner))
        {
            await PiShockApiHandler.PostShockerOpAsync(1, duration, intensity);
        }
    }
    
    public async Task TriggerBeep(int duration)
    {
        if (LocalContext.IsMe(base.Owner))
        {
            await PiShockApiHandler.PostShockerOpAsync(2, duration, 0);
        }
    }
    
    public async Task TriggerMultiShock(int instances)
    {
        if (LocalContext.IsMe(base.Owner))
        {
            for (int i = 0; i < instances; i++)
            {
                await TriggerShock((int)Config.MaxDuration, (int)Config.MaxIntensity); 
                await Task.Delay(((int)Config.MaxDuration * 1000) + 1000);
            }
        }
    }

    private int CalculateOperationIntensity(int damageTaken, int playerMaxHp)
    {
        if (Config.AlwaysMaxPower) {
            return (int)Config.MaxIntensity;
        }

        float damageRange = (float)Config.MaxIntensity - ((float)Config.MinIntensity - 1.0f);
        float damageIntensity = ((float)damageTaken / (float)playerMaxHp) * damageRange;
        damageIntensity += (float)Config.MinIntensity;
        
        if(damageIntensity > Config.MaxIntensity)
            damageIntensity = (float)Config.MaxIntensity;
        else if (damageIntensity < (float)Config.MinIntensity)
            damageIntensity = (float)Config.MinIntensity;
        
        return (int)damageIntensity;
    }

    private int CalculateOperationDuration(int damageTaken, int playerMaxHp)
    {
        if (Config.AlwaysMaxPower) {
            return (int)Config.MaxDuration;
        }

        float durationRange = (float)Config.MaxDuration - ((float)Config.MinDuration - 1.0f);
        float durationIntensity = ( (float)damageTaken / (float)playerMaxHp ) * durationRange;
        durationIntensity += (float)Config.MinDuration;
        
        if(durationIntensity > Config.MaxDuration)
            durationIntensity = (float)Config.MaxDuration;
        else if (durationIntensity < (float)Config.MinDuration)
            durationIntensity = (float)Config.MinDuration;
        
        return (int)durationIntensity;
    }

    public void DebugDump(int intensity, int duration) {
        if (Config.VerboseLogs)
        {
            MainFile.Logger.Info("------------------------------------------------------------.");
            MainFile.Logger.Info("Registered unblocked damage taken ammounting for " + _damageTakenThisTurn + ".");
            MainFile.Logger.Info("Attemping a shock with an intensity of " + intensity + " and a duration of " + duration + ".");
        }
    }

}