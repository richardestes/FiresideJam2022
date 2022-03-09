using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D col;
    private GameObject target;
    private Shake shake;
    private GameManager manager;
    private bool enemy;
    private Spaceship spaceship;
    private Vector2 mouseCursorPosition;
    private ParticleSystem _particleRef;
    private ParticleSystem ps;

    public ParticleSystem particles;
    public int ammo = 100;

    private void Awake()
    {
        Cursor.visible = false; // hide default cursor
    }

    private void Start()
    {
        if (!shake) shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        if (!manager) manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (!spaceship) spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Spaceship>();
        _particleRef = particles;  // DO NOT DELETE
    }

    void Update()
    {
        if (spaceship.dead)
        {
            gameObject.SetActive(false);
            return;
        }
        mouseCursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
            float points = 0f;
            if (target.tag == "Asteroid")
            {
                Asteroid asteroid = target.GetComponent<Asteroid>();
                points = asteroid.damage;
            }
            else if (target.tag == "Pirate")
            {
                Pirate pirate = target.GetComponent<Pirate>();
                points = pirate.damage;
            }    
            manager.IncreaseScore(points);
        }

        if (target)
        {
            if (target.tag == "Asteroid") CreateParticles(Color.gray);
            else if (target.tag == "Pirate") CreateParticles(Color.red);
            else CreateParticles(Color.gray);
            Destroy(target);
        }

        shake.CamShake();
        ammo -= 1;
    }

    void CreateParticles(Color color)
    {
        particles = Instantiate(_particleRef, mouseCursorPosition, Quaternion.identity);
        particles.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        ps = particles.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule _main = ps.main;
        _main.startColor = color;
        ps.Stop();
        RandomizeParticleDuration();
        ps.Play();
    }

    void RandomizeParticleDuration()
    {
        var main = ps.main;
        main.duration = Random.Range(0.25f, 0.5f);
    }
}
