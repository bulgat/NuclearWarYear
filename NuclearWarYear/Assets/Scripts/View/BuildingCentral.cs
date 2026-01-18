using Assets.Scripts.Model.AiTurn;
using Assets.Scripts.Model.param;
using Assets.Scripts.Model.paramTable;
using Assets.Scripts.View;
using System.Collections.Generic;
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
    public GameObject UfoObject;
    public GameObject WingMisslePrefabs;
    public GameObject DefectorsPrefabs;
    public GameObject UfoPrefabs;
    public GameObject RocketRichPrefabs;
    public GameObject CrazyCowPrefabs;
    public GameObject WingMissle;

    private Dictionary<string, bool> VisibleObjList;

    private int Speed = 5;

    bool SetAnglePosition;

    bool _animationProcess;
    float _animationTimeProcess;
    public TargetModel MyCity { get; private set; }
    public TargetModel FiendCity {  get; private set; }
    
    List<GameObject> TownList;

    public void Awake()
    {
        BuildingIndustry = gameObject.transform.GetChild(0).gameObject;
        DefenceObject = gameObject.transform.GetChild(1).gameObject;
        MissleObject = gameObject.transform.GetChild(2).gameObject;
        Propaganda = gameObject.transform.GetChild(3).gameObject;
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

        VisibleObjList = new Dictionary<string, bool>();
        VisibleObjList.Add(GlobalParam.TypeEvent.Bomber.ToString(), false);
        VisibleObjList.Add(GlobalParam.TypeEvent.AttackBomber.ToString(), false);
        VisibleObjList.Add(GlobalParam.TypeEvent.AttackMissle.ToString(), false);
        VisibleObjList.Add(GlobalParam.TypeEvent.Defectors.ToString(), false);
        VisibleObjList.Add(GlobalParam.TypeEvent.Ufo.ToString(), false);
        VisibleObjList.Add(GlobalParam.TypeEvent.Baby.ToString(), false);
        VisibleObjList.Add(GlobalParam.TypeEvent.RocketRich.ToString(), false);
        VisibleObjList.Add(GlobalParam.TypeEvent.CrazyCow.ToString(), false);


    }
    public void SetTargetModel(TargetModel myCity, TargetModel targetBuildingModel)
    {
        MyCity = myCity;
        FiendCity = targetBuildingModel;

        if (FiendCity != null)
        {
            if (VisibleObjList[GlobalParam.TypeEvent.RocketRich.ToString()])
            {
                Debug.Log("0008 RocketRich " + GetTarget(MyCity) );
                UfoObject.transform.position = GetTarget(MyCity);

            }
        }
    }

    public void UpdateVisibleBuilding(Incident incident)
    {
        var NameCommand = incident.GetName();

        Propaganda.SetActive(NameCommand == GlobalParam.TypeEvent.Propaganda);
        BuildingIndustry.SetActive(NameCommand == GlobalParam.TypeEvent.Build);
        DefenceObject.SetActive(NameCommand == GlobalParam.TypeEvent.Defence);
        //MissleObject.SetActive(NameCommand == GlobalParam.TypeEvent.Missle);
        MissleObject.SetActive(new GroupWeapon().GroupWeaponPresence(GlobalParam.GroupMissleList, incident));
        Airport.SetActive(NameCommand == GlobalParam.TypeEvent.Airport);
        AirportAttack.SetActive(NameCommand == GlobalParam.TypeEvent.AttackAirport);

        VisibleObjList[GlobalParam.TypeEvent.AttackBomber.ToString()] = NameCommand == GlobalParam.TypeEvent.AttackBomber;
        VisibleObjList[GlobalParam.TypeEvent.AttackMissle.ToString()] = NameCommand == GlobalParam.TypeEvent.AttackMissle;
        VisibleObjList[GlobalParam.TypeEvent.Defectors.ToString()] = NameCommand == GlobalParam.TypeEvent.Defectors;
        VisibleObjList[GlobalParam.TypeEvent.Ufo.ToString()] = NameCommand == GlobalParam.TypeEvent.Ufo;
        VisibleObjList[GlobalParam.TypeEvent.Baby.ToString()] = NameCommand == GlobalParam.TypeEvent.Baby;
        VisibleObjList[GlobalParam.TypeEvent.RocketRich.ToString()] = NameCommand == GlobalParam.TypeEvent.RocketRich;
        VisibleObjList[GlobalParam.TypeEvent.CrazyCow.ToString()] = NameCommand == GlobalParam.TypeEvent.CrazyCow;


        if (WingMissle != null)
        {
            WingMissle.SetActive(NameCommand == GlobalParam.TypeEvent.AttackMissle);
            MissleOpenObject.SetActive(NameCommand == GlobalParam.TypeEvent.AttackMissle);

        }

        //bomber
        if (BomberObject != null)
        {

            if (new GroupWeapon().GroupWeaponPresence(GlobalParam.GroupBomberList, incident) || NameCommand == GlobalParam.TypeEvent.AttackBomber)
            {
                bool vis = true;
                BomberObject.SetActive(vis);
                VisibleObjList[GlobalParam.TypeEvent.Bomber.ToString()] = vis;
            }

        }

        if (NameCommand == GlobalParam.TypeEvent.Ufo
                || NameCommand == GlobalParam.TypeEvent.Baby
                || NameCommand == GlobalParam.TypeEvent.CrazyCow
                || NameCommand == GlobalParam.TypeEvent.RocketRich
                || NameCommand == GlobalParam.TypeEvent.Defectors
                || NameCommand == GlobalParam.TypeEvent.AttackMissle
                )
        {

            UfoObject.SetActive(true);
            UFOmodel uFOmodel = UfoObject.GetComponent<UFOmodel>();
            if (NameCommand == GlobalParam.TypeEvent.Ufo)
            {
                uFOmodel.SetVisible(GlobalParam.TypeEvent.Ufo.ToString());
            }
            if (NameCommand == GlobalParam.TypeEvent.Baby)
            {
                uFOmodel.SetVisible(GlobalParam.TypeEvent.Baby.ToString());
            }
            if (NameCommand == GlobalParam.TypeEvent.CrazyCow)
            {
                uFOmodel.SetVisible(GlobalParam.TypeEvent.CrazyCow.ToString());
            }
            if (NameCommand == GlobalParam.TypeEvent.RocketRich)
            {

                uFOmodel.SetVisible(GlobalParam.TypeEvent.RocketRich.ToString());
            }
            if (NameCommand == GlobalParam.TypeEvent.Defectors)
            {
                uFOmodel.SetVisible(GlobalParam.TypeEvent.Defectors.ToString());
            }
            if (NameCommand == GlobalParam.TypeEvent.AttackMissle)
            {
                uFOmodel.SetVisible(GlobalParam.TypeEvent.AttackMissle.ToString());
            }
        }
        else
        {
            UfoObject.SetActive(false);
        }


    }

    void Update()
    {
        if (this._animationProcess)
        {
            if (VisibleObjList[GlobalParam.TypeEvent.Bomber.ToString()])
            {
                if (VisibleObjList[GlobalParam.TypeEvent.AttackBomber.ToString()] == false)
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
                    new ViewAttackBomber().SendBomberAndWingState(BomberObject,
                        Speed, transform, _animationTimeProcess, TownList, FiendCity);
                }
            }

            Vector3 targetBomber = GetTarget(FiendCity);

            if (VisibleObjList[GlobalParam.TypeEvent.Defectors.ToString()])
            {
                UfoObject.transform.position = new WithToInMove().SendWithToInMoveState(
                               UfoObject.transform.position,
                                   Speed, transform, _animationTimeProcess, TownList, false, FiendCity);
            }

            if (VisibleObjList[GlobalParam.TypeEvent.Ufo.ToString()]
                || VisibleObjList[GlobalParam.TypeEvent.Baby.ToString()]
                || VisibleObjList[GlobalParam.TypeEvent.CrazyCow.ToString()]
                )
            {
                UfoObject.transform.position = new ViewMoveDeflectors().SendBomberAndWingState(
                UfoObject.transform.position,
                    Speed, transform, _animationTimeProcess, TownList, false, GetTarget(MyCity));
            }
            

            if (VisibleObjList[GlobalParam.TypeEvent.AttackMissle.ToString()]
                )
            {
                UfoObject.transform.position = new ViewMoveDeflectors().SendBomberAndWingState(
                    UfoObject.transform.position,
                        Speed, transform, _animationTimeProcess, TownList, false, targetBomber);
            }

            if (VisibleObjList[GlobalParam.TypeEvent.RocketRich.ToString()])
            {
                var targetBomberRich = new Vector3(targetBomber.x + UIparam.OutFieldCosmic.x, targetBomber.y + +UIparam.OutFieldCosmic.y, targetBomber.z);


                UfoObject.transform.position = new ViewMoveDeflectors().SendBomberAndWingState(UfoObject.transform.position,
                        Speed, transform, _animationTimeProcess, TownList, false, targetBomberRich);
            }
        }
    }
    Vector3 GetTarget(TargetModel buildingCentral)
    {
        if (buildingCentral == null)
        {
            return Vector3.zero;
        }
        if (buildingCentral.GetTarget()==null)
        {
            return Vector3.zero;
        }

        GameObject cityTown = new SearchTownObject().GetTownViewWithId(buildingCentral.GetTarget(), TownList);

        CityView city = cityTown.GetComponent<CityView>();

        Vector3 targetBomber = cityTown.transform.position;

        return targetBomber;
    }

    public void ViewStartStateObject(
        List<GameObject> townList,
        float TimeDelete,
        CountryLider lider,
        Incident CommandIncident)
    {

        this._animationProcess = true;
        this._animationTimeProcess = Time.time;
        SetAnglePosition = false;


        this.TownList = townList;
        CreateObject(CommandIncident.GetName());

        DestroyObject(TimeDelete);
        UpdateVisibleBuilding(CommandIncident);
    }
    public void ViewEndState()
    {
        this._animationProcess = false;
    }
    private void CreateObject(GlobalParam.TypeEvent NameCommand)
    {
        WingMissle = Instantiate(WingMisslePrefabs, Propaganda.transform.position, Quaternion.identity);
        WingMissle.transform.parent = transform;
        BomberObject = Instantiate(BomberObjectPrefabs, Propaganda.transform.position, Quaternion.identity);
        BomberObject.transform.parent = transform;

        Vector3 startPosition = new Vector3(0, 4, 24);
        


        if (NameCommand == GlobalParam.TypeEvent.Defectors)
        {
            if (MyCity != null)
            {
                startPosition = new SearchTownObject().GetTownViewWithId(MyCity.GetTarget(), TownList).transform.position;
            }
        }
        

        UfoObject = Instantiate(UfoPrefabs, Propaganda.transform.position + startPosition, Quaternion.identity);
    }
    private void DestroyObject(float TimeDelete)
    {
        Destroy(WingMissle, TimeDelete);
        Destroy(BomberObject, TimeDelete);
        Destroy(UfoObject, TimeDelete);

        WingMissle.SetActive(false);
        BomberObject.SetActive(false);
    }

}
