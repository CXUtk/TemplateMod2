using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateMod2.Utils;
using Terraria;
using Terraria.ModLoader;

namespace TemplateMod2.Buffs {
    public class SuperToxic : ModBuff {
        public override void SetDefaults() {
            // 设置buff名字和描述
            DisplayName.SetDefault("剧毒");
            Description.SetDefault("你中毒了，祝你好运");

            // 因为buff严格意义上不是一个TR里面自定义的数据类型，所以没有像buff.XXXX这样的设置属性方式了
            // 我们需要用另外一种方式设置属性

            // 这个属性决定buff在游戏退出再进来后会不会仍然持续，true就是不会，false就是会
            Main.buffNoSave[Type] = false;

            // 用来判定这个buff算不算一个debuff，如果设置为true会得到TR里对于debuff的限制，比如无法取消
            Main.debuff[Type] = true;

            // 当然你也可以用这个属性让这个buff即使不是debuff也不能取消，设为false就是不能取消了
            this.canBeCleared = false;

            // 决定这个buff是不是照明宠物的buff，以后讲宠物和召唤物的时候会用到的，现在先设为false
            Main.lightPet[Type] = false;

            // 决定这个buff会不会显示持续时间，false就是会显示，true就是不会显示，一般宠物buff都不会显示
            Main.buffNoTimeDisplay[Type] = false;

            // 决定这个buff在专家模式会不会持续时间加长，false是不会，true是会
            this.longerExpertDebuff = false;

            // 如果这个属性为true，pvp的时候就可以给对手加上这个debuff/buff
            Main.pvpBuff[Type] = true;

            // 决定这个buff是不是一个装饰性宠物，用来判定的，比如消除buff的时候不会消除它
            Main.vanityPet[Type] = false;
        }
        // 注意这里我们选择的是对Player生效的Update，另一个是对NPC生效的Update
        public override void Update(Player player, ref int buffIndex) {
            // 把玩家的所有生命回复清除
            if (player.lifeRegen > 0) {
                player.lifeRegen = 0;
            }
            player.lifeRegenTime = 0;

            player.buffTime[buffIndex] = 2;
            // 让玩家的减血速率随着时间而减少
            // player.buffTime[buffIndex]就是这个buff的剩余时间
            player.lifeRegen -= player.buffTime[buffIndex];
        }
        public override void Update(NPC npc, ref int buffIndex) {
            if (npc.lifeRegen > 0) {
                npc.lifeRegen = 0;
            }
            npc.lifeRegen -= 50;
        }
        public override bool ReApply(Player player, int time, int buffIndex) {
            player.buffTime[buffIndex] += time;
            return true;
        }
    }
}
