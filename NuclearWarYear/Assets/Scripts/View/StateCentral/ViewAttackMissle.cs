using Assets.Scripts.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewAttackMissle : ViewSendAnimObj
{
    public void SendBomberAndWingState(GameObject bomberObject,
        int Speed, Transform transform, float animationTimeProcess, List<GameObject> TownList, TargetModel buildingCentralModel)
    {

        float step = Speed * Time.deltaTime; // calculate distance to move
        if (bomberObject != null)
        {

            bool returnBomber = false;
            float offset = 260f;
            GameObject cityTown = new SearchTownObject().GetTownViewWithId(buildingCentralModel.GetTargetBomber(), TownList);
            City city = cityTown.GetComponent<City>();
            Vector3 targetBomber = cityTown.transform.position;
           
           

            if (returnBomber)
            {
                targetBomber = transform.position;
            }

            bomberObject.transform.position = Vector3.MoveTowards(bomberObject.transform.position, targetBomber, step);

            //Vector2 direction = (Vector2)targetBomber - (Vector2)transform.position;
            Vector2 direction = (Vector2)targetBomber - (Vector2)bomberObject.transform.position;
            direction.Normalize();
            
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                bomberObject.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));

                

                //ExplodeTown
                float dist = Vector3.Distance(targetBomber, bomberObject.transform.position);
                if (dist < 1.5f)
                {
                    //draw explode
                    city.SetVisibleExplode(true);

                    // return bomber
                }
            

        }
    }
}
