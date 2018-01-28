using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AUDIO{
	JUMP

}

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

	public static AudioController instance;
	private AudioSource _audioSource;

	[SerializeField]
	private AudioClip[] _audioClips;

	public void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != null)
		{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
		_audioSource = GetComponent<AudioSource> ();
	}


	public void PLAY(AUDIO action) {
		//_audioSource.clip = _audioClips [(int)action];
		_audioSource.PlayOneShot(_audioClips [(int)action]);
	}
}
