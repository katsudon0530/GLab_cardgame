using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource _bgmSource;
    private AudioSource _seSource;
    
    [SerializeField] private List<AudioData> _audioList = new List<AudioData>();
    
     private Dictionary<string, AudioClip> _audioClipDictionary = new Dictionary<string, AudioClip>();
    private void Awake()
    {
        base.Awake();
        
        _bgmSource = GetComponent<AudioSource>();
        _bgmSource.loop = true;
        _seSource = GetComponent<AudioSource>();
        
        RegisterSound();
    }

    private void RegisterSound()
    {
        foreach (var sound in _audioList)
        {
            if (!_audioClipDictionary.ContainsKey(sound.name) && sound.clip != null)
            {
                _audioClipDictionary.Add(sound.name, sound.clip);
            }
        }
    }
    
    public void PlayBgm(string name)
    {
        if (_audioClipDictionary.ContainsKey(name))
        {
            _bgmSource.clip = _audioClipDictionary[name];
            _bgmSource.Play();
        }
        else
        {
            Debug.Log($"BGM {name} not found");
        }
    }
    

    public void PlaySe(string name)
    {
        if (_audioClipDictionary.ContainsKey(name))
        {
            _seSource.PlayOneShot(_audioClipDictionary[name]);
        }
        else
        {
            Debug.Log($"SE {name} not found");
        }
    }
}
