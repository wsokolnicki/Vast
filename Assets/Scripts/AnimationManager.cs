using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 0649

public class AnimationManager : MonoBehaviour
{
    [SerializeField] float animationSpeed;

    private void Update()
    {
        ChangeAnimationSpeed();
    }

    void ChangeAnimationSpeed()
    {
        GetComponent<Animator>().SetFloat("animationSpeed", animationSpeed);
    }
}

#pragma warning restore 0649
