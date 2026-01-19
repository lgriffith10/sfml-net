using System.Reflection.Metadata;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace sfml_csharp;

public sealed class Game
{
    private const float PlayerSpeed = 15.0f;
    private const float TimePerFrameInSeconds = 1.0f / 60.0f;
    
    private readonly RenderWindow _window = new(new VideoMode(640, 480), "SFML Application");
    private readonly Clock _clock = new();
    private readonly CircleShape _player = new(50)
    {
        Position = new Vector2f(100, 100),
    };

    private Texture _texture;

    private bool _isMovingUp = false;
    private bool _isMovingDown = false;
    private bool _isMovingLeft = false;
    private bool _isMovingRight = false;

    public Game()
    {
        try
        {
            _texture = new Texture("Assets/player.png");
        }
        catch (LoadingFailedException e)
        {
            Console.WriteLine("Error while loading texture: " + e.Message);
        }
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
        _player.Texture = _texture;
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
            default:
                break;
        }
    }
    
}