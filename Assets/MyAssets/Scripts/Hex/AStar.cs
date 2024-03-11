using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AStar
{
    List<TileData> tiles;
    public List<TileData> openList;
    public List<TileData> closedList;

    public List<TileData> FindPath(TileData startTile, TileData endTile)
    {
        
        openList = new List<TileData> { startTile };
        closedList = new List<TileData>();

        startTile.g = 0;
        startTile.h = HexDistance(startTile.Position, endTile.Position);
        startTile.f = startTile.h;
        while(openList.Count > 0)
        {
            TileData currentTile = openList.OrderBy(n=> n.f).First();
            if(currentTile == endTile)
            {
                return ReconstructPath(currentTile);
            }
            
            openList.Remove(currentTile);
            closedList.Add(currentTile);

            foreach (var neighbor in GetNeighbors(currentTile.Position))
            {
                TileData checkTile = GetTileData(neighbor);
                if (checkTile != null)
                {
                    if(checkTile.isWater || closedList.Contains(checkTile))
                    {
                        if (currentTile == checkTile.parent)
                        {
                            Debug.LogWarning("Tile is already current Parent!");
                            checkTile.parent = null;
                        }

                        continue;
                    }
                    int tentativeGScore = checkTile.g + HexDistance(currentTile.Position, checkTile.Position);

                    if(tentativeGScore < checkTile.g || !openList.Contains(checkTile))
                    {

                        checkTile.parent = currentTile;
                        checkTile.g = tentativeGScore;
                        checkTile.h = HexDistance(checkTile.Position, endTile.Position);
                        checkTile.f = checkTile.h + checkTile.g;
                        if (!openList.Contains(checkTile))
                        {
                            openList.Add(checkTile);
                        }
                    }
                }
            }
        }

        return null; // Path not found
        
    }

    private TileData GetTileData(Vector3Int pos)
    {
        //pulls the tile at position argument. Could be used to check if tile is water or not as well
        foreach (TileData tile in tiles)
        {

            if (tile.Position == pos)
            {
                return tile;
            }
        }
        return null;

    }
    public List<TileData> SendTileData(List<TileData>tiledata)
    {
        tiles = tiledata;
        return null;
    } 
    public List<Vector3Int> GetNeighbors(Vector3Int hexPos)
    {
        //convert to oddr offset. 
        List<Vector3Int> neighbors = new List<Vector3Int>();

        Vector3Int[] oddrDirectionsEvenRow = {
        new Vector3Int(+1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(-1, -1, 0),
        new Vector3Int(-1, 0, 0), new Vector3Int(-1, +1, 0), new Vector3Int(0, +1, 0)
        };

        Vector3Int[] oddrDirectionsOddRow = {
        new Vector3Int(+1, 0, 0), new Vector3Int(+1, -1, 0), new Vector3Int(0, -1, 0),
        new Vector3Int(-1, 0, 0), new Vector3Int(0, +1, 0), new Vector3Int(+1, +1, 0)
        };

        //handles if even row/if odd row
        Vector3Int[] directions = hexPos.y % 2 == 0 ? oddrDirectionsEvenRow : oddrDirectionsOddRow;


        for (int i = 0; i < 6; i++)
        {
            Vector3Int neighborPos = hexPos + directions[i];
            neighbors.Add(neighborPos);
        }
        return neighbors;
    }

    List<TileData> ReconstructPath(TileData currentTile)
    {
        
        List<TileData> path = new List<TileData>();
        while (currentTile.parent != null)
        {
            
            path.Add(currentTile);
            currentTile = currentTile.parent;
        }
        foreach(TileData tile in path)
        {
            tile.parent = null;
        }
        return path;
    }
    int HexDistance(Vector3Int a, Vector3Int b)
    {
        return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z)) / 2;
    }
}
