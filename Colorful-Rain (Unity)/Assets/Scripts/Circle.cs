using UnityEngine;

public class Circle : MonoBehaviour
{
    private float x;
    private float y;
    private float radius;
    private float red;
    private float green;
    private float blue;
    private float alpha;

    public Circle(float width, float height, float minSize, float maxSize)
    {
        x = Random.Range(-width, width);
        y = Random.Range(-height, height);
        radius = Random.Range(minSize, maxSize);
    }

    public void Init(Vector2 minW_H, Vector2 maxW_H, float minSize, float maxSize)
    {
        x = Random.Range(minW_H.x, maxW_H.x);
        y = Random.Range(minW_H.y, maxW_H.y);
        radius = Random.Range(minSize, maxSize);
    }

    public float GetX() { return x; }
    public float GetY() { return y; }
    public float GetRadius() { return radius; }

    public void SetRGBA(float r, float g, float b, float a)
    {
        red = r;
        green = g;
        blue = b;
        alpha = a;
    }

    public float GetDecimalR() { return red; }
    public float GetDecimalG() { return green; }
    public float GetDecimalB() { return blue; }
    public float GetDecimalA() { return alpha; }
}
