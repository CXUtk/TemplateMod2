using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Reflection;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

// 命名空间，注意它要与文件夹的名字相同
namespace TemplateMod2 {

    // 主要Mod类
    public class TemplateMod2 : Mod {

        // 给一个实例指针，以后会非常有用的
        public static TemplateMod2 Instance;
        public static float Strength;
        public static float Progress;
        public static Effect npcEffect;

        // 构造函数
        public TemplateMod2() {
        }
        public override void Load() {
            Instance = this;
            // 注意设置正确的Pass名字
            Filters.Scene["TemplateMod:GBlur"] = new Filter(
                new TestScreenShaderData(new Ref<Effect>(GetEffect("Effects/ShockWave")), "Test"), EffectPriority.Medium);
            Filters.Scene["TemplateMod:GBlur"].Load();


            npcEffect = GetEffect("Effects/EDge");


            base.Load();
        }
        public override void Unload() {
            Instance = null;
            Filters.Scene["TemplateMod:GBlur"].Deactivate();
            base.Unload();
        }
        public override void PostDrawInterface(SpriteBatch spriteBatch) {
        }

        public override void PreUpdateEntities() {
            if (!Filters.Scene["TemplateMod:GBlur"].IsActive()) {
                // Filters.Scene.Activate("TemplateMod:GBlur");
            }
        }
    }
}
