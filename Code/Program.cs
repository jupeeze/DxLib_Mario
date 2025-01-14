using DxLibDLL;

internal static class Game
{
	public static void Run() {
		Loop();
	}

	public static void Loop() {
		while (DX.ProcessMessage() == 0) {
			DX.ClearDrawScreen();

			Draw();

			DX.ScreenFlip();
		}
	}

	public static void Draw() {
		DX.DrawString(100, 100, "Hello World", DX.GetColor(255, 255, 255));
	}
}

internal class Program
{
	private static void Main() {
		Init();

		Game.Run();

		DX.DxLib_End();
	}

	private static void Init() {
		DX.ChangeWindowMode(DX.TRUE);
		DX.DxLib_Init();
		DX.SetDrawScreen(DX.DX_SCREEN_BACK);
	}
}
