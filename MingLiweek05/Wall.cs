using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MingLiweek05
{
    class Wall : BasicModel
    {
        Vector3 position;
        public Wall(Model model, Vector3 Position, GraphicsDevice device, camera camera)
            : base(model)
        {
            world = Matrix.CreateTranslation(Position);
            position = Position;
        }

        public override void Draw(GraphicsDevice device, camera camera)
        {

            world = Matrix.CreateRotationX(MathHelper.Pi / -2) * Matrix.CreateTranslation(position);

            device.SamplerStates[0] = SamplerState.LinearWrap;
            base.Draw(device, camera);
        }
    }
}
