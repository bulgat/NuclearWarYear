using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMoveDeflectors : ViewSendAnimObj
{
    public Vector3 SendBomberAndWingState(
        Vector3 bomberObject,
        int Speed,
        Transform transform,
        float animationTimeProcess,
        List<GameObject> TownList,
        TargetModel buildingCentralModel,
        bool returnBomber,
        Vector3 targetBomber)
    {

        float step = Speed * Time.deltaTime; // calculate distance to move
        if (bomberObject != null)
        {
            if (returnBomber)
            {
                targetBomber = transform.position;
            }

            bomberObject = Vector3.MoveTowards(bomberObject, targetBomber, step);

            Vector2 direction = (Vector2)targetBomber - (Vector2)bomberObject;
            direction.Normalize();
            

        }
        return bomberObject;
    }
}
