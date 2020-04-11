using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TemplateMod2.Items.Accessories {
    public class PurpleStone : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("");
            DisplayName.AddTranslation(GameCulture.Chinese, "紫色闪光石");
            Tooltip.SetDefault("");
            Tooltip.AddTranslation(GameCulture.Chinese, "闪光石，只不过是紫色品质的");
            base.SetStaticDefaults();
        }

        public override void SetDefaults() {
            // 跟以前没啥区别
            item.width = 22;
            item.height = 22;

            // 重点在这里，这个属性设为true才能带在身上
            item.accessory = true;

            // 物品的面板防御数值，装备了以后就会增加
            item.defense = 16;

            item.rare = 8;
            item.value = Item.sellPrice(0, 5, 0, 0);

            // 这个属性代表这是专家模式专有物品，稀有度颜色会是彩虹的！
            item.expert = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual) {
            player.lifeRegen += 10;
            player.jumpSpeedBoost = 5f;
            player.jumpBoost = true;
            // 连跳
            player.doubleJumpBlizzard = true;
            player.doubleJumpCloud = true;
            player.doubleJumpSail = true;
            player.doubleJumpFart = true;
            player.doubleJumpSandstorm = true;
            player.doubleJumpUnicorn = true;

            if (!player.controlJump && !player.controlDown) {
                player.gravDir = 0f;
                player.velocity.Y = 0;
                player.gravity = 0;
                player.noFallDmg = true;
            }
            if (player.controlDown) {
                player.gravity = Player.defaultGravity;
                player.gravDir = 1;
                player.noFallDmg = true;
            }
        }
        public override void UseStyle(Player player) {
            base.UseStyle(player);
        }

        public override void AddRecipes() {
            base.AddRecipes();
        }
    }
}
