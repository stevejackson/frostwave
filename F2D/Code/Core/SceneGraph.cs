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

        static private List<Cell> neighbourCells;

        static public List<Cell> NeighbourCells
        {
            get { return neighbourCells; }
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
            maxNeighbours = neighbours;

            renderCells = false;            
            parentCell = new Vector2Int();
            masterlist = new List<Renderable>();
            masterlistWorldItems = new List<WorldItem>();
            screenItems = new List<ScreenItem>();
            toBeUpdated = new List<WorldItem>();
            neighbourCells = new List<Cell>();

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

        static public void UnloadContent()
        {

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
            for (int j = 0; j < screenItems.Count; j++)
            {
                screenItems[j].Draw(batch);
            }

            //make sure the parent cell is actually on screen
            if(new Vector2Int(parentCell.X, parentCell.Y) != new Vector2Int(-1,-1))
                cells[parentCell.X, parentCell.Y].Draw(batch);


            //check if the neighbouring cells are within the bounds of the camera
            //and if so, draw them
            for (int i = 0; i < neighbourCells.Count; i++)
            {
                if (neighbourCells[i].Position.X + neighbourCells[i].Size > Camera.Position.X &&
                    neighbourCells[i].Position.X < Camera.Position.X + Camera.Size.X &&
                    neighbourCells[i].Position.Y + neighbourCells[i].Size > Camera.Position.Y &&
                    neighbourCells[i].Position.Y < Camera.Position.Y + Camera.Size.Y)
                {
                    neighbourCells[i].Draw(batch);
                }
            }
        }

        /// <summary>
        /// Updates the Scene Graph's spatial partitioning
        /// </summary>
        static public void Update()
        {            
            neighbourCells.Clear();

            for (int i = 0; i < toBeUpdated.Count; i++)
            {
                //if the object was off screen last frame, check if it is now on screen
                if (toBeUpdated[i].CurCell == new Vector2Int(-1, -1))
                {
                    //if it is now on screen
                    if (GetCell(toBeUpdated[i].Position) != new Vector2Int(-1, -1))
                    {
                        toBeUpdated[i].CurCell = GetCell(toBeUpdated[i].Position);
                    }
                }
                else
                {

                    //if the object is in a cell already, remove it from that cell's list
                    if (toBeUpdated[i].CurCell != null &&
                        cells[toBeUpdated[i].CurCell.X, toBeUpdated[i].CurCell.Y].WorldItems.Contains(toBeUpdated[i]))
                    {
                        cells[toBeUpdated[i].CurCell.X, toBeUpdated[i].CurCell.Y].WorldItems.Remove(toBeUpdated[i]);
                    }

                    //find out which cell it's in now
                    toBeUpdated[i].CurCell = GetCell(toBeUpdated[i].Position);

                    //if the object on the screen this frame
                    if (toBeUpdated[i].CurCell != new Vector2Int(-1, -1))
                    {
                        //add it to that cell
                        cells[toBeUpdated[i].CurCell.X, toBeUpdated[i].CurCell.Y].WorldItems.Add(toBeUpdated[i]);
                    }
                }
            }     

            toBeUpdated.Clear();
            
            for (int layer = 1; layer <= maxNeighbours; layer++)
            {
                //x = center cell location, the coord of the parent cell
                //y = center cell location
                int x = parentCell.X, y = parentCell.Y;
               

                //Start at the top left, draw to top right corner
                for (int distance = -layer; distance < layer; distance++)
                {
                    //Top bound / right bound / left bound
                    if (y - layer >= 0 && x + distance <= totalCells.X && x + distance >= 0)
                    {
                         neighbourCells.Add(cells[x + distance, y - layer]);
                    }
                }

                //Start at top right, go to bottom right
                for (int distance = -layer; distance < layer; distance++)
                {
                    //Right bound / bottom bound / top bound
                    if (x + layer <= totalCells.X && y + distance <= totalCells.Y && y + distance >= 0)
                    {
                        neighbourCells.Add(cells[x + layer, y + distance]);
                    }
                }

                //Start at bottom right, go to bottom left
                for (int distance = layer; distance > -layer; distance--)
                {
                    //Right bound / bottom bound / left bound
                    if (x + distance <= totalCells.X && y + layer <= totalCells.Y && x + distance >= 0)
                    {
                        neighbourCells.Add(cells[x + distance, y + layer]);
                    }
                }

                //Start at bottom left, go to top left
                for (int distance = layer; distance > -layer; distance--)
                {
                    //Left bound, top bound, bottom bound
                    if (x - layer >= 0 && y + distance >= 0 && y + distance <= totalCells.Y)
                    {
                       neighbourCells.Add(cells[x - layer, y + distance]);
                    }
                }
            }
        }
        
        static public Vector2Int GetCell(Vector2 position)
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

                return new Vector2Int(-1, -1);
        }

    }
}
