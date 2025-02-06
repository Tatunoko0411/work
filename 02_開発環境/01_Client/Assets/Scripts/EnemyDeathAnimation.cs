using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSorce;
    [SerializeField] AudioClip deathSE;

    void Start()
    {
        audioSorce = GetComponent<AudioSource>();
        audioSorce.PlayOneShot(deathSE);
    }
    public void OncompleteAnimation()
    {

        Destroy(gameObject);
    }
}
