using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace MingLiweek05
{
    class Tank : BasicModel
    {
        public Vector3 position;
        private Matrix rotation;
        private float newOrientation = MathHelper.Pi;
        private float TranslationOrientation = 0;
        private Vector3 TankDirection;
        static public float speed;
        private float Acceleration = 0.0005f;
        private float Resistance = 0.0005f;
        int timeSincelastFrame = 0;
        int millionsecondperFrame = 16;
        public Ray pickRay;
      
        // 炮台变量
        MousePick MousePick;
        public ModelBone turretBone;
        ModelBone rightFrontWheel;
        ModelBone leftFrontWheel;
        ModelBone rightRearWheel;
        ModelBone leftRearWheel;
        Vector3 turretDirection;
        float turretOrintation;
        Matrix turretRotation = Matrix.Identity;
        Matrix scale = Matrix.Identity;
        //SoundEffect soundFx;
        //SoundEffectInstance music;
        //SoundEffectInstance soundEffect;



        public Tank(Model model, GraphicsDevice device, camera camera)
        : base(model)
        {
            MousePick = new MousePick(device, camera);
            turretBone = model.Bones["turret_geo"];
            rightFrontWheel = model.Bones["r_front_wheel_geo"];
            leftFrontWheel = model.Bones["l_front_wheel_geo"];
            rightRearWheel = model.Bones["r_back_wheel_geo"];
            leftRearWheel = model.Bones["l_back_wheel_geo"];
  

        }


        public void rollingWheel(float speed)
        {
            rightFrontWheel.Transform = Matrix.CreateRotationX(speed / (1.69468266f/3)) * rightFrontWheel.Transform;
            leftFrontWheel.Transform = Matrix.CreateRotationX(speed / (1.69468266f/3)) * leftFrontWheel.Transform;
            rightRearWheel.Transform = Matrix.CreateRotationX(speed / (1.69468266f/6)) * rightRearWheel.Transform;
            leftRearWheel.Transform = Matrix.CreateRotationX(speed / (1.69468266f/6)) * leftRearWheel.Transform;
        }
        public override void Update(GameTime gameTime)
        {
            
            timeSincelastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSincelastFrame > millionsecondperFrame)
            {
                timeSincelastFrame -= millionsecondperFrame;
            }
            scale = Matrix.CreateScale(0.03f);
            // 坦克转动

            if (TranslationOrientation >= MathHelper.Pi + MathHelper.PiOver4)
            {
                TranslationOrientation = -MathHelper.PiOver2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && TranslationOrientation < MathHelper.Pi + MathHelper.PiOver2)
            {
                TranslationOrientation += (MathHelper.Pi / 300);

            }
            if (TranslationOrientation <= -MathHelper.Pi - MathHelper.PiOver2)
            {
                TranslationOrientation = MathHelper.PiOver2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && TranslationOrientation > -MathHelper.Pi - MathHelper.PiOver2)
            {
                TranslationOrientation -= (MathHelper.Pi / 300);

            }
            newOrientation = TranslationOrientation + MathHelper.Pi;
            rotation = Matrix.CreateRotationY(newOrientation);
            TankDirection.Y = 0;
            TankDirection.Z = 100;
            TankDirection.X = TankDirection.Z * (float)Math.Tan(newOrientation);
            TankDirection.Normalize();


            if (Keyboard.GetState().IsKeyDown(Keys.W) && speed < 0.3f)
            { speed += Acceleration; }

            if (Keyboard.GetState().IsKeyDown(Keys.S) && speed > 0.03f && speed < 0.3f)
            { speed -= Acceleration * 2; }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && speed > 0 && speed <= 0.03f)
            {
                speed = 0f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && speed <= 0f && speed > -0.1f)
            { speed -= Acceleration; }

            if (Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.S) && speed < -0.02f)
            {
                speed += Resistance;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.S) && speed > 0.02f)
            {
                speed -= Resistance;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.S) && speed > -0.02f && speed < 0.02f)
            {
                speed = 0f;
            }


                if (newOrientation > MathHelper.PiOver2 && newOrientation < MathHelper.Pi + MathHelper.PiOver2)
            { position -= TankDirection * speed * timeSincelastFrame; }
            else
            { position += TankDirection * speed * timeSincelastFrame; }
           
          
            // 炮台转动
            if (MousePick.GetCollisionPosition().HasValue == true && Mouse.GetState().RightButton != ButtonState.Pressed)
            {
                turretDirection = MousePick.GetCollisionPosition().Value - position;
                turretDirection.Normalize();
                turretOrintation = (float)Math.Atan2(turretDirection.X, turretDirection.Z) - newOrientation;
                pickRay = new Ray(position, turretDirection);

            }
            rollingWheel(speed);
            turretRotation = Matrix.CreateFromYawPitchRoll(turretOrintation, 0 ,0);
            turretBone.Transform = Matrix.CreateTranslation(0, 230, 0) * turretRotation;



            base.Update(gameTime);
        }
        public override void Draw(GraphicsDevice device, camera camera)
        {


            world = scale * rotation * Matrix.CreateTranslation(position);
            //soundEffect = ((Game1)Game).soundFx.CreateInstance();
            device.SamplerStates[0] = SamplerState.LinearWrap;
            base.Draw(device, camera);
        }

        //公共变量
        //public Vector3 GetTankPosition
        //{
        //    get{ return position; }
        //}
        //public Vector3 GetTurretDirection
        //{
        //    get { return turretDirection; }
        //}
        public bool isTankMoving
        {
            get
            {
                if(speed == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public override Vector3 GetTurretDirection()
        {
            return turretDirection;
        }





    }
}
