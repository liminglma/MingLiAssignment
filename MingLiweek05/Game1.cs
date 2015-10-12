using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace MingLiweek05
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        // public
        public GraphicsDevice device { get; protected set; }
        public camera camera { get; protected set; }
        // Random
        public Random rnd { get; protected set; }
        //shot
        float shotSpeed = 0.1f;
        int shotDelay = 300;
        int shotCountdown = 0;
        Vector3 shotDirection;

        //private
       
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SoundEffect soundFx;
        SoundEffectInstance music;
        SoundEffectInstance soundEffect;
        Texture2D crosshairTexture;
        MousePick mousePick;
        ModelManager ModelManager;
        //methods
        public Vector3 GetTankPosition()
        {
            return ModelManager.GettankPosition();
        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            rnd = new Random();
            mousePick = new MousePick(device, camera);
        }
        protected void FireShots(GameTime gameTime)
        {
            if (shotCountdown <= 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) ||
                    Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    ModelManager.AddShot((ModelManager.GettankPosition()+new Vector3(0,10,0)),
                        shotDirection, shotSpeed);
                    shotCountdown = shotDelay;
                }
            }
            else
                shotCountdown -= gameTime.ElapsedGameTime.Milliseconds;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new camera(this, new Vector3(0, 50, 200), new Vector3(0, 0, 0), Vector3.Up);
            Components.Add(camera);
          
            ModelManager = new ModelManager(this);
            Components.Add(ModelManager);
            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;

            soundFx = Content.Load<SoundEffect>(@"Audio/Wind");

            music = soundFx.CreateInstance();
            music.IsLooped = true;
            music.Volume = 1;
            music.Play();

            soundFx = Content.Load<SoundEffect>(@"Audio/tankMove");
            soundEffect = soundFx.CreateInstance();
            //draw crosshair
            crosshairTexture = Content.Load<Texture2D>(@"Textures/TankCrosshair");


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //shot logic
            shotDirection = ModelManager.GetTurretDirection();
            FireShots(gameTime);

            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            KeyboardState keyboardstate = Keyboard.GetState();
            
            if (Tank.speed!=0)
            {
                soundEffect.IsLooped = true;
                soundEffect.Play();
            }
            else
            {
                soundEffect.Stop();
            }
            //if((Tank).isTankMoving)
            //{

            //}
            base.Draw(gameTime);
            //spriteBatch.Begin();
            //spriteBatch.Draw(crosshairTexture,
            //    new Vector2((Window.ClientBounds.Width / 2)
            //    - (crosshairTexture.Width / 2),
            //    (Window.ClientBounds.Height / 2)
            //    - (crosshairTexture.Height / 2)),null,null,
            //                new Vector2((Window.ClientBounds.Width / 2)
            //    - (crosshairTexture.Width / 2),
            //    (Window.ClientBounds.Height / 2)
            //    - (crosshairTexture.Height / 2)),
            //0f,
            //new Vector2(0.5f,0.5f),
            //Color.White,
            //SpriteEffects.None,
            //0f);
            //spriteBatch.End();
            //GraphicsDevice.DepthStencilState = DepthStencilState.Default;


        }
    }
}
