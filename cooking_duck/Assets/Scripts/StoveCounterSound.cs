using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
  private AudioSource audioSource;
  [SerializeField] private StoveCounter stoveCounter;

  private void Awake()
  {
    audioSource = GetComponent<AudioSource>();
  }

  private void Start()
  {
    stoveCounter.OnStateChanged += StoveCounter_OnStateChange;
  }

  private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangedEventArgs e)
  {
    bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

    if (playSound)
    {
      audioSource.Play();
    }
    else
    {
      audioSource.Pause();
    }
  }
}
