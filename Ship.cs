using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonogameProject3_Spaceship
{
    internal class Ship
    {
        public Vector2 position = new Vector2(100, 100);
        public int speed = 10;
        int radius = 25;
        bool newSpeed = false;

        public void setRadius(int radius)
        {
            this.radius = radius;
        }
        public int getRadius() { 
            return this.radius;
        } 
        public void updateShip() {
            //3// Move the player along X-axis using Keyboard   
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Left) && state.IsKeyDown(Keys.Enter))
            {
                newSpeed = true;
                speed = 15;
            }
            else if (state.IsKeyUp(Keys.Enter))
            {
                newSpeed = false;
                speed = 10;
            }
            if (state.IsKeyDown(Keys.Left))
            {
                this.position.X -= speed;
            }
            if (state.IsKeyDown(Keys.Right) && state.IsKeyDown(Keys.Enter))
            {
                newSpeed = true;
                speed = 15;
            }
            else if (state.IsKeyUp(Keys.Enter))
            {
                newSpeed = false;
                speed = 10;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                this.position.X += speed;
            }
            //4// Move the player along X-axis using Keyboard  
            if (state.IsKeyDown(Keys.Up) && state.IsKeyDown(Keys.Enter))
            {
                newSpeed = true;
                speed =15;
            }
            else if (state.IsKeyUp(Keys.Enter))
            {
                newSpeed = false;
                speed = 10;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                this.position.Y -=speed ;
            }
            if (state.IsKeyDown(Keys.Down) && state.IsKeyDown(Keys.Enter))
            {
                newSpeed = true;
                speed = 15;
            }
            else if (state.IsKeyUp(Keys.Enter))
            {
                newSpeed = false;
                speed = 10;
            }
            if (state.IsKeyDown(Keys.Down))
            {
                this.position.Y +=speed ;
            }
        }
    }
}
