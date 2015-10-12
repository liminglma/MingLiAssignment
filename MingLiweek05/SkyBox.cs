using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MingLiweek05
{
    class SkyBox : BasicModel
    {
        ModelManager modelmanager;
        private Vector3 position = Vector3.Zero;
        Matrix scale = Matrix.Identity;
        public SkyBox(Model model, ModelManager modelManager)
        : base(model)
        {
            this.modelmanager = modelManager;
         }


        public override void Update(GameTime gameTime)
        {
            scale = Matrix.CreateScale(2000f);

            base.Update(gameTime);



        }
        public override void Draw(GraphicsDevice device, camera camera)
        {
            position = modelmanager.GettankPosition();
            position.Y = 0;
            world =scale* Matrix.CreateTranslation(position);
            device.SamplerStates[0] = SamplerState.LinearWrap;
            base.Draw(device, camera);
        }

     
     

    }   

}
