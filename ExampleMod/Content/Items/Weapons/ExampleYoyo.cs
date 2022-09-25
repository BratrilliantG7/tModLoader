using ExampleMod.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ExampleMod.Content.Items.Weapons
{
	// ExampleYoyo and ExampleYoyoProjectile show the minimum amound of code needed to create a Yoyo using the existing vanilla code and behavior.
	// ExampleAdvancedYoyo and ExampleAdvancedYoyoProjectile need to be consulted if more advanced customization is required.

	// ExampleYoyo is a copy of the Cascade yoyo.
	public class ExampleYoyo : ModItem {
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Example Yoyo");
			Tooltip.SetDefault("This is a modded Yoyo with normal behaviour.");
			SacrificeTotal = 1; // The number of items needed to be sacrificed in the Journey Mode menu to get infinite access to them.

			// The following sets are used for Gamepad controls and nothing else.
			// Yoyo is used in the gamepad reach check in conjunction with a yoyoString check to add the String's extra range to the cursor.
			ItemID.Sets.Yoyo[Item.type] = true;

			// GamepadExtraRange is how much extra range (in Tiles) should be given to the gamepad cursor while channeling to allow free movement of the yoyo.
			// Vanilla yoyos typically keep this number at just over the yoyo's max reach.
			// To find what number you should use, take your desired Yoyo range (see YoyosMaximumRange in ExampleYoyoProjectile and divide that by 16, rounded up) and subtract the player's base range of 5.
			// For example, the Cascade has a YoyosMaximumRange of 235. Divided by 16 and rounded up is 15, then subtract 5 to get 10, which is the same value the Cascade is set to have in vanilla.
			ItemID.Sets.GamepadExtraRange[Item.type] = 10;

			// GamepadSmartQuickReach is unused in vanilla code as far as I can tell, but may have been planned to force the yoyo to max range if the Smart Cursor was enabled.
			ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
		}

		public override void SetDefaults() {
			// These default values, aside from the Item.shoot value, match the Cascade's default values.

			// The Yoyo item's hitbox width and height (more or less unused for yoyos since noMelee and noUseGraphic are set to true and their projectiles have their own width and height).
			Item.width = 24;
			Item.height = 24;

			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (Holding Up, Swinging, etc.).
			Item.useAnimation = 25; // The use time in game ticks (60 ticks == 1 second).
			Item.useTime = 25; // The use time in game ticks (60 ticks == 1 second).

			Item.damage = 27; // The damage your yoyo will deal (hitting enemies behind walls reduces this by about 25%).
			Item.knockBack = 4.3f; // The yoyo's knockback.
			Item.DamageType = DamageClass.MeleeNoSpeed; // The type of damage the Yoyo deals. MeleeNoSpeed is used so that the player's Melee Speed doesn't effect the yoyo's useTime.

			Item.channel = true; // The yoyo will continually function while the mouse is held down.
			Item.noMelee = true; // Prevents the use animation from damaging anything, that's what the yoyo projectile is for.
			Item.noUseGraphic = true; // Hides the Yoyo graphic in the player's hands

			Item.UseSound = SoundID.Item1; // The sound the item makes when used.
			Item.value = Item.sellPrice(silver: 1, copper: 80); // This yoyo sells for 1 silver and 80 copper.
			Item.rare = ItemRarityID.Orange; // The color of the name of your item.

			Item.shootSpeed = 16f; // How fast the initial projectile will launch from the player.
			Item.shoot = ModContent.ProjectileType<ExampleYoyoProjectile>(); // The Yoyo projectile.
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<ExampleItem>()
				.AddTile<Tiles.Furniture.ExampleWorkbench>()
				.Register();
		}
	}
}
