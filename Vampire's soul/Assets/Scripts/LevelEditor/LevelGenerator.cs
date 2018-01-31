using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public Texture2D[] maps;
    //public Texture2D map;
    public ColorToPrefab[] colorMappings;

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        
        for (int z = 0; z < maps.Length; z++)
        {
            for (int x = 0; x < maps[z].width; x++)
            {
                for (int y = 0; y < maps[z].height; y++)
                {
                    GenerateTile(x, y, z);
                }
            }
        }

        /*
            for (int x = 0; x < map.width; x++)
            {
                for (int y = 0; y < map.height; y++)
                {
                    GenerateTile(x, y, 0);
                }
            }
        */
    }

    void GenerateTile(int x, int y, int z)
    {
        Color pixelColor = maps[z].GetPixel(x, y);
        //Color pixelColor = map.GetPixel(x, y);
        //Debug.Log(pixelColor+" "+x+","+y+","+z);

        if (pixelColor.a == 0)
        {
            return; // ignore transparent pixels
        }
        
        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if(colorMapping.color.Equals(pixelColor))
            {
                Vector3 position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + z);
                Instantiate(colorMapping.prefab, position, colorMapping.prefab.transform.rotation);
            }
        }
        
    }

}
