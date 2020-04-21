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
    public abstract class WormBodyNPC : SMNPC {
        /// <summary>
        /// 它前面的NPC的ID
        /// </summary>
        public WormBodyNPC Head {
            get {
                return _getNPCByID((int)npc.ai[2]);
            }
            set {
                npc.ai[2] = value.npc.whoAmI;
            }
        }
        /// <summary>
        /// 它后面的NPC的ID
        /// </summary>
        public WormBodyNPC Tail {
            get {
                return _getNPCByID((int)npc.ai[3]);
            }
            set {
                npc.ai[3] = value.npc.whoAmI;
            }
        }

        private WormBodyNPC _getNPCByID(int id) {
            if (id < 0) return null;
            NPC n = Main.npc[id];
            if (!n.active || n == null) return null;
            return (WormBodyNPC)n.modNPC;
        }
    }
    public class WormHead : WormBodyNPC {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("虫虫-头");
        }
        public override void SetDefaults() {
            npc.npcSlots = 5f;
            npc.width = 44;
            npc.height = 44;
            npc.aiStyle = -1;
            npc.damage = 50;
            npc.defense = 100;
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
        }

        private static WormBodyNPC GetWorm(NPC npc) {
            return npc.modNPC as WormBodyNPC;
        }

        public override void AIBefore() {
            npc.rotation = npc.velocity.ToRotation() + 1.57f;
            base.AIBefore();
        }

        private void MoveToPlayer() {
            Vector2 diff = Vector2.Normalize(TargetPlayer.Center - npc.Center);
            diff *= 35f;
            diff = (npc.velocity * 30 + diff) / 31f;
            float speedX = 0.5f;
            float speedY = 0.5f;
            npc.velocity.X += (npc.velocity.X < diff.X ? 1 : -1) * speedX;
            npc.velocity.Y += (npc.velocity.Y < diff.Y ? 1 : -1) * speedY;
        }

        public static WormBodyNPC SpawnWormPart(NPC npc, int type) {
            int id = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, type);
            NPC n = Main.npc[id];
            n.whoAmI = id;
            n.realLife = npc.realLife;
            return n.modNPC as WormBodyNPC;
        }

        private class NormalAttack : NPCState {
            public NormalAttack(SMNPC npc) : base(npc) { }
            public override void AI(SMNPC mnpc) {
                var wmnpc = mnpc as WormHead;
                //唯一目标
                if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead) {
                    npc.TargetClosest(true);
                }
                if (mnpc.Timer >= 0 && mnpc.Timer < 80) {
                    if (wmnpc.npc.velocity.Y < 20) {
                        wmnpc.npc.velocity.X += 0.5f;
                        wmnpc.npc.velocity.Y += 0.5f;
                    }
                } else {
                    wmnpc.MoveToPlayer();
                }
                mnpc.Timer++;
                if (mnpc.Timer == 400) {
                    WormBodyNPC cur = mnpc as WormBodyNPC;
                    for (int i = 0; i < 45; i++) {
                        WormBodyNPC nxt = cur.Tail;
                        if (i % 6 == 5) {
                            var lastHead = cur.Head;
                            var newHead = SpawnWormPart(cur.npc, ModContent.NPCType<WormFakeHead>());
                            newHead.npc.velocity = Main.rand.NextVector2CircularEdge(1, 1) * 25f;
                            newHead.Head = cur.Head;
                            newHead.Tail = cur;
                            cur.Head = newHead;

                            var newTail = SpawnWormPart(cur.npc, ModContent.NPCType<WormTail>());
                            newTail.Head = lastHead;
                            lastHead.Tail = newTail;
                        }
                        cur = nxt;
                    }
                    mnpc.Timer = 0;
                    mnpc.SetState<SplitAttack>();
                }
            }
        }

        private class SpawnState : NPCState {
            public SpawnState(SMNPC npc) : base(npc) { }
            public override void AI(SMNPC mnpc) {
                npc.realLife = npc.whoAmI;
                int length = 50;
                WormBodyNPC cur = (WormBodyNPC)mnpc;
                for (int i = 0; i < length; i++) {
                    int type = (i == length - 1) ? ModContent.NPCType<WormTail>() : ModContent.NPCType<WormBody>();
                    var part = SpawnWormPart(npc, type);
                    part.npc.realLife = npc.realLife;
                    part.Head = cur;
                    GetWorm(cur.npc).Tail = part;
                    // 每个蠕虫身体维护双向链表，前驱和后继
                    cur = part;
                }
                mnpc.SetState<NormalAttack>();
            }
        }

        private class Split : NPCState {
            public Split(SMNPC npc) : base(npc) { }
            public override void AI(SMNPC mnpc) {

            }
        }

        private class Merge : NPCState {
            public Merge(SMNPC npc) : base(npc) { }
            public override void AI(SMNPC mnpc) {

                mnpc.SetState<NormalAttack>();
            }
        }

        private class SplitAttack : NPCState {
            public SplitAttack(SMNPC npc) : base(npc) { }
            public override void AI(SMNPC mnpc) {
                var wmnpc = mnpc as WormHead;
                //唯一目标
                if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead) {
                    npc.TargetClosest(true);
                }
                if (mnpc.Timer >= 60 && mnpc.Timer < 80) {
                    if (wmnpc.npc.velocity.Y < 20)
                        wmnpc.npc.velocity.Y += 0.5f;
                } else {
                    wmnpc.MoveToPlayer();
                }
                mnpc.Timer++;
                if (mnpc.Timer == 360) {
                    foreach (var n in Main.npc) {
                        if (n.active && n.realLife == npc.whoAmI) {
                            if (n.type == ModContent.NPCType<WormFakeHead>()) {
                                var wormHead = n.modNPC as WormBodyNPC;
                                wormHead.SetState<WormFakeHead.MergeState>();
                            }
                        }
                    }
                } else if (mnpc.Timer > 360) {
                    if (wmnpc.npc.velocity.Y < 20)
                        wmnpc.npc.velocity.Y += 0.5f;
                    foreach (var n in Main.npc) {
                        if (n.active && n.realLife == npc.whoAmI && n.type == ModContent.NPCType<WormFakeHead>()) {
                            return;
                        }
                    }
                    mnpc.Timer = 0;
                    mnpc.SetState<NormalAttack>();
                }
            }
        }

        public override void Initialize() {
            RegisterState<SpawnState>();
            RegisterState<NormalAttack>();
            RegisterState<Split>();
            RegisterState<SplitAttack>();
            RegisterState<Merge>();
        }
    }
}
