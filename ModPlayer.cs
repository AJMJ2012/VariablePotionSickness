using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace VariablePotionSickness
{
    public class MPlayer : ModPlayer {
		public override void PreUpdate() {
			if (Config.Server.Life.Enabled) {
				Item.potionDelay = 0;
				Item.restorationDelay = 0;
			}
			else {
				Item.potionDelay = 3600;
				Item.restorationDelay = 3000;
			}
			if (Config.Server.Mana.Enabled) {
				Player.manaSickTime = 0;
				Player.manaSickTimeMax = Int32.MaxValue;
				Player.manaSickLessDmg = 0;
			}
			else {
				Player.manaSickTime = 300;
				Player.manaSickTimeMax = 600;
				Player.manaSickLessDmg = 0.25f;
			}
		}

		public override void PostUpdateBuffs() {
			if (Config.Server.Mana.Enabled) {
				int sicknessTime = 0;
				for (int i = 0; i < this.Player.buffType.Length; i++) {
					if (this.Player.buffType[i] == BuffID.ManaSickness) {
						sicknessTime = this.Player.buffTime[i];
						break;
					}
				}
				this.Player.manaSickReduction = (Config.Server.Mana.SickPowerReduction / 100f) * Math.Min(1, (float)sicknessTime / (float)(Config.Server.Mana.MaxSickSeconds * 60));
			}
		}
	}
}

