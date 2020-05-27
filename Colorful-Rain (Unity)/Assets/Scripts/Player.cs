using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    private float equalBucketScore;
    public int scoreB1;
    public int scoreB2;
    public float movementSpeed = 5f;
    public float wheelRotationSpeed = 15f;
    private float wheelRotation = 0f;
    public GameObject bucket1;
    public GameObject bucket2;
    public Slider bucket1Slider;
    public Slider bucket2Slider;
    public GameObject wheel;

    void Start()
    {
        FindObjectOfType<GameManager>().LoadGameSceneComponents();
        sr = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Scores
        scoreB1 = 0;
        scoreB2 = 0;
        // Sliders
        bucket1Slider.minValue = 0;
        bucket1Slider.maxValue = PlayerPrefs.GetInt("LevelTotal") * 2;
        bucket1Slider.value = scoreB1;
        bucket2Slider.minValue = 0;
        bucket2Slider.maxValue = PlayerPrefs.GetInt("LevelTotal") * 2;
        bucket2Slider.value = scoreB2;
        // Set Level Increment
        bucket1.GetComponent<Bucket>().SetScoreIncrement(PlayerPrefs.GetInt("ScoreIncrement"));
        bucket2.GetComponent<Bucket>().SetScoreIncrement(PlayerPrefs.GetInt("ScoreIncrement"));
        // Set Level Complete Bucket Score
        equalBucketScore = PlayerPrefs.GetInt("LevelTotal");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            FindObjectOfType<GameManager>().PauseCurrentGame();
        }
        // Check if Buckets are equal (Level Complete!)
        if (bucket1Slider.value == equalBucketScore && bucket1Slider.value == bucket2Slider.value)
        {
            // Level Complete
            FindObjectOfType<GameManager>().ViewLevelComplete();
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.gameOver);
        }
        else
        {
            // Check if Drops hit Buckets
            int[] tempScores = { scoreB1, scoreB2 };
            if (bucket1.GetComponent<Bucket>().DidDropHitBucket())
            {
                tempScores = bucket1.GetComponent<Bucket>().CheckDrop(scoreB1, scoreB2);
            }
            if (bucket2.GetComponent<Bucket>().DidDropHitBucket())
            {
                tempScores = bucket2.GetComponent<Bucket>().CheckDrop(scoreB1, scoreB2);
            }
            scoreB1 = tempScores[0];
            scoreB2 = tempScores[1];
            bucket1Slider.value = scoreB1;
            bucket2Slider.value = scoreB2;

            // Swap Buckets
            if (Input.GetButtonDown("Swap"))
            {
                SwapBuckets();
            }
        }
    }

    void FixedUpdate()
    {
        // Movement
        float horzMove = Input.GetAxisRaw("Horizontal");
        Vector2 vect = rb.velocity;
        rb.velocity = new Vector2(horzMove * movementSpeed * 100, vect.y);

        if (horzMove != 0)
        {
            if (horzMove < 0)
            {
                wheel.transform.rotation = Quaternion.Euler(0, 0, wheelRotation);
            }
            else if (horzMove > 0)
            {
                wheel.transform.rotation = Quaternion.Euler(0, 0, -wheelRotation);
            }
            wheelRotation += wheelRotationSpeed;
        }
    }

    private void SwapBuckets()
    {
        // Buckets
        Vector3 b1Pos = bucket1.transform.position;
        Vector3 b2Pos = bucket2.transform.position;
        bucket1.transform.position = b2Pos;
        bucket2.transform.position = b1Pos;
        bucket1.GetComponent<SpriteRenderer>().flipX = !bucket1.GetComponent<SpriteRenderer>().flipX;
        bucket2.GetComponent<SpriteRenderer>().flipX = !bucket2.GetComponent<SpriteRenderer>().flipX;
        // Scores
        if(bucket1Slider.direction == Slider.Direction.LeftToRight)
            bucket1Slider.direction = Slider.Direction.RightToLeft;
        else
            bucket1Slider.direction = Slider.Direction.LeftToRight;

        if (bucket2Slider.direction == Slider.Direction.LeftToRight)
            bucket2Slider.direction = Slider.Direction.RightToLeft;
        else
            bucket2Slider.direction = Slider.Direction.LeftToRight;

        AudioManager.Instance.PlayOneShot(AudioManager.Instance.swapBuckets);
    }


}
