using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
  private bool isFirstUpdate = true; // Ensure the loading screen has been loaded

  // Update is called once per frame
  void Update()
  {
    if (isFirstUpdate)
    {
      isFirstUpdate = false;
      Loader.LoadCallback();
    }
  }
}
