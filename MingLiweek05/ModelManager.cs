using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace MingLiweek05
{
    class ModelManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        
        List<BasicModel> enemy= new List<BasicModel>();
        List<BasicModel> Player = new List<BasicModel>();
        List<BasicModel> enviroment = new List<BasicModel>();
        List<BasicModel> shots = new List<BasicModel>();
        List<BasicModel> wall = new List<BasicModel>();
        float shotMinz = -4000;
        Vector3 maxSpawnlocation = new Vector3(800, 0, -3000);
        int nextSpawnTime = 0;
        int timeSinceLastSpawn = 0;
        //enemy count
        int enemiesThisLevel = 0;
        // Misses variables
        int missedThisLevel;
        // Current Level
        int currentLevel = 0;
        int LifeCount = 5;
        int score = 0;

        SoundEffect soundFx;
        SoundEffectInstance soundEffect;

        //Tank tank;

        List<Level> Levellist = new List<Level>();

        private void SpawnWall()
        {

            for (int i = -1000; i < 1000; i += 15)
            {
                Vector3 position = new Vector3(i, 0, -1000);

                wall.Add(new Wall(
                    Game.Content.Load<Model>(@"Models/wall/BrickPack"),
                    position, ((Game1)Game).GraphicsDevice,
                ((Game1)Game).camera
                    ));

            }

            for (int i = -1000; i < 1000; i += 15)
            {
                Vector3 position = new Vector3(i, 0, 1000);

                wall.Add(new Wall(
                   Game.Content.Load<Model>(@"Models/wall/BrickPack"),
                   position, ((Game1)Game).GraphicsDevice,
               ((Game1)Game).camera
                   ));

            }

            for (int i = -1000; i < 1000; i += 15)
            {
                Vector3 position = new Vector3(1000, 0, i);

                wall.Add(new Wall(
                   Game.Content.Load<Model>(@"Models/wall/BrickPack"),
                   position, ((Game1)Game).GraphicsDevice,
               ((Game1)Game).camera
                   ));

            }
            for (int i = -1000; i < 1000; i += 15)
            {
                Vector3 position = new Vector3(-1000, 0, i);

                wall.Add(new Wall(
                   Game.Content.Load<Model>(@"Models/wall/BrickPack"),
                   position, ((Game1)Game).GraphicsDevice,
               ((Game1)Game).camera
                   ));

            }

        }

        private void SetNextSpawnTime()
        {
            nextSpawnTime = ((Game1)Game).rnd.Next(
                Levellist[currentLevel].minSpawnTime,
                Levellist[currentLevel].maxSpawnTime);
            timeSinceLastSpawn = 0;
        }
        

        private void SpawnEnemy()
        {
            // Generate random position with random X and random Y
            // between -maxX and maxX Z is always
            Vector3 position = new Vector3(((Game1)Game).rnd.Next
                (-(int)maxSpawnlocation.X, (int)maxSpawnlocation.X), 0, maxSpawnlocation.Z);
            // speed is a random value between minSpeed and maxSpeed
            float speed = Levellist[currentLevel].Speed;
            // scale of enemy
            enemy.Add(new Enemy(
                Game.Content.Load<Model>(@"Models/enemyTank/tank"),
                position, speed, ((Game1)Game).GraphicsDevice,
                ((Game1)Game).camera , this
                ));
            // Increment # of enemies this level and set next spawn time
            ++enemiesThisLevel;
            SetNextSpawnTime();

        }

        protected void CheckToSpawnEnemy(GameTime gameTime)
        {
            if (enemiesThisLevel<
                Levellist[currentLevel].numberEnemies)
            {
                timeSinceLastSpawn += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastSpawn > nextSpawnTime)
                {
                    SpawnEnemy();
                }
            }
        }
      
        public void AddShot(Vector3 position, Vector3 direction, float speed)
        {
            shots.Add(new Shot(
                Game.Content.Load<Model>(@"Models/shot/ammo"),
                position, direction, speed, this));

        }
        protected void UpdateShot(GameTime gameTime)
        {
            for (int i = 0; i < shots.Count; i++)
            {
                shots[i].Update(gameTime);
                if (shots[i].world.Translation.Z < shotMinz)
                {
                    shots.RemoveAt(i);
                    i--;
                }
            }
        }

        public ModelManager (Game game)
            : base (game)
        {

        }

         public override void Initialize()
        {
            Levellist.Add(new Level(1000, 3000, 10, 0.05f, 5));
            Levellist.Add(new Level(900, 2800, 15, 0.08f, 5));
            Levellist.Add(new Level(800, 2700, 20, 0.1f, 5));
            Levellist.Add(new Level(600, 2000, 30, 0.12f, 5));
            Levellist.Add(new Level(300, 1500, 50, 0.15f, 5));
            Levellist.Add(new Level(100, 600, 170, 0.3f,  5));

            // set initial spawn time

            SetNextSpawnTime();





            base.Initialize();
        }

        protected override void LoadContent()
        {
            //models.Add(new BasicModel(
            //Game.Content.Load<Model>(@"Models/Ground/Ground")));

            enviroment.Add(new Ground(
                Game.Content.Load<Model>(@"Models/ground/Ground")));
            

            enviroment.Add(new SkyBox(
                Game.Content.Load<Model>(@"Models/skybox/skybox"), this));
           
            Player.Add(new Tank(
                Game.Content.Load<Model>(@"Models/Tank/tank"),
                ((Game1)Game).GraphicsDevice,
                ((Game1)Game).camera
                ));
            SpawnWall();
            SpawnEnemy();
            

            base.LoadContent();

        }
        protected void UpdateEnemy(GameTime gameTime)
        {
            // Loop through all models and call Update
            for (int i = 0; i < enemy.Count; ++i)
            {
                // Update each model
                enemy[i].Update(gameTime);
                // Remove models that are out of bound
                if (enemy[i].GetCollision(Player[0].model, Player[0].world))
                {
                    enemy.RemoveAt(i);
                    LifeCount -= 1;
                    --i;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Update player

            // Update Others
            CheckToSpawnEnemy(gameTime);
            foreach (BasicModel model in enviroment)
            {
                model.Update(gameTime);
                
            }
            foreach (BasicModel model in Player)
            {
                model.Update(gameTime);

            }
            UpdateEnemy(gameTime);
            UpdateShot(gameTime);


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (BasicModel model in enviroment)
            {
                model.Draw(((Game1)Game).device, ((Game1)Game).camera);
            }
            foreach (BasicModel model in Player)
            {
                model.Draw(((Game1)Game).device, ((Game1)Game).camera);
            }
            foreach (BasicModel model in enemy)
            {
                model.Draw(((Game1)Game).device, ((Game1)Game).camera);
            }
            foreach (BasicModel model in shots)
            {
                model.Draw(((Game1)Game).device, ((Game1)Game).camera);
            }
            foreach (BasicModel model in wall)
            {
                model.Draw(((Game1)Game).device, ((Game1)Game).camera);
            }
            base.Draw(gameTime);
        }

        // Get Tank Position

        public Vector3 GettankPosition()
        {
            return Player[0].world.Translation;
        }

        public Vector3 GetTurretDirection()
        {
            return Player[0].GetTurretDirection();
        }




    }
}




