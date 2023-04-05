﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace P1test.Items.Over
{
	
	public class ExampleHermesBoots : ModItem
	{
		public override void SetStaticDefaults() {
			Item.width = 32;
			Item.height = 32;
			DisplayName.SetDefault("Bracers of Dimensional Slip-Stream");
			Tooltip.SetDefault("Prepare for light speed");
		}

		public override void SetDefaults() {

			Item.accessory = true; // Makes this item an accessory.
			Item.rare = ItemRarityID.Blue; 
			Item.value = Item.sellPrice(gold: 100); // Sets the item sell price to 100 gold coin.
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{

			player.moveSpeed += 50.0f; // The acceleration multiplier of the player's movement speed
			player.jumpBoost = true;
			player.jumpSpeedBoost += 6.0f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.20f;
			player.accRunSpeed = 40f;
			player.blackBelt = true;
			player.dash = 1;
			//player.dashDelay = 0;
			//player.dashTime = 60;
			player.extraFall = 100;

			player.maxFallSpeed += 20.0f;
			hideVisual = true;
		}
		public override void AddRecipes()
		{
			// because we don't call base.AddRecipes(), we erase the previously defined recipe and can now make a different one
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 1);
			
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	
	}
}
