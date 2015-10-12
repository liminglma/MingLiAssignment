using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MingLiweek05
{
    public class MousePick
    {
        GraphicsDevice device;
        camera camera;
        public MousePick(GraphicsDevice device, camera camera)
        {
            this.device = device;
            this.camera = camera;
        }
        public Vector3? GetCollisionPosition()
        {
            MouseState mouseState = Mouse.GetState();

            Vector3 nearSource = new Vector3(mouseState.X, mouseState.Y, 0f);
            Vector3 farSource = new Vector3(mouseState.X, mouseState.Y, 1f);

            Vector3 nearPoint = device.Viewport.Unproject(
                nearSource,
                camera.projection,
                camera.view,
                Matrix.Identity);

            Vector3 farPoint = device.Viewport.Unproject(
                farSource,
                camera.projection,
                camera.view,
                Matrix.Identity);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            Ray pickRay = new Ray(nearPoint, direction);

            Nullable<float> result = pickRay.Intersects(new Plane(Vector3.Up, 0f));

            Vector3? resultVector = direction * result;
            Vector3? collisionPoint = resultVector + nearPoint;

            return collisionPoint;
        }

    }
}
