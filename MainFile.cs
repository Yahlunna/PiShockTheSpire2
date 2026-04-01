using BaseLib.Config;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Nodes.Relics;
using PiShockTheSpire2.PiShockTheSpire2Code;

namespace PiShockTheSpire2;

[ModInitializer(nameof(Initialize))]
public partial class MainFile : Node
{
    public const string ModId = "PiShockTheSpire2"; 

    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } =
        new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    public static void Initialize()
    {
        ModConfigRegistry.Register(ModId, new Config());
        
        Harmony harmony = new(ModId);

        harmony.PatchAll();
    }
    
    /*
    public override async Task AfterActEntered()
    {
        foreach (Player player in base.RunState.Players)
        {
            CardModel canonicalCard = base.RunState.Rng.Niche.NextItem(from c in ModelDb.CardPool<CurseCardPool>().GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint)
                where c.CanBeGeneratedByModifiers
                select c);
            CardModel card = player.RunState.CreateCard(canonicalCard, player);
            CardPileAddResult result = await CardPileCmd.Add(card, PileType.Deck);
            if (LocalContext.IsMe(player))
            {
                CardCmd.PreviewCardPileAdd(result);
            }
        }
    }
    */
    


}