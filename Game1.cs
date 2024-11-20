using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MonogameProject3_Spaceship
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D shipSprite;
        Texture2D asteroidSprite;
        Texture2D spaceSprite;
        SpriteFont gameFont;
        SpriteFont timerFont;

        List<Asteroid> asteroids = new List<Asteroid>();
        Random random = new Random();
        int score =0;
        bool gameWon = false;   

       
        
        Ship player = new Ship();
        Asteroid ast1 = new Asteroid(4);

        // Timer variables
        private TimeSpan elapsedTime;
        private int secondsElapsed;

        //Controller object
        Controller controller = new Controller();

        //Game State
        bool inGame = true;
          
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();

            //timer related
            elapsedTime = TimeSpan.Zero;
            secondsElapsed = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            shipSprite = Content.Load<Texture2D>("ship");
            asteroidSprite = Content.Load<Texture2D>("asteroid");
            spaceSprite = Content.Load<Texture2D>("space");
            gameFont = Content.Load<SpriteFont>("spaceFont");
            timerFont = Content.Load<SpriteFont>("timerFont");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //1// Auto Move the player along X-axis
            //player.position.X++;

            //2// Auto Move the player along Y-axis
            //player.position.Y++;

            //3// Move the player along X-axis and Y-axis using Keyboard   
            player.setRadius(shipSprite.Width);
            player.updateShip();
            ast1.updateAsteroid();

            // Get updated seconds count from Controller
            secondsElapsed = controller.updateTime(gameTime);

            // Check for collision
            if (controller.didCollisionHappen(player, ast1))
            {
                inGame = false;
            }

            
            base.Update(gameTime);

           
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(spaceSprite, new Vector2(0, 0), Color.White);
            //_spriteBatch.Draw(shipSprite, player.position, Color.White);//without centering the sprite
            _spriteBatch.Draw(shipSprite, new Vector2(player.position.X-shipSprite.Width/2, player.position.Y-shipSprite.Height/2), Color.White);//Using Offset and With Centering the sprite
            _spriteBatch.Draw(asteroidSprite, new Vector2(ast1.position.X-Asteroid.radius , ast1.position.Y-Asteroid.radius), Color.White);

            // Displaying Timer
            _spriteBatch.DrawString(timerFont, "Time: " + secondsElapsed, new Vector2(_graphics.PreferredBackBufferWidth / 2, 30), Color.White);

            // Displaying Game Over Message
            if (!inGame)
            {
                _spriteBatch.DrawString(gameFont, controller.gameEndScript(), new Vector2(_graphics.PreferredBackBufferWidth / 2 - 100, _graphics.PreferredBackBufferHeight / 2), Color.White);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
