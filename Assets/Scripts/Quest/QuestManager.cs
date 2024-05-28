using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestManager : MonoBehaviour {
    public static QuestManager instance;

    // Armazena todas as quests
    public Dictionary<string, Quest> quests = new Dictionary<string, Quest>();

    // Eventos chamados quando o estado de uma quest muda
    public Action<string> OnQuestStart, OnQuestAdvance, OnQuestFinish;
    public Action<Quest> OnQuestStateChanged;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        quests = LoadQuests();

        foreach (Quest quest in quests.Values) {
            if (OnQuestStateChanged != null) {
                OnQuestStateChanged(quest);
            }
        }
    }

    void FixedUpdate() {
        foreach (Quest quest in quests.Values) {
            if (quest.state == QuestState.WAITING_REQUIREMENTS && CheckRequirements(quest)) {
                ChangeQuestState(quest.info.questId, QuestState.CAN_START);
            }
        }
    }

    public void StartQuest(string questId) {
        Quest quest = GetQuestById(questId);

        GameObject stepGO = quest.InstantiateStep(transform);
        stepGO.GetComponent<QuestStep>().Initialize(questId);

        ChangeQuestState(questId, QuestState.IN_PROGRESS);
    }

    public void AdvanceQuest(string questId) {
        Quest quest = GetQuestById(questId);
        
        if (quest.NextStep()) {
            GameObject stepGO = quest.InstantiateStep(transform);
            stepGO.GetComponent<QuestStep>().Initialize(questId);

            if (OnQuestAdvance != null) {
                OnQuestAdvance(questId);
            }
        } else {
            ChangeQuestState(questId, QuestState.CAN_FINISH);
        }
    }

    public void FinishQuest(string questId) {
        Quest quest = GetQuestById(questId);
        ClaimReward(quest);
        ChangeQuestState(questId, QuestState.FINISHED);
    }

    void ClaimReward(Quest quest) {
        GameManager.instance.AddXP(quest.info.xpReward);
        GameManager.instance.AddCoins(quest.info.goldReward);
    }

    void ChangeQuestState(string questId, QuestState state) {
        Quest quest = GetQuestById(questId);
        quest.state = state;

        if (OnQuestStateChanged != null) {
            OnQuestStateChanged(quest);
        }
    }

    bool CheckRequirements(Quest quest) {
        // Verifica se o jogador tem os requisitos para começar a quest
        return GameManager.instance.GetXP() >= quest.info.xpRequirement;
    }

    // Carrega quests e salva em um dicionário
    Dictionary<string, Quest> LoadQuests() {
        Dictionary<string, Quest> quests = new Dictionary<string, Quest>();

        // Carrega todos QuestInfo do [Resources/Quests]
        QuestInfo[] questInfos = Resources.LoadAll<QuestInfo>("Quests");

        foreach (QuestInfo questInfo in questInfos) {
            Quest quest = new Quest(questInfo);
            quests.Add(questInfo.questId, quest);
        }

        return quests;
    }

    // Pega uma quest pelo ID
    Quest GetQuestById(string questId) {
        if (quests.ContainsKey(questId)) {
            return quests[questId];
        }

        return null;
    }
}
