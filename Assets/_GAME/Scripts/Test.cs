using UnityEngine;
using MD.Swipe;
using System;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        SwipeController.OnSwiped += OnSwiped;
    }
    private void OnDestroy()
    {
        SwipeController.OnSwiped -= OnSwiped;
    }

    private void OnSwiped(SwipeDirection direction)
    {
        Debug.LogWarning("Move\n " + Enum.GetName(typeof(SwipeDirection), direction));
    }
}
