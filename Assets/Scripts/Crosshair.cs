using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D col;
    private bool enemyInRadius;
    private GameObject enemy;
    private Shake shake;
    private UIManager manager;

    public int ammo = 100;

    private void Awake()
    {
        Cursor.visible = false; // hide default cursor
    }

    private void Start()
    {
        if (!shake) shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        if (!manager) manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
    }

    void Update()
    {
        Vector2 mouseCursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseCursorPosition;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (enemyInRadius && ammo > 0)
            {
                Shoot();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            enemy = collision.gameObject;
            enemyInRadius = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            enemyInRadius = false;
            enemy = null;
        }
    }

    public void Shoot()
    {
        int points = enemy.GetComponent<Asteroid>().damage;
        Destroy(enemy);
        shake.CamShake();
        ammo -= 1;
        manager.IncreaseScore(points);
    }

}
