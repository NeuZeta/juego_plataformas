using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public static AudioController instance;

    public AudioSource audioSource;
    public AudioClip click, carCrash, cheer;

	
	void Awake () {
        MakeSingleton();
        audioSource = GetComponent<AudioSource>();
    }
	
	void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }






}//AudioController
