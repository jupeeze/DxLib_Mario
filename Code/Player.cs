using DxLibDLL;

internal static class Player {
    private static int _y = Game.GROUND_POS;
    private static int _gravity = 0;

    private static int[] _playerImage;

    private static readonly int TILE_SIZE = 32;
    private static readonly int JUMP_FORCE = -15;
    private static readonly int GRAVITY_INCREMENT = 1;

    public static void Load() {
        _playerImage = Program.LoadSprites(@"tileset_ramina.png", 3, 2, 32, 64);
    }

    public static void Draw() {
        Program.DrawEXGraph(TILE_SIZE, _y - 96, 48, 96, _playerImage[1]);
    }

    public static void Update() {
        if (!IsGround()) {
            _gravity += GRAVITY_INCREMENT;
        } else {
            _y = Game.GROUND_POS;
            _gravity = 0;
        }

        if (DX.GetMouseInput() == DX.MOUSE_INPUT_LEFT && IsGround()) {
            _gravity = JUMP_FORCE;
        }

        _y += _gravity;
    }

    private static bool IsGround() {
        return _y >= Game.GROUND_POS;
    }
}
