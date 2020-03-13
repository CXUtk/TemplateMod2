using Microsoft.Xna.Framework;
using System;
using TemplateMod2.Buffs;
using TemplateMod2.Utils;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

// 注意这里命名空间变了，多了个.Items
// 因为这个文件在Items文件夹，而读取图片的时候是根据命名空间读取的，如果写错了可能图片就读不到了
namespace TemplateMod2.Items {

    // 保证类名跟文件名一致，这样也方便查找
    public class SkirtSword : ModItem {

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
            // 伤害！想都不要想，后面这个值随便改吧，但是不要超过2147483647
            // 不然…… 你试试就知道了
            item.damage = 10;

            // 决定了这个武器的伤害属性，
            // melee 代表近战
            // ranged 代表远程
            // magic 代表膜法，不，魔法
            // summon 代表召唤
            // thrown 代表投掷
            item.melee = true;

            // 物品的碰撞体积大小，可以与贴图无关，但是建议设为跟贴图一样的大小
            // 不然鬼知道会不会发生奇怪的事情
            item.width = 40;
            item.height = 40;

            // 攻击速度和攻击动画持续时间！
            // 这个数值越低越快，因为TR游戏速度每秒是60帧，这里的20就是
            // 20.0 / 60.0 = 0.333 秒挥动一次！也就是一秒三次
            // 一般来说我们要把这两个值设成一样，但也有例外的时候，我们以后会讲
            item.useTime = 4;
            item.useAnimation = 4;

            // 使用方式，这个值决定了武器使用时到底是按什么样的动画播放
            // 1 代表挥动，也就是剑类武器！
            // 2 代表像药水一样喝下去，emmmm这个放在剑上会不会很奇怪（吞
            // 3 代表像同志短剑一样刺x 出去
            // 4 唔，这个一般不是用在武器上的，想象一下生命水晶使用的时候的动作
            // 5 手持，枪、弓、法杖类武器的动作，用途最广
            item.useStyle = 1;

            // 击退，你懂的，但是这个击退有个上限就是20，超过20击退效果跟20没什么区别
            // 后面的 'f' 表示这是个浮点数：8.25，但是这个'f'不可省略
            item.knockBack = 8.25f;

            // 物品的价格，这里用sellPrice，也就是卖出物品的价格作为基准
            // 这件物品卖出时会获得 0白金 1金 60银 0铜 这么多的钱 （就这？
            item.value = Item.sellPrice(0, 1, 60, 0);

            // 物品的稀有度，由-1到13越来越高，具体参考维基百科
            // https://terraria.gamepedia.com/Rarity 或者裙中世界的补充栏目
            item.rare = 1;

            // 设置这个物品使用时发出的声音，以后会讲到怎么调出其他声音
            // 在这里我用的是普通的挥剑声音
            item.UseSound = SoundID.Item1;

            // 决定了这个武器鼠标按住不放能不能一直攻击， true代表可以, false代表不行
            // （鼠标别按废了
            item.autoReuse = true;

            // 射出泰拉剑气
            item.shoot = ProjectileID.TerraBeam;
            item.shootSpeed = 7f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox) {

        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit) {
            // 给怪物加上我们之前做的Buff，持续10秒
            target.AddBuff(ModContent.BuffType<SuperToxic>(), 600);
        }

        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit) {
            base.ModifyHitNPC(player, target, ref damage, ref knockBack, ref crit);
        }

        //public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
        //    ref int type, ref int damage, ref float knockBack) {
        //    type = Main.rand.Next(Main.projectileTexture.Length);
        //    return true;
        //}





        // 物品合成表的设置部分
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            // 合成材料，需要10个泥土块
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            // 以及在工作台旁边
            recipe.AddTile(TileID.WorkBenches);
            // 生成1个这种物品
            recipe.SetResult(this);
            // 这样可以生成50个
            // recipe.SetResult(this, 50);

            // 把这个合成表装进tr的系统里
            recipe.AddRecipe();
        }
    }
}
