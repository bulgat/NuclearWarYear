using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using System.Text;
using System.Reflection;
using Assets.Scripts.View;
using Assets.Scripts.Model;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using static UnityEngine.ParticleSystem;
using System.Xml.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.VersionControl;
using Assets.Scripts.Model.param;
using UnityEngine.Video;

public class MenuScript : MonoBehaviour
{
    public Text TurnYear;
    public GameObject CardWeapon;
    public List<Sprite> IconCardList;
    public List<GameObject> UICardTownList;

    public GameObject panelMain;
    public GameObject CanvasTacticRealPrefabs;
    public GameObject CanTacticReal;
    public GameObject CanResPlayerPrefabs;
    public GameObject NewPaperPrefabs;
    public Image CanvasResourcePlayerImageLider;
    public Image CanvasResourceFlagImageLider;
    public Text CanvasResourcePlayerTextLider;

    public GameObject CanvasReportPrefabs;

    //public Text CanvasReportTextMessage;

    public List<GameObject> MapNationFlagList;

    public Text CanvasResourcePlayerPopulation;

    public GameObject NuclearMap;
    private Vector3 _targetNuclearMap;

    public GameObject Canvas;
    public GameObject CardGamePrefabs;
    public Image CircleReady;

    public GameObject TownCard;

    public Button ButtonResource;


    public Button TurnButton;

    public Button LiderButton_1;
    public Button LiderButton_2;
    public Button LiderButton_3;
    public Button LiderButton_4;
    public Button LiderButton_5;

    public Button NewPaperButton;

    public List<Sprite> LiderImageList;
    public List<Sprite> FlagImageList;
    public List<Sprite> IconCircleReadyList;

    public List<GameObject> CountryLiderPropagandaBuildingList;

    public List<Sprite> TownSpriteList;

    float waitTime = 4.0f;
    float waitTurnTime = 3.0f;
    float _animationTime = 2.0f;
    bool _visiblePanel = true;

    private List<GameObject> TownViewList;

    [SerializeField]
    List<CountryLider> CountryLiderList;

    public Controller _controller;
    MainModel _mainModel;
    private int flagIdPlayer;
    public Text TextTypeWriter;


    float NuclearMapLeftX = 2.5f;
    float NuclearMapRightX = -2.4f;
    float NuclearMapTopY = -1.2f;
    float NuclearMapDowmY = 1.2f;

    private List<GameObject> CardButtonList;
    public GameObject[] CountryList;
    public GameObject[] CountryLineList;
    public FixedJoystick Joystick;
    void Awake()
    {
        this.CountryLiderList = null;

        _mainModel = new MainModel(CountryLiderPropagandaBuildingList);
        this.flagIdPlayer = _mainModel.GetCurrentPlayerFlag();
        this.CountryLiderList = _mainModel.GetCountryLiderList();

    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // all objects are loaded, call other methods

        // StartCoroutine(PrintTypeWriter("LOAD SCENE"));
    }

    void Start()
    {

        _controller = new Controller(_mainModel);


        _targetNuclearMap = new Vector3(0, 0, 0);


        ButtonResource.onClick.AddListener(() => ButtonResourceMethod());

        TurnButton.onClick.AddListener(() => TurnButtonMethod());

        LiderButton_1.onClick.AddListener(() => LiderButton_1_Method(LiderButton_1));
        LiderButton_2.onClick.AddListener(() => LiderButton_2_Method(LiderButton_2));
        LiderButton_3.onClick.AddListener(() => LiderButton_3_Method(LiderButton_3));
        LiderButton_4.onClick.AddListener(() => LiderButton_4_Method(LiderButton_4));
        LiderButton_5.onClick.AddListener(() => LiderButton_5_Method(LiderButton_5));

        NewPaperButton.onClick.AddListener(() => ButtonNewPaper());

        

        _controller.TurnAi();
   
        SelectCountryOne();
        SetAllCityVisibleComponent();

        SetImageLiderButton();
        ChangeImageLider();

        EnableButtonPlayer();

        CircleImageReadyParam(0, false);

        GlueTownView();

        RefreshViewCard();

        ResetCountryOutline();

        CreateTownInfoList();

        new ViewPlayerButton().SetPropagand(this, this._mainModel.GetCurrenFlagPlayer(), this._mainModel);
    }
    void CreateTownInfoList()
    {
        this.UICardTownList = new List<GameObject>();
        foreach (var town in this.TownViewList)
        {
            GameObject CardTown = Instantiate(TownCard, new Vector2(100, 100), Quaternion.identity);
            TownCardInfo townCardInfo = CardTown.GetComponent<TownCardInfo>();
            City townCity = town.GetComponent<City>();
            townCardInfo.SetParam(this.FlagImageList, townCity);

            this.UICardTownList.Add(CardTown);

        }
    }

    void RefreshViewCard()
    {
        if (this.CardButtonList == null)
        {
            this.CardButtonList = new List<GameObject>();
        }
        foreach (var item in this.CardButtonList)
        {
            Destroy(item);
        }
        this.CardButtonList.Clear();
        //CardWeapon

        List<IWeapon> missleList = _mainModel.GetStaticWeapon();

        missleList.AddRange(_mainModel.GetCurrentWeapon());

        int count = 0;
        foreach (var item in missleList)
        {


            GameObject CardWing = Instantiate(CardWeapon, new Vector2(100 + (count * 100), 100), Quaternion.identity);
            CardWing.transform.SetParent(panelMain.transform);
            ViewCardWeapon viewCardWeapon = CardWing.GetComponent<ViewCardWeapon>();
            viewCardWeapon.SetParam(item.GetName(), IconCardList, item.GetId());
            viewCardWeapon.SetCallback(MissleMethodClick);
            this.CardButtonList.Add(CardWing);
            count++;
        }


    }
    void SetImageLiderButton()
    {
        List<CountryLider> fiendLider_ar = _mainModel.GetFiendCountryLiderList();



        ViewLiderButton viewLiderButton = LiderButton_1.GetComponent<ViewLiderButton>();
        viewLiderButton.Init(LiderImageList, FlagImageList, _mainModel,
            this.IconCircleReadyList, this.IconCardList, fiendLider_ar[0]);

        ViewLiderButton viewLiderButton_2 = LiderButton_2.GetComponent<ViewLiderButton>();
        viewLiderButton_2.Init(LiderImageList, FlagImageList, _mainModel,
            this.IconCircleReadyList, this.IconCardList, fiendLider_ar[1]);



        ViewLiderButton viewLiderButton_3 = LiderButton_3.GetComponent<ViewLiderButton>();
        viewLiderButton_3.Init(LiderImageList, FlagImageList, _mainModel,
        this.IconCircleReadyList, this.IconCardList, fiendLider_ar[2]);


        ViewLiderButton viewLiderButton_4 = LiderButton_4.GetComponent<ViewLiderButton>();
        viewLiderButton_4.Init(LiderImageList, FlagImageList, _mainModel,
            this.IconCircleReadyList, this.IconCardList, fiendLider_ar[3]);

        SetFlagNation();
    }
    void SetFlagNation()
    {
        List<CountryLider> fiendLider_ar = _mainModel.GetCountryLiderList();
        ChangeFlag(0, fiendLider_ar[0]);
        ChangeFlag(1, fiendLider_ar[1]);
        ChangeFlag(2, fiendLider_ar[2]);
        ChangeFlag(3, fiendLider_ar[3]);
        ChangeFlag(4, fiendLider_ar[4]);
    }
    private void ChangeImageLider()
    {


        ViewLiderButton viewLiderButton = LiderButton_1.GetComponent<ViewLiderButton>();
        viewLiderButton.ButtonLiderFrame(_mainModel.GetCurrentPlayerFlag());

        ViewLiderButton viewLiderButton_2 = LiderButton_2.GetComponent<ViewLiderButton>();
        viewLiderButton_2.ButtonLiderFrame(_mainModel.GetCurrentPlayerFlag());


        ViewLiderButton viewLiderButton_3 = LiderButton_3.GetComponent<ViewLiderButton>();
        viewLiderButton_3.ButtonLiderFrame(_mainModel.GetCurrentPlayerFlag());

        ViewLiderButton viewLiderButton_4 = LiderButton_4.GetComponent<ViewLiderButton>();
        viewLiderButton_4.ButtonLiderFrame(_mainModel.GetCurrentPlayerFlag());


    }
    private void ChangeFlag(int Index, CountryLider countryLider)
    {
        MapNationFlagList[Index].GetComponent<SpriteRenderer>().sprite = FlagImageList[countryLider.GraphicId];

    }
    private void GlueTownView()
    {


        this.TownViewList = new List<GameObject>();
        GameObject[] TownListView = GameObject.FindGameObjectsWithTag("Town");

        List<CityModel> cityModelList = _mainModel.GetTownList();

        for (int i = 0; i < TownListView.Length; i++)
        {

            foreach (CityModel cityModel in cityModelList)
            {



                City townCity = TownListView[i].GetComponent<City>();
                if (townCity.ViewId == cityModel.GetId())
                {
                    townCity.SetCityModelView(cityModel);
                    townCity.SetId(cityModel.GetId(), SelectCityTargetIdPlayer, TownSpriteList);

                    this.TownViewList.Add(TownListView[i]);
                }
            }


        }

    }

    private void SelectCityTargetIdPlayer(int CityId)
    {

        City selectCityTarget = ClearCityTargetMark(CityId, true);

        _controller.SelectCityEnemyTargetPlayer(CityId, _mainModel.GetCurrentPlayerFlag());

        // TargetSity
        if (_mainModel.GetCurrentPlayerFlag() != selectCityTarget.FlagId)
        {
            selectCityTarget.SetTargetAim(true);
        }
        else
        {
            // auto Set attack

            selectCityTarget.SetTargetAim(false);

        }

    }
    private City ClearCityTargetMark(int CityId, bool Player)
    {

        City selectCityTarget = null;
        foreach (GameObject city in TownViewList)
        {
            if (city != null)
            {

                City townCity = city.GetComponent<City>();
                townCity.ClearTargetAim();

                if (townCity.GetId() == CityId)
                {
                    selectCityTarget = townCity;

                }
            }

        }
        if (Player)
        {
            //reset?
            _controller.ResetSelectCityEnemyTargetPlayer(0);
        }
        return selectCityTarget;
    }
    private void SetAllCityVisibleComponent()
    {
        if (TownViewList != null)
        {
            foreach (var city in TownViewList)
            {
                if (city)
                {
                    City townCity = city.GetComponent<City>();
                    townCity.SetVisibleExplode(false);
                    townCity.SetVisibleShild(false);
                }
            }
        }
    }
    void MoveMapNuclear()
    {

        if (_visiblePanel)
        {
            /*
            float speed = 1.0f;

            float step = speed * Time.deltaTime; // calculate distance to move
            NuclearMap.transform.position = Vector3.MoveTowards(NuclearMap.transform.position, _targetNuclearMap, step);
            */
        }
        else
        {
            NuclearMap.transform.position = new Vector3(0, 0, 0);
        }
    }

    void ButtonResourceMethod()
    {
        GameObject CanResPlayer = Instantiate(CanResPlayerPrefabs, new Vector2(100, 100), Quaternion.identity);
        CanResPlayer.transform.parent = panelMain.transform;
        ViewResourcуBase viewResourceMethod = CanResPlayer.GetComponent<ViewResourcуBase>();

        viewResourceMethod.SetResourceMethodTable(this, this.LiderImageList, this.FlagImageList, this._mainModel);
    }

    void RefreshPlayerView()
    {
        this.flagIdPlayer = this._mainModel.GetCurrentPlayerFlag();

        SetImageLiderButton();
        ChangeImageLider();

    }
    void TacticReal(string EventMessage, int indexFlagId, int idImage, CountryLider lider)
    {


        if (this.CanTacticReal == null)
        {
            this.CanTacticReal = Instantiate(CanvasTacticRealPrefabs, new Vector2(100, 100), Quaternion.identity);
            //this.CanTacticReal.transform.SetParent(panelMain.transform);
        }

        ViewTacticReal viewTacticReal = this.CanTacticReal.AddComponent<ViewTacticReal>();
        viewTacticReal.Init(this.FlagImageList, this.IconCardList, this.TownViewList, this.UICardTownList);
        viewTacticReal.CanvasTacticRealSetText(EventMessage, indexFlagId, idImage, this.LiderImageList, this._mainModel, lider.FlagId - 1);
    }

    void TurnButtonMethod()
    {
        _controller.TurnAi();

        _controller.DoneMoveMadeCurrentPlayer();

        //Ходы игроков.
        //Все игроки сходили?


        if (this._mainModel.EveryonePlayerWent() == false)
        {
            //Переключится на другого игрока.

            _controller.ChangeCurrentPlayer();
            RefreshPlayerView();
            return;
        }

        _visiblePanel = false;
        MoveMapNuclear();

        TacticReal("Начало хода", GlobalParam.StartTurnIdFlag, GlobalParam.StartTurnIdImage, _mainModel.CountryLiderList.FirstOrDefault());

        // accept animation Central Building Propagation
        int indexLiderTime = 0;
        foreach (CountryLider lider in _mainModel.CountryLiderList)
        {

            foreach (CommandLider commandLider in lider.GetCommandLider())
            {

                StartCoroutine(TurnOneLider(lider, indexLiderTime, commandLider.GetIncident()));
                indexLiderTime++;
            }

        }
        float openWaitTime = waitTime + this.waitTurnTime * _mainModel.CountryLiderList.Count();

        StartCoroutine(OpenMenu(openWaitTime));
        StartCoroutine(AnimationPlayer(openWaitTime + _animationTime));

        // reset view
        foreach (GameObject city in TownViewList)
        {
            City townCity = city.GetComponent<City>();
            townCity.SetVisibleExplode(false);
            townCity.SetVisibleShild(false);
        }
        CircleImageReadyParam(0, false);

    }

    private IEnumerator TurnOneLider(CountryLider lider, int indexLider, Incident CommandIncident)
    {
        yield return new WaitForSeconds(this.waitTime + (this.waitTurnTime * indexLider));

        CommandIncident = _controller.TurnSatisfyOneLider(lider.FlagId, CommandIncident);


        this.TacticReal(CommandIncident.FullMessage(lider), lider.FlagId - 1, CommandIncident.IdImage, lider);

        BuildingCentral buildingCentral = lider.GetCentralBuildingPropogation().GetComponent<BuildingCentral>();
        buildingCentral.ViewStartStateObject(TownViewList, waitTime + (this.waitTurnTime * indexLider), lider, CommandIncident);

        buildingCentral.SetTargetBomber(TargetManager(lider));

        StartCoroutine(AfterTurnOneLider(CommandIncident, lider));

        Debug.Log(" year = "+_mainModel.CountYear+"  DoneMoveMadeCurrentP "+ CommandIncident.Name+ " f     =" + CommandIncident.GetMessage());

    }
    private IEnumerator AfterTurnOneLider(Incident CommandIncident, CountryLider lider)
    {
        yield return new WaitForSeconds(this.waitTurnTime - 1.0f);



        _controller.ReleasePopulationEvent(CommandIncident);
        BuildingCentral buildingCentral = lider.GetCentralBuildingPropogation().GetComponent<BuildingCentral>();
        buildingCentral.ViewEndState();
    }
    private CityModel TargetManager(CountryLider lider)
    {
        CityModel cityTown = lider.GetCommandLider().First()._TargetCity.TargetCity;
        if (cityTown != null)
        {
            GameObject viewTown = new ViewTown().GetTownViewWithId(TownViewList, cityTown);
            City city = viewTown.GetComponent<City>();


            GameObject targetCityObj = new CityGameObjHelper().GetCityCameObjectWithId(TownViewList, cityTown.GetId());

        }
        return cityTown;
    }

    void MissleMethodClick(int IdMissle)
    {

        foreach (var item in this.CardButtonList)
        {
            item.transform.localScale = new Vector2(1, 1);
        }


        CountryLider liderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, _mainModel.GetCurrenFlagPlayer());
        var missleList = new DictionaryEssence().GetIdTypeEventList(GlobalParam.TypeEvent.Missle).Select(a => { return a.Id; }).ToList();

var missleBomberIncident = new DictionaryEssence().GetIncident(IdMissle);

        if (missleList.Contains(IdMissle))
        {
            
            
            _controller.SetMissle(_mainModel.GetCurrenFlagPlayer(), missleBomberIncident.Name);
            CanvasReportWindow("Prepare a missle ", IdMissle);
        }
        if (new int[2] { 4, 5 }.Contains(IdMissle))
        {
            //EventController eventController = new EventController(Controller.Command.Bomber, new EventBomber(_mainModel.GetCurrenFlagPlayer(), IdMissle));
            //_controller.SendCommand(eventController);

            _controller.SetBomber(_mainModel.GetCurrenFlagPlayer(), missleBomberIncident.Name);
            CanvasReportWindow("Prepare bomber", IdMissle);

        }
        if (new int[2] { 6, 7 }.Contains(IdMissle))
        {

            _controller.Defence(_mainModel.GetCurrenFlagPlayer());
            CanvasReportWindow(" defence", IdMissle);
        }
        if (new int[1] { 9 }.Contains(IdMissle))
        {
            new ViewPlayerButton().SetPropagand(this, this._mainModel.GetCurrenFlagPlayer(), this._mainModel);

            CanvasReportWindow("\n propaganda", IdMissle);
        }
        if (new int[1] { 8 }.Contains(IdMissle))
        {

            _controller.Building(_mainModel.GetCurrenFlagPlayer());
            CanvasReportWindow(" build weapon", IdMissle);
        }


    }

    void SelectCountryOne()
    {
        _targetNuclearMap = new Vector3(NuclearMapLeftX, NuclearMapTopY, 0);
    }
    void LiderButton_1_Method(Button buttonPressed)
    {
        ResetCountryOutline();
        /*
        EventController eventController = new EventController(Controller.Command.LiderTargetPlayer, new EventSendLider(1));
        _controller.SendCommand(eventController);
        */
        _controller.LiderTargetPlayer(1);

        SelectCountryOne();

        ClearCityTargetMark(0, false);
        CountryLineList[4].SetActive(true);
    }
    void LiderButton_2_Method(Button buttonPressed)
    {
        ResetCountryOutline();
        /*
        EventController eventController = new EventController(Controller.Command.LiderTargetPlayer, new EventSendLider(2));
        _controller.SendCommand(eventController);
        */
        _controller.LiderTargetPlayer(2);

        ClearCityTargetMark(0, false);
        CountryLineList[2].SetActive(true);
    }
    void LiderButton_3_Method(Button buttonPressed)
    {
        ResetCountryOutline();
        /*
        EventController eventController = new EventController(Controller.Command.LiderTargetPlayer, new EventSendLider(3));
        _controller.SendCommand(eventController);
        */
        _controller.LiderTargetPlayer(3);

        ClearCityTargetMark(0, false);
        CountryLineList[3].SetActive(true);
    }
    void LiderButton_4_Method(Button buttonPressed)
    {
        ResetCountryOutline();
        /*
        EventController eventController = new EventController(Controller.Command.LiderTargetPlayer, new EventSendLider(4));
        _controller.SendCommand(eventController);
        */
        _controller.LiderTargetPlayer(4);

        ClearCityTargetMark(0, false);
        CountryLineList[1].SetActive(true);
    }
    void LiderButton_5_Method(Button buttonPressed)
    {
        ResetCountryOutline();
        CountryLineList[0].SetActive(true);
    }
    void ButtonNewPaper()
    {

        GameObject CanResPlayer = Instantiate(NewPaperPrefabs, new Vector2(100, 100), Quaternion.identity);
        CanResPlayer.transform.parent = panelMain.transform;
        ViewNewPaperMethod viewResourceMethod = CanResPlayer.GetComponent<ViewNewPaperMethod>();

        viewResourceMethod.SetResourceMethodTable(this, this.LiderImageList, this.FlagImageList, this._mainModel);



        viewResourceMethod.SetMessage(_mainModel.GetAllMessageTurn());

    }

    void ResetCountryOutline()
    {
        CountryLineList[0].SetActive(false);
        CountryLineList[1].SetActive(false);
        CountryLineList[2].SetActive(false);
        CountryLineList[3].SetActive(false);
        CountryLineList[4].SetActive(false);
    }



    void ResetAction()
    {

        _mainModel.ResetAction();
    }
    // Main time
    private IEnumerator OpenMenu(float WaitTimeOpen)
    {
        yield return new WaitForSeconds(WaitTimeOpen);
        _visiblePanel = true;
        NuclearMap.transform.position = _targetNuclearMap;
    }
    // fast time
    private IEnumerator AnimationPlayer(float AnimationTime)
    {

        yield return new WaitForSeconds(AnimationTime);

        _controller.ChangeCurrentPlayer();

        _mainModel.ResetDoneMoveAll();

        //button player
        EnableButtonPlayer();

        ResetAction();

        ChangeImageLider();
    }
    void EnableButtonPlayer()
    {
        StringBuilder printMessage = new StringBuilder("");

        CountryLider liderPlayer0 = new LiderHelperOne().GetLiderOne(this.CountryLiderList, _mainModel.GetCurrenFlagPlayer());

        if (liderPlayer0.GetCommandLider().First().GetVisibleMissle())
        {

            var cityTarget = liderPlayer0.GetTargetCitySelectPlayer();
            if (cityTarget == null)
            {
                printMessage.Append("\n Ready. Not target for missle. Select Target!");
            }
            else
            {
                printMessage.Append("\n Ready. Select target for missle");
            }
            CircleImageReadyParam(1, true);
            /*
            EventController eventController = new EventController(Controller.Command.AttackMissle, new EventSendLider(_mainModel.GetCurrenFlagPlayer()));
            _controller.SendCommand(eventController);
            */
            _controller.AttackMissle(_mainModel.GetCurrenFlagPlayer());
        }

        CountryLider liderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, _mainModel.GetCurrenFlagPlayer());
        if (liderPlayer.GetCommandLiderFirst().GetVisibleBomber())
        {

            var cityTarget = liderPlayer.GetTargetCitySelectPlayer();
            if (cityTarget == null)
            {

                printMessage.Append("\n not target. Select Target!");
            }
            else
            {

                printMessage.Append("\n select target bomber");
            }

            CircleImageReadyParam(0, true);

            _controller.AttackBomber(_mainModel.GetCurrenFlagPlayer());
        }


        ManagerButton();



        SetAllCityVisibleComponent();
        CanvasReportWindow(printMessage.ToString(), 0);

        RefreshViewCard();
    }
    private void CanvasReportWindow(string PrintMessage, int IdEvent)
    {

        if (0 == PrintMessage.Length)
        {
            return;
        }
        GameObject canvasReport = Instantiate(CanvasReportPrefabs, new Vector2(100, 100), Quaternion.identity);
        ViewCanvasReport viewCanvasReport = canvasReport.GetComponent<ViewCanvasReport>();
        viewCanvasReport.SetMessage(PrintMessage);
        viewCanvasReport.SetParam(this.IconCardList, IdEvent);

    }

    private void ManagerButton()
    {

        new ViewManageWeapon().ManagerButton(this, this._mainModel);

    }


    private void CircleImageReadyParam(int IndexImage, bool Visible)
    {
        CircleReady.enabled = Visible;

        CircleReady.sprite = IconCircleReadyList[IndexImage];

    }

    private void UpdatePanelVisible()
    {
        panelMain.GetComponent<Canvas>().enabled = _visiblePanel;
        if (_visiblePanel)
        {
            Destroy(this.CanTacticReal);
        }

    }
    private void SetAllCityVisibleLabelView(bool Visible)
    {


        if (TownViewList != null)
        {
            foreach (GameObject city in TownViewList)
            {

                if (city)
                {

                    City townCity = city.GetComponent<City>();
                    townCity.SetVisibleLabel(Visible);
                }

            }
        }
    }
    void Update()
    {
        UpdatePanelVisible();


        CountryLider fiendLider = new BuildingCentralHelper().GetFiendLider(_mainModel.CountryLiderList, _mainModel.GetCurrenFlagPlayer());
        BuildingCentral buildingCentral = new BuildingCentralHelper().GetBuildingCentral(fiendLider);

        //player

        CountryLider liderPLayer = _mainModel.GetCurrenPlayer();

        MoveAi();

        SetAllCityVisibleLabelView(_visiblePanel == false);

        MoveMapNuclear();

        if (_mainModel._endGame)
        {

            SceneManager.LoadScene("Victory", LoadSceneMode.Single);
        }
        if (Input.GetMouseButtonDown(0))
        {


            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit2D = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit2D)
            {

                City city = hit2D.transform.gameObject.GetComponent<City>();
                SelectCityTargetIdPlayer(city.GetId());


                ViewCardWeapon viewCardWeapon = hit2D.transform.gameObject.GetComponent<ViewCardWeapon>();

            }

        }
        DrawTownInfoList();
        UpdateJoystick();
        TurnYear.text = _mainModel.CountYear.ToString();
    }
    void DrawTownInfoList()
    {
        float h = gameObject.GetComponent<RectTransform>().rect.height;
        int count = 0;
        foreach (var town in this.TownViewList)
        {
            Vector3 coordinates = Camera.main.WorldToScreenPoint(town.transform.position);
            UICardTownList[count].transform.SetParent(gameObject.transform.GetChild(0));
            UICardTownList[count].transform.position = new Vector3(coordinates.x, coordinates.y - h / 10, coordinates.z);

            //Population
            count++;
        }


    }
    void UpdateJoystick()
    {
        //_targetNuclearMap 
        if (Joystick.Horizontal != 0 || Joystick.Vertical != 0)
        {
            _targetNuclearMap = new Vector3(
            _targetNuclearMap.x - Joystick.Horizontal / 50,
            _targetNuclearMap.y - Joystick.Vertical / 50,
            _targetNuclearMap.z);
        }
        NuclearMap.transform.position = _targetNuclearMap;

    }
    private void MoveAi()
    {
        foreach (CountryLider lider in _mainModel.CountryLiderList)
        {

            if (lider.FlagId != _mainModel.GetCurrenFlagPlayer())
            {
                CountryLider fiendLider = new BuildingCentralHelper().GetFiendLider(_mainModel.CountryLiderList, lider.FlagId);
                BuildingCentral buildingCentral = new BuildingCentralHelper().GetBuildingCentral(fiendLider);

            }
        }
    }

}
