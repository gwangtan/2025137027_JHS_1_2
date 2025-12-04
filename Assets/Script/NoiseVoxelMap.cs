using UnityEngine;

public class NoiseVoxelMap : MonoBehaviour
{
    // 참고 1개
    public GameObject blockPrefab;
    public GameObject lassPrefab;
    public GameObject waterPrefab;
    // 참고 1개
    public int width = 20;
    // 참고 1개
    public int depth = 20;

    [SerializeField] int waterheight = 5;

    // 참고 1개
    public int maxHeight = 16; // Y
    // 참고 2개
    [SerializeField] float noiseScale = 20f;

    // 참고 0개
    void Start()
    {
        float offsetX = Random.Range(-9999f, 9999f);
        float offsetZ = Random.Range(-9999f, 9999f);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                float nx = (x + offsetX) / noiseScale;
                float nz = (z + offsetZ) / noiseScale;

                float noise = Mathf.PerlinNoise(nx, nz);

                int h = Mathf.FloorToInt(noise * maxHeight);

                if (h <= 0) continue;

                for (int y = 0; y < h; y++)
                {
                    if (y == h - 1)
                        PlaceGrass(x, y, z);
                    else
                        PlaceDirt( x, y, z);
                }

                for (int y = h; y < waterheight; y++)
                {
                    PlaceWater(x, y, z);
                }


            }
        }
    }
    public void PlaceTile(Vector3Int pos, ItemType type)
    {
        switch (type)
        {
            case ItemType.Dirt:
                PlaceDirt(pos.x, pos.y, pos.z);
                break;
            case ItemType.Grass:
                PlaceGrass(pos.x, pos.y, pos.z);
                break;
            case ItemType.Water:
                PlaceWater(pos.x, pos.y, pos.z);
                break;

        }
    }

    // 참고 1개
    private void PlaceDirt( int x, int y, int z)
    {
        var go = Instantiate(blockPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Dirt_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.Dirt;
        b.maxHP = 3;
        b.dropCount = 1;
        b.mineable = true;
    }

    private void PlaceGrass(int x, int y, int z)
    {
        var go = Instantiate(lassPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Dirt_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.Dirt;
        b.maxHP = 3;
        b.dropCount = 1;
        b.mineable = true;
    }

    private void PlaceWater(int x, int y, int z)
    {
        var go = Instantiate(waterPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Dirt_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.Dirt;
        b.maxHP = 2;
        b.dropCount = 1;
        b.mineable = true;
    }
}