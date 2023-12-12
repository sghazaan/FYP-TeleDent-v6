using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipScript : MonoBehaviour
{
    public Text helptext;
    public Text tooltipText;
    public string helpString;
    public string tooltipString;
    // Start is called before the first frame update
    void Start()
    {
        helptext = GameObject.Find("HelpText").GetComponent<Text>();
        tooltipText = GameObject.Find("ToolTipText").GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnVREnter()
    {
        helptext.text = helpString;
        tooltipText.text = tooltipString;
    }

     void OnVRExit()
    {
        helptext.text = "";
        tooltipText.text = "";
    }

    
}
