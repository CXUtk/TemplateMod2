using Microsoft.Xna.Framework;
using System;
using TemplateMod2.Buffs;
using TemplateMod2.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

// 注意这里命名空间变了，多了个.Items
// 因为这个文件在Items文件夹，而读取图片的时候是根据命名空间读取的，如果写错了可能图片就读不到了
namespace TemplateMod2.Items {

    // 保证类名跟文件名一致，这样也方便查找
    public class PoisonousPotion : ModItem {

        // 设置物品名字，描述的地方，这个函数需要记住
        public override void SetStaticDefaults() {

            // 这个是物品名字，也就是忽略游戏语言的情况下显示的文字
            DisplayName.SetDefault("Skirt Sword");
            // 推荐通过AddTranslation的方式添加其在切换到中文的时候显示中文名字
            DisplayName.AddTranslation(GameCulture.Chinese, "模板剑");

            // 物品的描述，加入换行符 '\n' 可以多行显示哦
            Tooltip.SetDefault("What is this blade made of?\n" +
                "Ohh, Iron...");
            // 同理，我们加一个中文的翻译（？？？我们不本来就是中国人？
            Tooltip.AddTranslation(GameCulture.Chinese, "它是由什么做的？\n" +
                "哦铁啊，那没事了");
        }


        public override void SetDefaults() {
            // 这部分就不说了
            item.width = 14;
            item.height = 24;
            item.useAnimation = 17;
            item.useTime = 17;
            item.maxStack = 30;
            item.rare = 5;
            item.value = Item.sellPrice(0, 0, 50, 0);


            // 物品的使用方式，还记得2是什么吗
            item.useStyle = 2;
            // 喝药的声音
            item.UseSound = SoundID.Item3;

            // 决定这个物品使用以后会不会减少，true就是使用后物品会少一个，默认为false
            item.consumable = true;
            // 决定使用动画出现后，玩家转身会不会影响动画的方向，true就是会，默认为false
            item.useTurn = true;
            // 告诉TR内部系统，这个物品是一个生命药水物品，用于TR系统的特殊目的（比如一键喝药水），默认为false
            item.potion = false;
            // 这个药水能给玩家加多少血，跟potion一起使用喝完药就会有抗药性debuff
            item.healLife = 50;
            // 加buff的方法1：设置物品的buffType为buff的ID
            // 这里我设置了着火debuff（2333
            // item.buffType = BuffID.Poisoned;
            // 用于在物品描述上显示buff持续时间
            //item.buffTime = 60000;
        }

        // 当物品使用的时候触发，返回值貌似是什么都不会有影响
        public override bool UseItem(Player player) {
            // 给玩家加上中毒buff，持续 60000 / 60 = 1000秒
            // 第一个填buff的ID，第二个填持续时间
            player.AddBuff(ModContent.BuffType<SuperToxic>(), 300);
            return false;
        }

        // 物品合成表的设置部分
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldBar, 5);
            recipe.AddIngredient(ItemID.IronBar, 5);
            recipe.AddIngredient(ItemID.Torch, 25);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 99);
            recipe.AddRecipe();

        }
    }
}
