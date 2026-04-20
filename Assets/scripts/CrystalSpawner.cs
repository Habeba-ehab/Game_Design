using UnityEngine;

public class CrystalSpawner : MonoBehaviour
{
    public GameObject[] crystalPrefabs;
    public int numberOfCrystals = 8;

    public float minX = 5f;
    public float maxX = 55f;
    public float minZ = 5f;
    public float maxZ = 55f;
    public float heightAboveGround = 0f;
    public float crystalScale = 0.2f;
    public float maxSpawnHeight = 2f;

    void Start()
    {
        int spawned = 0;
        int attempts = 0;

        while (spawned < numberOfCrystals && attempts < 200)
        {
            attempts++;

            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);

            Ray ray = new Ray(new Vector3(randomX, 500f, randomZ), Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f))
            {
                if (hit.point.y <= maxSpawnHeight)
                {
                    float groundY = hit.point.y + heightAboveGround;
                    Vector3 spawnPos = new Vector3(randomX, groundY, randomZ);
                    GameObject crystal = Instantiate(crystalPrefabs[Random.Range(0, crystalPrefabs.Length)], spawnPos, Quaternion.identity);
                    
                    // Only set scale on parent, no child loop
                    crystal.transform.localScale = Vector3.one * crystalScale;

                    SphereCollider sc = crystal.GetComponent<SphereCollider>();
                    if (sc != null) sc.radius = 3f;

                    spawned++;
                }
            }
        }

        OrbManager.instance.SetTotalOrbs(spawned);
    }
}

// Picks a random position on the terrain
// Checks the position is on flat ground and not too high
// Spawns a random crystal there
// Repeats until all 8 crystals are placed
// Then tells OrbManager "there are 8 crystals total"