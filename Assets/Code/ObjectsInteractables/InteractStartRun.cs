using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractStartRun : MonoBehaviour, IInteractable
{
    public CameraFade fade;
    public void Interact()
    {
        StartCoroutine(DoTransition());

        Debug.Log("Aqui se empieza la run");
    }

    private IEnumerator DoTransition()
    {
        yield return fade.FadeOut();
        SceneManager.LoadScene("Gameplay_Testing");
    }
}
