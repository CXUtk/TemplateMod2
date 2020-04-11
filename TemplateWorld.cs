using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;


namespace TemplateMod2 {
    public class TemplateWorld : ModWorld {
        public override void TileCountsAvailable(int[] tileCounts) {
            base.TileCountsAvailable(tileCounts);
        }
    }

}
