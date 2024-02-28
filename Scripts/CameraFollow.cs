using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Start is called before the first frame update
    private void Start()
    {
        transform.position = playerTransform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTransform != null)
        {
            var clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);
            var clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY);

            // transform.position = Vector2.Lerp(transform.position, playerTransform.position, speed);
            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed);
        }
    }
}