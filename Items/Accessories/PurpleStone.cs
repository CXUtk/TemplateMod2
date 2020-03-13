using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateMod2.Utils;
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
            player.lifeRegen += 20;
            player.statLife++;
            player.jumpSpeedBoost = 5f;
            player.jumpBoost = true;
            // 连跳
            player.doubleJumpBlizzard = true;
            player.doubleJumpCloud = true;
            player.doubleJumpSail = true;
            player.doubleJumpFart = true;
            player.doubleJumpSandstorm = true;
            player.doubleJumpUnicorn = true;

            if (Main.time % 3 < 1) {
                Dust dust = Dust.NewDustDirect(player.position, player.width, player.height,
                    MyDustId.Fire, -player.velocity.X, -player.velocity.Y, 100, Color.White, 2.0f);
                dust.noGravity = true;
            }
        }


        public override void AddRecipes() {
            base.AddRecipes();
        }
    }
}
