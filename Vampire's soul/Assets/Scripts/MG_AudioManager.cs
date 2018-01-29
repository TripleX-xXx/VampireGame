using System;
using UnityEngine;

public class MG_AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static MG_AudioManager instance;

    private bool sound = true;

    void Awake ()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        //Play("bacground");
    }

    public void Play (string name)
    {
        if (name != "bacground" && !sound) return;
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        s.source.Stop();
    }

    public void Sounds(bool value)
    {
        sound = value;
    }

    public void Music(bool value)
    {
        if (value) Play("bacground");
        else Stop("bacground");
    }

}
