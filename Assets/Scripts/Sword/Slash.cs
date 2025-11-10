using UnityEngine;

public class Slash : MonoBehaviour
{
    [Header("Slash")]
    [SerializeField] float destroyTime = 2.0f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
