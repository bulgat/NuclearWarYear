using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCentral : MonoBehaviour
{
    public GameObject Propaganda;
    public GameObject Building;
    public GameObject DefenceObject;
    public GameObject MissleObject;
    public GameObject MissleOpenObject;

    public GameObject BomberObjectPrefabs;
    public GameObject BomberObject;
    public GameObject DefectorsObject;
    public GameObject UfoObject;
   // private GameObject UFOObject;
    public GameObject BabyObject;
    public GameObject RocketRichObject;
    public GameObject CrazyCowObject;

    public GameObject WingMisslePrefabs;
    public GameObject DefectorsPrefabs;
    public GameObject UfoPrefabs;
   // public GameObject UFOPrefabs;
    public GameObject BabyPrefabs;
    public GameObject RocketRichPrefabs;
    public GameObject CrazyCowPrefabs;

    public GameObject WingMissle;
    public GameObject Airport;
    public GameObject AirportAttack;


    private Dictionary<string, bool> VisibleObjList;

    private int Speed = 5;

    bool SetAnglePosition;

    bool _animationProcess;
    float _animationTimeProcess;
    BuildingCentralModel buildingCentralModel;
    List<GameObject> TownList;
    private bool BomberReturn = false;
    public BuildingCentral()
    {

    }

    public void Awake()
    {
        this.buildingCentralModel = new BuildingCentralModel();
        VisibleObjList = new Dictionary<string, bool>();
        VisibleObjList.Add("Bomber", false);
        VisibleObjList.Add("AttackBomber", false);
        VisibleObjList.Add("AttackMissle", false);
        VisibleObjList.Add("Defectors", false);
        VisibleObjList.Add("Ufo", false);
        VisibleObjList.Add("Baby", false);
        VisibleObjList.Add("RocketRich", false);
        VisibleObjList.Add("CrazyCow", false);
    }
    public void SetTargetBomber(CityModel target)
    {
        // Vector3
        this.buildingCentralModel.SetTargetBomber(target);


    }
    public void VisibleBuilding(CommandLider commandLider)
    {
       

        Propaganda.SetActive(commandLider.VisibleEventList["Prop"]);
        Building.SetActive(commandLider.VisibleEventList["Build"]);
        DefenceObject.SetActive(commandLider.VisibleEventList["Defence"]);
        MissleObject.SetActive(commandLider.VisibleEventList["Missle"]);
        Airport.SetActive(commandLider.VisibleEventList["Airport"]);
        AirportAttack.SetActive(commandLider.VisibleEventList["AttackAirport"]);
        

        if (WingMissle != null)
        {


            WingMissle.SetActive(commandLider.VisibleEventList["AttackMissle"]);
            MissleOpenObject.SetActive(commandLider.VisibleEventList["AttackMissle"]);
        }

        //bomber
        if (BomberObject != null)
        {
            if (commandLider.GetVisibleBomber() || commandLider.GetVisibleAttackBomber())
            {
                bool vis = true;
                BomberObject.SetActive(vis);
                //_visibleBomber = vis;
                VisibleObjList["Bomber"] = vis;
            }

        }
        if (DefectorsObject!=null) { 
            DefectorsObject.SetActive(commandLider.VisibleEventList["Defectors"]);
        }
        if (UfoObject != null)
        {
            UfoObject.SetActive(commandLider.VisibleEventList["Ufo"]);
        }
        VisibleObjList["AttackBomber"] = commandLider.GetVisibleAttackBomber();
        VisibleObjList["AttackMissle"] = commandLider.VisibleEventList["AttackMissle"];
        VisibleObjList["Defectors"] = commandLider.VisibleEventList["Defectors"];
        VisibleObjList["Ufo"] = commandLider.VisibleEventList["Ufo"];
        VisibleObjList["Baby"] = commandLider.VisibleEventList["Baby"];
        VisibleObjList["RocketRich"] = commandLider.VisibleEventList["RocketRich"];
        VisibleObjList["CrazyCow"] = commandLider.VisibleEventList["CrazyCow"];

        //var allImage_ar = UFOObject.transform.GetComponentsInChildren<GameObject>();
        //Debug.Log("  000 x =="+ allImage_ar.Length);
        //UFOObject
    }

    void Update()
    {
        if (_animationProcess)
        {
            if (VisibleObjList["Bomber"])
            {


                if (VisibleObjList["AttackBomber"] == false)
                {

                   // new ViewCircleBomber().SendBomberAndWingState(BomberObject, SetAnglePosition,
                    //    Speed, gameObject.transform, _animationTimeProcess, TownList, buildingCentralModel);
                   
                    if (BomberObject != null)
                    {
                        if (SetAnglePosition == false)
                        {
                            BomberObject.transform.position = new Vector3(Propaganda.transform.position.x, Propaganda.transform.position.y + 1, 0);

                            BomberObject.transform.Rotate(new Vector3(0, 0, 90));
                            SetAnglePosition = true;
                        }

                        Vector3 pivot = new Vector3(transform.position.x, transform.position.y, 0);
                        float speed = 100.0f;


                        // To rotate around the world's up axis
                        BomberObject.transform.RotateAround(pivot, Vector3.forward, speed * Time.deltaTime);
                    }
                    


                }
                else
                {
                    //new ViewSendAnimObj().SendBomberAndWing(BomberObject,true,true,false, false,
                      //  Speed, transform, _animationTimeProcess,  TownList,  buildingCentralModel);

                    new ViewAttackBomber ().SendBomberAndWingState(BomberObject, 
                        Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);
                }
            }
            if (VisibleObjList["AttackMissle"])
            {

                //new ViewSendAnimObj().SendBomberAndWing(WingMissle,false,true, false, false,
                    //Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);

                new ViewAttackMissle().SendBomberAndWingState(WingMissle,
                        Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);
            }
            if (VisibleObjList["Defectors"])
            {
               // new ViewSendAnimObj().SendBomberAndWing(DefectorsObject, false,false, false, false,
                  //  Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);

                new ViewMoveDeflectors().SendBomberAndWingState(DefectorsObject,
                        Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);
            }
            if (VisibleObjList["Ufo"])
            {
               // new ViewSendAnimObj().SendBomberAndWing(UfoObject, false, false, false, false,
                 //   Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);

                new ViewMoveDeflectors().SendBomberAndWingState(UfoObject,
                        Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);
            }
            if (VisibleObjList["Baby"])
            {
                //new ViewSendAnimObj().SendBomberAndWing(BabyObject, false, false, false, false,
                   // Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);

                new ViewMoveDeflectors().SendBomberAndWingState(BabyObject,
                        Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);
            }
            if (VisibleObjList["RocketRich"])
            {
                //new ViewSendAnimObj().SendBomberAndWing(RocketRichObject, false, false,true, false,
                 //   Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);

                new ViewRocketRich().SendBomberAndWingState(RocketRichObject, 
                    Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);
            }
            if (VisibleObjList["CrazyCow"])
            {
                //new ViewSendAnimObj().SendBomberAndWing(CrazyCowObject, false, true, false,true,
                //    Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);

                new ViewCrazyCow().SendBomberAndWingState(CrazyCowObject, 
                    Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);
            }
        }
    }
    public void ViewStartStateObject(List<GameObject> townList, float TimeDelete, CountryLider lider)
    {
        this._animationProcess = true;
        this._animationTimeProcess = Time.time;
        SetAnglePosition = false;
        
        this.TownList = townList;
        CreateObject();
        DestroyObject(TimeDelete);
        
        
    }
    private void CreateObject()
    {
        WingMissle = Instantiate(WingMisslePrefabs, Propaganda.transform.position, Quaternion.identity);
        BomberObject = Instantiate(BomberObjectPrefabs, Propaganda.transform.position, Quaternion.identity);
        DefectorsObject = Instantiate(DefectorsPrefabs, Propaganda.transform.position, Quaternion.identity);
        UfoObject = Instantiate(UfoPrefabs, Propaganda.transform.position+new Vector3(0,4,24), Quaternion.identity);
        BabyObject = Instantiate(BabyPrefabs, Propaganda.transform.position + new Vector3(0, 5, 24), Quaternion.identity);
        RocketRichObject = Instantiate(RocketRichPrefabs, Propaganda.transform.position, Quaternion.identity);
        CrazyCowObject = Instantiate(CrazyCowPrefabs, Propaganda.transform.position, Quaternion.identity);
       // UFOObject = Instantiate(UFOPrefabs, Propaganda.transform.position + new Vector3(0, 5, 24), Quaternion.identity);
    }
    private void DestroyObject(float TimeDelete)
    {
        Destroy(WingMissle, TimeDelete);
        Destroy(BomberObject, TimeDelete);
        Destroy(DefectorsObject, TimeDelete);
        Destroy(UfoObject, TimeDelete);
        Destroy(BabyObject, TimeDelete);
        Destroy(RocketRichObject, TimeDelete);
        Destroy(CrazyCowObject, TimeDelete);

        WingMissle.SetActive(false);
        BomberObject.SetActive(false);
        DefectorsObject.SetActive(false);
        UfoObject.SetActive(false);
        BabyObject.SetActive(false);
        RocketRichObject.SetActive(false);
        CrazyCowObject.SetActive(false);


    }
  
}
