using UnityEngine.SceneManagement;
using UnityEngine;

public class Collusion : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay=2f; 
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
  

     bool isTransitioning=false;
     void Start()
     {
     audioSource=GetComponent<AudioSource>();
     }
     void Update(){
         RespondToDebugKeys();  
     }
     void RespondToDebugKeys(){
        if(Input.GetKeyDown(KeyCode.L)){
            NextLevel();
        }
     }
    void OnCollisionEnter(Collision other)
     {
    if(isTransitioning){return;}
    switch (other.gameObject.tag)
    {
        case"Friendly":
        Debug.Log("This thing is Friendly");
        break;

        case"Finish":
         StartSuccessSequence();
        break;
    
        default:
         StartCrashSequence();
        break;
    }
  }  
  void StartSuccessSequence()
  {
        isTransitioning=true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled=false;
        Invoke("NextLevel",LevelLoadDelay);
  }
    void StartCrashSequence()
    {
          isTransitioning=true;
          audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled=false;
        Invoke("ReloadLevel",LevelLoadDelay);
    }
    void ReloadLevel()
    {
        int CurrentSceneİndex=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneİndex);
    } 
    void NextLevel()
    {
        int CurrentSceneİndex=SceneManager.GetActiveScene().buildIndex;
        int NextSceneİndex=CurrentSceneİndex+1;
        SceneManager.LoadScene(NextSceneİndex);
        if (NextSceneİndex ==SceneManager.sceneCountInBuildSettings)
        {
         NextSceneİndex=0;   
        }
        SceneManager.LoadScene(NextSceneİndex);
    }
 }  
