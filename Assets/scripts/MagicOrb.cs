using UnityEngine;

public class MagicOrb : MonoBehaviour
{
    public float floatSpeed = 1.5f;
    public float floatHeight = 0.5f; // floats UP only, never below start
    public float rotateSpeed = 60f;

    private float groundY;

    void Start()
    {
        groundY = transform.position.y; // remember ground position
    }

    void Update()
    {
        // Only float ABOVE ground position, never below
        float newY = groundY + Mathf.Abs(Mathf.Sin(Time.time * floatSpeed)) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OrbManager.instance.CollectOrb();
            Destroy(gameObject);
        }
    }
}

//Makes the crystal float up and down
// Makes the crystal spin
// When the player touches it → tells OrbManager "I got collected" → destroys itself