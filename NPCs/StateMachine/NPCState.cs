using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace TemplateMod2.NPCs.StateMachine {
    public abstract class NPCState {
        protected NPC npc { get; set; }
        public NPCState(SMNPC mnpc) {
            npc = mnpc.npc;
        }
        public abstract void AI(SMNPC mnpc);
    }

    public class EmptyState : NPCState {
        public EmptyState(SMNPC mnpc) : base(mnpc) { }
        public override void AI(SMNPC mnpc) {

        }
    }
}
