using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D col;
    private GameObject target;
    private Shake shake;
    private UIManager manager;
    private bool enemy;
    private Spaceship spaceship;

    public int ammo = 100;

    private void Awake()
    {
        Cursor.visible = false; // hide default cursor
    }

    private void Start()
    {
        if (!shake) shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        if (!manager) manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
        if (!spaceship) spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Spaceship>();
    }

    void Update()
    {
        if (spaceship.dead)
        {
            gameObject.SetActive(false);
            return;
        }
        Vector2 mouseCursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseCursorPosition;
    }

    private void LateUpdate()
    {
        if (spaceship.dead) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (ammo > 0)
            {
                Shoot();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            enemy = true;
            target = collision.gameObject;
        }
        else if (collision.gameObject.layer == 7)
        {
            enemy = false;
            target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 7)
        {
            target = null;
            enemy = false;
        }
    }

    public void Shoot()
    {
        if (enemy)
        {
            int points = target.GetComponent<Asteroid>().damage;    
            manager.IncreaseScore(points);
        }
        if (target) Destroy(target);
        shake.CamShake();
        ammo -= 1;
    }
}
