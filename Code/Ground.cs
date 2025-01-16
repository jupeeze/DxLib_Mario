using DxLibDLL;

internal class Ground
{
	private static readonly int TILE_COUNT = Program.SCREEN_X / Game.GROUND_SIZE + 1;

	private static int[] _groundImages;

	public static void Load() {
		_groundImages = Program.LoadSprites(@"tileset.png", 25, 23, 16, 16);
	}

	public static void Draw() {
		for (int i = 0; i < TILE_COUNT; i++) {
			Program.DrawEXGraph(i * Game.GROUND_SIZE, Game.GROUND_POS, Game.GROUND_SIZE, Game.GROUND_SIZE, _groundImages[28]);
		}
	}
}
