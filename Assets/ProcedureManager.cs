using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Include TextMeshPro namespace

public class ProcedureManager : MonoBehaviour
{
    public TextMeshProUGUI instructionText; // Reference to the TextMeshProUGUI component

    public GameObject decayedObject1; 
    public GameObject decayedObject2; 
    public GameObject decayedObject3; 

    public GameObject amal1; 
    public GameObject amal2; 
    public GameObject amal3; 

    public GameObject syringe;
    public GameObject drill;
    public GameObject amalgamCarrier;

    private int currentStep = 0;
    private List<string> instructions = new List<string>()
    {
        "Inject syringe in gum",
        "Drill decayed parts",
        "Place amalgam on teeth",
        "Procedure completed"
    };

    // Start is called before the first frame update
    void Start()
    {
        UpdateInstruction();
    }

    // Update is called once per frame
    void Update()
    {
        // Call these methods to check if they need to progress the current step
        CheckForInjection();
        CheckForDrilling();
        CheckForAmalgam();
    }

    // Method to move to the next step
    public void NextStep()
    {
        if (currentStep < instructions.Count - 1)
        {
            currentStep++;
            UpdateInstruction();
        }
    }

    // Method to update the instruction text
    private void UpdateInstruction()
    {
        instructionText.text = instructions[currentStep];
    }

    // Method to check for syringe injection
    private void CheckForInjection()
    {
        if (currentStep == 0) // Check if current step is for syringe injection
        {
            bool isActive = syringe.activeSelf;
            if (isActive && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                NextStep(); 
            }
        }
    }

    // Method to check for drilling
    private void CheckForDrilling()
    {
        if (currentStep == 1) // Check if current step is for drilling
        {
            if (decayedObject1 == null && decayedObject2 == null && decayedObject3 == null)
            {
                NextStep();
            }
        }
    }

    // Method to check for placing amalgam
    private void CheckForAmalgam()
    {
        if (currentStep == 2) // Check if current step is for placing amalgam
        {
            bool isActive1 = amal1.activeSelf;
            bool isActive2 = amal2.activeSelf;
            bool isActive3 = amal3.activeSelf;

            if (isActive1 == true && isActive2 == true && isActive3 == true)
            {
                NextStep(); 
            }
        }
    }
}
