using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCameraFollow : MonoBehaviour
{
    public Vector3 Offset;
    public Transform target;
    
    private void Update()
    {
        transform.position = target.position + Offset;
    }
}
