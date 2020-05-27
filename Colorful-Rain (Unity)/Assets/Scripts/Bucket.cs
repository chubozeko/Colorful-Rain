using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bucket : MonoBehaviour
{
    
    public int scoreIncrement;
    //private string btag;
    private bool receivedDrop = false;
    private bool receivedDropInBucket1 = false;
    private bool receivedDropInBucket2 = false;
    private GameObject drop;

    private void Awake()
    {
        //tag = gameObject.tag;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col != null)
        {
            if (col.tag == "Drop")
            {
                receivedDrop = true;
                if (gameObject.tag == "Bucket1")
                    receivedDropInBucket1 = true;
                else if (gameObject.tag == "Bucket2")
                    receivedDropInBucket2 = true;

                drop = col.gameObject;
                drop.GetComponent<Drop>().isOffscreen = true;
                drop.GetComponent<Drop>().hitAnyBucket = true;

                AudioManager.Instance.PlayOneShot(AudioManager.Instance.paintDrop);
            }
        }
    }

    public void SetScoreIncrement(int inc) { scoreIncrement = inc; }

    public bool DidDropHitBucket()
    {
        return receivedDrop;
    }

    public int[] CheckDrop(int b1, int b2)
    {
        // METHOD 2:
        // 1. Check which bucket received the drop
        // 2. Check if the drop color == bucket color
        if(receivedDropInBucket1)
        {
            Material dropMat = drop.GetComponent<SpriteRenderer>().material;
            Material bucketMat = gameObject.GetComponent<SpriteRenderer>().material;
            if (dropMat.color == bucketMat.color)
            {
                // Increase score of Bucket 1
                b1 += scoreIncrement;
            }
            else
            {
                // Decrease score of Bucket 2
                b2 -= scoreIncrement;
            }
        }

        if (receivedDropInBucket2)
        {
            Material dropMat = drop.GetComponent<SpriteRenderer>().material;
            Material bucketMat = gameObject.GetComponent<SpriteRenderer>().material;
            if (dropMat.color == bucketMat.color)
            {
                // Increase score of Bucket 2
                b2 += scoreIncrement;
            }
            else
            {
                // Decrease score of Bucket 1
                b1 -= scoreIncrement;
            }
        }

        receivedDrop = false;
        receivedDropInBucket1 = false;
        receivedDropInBucket2 = false;

        return new int[] { b1, b2 } ;
    }

}
