using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    Vector3 direction;
    public float speed;
    public GroundSpawner groundSpawner; //veri cekmek icin GroundSpawner scriptini tanimliyoruz.
    public static bool isFall;

    public static bool gameOver;
    public GameObject gameOverCanvas;

    public Text lastScoreText;
    public Text bestScoreText;

    public GameObject ps; //particle system

    void Start()
    {
        gameOver = false;
        direction = Vector3.forward; //oyun basinda z yonunde ileri gitmesini istiyorum.
        isFall = false;
    }


    void Update()
    {
        //dusup dusmedigimizi kontrol ediyoruz.
        if (transform.position.y < 0.5f)
        {
            isFall = true;
            GameOver();
        }

        if (isFall == true)
        {
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Score.score++; //skoru arttir

            if (direction.x == 0) //x 0 sa ileri gidiyordur
            {
                direction = Vector3.left; //sola gitsin
            }
            else
            {
                direction = Vector3.forward; //soldaysa ileri gidecek
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 move = direction * Time.deltaTime * speed;
        transform.position += move;
    }

    //Temastan ayrilinca calisir
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            //temastan kesilince rigidbody ekleyerek dusmesini sagliyoruz.
            other.gameObject.AddComponent<Rigidbody>();

            groundSpawner.GroundCreate();
            StartCoroutine(DeleteGround(other.gameObject));
        }
    }

    //Zeminleri silmemiz icin gereken komut
    IEnumerator DeleteGround(GameObject DeletedGround)
    {
        //belirli sure sonra sildirmek icin bekletiyoruz.

        yield return new WaitForSeconds(1f);
        Destroy(DeletedGround);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUp")
        {
            Instantiate(ps, transform.position, Quaternion.identity);
            Score.score = Score.score + 2;
            other.gameObject.SetActive(false);
        }
    }

    private void GameOver()
    {
        lastScoreText.text = Score.score.ToString();

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (Score.score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", Score.score);
        }

        bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
    }
}