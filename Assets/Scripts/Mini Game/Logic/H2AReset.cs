using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class H2AReset : Interactive
{
    private Transform gearSprite;

    private void Awake()
    {
        gearSprite = transform.GetChild(0);
    }

    public override void EmptyClicked()
    {
        // ÷ÿ÷√”Œœ∑
        GameController.Instance.Resetgame();
        gearSprite.DOPunchRotation(Vector3.forward*180,1,1,0);
    }
}
