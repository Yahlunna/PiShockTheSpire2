using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using PiShockTheSpire2.PiShockTheSpire2Code.Utils;

namespace PiShockTheSpire2.PiShockTheSpire2Code.Cards;
  
[Pool(typeof(TokenCardPool))]
public class Safeword() : CustomCardModel(0, CardType.Skill,
    CardRarity.Basic, TargetType.Self)
{
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

    //public override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<Mercy Power>(1)];  Create Power: Mercy
    //public override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<Mercy Power>()]; Define Mercy
    
    
    public override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        // Add Mercy.
    }

    public override void OnUpgrade()
    {
        //DynamicVars.Power.UpgradeValueBy(1);  (Im just guessing it will be something like this but i honestly have no idea lmao.)
    }
    
    
    
    
}