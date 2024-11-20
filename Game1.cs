﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        int score = 0;
        static public int surpassed = 0;
        bool gameWon = false;
        bool gameOver = false;
        bool timeOver = false;
        bool ShipCrashed = false;
        SoundEffect Start;

        // timer Vareables
        const int maxTime = 15;
        const int spawnInterval = 2;
        TimeSpan asteroidSpawnTime = TimeSpan.Zero;

        Ship player = new Ship();
        //Asteroid ast1 = new Asteroid(4);

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
            Start = Content.Load<SoundEffect>("start");
            // TODO: use this.Content to load your game content here
            shipSprite = Content.Load<Texture2D>("ship");
            asteroidSprite = Content.Load<Texture2D>("asteroid");
            spaceSprite = Content.Load<Texture2D>("space");
            gameFont = Content.Load<SpriteFont>("spaceFont");
            timerFont = Content.Load<SpriteFont>("timerFont");
            Start.Play();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))

                Exit();

            // TODO: Add your update logic here

            // Start sound
            
            // Updating the timer
            elapsedTime += gameTime.ElapsedGameTime;
            secondsElapsed = Math.Min(controller.updateTime(gameTime), maxTime);

            if (secondsElapsed >= maxTime)
            {
                gameWon = true;
                timeOver = true;
                inGame = false;

            }

            if (elapsedTime > asteroidSpawnTime + TimeSpan.FromSeconds(spawnInterval))
            {
                int speed = random.Next(1, 10);
                Vector2 position = new Vector2(_graphics.PreferredBackBufferWidth, random.Next(0, _graphics.PreferredBackBufferHeight));
                asteroids.Add(new Asteroid(speed, position));
                asteroidSpawnTime = elapsedTime;
            }

            // update each asteroid

            for (int i = asteroids.Count - 1; i >= 0; i--)
            {
                asteroids[i].updateAsteroid();
                if (asteroids[i].position.X < player.position.X - shipSprite.Width / 2)
                {
                    score++;
                    surpassed++;
                    asteroids.RemoveAt(i);

                }
                else if (controller.didCollisionHappen(player, asteroids[i]))
                {
                    {
                        score -= 3;
                        asteroids.RemoveAt(i);
                        if (score < 0)
                        {
                            ShipCrashed = true;
                            inGame = false; // this will end the game if the score reachs zero or negative
                        }

                    }
                }
            }





            //3// Move the player along X-axis and Y-axis using Keyboard   
            player.setRadius(shipSprite.Width);
            player.updateShip();


            // Get updated seconds count from Controller
            secondsElapsed = controller.updateTime(gameTime);
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(spaceSprite, new Vector2(0, 0), Color.White);
            //_spriteBatch.Draw(shipSprite, player.position, Color.White);//without centering the sprite
            _spriteBatch.Draw(shipSprite, new Vector2(player.position.X - shipSprite.Width / 2, player.position.Y - shipSprite.Height / 2), Color.White);//Using Offset and With Centering the sprite
            //_spriteBatch.Draw(asteroidSprite, new Vector2(ast1.position.X-Asteroid.radius , ast1.position.Y-Asteroid.radius), Color.White);

            foreach (var asteroid in asteroids)
            {
                _spriteBatch.Draw(asteroidSprite, asteroid.position, Color.White);
            }

            // Displaying Timer
            _spriteBatch.DrawString(timerFont, "Time: " + secondsElapsed, new Vector2(_graphics.PreferredBackBufferWidth / 2, 30), Color.White);
            _spriteBatch.DrawString(gameFont, $"Score: {score}", new Vector2(10, 10), Color.White);
            _spriteBatch.DrawString(timerFont, $"Time: {secondsElapsed}", new Vector2(10, 50), Color.White);

            // Displaying Game Over Message
            if (!inGame)
            {
                if (timeOver)
                {
                    _spriteBatch.DrawString(gameFont, controller.gameEndScript(), new Vector2(_graphics.PreferredBackBufferWidth / 2 - 100, _graphics.PreferredBackBufferHeight / 2), Color.White);
                }
                else
                {
                    _spriteBatch.DrawString(gameFont, controller.gameEnd(), new Vector2(_graphics.PreferredBackBufferWidth / 2 - 100, _graphics.PreferredBackBufferHeight / 2), Color.White);

                }


            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
