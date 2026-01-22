using sfml_csharp.Resources;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace sfml_csharp;

public sealed class Game
{
    private const float PlayerSpeed = 100.0f;
    private const float TimePerFrameInSeconds = 1.0f / 60.0f;
    private readonly Clock _clock = new();

    private readonly CircleShape _player = new(50)
    {
        Position = new Vector2f(100, 100)
    };

    private readonly RenderWindow _window = new(new VideoMode(640, 480), "SFML Application");
    private bool _isMovingDown;
    private bool _isMovingLeft;
    private bool _isMovingRight;

    private bool _isMovingUp;

    private readonly TextureHolder _texture = new();

    public Game()
    {
        _texture.Load(TextureIdEnum.Player, "Assets/player.png");
        _player.Texture = _texture.Get(TextureIdEnum.Player);
    }

    public void Run()
    {
        var timeSinceLastUpdate = Time.Zero;

        while (_window.IsOpen)
        {
            ProcessEvents();
            timeSinceLastUpdate += _clock.ElapsedTime;

            while (timeSinceLastUpdate.AsMilliseconds() > TimePerFrameInSeconds)
            {
                timeSinceLastUpdate -= _clock.ElapsedTime;
                ProcessEvents();
                Update();
            }

            Render();
        }
    }

    private void ProcessEvents()
    {
        _window.Closed += (_, _) => _window.Close();
        _window.KeyPressed += (_, e) => HandlePlayerInput(e.Code, true);
        _window.KeyReleased += (_, e) => HandlePlayerInput(e.Code, false);

        _window.DispatchEvents();
    }

    private void Update()
    {
        var movement = new Vector2f(0, 0);

        if (_isMovingUp) movement.Y -= 1;
        if (_isMovingDown) movement.Y += 1;
        if (_isMovingRight) movement.X += 1;
        if (_isMovingLeft) movement.X -= 1;

        // Sums the two vectors
        _player.Position += movement * TimePerFrameInSeconds * PlayerSpeed;
    }

    private void Render()
    {
        _window.Clear(Color.Black);
        _window.Draw(_player);
        _window.Display();
    }

    private void HandlePlayerInput(Keyboard.Key key, bool isPressed)
    {
        switch (key)
        {
            case Keyboard.Key.Q:
                _isMovingLeft = isPressed;
                break;
            case Keyboard.Key.Z:
                _isMovingUp = isPressed;
                break;
            case Keyboard.Key.D:
                _isMovingRight = isPressed;
                break;
            case Keyboard.Key.S:
                _isMovingDown = isPressed;
                break;
        }
    }
}