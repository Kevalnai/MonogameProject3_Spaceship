using Microsoft.Xna.Framework;


namespace MonogameProject3_Spaceship
{
    internal class Asteroid
    {
        public Vector2 position = new Vector2(1300, 300);
        public int speed;
        static public int radius = 30;

        public Asteroid(int speed, Vector2 position)
        {
            this.speed = speed;
            this.position = position;
        }



        public void updateAsteroid()
        {
            this.position.X -= this.speed;
        }
    }
}
