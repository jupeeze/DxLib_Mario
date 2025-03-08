using DxLibDLL;
using System;

internal static class Player {
    private static readonly int SIZE_X = 32, SIZE_Y = 64;
    private static readonly int JUMP_FORCE = -15;
    private static readonly int GRAVITY_INCREMENT = 1;

    private static int _imagePosX2 = 100;
    private static int _imagePosY2 = Game.GROUND_POS;

    private static int _gravity = 0;

    private static int _imageNum = 0;
    private static int[] _playerImages;

    public static int ImagePosX1 => ImagePosX2 - (2 * SIZE_X);
    public static int ImagePosX2 => _imagePosX2;

    public static int ImagePosY1 => ImagePosY2 - (2 * SIZE_Y);
    public static int ImagePosY2 => _imagePosY2;

    public static int HitboxPosX1 => ((ImagePosX1 + ImagePosX2) / 2) - 10;
    public static int HitboxPosX2 => ((ImagePosX1 + ImagePosX2) / 2) + 10;
    public static int HitboxCenterX => (HitboxPosX1 + HitboxPosX2) / 2;

    public static int HitboxPosY1 => ((ImagePosY1 + ImagePosY2) / 2) - 20;
    public static int HitboxPosY2 => ImagePosY2;
    public static int HitboxCenterY => (HitboxPosY1 + HitboxPosY2) / 2;

    private enum PlayerState { Idle, Running, Jumping }

    private static PlayerState _state = PlayerState.Idle;

    private static PlayerState State {
        get => _state;
        set {
            _state = value;
            _imageNum = 0;

            if (_state == PlayerState.Idle) {
                _gravity = 0;
                _imagePosY2 = Game.GROUND_POS;
            } else if (_state == PlayerState.Jumping) {
                _gravity = JUMP_FORCE;
            }
        }
    }

    public static void Load() {
        _playerImages = Program.LoadSprites(@"tileset_ramina.png", 3, 2, SIZE_X, SIZE_Y);
    }

    #region Update

    public static void Update() {
        ApplyGravity();
        HandleInput();
        UpdatePosition();
    }

    private static void ApplyGravity() {
        if (!IsGround())
            _gravity += GRAVITY_INCREMENT;
        else if (State != PlayerState.Idle)
            State = PlayerState.Idle;
    }

    private static void HandleInput() {
        bool isMouseLeftClicked = (DX.GetMouseInput() == DX.MOUSE_INPUT_LEFT);

        if (isMouseLeftClicked && IsGround())
            State = PlayerState.Jumping;
    }

    private static void UpdatePosition() {
        _imagePosY2 += _gravity;
    }

    #endregion Update

    #region Draw

    public static void Draw() {
        Program.DrawEXGraph(ImagePosX1, ImagePosY1, ImagePosX2 - ImagePosX1, ImagePosY2 - ImagePosY1, GetCurrentImage());

        DX.DrawBox(HitboxPosX1, HitboxPosY1, HitboxPosX2, HitboxPosY2, Program.BLUE_COLOR, DX.FALSE);
    }

    private static int GetCurrentImage() {
        switch (State) {
            case PlayerState.Idle:
                return _playerImages[0];

            case PlayerState.Running:
            case PlayerState.Jumping:
                _imageNum ^= 1;
                return _playerImages[1 + _imageNum];

            default: throw new InvalidOperationException();
        }
    }

    #endregion Draw

    private static bool IsGround() {
        return _imagePosY2 >= Game.GROUND_POS;
    }
}
