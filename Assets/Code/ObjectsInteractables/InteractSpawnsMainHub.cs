using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class InteractSpawnsMainHub : MonoBehaviour, IInteractable
{
    public Transform player;
    public Transform destination;

    public void Interact()
    {
        player.position = destination.position;
    }
}
