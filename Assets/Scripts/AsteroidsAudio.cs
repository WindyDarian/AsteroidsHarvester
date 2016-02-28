using UnityEngine;
using System.Collections;

public class AsteroidsAudio{

    public static AudioSource playClipAt(AudioClip clip, Vector3 pos)
    {
        var tempGO = new GameObject("TempAudio"); 
        tempGO.transform.position = pos;
        AudioSource aSource = (AudioSource)tempGO.AddComponent(typeof(AudioSource)); 
        aSource.clip = clip; 
        aSource.Play(); 
        GameObject.Destroy(tempGO, clip.length); 
        return aSource; 
    }
	
}
