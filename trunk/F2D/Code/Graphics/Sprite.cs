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
using F2D.Code.Graphics;
using F2D.Management;
using F2D;
using F2D.StateManager;

namespace F2D.Code.Graphics
{
    /// <summary>
    /// Sprites are represented by a 2D image, are not animated, and contain physics.
    /// </summary>
    public class Sprite : F2D.Code.Graphics.WorldItem

    {
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;                
                physicsBody.Position = value;                

                CurCell = F2D.Management.Grid.GetCell(position, this);
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

        private Vector2 size;
        public Vector2 Size
        {
            get { return size ; }
            set { size = value; }
        }


        //Drawing vars
        Texture2D texture;
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
            LoadXML(name);
            InitPhysics();
            CurCell = new F2D.Math.Vector2Int();
            ScreenManager.WorldItems.Add(this);
            CurCell = F2D.Management.Grid.GetCell(position, this);

            Layer = 0.5f;
        }

        public void Initialize(string name, Vector2 position)
        {
            this.name = name;
        
            this.position = position;
            this.rotation = 0f;
            LoadXML(name);
            InitPhysics();
            CurCell = new F2D.Math.Vector2Int();
            ScreenManager.WorldItems.Add(this);
            CurCell = F2D.Management.Grid.GetCell(position, this);

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
                //Xsize
                if (curNode < maxNode)
                {
                    size = new Vector2(Convert.ToSingle(node.ChildNodes.Item(curNode).InnerText), 1f);
                    curNode++;
                }
                //Ysize
                if (curNode < maxNode)
                {
                    size = new Vector2(size.X, Convert.ToSingle(node.ChildNodes.Item(curNode).InnerText));
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
                this.physicsBody = BodyFactory.Instance.CreateCircleBody(size.X, mass);
                this.physicsGeometry = GeomFactory.Instance.CreateCircleGeom(F2D.Core.Farseer.Physics, physicsBody, (int)size.X, 16);
            }
            else //gonna be a quad
            {
                this.physicsBody = BodyFactory.Instance.CreateRectangleBody(size.X, this.size.Y, this.mass);
                this.physicsGeometry = GeomFactory.Instance.CreateRectangleGeom(F2D.Core.Farseer.Physics, physicsBody, size.X, size.Y);
            }

            physicsBody.IsStatic = isStatic;
            physicsBody.Position = position;
            physicsBody.Rotation = rotation;

            this.physicsGeometry.RestitutionCoefficient = (2f / mass);
            this.physicsGeometry.FrictionCoefficient = 0.2f;

            F2D.Core.Farseer.Physics.Add(physicsBody);

        }

        public void LoadContent(ContentManager contentManager, string filename)
        {
            content = contentManager;
            texture = content.Load<Texture2D>(filename);
        }

        public void UnloadContent()
        {
            F2D.Management.Grid.Cells[this.CurCell.X, this.CurCell.Y].Objects.Remove(this);
            F2D.StateManager.ScreenManager.WorldItems.Remove(this);
            content.Unload();
        }

        public override void Draw()
        {
            position = physicsBody.Position;
            rotation = physicsBody.Rotation;

            Vector2 posBuffer = position - Camera.Position;


            ScreenManager.SceneBatch.Draw(texture, posBuffer, null,
                Color.White, rotation, new Vector2(size.X /2, size.Y/2), Vector2.One, 
                SpriteEffects.None, Layer);
        }

        #endregion

    }
}
