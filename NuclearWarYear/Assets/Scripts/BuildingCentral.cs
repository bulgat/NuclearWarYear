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

    public GameObject WingMisslePrefabs;
    public GameObject DefectorsPrefabs;
    public GameObject UfoPrefabs;
    public GameObject BabyPrefabs;
    public GameObject WingMissle;
    public GameObject Airport;
    public GameObject AirportAttack;

    private bool _visibleBomber;
    private bool _attackBomber;
    private bool _attackMissle;
    private bool _visibleDefectors;
    private bool _visibleUfo;
    private bool _visibleBaby;
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
            //WingMissle.SetActive(commandLider.VisibleAttackMissle);
            //MissleOpenObject.SetActive(commandLider.VisibleAttackMissle);

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
                _visibleBomber = vis;
            }
            /*
            else
            {
                BomberObject.SetActive(false);
                _visibleBomber = false;
            }
            */
        }
        if (DefectorsObject!=null) { 
            DefectorsObject.SetActive(commandLider.VisibleEventList["Defectors"]);
        }
        if (UfoObject != null)
        {
            UfoObject.SetActive(commandLider.VisibleEventList["Ufo"]);
        }
        _attackBomber = commandLider.GetVisibleAttackBomber();
        //_attackMissle = commandLider.VisibleAttackMissle;
        _attackMissle = commandLider.VisibleEventList["AttackMissle"];
        _visibleDefectors = commandLider.VisibleEventList["Defectors"];
        _visibleUfo = commandLider.VisibleEventList["Ufo"];
        _visibleBaby = commandLider.VisibleEventList["Baby"];
    }

    void Update()
    {
        if (_animationProcess)
        {
            if (_visibleBomber)
            {


                if (_attackBomber == false)
                {

                    if (BomberObject != null)
                    {
                        if (SetAnglePosition == false)
                        {
                            BomberObject.transform.position = new Vector3(Propaganda.transform.position.x, Propaganda.transform.position.y + 1, 0);

                            BomberObject.transform.Rotate(new Vector3(0, 0, 90));
                            SetAnglePosition = true;
                        }

                        //timeBomber += Time.deltaTime;

                        Vector3 pivot = new Vector3(transform.position.x, transform.position.y, 0);
                        float speed = 100.0f;


                        // To rotate around the world's up axis
                        BomberObject.transform.RotateAround(pivot, Vector3.forward, speed * Time.deltaTime);
                    }



                }
                else
                {
                    SendBomberAndWing(BomberObject,true,true,false);


                }
            }
            if (_attackMissle)
            {

                SendBomberAndWing(WingMissle,false,true,false);
            }
            if (_visibleDefectors)
            {
                SendBomberAndWing(DefectorsObject, false,false,false);
            }
            if (_visibleUfo)
            {
                SendBomberAndWing(UfoObject, false, false,true);
            }
            if (_visibleBaby)
            {
                SendBomberAndWing(BabyObject, false, false, true);
            }
        }
    }
    public void ViewStartStateObject(List<GameObject> townList, float TimeDelete, CountryLider lider)
    {
        this._animationProcess = true;
        this._animationTimeProcess = Time.time;
        SetAnglePosition = false;
        WingMissle = Instantiate(WingMisslePrefabs, Propaganda.transform.position, Quaternion.identity);
        BomberObject = Instantiate(BomberObjectPrefabs, Propaganda.transform.position, Quaternion.identity);
        DefectorsObject = Instantiate(DefectorsPrefabs, Propaganda.transform.position, Quaternion.identity);
        UfoObject = Instantiate(UfoPrefabs, Propaganda.transform.position+new Vector3(0,4,24), Quaternion.identity);
        BabyObject = Instantiate(BabyPrefabs, Propaganda.transform.position + new Vector3(0, 4, 24), Quaternion.identity);
        this.TownList = townList;
        DestroyObject(TimeDelete);
        
        Debug.Log("   ---0200   AIf d  tchAction  = ");
    }
    public void DestroyObject(float TimeDelete)
    {
        Destroy(WingMissle, TimeDelete);
        Destroy(BomberObject, TimeDelete);
        Destroy(DefectorsObject, TimeDelete);
        Destroy(UfoObject, TimeDelete);
    }
  
    void SendBomberAndWing(GameObject bomberObject,bool AirPlane,
        bool RotationAndExplode,bool Ufo)
    {

        float step = Speed * Time.deltaTime; // calculate distance to move
        if (bomberObject != null)
        {

            bool returnBomber = false;
            if (AirPlane)
            {
                if (this._animationTimeProcess + 3 < Time.time)
                {
                    returnBomber = true;

                }
            }
            if (Ufo)
            {

            }

            var offset = 260f;
            GameObject cityTown = GetTownViewWithId(this.buildingCentralModel.GetTargetBomber());
            City city = cityTown.GetComponent<City>();
            Vector3 targetBomber = cityTown.transform.position;
            if (returnBomber){
                targetBomber = transform.position;
            }
            Debug.Log(Time.time+"   E   " + city .GetId()+ " =  returnBomber  = " + returnBomber+"_____time = "+Time.deltaTime +"> " +(this._animationTimeProcess + .0005f));
            bomberObject.transform.position = Vector3.MoveTowards(bomberObject.transform.position, targetBomber, step);

            //Vector2 direction = (Vector2)targetBomber - (Vector2)transform.position;
            Vector2 direction = (Vector2)targetBomber - (Vector2)bomberObject.transform.position;
            direction.Normalize();
            if (RotationAndExplode)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                bomberObject.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
           
                //ExplodeTown
                //var kol = targetBomber - bomberObject.transform.position;
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
    public GameObject GetTownViewWithId(CityModel cityModel)
    {
        foreach (GameObject cityTown in this.TownList)
        {
            City city = cityTown.GetComponent<City>();
            if (city.GetId() == cityModel.GetId())
            {
                return cityTown;

            }
        }
        return null;
    }

}
