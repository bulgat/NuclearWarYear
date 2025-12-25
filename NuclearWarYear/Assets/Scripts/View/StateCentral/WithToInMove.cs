using Assets.Scripts.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithToInMove
{
    public Vector3 SendWithToInMoveState(
        Vector3 bomberObject,
        int Speed,
        Transform transform,
        float animationTimeProcess,
        List<GameObject> TownList,
        bool returnBomber,
        TargetModel targetBomber)
    {

        float step = Speed * Time.deltaTime; // calculate distance to move

        GameObject cityTown = new SearchTownObject().GetTownViewWithId(targetBomber.GetTarget(), TownList);

        if (bomberObject != null)
        {
            //if (returnBomber)
            //{
               // targetBomber = cityTown.transform.position;
            //}

            bomberObject = Vector3.MoveTowards(bomberObject, cityTown.transform.position, step);

            Vector2 direction = (Vector2)cityTown.transform.position - (Vector2)bomberObject;
            direction.Normalize();
            

        }
        return bomberObject;
    }
}
