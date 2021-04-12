using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotator : MonoBehaviour
{
    public float rotation = 45f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotation * Time.deltaTime, 0f);
    }
}

