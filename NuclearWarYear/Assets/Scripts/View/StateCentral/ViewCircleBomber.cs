using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCircleBomber : ViewSendAnimObj
{
    public void SendBomberAndWingState(GameObject bomberObject, bool SetAnglePosition,
         int Speed, Transform transform, float animationTimeProcess, List<GameObject> TownList, BuildingCentralModel buildingCentralModel)
    {

        if (bomberObject != null)
        {
            if (SetAnglePosition == false)
            {
                bomberObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);

                bomberObject.transform.Rotate(new Vector3(0, 0, 90));
                SetAnglePosition = true;
            }

            Vector3 pivot = new Vector3(transform.position.x, transform.position.y, 0);
            float speed = 100.0f;


            // To rotate around the world's up axis
            bomberObject.transform.RotateAround(pivot, Vector3.forward, speed * Time.deltaTime);
        }


    }
}
