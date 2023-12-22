using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * bulletSpeed * Time.deltaTime;
        if (transform.position.x > 18.5) Destroy(gameObject);
    }
}
