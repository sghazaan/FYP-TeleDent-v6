using UnityEngine;
using UnityEngine.SceneManagement; // Include this namespace

public class SceneChanger : MonoBehaviour
{
    // This method will be called when the button is clicked
    public void CS_CavityPrep()
    {
        SceneManager.LoadScene("FixedCameraScene");
    }
    public void CS_Scaling()
    {
        SceneManager.LoadScene("ScalingScene");
    }
    public void CS_Extraction()
    {
        SceneManager.LoadScene("ExtractionScene");
    }
}
