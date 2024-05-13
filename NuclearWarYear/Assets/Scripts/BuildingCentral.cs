using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingCentral : MonoBehaviour
{
    private GameObject Propaganda;
    private GameObject BuildingIndustry;
    private GameObject DefenceObject;
    private GameObject MissleObject;
    private GameObject MissleOpenObject;
    private GameObject Airport;
    private GameObject AirportAttack;

    public GameObject BomberObjectPrefabs;
    public GameObject BomberObject;
    public GameObject DefectorsObject;
    public GameObject UfoObject;
    //public GameObject BabyObject;
    public GameObject RocketRichObject;
    public GameObject CrazyCowObject;

    public GameObject WingMisslePrefabs;
    public GameObject DefectorsPrefabs;
    public GameObject UfoPrefabs;
    //public GameObject BabyPrefabs;
    public GameObject RocketRichPrefabs;
    public GameObject CrazyCowPrefabs;
    

    public GameObject WingMissle;

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
            
        BuildingIndustry = gameObject.transform.GetChild(0).gameObject;
        DefenceObject = gameObject.transform.GetChild(1).gameObject;
        MissleObject = gameObject.transform.GetChild(2).gameObject;
        Propaganda =gameObject.transform.GetChild(3).gameObject;
        Airport = gameObject.transform.GetChild(4).gameObject;
        AirportAttack = gameObject.transform.GetChild(5).gameObject;
        MissleOpenObject = gameObject.transform.GetChild(6).gameObject;
        
        BuildingIndustry.SetActive(false);
        DefenceObject.SetActive(false);
        MissleObject.SetActive(false);
        Propaganda.SetActive(false);
        Airport.SetActive(false);
        AirportAttack.SetActive(false);
        MissleOpenObject.SetActive(false);



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
    private bool VisibleObjectName(List<CommandLider> commandLiderList,string Name)
    {
        return commandLiderList.Any(a=>a.GetNameExecute(Name));
    }

    public void UpdateVisibleBuilding(string NameCommand)
    {

        Propaganda.SetActive(NameCommand==DictionaryEssence.TypeEvent.Propaganda.ToString());
        BuildingIndustry.SetActive(NameCommand== DictionaryEssence.TypeEvent.Building.ToString());
        DefenceObject.SetActive(NameCommand== DictionaryEssence.TypeEvent.Defence.ToString());
        MissleObject.SetActive(NameCommand==DictionaryEssence.TypeEvent.Missle.ToString());
        Airport.SetActive(NameCommand==DictionaryEssence.TypeEvent.Airport.ToString());
        AirportAttack.SetActive(NameCommand== DictionaryEssence.TypeEvent.AttackAirport.ToString());

        VisibleObjList[DictionaryEssence.TypeEvent.AttackBomber.ToString()] = NameCommand == DictionaryEssence.TypeEvent.AttackBomber.ToString();
        VisibleObjList[DictionaryEssence.TypeEvent.AttackMissle.ToString()] = NameCommand == DictionaryEssence.TypeEvent.AttackMissle.ToString();
        VisibleObjList[DictionaryEssence.TypeEvent.Defectors.ToString()] = NameCommand == DictionaryEssence.TypeEvent.Defectors.ToString();
        VisibleObjList[DictionaryEssence.TypeEvent.Ufo.ToString()] = NameCommand == DictionaryEssence.TypeEvent.Ufo.ToString();
        VisibleObjList[DictionaryEssence.TypeEvent.Baby.ToString()] = NameCommand == DictionaryEssence.TypeEvent.Baby.ToString();
        VisibleObjList[DictionaryEssence.TypeEvent.RocketRich.ToString()] = NameCommand == DictionaryEssence.TypeEvent.RocketRich.ToString();
        VisibleObjList[DictionaryEssence.TypeEvent.CrazyCow.ToString()] = NameCommand == DictionaryEssence.TypeEvent.CrazyCow.ToString();


        if (WingMissle != null)
        {
            WingMissle.SetActive(NameCommand== DictionaryEssence.TypeEvent.AttackMissle.ToString());
            MissleOpenObject.SetActive(NameCommand== DictionaryEssence.TypeEvent.AttackMissle.ToString());
            /*
            WingMissle.SetActive(commandLider.VisibleEventList[DictionaryEssence.TypeEvent.AttackMissle.ToString()]);
            MissleOpenObject.SetActive(commandLider.VisibleEventList[DictionaryEssence.TypeEvent.AttackMissle.ToString()]);
            */
        }

        //bomber
        if (BomberObject != null)
        {
            //if (commandLider.GetVisibleBomber() || commandLider.GetVisibleAttackBomber())
             if(NameCommand== DictionaryEssence.TypeEvent.Bomber.ToString()|| NameCommand== DictionaryEssence.TypeEvent.AttackBomber.ToString())
            {
                bool vis = true;
                BomberObject.SetActive(vis);
                VisibleObjList[DictionaryEssence.TypeEvent.Bomber.ToString()] = vis;
            }

        }
        
        if (UfoObject != null)
        {

            if (NameCommand== DictionaryEssence.TypeEvent.Ufo.ToString() 
                || NameCommand== DictionaryEssence.TypeEvent.Baby.ToString()
                || NameCommand == DictionaryEssence.TypeEvent.CrazyCow.ToString()
                || NameCommand == DictionaryEssence.TypeEvent.RocketRich.ToString()
                || NameCommand == DictionaryEssence.TypeEvent.Defectors.ToString()
                || NameCommand == DictionaryEssence.TypeEvent.AttackMissle.ToString()
                )
            {
                Debug.Log(VisibleObjList[DictionaryEssence.TypeEvent.Ufo.ToString()]+"T _   ange  = "+ VisibleObjList[DictionaryEssence.TypeEvent.Baby.ToString()]);
                //DefectorsObject.SetActive(commandLider.GetNameExecute(DictionaryEssence.TypeEvent.Defectors.ToString()));
                //DefectorsObject.SetActive(commandLider.VisibleEventList[DictionaryEssence.TypeEvent.Defectors.ToString()]);
                UfoObject.SetActive(true);
                UFOmodel uFOmodel = UfoObject.GetComponent<UFOmodel>();
                if (NameCommand== DictionaryEssence.TypeEvent.Ufo.ToString())
                {
                    uFOmodel.SetVisible(DictionaryEssence.TypeEvent.Ufo.ToString());
                }
                if (NameCommand== DictionaryEssence.TypeEvent.Baby.ToString())
                {
                    uFOmodel.SetVisible(DictionaryEssence.TypeEvent.Baby.ToString());
                }
                if (NameCommand == DictionaryEssence.TypeEvent.CrazyCow.ToString())
                {
                    uFOmodel.SetVisible(DictionaryEssence.TypeEvent.CrazyCow.ToString());
                }
                if (NameCommand == DictionaryEssence.TypeEvent.RocketRich.ToString())
                {
                    uFOmodel.SetVisible(DictionaryEssence.TypeEvent.RocketRich.ToString());
                }
                if (NameCommand == DictionaryEssence.TypeEvent.Defectors.ToString())
                {
                    uFOmodel.SetVisible(DictionaryEssence.TypeEvent.Defectors.ToString());
                }
                if (NameCommand == DictionaryEssence.TypeEvent.AttackMissle.ToString())
                {
                    uFOmodel.SetVisible(DictionaryEssence.TypeEvent.AttackMissle.ToString());
                }
            }
            else
            {
                UfoObject.SetActive(false);
            }
        }
   
        //UFOObject
    }

    void Update()
    {
        
        if (this._animationProcess)
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


            if (VisibleObjList[DictionaryEssence.TypeEvent.Ufo.ToString()] 
                || VisibleObjList[DictionaryEssence.TypeEvent.Baby.ToString()]
                || VisibleObjList[DictionaryEssence.TypeEvent.CrazyCow.ToString()]
                || VisibleObjList[DictionaryEssence.TypeEvent.RocketRich.ToString()]
                || VisibleObjList[DictionaryEssence.TypeEvent.Defectors.ToString()]
                || VisibleObjList[DictionaryEssence.TypeEvent.AttackMissle.ToString()]
                )
            {
                Debug.Log(Speed + "  &    ______________ _____ ___"+ UfoObject);

                new ViewMoveDeflectors().SendBomberAndWingState(UfoObject,
                        Speed, transform, _animationTimeProcess, TownList, buildingCentralModel);
            }
            
            
        }
    }
    public void ViewStartStateObject(List<GameObject> townList, float TimeDelete, CountryLider lider, Incident CommandIncident)
    {
        this._animationProcess = true;
        this._animationTimeProcess = Time.time;
        SetAnglePosition = false;
        
        this.TownList = townList;
        CreateObject();
        DestroyObject(TimeDelete);

        
        UpdateVisibleBuilding(CommandIncident.GetName());
Debug.Log("  _animation  = " + CommandIncident.GetName());
    }
    public void ViewEndState()
    {
        this._animationProcess = false;
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
        //BabyObject = Instantiate(BabyPrefabs, Propaganda.transform.position + new Vector3(0, 5, 24), Quaternion.identity);
        //BabyObject.transform.parent = transform;
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
        //Destroy(BabyObject, TimeDelete);
        Destroy(RocketRichObject, TimeDelete);
        Destroy(CrazyCowObject, TimeDelete);

        WingMissle.SetActive(false);
        BomberObject.SetActive(false);
        DefectorsObject.SetActive(false);
        //UfoObject.SetActive(false);
        //BabyObject.SetActive(false);
        RocketRichObject.SetActive(false);
        CrazyCowObject.SetActive(false);


    }
  
}
