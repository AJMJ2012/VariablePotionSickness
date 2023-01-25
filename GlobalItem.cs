using System.Collections.Generic;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace VariablePotionSickness
{
    public class GItem : GlobalItem {
		public static float GetLifeSickness(Item item, Player player, bool cap = true) {
			float delay = 0;
			if (Config.Server.Life.Ratio.Enabled) {
				delay = ((float)item.healLife / ((float)player.statLifeMax2 - (player.statLifeMax > 400 ? (player.statLifeMax > 500 ? 100 : player.statLifeMax - 400) : 0))) * Config.Server.Life.Ratio.BaseTime;
				delay += Config.Server.Life.Ratio.AdditionalSickTime;
			}
			else {
				delay = ((float)item.healLife / (float)((float)Config.Server.Life.Fixed.BaseAmount / (float)(Config.Server.Life.Fixed.BaseSickTime)));
				delay += Config.Server.Life.Fixed.AdditionalSickTime;
			}
			if (item.type == ItemID.RestorationPotion || player.pStone) {
				delay *= 0.75f;
			}
			return delay * 60;
		}

		public static float GetManaSickness(Item item, Player player, bool cap = true) {
			float delay = 0;
			if (Config.Server.Mana.Ratio.Enabled) {
				delay = ((float)item.healMana / (float)player.statManaMax2) * Config.Server.Mana.Ratio.BaseTime;
				delay += Config.Server.Mana.Ratio.AdditionalSickTime;
			}
			else {
				delay = ((float)item.healMana / (float)((float)Config.Server.Mana.Fixed.BaseAmount / (float)(Config.Server.Mana.Fixed.BaseSickTime)));
				delay += Config.Server.Mana.Fixed.AdditionalSickTime;
			}
			if (player.pStone && Config.Server.Mana.PhilosophersReduceSickTime) {
				delay *= 0.75f;
			}
			return delay * 60;
		}


		public override bool? UseItem(Item item, Player player) {
			if (item.healLife > 0 && Config.Server.Life.Enabled) {
				float delay = GetLifeSickness(item, player);
				player.AddBuff(BuffID.PotionSickness, (int)delay, false);
			}
			if (item.healMana > 0 && Config.Server.Mana.Enabled) {
				float delay = GetManaSickness(item, player);
				player.AddBuff(BuffID.ManaSickness, (int)delay / 2, true);
			}
			return base.UseItem(item, player);
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
			Player player = Main.player[Main.myPlayer];
			if (item.healLife > 0 && Config.Server.Life.Enabled) {
				float delay = GetLifeSickness(item, player);
				tooltips.Add(new TooltipLine(Mod, "HealthSickness", "Adds " + Math.Round(delay / 60f, 1) + " seconds of healing sickness"));
			}
			if (item.healMana > 0 && Config.Server.Mana.Enabled) {
				float delay = GetManaSickness(item, player);
				tooltips.Add(new TooltipLine(Mod, "ManaSickness", "Adds " + Math.Round(delay / 60f, 1) + " seconds of mana sickness"));
			}
		}
	}
}
