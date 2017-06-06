using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2DSpriteScript : MonoBehaviour {

   // private Collider2D ColliderItem;

    // Use this for initialization
    void Start() { }
    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(Vector3.up * Time.deltaTime*300, Space.World);

        if (transform.position.y < -7f || transform.position.y > 7f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
