using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance = null;
	public AudioSource soundEffectAudio;
	[Header("Global Sounds")]
	public AudioClip menuJingle;
	public AudioClip gameJingle;
	public AudioClip buttonSound;
	public AudioClip gameOver;
	[Header("Game Sounds")]
	public AudioClip paintDrop;
	public AudioClip paintSplash;
	public AudioClip swapBuckets;

	void Start()
	{
		if (Instance == null)
		{
			Instance = this;    // makes sure this is the only SoundManager
		}
		else if (Instance != this)
		{
			Destroy(gameObject);    // if there are others, destroy them
		}

		AudioSource[] sources = GetComponents<AudioSource>();
		foreach (AudioSource source in sources)
		{
			if (source.clip == null)
			{
				soundEffectAudio = source;
			}
		}

		DontDestroyOnLoad(gameObject.transform);
	}

	public void PlayOneShot(AudioClip clip)
	{   // plays the audio clip
		soundEffectAudio.PlayOneShot(clip);
	}
}
