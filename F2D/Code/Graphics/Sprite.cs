/* Frostwave 2D
 * (c) Snowfall Media 2009
 * Steven Jackson, Vedran Budimcic
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using F2D.Math;
using F2D.Core;
using System.Xml;
using System.Collections.Generic;
using System;
using System.Collections;

namespace F2D.Graphics
{
    /// <summary>
    ///  Represents the animation of a sprite.
    /// </summary>
    public class Sprite : WorldItem
    {
        #region Properties

        private float timer;

        /// <summary>
        /// List of animations for this sprite.
        /// </summary>
        public Dictionary<string, SpriteAnimation> Animations;

        private string textureFilename;
        private Texture2D texture;

        private string currentAnimation;
        public string CurrentAnimation
        {
            get { return currentAnimation; }
            set
            {
                if (Animations.ContainsKey(value) && currentAnimation != value)
                {
                    Animations[value].CurrentFrameNumber = 0;
                    currentAnimation = value;
                }
            }
        }

        public bool AnimationActive;

        private float framesPerSecond;

        /// <summary>
        /// The speed of the animation.
        /// </summary>
        public int FramesPerSecond
        {
            get { return (int)(1f / framesPerSecond); }
            set { framesPerSecond = 1f / (float)value; }
        }

        public bool FlipVertical;
        public bool FlipHorizontal;

        public Vector2 Scale;

        #endregion

        public Sprite(string filename)
        {
            Scale = Vector2.One;
            FlipHorizontal = FlipVertical = false;
            framesPerSecond = 2f;
            timer = 0f;
            currentAnimation = "";
            AnimationActive = false;
            Animations = new Dictionary<string, SpriteAnimation>();
            LoadAnimations(filename);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(textureFilename);
        }

        public void UnloadContent()
        {

        }

        /// <summary>
        /// Load animation data from an XML file.
        /// </summary>
        /// <param name="filename"></param>
        public void LoadAnimations(string filename)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);
            
            // Get filename for sprite sheet texture
            textureFilename = xmlDoc.GetElementsByTagName("sprite").Item(0).Attributes.GetNamedItem("file").Value;
            
            XmlNodeList anims = xmlDoc.GetElementsByTagName("animation");

            // Loop through each animation element
            foreach (XmlNode animNode in anims)
            {
                SpriteAnimation anim = new SpriteAnimation();
                anim.Name = animNode.Attributes[0].Value;

                // Loop through frames in this animation
                foreach (XmlNode frameNode in animNode.ChildNodes)
                {
                    Frame frame = new Frame();
                    
                    // Retrieve the values and split them into arrays of their values
                    string[] imageVal = frameNode.Attributes.GetNamedItem("image").Value.Split(new char[]{','});
                    string[] collisionVal = frameNode.Attributes.GetNamedItem("collision").Value.Split(new char[]{','});
                    string[] hotspot = frameNode.Attributes.GetNamedItem("origin").Value.Split(new char[]{','});

                    frame.Image = new Rectangle(
                        Convert.ToInt32(imageVal[0]),
                        Convert.ToInt32(imageVal[1]), 
                        Convert.ToInt32(imageVal[2]),
                        Convert.ToInt32(imageVal[3]));

                    frame.Collision = new Rectangle(
                        Convert.ToInt32(collisionVal[0]),
                        Convert.ToInt32(collisionVal[1]),
                        Convert.ToInt32(collisionVal[2]),
                        Convert.ToInt32(collisionVal[3]));

                    frame.Origin = new Vector2Int(
                        Convert.ToInt32(hotspot[0]),
                        Convert.ToInt32(hotspot[1]));

                    // Add the frame to this animation.
                    anim.Frames.Add(frame);
                }

                // Add this animation to this sprite.
                Animations.Add(anim.Name, anim);

                Dictionary<string, SpriteAnimation>.KeyCollection.Enumerator dicEnum =
                    Animations.Keys.GetEnumerator();

                dicEnum.MoveNext();
                CurrentAnimation = dicEnum.Current;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (currentAnimation == "")
            {
                Dictionary<string, SpriteAnimation>.KeyCollection.Enumerator dicEnum = 
                    Animations.Keys.GetEnumerator();

                dicEnum.MoveNext();
                CurrentAnimation = dicEnum.Current;
            }

            if (AnimationActive)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (timer > framesPerSecond)
                {
                    timer = 0f;
                    
                    int curFrame = Animations[currentAnimation].CurrentFrameNumber;

                    Animations[currentAnimation].CurrentFrameNumber = 
                        (curFrame + 1) % Animations[currentAnimation].Frames.Count;
                }
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(texture,
                Position,
                Animations[currentAnimation].CurrentFrame.Image,
                Color.White,
                0,
                Animations[currentAnimation].CurrentFrame.Origin.ToVector2(),
                Scale,
                FlipHorizontal ? SpriteEffects.FlipHorizontally : (FlipVertical ? SpriteEffects.FlipVertically : SpriteEffects.None),
                Layer);

        }
    }
}