using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankTroubleEswatinskeKvality.Content;

public class Bullet
{
    public Vector2 Position;
    private Vector2 _direction;
    private float _speed;

    public Bullet(Vector2 startPosition, Vector2 targetPosition, float speed)
    {
        Position = startPosition;
        _speed = speed;
        _direction = targetPosition - startPosition;
        if (_direction != Vector2.Zero)
            _direction.Normalize();
    }

    public void Update(GameTime gameTime)
    {
        Position += _direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D bulletTexture)
    {
        var bulletRectangle = new Rectangle((int)Position.X, (int)Position.Y, 15, 15);
        spriteBatch.Draw(bulletTexture, bulletRectangle, Color.White);
    }
}