using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TemplateMod2.Projectiles;
using TemplateMod2.Projectiles.StateMachine;
using TemplateMod2.Utils;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TemplateMod2.Projectiles {
    public class ProjProj : SMProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("回力标");
        }
        public override void SetDefaults() {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.light = 0.1f;
            projectile.timeLeft = 600;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.scale = 1.25f;
        }

        public override Color? GetAlpha(Color lightColor) {
            return Color.White;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.immune[projectile.owner] = 4;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
            var tex = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(tex,
                projectile.Center - Main.screenPosition, tex.Frame(),
                Color.White, projectile.rotation,
                tex.Size() * 0.5f,
                projectile.scale, SpriteEffects.None, 0f);
            // 返回false阻止原版的绘制
            return false;
        }


        private class ForwardState : ProjState {
            public override void AI(SMProjectile proj) {
                var projectile = proj.projectile;
                projectile.rotation += 0.05f * projectile.velocity.Length();
                proj.Timer++;
                float factor = proj.Timer / 90f;
                factor *= factor;
                projectile.velocity = Vector2.Normalize(projectile.velocity) * 9f * (1.0f - factor);
                if (proj.Timer >= 90) proj.SetState<ChaseState>();
            }
        }

        private class ChaseState : ProjState {
            public override void AI(SMProjectile proj) {
                var projectile = proj.projectile;
                proj.Timer++;
                projectile.rotation += 0.05f * projectile.velocity.Length();
                var target = ProjUtils.FindNearestEnemy(projectile.Center, 1000, (npc) => npc.value > 1 || npc.damage > 1);
                if (target != null) {
                    var unit = Vector2.Normalize(target.Center - projectile.Center);
                    projectile.velocity = (projectile.velocity * 15f + unit * 10f) / 16f;
                }
                if (proj.Timer >= 300) proj.SetState<BackwardState>();
            }
        }


        private class BackwardState : ProjState {
            public override void AI(SMProjectile proj) {
                var projectile = proj.projectile;
                projectile.rotation -= 0.05f * projectile.velocity.Length();
                Player owner = Main.player[projectile.owner];
                projectile.velocity = Vector2.Normalize(owner.Center - projectile.Center) * 9f;
                if (projectile.Hitbox.Intersects(owner.Hitbox)) {
                    projectile.Kill();
                }
            }
        }

        public override void Initialize() {
            RegisterState(new ForwardState());
            RegisterState(new ChaseState());
            RegisterState(new BackwardState());
        }
    }
}
