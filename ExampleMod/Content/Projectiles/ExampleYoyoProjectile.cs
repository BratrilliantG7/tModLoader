using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExampleMod.Content.Projectiles
{
	// ExampleYoyo and ExampleYoyoProjectile show the minimum amound of code needed to create a Yoyo using the existing vanilla code and behavior.
	// ExampleAdvancedYoyo and ExampleAdvancedYoyoProjectile need to be consulted if more advanced customization is required.

	// ExampleYoyoProjectile is a copy of the Cascade yoyo projectile.
	public class ExampleYoyoProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			// The following sets are only applicable to yoyos that use aiStyle 99.
			// The values used here are the Cascade's default values.

			// YoyosLifeTimeMultiplier is how long (in seconds) the yoyo will stay out before automatically returning to the player.
			// Vanilla values range from 3f(Wood) to 16f(Chik), and defaults to -1. Leaving as -1 will make the time infinite.
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 13f; // The Cascade will remain active for ~13 seconds.

			// YoyosMaximumRange is the maximum distance the yoyo will rest from the player. The range in tiles is approximately YoyosMaximumRange divided by 16.
			// Vanilla values range from 130f(Wood) to 400f(Terrarian), and defaults to 200f.
			// Values above 960f (60 tiles), or 744f if the player has yoyoString, have little to no benefit since the player normally reach beyond that range.
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 235f; // The Cascade's range is approx. 14.6875 tiles.

			// YoyosTopSpeed is the top speed of the yoyo projectile.
			// Vanilla values range from 9f(Wood) to 17.5f(Terrarian), and defaults to 10f.
			// Speeds above 18f are not recommended as the yoyo has a tendency to fly past the cursor instead of stop on it, making it unwieldy to use.
			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 14f;
		}

		public override void SetDefaults() {
			Projectile.netImportant = true; // This ensures that the projectile is synced when a player joins the world.
			Projectile.width = 16; // The width of the yoyo projectile.
			Projectile.height = 16; // The height of the yoyo projectile.
			Projectile.friendly = true; // Lets the projectile hit enemy NPCs.
			Projectile.penetrate = -1; // The yoyo projectile has infinite piercing.
			Projectile.scale = 1f; // How large to render the projectile graphic. Some vanilla yoyos have a larger scale, such as the Kraken at 1.2f or the Eye of Cthulhu at 1.15f.
			Projectile.DamageType = DamageClass.Melee; // This projectile deals Melee damage.

			// Here we use the yoyo aiStyle(99) and copy the Cascade's unique AI code, which only covers the dust particles it generates. We will have to adapt other parts of the code that we want to use.
			Projectile.aiStyle = ProjAIStyleID.Yoyo;
			AIType = ProjectileID.Cascade;
		}

		// The following Method is an additional behaviour of the Cascade that is not automatically inherited through the use of Projectile.aiStyle and AIType.
		// When we hit an NPC, there is a 1 in 3 chance of applying the On Fire debuff to them for 1 to 4 seconds.
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			if (Main.rand.NextBool(3)) {
				target.AddBuff(BuffID.OnFire, Main.rand.Next(60, 240));
			}
		}
	}
}
