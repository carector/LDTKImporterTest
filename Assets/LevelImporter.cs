using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Entity
{
    public string id = null;
    public int x;
    public int y;
}

[System.Serializable]
public class LevelTile
{
    public int id;
    public Vector2Int position;

    public LevelTile(int x, int y)
    {
        position = new Vector2Int(x, y);
    }
}

[System.Serializable] 
public class LevelData
{
    public int width;
    public int height;
    public List<LevelTile> tiles;

    public static LevelData CreateFromJSON(string levelFolderPath)
    {
        string dataPath = levelFolderPath + "/data.json";
        string csvPath = levelFolderPath + "/tiledata.csv";

        StreamReader sr = new StreamReader(dataPath);
        string json = sr.ReadToEnd();
        return JsonConvert.DeserializeObject<LevelData>(json);
    }
}

public class LevelImporter : MonoBehaviour
{
    public Tilemap tilemap;
    public List<TileBase> tiles;
    public LevelData l;

    private void Start()
    {
        tilemap.ClearAllTiles();
        tilemap.SetTile(Vector3Int.zero, tiles[0]);
        LoadLevel("Level_0");
    }

    public void LoadLevel(string levelID)
    {
        string levelPath = Application.streamingAssetsPath + "/TestProject/simplified/" + levelID;
        l = LevelData.CreateFromJSON(levelPath);


    }
}
