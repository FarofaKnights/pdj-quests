using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPoint : MonoBehaviour {
    public GameObject indicador;
    public QuestInfo questInfo;
    bool playerNear = false;

    QuestState questState;

    void Start() {
        indicador.SetActive(false);
        QuestManager.instance.OnQuestStateChanged += OnQuestStateChanged;
    }

    // Mantem questState sempre atualizado com o estado da quest do ponto
    void OnQuestStateChanged(Quest quest) {
        if (quest.info.questId == questInfo.questId) {
            questState = quest.state;

            // Mostra um efeito para o jogador saber que pode interagir com o ponto
            if (questState == QuestState.CAN_START || questState == QuestState.CAN_FINISH) {
                indicador.SetActive(true);
            } else {
                indicador.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            playerNear = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            playerNear = false;
        }
    }

    void Update() {
        if (playerNear && Input.GetKeyDown(KeyCode.E)) {
            Interact();
        }
    }

    void Interact() {
        if (!playerNear) return;

        if (questState == QuestState.CAN_START) {
            string pergunta = questInfo.introductionText;

            UIController.instance.ShowChoice(pergunta, () => {
                QuestManager.instance.StartQuest(questInfo.questId);
            }, () => {});

        } else if (questState == QuestState.CAN_FINISH) {
            UIController.instance.ShowText(questInfo.finishedText);
            QuestManager.instance.FinishQuest(questInfo.questId);

        } else if (questState == QuestState.IN_PROGRESS) {
            UIController.instance.ShowText(questInfo.questText);
        }
    }
}
