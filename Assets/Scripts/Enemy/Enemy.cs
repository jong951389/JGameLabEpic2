using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] Transform player;
    [SerializeField] float moveSpeed = 10.0f;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        TracePlayer();
    }

    void TracePlayer()
    {
        transform.position =  Vector2.MoveTowards(transform.position, player.position, moveSpeed*Time.fixedDeltaTime);

        //플레이어 쳐다보게
        Vector2 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnDestroy()
    {
        EnemySpawnManager.Instance.enemyCount--;
    }
}
