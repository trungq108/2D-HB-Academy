using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;
    [SerializeField] Vector3 offset;
    [SerializeField] float speed;

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, Time.deltaTime * speed);
    }
}
