using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRocketRich : ViewSendAnimObj
{
    public void SendBomberAndWingState(GameObject bomberObject, 
          
        int Speed, Transform transform, float animationTimeProcess, List<GameObject> TownList, BuildingCentralModel buildingCentralModel)
    {

        float step = Speed * Time.deltaTime; // calculate distance to move
        if (bomberObject != null)
        {

            GameObject cityTown = GetTownViewWithId(buildingCentralModel.GetTargetBomber(), TownList);
            City city = cityTown.GetComponent<City>();
            Vector3 targetBomber = cityTown.transform.position;
            
             targetBomber = new Vector2(0, 6);
            


            bomberObject.transform.position = Vector3.MoveTowards(bomberObject.transform.position, targetBomber, step);

            Vector2 direction = (Vector2)targetBomber - (Vector2)bomberObject.transform.position;
            direction.Normalize();
            

        }
    }
}
