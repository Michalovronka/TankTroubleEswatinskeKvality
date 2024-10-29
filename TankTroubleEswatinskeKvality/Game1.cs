using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TankTroubleEswatinskeKvality.Content;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;

namespace TankTroubleEswatinskeKvality;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _playerTexture;
    private Vector2 _playerPosition;
    private readonly Rectangle _playerRectangle = new Rectangle(0, 0, 60, 35);
    
    private Texture2D _bulletTexture;
    private List<Bullet> _bullets = []; 

    private const int Speed = 2;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _playerPosition = new Vector2(100, 100);
        _playerPosition.Normalize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _playerTexture = new Texture2D(GraphicsDevice, 1, 1);
        _playerTexture.SetData(new[] { Color.White });
        _bulletTexture = new Texture2D(GraphicsDevice, 1, 1);
        _bulletTexture.SetData(new[] { Color.Red });
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        KeyboardState state = Keyboard.GetState();
        Vector2 movement = Vector2.Zero;
        
        if (state.IsKeyDown(Keys.D) && !IsOffScreen(new Vector2(_playerPosition.X + Speed, _playerPosition.Y)))
            movement.X += 1;
        if (state.IsKeyDown(Keys.A) && !IsOffScreen(new Vector2(_playerPosition.X - Speed, _playerPosition.Y)))
            movement.X -= 1;
        if (state.IsKeyDown(Keys.W) && !IsOffScreen(new Vector2(_playerPosition.X, _playerPosition.Y - Speed)))
            movement.Y -= 1;
        if (state.IsKeyDown(Keys.S) && !IsOffScreen(new Vector2(_playerPosition.X, _playerPosition.Y + Speed)))
            movement.Y += 1;
        
        if (movement.Length() > 0)
        {
            movement.Normalize(); 
        }
        _playerPosition += movement * Speed;
        
        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            Vector2 startPosition = _playerPosition;
            Vector2 targetPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Bullet newBullet = new Bullet(startPosition, targetPosition, Speed*100);
            _bullets.Add(newBullet);
        }

        for (int i = _bullets.Count - 1; i >= 0; i--)
        {
            _bullets[i].Update(gameTime);
            if (IsOffScreen(_bullets[i].Position))
            {
                _bullets.RemoveAt(i);
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_playerTexture, _playerPosition, _playerRectangle, Color.White);
        
        foreach (var bullet in _bullets)
        {
            bullet.Draw(_spriteBatch, _bulletTexture);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
    
    private bool IsOffScreen(Vector2 position)
    {
        return position.X < 0 ||  position.X + _playerRectangle.Width > GraphicsDevice.Viewport.Width || position.Y < 0 || position.Y + _playerRectangle.Height > GraphicsDevice.Viewport.Height;
    }
    
}