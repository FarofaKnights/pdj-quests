using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaStep : QuestStep {
    public int mesa;

    void Start() {
        GameManager.instance.OnMesaArrive += MesaVisitada;
    }

    void OnDestroy() {
        GameManager.instance.OnMesaArrive -= MesaVisitada;
    }

    protected void MesaVisitada(int mesa) {
        if (mesa == this.mesa)
            FinishStep();
    }
}
