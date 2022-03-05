using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D col;
    private bool enemyInRadius;
    private GameObject enemy;

    private Shake shake;

    private void Awake()
    {
        Cursor.visible = false; // hide default cursor
    }

    private void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
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
            if (enemyInRadius)
            {
                Destroy(enemy);
                shake.CamShake();
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

}
