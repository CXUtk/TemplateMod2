using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateMod2.NPCs.StateMachine;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace TemplateMod2.NPCs {
    public class WormBody : WormBodyNPC {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("虫虫-身体");
        }
        public override void SetDefaults() {
            npc.npcSlots = 5f;
            npc.width = 44;
            npc.height = 44;
            npc.aiStyle = -1;
            npc.damage = 20;
            npc.defense = 300;
            npc.lifeMax = 1000000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath10;
            npc.noGravity = true;
            npc.scale = 1.5f;
            npc.boss = true;
            npc.noTileCollide = true;
            for (int i = 0; i < npc.buffImmune.Length - 1; i++) {
                npc.buffImmune[i] = true;
            }
            npc.knockBackResist = 0f;
            npc.behindTiles = true;
            npc.value = Item.buyPrice(10, 0, 0, 0);
            npc.netAlways = true;
            npc.dontCountMe = true;
        }
        public override void AIBefore() {
            npc.TargetClosest();
            var head = Main.npc[npc.realLife];
            // 如果大哥gg了
            if (!head.active || head.life <= 0) {
                // 自己也必死无疑
                npc.life = 0;
                npc.HitEffect(0, 10.0);
                npc.checkDead();
                npc.netUpdate = true;
            }
            // 否则我们就跟随前面的大哥走
            var front = Head.npc;
            Vector2 diff = front.Center - npc.Center;
            // 让npc移动到大哥后面height距离的位置
            diff.Normalize();
            npc.Center = front.Center - diff * npc.height;
            // 面朝大哥
            npc.rotation = diff.ToRotation() + 1.57f;
            base.AIBefore();
        }


        public override void Initialize() {
            RegisterState<EmptyState>();
        }
    }
}
