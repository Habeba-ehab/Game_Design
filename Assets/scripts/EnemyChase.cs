using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private bool triggered = false;
    private AudioSource monsterAudio; // NEW

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        monsterAudio = GetComponent<AudioSource>(); // NEW
    }

    void Update()
    {
        if (player == null || triggered) return;

        agent.SetDestination(player.position);

        // Start playing monster sound when the enemy is active --- NEW
        if (!monsterAudio.isPlaying)
            monsterAudio.Play();

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < 1.5f)
        {
            triggered = true;
            monsterAudio.Stop(); // Stop growl on catch --- NEW
            GameManager.instance.ShowGameOver();
        }
    }
}