using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static soundManager Instance;

    [SerializeField] private AudioSource SFXSource;

    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip boom;

    void Start()
    {
        Instance = this;
    }

    public void Sound_Play(string name)
    {
        switch (name)
        {
            case "shoot":
                SFXSource.PlayOneShot(shoot);
                break;

            case "boom":
                SFXSource.PlayOneShot(boom);
                break;
        }
    }
}
