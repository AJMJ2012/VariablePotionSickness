using System.ComponentModel;
using System;
using Terraria.ModLoader.Config;

namespace VariablePotionSickness
{
    [Label("Server Config")]
	public class ServerConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;
		public static ServerConfig Instance;

		public LifeConfig Life = new LifeConfig();
		[Label("Health Settings")]
		[BackgroundColor(255, 150, 150)] // Colors.RarityRed
		public class LifeConfig {
			[Label("Enabled")]
			[DefaultValue(true)]
			public bool Enabled = true;

			[Label("Ratio")]
			[BackgroundColor(255, 150, 150)] // Colors.RarityRed
			public RatioMethod Ratio = new RatioMethod();
			public class RatioMethod {
				[Label("Use Ratio")]
				[Tooltip("Calculations are based on your maximum health")]
				[DefaultValue(false)]
				public bool Enabled = false;

				[Label("Base Time")]
				[Tooltip("Time based on the healed amount vs max health (seconds)")]
				[Range(0, 600)]
				[DefaultValue(200)]
				public int BaseTime = 120;

				[Label("Additional Sickness Time (seconds)")]
				[Range(-600, 600)]
				[DefaultValue(0)]
				public int AdditionalSickTime = 0;
			}

			[Label("Fixed")]
			[BackgroundColor(255, 150, 150)] // Colors.RarityRed
			public FixedMethod Fixed = new FixedMethod();
			public class FixedMethod {
				[Label("Base Healing Amount")]
				[Range(0, 600)]
				[DefaultValue(150)]
				public int BaseAmount = 150;

				[Label("Base Sickness Time (seconds)")]
				[Range(0, 600)]
				[DefaultValue(45)]
				public int BaseSickTime = 45;

				[Label("Additional Sickness Time (seconds)")]
				[Range(-600, 600)]
				[DefaultValue(15)]
				public int AdditionalSickTime = 15;
			}

			[Label("Maximum Sickness Time (seconds)")]
			[Range(0, 600)]
			[DefaultValue(120)]
			public int MaxSickSeconds = 120;
		}

		public ManaConfig Mana = new ManaConfig();
		[Label("Mana Settings")]
		[BackgroundColor(150, 150, 255)] // Colors.RarityBlue
		public class ManaConfig {
			[Label("Enabled")]
			[DefaultValue(true)]
			public bool Enabled = true;

			[Label("Ratio")]
			[BackgroundColor(150, 150, 255)] // Colors.RarityBlue
			public RatioMethod Ratio = new RatioMethod();
			public class RatioMethod {
				[Label("Use Ratio")]
				[Tooltip("Calculations are based on your maximum mana")]
				[DefaultValue(false)]
				public bool Enabled = false;

				[Label("Base Time")]
				[Tooltip("Time based on the restored amount vs max mana (seconds)")]
				[Range(0, 600)]
				[DefaultValue(3f)]
				public int BaseTime = 6;

				[Label("Additional Sickness Time (seconds)")]
				[Range(-600, 600)]
				[DefaultValue(0)]
				public int AdditionalSickTime = 0;
			}

			[Label("Fixed")]
			[BackgroundColor(150, 150, 255)] // Colors.RarityBlue
			public FixedMethod Fixed = new FixedMethod();
			public class FixedMethod {
				[Label("Base Restoration Amount")]
				[Range(0, Int32.MaxValue)]
				[DefaultValue(200)]
				public int BaseAmount = 200;

				[Label("Base Sickness Time (seconds)")]
				[Range(0, 600)]
				[DefaultValue(4)]
				public int BaseSickTime = 4;

				[Label("Additional Sickness Time (seconds)")]
				[Range(-600, 600)]
				[DefaultValue(2)]
				public int AdditionalSickTime = 2;
			}

			[Label("Base Maximum Sickness Time (seconds)")]
			[Range(0, 600)]
			[DefaultValue(20)]
			public int MaxSickSeconds = 20;

			[Label("Sickness Magic Power Reduction (percent)")]
			[Slider]
			[Range(0, 100)]
			[DefaultValue(50)]
			public int SickPowerReduction = 50;

			[Label("Philosopher's Stone Reduces Mana Sickness")]
			[DefaultValue(false)]
			public bool PhilosophersReduceSickTime = false;
		}

		public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message) {
			return true;//DALib.Auth.IsAdmin(whoAmI, ref message);
		}
	}

	public static class Config {
		public static ServerConfig Server => ServerConfig.Instance;
	}
}
