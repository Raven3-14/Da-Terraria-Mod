using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace P1test.Projectiles
{
    public class HydraRocket : ModProjectile
    {
        public bool count = false;

        public override void SetDefaults()
        {
            Projectile.width = 22;//22
            Projectile.height = 14;//14
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 600;
            Projectile.alpha = 1;
            Projectile.light = 0.3f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            AIType = 34;
        }
        /*public override void PreDraw(SpriteBatch spriteBatch, Color lightColor) 
        { 
            
            int num1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 15, projectile.velocity.X, projectile.velocity.Y, 100f, default(Color), 1f);

            Main.dust[num1].noGravity = true;
            Main.dust[num1].velocity *= 0.1f;

        }*/

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(Mod.Find<ModBuff>("VoidBurn").Type, 60);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(Mod.Find<ModBuff>("VoidBurn").Type, 60);
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
            Player player = Main.player[Projectile.owner];

            //Just showing off each parameter of NewProjectile function
            float centerX = Projectile.Center.X; //Spawn at the projectile's last center x when it gets destroyed
            float centerY = Projectile.Center.Y; //Spawn at the projectile's last center y when it gets destroyed
            float velocityX = 0; //Doesn't move in the x direction
            float velocityY = 0; //Doesn't move in the y direction
			
            int type = Mod.Find<ModProjectile>("KaBoom").Type; //just the projectile you'll be firing
            int damage = 75; //no damage
            float knockBack = 20f; //no knockback

            //Many ways of doing owner
            int owner = Main.myPlayer;
            owner = Projectile.owner;
            owner = player.whoAmI;

            //Now let's put this all together, first is most simple
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), centerX, centerY, velocityX, velocityY, type, damage, knockBack, owner);


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
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) ; //+ 1.57f

            //P1testRocketMethods.chaseEnemy(projectile.identity, projectile.type);
            for (int index1 = 0; index1 < 4; ++index1)//10
            {
                float num1 = (float)(Projectile.Center.X - Projectile.velocity.X / 10.0 * (double)index1);
                float num2 = (float)(Projectile.Center.Y - Projectile.velocity.Y / 10.0 * (double)index1);
                int index2 = Dust.NewDust(new Vector2(num1, num2), 1, 1, 31, 0.0f, 0.0f, 0, default(Color), 1.25f);
                Main.dust[index2].alpha = Projectile.alpha;
                Main.dust[index2].position.X = (float)num1;
                Main.dust[index2].position.Y = (float)num2;
                Main.dust[index2].velocity = new Vector2(0, 0);
                Main.dust[index2].noGravity = true;
            }
            for (int index1 = 0; index1 < 3; ++index1)//10
            {
                float num1 = (float)(Projectile.Center.X - Projectile.velocity.X / 10.0 * (double)index1);
                float num2 = (float)(Projectile.Center.Y - Projectile.velocity.Y / 10.0 * (double)index1);
                int index2 = Dust.NewDust(new Vector2(num1, num2), 1, 1, 160, 0.0f, 0.0f, 0, Color.Blue, 1.75f);//59, 29
                Main.dust[index2].alpha = Projectile.alpha;
                Main.dust[index2].position.X = (float)num1;
                Main.dust[index2].position.Y = (float)num2;
                Main.dust[index2].velocity = new Vector2(0, 0);
                Main.dust[index2].noGravity = true;
            }





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