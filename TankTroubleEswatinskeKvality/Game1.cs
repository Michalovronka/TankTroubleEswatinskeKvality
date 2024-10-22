using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace TankTroubleEswatinskeKvality;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D spriteTexture;
    private Vector2 spritePosition;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice); 
        
        spriteTexture = new Texture2D(GraphicsDevice, 1, 1);
        spriteTexture.SetData(new[] { Color.White });
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        KeyboardState state = Keyboard.GetState();
        if (state.IsKeyDown(Keys.D))
        {
            spritePosition.X += 1;
        }
        if (state.IsKeyDown(Keys.A))
        {
            spritePosition.X -= 1;
        }
        if (state.IsKeyDown(Keys.W))
        {
            spritePosition.Y -= 1;
        }
        if (state.IsKeyDown(Keys.S))
        {
            spritePosition.Y += 1;
        }
        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin();
        
        var rectangle = new Rectangle(0, 0, 60, 35);
        
        _spriteBatch.Draw(spriteTexture, spritePosition, rectangle, Color.White);
        _spriteBatch.End();
        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
