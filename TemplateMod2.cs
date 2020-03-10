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
        private Effect fontEffect;

        // 构造函数
        public TemplateMod2() {
        }
        public override void Load() {
            Instance = this;
            Filters.Scene["TemplateMod:Gray"] = new Filter(
                new TestScreenShaderData(new Ref<Effect>(GetEffect("Effects/fuzzy")), "Test"), EffectPriority.Medium);
            Filters.Scene["TemplateMod:Gray"].Load();
            fontEffect = GetEffect("Effects/font");
            base.Load();
        }
        public override void Unload() {
            Instance = null;
            Filters.Scene["TemplateMod:Gray"].Deactivate();
            base.Unload();
        }
        public override void PostDrawInterface(SpriteBatch spriteBatch) {
            //spriteBatch.End();
            //var font = Main.fontMouseText;
            //Main.instance.GraphicsDevice.Clear(Color.CornflowerBlue);
            //fontEffect.Parameters["Projection"].SetValue(Matrix.CreateOrthographicOffCenter(0, Main.instance.GraphicsDevice.Viewport.Width, Main.instance.GraphicsDevice.Viewport.Height, 0, 0, 1));
            //fontEffect.Parameters["Color1"].SetValue(Color.Red.ToVector4());
            //fontEffect.Parameters["Color2"].SetValue(Color.Blue.ToVector4());
            //fontEffect.Parameters["FontHeight"].SetValue(font.LineSpacing);
            //FieldInfo fontTexture = typeof(SpriteFont).GetField("textureValue", BindingFlags.NonPublic | BindingFlags.Instance);
            //fontEffect.Parameters["FontTexture"].SetValue((Texture2D)fontTexture.GetValue(font));
            //fontEffect.CurrentTechnique.Passes[0].Apply();

            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, fontEffect);
            //spriteBatch.DrawString(font, "abcdefghijklmnopqrstuvxyz", new Vector2(500, 500), Color.White);
            //spriteBatch.End();

            //spriteBatch.End();
            //var ShaderRenderTarget = new RenderTarget2D(spriteBatch.GraphicsDevice, 100, 30, false, SurfaceFormat.Color, DepthFormat.None);
            //Main.instance.GraphicsDevice.SetRenderTarget(ShaderRenderTarget);
            //Main.instance.GraphicsDevice.Clear(Color.Transparent);
            //SpriteBatch sb = new SpriteBatch(Main.instance.GraphicsDevice);
            //sb.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, fontEffect);

            //sb.DrawString(Main.fontMouseText, "冲冲冲啊", new Vector2(0, 0), Color.White);
            //sb.End();
            //Main.instance.GraphicsDevice.SetRenderTarget(Main.screenTargetSwap);
            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            //var pass = fontEffect.Techniques[0].Passes["Color"];
            //fontEffect.Parameters["uTime"].SetValue((float)Main.GlobalTime);
            //pass.Apply();
            ////spriteBatch.Draw(Main.screenTargetSwap, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
            //spriteBatch.Draw(ShaderRenderTarget, new Rectangle(300, 300, 100, 50), Color.White);
            //spriteBatch.End();
            //spriteBatch.Begin();
        }

        public override void PreUpdateEntities() {
            if (!Filters.Scene["TemplateMod:Gray"].IsActive()) {
                //Filters.Scene.Activate("TemplateMod:Gray");
            }
            if (Strength < 10) Strength += 0.1f;
            if (Progress < 1.0) Progress += 0.02f;
        }
    }
}
