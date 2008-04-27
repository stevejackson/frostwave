using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using FarseerGames;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using F2D.Core;
using F2D;
using F2D.Graphics;
using F2D.Math;

namespace F2D.Graphics
{
    /// <summary>
    /// Sprites are represented by a 2D image, are not animated, and contain physics.
    /// </summary>
    public class Sprite : F2D.Graphics.WorldItem
    {
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;                
                physicsBody.Position = value;                

                CurCell = F2D.Core.Grid.GetCell(position, this);
            }
        }

        private float rotation;
        public float Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
                physicsBody.Rotation = value;                
            }
        }

        public Vector2Int Size;  
        public Vector2 Origin;

        public Vector2 Scale;


        //Drawing vars
        public Texture2D texture;
        string name;

        //physics
        public Body physicsBody;
        bool isStatic;
        string shape;
        float mass;

        ContentManager content;


        #region Main Methods ( Initialize, LoadContent, Draw, Update)

        public void Initialize(string name, Vector2 position, float rotation)
        {
            this.name = name;
        
            this.position = position;
            this.rotation = rotation;
            Origin = Vector2.Zero;
            LoadXML(name);
            CurCell = new F2D.Math.Vector2Int();
            Director.WorldItems.Add(this);
            CurCell = F2D.Core.Grid.GetCell(position, this);
            Scale = Vector2.One;
            Layer = 0.5f;
        }

        public void Initialize(string name, Vector2 position)
        {
            this.name = name;
            Origin = Vector2.Zero;
            this.position = position;
            this.rotation = 0f;
            LoadXML(name);
            CurCell = new F2D.Math.Vector2Int();
            Director.WorldItems.Add(this);
            CurCell = F2D.Core.Grid.GetCell(position, this);
            Scale = Vector2.One;
            Layer = 0.5f;
        }

        public void Initialize(Vector2 position, string shape, float mass)
        {
            this.name = "";
            Origin = Vector2.Zero;
            this.position = position;
            this.rotation = 0f;
            this.mass = mass;
            this.shape = shape;
            this.isStatic = false;
            CurCell = new F2D.Math.Vector2Int();
            Director.WorldItems.Add(this);
            CurCell = F2D.Core.Grid.GetCell(position, this);
            Scale = Vector2.One;
            Layer = 0.5f;
        }

        private void LoadXML(string name)
        {
            int curNode = 0;
            int maxNode = 0;

            XmlDocument doc = new XmlDocument();
            doc.Load(@"Content\XML\sprites.xml");

            //the nodelist holds all the nodes within the directory
            XmlNodeList nodeList;

            string directory = "descendant::" + name;
            nodeList = doc.SelectNodes(directory);

            //maxNode is one-based
            maxNode = nodeList.Item(0).ChildNodes.Count;

            foreach (XmlNode node in nodeList)
            {
                //first shape
                if (curNode < maxNode)
                {
                    shape = node.ChildNodes.Item(curNode).InnerText;
                    curNode++;
                }
                //rotation
                if (curNode < maxNode)
                {
                    rotation = Convert.ToSingle(node.ChildNodes.Item(curNode).InnerText);
                    curNode++;
                }
                //Static
                if (curNode < maxNode)
                {
                    isStatic = Convert.ToBoolean(node.ChildNodes.Item(curNode).InnerText);
                    curNode++;
                }
                //Mass
                if (curNode < maxNode)
                {
                    mass = Convert.ToSingle(node.ChildNodes.Item(curNode).InnerText);
                }
            }
        }

        private void InitPhysics()
        {
            if (shape == "Circle")
            {
                this.physicsBody = BodyFactory.Instance.CreateCircleBody(Size.X, mass);
                this.physicsGeometry = GeomFactory.Instance.CreateCircleGeom(F2D.Core.Farseer.Physics, physicsBody, (int)Size.X, 16);
            }
            else //gonna be a quad
            {
                this.physicsBody = BodyFactory.Instance.CreateRectangleBody(Size.X, this.Size.Y, this.mass);
                this.physicsGeometry = GeomFactory.Instance.CreateRectangleGeom(F2D.Core.Farseer.Physics, physicsBody, Size.X, Size.Y);
            }

            physicsBody.IsStatic = isStatic;
            physicsBody.Position = position;
            physicsBody.Rotation = rotation;

            this.physicsGeometry.RestitutionCoefficient = 0f;// (4f / mass);
            this.physicsGeometry.FrictionCoefficient = 0f;
            this.physicsBody.LinearDragCoefficient = 0f;
            this.physicsBody.RotationalDragCoefficient = 0f;

            F2D.Core.Farseer.Physics.Add(physicsBody);
        }

        public void LoadContent(ContentManager contentManager, string filename)
        {
            content = contentManager;
            texture = content.Load<Texture2D>(filename);
            if (this.Size == null)
            {
                this.Size = new Vector2Int(texture.Width, texture.Height);
            }
            InitPhysics();
        }

        public void UnloadContent()
        {
            F2D.Core.Grid.Cells[this.CurCell.X, this.CurCell.Y].Objects.Remove(this);
            F2D.Core.Director.WorldItems.Remove(this);
            content.Unload();
        }

        public override void Draw()
        {           
            position = physicsBody.Position;
            rotation = physicsBody.Rotation;
            
            Vector2 posBuffer = position - Camera.Position;

            Director.SceneBatch.Draw(texture, posBuffer, null,
                Color.White, rotation, Origin, Scale, 
                SpriteEffects.None, Layer);
        }

        #endregion

    }
}
