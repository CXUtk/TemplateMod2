using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TemplateMod2 {
    public class TemplateGlobalItem : GlobalItem {
        public override void UpdateArmorSet(Player player, string set) {
            Main.NewText(set);
            if (set.Equals(Language.GetTextValue("ArmorSetBonus.Wood"))) {
                player.statDefense += 100;
            }
            base.UpdateArmorSet(player, set);
        }

        public override string IsArmorSet(Item head, Item body, Item legs) {
            return "?";
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual) {
            //Main.NewText("?");
            base.UpdateAccessory(item, player, hideVisual);
        }
    }
}
