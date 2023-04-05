using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace P1test.Projectiles
{
    public class SingWasp : ModProjectile
    {
        public bool count = false;

        public override void SetDefaults()
        {
            Projectile.width = 4;//22
            Projectile.height = 3;//14
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 300;
            Projectile.alpha = 1;
            Projectile.light = 0.3f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            AIType = 34;
            Projectile.penetrate = 40;
        }
        /*public override void PreDraw(SpriteBatch spriteBatch, Color lightColor) 
        { 
            
            int num1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 15, projectile.velocity.X, projectile.velocity.Y, 100f, default(Color), 1f);

            Main.dust[num1].noGravity = true;
            Main.dust[num1].velocity *= 0.1f;

        }*/


        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //If collide with tile, reduce the penetrate.
            //So the projectile can reflect at most 5 times
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                    Projectile.velocity.X = Projectile.velocity.X + Main.rand.Next(3) - 2;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                    Projectile.velocity.Y = Projectile.velocity.Y + Main.rand.Next(3) - 2;
                }
            }
            return false;
        }


        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {

            target.AddBuff(Mod.Find<ModBuff>("Wasped").Type, 600);
            Projectile.Kill();
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(Mod.Find<ModBuff>("Wasped").Type, 600);
            Projectile.Kill();
        }


        public override void Kill(int timeLeft)
        {
           
            /*for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Fire Dust spawn
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }*/
           

        }


       /*public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            if(count == true)
            {
                hitbox.Inflate(500, 500); /////////////
            }
                
        }*/
        

        public override void AI()
        {
            /*
            float WaspS = (0 + Main.rand.Next(3) - 2) * 0.1f;

            projectile.velocity.X = projectile.velocity.X + WaspS;
            projectile.velocity.Y = projectile.velocity.Y + WaspS;
            */

            //projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) ; //+ 1.57f
            
            /*
            for (int index1 = 0; index1 < 4; ++index1)//10
            {
                float num1 = (float)(projectile.Center.X - projectile.velocity.X / 10.0 * (double)index1);
                float num2 = (float)(projectile.Center.Y - projectile.velocity.Y / 10.0 * (double)index1);
                int index2 = Dust.NewDust(new Vector2(num1, num2), 1, 1, 31, 0.0f, 0.0f, 0, default(Color), 1.25f);
                Main.dust[index2].alpha = projectile.alpha;
                Main.dust[index2].position.X = (float)num1;
                Main.dust[index2].position.Y = (float)num2;
                Main.dust[index2].velocity = new Vector2(0, 0);
                Main.dust[index2].noGravity = true;
            }
            for (int index1 = 0; index1 < 3; ++index1)//10
            {
                float num1 = (float)(projectile.Center.X - projectile.velocity.X / 10.0 * (double)index1);
                float num2 = (float)(projectile.Center.Y - projectile.velocity.Y / 10.0 * (double)index1);
                int index2 = Dust.NewDust(new Vector2(num1, num2), 1, 1, 160, 0.0f, 0.0f, 0, Color.Blue, 1.75f);//59, 29
                Main.dust[index2].alpha = projectile.alpha;
                Main.dust[index2].position.X = (float)num1;
                Main.dust[index2].position.Y = (float)num2;
                Main.dust[index2].velocity = new Vector2(0, 0);
                Main.dust[index2].noGravity = true;
            }
            */




            float num132 = (float)Math.Sqrt((double)(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y));
            float num133 = Projectile.localAI[0];
            if (num133 == 0f)
            {
                Projectile.localAI[0] = num132;
                num133 = num132;
            }
            float num134 = Projectile.position.X;
            float num135 = Projectile.position.Y;
            float num136 = 600f;//300f
            bool flag3 = false;
            int num137 = 0;
            if (Projectile.ai[1] == 0f)
            {
                for (int num138 = 0; num138 < 200; num138++)
                {
                    if (Main.npc[num138].CanBeChasedBy(this, false) && (Projectile.ai[1] == 0f || Projectile.ai[1] == (float)(num138 + 1)))
                    {
                        float num139 = Main.npc[num138].position.X + (float)(Main.npc[num138].width / 2);
                        float num140 = Main.npc[num138].position.Y + (float)(Main.npc[num138].height / 2);
                        float num141 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num139) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num140);
                        if (num141 < num136 && Collision.CanHit(new Vector2(Projectile.position.X + (float)(Projectile.width / 2), Projectile.position.Y + (float)(Projectile.height / 2)), 1, 1, Main.npc[num138].position, Main.npc[num138].width, Main.npc[num138].height))
                        {
                            num136 = num141;
                            num134 = num139;
                            num135 = num140;
                            flag3 = true;
                            num137 = num138;
                        }
                    }
                }
                if (flag3)
                {
                    Projectile.ai[1] = (float)(num137 + 1);
                }
                flag3 = false;
            }
            if (Projectile.ai[1] > 0f)
            {
                int num142 = (int)(Projectile.ai[1] - 1f);
                if (Main.npc[num142].active && Main.npc[num142].CanBeChasedBy(this, true) && !Main.npc[num142].dontTakeDamage)
                {
                    float num143 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
                    float num144 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
                    if (Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num143) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num144) < 1000f)
                    {
                        flag3 = true;
                        num134 = Main.npc[num142].position.X + (float)(Main.npc[num142].width / 2);
                        num135 = Main.npc[num142].position.Y + (float)(Main.npc[num142].height / 2);
                    }
                }
                else
                {
                    Projectile.ai[1] = 0f;
                }
            }
            if (!Projectile.friendly)
            {
                flag3 = false;
            }
            if (flag3)
            {
                float num145 = num133;
                Vector2 vector10 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num146 = num134 - vector10.X;
                float num147 = num135 - vector10.Y;
                float num148 = (float)Math.Sqrt((double)(num146 * num146 + num147 * num147));
                num148 = num145 / num148;
                num146 *= num148;
                num147 *= num148;
                int num149 = 8;
              
                Projectile.velocity.X = (Projectile.velocity.X * (float)(num149 - 1) + num146) / (float)num149;
                Projectile.velocity.Y = (Projectile.velocity.Y * (float)(num149 - 1) + num147) / (float)num149;
            }
        }



    }
}