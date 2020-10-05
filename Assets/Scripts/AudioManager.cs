using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    [SerializeField] private AudioClip music;
    [SerializeField] private SFX[] sfxs;

    private AudioSource audioSource;
    private Dictionary<string, SFX> sfxMap = new Dictionary<string, SFX>();

    private void Awake(){
        if(instance == null){
            instance = this;
        }
        else if(instance != this){
            Destroy(this.gameObject);
        }
        //Want this to persist throughout the game
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = music;
        audioSource.Play();

        foreach(SFX sfx in sfxs){
            if(!sfxMap.ContainsKey(sfx.name)){
                sfxMap.Add(sfx.name, sfx);
            }
        }
    }

    public void Play(string key){
        if(sfxMap.ContainsKey(key)){
            SFX sfx = sfxMap[key];
            audioSource.PlayOneShot(sfx.clip, sfx.volume);
        }
    }

}

[System.Serializable]
public class SFX {
    public string name;
    [Range(0.0f, 1.0f)]
    public float volume;
    public AudioClip clip;
}