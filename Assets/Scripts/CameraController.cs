using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    [SerializeField] private float offsetZ;

    [SerializeField] private Transform player;

    private void Update()
    {
        transform.position = player.transform.position + new Vector3(offsetX, offsetY, -offsetZ);
    }

}
