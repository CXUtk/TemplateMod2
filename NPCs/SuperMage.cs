using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateMod2.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TemplateMod2.NPCs {
    public class SuperMage : ModNPC {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("测试小伙");
            //该NPC的游戏内显示名
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Wizard];
            //NPC总共帧图数，一般为16+下面两种帧的帧数
            NPCID.Sets.ExtraFramesCount[npc.type] = NPCID.Sets.ExtraFramesCount[NPCID.Wizard];
            //额外活动帧，一般为5
            NPCID.Sets.AttackFrameCount[npc.type] = NPCID.Sets.AttackFrameCount[NPCID.Wizard];
            //攻击帧，这个帧数取决于你的NPC攻击类型，射手填5，战士和投掷填4，法师填2，当然，也可以多填，就是不知效果如何（这里直接引用商人的）
            NPCID.Sets.DangerDetectRange[npc.type] = 1000;
            //巡敌范围，以像素为单位，这个似乎是半径
            NPCID.Sets.AttackType[npc.type] = NPCID.Sets.AttackType[NPCID.Wizard];
            //攻击类型，一般为0，想要模仿其他NPC就填他们的ID
            NPCID.Sets.AttackTime[npc.type] = 60;
            //单次攻击持续时间，越短，则该NPC攻击越快（可以用来模拟长时间施法的NPC）
            NPCID.Sets.AttackAverageChance[npc.type] = 5;
            //NPC遇敌的攻击优先度，该数值越大则NPC遇到敌怪时越会优先选择逃跑，反之则该NPC越好斗。
            //最小一般为1，你可以试试0或负数LOL~
            NPCID.Sets.MagicAuraColor[npc.type] = Color.Cyan;
            //如果该NPCc属于法师类，你可以加上这个来改变NPC的魔法光环颜色，这里用紫色

        }

        public override void SetDefaults() {
            npc.townNPC = true;
            //必带项，没有为什么
            npc.friendly = true;
            //如果你想写敌对NPC也行
            npc.width = 22;
            //碰撞箱宽
            npc.height = 32;
            //碰撞箱高            
            npc.aiStyle = 7;
            //必带项，如果你能自己写出城镇NPC的AI可以不带
            npc.damage = 10;
            //碰撞伤害，由于城镇NPC没有碰撞伤害所以可以忽略
            npc.defense = 75;
            //防御力
            npc.lifeMax = 2500;
            //生命值
            npc.HitSound = SoundID.NPCHit1;
            //受伤音效
            npc.DeathSound = SoundID.NPCDeath1;
            //死亡音效
            npc.knockBackResist = 0.3f;
            //抗击退性，数字越大抗性越低
            animationType = NPCID.Wizard;
            //如果你的NPC属于除投掷类NPC以外的其他攻击类型，请带上，值可以填对应NPC的ID
        }

        public override string TownNPCName() {
            switch (WorldGen.genRand.Next(3)) {
                case 0:
                    return "杨永信";
                case 1:
                    return "杨永信";
                default:
                    return "杨永信";
            }
        }

        public override void FindFrame(int frameHeight) {
            npc.frame.
            base.FindFrame(frameHeight);
        }


        //法师NPC专属：魔法光环的照明范围，最多起装饰作用（选带项）
        public override void TownNPCAttackMagic(ref float auraLightMultiplier) {
            auraLightMultiplier = 2f;
        }

        //NPC攻击一次后的间隔
        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) {
            cooldown = 4;
            randExtraCooldown = 4;
            //间隔的算法：实际间隔会大于或等于cooldown的值且总是小于cooldown+randExtraCooldown的总和（TR总整这些莫名其妙的玩意）
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback) {
            damage = 50;
            knockback = 6f;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay) {
            projType = ModContent.ProjectileType<LightTreePro>();
            attackDelay = 30;
        }
        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset) {
            multiplier = 10f;
            randomOffset = 2f;
        }

    }
}
