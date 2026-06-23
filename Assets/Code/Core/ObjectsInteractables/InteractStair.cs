using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class InteractStair : MonoBehaviour, IInteractable
{
    public Transform player;
    public Transform destination;
    public CameraFade fade;

    public void Interact()
    {
        StartCoroutine(DoTransition());
    }

    private IEnumerator DoTransition()
    {
        yield return fade.FadeOut();
        player.position = destination.position;
        yield return new WaitForSeconds(0.5f);
        yield return fade.FadeIn();
    }
}
