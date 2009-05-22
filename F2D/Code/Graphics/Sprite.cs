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

        public Vector2Int CurrentFrameSize
        {
            get { return new Vector2Int(
                (int)(Animations[currentAnimation].CurrentFrame.Image.Width * Scale.X),
                (int)(Animations[currentAnimation].CurrentFrame.Image.Height * Scale.Y));
            }
        }

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
                anim.Speed = Convert.ToInt32(animNode.Attributes[1].Value);

                // Loop through frames in this animation
                foreach (XmlNode frameNode in animNode.ChildNodes)
                {
                    Frame frame = new Frame();
                    
                    // Retrieve the values and split them into arrays of their values
                    string[] imageVal = frameNode.Attributes.GetNamedItem("image").Value.Split(new char[]{','});
                    string[] hotspot = frameNode.Attributes.GetNamedItem("origin").Value.Split(new char[]{','});
                    string[] colRect = frameNode.Attributes.GetNamedItem("collision").Value.Split(new char[] { ',' });

                    frame.Collision = new Rectangle(
                        Convert.ToInt32(colRect[0]),
                        Convert.ToInt32(colRect[1]),
                        Convert.ToInt32(colRect[2]),
                        Convert.ToInt32(colRect[3]));

                    frame.Image = new Rectangle(
                        Convert.ToInt32(imageVal[0]),
                        Convert.ToInt32(imageVal[1]), 
                        Convert.ToInt32(imageVal[2]),
                        Convert.ToInt32(imageVal[3]));

                    frame.Origin = new Vector2Int(
                        Convert.ToInt32(hotspot[0]),
                        Convert.ToInt32(hotspot[1]));

                    //foreach (XmlNode colNode in frameNode.ChildNodes)
                    //{
                    //    string type = frameNode.Attributes.GetNamedItem("type").Value.ToString();

                    //    if (type.ToLower() == "rect")
                    //        frame.ObjectType = CollisionObjectType.Rect;
                    //    else
                    //        frame.ObjectType = CollisionObjectType.Circle;

                    //    string[] position = colNode.Attributes.GetNamedItem("position").Value.Split(new char[] { ',' });
                        
                    //    frame.Position = new Vector2((float)Convert.ToDouble(position[0]), (float)Convert.ToDouble(position[1]));

                    //    if (frame.ObjectType == CollisionObjectType.Rect)
                    //    {
                    //        string[] size = colNode.Attributes.GetNamedItem("size").Value.Split(new char[] { ',' });
                    //        frame.Size = new Vector2Int(Convert.ToInt32(size[0]), Convert.ToInt32(size[1]));
                    //    }
                    //    else // Circle
                    //    {
                    //        string radius = colNode.Attributes.GetNamedItem("size").Value.ToString();
                    //        frame.Radius = Convert.ToInt32(radius);
                    //    }
                    //}

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