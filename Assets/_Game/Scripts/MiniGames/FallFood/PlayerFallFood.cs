using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallFood : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FallFoodController.Instance.GameOver)
        {
            return;
        }

        PrefabFallFood p = collision.GetComponent<PrefabFallFood>();

        if (p == null)
            return;

        FallFoodController.Instance.CollideItem(p);
    }
}
