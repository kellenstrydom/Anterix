using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
