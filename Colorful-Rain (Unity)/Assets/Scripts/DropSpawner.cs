using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    public GameObject drop;
    public List<GameObject> drops;
    public float dropPosition;
    public float startingYPosition = 320;
    public float dropSpawningTime = 30;
    public List<Material> bucketColors;
    private int nrOfWastedDrops = 0;
    private int totalDrops = 0;

    void Start()
    {
        // AudioManager.Instance.soundEffectAudio.Play();
    }

    void Update()
    {
        if (Time.frameCount % dropSpawningTime == 0)
        {
            SpawnDroplet();
        }

        try
        {
            foreach (GameObject d in drops.ToArray())
            {
                if (d != null)
                {
                    if (d.GetComponent<Drop>().isOffscreen == true)
                    {
                        // Check if Drop didn't hit a bucket
                        if(!d.GetComponent<Drop>().hitAnyBucket)
                        {
                            nrOfWastedDrops++;
                            AudioManager.Instance.PlayOneShot(AudioManager.Instance.paintSplash);
                        }
                        Destroy(drops[drops.IndexOf(d)]);
                        drops.Remove(d);
                    }
                }
                if (d == null)
                {
                    drops.Remove(d);
                }
            }
        }
        catch (MissingComponentException e) { }
    }

    public void SpawnDroplet()
    {
        GameObject temp;
        // Drop
        System.Random rdEraser = new System.Random();
        dropPosition = rdEraser.Next(-385, 385);
        // Random color
        Material mat = bucketColors[Random.Range(0, bucketColors.Count)];
        drop.GetComponent<SpriteRenderer>().material = mat;
        // Instantiate
        temp = Instantiate(drop,
            new Vector2(dropPosition, startingYPosition),
            Quaternion.identity);
        drops.Add(temp);

        totalDrops++;
    }

    public int GetNumberOfWastedDrops() { return nrOfWastedDrops; }
    public int GetTotalNumberOfDrops() { return totalDrops; }

    public void DestroyRemainingDrops()
    {
        try
        {
            foreach (GameObject d in drops.ToArray())
            {
                drops.Remove(d);
            }
        }
        catch (MissingComponentException e) { }
    }
}
