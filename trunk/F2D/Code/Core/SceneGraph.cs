/* Frostwave 2D
 * (c) Snowfall Media 2008
 * Steven Jackson, Vedran Budimcic
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using F2D;
using F2D.Math;
using F2D.Graphics;
using F2D.Graphics.Gui;


namespace F2D.Core
{
    static public class SceneGraph
    {

        static private int maxNeighbours;

        /// <summary>
        /// The number of cells around the parent cell that will be rendered
        /// </summary>
        static public int MaxNeighbours
        {
            get { return maxNeighbours; }
        }

        static private Vector2Int parentCell;

        /// <summary>
        /// The cell where the neighbouring cells are centered around
        /// </summary>
        static public Vector2Int ParentCell
        {
            get { return parentCell; }
            set { parentCell = value; }
        }

        static private bool renderCells;

        static public bool RenderCells
        {
            get { return renderCells; }
            set { renderCells = value; }
        }

        static private Vector2Int sceneSize;

        /// <summary>
        /// The Vector2Int which stores the dimensions of the SceneGraph
        /// </summary>
        static public Vector2Int SceneSize
        {
            get { return sceneSize; }
        }

        static private int cellSize;
        static public int CellSize
        {
            get { return cellSize; }
        }	

        static private Cell[,] cells;
        static public Cell[,] Cells
        {
            get { return cells; }
        }

        static private List<Cell> drawnCells;

        static public List<Cell> DrawnCells
        {
            get { return drawnCells; }
        }

        static private List<Renderable> masterlist;

        /// <summary>
        /// This contains all of the screen items and world items.
        /// </summary>
        static public List<Renderable> Masterlist
        {
            get { return masterlist; }
        }

        static private List<WorldItem> masterlistWorldItems;

        /// <summary>
        /// This contains all of the world items, whether they are being rendered or not
        /// </summary>
        static public List<WorldItem> MasterlistWorldItems
        {
            get { return masterlistWorldItems; }
        }

        static private List<ScreenItem> screenItems;

        /// <summary>
        /// This conatins all of the screen items.
        /// </summary>
        static public List<ScreenItem> ScreenItems
        {
            get { return screenItems; }
        }

        static private List<WorldItem> toBeUpdated;

        /// <summary>
        /// Contains a list of all world items whose position has changed since last frame.
        /// </summary>
        static public List<WorldItem> ToBeUpdated
        {
            get { return toBeUpdated; }
        }	
        
        static private Vector2Int totalCells;

        /// <summary>
        /// The number of cells within the scene batch, organized into X and Y directions with a Vector2Int.
        /// </summary>
        static public Vector2Int TotalCells
        {
            get { return totalCells; }
        }

        /// <summary>
        /// Setup the Scene Graph.
        /// </summary>
        /// <param name="mapSize">Size of the entire grid of cells (size of the map).</param>
        /// <param name="cSize">Size of each cell (each cell is a square).</param>
        /// <param name="neighbours">The number of cells around the parent cell that will be rendered.</param>
        static public void Initialize(Vector2Int mapSize, int cSize, int neighbours)
        {            
            sceneSize = mapSize;
            cellSize = cSize;

            renderCells = false;
            parentCell = new Vector2Int();
            masterlist = new List<Renderable>();
            masterlistWorldItems = new List<WorldItem>();
            screenItems = new List<ScreenItem>();
            toBeUpdated = new List<WorldItem>();
            drawnCells = new List<Cell>();

            totalCells = new Vector2Int((int)System.Math.Ceiling((float)sceneSize.X / (float)cellSize), 
                                        (int)System.Math.Ceiling((float)sceneSize.Y / (float)cellSize));

            cells = new Cell[totalCells.X + 1, totalCells.Y + 1];

            for (int x = 0; x <= totalCells.X; x++)
            {
                for (int y = 0; y <= totalCells.Y; y++)
                {
                    cells[x, y] = new Cell(new Vector2(x * cellSize, y * CellSize), cellSize);
                }
            }
        }

        static public void LoadContent(ContentManager content, string cellFilename)
        {
            for (int x = 0; x <= totalCells.X; x++)
            {
                for (int y = 0; y <= totalCells.Y; y++)
                {
                    cells[x, y].LoadContent(content, cellFilename);
                }
            }
        }

        /// <summary>
        /// Add a world item to be drawn by the Scene Graph
        /// </summary>
        static public void Add(WorldItem worldItem)
        {
            Masterlist.Add(worldItem);
            MasterlistWorldItems.Add(worldItem);
            ToBeUpdated.Add(worldItem);
        }

        /// <summary>
        /// Add a screen item to be drawn by the Scene Graph
        /// </summary>
        static public void Add(ScreenItem screenItem)
        {
            Masterlist.Add(screenItem);
            ScreenItems.Add(screenItem);
        }

        /// <summary>
        /// Call Cell.Draw() for all of the cells that are to be rendered
        /// </summary>
        /// <param name="batch"></param>
        static public void Draw(SpriteBatch batch)
        {
            cells[parentCell.X, parentCell.Y].Draw(batch);          

            //check if the neighbouring cells are within the bounds of the camera
            //and if so, draw them
            for (int i = 0; i < drawnCells.Count; i++)
            {
                if (drawnCells[i].Position.X + drawnCells[i].Size > Camera.Position.X &&
                    drawnCells[i].Position.X < Camera.Position.X + Camera.Size.X &&
                    drawnCells[i].Position.Y + drawnCells[i].Size > Camera.Position.Y &&
                    drawnCells[i].Position.Y < Camera.Position.Y + Camera.Size.Y)
                {
                    drawnCells[i].Draw(batch);
                }
            }
        }

        /// <summary>
        /// Updates the Scene Graph's spatial partitioning
        /// </summary>
        static public void Update()
        {            
            drawnCells.Clear();

            for (int i = 0; i < toBeUpdated.Count; i++)
            {
                GetCell(toBeUpdated[i].Position);
            }

            for (int layer = 1; layer < maxNeighbours + 1; layer++)
            {
                //x , y = center cell location  (parent cell)  
                int x = parentCell.X, y = parentCell.Y;

                //start at the top left cell, draw to top right cell
                for (int distance = -layer; distance < layer; distance++)
                {
                    //top bound / right bound / left bound
                    if (y - layer >= 0 && x + distance <= totalCells.X && x + distance >= 0)
                    {
                        drawnCells.Add(cells[x + distance, y - layer]);
                    }
                }

                //start at top right cell, go to bottom right cell
                for (int distance = -layer; distance < layer; distance++)
                {
                    //right bound / bottom bound / top bound
                    if (x + layer <= totalCells.X && y + distance <= totalCells.Y && y + distance >= 0)
                    {
                        drawnCells.Add(cells[x + layer, y + distance]);
                    }
                }

                //Start at bottom right cell, go to bottom left cell
                for (int distance = layer; distance > -layer; distance--)
                {
                    //Right bound / bottom bound / left bound
                    if (x + distance <= totalCells.X && y + layer <= totalCells.Y && x + distance >= 0)
                    {
                        //Grid.Cells[(int)x + distance, (int)y + layer].Draw();
                        drawnCells.Add(cells[(int)x + distance, (int)y + layer]);
                    }
                }

                //Start at bottom left, go to top left
                for (int distance = layer; distance > -layer; distance--)
                {
                    //Left bound, top bound, bottom bound
                    if (x - layer >= 0 && y + distance >= 0 && y + distance <= totalCells.Y)
                    {
                        //Grid.Cells[(int)x - layer, (int)y + distance].Draw();
                        drawnCells.Add(cells[(int)x - layer, (int)y + distance]);
                    }
                }
            }
        }
        
        static private Vector2Int GetCell(Vector2 position)
        {
            for (int x = 0; x <= totalCells.X; x++)
                {
                    for (int y = 0; y <= totalCells.Y; y++)
                    {
                        if (position.X >= Cells[x, y].Position.X &&
                            position.X <= (Cells[x, y].Position.X + Cells[x, y].Size) &&
                            position.Y >= Cells[x, y].Position.Y &&
                            position.Y <= (Cells[x, y].Position.Y + Cells[x, y].Size))
                        {
                            return new Vector2Int(x, y);
                        }

                    }
                }

                return new Vector2Int(0, 0);
        }

    }
}
