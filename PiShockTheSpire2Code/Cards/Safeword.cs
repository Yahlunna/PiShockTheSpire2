using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace PiShockTheSpire2.PiShockTheSpire2Code.Cards;

  
[Pool(typeof(TokenCardPool))]
public class Safeword() : CustomCardModel(0, CardType.Skill,
    CardRarity.Basic, TargetType.Self)
{
    public override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        
    }

    public override void OnUpgrade()
    {

    }
    
    
    
    
}