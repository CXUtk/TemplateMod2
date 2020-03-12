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
        //public override void UpdateArmorSet(Player player, string set) {
        //    Main.NewText(set);
        //    if (set.Equals(Language.GetTextValue("ArmorSetBonus.Wood"))) {
        //        player.statDefense += 100;
        //    }
        //    base.UpdateArmorSet(player, set);
        //}

        //public override string IsArmorSet(Item head, Item body, Item legs) {
        //    return "?";
        //}

        public override void UpdateEquip(Item item, Player player) {
            //if ((player.head == 52 && player.body == 32 && player.legs == 31) || (player.head == 53 && player.body == 33 && player.legs == 32) || (player.head == 54 && player.body == 34 && player.legs == 33) || (player.head == 55 && player.body == 35 && player.legs == 34) || (player.head == 70 && player.body == 46 && player.legs == 42) || (player.head == 71 && player.body == 47 && player.legs == 43) || (player.head == 166 && player.body == 173 && player.legs == 108) || (player.head == 167 && player.body == 174 && player.legs == 109)) {
            //    player.setBonus = "防御力增加1000倍";
            //    player.statDefense *= 1000;
            //}
            base.UpdateEquip(item, player);
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual) {
            //Main.NewText("?");
            base.UpdateAccessory(item, player, hideVisual);
        }
    }
}
