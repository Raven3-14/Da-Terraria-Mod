using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace P1test.Buffs
{
	// Ethereal Flames is an example of a buff that causes constant loss of life.
	// See ExamplePlayer.UpdateBadLifeRegen and ExampleGlobalNPC.UpdateLifeRegen for more information.
	public class Wasped : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Wasped!");
			Description.SetDefault("You can feel it burrowing inside you!");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			

			//debuff = true;
		}


		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<P1testPlayer>().Wasped = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<P1testGlobalNPC>().Wasped = true;
		}

	}
}
