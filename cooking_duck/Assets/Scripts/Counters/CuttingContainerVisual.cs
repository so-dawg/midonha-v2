using UnityEngine;

public class CuttingContainerVisual : MonoBehaviour
{
  private const string cut = "Cut";

  [SerializeField] private CuttingCounter cuttingCounter;

  private Animator animator;

  private void Awake()
  {
    animator = GetComponent<Animator>();
  }

  public void Start()
  {
    cuttingCounter.OnCut += CuttingCounter_OnCut;
  }

  private void CuttingCounter_OnCut(object sender, System.EventArgs e)
  {
    animator.SetTrigger(cut);
  }
}
