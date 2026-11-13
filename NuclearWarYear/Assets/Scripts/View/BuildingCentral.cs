using Assets.Scripts.Model.AiTurn;
using Assets.Scripts.Model.param;
using Assets.Scripts.Model.paramTable;
using Assets.Scripts.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCentral : MonoBehaviour
{
    public GameObject BomberObjectPrefabs;
    public GameObject BomberObject;
    public GameObject WingMisslePrefabs;
    public GameObject UfoPrefabs;
    public GameObject WingMissle;
    private GameObject Propaganda;
    private GameObject BuildingIndustry;
    private GameObject DefenceObject;
    private GameObject MissleObject;
    private GameObject MissleOpenObject;
    private GameObject Airport;
    private GameObject AirportAttack;

    private GameObject UfoObject;
    private GameObject DefectorsPrefabs;
    private GameObject RocketRichPrefabs;
    private GameObject CrazyCowPrefabs;

    private Dictionary<string, bool> VisibleObjList;

    private int Speed = 5;

    bool SetAnglePosition;

    // Данные для баллистической траектории ракеты.
    Vector3 _missleStartPos;
    bool _missleStartPosInited;

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
        VisibleObjList.Add(GlobalParam.TypeEvent.Rain.ToString(), false);

    }
    public void SetTargetModel(TargetModel myCity, TargetModel targetBuildingModel)
    {
        MyCity = myCity;
        FiendCity = targetBuildingModel;

        if (FiendCity != null)
        {
            if (VisibleObjList[GlobalParam.TypeEvent.RocketRich.ToString()])
            {
                Debug.Log("0608 RocketRich " + GetTarget(MyCity) );

                if (UfoObject != null)
                {
                    UfoObject.transform.position = GetTarget(MyCity);
                }
            }
        }
    }

    public void UpdateVisibleBuilding(Incident incident)
    {
        GlobalParam.TypeEvent nameCommand = incident.GetName();
        Debug.Log("0600  coun   = " + nameCommand+" = "+ incident.GetTypeWeapon());

        Propaganda.SetActive(nameCommand == GlobalParam.TypeEvent.Propaganda);
        BuildingIndustry.SetActive(nameCommand == GlobalParam.TypeEvent.Build);
        DefenceObject.SetActive(nameCommand == GlobalParam.TypeEvent.Defence);
        MissleObject.SetActive(new GroupWeapon().GroupWeaponPresence(GlobalParam.GroupMissleList, incident));
        Airport.SetActive(nameCommand == GlobalParam.TypeEvent.Airport);
        AirportAttack.SetActive(nameCommand == GlobalParam.TypeEvent.AttackAirport);

        VisibleObjList[GlobalParam.TypeEvent.AttackBomber.ToString()] = nameCommand == GlobalParam.TypeEvent.AttackBomber;
        VisibleObjList[GlobalParam.TypeEvent.AttackMissle.ToString()] = nameCommand == GlobalParam.TypeEvent.AttackMissle;
        VisibleObjList[GlobalParam.TypeEvent.Defectors.ToString()] = nameCommand == GlobalParam.TypeEvent.Defectors;
        VisibleObjList[GlobalParam.TypeEvent.Ufo.ToString()] = nameCommand == GlobalParam.TypeEvent.Ufo;
        VisibleObjList[GlobalParam.TypeEvent.Baby.ToString()] = nameCommand == GlobalParam.TypeEvent.Baby;
        VisibleObjList[GlobalParam.TypeEvent.RocketRich.ToString()] = nameCommand == GlobalParam.TypeEvent.RocketRich;
        VisibleObjList[GlobalParam.TypeEvent.CrazyCow.ToString()] = nameCommand == GlobalParam.TypeEvent.CrazyCow;
        VisibleObjList[GlobalParam.TypeEvent.Rain.ToString()] = nameCommand == GlobalParam.TypeEvent.Rain;

        if (WingMissle != null)
        {
            WingMissle.SetActive(nameCommand == GlobalParam.TypeEvent.AttackMissle);
            MissleOpenObject.SetActive(nameCommand == GlobalParam.TypeEvent.AttackMissle);

        }

        //bomber
        if (BomberObject != null)
        {
            if (new GroupWeapon().GroupWeaponPresence(GlobalParam.GroupBomberList, incident) || nameCommand == GlobalParam.TypeEvent.AttackBomber)
            {
                bool vis = true;
                BomberObject.SetActive(vis);
                VisibleObjList[GlobalParam.TypeEvent.Bomber.ToString()] = vis;
            }
            var bomber = BomberObject.GetComponent<ViewNucklearBomber>();

            if (bomber != null) {
                Debug.Log("0601   messag  = "+ bomber);

                bomber.SetParam(nameCommand);
            }
        }

        if (nameCommand == GlobalParam.TypeEvent.Ufo
                || nameCommand == GlobalParam.TypeEvent.Baby
                || nameCommand == GlobalParam.TypeEvent.CrazyCow
                || nameCommand == GlobalParam.TypeEvent.RocketRich
                || nameCommand == GlobalParam.TypeEvent.Defectors
                || nameCommand == GlobalParam.TypeEvent.AttackMissle
                || nameCommand == GlobalParam.TypeEvent.Rain
                )
        {

            UfoObject.SetActive(true);
            UFOmodel uFOmodel = UfoObject.GetComponent<UFOmodel>();
            if (nameCommand == GlobalParam.TypeEvent.Ufo)
            {
                uFOmodel.SetVisible(GlobalParam.TypeEvent.Ufo);
            }
            if (nameCommand == GlobalParam.TypeEvent.Baby)
            {
                uFOmodel.SetVisible(GlobalParam.TypeEvent.Baby);
            }
            if (nameCommand == GlobalParam.TypeEvent.CrazyCow)
            {
                uFOmodel.SetVisible(GlobalParam.TypeEvent.CrazyCow);
            }
            if (nameCommand == GlobalParam.TypeEvent.RocketRich)
            {
                uFOmodel.SetVisible(GlobalParam.TypeEvent.RocketRich);
            }
            if (nameCommand == GlobalParam.TypeEvent.Defectors)
            {
                uFOmodel.SetVisible(GlobalParam.TypeEvent.Defectors);
            }
            if (nameCommand == GlobalParam.TypeEvent.AttackMissle)
            {
                uFOmodel.SetVisible(GlobalParam.TypeEvent.AttackMissle);
            }
            if (nameCommand == GlobalParam.TypeEvent.Rain)
            {
                uFOmodel.SetVisible(GlobalParam.TypeEvent.Rain);
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
                || VisibleObjList[GlobalParam.TypeEvent.Rain.ToString()]
                )
            {
                UfoObject.transform.position = new ViewMoveDeflectors().SendBomberAndWingState(
                UfoObject.transform.position,
                    Speed, transform, _animationTimeProcess, TownList, false, GetTarget(MyCity));
            }
            

            if (VisibleObjList[GlobalParam.TypeEvent.AttackMissle.ToString()]
                )
            {
                // Сохраняем стартовую позицию ракеты один раз при начале полёта.
                if (!_missleStartPosInited)
                {
                    _missleStartPos = UfoObject.transform.position;
                    _missleStartPosInited = true;
                }

                // Баллистическая траектория вверх: прямолинейное движение + параболическая добавка по Y.
                UfoObject.transform.position = SendMissleBallistic(
                    UfoObject.transform.position,
                    _missleStartPos,
                    targetBomber,
                    Speed);

                // Поворачиваем ракету носом к цели.
                // missleRotationOffset компенсирует исходную ориентацию спрайта (обычно нос смотрит вверх, поэтому -90).
                // Если спрайт нарисован иначе - подберите это значение.
                const float missleRotationOffset = -90f;
                Vector2 missleDirection = (Vector2)targetBomber - (Vector2)UfoObject.transform.position;
                if (missleDirection.sqrMagnitude > 0.01f)
                {
                    float missleAngle = Mathf.Atan2(missleDirection.y, missleDirection.x) * Mathf.Rad2Deg;
                    UfoObject.transform.rotation = Quaternion.Euler(Vector3.forward * (missleAngle + missleRotationOffset));
                }
            }

            if (VisibleObjList[GlobalParam.TypeEvent.RocketRich.ToString()])
            {
                var targetBomberRich = new Vector3(targetBomber.x + UIparam.OutFieldCosmic.x, targetBomber.y + +UIparam.OutFieldCosmic.y, targetBomber.z);


                UfoObject.transform.position = new ViewMoveDeflectors().SendBomberAndWingState(UfoObject.transform.position,
                        Speed, transform, _animationTimeProcess, TownList, false, targetBomberRich);
            }
        }
    }
    // Плавно двигает ракету к цели по небольшой параболической дуге вверх.
    Vector3 SendMissleBallistic(Vector3 currentPos, Vector3 startPos, Vector3 targetPos, int speed)
    {
        float step = speed * Time.deltaTime;

        Vector3 toTarget = targetPos - startPos;
        float totalDistance = toTarget.magnitude;
        if (totalDistance < 0.0001f)
        {
            return targetPos;
        }

        // Текущий прогресс полёта 0..1.
        float progress = Vector3.Distance(startPos, currentPos) / totalDistance;
        // Следующая позиция с той же скоростью (чтобы не замедлять сам полёт — считаем по горизонтали/расстоянию).
        Vector3 nextPos = Vector3.MoveTowards(currentPos, targetPos, step);
        float nextProgress = Vector3.Distance(startPos, nextPos) / totalDistance;

        // Параболическая высота: 4*h*progress*(1-progress), максимум в середине пути.
        // Очень малая дуга, чтобы баллистика была едва заметной на скорости ракеты.
        float arcHeight = Mathf.Abs(Vector3.Distance(startPos, targetPos)) * 0.003f;
        float curveProgress = 0.2f * (progress + nextProgress); // средняя точка для плавности
        float heightOffset = 0.2f * arcHeight * curveProgress * (1f - curveProgress);

        nextPos.y += heightOffset;
        return nextPos;
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
        _missleStartPosInited = false;


        this.TownList = townList;
        CreateObject(CommandIncident.GetName());

        DestroyObject(TimeDelete);
        UpdateVisibleBuilding(CommandIncident);
    }
    public void ViewEndState()
    {
        this._animationProcess = false;
        _missleStartPosInited = false;

        // Скрываем все движущиеся объекты сразу после завершения хода,
        // чтобы они не оставались видимыми на сцене до отложенного Destroy(TimeDelete).
        if (WingMissle != null) WingMissle.SetActive(false);
        if (BomberObject != null) BomberObject.SetActive(false);
        if (UfoObject != null) UfoObject.SetActive(false);
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

                if (MyCity.GetTarget() != null)
                {
                    startPosition = new SearchTownObject().GetTownViewWithId(MyCity.GetTarget(), TownList).transform.position;
                }
            }
        }

        // Ракета должна вылетать из атакующей страны (позиции здания пропаганды),
        // поэтому без смещения.
        if (NameCommand == GlobalParam.TypeEvent.AttackMissle)
        {
            startPosition = Vector3.zero;
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
