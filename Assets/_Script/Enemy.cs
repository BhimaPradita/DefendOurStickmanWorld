using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float movespeed = 2f;

    private Rigidbody2D rb;
    private Transform checkpoint;
    private int index = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        checkpoint = EnemyManager.main.checkpoints[index];
    }

    void Update()
    {
        checkpoint = EnemyManager.main.checkpoints[index];

        if(Vector2.Distance(checkpoint.transform.position,transform.position) <= 0.1f)
        {
            index++;
            if(index >= EnemyManager.main.checkpoints.Length)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (checkpoint.position - transform.position).normalized;
        transform.right = checkpoint.position - transform.position;
        rb.linearVelocity = direction * movespeed;
    }
}
