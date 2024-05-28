using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    int moedas = 0;
    int xp = 0;
    
    // Eventos
    public Action<int> OnCoinsChanged, OnCoinsCollected, OnXPChanged;
    public Action<int> OnMesaArrive;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        if (OnCoinsChanged != null)
            OnCoinsChanged(moedas);
    }

    public void AddCoins(int i = 1) {
        moedas += i;

        if (OnCoinsChanged != null)
            OnCoinsChanged(moedas);

        if (OnCoinsCollected != null)
            OnCoinsCollected(i);
    }

    public int GetCoins() {
        return moedas;
    }

    public void AddXP(int i = 1) {
        xp += i;

        if (OnXPChanged != null)
            OnXPChanged(xp);
    }

    public int GetXP() {
        return xp;
    }

    public void HandleMesaVisited(int mesa) {
        if (OnMesaArrive != null)
            OnMesaArrive(mesa);
    }
}
