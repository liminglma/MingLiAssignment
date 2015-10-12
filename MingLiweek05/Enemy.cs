using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MingLiweek05
{
    class Enemy : BasicModel
    {

        ModelManager modelmanager;
        Matrix scale = Matrix.Identity;
        Vector3 position;
        Vector3 direction;
        Matrix rotation = Matrix.Identity;
        float speed;
        int timeSincelastFrame = 0;
        int millionsecondperFrame = 16;
        //Tank tank;
        public int count = 0;
        public Enemy(Model model, Vector3 Position, float Speed, GraphicsDevice device, camera camera, ModelManager modelManager)
        : base(model)
        {
            this.modelmanager = modelManager;
            world =  Matrix.CreateTranslation(Position);
            position = Position;
            speed = Speed;
        

        }
        public override void Update(GameTime gameTime)
        {
            timeSincelastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSincelastFrame > millionsecondperFrame)
            {
                timeSincelastFrame -= millionsecondperFrame;
            }

            base.Update(gameTime);
        }
        public override void Draw(GraphicsDevice device, camera camera)
        {
            scale = Matrix.CreateScale(.03f);
            direction = modelmanager.GettankPosition() - position;
            direction.Normalize();
          
            position += direction * speed * timeSincelastFrame;
            rotation = Matrix.CreateRotationY((float)Math.Atan2(direction.X, direction.Z));
            world = scale * rotation * Matrix.CreateTranslation(position);



            
            device.SamplerStates[0] = SamplerState.LinearWrap;
            base.Draw(device, camera);
        }

    }
}
