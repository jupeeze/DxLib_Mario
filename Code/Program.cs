using DxLibDLL;

internal static class Game {
    public static readonly int GROUND_SIZE = 32;
    public static readonly int GROUND_POS = Program.SCREEN_Y - GROUND_SIZE;

    public static int CameraOffsetX = 0;

    public static void Run() {
        Load();
        Loop();
    }

    private static void Load() {
        Ground.Load();
        Player.Load();
    }

    private static void Loop() {
        while (DX.ProcessMessage() == 0) {
            Update();
            Draw();
        }
    }

    private static void Update() {
        Player.Update();
    }

    private static void Draw() {
        DX.ClearDrawScreen();

        Ground.Draw();
        Player.Draw();

        DX.ScreenFlip();
    }
}

internal static class Program {
    public static readonly int SCREEN_X = 640, SCREEN_Y = 480;

    public static string ASSET_PATH = @"..\..\Assets\";

    public static readonly uint BLUE_COLOR = DX.GetColor(0, 0, 255);

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

    public static int[] LoadSprites(string filePath, int divX, int divY, int sizeX, int sizeY) {
        int spriteCount = divX * divY;
        int[] sprites = new int[spriteCount];
        DX.LoadDivGraph(ASSET_PATH + filePath, spriteCount, divX, divY, sizeX, sizeY, sprites);
        return sprites;
    }

    public static void DrawEXGraph(int posX, int posY, int sizeX, int sizeY, int GrHandle) =>
        DX.DrawExtendGraph(posX, posY, posX + sizeX, posY + sizeY, GrHandle, DX.TRUE);
}
