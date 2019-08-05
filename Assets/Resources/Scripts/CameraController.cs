using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Vector3 offset;

    void Start()
    {
        player = GameObject.Find("Player");
        offset = transform.position;
    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
