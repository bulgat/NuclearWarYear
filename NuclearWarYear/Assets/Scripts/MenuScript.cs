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

public class MenuScript : MonoBehaviour
{
    public GameObject CardWeapon;
    public List<Sprite> IconCardList;

    public GameObject panelMain;
    public GameObject CanvasTacticReal;
    public GameObject CanvasResourcePlayer;
    public Image CanvasResourcePlayerImageLider;
    public Image CanvasResourceFlagImageLider;
    public Text CanvasResourcePlayerTextLider;

    public GameObject CanvasReport;
    public Button CanvasReportButtonClose;
    public Text CanvasReportTextMessage;
    //public Image CanvasReportIcon;

    public List<GameObject> MapNationFlagList; 

    public Text CanvasResourcePlayerPopulation;

    public GameObject NuclearMap;
    private Vector3 _targetNuclearMap;

    public GameObject Canvas;
    public GameObject CardGamePrefabs;
    public Image CircleReady;
    public Image ImageTestCard;

    public Button ButtonResource;
    public Button ButtonCloseResource;
    
    public Button TurnButton;

    public Button LiderButton_1;
    public Button LiderButton_2;
    public Button LiderButton_3;
    public Button LiderButton_4;
    public Button LiderButton_5;


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
    public ViewTacticReal _viewTacticReal;
    void Awake()
    {
        this.CountryLiderList = null;
        
         _mainModel = new MainModel( CountryLiderPropagandaBuildingList);
        this.flagIdPlayer = _mainModel.GetCurrentPlayerFlag();
        this.CountryLiderList = _mainModel.GetCountryLiderList();

        
        this._viewTacticReal = CanvasTacticReal.transform.GetChild(0).GetComponent<ViewTacticReal>();
        this._viewTacticReal.Init(this.FlagImageList,this.IconCardList);

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


        ButtonResource.onClick.AddListener(() => ButtonResourceMethod(ButtonResource));
        ButtonCloseResource.onClick.AddListener(() => ButtonCloseResourceMethod(ButtonCloseResource));
        TurnButton.onClick.AddListener(() => TurnButtonMethod(TurnButton));


        CanvasReportButtonClose.onClick.AddListener(() => CanvasReportButtonCloseMethod());

        
        

        LiderButton_1.onClick.AddListener(() => LiderButton_1_Method(LiderButton_1));
        LiderButton_2.onClick.AddListener(() => LiderButton_2_Method(LiderButton_2));
        LiderButton_3.onClick.AddListener(() => LiderButton_3_Method(LiderButton_3));
        LiderButton_4.onClick.AddListener(() => LiderButton_4_Method(LiderButton_4));
        LiderButton_5.onClick.AddListener(() => LiderButton_5_Method(LiderButton_5));
   
        

        new ViewPlayerButton().SetPropagand(this,this._mainModel.GetCurrenFlagPlayer(), this._mainModel);
        
        new AICreateCommand().EstimationSetCommandAi(ResetAction, _mainModel.CountryLiderList, 
            _mainModel.GetTownList(), _mainModel.GetCurrentPlayerFlag(), _mainModel.GetCurrentPlayerFlag());

        SelectCountryOne();
        SetAllCityVisibleComponent();

        SetImageLiderButton();
        ChangeImageLider();

        EnableButtonPlayer();

        //ReplaceCardGame();
        PrintTypeWriter("privet user");
 
        
        CanvasResourcePlayer.SetActive(false);
        CircleImageReadyParam(0, false);

        GlueTownView();
       
        CanvasReport.SetActive(false);

        RefreshViewCard();

        //CountryLineList[0].SetActive(false);
        ResetCountryOutline();
    }
    void RefreshViewCard()
    {
        if (this.CardButtonList == null)
        {
            this.CardButtonList = new List<GameObject>();
        }
        foreach(var item in this.CardButtonList)
        {
            Destroy(item);
        }
        this.CardButtonList.Clear();
        //CardWeapon
        //panelMain
        List<IWeapon> missleList = new List<IWeapon>();
        missleList.Add(new DictionaryEssence().GetIndustry());
        missleList.Add(new DictionaryEssence().GetPropaganda());
        missleList.AddRange(_mainModel.GetCurrenPlayer().GetDefenceWeapon());
        missleList.AddRange( _mainModel.GetCurrenPlayer().GetMissleList());

        int count = 0;
        foreach (var item in missleList)
        {
            Debug.LogWarning(item.GetSize()+" T  = " + item.GetTypeWeapon());

            GameObject CardWing = Instantiate(CardWeapon, new Vector2(100+(count*100),100), Quaternion.identity);
                CardWing.transform.parent = panelMain.transform;
                ViewCardWeapon viewCardWeapon = CardWing.GetComponent<ViewCardWeapon>();
                viewCardWeapon.SetParam(item.GetName(), IconCardList,item.GetId());
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
            this.IconCircleReadyList, fiendLider_ar[0]);

        //ChangeFlag(0, fiendLider_ar[0]);

        ViewLiderButton viewLiderButton_2 = LiderButton_2.GetComponent<ViewLiderButton>();
        viewLiderButton_2.Init(LiderImageList, FlagImageList, _mainModel,
            this.IconCircleReadyList, fiendLider_ar[1]);

        
 
            ViewLiderButton viewLiderButton_3 = LiderButton_3.GetComponent<ViewLiderButton>();
            viewLiderButton_3.Init(LiderImageList, FlagImageList, _mainModel,
            this.IconCircleReadyList, fiendLider_ar[2]);

        
            ViewLiderButton viewLiderButton_4 = LiderButton_4.GetComponent<ViewLiderButton>();
            viewLiderButton_4.Init(LiderImageList, FlagImageList, _mainModel,
                this.IconCircleReadyList, fiendLider_ar[3]);

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
    private void ChangeFlag(int Index,CountryLider countryLider)
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

    private void ReplaceCardGame()
    {
        for (int i = 0; i < 5; i++)
        {
            var CardBomberObject = Instantiate(CardGamePrefabs, new Vector3(-150 + (i * 80), -130, 0), Quaternion.identity);
            CardBomberObject.transform.SetParent(Canvas.transform, false);
        }
    }

   
  

    private void SelectCityTargetIdPlayer(int CityId)
    {
        
        City selectCityTarget = ClearCityTargetMark(CityId,true);
        EventController cityEvent = new EventController(Controller.Command.SelectCityEnemyTargetPlayer, new CityEvent( CityId));
        _controller.SendCommand(cityEvent);

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
    private City ClearCityTargetMark(int CityId,bool Player)
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
            EventController cityEvent = new EventController(Controller.Command.ResetSelectCityEnemyTargetPlayer, new CityEvent(0));
            _controller.SendCommand(cityEvent);
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
            float speed = 1.0f;

            float step = speed * Time.deltaTime; // calculate distance to move
            NuclearMap.transform.position = Vector3.MoveTowards(NuclearMap.transform.position, _targetNuclearMap, step);
        }
        else
        {
            NuclearMap.transform.position = new Vector3(0, 0, 0);
        }
    }
    void CanvasReportButtonCloseMethod()
    {
        CanvasReport.SetActive(false);
    }
    void ButtonResourceMethod(Button buttonResource)
    {
        new ViewResourceMethod().ViewResourceMethodTable(this, this.LiderImageList, this.FlagImageList,this._mainModel);

        
    }
    void ButtonCloseResourceMethod(Button buttonCloseResource) {
        CanvasResourcePlayer.SetActive(false);
        
    }
    void RefreshPlayerView()
    {
        this.flagIdPlayer = this._mainModel.GetCurrentPlayerFlag();
 
        SetImageLiderButton();
        ChangeImageLider();
        
    }
    void TurnButtonMethod(Button buttonPressed)
    {
        EventController eventController = new EventController(Controller.Command.DoneMoveMadeCurrentPlayer, null);
        _controller.SendCommand(eventController);

        //Ходы игроков.
        //Все игроки сходили?


        if (this._mainModel.EveryonePlayerWent()==false)
        {
            //Переключится на другого игрока.
            
            EventController eventController0 = new EventController(Controller.Command.ChangeCurrentPlayer, null);
            _controller.SendCommand(eventController0);
            RefreshPlayerView();
            return;
        }

        _visiblePanel = false;
        MoveMapNuclear();
        
        _viewTacticReal.CanvasTacticRealSetText("Начало хода",0,0, this.LiderImageList, this._mainModel,0);
        // accept animation Central Building Propagation
        int indexLider = 0;
        foreach (CountryLider lider in _mainModel.CountryLiderList)
        {
            StartCoroutine(TurnOneLider(lider, indexLider));

            
            indexLider++;
        }
        float openWaitTime = waitTime + this.waitTurnTime * _mainModel.CountryLiderList.Count();

        StartCoroutine(OpenMenu(openWaitTime));
        StartCoroutine(AnimationPlayer(openWaitTime+_animationTime));

        // reset view
        foreach (GameObject city in TownViewList)
        {
            City townCity = city.GetComponent<City>();
            townCity.SetVisibleExplode(false);
            townCity.SetVisibleShild(false);
        }
        CircleImageReadyParam(0, false);
        new AICreateCommand().EstimationSetCommandAi(ResetAction, _mainModel.CountryLiderList,
            _mainModel.GetTownList(), _mainModel.GetCurrenFlagPlayer(), _mainModel.GetCurrenFlagPlayer());
    }
    private IEnumerator TurnText(string Name,int indexLider) {
       
       
        yield return new WaitForSeconds(waitTime+(waitTurnTime * indexLider));
        
        
    }

    private IEnumerator TurnOneLider(CountryLider lider, int indexLider)
    {
        yield return new WaitForSeconds(waitTime + (waitTurnTime * indexLider));

        EventController eventController = new EventController(Controller.Command.TurnSatisfyOneLider, new EventSendLider(lider.FlagId));
        _controller.SendCommand(eventController);

        var idEvent = new DictionaryEssence().GetIdEvent(lider.GetCommandLider().GetNameCommand());
        
        Debug.Log(idEvent+"  = Controller = " + eventController.NameCommand+"   "+ lider.GetCommandLider().GetNameCommand());


        _viewTacticReal.CanvasTacticRealSetText(lider.GetName()+"  = "+ lider.GetCommandLider().GetNameCommand()+lider.GetEventTotalTurn(),
            lider.FlagId-1, idEvent, this.LiderImageList, this._mainModel, indexLider);



        BuildingCentral buildingCentral = lider.GetCentralBuildingPropogation().GetComponent<BuildingCentral>();
        buildingCentral.ViewStartStateObject(TownViewList, waitTime + (waitTurnTime * indexLider), lider);

        buildingCentral.SetTargetBomber(TargetManager(lider));
        
    }
    private CityModel TargetManager(CountryLider lider)
    {
        CityModel cityTown = lider.GetCommandLider().GetTargetCity();
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
        if (new int[4] { 0,1,2, 3 }.Contains(IdMissle))
            {
            EventController eventController = new EventController(Controller.Command.Missle, new EventMissle(_mainModel.GetCurrenFlagPlayer(), IdMissle));
            _controller.SendCommand(eventController);
            PrintTypeWriter("Prepare a missle "+ IdMissle);
        } 
        if (new int[2] { 4, 5 }.Contains(IdMissle))
        {
            EventController eventController = new EventController(Controller.Command.Bomber, new EventBomber(_mainModel.GetCurrenFlagPlayer(), IdMissle));
            _controller.SendCommand(eventController);
            PrintTypeWriter("Prepare bomber");

        }
        if (new int[2] { 6, 7 }.Contains(IdMissle)) {
            EventController eventController = new EventController(Controller.Command.Defence, new EventSendLider(_mainModel.GetCurrenFlagPlayer()));
            _controller.SendCommand(eventController);
            PrintTypeWriter(" defence");
        }
        if (new int[1] { 9 }.Contains(IdMissle))
        {
            new ViewPlayerButton().SetPropagand(this, this._mainModel.GetCurrenFlagPlayer(), this._mainModel);

            PrintTypeWriter("\n propaganda");
        }
        if (new int[1] { 8 }.Contains(IdMissle))
        {
            EventController eventController = new EventController(Controller.Command.Building, new EventSendLider(_mainModel.GetCurrenFlagPlayer()));
            _controller.SendCommand(eventController);

            PrintTypeWriter(" build weapon");
        }
        Debug.Log(" IdMissle = "+ IdMissle);
        var iconCard = CanvasReport.transform.GetChild(0).GetChild(1);
        ViewIconCard viewIconCard = iconCard.GetComponent<ViewIconCard>();
        viewIconCard.SetParam(this.IconCardList,IdMissle);
        //CanvasReportIcon.sprite = this.IconCardList[IdMissle];
    }

    void SelectCountryOne()
    {
        _targetNuclearMap = new Vector3(NuclearMapLeftX, NuclearMapTopY, 0);
    }
    void LiderButton_1_Method(Button buttonPressed)
    {
        ResetCountryOutline();
        EventController eventController = new EventController(Controller.Command.LiderTargetPlayer, new EventSendLider( 1));
        _controller.SendCommand(eventController);
     
        SelectCountryOne();

        ClearCityTargetMark(0,false);
        CountryLineList[4].SetActive(true);
    }
    void LiderButton_2_Method(Button buttonPressed)
    {
        ResetCountryOutline();
        EventController eventController = new EventController(Controller.Command.LiderTargetPlayer, new EventSendLider(2));
        _controller.SendCommand(eventController);
        _targetNuclearMap = new Vector3(NuclearMapRightX, NuclearMapTopY, 0);

        ClearCityTargetMark(0,false);
        CountryLineList[2].SetActive(true);
    }
    void LiderButton_3_Method(Button buttonPressed)
    {
        ResetCountryOutline();
        EventController eventController = new EventController(Controller.Command.LiderTargetPlayer, new EventSendLider(3));
        _controller.SendCommand(eventController);
        _targetNuclearMap = new Vector3(NuclearMapLeftX, NuclearMapDowmY, 0);

        ClearCityTargetMark(0,false);
        CountryLineList[3].SetActive(true);
    }
    void LiderButton_4_Method(Button buttonPressed)
    {
        ResetCountryOutline();
        EventController eventController = new EventController(Controller.Command.LiderTargetPlayer, new EventSendLider(4));
        _controller.SendCommand(eventController);
        _targetNuclearMap = new Vector3(NuclearMapRightX, NuclearMapDowmY, 0);

        ClearCityTargetMark(0,false);
        CountryLineList[1].SetActive(true);
    }
    void LiderButton_5_Method(Button buttonPressed)
    {
        ResetCountryOutline();
        _targetNuclearMap = new Vector3(NuclearMapRightX/3, NuclearMapDowmY/2, 0);
        CountryLineList[0].SetActive(true);
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

        EventController eventController0 = new EventController(Controller.Command.ChangeCurrentPlayer, null);
        _controller.SendCommand(eventController0);
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

        if (liderPlayer0.GetCommandLider().GetVisibleMissle())
        {
            
            //MissleButton.GetComponent<Button>().interactable = false;

           // CountryLider liderPlayer0 = new LiderHelperOne().GetLiderOne(this.CountryLiderList, _mainModel.GetCurrenFlagPlayer());
            var cityTarget = liderPlayer0.GetTargetCitySelectPlayer();
            if (cityTarget == null)
            {
                //StartCoroutine(PrintTypeWriter("\n Ready. Not target for missle. Select Target!"));
                printMessage.Append("\n Ready. Not target for missle. Select Target!");
            }
            else
            {
                //StartCoroutine(PrintTypeWriter("\n Ready. Select target for missle"));
                printMessage.Append("\n Ready. Select target for missle");
            }
            CircleImageReadyParam(1,true);
            EventController eventController = new EventController(Controller.Command.AttackMissle, new EventSendLider(_mainModel.GetCurrenFlagPlayer()));
            _controller.SendCommand(eventController);
        }
        else
        {
            
            //MissleButton.GetComponent<Button>().interactable = true;
        }
        CountryLider liderPlayer = new LiderHelperOne().GetLiderOne(this.CountryLiderList, _mainModel.GetCurrenFlagPlayer());
        if (liderPlayer.GetCommandLider().GetVisibleBomber())
        {
            Debug.Log("0011 ### and  FlagId    Id ="  );
            //BomberButton.GetComponent<Button>().interactable = false;
 
            var cityTarget = liderPlayer.GetTargetCitySelectPlayer();
            if (cityTarget == null)
            {

                printMessage.Append("\n not target. Select Target!");
            }
            else { 
         
                printMessage.Append("\n select target bomber");
            }

            CircleImageReadyParam(0,true);
            
            EventController eventController = new EventController(Controller.Command.AttackBomber, new EventSendLider(_mainModel.GetCurrenFlagPlayer()));
            _controller.SendCommand(eventController);

        }
        else
        {
            //BomberButton.GetComponent<Button>().interactable = true;
        }

        printMessage.Append("\n  * " + liderPlayer0.GetEventTotalTurn());

        
        ManagerButton();

        

        SetAllCityVisibleComponent();
        CanvasReportWindow(printMessage.ToString());

        RefreshViewCard();
    }
    private void CanvasReportWindow(string PrintMessage)
    {
        CanvasReport.SetActive(true);
        CanvasReportTextMessage.text = PrintMessage;

    }

    private void ManagerButton() {

        new ViewManageWeapon().ManagerButton(this,this._mainModel);

    }


    private void CircleImageReadyParam(int IndexImage,bool Visible) {
        CircleReady.enabled = Visible;
        
        CircleReady.sprite = IconCircleReadyList[IndexImage];

    }

    private void UpdatePanelVisible() { 
        panelMain.GetComponent<Canvas>().enabled = _visiblePanel;
        CanvasTacticReal.GetComponent<Canvas>().enabled = (_visiblePanel==false);
        
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



        BuildingCentral buildingCentral = new BuildingCentralHelper().GetBuildingCentral(_mainModel.CountryLiderList, _mainModel.GetCurrenFlagPlayer());

        //player
        
        CountryLider liderPLayer = _mainModel.GetCurrenPlayer();


        buildingCentral.VisibleBuilding(liderPLayer.GetCommandLider());

        MoveAi();
        // visible label sity;
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
ImageTestCard.transform.position = new Vector2(this.TownViewList.Last().transform.position.x*100, this.TownViewList.Last().transform.position.y*100);
               // Debug.Log( "  this.FlagImage  = "  );
    }


    private void MoveAi()
    {
        foreach (CountryLider lider in _mainModel.CountryLiderList)
        {

            if (lider.FlagId != _mainModel.GetCurrenFlagPlayer())
            {
                BuildingCentral buildingCentral = new BuildingCentralHelper().GetBuildingCentral(_mainModel.CountryLiderList, lider.FlagId);

                buildingCentral.VisibleBuilding(lider.GetCommandLider());

            }
        }
    }
    void PrintTypeWriter(string Message)
    {
        CanvasReportWindow(Message);
        /*
        foreach (var item in Message)
        {
            TextTypeWriter.text += item;
            yield return new WaitForSeconds(0.1f);
        }
        */
    }
}
