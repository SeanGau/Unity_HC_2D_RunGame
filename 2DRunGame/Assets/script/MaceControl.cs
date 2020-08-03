using UnityEngine;

public class MaceControl : MonoBehaviour
{
    public float movingDistance = 5;
    public float moving_speed = 0.8f;
    bool dir = true; //true -> ; false <-
    Vector3 orig_pos;
    Renderer m_Renderer;
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        orig_pos = transform.position;
        transform.position += new Vector3(movingDistance, 0, 0);
    }
    
    void FixedUpdate()
    {

        if (m_Renderer.isVisible)
        {
            if (dir)
            {
                transform.position += new Vector3(moving_speed / 20, 0, 0);
            }
            else
            {
                transform.position += new Vector3(-1 * moving_speed / 20, 0, 0);
            }

            if (transform.position.x >= orig_pos.x + movingDistance)
            {
                dir = false;
            }
            else if (transform.position.x <= orig_pos.x - movingDistance)
            {
                dir = true;
            }
        }
    }
}
