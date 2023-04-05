using System;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace P1test.Items.Weapons
{
	public class WaspRifle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Singulatiy-Wasp Deployment Mechanism");
			Tooltip.SetDefault("Impractical Doom");
		}

		public override void SetDefaults()
		{
			Item.damage = 40; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			Item.DamageType = DamageClass.Ranged; // sets the damage type to ranged
			Item.width = 44; // hitbox width of the item//44
			Item.height = 18; // hitbox height of the item//18
			Item.useTime = 35; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 35; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.reuseDelay = 36;
			Item.useStyle = ItemUseStyleID.Shoot; // how you use the item (swinging, holding out, etc)
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4.0f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			Item.value = 10000; // how much the item sells for (measured in copper)
			Item.rare = ItemRarityID.Green; // the color that the item's name will be in-game
			Item.UseSound = SoundID.Item11; // The sound that this item plays when used.
			Item.autoReuse = true; // if you can hold click to automatically use it again
			Item.shoot = ModContent.ProjectileType<Projectiles.WaspBullet>();  //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 10f; // the speed of the projectile (measured in pixels per frame)
			Item.useAmmo = ModContent.ItemType<WaspCan>(); // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
		}




		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8, 0);//-1
		}

		// What if I wanted it to shoot like a shotgun?
		// Shotgun style: Multiple Projectiles, Random spread 
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{





			Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			/*
			Vector2 Temp = muzzleOffset;
			Vector2 Temp2 = muzzleOffset;
			Temp = muzzleOffset / 2;
			Temp2 = Temp2 - Temp;

			int dustQuantity = 7; // How many particles do you want ?

			for (int i = 0; i < dustQuantity; i++)

			{

				Vector2 dustOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 64f;//64

				Vector2 spawnPosition = player.Center + Temp2;//use muzzle offset

				int dust = Dust.NewDust(spawnPosition, 0, 0, 75); // Create the dust, "6" is the dust type (fire, in that case).



				Main.dust[dust].noGravity = true; // Is the dust affected by gravity ?

				Main.dust[dust].velocity *= 0.01f;    // Change the dust velocity.

				Main.dust[dust].scale = 0.5f;    // Change the dust size.

			}

			*/

			/*
			type = ModContent.ProjectileType<Projectiles.WaspBullet>();

			Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
			*/
			return true; // return false because we don't want tmodloader to shoot projectile
		}

		// What if I wanted an inaccurate gun? (Chain Gun)
		// Inaccurate Gun style: Single Projectile, Random spread 
		/*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}*/

		// What if I wanted multiple projectiles in a even spread? (Vampire Knives) 
		// Even Arc style: Multiple Projectile, Even Spread 
		/*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 3 + Main.rand.Next(3); // 3, 4, or 5 shots
			float rotation = MathHelper.ToRadians(45);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}*/

		// Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
		/*public override Vector2? HoldoutOffset()
		{
			return new Vector2(10, 0);
		}*/

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
	}

}

