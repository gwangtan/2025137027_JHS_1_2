using UnityEngine;

public class NoiseVoxelMap : MonoBehaviour
{
    // 참고 1개
    public GameObject blockPrefab;
    public GameObject lassPrefab;
    public GameObject waterPrefab;
    public GameObject Diamond;
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
                        Place(lassPrefab, x, y, z);
                    else
                        Place(blockPrefab, x, y, z);
                }

                for (int y = h; y < waterheight; y++)
                {
                    Place(waterPrefab, x, y, z);
                }


            }
        }
    }

    // 참고 1개
    private void Place(GameObject Prefab, int x, int y, int z)
    {
        var go = Instantiate(Prefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Dirt_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = BlockType.Dirt;
        b.maxHP = 3;
        b.dropCount = 1;
        b.mineable = true;
    }
}