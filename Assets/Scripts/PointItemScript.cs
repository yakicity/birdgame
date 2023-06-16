using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointItemScript : MonoBehaviour
{
    [Header("獲得によるポイント")]
    public int Point = 10;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            Destroy(this.gameObject);
        }
    }
}
