﻿using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;

public class ProgressTracker : MonoBehaviour
{
    public TextMeshProUGUI progressDescription;
    // List to store tool GameObjects
    public List<GameObject> tools = new List<GameObject>();

    // Dictionary to store interaction counts for each tool
    private Dictionary<GameObject, int> interactionCounts = new Dictionary<GameObject, int>();

    // Dictionary to store error counts for each tool
    private Dictionary<GameObject, int> errorCounts = new Dictionary<GameObject, int>();

    // Reference to the last used tool in case of error
    private GameObject lastUsedTool;

    // Score variables
    private int totalInteractions = 0;
    private int totalErrors = 0;
    private float startTime;

    void Start()
    {
        // Initialize dictionaries
        foreach (GameObject tool in tools)
        {
            interactionCounts[tool] = 0;
            errorCounts[tool] = 0;
        }
        startTime = Time.time;

    }

  

    // Method to log tool interaction
    public void LogInteraction(GameObject tool, bool isSuccess)
    {
        totalInteractions++;
        interactionCounts[tool]++;

        if (!isSuccess)
        {
            totalErrors++;
            errorCounts[tool]++;
            lastUsedTool = tool; // Update last used tool in case of error
            Debug.Log("Error with " + tool.name);
            // Implement feedback for errors, such as visual cues or messages
        }

        // Update progress metrics, such as score or completion percentage
        UpdateProgress();
    }

    // Method to get last used tool in case of error
    public GameObject GetLastUsedTool()
    {
        return lastUsedTool;
    }

    // Method to update progress metrics
    private void UpdateProgress()
    {
        // Example: Calculate completion percentage
        float completionPercentage = (totalInteractions - totalErrors) / (float)totalInteractions * 100f;
        Debug.Log("Completion Percentage: " + completionPercentage.ToString("F2") + "%");

        // You can implement more sophisticated progress tracking logic here
    }


    // Method to print progress report
public void PrintProgressReport()
{
    StringBuilder reportBuilder = new StringBuilder();

    // Calculate session duration
    float sessionDuration = Time.time - startTime;
    int minutes = Mathf.FloorToInt(sessionDuration / 60f);
    int seconds = Mathf.FloorToInt(sessionDuration % 60f);

    // Append session duration to the report
    reportBuilder.AppendLine($"Session Duration: {minutes}m {seconds}s");

    // Iterate through each tool
    for (int i = 0; i < tools.Count; i++)
    {
        GameObject tool = tools[i];
        int interactions = interactionCounts.ContainsKey(tool) ? interactionCounts[tool] : 0;
        int errors = errorCounts.ContainsKey(tool) ? errorCounts[tool] : 0;
        float accuracy = interactions > 0 ? ((float)(interactions - errors) / interactions) * 100f : 0f;

        // Append tool information to the report
        reportBuilder.AppendLine($"Tool {i + 1}:");
        reportBuilder.AppendLine($"{tool.name} interactions: {interactions}");
        reportBuilder.AppendLine($"{tool.name} errors: {errors}");
        reportBuilder.AppendLine($"{tool.name} accuracy: {accuracy:F2}%");
        // Add an empty line for readability
        reportBuilder.AppendLine();
    }

    // Print the progress report
    Debug.Log("Progress Report:\n" + reportBuilder.ToString());
    progressDescription.text = reportBuilder.ToString();
}

   
}
