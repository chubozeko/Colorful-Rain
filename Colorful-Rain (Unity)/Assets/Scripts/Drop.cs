using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public float dropSpeed = 5f;
    public bool isOffscreen;
    public bool hitAnyBucket;

    void Start()
    {
        isOffscreen = false;
        hitAnyBucket = false;
        dropSpeed = PlayerPrefs.GetFloat("DropSpeed");
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * dropSpeed * 100;
    }

    private void OnBecameInvisible()
    {
        isOffscreen = true;
    }
}
