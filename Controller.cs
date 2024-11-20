﻿using Microsoft.Xna.Framework;
using System;

namespace MonogameProject3_Spaceship
{
    internal class Controller
    {
        private TimeSpan elapsedTime;
        private int secondsElapsed;

        public Controller()
        {
            elapsedTime = TimeSpan.Zero;
            secondsElapsed = 0;
        }

        // Update the timer and return the seconds elapsed
        public int updateTime(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime.TotalSeconds >= 1)
            {
                secondsElapsed++;
                elapsedTime = TimeSpan.Zero;
            }

            return secondsElapsed; // Return the current seconds count
        }

        public bool didCollisionHappen(Ship player, Asteroid ast)
        {
            int playerRadius = player.getRadius();
            int astRadius = Asteroid.radius;
            int distance = playerRadius + astRadius;
            if (Vector2.Distance(player.position, ast.position) < distance)
            {
                return true;
            }
            return false;
        }


        public String gameEndScript()
        {
            String gameEndMessage = "Congratulation You Fhinished Game!";
            return gameEndMessage;
        }
        public String gameEnd()
        {
            String gameEnd = "Spaceship hit by asteroid, " + Game1.surpassed + "Surpassed";
            return gameEnd;
        }
    }
}
