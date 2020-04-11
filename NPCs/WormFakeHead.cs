using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateMod2.NPCs.StateMachine;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TemplateMod2.NPCs {
    public class WormFakeHead : WormBodyNPC {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("虫虫-头");
        }
        public override void SetDefaults() {
            npc.npcSlots = 5f;
            npc.width = 44;
            npc.height = 44;
            npc.aiStyle = -1;
            npc.damage = 20;
            npc.defense = 70;
            npc.lifeMax = 1000000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath10;
            npc.boss = true;
            npc.noGravity = true;
            npc.scale = 1.5f;
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
            npc.rotation = npc.velocity.ToRotation() + 1.57f;
            base.AIBefore();
        }

        private void MoveToPlayer() {
            Vector2 diff = Vector2.Normalize(TargetPlayer.Center - npc.Center);
            diff *= 50f;
            diff = (npc.velocity * 30 + diff) / 31f;
            float speedX = 0.3f;
            float speedY = 0.3f;
            npc.velocity.X += (npc.velocity.X < diff.X ? 1 : -1) * speedX;
            npc.velocity.Y += (npc.velocity.Y < diff.Y ? 1 : -1) * speedY;
        }

        private class AttackState : NPCState {
            public AttackState(SMNPC npc) : base(npc) { }
            public override void AI(SMNPC mnpc) {
                var wmnpc = mnpc as WormFakeHead;
                //唯一目标
                if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead) {
                    npc.TargetClosest(true);
                }
                wmnpc.MoveToPlayer();
            }
        }

        private class SpawnState : NPCState {
            public SpawnState(SMNPC npc) : base(npc) { }
            public override void AI(SMNPC mnpc) {
                var worm = (WormBodyNPC)(mnpc);
                npc.realLife = worm.Head.npc.realLife;
                mnpc.SetState<AttackState>();
            }
        }

        public class MergeState : NPCState {
            public MergeState(SMNPC npc) : base(npc) { }
            public override void AI(SMNPC mnpc) {
                var worm = (WormBodyNPC)(mnpc);
                npc.velocity = Vector2.Normalize(worm.Head.npc.Center - npc.Center) * 30f;
                if (npc.Hitbox.Intersects(worm.Head.npc.Hitbox)) {
                    // 接上去
                    worm.Tail.Head = worm.Head;
                    // 把上一个尾巴咬掉
                    worm.Head.Tail.npc.active = false;
                    worm.Head.Tail = worm.Tail;
                    // 自己也死掉
                    npc.active = false;
                }
            }
        }

        public override void Initialize() {
            RegisterState<SpawnState>();
            RegisterState<AttackState>();
            RegisterState<MergeState>();
        }
    }
}
