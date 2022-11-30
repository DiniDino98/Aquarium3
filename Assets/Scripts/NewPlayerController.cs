using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayerController : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 5;
    [SerializeField] private Healthbar _healthbar;
    [SerializeField] private AudioSource thump;
    [SerializeField] private Image teleportscreen;

    private GameObject gameObject2;
    public int speed;
    public float xRange;
    public float yRange;
    public float zRange;
    public float negxRange;
    public float negyRange;
    public float negzRange;
    private float _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
        _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
        StartCoroutine(AttenboroughBegins(10));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            transform.position = transform.position + Camera.main.transform.forward * speed * Time.deltaTime;
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x < negxRange)
        {
            transform.position = new Vector3(negxRange, transform.position.y, transform.position.z);
        }

        if (transform.position.y > yRange)
        {
            transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        }

        if (transform.position.y < negyRange)
        {
            transform.position = new Vector3(transform.position.x, negyRange, transform.position.z);
        }

        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }

        if (transform.position.z < negzRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, negzRange);
        }
    }

    public void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Teleport"))
        {
            _currentHealth -= 1;
            _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);

            teleportscreen.enabled = true;

            StartCoroutine(Teleport(3));

        }
    }

    IEnumerator AttenboroughBegins(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        thump = GameObject.Find("begintext").GetComponent<AudioSource>();
        thump.Play();
    }

    IEnumerator Teleport(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Application.LoadLevel(1);
    }
       
}
