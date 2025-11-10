using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;

    [Header("EnemySpawn")]
    [SerializeField] Camera cam;                 
    [SerializeField] GameObject enemyPrefab;
    [Tooltip("화면 밖에서 얼마나 떨어뜨릴지")]
    [SerializeField] float SpawnOffset = 1.0f;    // 화면 밖으로 얼마나 떨어뜨릴지(월드 유닛)
    [SerializeField] float enemyZ = 0f;          // 2D 레이어 Z
    [SerializeField] int enemyCountLimit = 10;
    public int enemyCount = 0;

    void Awake()
    {
        //싱글턴
        Instance = this;

        cam = Camera.main;
    }

    private void Update()
    {
        if (enemyCount == enemyCountLimit) return;

        SpawnOne();
    }

    //호출 시 1마리 스폰
    public void SpawnOne()
    {
        enemyCount++;

        Vector3 pos = GetRandomOffscreenPos2D(cam, SpawnOffset, enemyZ);
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }

    // 화면 “바로 밖” 랜덤 위치 (좌/우/상/하 중 한 변) 반환
    public static Vector3 GetRandomOffscreenPos2D(Camera cam, float margin, float zPos = 0f)
    {
        Vector3 c = cam.transform.position;
        float halfH = cam.orthographicSize;
        float halfW = halfH * cam.aspect;

        int side = Random.Range(0, 4); // 0=Left,1=Right,2=Top,3=Bottom
        float x = 0f, y = 0f;

        switch (side)
        {
            case 0: // Left
                x = c.x - halfW - margin;
                y = Random.Range(c.y - halfH, c.y + halfH);
                break;
            case 1: // Right
                x = c.x + halfW + margin;
                y = Random.Range(c.y - halfH, c.y + halfH);
                break;
            case 2: // Top
                y = c.y + halfH + margin;
                x = Random.Range(c.x - halfW, c.x + halfW);
                break;
            default: // Bottom
                y = c.y - halfH - margin;
                x = Random.Range(c.x - halfW, c.x + halfW);
                break;
        }

        return new Vector3(x, y, zPos);
    }
}
