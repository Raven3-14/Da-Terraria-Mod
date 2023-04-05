using System;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace P1test.Items.Weapons
{
	public class AirStrike : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("B.R.B");
			Tooltip.SetDefault("Big Red Button \n For all your airstrike needs! \n25 rocket air strike at cursor");
		}

		public override void SetDefaults()
		{
			Item.damage = 150; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			Item.DamageType = DamageClass.Ranged; // sets the damage type to ranged
			Item.width = 22; // hitbox width of the item
			Item.height = 23; // hitbox height of the item
			Item.useTime = 360; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 180; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = 4; // how you use the item (swinging, holding out, etc)
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 40.0f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			Item.value = 10000; // how much the item sells for (measured in copper)
			Item.rare = ItemRarityID.Green; // the color that the item's name will be in-game
			Item.UseSound = SoundID.Item11; // The sound that this item plays when used.
			Item.autoReuse = false; // if you can hold click to automatically use it again
			Item.shoot = 10; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 10f; // the speed of the projectile (measured in pixels per frame)
			Item.useAmmo = AmmoID.Rocket; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

		/*
		 * Feel free to uncomment any of the examples below to see what they do
		 */

		//What if I wanted this gun to have a 38% chance not to consume ammo?
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= .1f;
		}
		/*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)  //Microsoft.Xna.Framework.//was override instead of virtual
		{
			for (int i = 0; i < 5; i++) //replace 3 with however many projectiles you like
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); //12 is the spread in degrees, although like with Set Spread it's technically a 24 degree spread due to the fact that it's randomly between 12 degrees above and 12 degrees below your cursor.
				Projectile.NewProjectile(position.X+i, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI); //create the projectile perturbedSpeed.X, perturbedSpeed.Y
			}
			return true;
		}*/


		// What if I wanted it to work like Uzi, replacing regular bullets with High Velocity Bullets?
		// Uzi/Molten Fury style: Replace normal Bullets with High Velocity
		/*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileID.Bullet) // or ProjectileID.WoodenArrowFriendly
			{
				type = ProjectileID.BulletHighVelocity; // or ProjectileID.FireArrow;
			}
			return true; // return true to allow tmodloader to call Projectile.NewProjectile as normal
		}*/

		// What if I wanted it to shoot like a shotgun?
		// Shotgun style: Multiple Projectiles, Random spread 
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			type = ProjectileID.RocketIII;
			int numberProjectiles = 25; // 4 or 5 shots

			//int numberProjectiles = 6; // shoots 6 projectiles
			for (int index = 0; index < numberProjectiles; ++index)
			{
				Vector2 vector2_1 = new Vector2((float)((double)player.position.X + (double)player.width * 0.5 + (double)(Main.rand.Next(201) * -player.direction) + ((double)Main.mouseX + (double)Main.screenPosition.X - (double)player.position.X)), (float)((double)player.position.Y + (double)player.height * 0.5 - 600.0));   //this defines the projectile width, direction and position
				vector2_1.X = (float)(((double)vector2_1.X + (double)player.Center.X) / 2.0) + (float)Main.rand.Next(-200, 201);
				vector2_1.Y -= (float)(100 * index);
				float num12 = (float)Main.mouseX + Main.screenPosition.X - vector2_1.X;
				float num13 = (float)Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
				if ((double)num13 < 0.0) num13 *= -1f;
				if ((double)num13 < 20.0) num13 = 20f;
				float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
				float num15 = Item.shootSpeed / num14;
				float num16 = num12 * num15;
				float num17 = num13 * num15;
				float SpeedX = num16 + (float)Main.rand.Next(-40, 41) * 0.02f; //change the Main.rand.Next here to, for example, (-10, 11) to reduce the spread. Change this to 0 to remove it altogether
				float SpeedY = num17 + (float)Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(source, vector2_1.X, vector2_1.Y, SpeedX, SpeedY, type, damage, knockback, Main.myPlayer, 0.0f, (float)Main.rand.Next(5));
			}
			

			
			/*for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(18)); // 30 degree spread.
																												// If you want to randomize the speed to stagger the projectiles
				float scale = 1f - (Main.rand.NextFloat() * .3f);
				perturbedSpeed = perturbedSpeed * scale;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);

			
			}*/
			return false; // return false because we don't want tmodloader to shoot projectile
		}
		

		
		
			// What if I wanted an inaccurate gun? (Chain Gun)
		// Inaccurate Gun style: Single Projectile, Random spread 
		

		// What if I wanted multiple projectiles in a even spread? (Vampire Knives) 
		// Even Arc style: Multiple Projectile, Even Spread 
		
	
		// Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
		/*public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, 18);
		}*/
	}
}
// How can I make the shots appear out of the muzzle exactly?
// Also, when I do this, how do I prevent shooting through tiles?
/*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
{
	Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
	if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
	{
		position += muzzleOffset;
	}
	return true;
}*/

// How can I get a "Clockwork Assault Rifle" effect?
// 3 round burst, only consume 1 ammo for burst. Delay between bursts, use reuseDelay
/*	The following changes to SetDefaults()
	item.useAnimation = 12;
	item.useTime = 4;
	item.reuseDelay = 14;
public override bool ConsumeAmmo(Player player)
{
	// Because of how the game works, player.itemAnimation will be 11, 7, and finally 3. (useAnimation - 1, then - useTime until less than 0.) 
	// We can get the Clockwork Assault Riffle Effect by not consuming ammo when itemAnimation is lower than the first shot.
	return !(player.itemAnimation < item.useAnimation - 2);
}*/

// How can I shoot 2 different projectiles at the same time?
/*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
{
	// Here we manually spawn the 2nd projectile, manually specifying the projectile type that we wish to shoot.
	Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ProjectileID.GrenadeI, damage, knockBack, player.whoAmI);
	// By returning true, the vanilla behavior will take place, which will shoot the 1st projectile, the one determined by the ammo.
	return true;
}*/
// How can I choose between several projectiles randomly?
/*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
{
	// Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
	type = Main.rand.Next(new int[] { type, ProjectileID.GoldenBullet, ProjectileType<Projectiles.ExampleBullet>() });
	return true;
}*/



