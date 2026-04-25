using UnityEngine;

public class OrbManager : MonoBehaviour
{
    public static OrbManager instance;
    private AudioSource collectSound; // NEW

    void Awake()
    {
        instance = this;
        collectSound = GetComponent<AudioSource>(); // NEW
    }

    public void SetTotalOrbs(int total)
    {
        GameManager.instance.SetTotalCrystals(total);
    }

    public void CollectOrb()
    {
        if (collectSound != null)
            collectSound.Play(); // NEW
            
        GameManager.instance.CrystalCollected();
    }
}