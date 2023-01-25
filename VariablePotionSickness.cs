using Terraria.ModLoader;
using Terraria;

namespace VariablePotionSickness {
	public class VariablePotionSickness : Mod {
		public override void Unload() {
			Item.potionDelay = 3600;
			Item.restorationDelay = 3000;
			Player.manaSickTime = 300;
			Player.manaSickTimeMax = 600;
			Player.manaSickLessDmg = 0.25f;
		}
	}
}
