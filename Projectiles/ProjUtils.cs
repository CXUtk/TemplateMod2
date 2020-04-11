using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TemplateMod2.Projectiles {
    public static class ProjUtils {
        public static NPC FindNearestEnemy(Vector2 pos, float maxDis, Func<NPC, bool> cond = null) {
            NPC target = null;
            foreach (var npc in Main.npc) {
                if (npc.active && !npc.friendly && cond(npc)) {
                    float dis = Vector2.Distance(pos, npc.Center);
                    if (dis < maxDis) {
                        maxDis = dis;
                        target = npc;
                    }
                }
            }
            return target;
        }
    }
}
