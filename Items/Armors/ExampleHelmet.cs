using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

// 确保这个文件一定要放在Items/Armors/文件夹里，与命名空间匹配
// 这个套装的贴图来自ExampleMOD
namespace TemplateMod2.Items.Armors {
    // 注意这里，这是C#里面的一个神奇的语法
    // 作用是给一个类附加一个属性
    // 比如这里就是给这个类附加一个装备样式为头盔的属性，这样TML就会把它识别成头盔
    [AutoloadEquip(EquipType.Head)]
    public class ExampleHelmet : ModItem {
        // 设置物品描述的地方 
        public override void SetStaticDefaults() {
            DisplayName.AddTranslation(GameCulture.Chinese, "模板头盔");
            Tooltip.AddTranslation(GameCulture.Chinese, "这是一个被魔改了的头盔" +
                "\n身体周围会发红光");
        }

        public override void SetDefaults() {
            item.width = 18;
            item.height = 18;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = ItemRarityID.Orange;

            // 防御+12
            item.defense = 50;
        }

        // 重点来了，IsArmorSet这个重写函数用来判断身上的物品能不能组成一件完整套装
        // 比如这里我需要让头盔，胸甲，护腿全部都是模板装备的才能算是套装
        public override bool IsArmorSet(Item head, Item body, Item legs) {
            return body.type == mod.ItemType("ExampleBreastplate") && legs.type == mod.ItemType("ExampleLeggings");
        }
        // 如果在上面的函数中玩家被判定穿上了模板套装，那么就会在这里执行其效果
        public override void UpdateArmorSet(Player player) {
            // 套装描述，就是鼠标移上去最底下显示的套装效果
            player.setBonus = "进一步增加回血速度，吸取红心范围增大" +
                "\n增加10%伤害减免";
            player.endurance += 0.6f;
            player.lifeRegen += 2;
            player.lifeMagnet = true;
            // 加点特技
            player.armorEffectDrawShadow = true;
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
