using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            GameManager.instance.AddCoins(1);
            Destroy(gameObject);
        }
    }
}
