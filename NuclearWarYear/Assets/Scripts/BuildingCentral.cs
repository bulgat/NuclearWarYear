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
    public GameObject BabyObject;
    public GameObject RocketRichObject;
    public GameObject CrazyCowObject;

    public GameObject WingMisslePrefabs;
    public GameObject DefectorsPrefabs;
    public GameObject UfoPrefabs;
    public GameObject BabyPrefabs;
    public GameObject RocketRichPrefabs;
    public GameObject CrazyCowPrefabs;

    public GameObject WingMissle;
    public GameObject Airport;
    public GameObject AirportAttack;
    //public GameObject MapMain;


    private Dictionary<string, bool> VisibleObjList;

    private int Speed = 5;

    bool SetAnglePosition;

    bool _animationProcess;
    float _animationTimeProcess;
    BuildingCentralModel buildingCentralModel;
    List<GameObject> TownList;
    //private bool BomberReturn = false;
    public BuildingCentral()
    {

    }

    public void Awake()
    {
        this.buildingCentralModel = new BuildingCentralModel();
        VisibleObjList = new Dictionary<string, bool>();
        VisibleObjList.Add(DictionaryEssence.TypeEvent.Bomber.ToString(), false);
        VisibleObjList.Add(DictionaryEssence.TypeEvent.AttackBomber.ToString(), false);
        VisibleObjList.Add(DictionaryEssence.TypeEvent.AttackMissle.ToString(), false);
        VisibleObjList.Add(DictionaryEssence.TypeEvent.Defectors.ToString(), false);
        VisibleObjList.Add(DictionaryEssence.TypeEvent.Ufo.ToString(), false);
        VisibleObjList.Add(DictionaryEssence.TypeEvent.Baby.ToString(), false);
        VisibleObjList.Add(DictionaryEssence.TypeEvent.RocketRich.ToString(), false);
        VisibleObjList.Add(DictionaryEssence.TypeEvent.CrazyCow.ToString(), false);
    }
    public void SetTargetBomber(CityModel target)
    {
        // Vector3
        this.buildingCentralModel.SetTargetBomber(target);


    }
    public void VisibleBuilding(CommandLider commandLider)
    {

        Propaganda.SetActive(commandLider.GetVisible(DictionaryEssence.TypeEvent.Propaganda.ToString()));
        Building.SetActive(commandLider.GetVisible(DictionaryEssence.TypeEvent.Building.ToString()));
        DefenceObject.SetActive(commandLider.GetVisible(DictionaryEssence.TypeEvent.Defence.ToString()));
        MissleObject.SetActive(commandLider.GetVisible(DictionaryEssence.TypeEvent.Missle.ToString()));
        Airport.SetActive(commandLider.GetVisible(DictionaryEssence.TypeEvent.Airport.ToString()));
        AirportAttack.SetActive(commandLider.GetVisible(DictionaryEssence.TypeEvent.AttackAirport.ToString()));

        /*
        Propaganda.SetActive(commandLider.VisibleEventList[DictionaryEssence.TypeEvent.Propaganda.ToString()]);
        Building.SetActive(commandLider.VisibleEventList[DictionaryEssence.TypeEvent.Building.ToString()]);
        DefenceObject.SetActive(commandLider.VisibleEventList[DictionaryEssence.TypeEvent.Defence.ToString()]);
        MissleObject.SetActive(commandLider.VisibleEventList[DictionaryEssence.TypeEvent.Missle.ToString()]);
        Airport.SetActive(commandLider.VisibleEventList[DictionaryEssence.TypeEvent.Airport.ToString()]);
        AirportAttack.SetActive(commandLider.VisibleEventList[DictionaryEssence.TypeEvent.AttackAirport.ToString()]);
        */

        if (WingMissle != null)
        {
            WingMissle.SetActive(commandLider.GetVisible(DictionaryEssence.TypeEvent.AttackMissle.ToString()));
            MissleOpenObject.SetActive(commandLider.GetVisible(DictionaryEssence.TypeEvent.AttackMissle.ToString()));
            /*
            WingMissle.SetActive(commandLider.VisibleEventList[DictionaryEssence.TypeEvent.AttackMissle.ToString()]);
            MissleOpenObject.SetActive(commandLider.VisibleEventList[DictionaryEssence.TypeEvent.AttackMissle.ToString()]);
            */
        }

        //bomber
        if (BomberObject != null)
        {
            if (commandLider.GetVisibleBomber() || commandLider.GetVisibleAttackBomber())
            {
                bool vis = true;
                BomberObject.SetActive(vis);
                VisibleObjList[DictionaryEssence.TypeEvent.Bomber.ToString()] = vis;
            }

        }
        if (DefectorsObject!=null) {
            DefectorsObject.SetActive(commandLider.GetVisible(DictionaryEssence.TypeEvent.Defectors.ToString()));
            //DefectorsObject.SetActive(commandLider.VisibleEventList[DictionaryEssence.TypeEvent.Defectors.ToString()]);
        }
        if (UfoObject != null)
        {
            UfoObject.SetActive(commandLider.GetVisible(DictionaryEssence.TypeEvent.Ufo.ToString()));
            //UfoObject.SetActive(commandLider.VisibleEventList[DictionaryEssence.TypeEvent.Ufo.ToString()]);
        }
        VisibleObjList[DictionaryEssence.TypeEvent.AttackBomber.ToString()] = commandLider.GetVisibleAttackBomber();


        VisibleObjList[DictionaryEssence.TypeEvent.AttackMissle.ToString()] = commandLider.GetVisible(DictionaryEssence.TypeEvent.AttackMissle.ToString());
        VisibleObjList[DictionaryEssence.TypeEvent.Defectors.ToString()] = commandLider.GetVisible(DictionaryEssence.TypeEvent.Defectors.ToString());
        VisibleObjList[DictionaryEssence.TypeEvent.Ufo.ToString()] = commandLider.GetVisible(DictionaryEssence.TypeEvent.Ufo.ToString());
        VisibleObjList[DictionaryEssence.TypeEvent.Baby.ToString()] = commandLider.GetVisible(DictionaryEssence.TypeEvent.Baby.ToString());
        VisibleObjList[DictionaryEssence.TypeEvent.RocketRich.ToString()] = commandLider.GetVisible(DictionaryEssence.TypeEvent.RocketRich.ToString());
        VisibleObjList[DictionaryEssence.TypeEvent.CrazyCow.ToString()] = commandLider.GetVisible(DictionaryEssence.TypeEvent.CrazyCow.ToString());
        /*
        VisibleObjList[DictionaryEssence.TypeEvent.AttackMissle.ToString()] = commandLider.VisibleEventList[DictionaryEssence.TypeEvent.AttackMissle.ToString()];
        VisibleObjList[DictionaryEssence.TypeEvent.Defectors.ToString()] = commandLider.VisibleEventList[DictionaryEssence.TypeEvent.Defectors.ToString()];
        VisibleObjList[DictionaryEssence.TypeEvent.Ufo.ToString()] = commandLider.VisibleEventList[DictionaryEssence.TypeEvent.Ufo.ToString()];
        VisibleObjList[DictionaryEssence.TypeEvent.Baby.ToString()] = commandLider.VisibleEventList[DictionaryEssence.TypeEvent.Baby.ToString()];
        VisibleObjList[DictionaryEssence.TypeEvent.RocketRich.ToString()] = commandLider.VisibleEventList[DictionaryEssence.TypeEvent.RocketRich.ToString()];
        VisibleObjList[DictionaryEssence.TypeEvent.CrazyCow.ToString()] = commandLider.VisibleEventList[DictionaryEssence.TypeEvent.CrazyCow.ToString()];
        */
        //UFOObject
    }

    void Update()
    {
        if (_animationProcess)
        {
            if (VisibleObjList[DictionaryEssence.TypeEvent.Bomber.ToString()])
            {


                if (VisibleObjList[DictionaryEssence.TypeEvent.AttackBomber.ToString()] == false)
                {

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
 
                    new ViewAttackBomber ().SendBomberAndWingState(BomberObject, 
                        Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);
                }
            }
            if (VisibleObjList[DictionaryEssence.TypeEvent.AttackMissle.ToString()])
            {



                new ViewAttackMissle().SendBomberAndWingState(WingMissle,
                        Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);
            }
            if (VisibleObjList[DictionaryEssence.TypeEvent.Defectors.ToString()])
            {
   

                new ViewMoveDeflectors().SendBomberAndWingState(DefectorsObject,
                        Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);
            }
            if (VisibleObjList[DictionaryEssence.TypeEvent.Ufo.ToString()])
            {
  

                new ViewMoveDeflectors().SendBomberAndWingState(UfoObject,
                        Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);
            }
            if (VisibleObjList[DictionaryEssence.TypeEvent.Baby.ToString()])
            {


                new ViewMoveDeflectors().SendBomberAndWingState(BabyObject,
                        Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);
            }
            if (VisibleObjList[DictionaryEssence.TypeEvent.RocketRich.ToString()])
            {
       

                new ViewRocketRich().SendBomberAndWingState(RocketRichObject, 
                    Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);
            }
            if (VisibleObjList[DictionaryEssence.TypeEvent.CrazyCow.ToString()])
            {


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
        WingMissle.transform.parent = transform;
        BomberObject = Instantiate(BomberObjectPrefabs, Propaganda.transform.position, Quaternion.identity);
        BomberObject.transform.parent = transform;
        DefectorsObject = Instantiate(DefectorsPrefabs, Propaganda.transform.position, Quaternion.identity);
        DefectorsObject.transform.parent = transform;
        UfoObject = Instantiate(UfoPrefabs, Propaganda.transform.position+new Vector3(0,4,24), Quaternion.identity);
        UfoObject.transform.parent = transform;
        BabyObject = Instantiate(BabyPrefabs, Propaganda.transform.position + new Vector3(0, 5, 24), Quaternion.identity);
        BabyObject.transform.parent = transform;
        RocketRichObject = Instantiate(RocketRichPrefabs, Propaganda.transform.position, Quaternion.identity);
        RocketRichObject.transform.parent = transform;
        CrazyCowObject = Instantiate(CrazyCowPrefabs, Propaganda.transform.position, Quaternion.identity);
        CrazyCowObject.transform.parent = transform;

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
