using UnityEngine;

public class Warning : MonoBehaviour
{
    
    void Start()
    {

        AudioSource warningSound = GetComponent<AudioSource>();
        warningSound.PlayOneShot(warningSound.clip, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }


}
