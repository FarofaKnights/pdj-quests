using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController instance;

    public Text coinText, textText, xpText;
    public GameObject choicePanel;

    System.Action onYes, onNo;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        SetCoinText(GameManager.instance.GetCoins());
        SetXPText(GameManager.instance.GetXP());
        GameManager.instance.OnCoinsChanged += SetCoinText;
        GameManager.instance.OnXPChanged += SetXPText;

        choicePanel.SetActive(false);
        textText.text = "";
    }

    public void SetCoinText(int coins) {
        coinText.text = "Moedas: " + coins;
    }

    public void SetXPText(int xp) {
        xpText.text = "XP: " + xp;
    }

    public void ShowText(string text) {
        choicePanel.SetActive(false);
        textText.text = text;

        StartCoroutine(HideText());
    }

    IEnumerator HideText() {
        yield return new WaitForSeconds(3f);
        textText.text = "";
    }

    public void ShowChoice(string text, System.Action onYes, System.Action onNo) {
        choicePanel.SetActive(true);
        textText.text = text;

        this.onYes = onYes;
        this.onNo = onNo;
    }

    public void HandleYes() {
        textText.text = "";
        choicePanel.SetActive(false);
        onYes();
    }

    public void HandleNo() {
        textText.text = "";
        choicePanel.SetActive(false);
        onNo();
    }
}
