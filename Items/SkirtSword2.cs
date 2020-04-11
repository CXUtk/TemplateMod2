using Microsoft.Xna.Framework;
using System;
using TemplateMod2.NPCs;
using TemplateMod2.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

// 注意这里命名空间变了，多了个.Items
// 因为这个文件在Items文件夹，而读取图片的时候是根据命名空间读取的，如果写错了可能图片就读不到了
namespace TemplateMod2.Items {
    //public class Item2 {
    //    // 体积
    //    public double volume;
    //    // 密度
    //    public double density;
    //    // 单位价格
    //    public double price;
    //    // 物品ID
    //    private int id;

    //    public Item2(double volume, double density, double price, int id) {
    //        this.volume = volume;
    //        this.density = density;
    //        this.price = price;
    //        this.id = id;
    //    }

    //    public virtual void Use() {
    //        Main.NewText("物品被使用了！");
    //    }
    //    protected virtual double Price {
    //        get {
    //            return volume * density * price;
    //        }
    //    }
    //    public void Sell() {
    //        Main.NewText($"物品被卖出了{GetPrice()}元钱");
    //    }

    //    public int ID {
    //        get { return id; }
    //    }
    //}


    // 保证类名跟文件名一致，这样也方便查找
    public class SkirtSword2 : ModItem {

        // 设置物品名字，描述的地方，这个函数需要记住
        public override void SetStaticDefaults() {

            // 这个是物品名字，也就是忽略游戏语言的情况下显示的文字
            DisplayName.SetDefault("Skirt Sword");
            // 推荐通过AddTranslation的方式添加其在切换到中文的时候显示中文名字
            DisplayName.AddTranslation(GameCulture.Chinese, "版模剑");

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
            item.damage = 50;

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
            item.useTime = 14;
            item.useAnimation = 14;
            item.shoot = ModContent.ProjectileType<ProjProj>();
            item.shootSpeed = 6f;

            // 使用方式，这个值决定了武器使用时到底是按什么样的动画播放
            // 1 代表挥动，也就是剑类武器！
            // 2 代表像药水一样喝下去，emmmm这个放在剑上会不会很奇怪（吞
            // 3 代表像同志短剑一样刺x 出去
            // 4 唔，这个一般不是用在武器上的，想象一下生命水晶使用的时候的动作
            // 5 手持，枪、弓、法杖类武器的动作，用途最广
            item.useStyle = 1;

            // 击退，你懂的，但是这个击退有个上限就是20，超过20击退效果跟20没什么区别
            // 后面的 'f' 表示这是个浮点数：8.25，但是这个'f'不可省略
            item.knockBack = 0f;

            // 物品的价格，这里用sellPrice，也就是卖出物品的价格作为基准
            // 这件物品卖出时会获得 0白金 1金 60银 0铜 这么多的钱 （就这？
            item.value = Item.sellPrice(0, 1, 60, 0);

            // 物品的稀有度，由-1到13越来越高，具体参考维基百科
            //https://terraria.gamepedia.com/Rarity 或者裙中世界的补充栏目
            item.rare = 1;

            // 设置这个物品使用时发出的声音，以后会讲到怎么调出其他声音
            // 在这里我用的是普通的挥剑声音
            item.UseSound = SoundID.Item1;

            // 决定了这个武器鼠标按住不放能不能一直攻击， true代表可以, false代表不行
            // （鼠标别按废了
            item.autoReuse = false;


        }
        private float crossProduct(Vector2 v1, Vector2 v2) {
            return v1.X * v2.Y - v1.Y * v2.X;
        }

        // 参数分别是，射出位置，射出速度，目标的位置和重力系数
        private Tuple<float, int> GetHitPoint(Vector2 pos, Vector2 shootVec, Vector2 targetPos, float gravity) {
            int t = 1;
            while (true) {
                pos += shootVec;
                // 如果速度向下，就是下落状态，并且下落到与目标相同（或更低）的高度
                // 越过了这个高度下落弹幕就没办法再击中敌人了，直接返回X坐标和所用时间
                if (shootVec.Y > 0 && pos.Y > targetPos.Y)
                    return new Tuple<float, int>(pos.X, t);
                // 模拟重力作用
                shootVec.Y += gravity;
                t++;
            }
        }

        // 获取到达固定点target所需的发射向量以及时间
        private Tuple<Vector2, int> GetShootVec(Vector2 pos, Vector2 target, float speed, float gravity) {
            // 二分法的范围包括了左右两边的弧度，要注意一下
            float L = -MathHelper.PiOver2, R = -MathHelper.PiOver4;
            Tuple<Vector2, int> ans = new Tuple<Vector2, int>(Vector2.Zero, 0);
            // 精度足够我们就停止
            while (R - L > 0.001f) {
                float mid = (L + R) / 2;
                Tuple<float, int> tmp = GetHitPoint(pos, mid.ToRotationVector2() * speed, target, gravity);
                // 如果X坐标在目标左边说明我们要降低角度，反之增加
                if (tmp.Item1 < target.X) {
                    L = mid;
                } else {
                    R = mid;
                }
                ans = new Tuple<Vector2, int>(mid.ToRotationVector2() * speed, tmp.Item2);
            }
            return ans;
        }
        // 获取预判向量
        private Vector2 GetPredictVec(Vector2 pos, NPC npc, float speed, float gravity) {
            // 一开始我们假设往npc现在所处位置发射弹幕
            Vector2 target = npc.Center - pos;
            for (int i = 0; i < 20; i++) {
                // 获取弹幕的飞行时间和射出速度
                Tuple<Vector2, int> info = GetShootVec(pos, target, speed, gravity);
                // 过了这么多时间以后npc到哪里了？
                Vector2 npcPos = npc.Center + info.Item2 * npc.velocity;
                // 我们射到的位置是否距离npc最终位置足够近？
                if (Vector2.Distance(target, npcPos) < 0.1f) {
                    Main.NewText($"足够近 {info.Item2} {info.Item1}");
                    // 足够近，我们直接返回这个发射向量
                    return info.Item1;
                }
                // 不够近，我们把目标位置改动一下，进行下一次尝试
                target = npcPos;
            }
            // 怎么样都不够近，算了放弃吧
            return Vector2.Zero;
        }

        bool flag = false;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            //Main.dayTime = false;
            //Main.time = 0;
            NPC target = null;
            foreach (var npc in Main.npc) {
                if (npc.active && npc.type == ModContent.NPCType<WormHead>()) {
                    target = npc;
                    break;
                }
            }
            if (target != null) {
                if (!flag) {
                    WormHead head = (WormHead)target.modNPC;
                    WormBodyNPC wmb = (WormBodyNPC)target.modNPC;
                    for (int i = 0; i < 45; i++) {
                        WormBodyNPC nxt = (WormBodyNPC)Main.npc[wmb.Tail].modNPC;
                        if (i % 6 == 5) {
                            var nhead = WormHead.SpawnHead(wmb.npc);
                            nhead.npc.velocity = Main.rand.NextVector2CircularEdge(1, 1) * 20f;
                            wmb.Head = nhead.npc.whoAmI;
                        }
                        wmb = nxt;
                    }
                } else {
                    foreach (var npc in Main.npc) {
                        if (npc.active && npc.type == ModContent.NPCType<WormHead>()) {
                            var worm = (WormBodyNPC)npc.modNPC;
                            if (worm.Tail == 0) {
                                npc.active = false;
                            }
                        }
                        if (npc.active && npc.modNPC is WormBodyNPC) {
                            var worm = (WormBodyNPC)npc.modNPC;
                            if (worm.Tail != 0) {
                                var pv = (WormBodyNPC)Main.npc[worm.Tail].modNPC;
                                pv.Head = worm.npc.whoAmI;
                            }
                        }
                    }
                }
                flag ^= true;
            }
            return true;
            //float maxDis = 1000f;
            //NPC target = null;
            //foreach (var npc in Main.npc) {
            //    if (npc.active && !npc.friendly && npc.value > 0 && !npc.dontTakeDamage) {
            //        float dis = Vector2.Distance(npc.Center, position);
            //        if (dis < maxDis) {
            //            maxDis = dis;
            //            target = npc;
            //        }
            //    }
            //}
            //if (target != null)
            //    Projectile.NewProjectile(position, GetPredictVec(position, target, 16f, 0.3f), type, 100, 5f, player.whoAmI);
            //return false;
            //float maxDis = 1000f;
            //NPC target = null;
            //foreach (var npc in Main.npc) {
            //    if (npc.active && !npc.friendly && npc.value > 0 && !npc.dontTakeDamage) {
            //        float dis = Vector2.Distance(npc.Center, position);
            //        if (dis < maxDis) {
            //            maxDis = dis;
            //            target = npc;
            //        }
            //    }
            //}
            //if (target != null) {
            //    Vector2 targetPos = target.Center;
            //    Vector2 plrToNPC = targetPos - position;
            //    float speed = 20f;
            //    float tmp = crossProduct(plrToNPC, target.velocity);
            //    float offset = plrToNPC.ToRotation();
            //    float G = tmp / speed / plrToNPC.Length();
            //    if (G > 1 || G < -1) {
            //        Main.NewText("无法预判QAQ");
            //        return true;
            //    }
            //    float realr = (float)(offset + Math.Asin(G));
            //    Projectile.NewProjectile(position, realr.ToRotationVector2() * speed / 2, type, 100, 5f, player.whoAmI);
            //    return false;
            //} else {
            //    return true;
            //}
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
