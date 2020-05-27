using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleArt : MonoBehaviour
{
    public GameObject cSprite;
    public float cWidth = 600;
    public float cHeight = 600;
    public Vector2 minWidthAndHeight;
    public Vector2 maxWidthAndHeight;
    public float circleMinSize = 5;
    public float circleMaxSize = 36;
    public int nrOfCircles = 100;
    public int maxNrOfCircles;
    public List<GameObject> circles;
    public List<Material> buckets;
    void Awake()
    {
        int protection = 0;
        nrOfCircles = Random.Range(75, maxNrOfCircles*10);
        circles = new List<GameObject>();
        while (circles.Count <= 300)
        {
            // Circle temp = new Circle(cWidth, cHeight, circleMinSize, circleMaxSize);
            Circle temp = cSprite.GetComponent<Circle>();
            temp.Init(minWidthAndHeight, maxWidthAndHeight, circleMinSize, circleMaxSize);
            int b = Random.Range(0, buckets.Count);
            Color bColor = buckets[b].color;
            temp.SetRGBA(bColor.r, bColor.g, bColor.b, Random.Range(0.1f, 1f));
            //temp.SetRGBA(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0.4f, 1f));

            /*
            var circle = {
            x: random(width),
            y: random(height),
            r: random(5, 36),
            red: random(0, 255),
            green: random(0, 255),
            blue: random(0, 255)
            };
            */

            bool overlapping = false;
            foreach (GameObject g in circles)
            {
                Circle other = g.GetComponent<Circle>();
                float d = Mathf.Sqrt(Mathf.Pow(temp.GetX() - other.GetX(), 2) + Mathf.Pow(temp.GetY() - other.GetY(), 2));
                if (d < temp.GetRadius() + other.GetRadius())
                {
                    overlapping = true;
                }
            }

            if (!overlapping)
            {
                circles.Add(cSprite);
            }

            protection++;

            if (protection > nrOfCircles)
            {
                break;
            }


            /*
            for (var i = 0; i < circles.length; i++){   // Colourful
                fill(circles[i].red, circles[i].green, circles[i].blue, random(100, 255));
                noStroke();
                ellipse(circles[i].x, circles[i].y, circles[i].r*2, circles[i].r*2);
            }

            for (var i = 0; i < circles.length; i++){   // Red (currently)
                fill(circles[i].red, 0, 0, random(50, 255));
                noStroke();
                ellipse(circles[i].x, circles[i].y, circles[i].r*2, circles[i].r*2);
            }
            */

            foreach (GameObject c in circles)
            {
                // Black and White
                /*
                fill(circles[i].red, circles[i].red, circles[i].red, random(100, 255));
                noStroke();
                ellipse(circles[i].x, circles[i].y, circles[i].r*2, circles[i].r*2);
                */

                Circle cir = c.GetComponent<Circle>();
                // Change Color
                c.GetComponent<SpriteRenderer>().color = new Color(cir.GetDecimalR(), cir.GetDecimalG(), cir.GetDecimalB(), cir.GetDecimalA());
                // Change Size
                c.GetComponent<Transform>().localScale = new Vector3(cir.GetRadius() * 2, cir.GetRadius() * 2, 1f);
                // Instantiate Position
                GameObject x = Instantiate(c, new Vector2(cir.GetX(), cir.GetY()), Quaternion.identity);
                x.transform.SetParent(GameObject.FindGameObjectWithTag("CircleArt").transform, false);
                //Debug.Log("Circle: " + cir.GetDecimalR() + " " + cir.GetDecimalG() + " " + cir.GetDecimalB() + " " + cir.GetDecimalA());
            }

        }
    }

}
