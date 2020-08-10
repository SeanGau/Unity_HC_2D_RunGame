using UnityEngine;

public class MaceControl : MonoBehaviour
{
    public float movingDistance = 5;
    public float moving_speed = 0.8f;
    public bool verticle = false;
    bool dir = true; //true -> ; false <-
    Vector3 orig_pos;
    Renderer m_Renderer;
    int verticle_val;
    void Start()
    {
        verticle_val = verticle ? 0 : 1;
        m_Renderer = GetComponent<Renderer>();
        orig_pos = transform.position;
        transform.position += new Vector3(movingDistance*verticle_val, movingDistance * (1-verticle_val), 0);
    }
    
    void FixedUpdate()
    {

        if (m_Renderer.isVisible)
        {
            if (dir)
            {
                transform.position += new Vector3(moving_speed * verticle_val / 20, moving_speed * (1 - verticle_val) / 20, 0);
            }
            else
            {
                transform.position += new Vector3(-1 * moving_speed * verticle_val / 20, -1 * moving_speed * (1 - verticle_val) / 20, 0);
            }

            if (transform.position.x >= orig_pos.x + movingDistance || transform.position.y >= orig_pos.y + movingDistance)
            {
                dir = false;
            }
            else if (transform.position.x <= orig_pos.x - movingDistance || transform.position.y <= orig_pos.y - movingDistance)
            {
                dir = true;
            }
        }
    }
}
