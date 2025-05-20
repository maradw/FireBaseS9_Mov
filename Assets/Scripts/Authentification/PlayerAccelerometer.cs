using UnityEngine;

public class PlayerAccelerometer : MonoBehaviour
{
    [SerializeField] private float speedM = 10f;

    void Update()
    {
        float moveInput = Input.acceleration.x;
        float movement = moveInput * speedM;
        transform.Translate(Vector3.right * movement * Time.deltaTime);
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        Vector3 pos = transform.position;
        if (pos.x > rightEdge.x)
        {
            pos.x = leftEdge.x;
        }
        else if (pos.x < leftEdge.x)
        {
            pos.x = rightEdge.x;
        }

        transform.position = pos;
    }

}
