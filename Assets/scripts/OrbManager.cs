using UnityEngine;

public class OrbManager : MonoBehaviour
{
    public static OrbManager instance;
    private int orbsCollected = 0;
    private int totalOrbs;

    void Awake()
    {
        instance = this;
    }

    public void SetTotalOrbs(int total)
    {
        totalOrbs = total;
        Debug.Log("Total crystals to collect: " + totalOrbs);
    }

    public void CollectOrb()
    {
        orbsCollected++;
        Debug.Log("Crystals collected: " + orbsCollected + " / " + totalOrbs);

        if (orbsCollected >= totalOrbs)
        {
            Debug.Log("All crystals collected! You win!");
        }
    }
}

// Receives the total number of crystals from CrystalSpawner at the start
// Every time a crystal is collected it adds 1 to the score
// Prints score to Console
// When score reaches total → "You Win!"