using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectStep : QuestStep {
    public int coinsToCollect = 10;
    int colletedCoins = 0;

    void Start() {
        GameManager.instance.OnCoinsCollected += CollectCoins;
    }

    void OnDestroy() {
        GameManager.instance.OnCoinsCollected -= CollectCoins;
    }

    protected void CollectCoins(int coins) {
        colletedCoins += coins;

        if (colletedCoins >= coinsToCollect) {
            FinishStep();
        }
    }
}
