using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo", menuName = "Quest/QuestInfo")]
public class QuestInfo : ScriptableObject {
    [Header("Quest Info")]
    public string questId;
    
    public string introductionText;
    public string questText;
    public string finishedText;

    [Header("Quest Requirements")]
    public int xpRequirement;

    [Header("Quest Steps")]
    public GameObject[] steps;

    [Header("Quest Rewards")]
    public int xpReward;
    public int goldReward;

    void OnValidate() {
        // Define o ID da quest como o nome do objeto de forma automatica
        #if UNITY_EDITOR
        questId = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
