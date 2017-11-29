using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Camera2D
    {
        private readonly Viewport _viewport;

        public Camera2D(Viewport viewport)
        {
            _viewport = viewport;

            Rotation = 0;
            Zoom = 1;
            Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            Position = Vector2.Zero;
        }

        public Vector2 ViewportCenter
        {
            get
            {
                return new Vector2(_viewport.Width * 0.5f, _viewport.Height * 0.5f);
            }
        }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }
        public Vector2 Origin { get; set; }
        public Matrix GetViewMatrix()
        {
            // Position = new Vector2(100, 200);
            Matrix m =
                Matrix.CreateTranslation(new Vector3(-Position, 0)) *//(-Position, 0.0f)) *
                                                                     // Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                 Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1);
            //Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));
            //    Matrix.CreateTranslation(new Vector3(Position, 0.0f));

            return m;
        }
    }
}
