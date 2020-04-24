using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using TemplateMod2.UI;
using TemplateMod2.UI.Instances;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.UI;

// 命名空间，注意它要与文件夹的名字相同
namespace TemplateMod2 {

    // 主要Mod类
    public class TemplateMod2 : Mod {

        // 给一个实例指针，以后会非常有用的
        public static TemplateMod2 Instance;
        public static float Strength;
        public static float Progress;
        public static Effect npcEffect;
        public static UIStateMachine UIStateMachine;

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
            UIStateMachine = new UIStateMachine();
            UIStateMachine.Add(new TestState());
            //Main.graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            base.Load();
        }
        public override void Unload() {
            Instance = null;
            Filters.Scene["TemplateMod:GBlur"].Deactivate();
            //Main.graphics.PreferredDepthStencilFormat = DepthFormat.Depth24;
            base.Unload();
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            int mouseLayer = layers.FindIndex((layer) => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseLayer != -1) {
                layers.Insert(mouseLayer, new LegacyGameInterfaceLayer("TemplateMod: UI",
                    () => {
                        try {
                            UIStateMachine.Draw(Main.spriteBatch);
                        } catch (Exception ex) {
                            // Ignored
                            Logger.Error(ex.ToString());
                        }
                        return true;
                    })
                );
            }
        }

        public override void UpdateUI(GameTime gameTime) {
            UIStateMachine.Update(gameTime);
            base.UpdateUI(gameTime);
        }

        public override void PostDrawInterface(SpriteBatch spriteBatch) {
            //RenderTarget2D tmp = new RenderTarget2D(spriteBatch.GraphicsDevice, 100, 100);
            //spriteBatch.End();
            //spriteBatch.GraphicsDevice.SetRenderTarget(tmp);
            //spriteBatch.Begin();
            //spriteBatch.Draw(Main.magicPixel, new Rectangle(0, 0, 50, 50), Color.Red);
            //spriteBatch.End();
            //spriteBatch.GraphicsDevice.SetRenderTarget(null);
            //spriteBatch.Begin();
            //spriteBatch.Draw(Main.screenTarget, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
            //spriteBatch.Draw(tmp, new Rectangle(0, 0, 100, 100), Color.White);
        }

        public override void PreUpdateEntities() {
            if (!Filters.Scene["TemplateMod:GBlur"].IsActive()) {
                // Filters.Scene.Activate("TemplateMod:GBlur");
            }
        }
    }
}
