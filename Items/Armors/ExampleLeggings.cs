using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

// 确保这个文件一定要放在Items/Armors/文件夹里，与命名空间匹配
// 这个套装的贴图来自ExampleMOD
namespace TemplateMod2.Items.Armors {
    // 注意这里，这是C#里面的一个神奇的语法
    // 作用是给一个类附加一个属性
    // 比如这里就是给这个类附加一个装备样式为护腿的属性，这样TML就会把它识别成护腿
    [AutoloadEquip(EquipType.Legs)]
    public class ExampleLeggings : ModItem {
        // 设置物品描述的地方
        public override void SetStaticDefaults() {
            DisplayName.AddTranslation(GameCulture.Chinese, "模板护腿");
            Tooltip.AddTranslation(GameCulture.Chinese, "这是一个被魔改了的护腿"
                + "\n玩家在移动的时候增加5%全部伤害");
        }

        public override void SetDefaults() {
            item.width = 18;
            item.height = 18;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = ItemRarityID.Orange;

            // 防御+12
            item.defense = 15;
        }

        public override void UpdateEquip(Player player) {
            // 如果玩家的速度的值大于一定值，也就是玩家在移动
            if (player.velocity.Length() > 0.05f) {
                // 就增加全部伤害
                player.allDamage += 0.05f;
            }
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimtaneBar, 99);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
