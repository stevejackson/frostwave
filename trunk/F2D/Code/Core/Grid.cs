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
        
        static private Rectangle mapRect;
        static public Rectangle MapRect
        {
            get { return mapRect; }
            set { mapRect = value; }
        }

        static private Cell[,] cells;
        static public Cell[,] Cells
        {
            get { return cells; }
        }

        static private Vector2Int parentCell;
        static public Vector2Int ParentCell
        {
            get { return parentCell; }
            set { parentCell = value; }
        }	
        
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


        static private int cellSize;
        static public int CellSize
        {
            get { return cellSize; }
            set { cellSize = value; }            
        }

        static private Vector2 cellPos;
        static public Vector2 CellPos
        {
            get { return cellPos; }
            set { cellPos = value; }
        }

        static private List<Cell> neighbourCells;
        static public List<Cell> NeighbourCells
        {
            get { return neighbourCells; }
            set { neighbourCells = value; }
        }

        static private ContentManager content;


        static public void Initialize(int sizeOfCell, Vector2 sizeOfMap, int neighbours)
        {
            cellSize = sizeOfCell;
            mapRect = new Rectangle(0, 0, (int)sizeOfMap.X, (int)sizeOfMap.Y);
            maxNeighbour = neighbours;

            totalXCells = (int)(mapRect.Right / cellSize);
            totalYCells = (int)(mapRect.Bottom / cellSize);

            cells = new Cell[totalXCells + 1, totalYCells + 1];
            neighbourCells = new List<Cell>();

            for (int x = 0; x <= totalXCells; x++)
            {
                for (int y = 0; y <= totalYCells; y++)
                {
                    cells[x, y] = new Cell();
                    cells[x, y].Initialize(new Vector2(x * cellSize, y * cellSize), cellSize);
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
                    cells[x, y].LoadContent(content, cellFilename);
                }
            }
        }

        static public void UnloadContent()
        {
            for (int x = 0; x <= totalXCells; x++)
            {
                for (int y = 0; y <= totalYCells; y++)
                {
                    cells[x, y].UnloadContent();
                }
            }
        }

        static public Vector2Int GetCell(Vector2 position, WorldItem worldItem)
        {
            cells[worldItem.CurCell.X, worldItem.CurCell.Y].Objects.Remove(worldItem);

            for (int x = 0; x <= totalXCells; x++)
            {
                for (int y = 0; y <= totalYCells; y++)
                {
                    if (position.X >= cells[x, y].Position.X &&
                        position.X <= (cells[x, y].Position.X + cells[x, y].Size) &&
                        position.Y >= cells[x, y].Position.Y &&
                        position.Y <= (cells[x, y].Position.Y + cells[x, y].Size))
                    {
                        cells[x, y].Objects.Add(worldItem);

                        return new Vector2Int(x, y);
                    }

                }
            }

            return new Vector2Int(0, 0);            
        }
    }
}
