using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MingLiweek05
{
    public class camera:Microsoft.Xna.Framework.GameComponent
    {

        Vector3 cameraPosition;
        Vector3 target;
        public Matrix view { get; protected set; }
        public Matrix projection { get; protected set; }
        public camera (Game game, Vector3 pos, Vector3 target, Vector3 up)
            : base (game)
        {
         
            view = Matrix.CreateLookAt(pos, target, up);
            projection = Matrix.CreatePerspectiveFieldOfView(
             MathHelper.PiOver2, (float)Game.Window.ClientBounds.Width / (float)Game.Window.ClientBounds.Height, 1, 6000);
            
        }
        //camera vectors




        public override void Initialize()
        {
            
            base.Initialize();


        }


        public override void Update(GameTime gameTime)
        {


            cameraPosition = ((Game1)Game).GetTankPosition() + new Vector3(0, 50, 50);
            target = ((Game1)Game).GetTankPosition()+(new Vector3(0,0,-50));

            view = Matrix.CreateLookAt(cameraPosition, target, Vector3.Up);
          

            base.Update(gameTime);
  
        }
       

      

    }
}
