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
        public int Head {
            get {
                return (int)npc.ai[2];
            }
            set {
                npc.ai[2] = value;
            }
        }
        /// <summary>
        /// 它后面的NPC的ID
        /// </summary>
        public int Tail {
            get {
                return (int)npc.ai[3];
            }
            set {
                npc.ai[3] = value;
            }
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
            diff *= npc.Distance(TargetPlayer.Center) > 2000 ? 50 : 30;
            diff = (npc.velocity * 20 + diff) / 21f;
            float speedX = 0.3f;
            float speedY = 0.3f;
            npc.velocity.X += (npc.velocity.X < diff.X ? 1 : -1) * speedX;
            npc.velocity.Y += (npc.velocity.Y < diff.Y ? 1 : -1) * speedY;
        }

        public static WormBodyNPC SpawnHead(NPC npc) {
            int id = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, ModContent.NPCType<WormHead>());
            NPC n = Main.npc[id];
            n.whoAmI = id;
            var nhead = (WormBodyNPC)n.modNPC;
            nhead.Initialize();
            nhead.SetState<AttackState>();
            return nhead;
        }

        public static WormBodyNPC SpawnTail(NPC npc) {
            int id = NPC.NewNPC((int)npc.position.X + 10, (int)npc.position.Y + 10, ModContent.NPCType<WormTail>());
            NPC n = Main.npc[id];
            return (WormBodyNPC)n.modNPC;
        }

        private class AttackState : NPCState {
            public AttackState(SMNPC npc) : base(npc) { }
            public override void AI(SMNPC mnpc) {
                var wmnpc = mnpc as WormHead;
                //唯一目标
                if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead) {
                    npc.TargetClosest(true);
                }
                wmnpc.MoveToPlayer();
                mnpc.Timer++;
                //if (mnpc.Timer == 200) {
                //    var cur = mnpc as WormBodyNPC;
                //    for (int i = 0; i < 40; i++) {
                //        // if (cur == null || Main.npc[cur.Tail] == null || !Main.npc[cur.Tail].active) break;
                //        var next = GetWorm(Main.npc[cur.Tail]);
                //        if (i % 10 == 9) {
                //            //var newTail = SpawnTail(cur.npc);
                //            //var last = GetWorm(Main.npc[cur.Next]);
                //            //newTail.npc.realLife = last.npc.realLife;
                //            //newTail.Next = last.npc.whoAmI;

                //            var newhead = SpawnHead(cur.npc);
                //            cur.Head = newhead.npc.whoAmI;
                //            newhead.Initialize();
                //            newhead.SetState<AttackState>();
                //            newhead.npc.velocity = Main.rand.NextVector2CircularEdge(1, 1) * 20f;
                //            cur.npc.realLife = cur.Head;
                //        }
                //        cur = next;
                //    }
                //}
            }
        }

        private class SpawnState : NPCState {
            public SpawnState(SMNPC npc) : base(npc) { }
            public override void AI(SMNPC mnpc) {
                npc.realLife = npc.whoAmI;
                int length = 50;
                int curID = npc.whoAmI;
                for (int i = 0; i < length; i++) {
                    int type = (i == length - 1) ? ModContent.NPCType<WormTail>() : ModContent.NPCType<WormBody>();
                    int id = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, type);
                    var child = Main.npc[id];
                    child.realLife = npc.realLife;
                    GetWorm(child).Head = curID;
                    GetWorm(Main.npc[curID]).Tail = id;
                    // 每个蠕虫身体维护双向链表，前驱和后继
                    curID = id;
                }
                mnpc.SetState<AttackState>();
            }
        }

        public override void Initialize() {
            RegisterState<SpawnState>();
            RegisterState<AttackState>();
        }
    }
}
