using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace MingLiweek05
{
    class BasicModel
    {
        public Model model { get; protected set; }

        public Matrix world = Matrix.Identity;
        public Matrix[] transforms;

        public BasicModel (Model model)
        {
            this.model =  model;

        }

    
        public virtual void Update(GameTime gameTime)
        {
            

        }
        public bool GetCollision (Model otherModel, Matrix otherWorld)
        {
            foreach (ModelMesh MyMesh in model.Meshes)
            {
                foreach (ModelMesh OtherMesh in otherModel.Meshes)
                {
                    if (MyMesh.BoundingSphere.Transform(
                        world).Intersects(
                        OtherMesh.BoundingSphere.Transform(otherWorld)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public virtual void Draw (GraphicsDevice device, camera camera)
        {
            transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = transforms[mesh.ParentBone.Index] * world;
                    effect.View = camera.view;
                    effect.Projection = camera.projection;
                    effect.TextureEnabled = true;
                    effect.Alpha = 1;
                    //effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }

        }

        public virtual Vector3 GetTurretDirection()
        {
            return Vector3.Zero;

        }
           
       





    }
}
