using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models.RelicPools;
using PiShockTheSpire2.PiShockTheSpire2Code.Utils;

namespace PiShockTheSpire2.PiShockTheSpire2Code.Relics;

[Pool(typeof(SharedRelicPool))]
public class PishockCollar() : CustomRelicModel
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;

    // TODO: Sanitize and generalize this.
    public override string PackedIconPath => "pishockcollar.png".RelicImagePath();
    public override string PackedIconOutlinePath => "pishockcollar_outline.png".RelicImagePath();
    public override string BigIconPath => "pishockcollar.png".BigRelicImagePath();
    
    
    /*
    public override string PackedIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".RelicImagePath();
            return ResourceLoader.Exists(path) ? path : "relic.png".RelicImagePath();
        }
    }

    public override string PackedIconOutlinePath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}_outline.png".RelicImagePath();
            return ResourceLoader.Exists(path) ? path : "relic_outline.png".RelicImagePath();
        }
    }

    public override string BigIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigRelicImagePath();
            return ResourceLoader.Exists(path) ? path : "relic.png".BigRelicImagePath();
        }
    }
    */




}