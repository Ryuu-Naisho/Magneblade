using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hintTMP;
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
}
