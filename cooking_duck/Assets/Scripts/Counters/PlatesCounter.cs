using System;
using UnityEngine;

public class PlateCounter : BaseCounter
{
  public event EventHandler OnPlateSpawned;
  public event EventHandler OnPlateRemoved;

  [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

  private float swpanPlateTimer;
  private float swpanPlateTimerMax = 4f;
  private int plateSpawneAmount;
  private int plateSpawneAmountMax = 4;

  private void Update()
  {
    swpanPlateTimer += Time.deltaTime;

    if (swpanPlateTimer > swpanPlateTimerMax)
    {
      swpanPlateTimer = 0f;

      if (plateSpawneAmount < plateSpawneAmountMax)
      {
        plateSpawneAmount++;
        OnPlateSpawned?.Invoke(this, EventArgs.Empty);
      }
    }
  }

  public override void Interact(Player player)
  {
    if (!player.HasKitchenObject())
    {
      // Player is empty-handed
      if (plateSpawneAmount > 0)
      {
        plateSpawneAmount--;

        KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

        OnPlateRemoved?.Invoke(this, EventArgs.Empty);
      }
    }
  }
}
