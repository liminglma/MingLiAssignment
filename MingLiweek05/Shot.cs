using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MingLiweek05
{
    class Shot: BasicModel
    {
        ModelManager modelmanager;
        Vector3 shotPosition;
        Vector3 shotDirection;
        Matrix Shotscale;
        float shotSpeed;
        int timeSincelastFrame = 0;
        int millionsecondperFrame = 16;

        //Tank tank;
        public Shot(Model model, Vector3 Position, Vector3 Direction, float Speed, ModelManager modelManager)
        : base(model)
        {
            this.modelmanager = modelManager;
            shotPosition = Position;
            shotDirection = Direction;
            shotSpeed = Speed;
            world = Matrix.CreateTranslation(Position);


        }
        public override void Update(GameTime gameTime)
        {
            timeSincelastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSincelastFrame > millionsecondperFrame)
            {
                timeSincelastFrame -= millionsecondperFrame;
            }
            shotPosition += shotDirection * shotSpeed * timeSincelastFrame;
            Shotscale = Matrix.CreateScale(2F);
            world = Shotscale * Matrix.CreateTranslation(shotPosition);

            base.Update(gameTime);
        }
        public override void Draw(GraphicsDevice device, camera camera)
        {

            device.SamplerStates[0] = SamplerState.LinearWrap;
            
            base.Draw(device, camera);
        }


    }
}
