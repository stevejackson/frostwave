using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using F2D.Core;
using F2D.Graphics;
using F2D.Math;
using F2D;

namespace F2D.Core
{
    static public class Grid
    {
        static public Rectangle MapRect;
        static public Cell[,] Cells;
        static public Vector2Int ParentCell;
        
        static private int maxNeighbour;
        static public int MaxNeighbour
        {
            get { return maxNeighbour; }
        }

        static private int totalXCells;
        static public int TotalXCells
        {
            get { return totalXCells; }
        }


        static private int totalYCells;
        static public int TotalYCells
        {
            get { return totalYCells; }
        }

        static public int CellSize;
        static public Vector2 CellPos;

        static public List<Cell> NeighbourCells;

        static private ContentManager content;

        static public void Initialize(int sizeOfCell, Vector2 sizeOfMap, int neighbours)
        {
            CellSize = sizeOfCell;
            MapRect = new Rectangle(0, 0, (int)sizeOfMap.X, (int)sizeOfMap.Y);
            maxNeighbour = neighbours;

            totalXCells = (int)(MapRect.Right / CellSize);
            totalYCells = (int)(MapRect.Bottom / CellSize);

            Cells = new Cell[totalXCells + 1, totalYCells + 1];
            NeighbourCells = new List<Cell>();

            for (int x = 0; x <= totalXCells; x++)
            {
                for (int y = 0; y <= totalYCells; y++)
                {
                    Cells[x, y] = new Cell();
                    Cells[x, y].Initialize(new Vector2(x * CellSize, y * CellSize), CellSize);
                }
            }
        }

        static public void LoadContent(ContentManager contentManager, string cellFilename)
        {
            content = contentManager;

            for (int x = 0; x <= totalXCells; x++)
            {
                for (int y = 0; y <= totalYCells; y++)
                {
                    Cells[x, y].LoadContent(content, cellFilename);
                }
            }
        }

        static public void UnloadContent()
        {
            for (int x = 0; x <= totalXCells; x++)
            {
                for (int y = 0; y <= totalYCells; y++)
                {
                    Cells[x, y].UnloadContent();
                }
            }
        }

        static public Vector2Int GetCell(Vector2 position, WorldItem worldItem)
        {
            Cells[worldItem.CurCell.X, worldItem.CurCell.Y].Objects.Remove(worldItem);

            for (int x = 0; x <= totalXCells; x++)
            {
                for (int y = 0; y <= totalYCells; y++)
                {
                    if (position.X >= Cells[x, y].Position.X &&
                        position.X <= (Cells[x, y].Position.X + Cells[x, y].Size) &&
                        position.Y >= Cells[x, y].Position.Y &&
                        position.Y <= (Cells[x, y].Position.Y + Cells[x, y].Size))
                    {
                        Cells[x, y].Objects.Add(worldItem);

                        return new Vector2Int(x, y);
                    }

                }
            }

            return new Vector2Int(0, 0);            
        }
    }
}
