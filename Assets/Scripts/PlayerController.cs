using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] FlashImage _flashImage = null;
    [SerializeField] Color _newColor = Color.white;
    [SerializeField] private Healthbar _healthbar;
    [SerializeField] private float _maxHealth = 4;
    [SerializeField] private Image customImage;
    [SerializeField] private AudioSource thump;
    [SerializeField] private Animator punch;
    [SerializeField] private Image gameoverscreen;
    [SerializeField] private TMP_Text part1;
    [SerializeField] private TMP_Text part2;
    [SerializeField] private TMP_Text part3;
    [SerializeField] private TMP_Text part4;
    [SerializeField] private TMP_Text part5;
    [SerializeField] private TMP_Text part6;
    [SerializeField] private TMP_Text part7;
    [SerializeField] private AudioSource endcredits;



    public int speed;
    public float xRange;
    public float yRange;
    public float zRange;
    public float negxRange;
    public float negyRange;
    public float negzRange;
    private float _currentHealth;
    public CameraShake cameraShake;
    public CameraRotator cameraRotate;
    public MeshRenderer food1;
    public MeshRenderer food2;
    public MeshRenderer food3;

    



    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = 4;
        _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
        customImage = GameObject.Find("Memory").GetComponent<Image>();
        customImage.enabled = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FishFood"))
        {
            food1 = GameObject.Find("food3memory").GetComponent<MeshRenderer>();
            food1.enabled = false;
            food2 = GameObject.Find("food1thump").GetComponent<MeshRenderer>();
            food2.enabled=false;
            food3 = GameObject.Find("food4camera").GetComponent<MeshRenderer>();
            food3.enabled=false;

            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;

            StartCoroutine(cameraRotate.Dizzy(10f));

            StartCoroutine(OffWhenReading(35));

            StartCoroutine(AttenboroughDizzy(10));

            _currentHealth -= 1;
            _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
        }

        if (other.gameObject.CompareTag("FishFood1"))
        {
            food1 = GameObject.Find("food3memory").GetComponent<MeshRenderer>();
            food1.enabled = false;
            food2 = GameObject.Find("food1thump").GetComponent<MeshRenderer>();
            food2.enabled = false;
            food3 = GameObject.Find("food2dizzy").GetComponent<MeshRenderer>();
            food3.enabled = false;

            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;

            StartCoroutine(CameraFlash(30));

            StartCoroutine(OffWhenReading(45));

            StartCoroutine(AttenboroughCamera(30));

            _currentHealth -= 1;
            _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
        }

        if (other.gameObject.CompareTag("FishFood3"))
        {
            food1 = GameObject.Find("food2dizzy").GetComponent<MeshRenderer>();
            food1.enabled = false;
            food2 = GameObject.Find("food1thump").GetComponent<MeshRenderer>();
            food2.enabled = false;
            food3 = GameObject.Find("food4camera").GetComponent<MeshRenderer>();
            food3.enabled = false;

            _currentHealth -= 1;
            _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;

            customImage.enabled = true;

            StartCoroutine(MemoryFlashInOut(10));

            StartCoroutine(OffWhenReading(40));

            StartCoroutine(AttenboroughMemory(10));

            thump = GameObject.Find("visitor3").GetComponent<AudioSource>();
            thump.Play();
        }

        if (other.gameObject.CompareTag("FishFoodThump"))
        {
            food1 = GameObject.Find("food3memory").GetComponent<MeshRenderer>();
            food1.enabled = false;
            food2 = GameObject.Find("food2dizzy").GetComponent<MeshRenderer>();
            food2.enabled = false;
            food3 = GameObject.Find("food4camera").GetComponent<MeshRenderer>();
            food3.enabled = false;

            StartCoroutine(WindowThump(3));

            StartCoroutine(cameraShake.Shake(8f, .5f));

            StartCoroutine(AttenboroughThump(10));

            StartCoroutine(OffWhenReading(35));

            StartCoroutine(StandStill(8));

            _currentHealth -= 1;
            _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
            other.gameObject.GetComponent<MeshRenderer>().enabled=false;
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

        if (_currentHealth == 0)
        {
            StartCoroutine(GameOver(40));
        }

        IEnumerator GameOver(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            gameoverscreen.enabled = true;
            endcredits.Play();
            StartCoroutine(GameOverPart1(1));
            StartCoroutine(GameOverPart2(16));
            StartCoroutine(GameOverPart3(31));
            StartCoroutine(GameOverPart4(46));
            StartCoroutine(GameOverPart5(61));
            StartCoroutine(GameOverPart6(76));
            StartCoroutine(GameOverPart7(91));
            GameObject.Find("aquariumground").SetActive(false);
        }

        IEnumerator StandStill(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            punch = GameObject.Find("visitor1").GetComponent<Animator>();
            punch.Play("Idle", 0, 0.0f);
        }

        IEnumerator GameOverPart1(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            part1.enabled = true;
        }

        IEnumerator GameOverPart2(int seconds)
        {
            
            yield return new WaitForSeconds(seconds);
            part1.enabled = false;
            part2.enabled = true;
        }

        IEnumerator GameOverPart3(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            part2.enabled = false;
            part3.enabled = true;
        }

        IEnumerator GameOverPart4(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            part3.enabled = false;
            part4.enabled = true;
        }

        IEnumerator GameOverPart5(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            part4.enabled = false;
            part5.enabled = true;
        }

        IEnumerator GameOverPart6(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            part5.enabled = false;
            part6.enabled = true;
        }

        IEnumerator GameOverPart7(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            part6.enabled = false;
            part7.enabled = true;
        }

        IEnumerator WindowThump(int times)
        {
            for (int i = 0; i < times; i++)
            {
                thump = GameObject.Find("visitor1").GetComponent<AudioSource>();
                thump.Play();
                punch = GameObject.Find("visitor1").GetComponent<Animator>();
                punch.Play("Hook Punch", 0, 0.0f);
                yield return new WaitForSeconds(2);
            }
        }

        IEnumerator AttenboroughThump(int seconds)
        {
            yield return new WaitForSeconds(seconds);

            thump = GameObject.Find("thumptext").GetComponent<AudioSource>();
            thump.Play();
        }

        IEnumerator AttenboroughMemory(int seconds)
        {
            yield return new WaitForSeconds(seconds);

            thump = GameObject.Find("memorytext").GetComponent<AudioSource>();
            thump.Play();
        }

        IEnumerator OffWhenReading(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            food1.enabled = true;
            food2.enabled = true;
            food3.enabled = true;
        }   

        IEnumerator AttenboroughCamera(int seconds)
        {
            yield return new WaitForSeconds(seconds);

            thump = GameObject.Find("cameratext").GetComponent<AudioSource>();
            thump.Play();
        }

        IEnumerator AttenboroughDizzy(int seconds)
        {
            yield return new WaitForSeconds(seconds);

            thump = GameObject.Find("dizzytext").GetComponent<AudioSource>();
            thump.Play();
        }

        IEnumerator MemoryFlashInOut(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            customImage.enabled=false;
        }

        IEnumerator CameraFlash(int times)
        {
            for (int i = 0; i < times; i++)
            {
                _flashImage = GameObject.Find("FlashImage").GetComponent<FlashImage>();
                _flashImage.StartFlash(.25f, 10f, _newColor);
                thump = GameObject.Find("visitor2").GetComponent<AudioSource>();
                thump.Play();
                yield return new WaitForSeconds(1);

            }
        }

    }
}
