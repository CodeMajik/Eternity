using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eternity
{
    public class Camera2d
    {
        private const float zoomUpperLimit = 3.0f;
        private const float zoomLowerLimit = .3f;

        private float _zoom;
        private Matrix _transform;
        private Vector2 _pos;
        private float _rotation;
        private int _viewportWidth;
        private int _viewportHeight;
        private int _worldWidth;
        private int _worldHeight;
        private float leftX, rightX, topY, botY;

        public Camera2d(Viewport viewport, int worldWidth,
           int worldHeight, float initialZoom, Vector2 pos)
        {
            _zoom = initialZoom;
            _rotation = 0.0f;
            _pos = pos;
            _viewportWidth = viewport.Width;
            _viewportHeight = viewport.Height;
            _worldWidth = worldWidth;
            _worldHeight = worldHeight;
        }

        public void InitBounds(float left, float right, float top, float bot)
        {
            leftX = left;
            rightX = right;
            topY = top;
            botY = bot;
        }

        #region Properties

        public float Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                if (_zoom < zoomLowerLimit)
                    _zoom = zoomLowerLimit;
                if (_zoom > zoomUpperLimit)
                    _zoom = zoomUpperLimit;
            }
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public void Move(Vector2 amount)
        {
            _pos += amount;
        }

        public Vector2 Pos
        {
            get { return _pos; }
            set
            {
                _pos = value;
                if (_pos.X < leftX)
                    _pos.X = leftX;
                if (_pos.X > rightX)
                    _pos.X = rightX;
                if (_pos.Y > topY)
                    _pos.Y = topY;
                if (_pos.Y < botY)
                    _pos.Y = botY;
            }
        }

        #endregion

        public Matrix GetTransformation()
        {
            _transform =
               Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
               Matrix.CreateRotationZ(Rotation) *
               Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
               Matrix.CreateTranslation(new Vector3(_viewportWidth * 0.5f,
                   _viewportHeight * 0.5f, 0));

            return _transform;
        }
    }
}
