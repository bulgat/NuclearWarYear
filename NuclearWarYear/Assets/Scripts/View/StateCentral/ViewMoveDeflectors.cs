using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMoveDeflectors : ViewSendAnimObj
{
    public void SendBomberAndWingState(GameObject bomberObject,
        int Speed, Transform transform, float animationTimeProcess,
        List<GameObject> TownList, BuildingCentralModel buildingCentralModel, bool returnBomber, Vector3 targetBomber)
    {

        float step = Speed * Time.deltaTime; // calculate distance to move
        if (bomberObject != null)
        {
            //GameObject cityTown = GetTownViewWithId(buildingCentralModel.GetTargetBomber(), TownList);
            //City city = cityTown.GetComponent<City>();
            //Vector3 targetBomber = cityTown.transform.position;
 

            if (returnBomber)
            {
                targetBomber = transform.position;
            }

            bomberObject.transform.position = Vector3.MoveTowards(bomberObject.transform.position, targetBomber, step);

            Vector2 direction = (Vector2)targetBomber - (Vector2)bomberObject.transform.position;
            direction.Normalize();
            

        }
    }
}
