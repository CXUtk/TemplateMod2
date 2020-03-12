using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TemplateMod2.Items.Accessories {
    [AutoloadEquip(EquipType.Wings)]
    public class ExampleWings : ModItem {
        public override void SetStaticDefaults() {
            base.SetStaticDefaults();
        }
        public override void SetDefaults() {
            item.width = 22;
            item.height = 20;
            item.value = 10000;
            item.rare = 2;
            item.accessory = true;
        }
        //these wings use the same values as the solar wings
        public override void UpdateAccessory(Player player, bool hideVisual) {
            if (hideVisual) {
                player.wingTimeMax = 50;
            } else {
                player.wingTimeMax = 200;
            }
            //player.wingTime = player.wingTimeMax;
            //if (!player.controlJump && !player.controlDown) {
            //    player.gravDir = 0f;
            //    player.velocity.Y = 0;
            //    player.gravity = 0;
            //    player.noFallDmg = true;
            //}
            //if (player.controlDown) {
            //    player.gravity = Player.defaultGravity;
            //    player.gravDir = 1;
            //    player.noFallDmg = true;
            //}
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend) {
            constantAscend = 1f;
            maxAscentMultiplier = 2f;
            //maxCanAscendMultiplier = 2f;
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration) {
            //speed = 100f;
            //acceleration = 2.5f;
        }


        public override void AddRecipes() {
            base.AddRecipes();
        }
    }
}
