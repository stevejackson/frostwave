using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace F2D.Code.Management
{

    /// <summary>
    /// Enum that can represent a screen resolution
    /// </summary>
    
    public enum ScreenMode
    {
        /// <summary>
        /// 800x600
        /// </summary>
        REG1,
        /// <summary>
        /// 832x624
        /// </summary>
        REG2,
        /// <summary>
        /// 1024x768
        /// </summary>
        REG3,
        /// <summary>
        /// 1152x864
        /// </summary>
        REG4,
        /// <summary>
        /// 1280x960
        /// </summary>
        REG5,
        /// <summary>
        /// 1280x1024
        /// </summary>
        REG6,
        /// <summary>
        /// 1400x1050
        /// </summary>
        REG7,
        /// <summary>
        /// 1600x1200
        /// </summary>
        REG8,
        /// <summary>
        /// 1280x800
        /// </summary>
        WIDE1,
        /// <summary>
        /// 1440x900
        /// </summary>
        WIDE2,
        /// <summary>
        /// 1680x1050
        /// </summary>
        WIDE3,
        /// <summary>
        /// 1920x1200
        /// </summary>
        WIDE4
    }

    public class Resolution
    {
        private int screenWidth;
        private int screenHeight;
        private Matrix scale;
        private ScreenMode currentMode;
        private ScreenMode baseMode;
        private Vector2 viewportOrigin;
        private Vector2 viewportSize;
        public float letterboxing;

        public static readonly int[,] resolutions =
        new int[,] {
        // Normal
        {800, 600},
        {832, 624},
        {1024, 768},
        {1152, 864},
        {1280, 960},
        {1280, 1024},
        {1600, 1200},
        // Widescreen
        {1280, 800},
        {1440, 900},
        {1680, 1050},
        {1920, 1200}
        };


        public int ScreenWidth { get { return this.screenWidth; } }

        public int ScreenHeight { get { return this.screenHeight; } }

        public Vector2 ViewportOrigin { get { return this.viewportOrigin; } }

        public Vector2 ViewportSize { get { return this.viewportSize; } }

        public Matrix Scale { get { return this.scale; } }

        public ScreenMode BaseMode
        {
            get { return this.baseMode; }
            set { this.baseMode = value; }
        }

        public ScreenMode Mode
        {
            get { return currentMode; }
            set
            {
                currentMode = value;
                this.screenWidth = resolutions[(int)currentMode, 0];
                this.screenHeight = resolutions[(int)currentMode, 1];

                if (currentMode == ScreenMode.REG5)
                    letterboxing = (resolutions[(int)currentMode, 0] - (resolutions[(int)currentMode, 1] * 1.25f)) / 2;

                else
                    letterboxing = (resolutions[(int)currentMode, 0] - (resolutions[(int)currentMode, 1] * 1.33333f)) / 2;

                viewportOrigin = new Vector2(letterboxing, 0);
                viewportSize = new Vector2(resolutions[(int)currentMode, 0] - (letterboxing * 2), resolutions[(int)currentMode, 1]);

            }
        }

        public Resolution(GraphicsDeviceManager graphics, ScreenMode mode)
        {
            this.Mode = mode;
            this.baseMode = this.currentMode;
            SetResolution(graphics);
        }

        public void SetResolution(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferWidth = this.screenWidth;
            graphics.PreferredBackBufferHeight = this.screenHeight;

            scale = Matrix.CreateScale(
                    (float)viewportSize.X / (float)resolutions[(int)baseMode, 0],
                    (float)viewportSize.Y / (float)resolutions[(int)baseMode, 1],
                    1f);

            graphics.ApplyChanges();
        }
    }
}