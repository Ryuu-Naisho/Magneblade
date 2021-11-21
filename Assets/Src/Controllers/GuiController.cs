using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hintTMP;
    [SerializeField] private TextMeshProUGUI ammoTMP;
    [SerializeField] private TextMeshProUGUI powerModuleTMP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetHint(string hint)
    {
        string preface = "COM:: ";
        hintTMP.text = string.Concat(preface,hint);
    }


    public void clearHint()
    {
        hintTMP.text = "";
    }


    public void writeHUD(int ammo, int powerModule)
    {
        string ammoString = String.Format("Ammo {0}/6", ammo.ToString());
        string powerModuleString = String.Format("Power Module {0}/6", powerModule.ToString());
        ammoTMP.text = ammoString;
        powerModuleTMP.text = powerModuleString;
    }
}
