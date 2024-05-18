using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] Vector3 momentVector;
    [SerializeField][Range(0f, 1f)] float movementFactor;
    [SerializeField] float period = 5f;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float cycle = Time.time / period;
        float tau = Mathf.PI * 2;
        float sinWave = Mathf.Sin(tau * cycle);

        movementFactor = (sinWave + 1) / 2;
        Vector3 offset = momentVector * movementFactor;
        transform.position = startPosition + offset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
