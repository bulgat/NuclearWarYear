using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public GameObject panelMain;
    public GameObject CanvasTacticReal;
    public GameObject CanvasResourcePlayer;
    public Text CanvasResourcePlayerPopulation;

    public GameObject NuclearMap;
    private Vector3 _targetNuclearMap;

    public GameObject Canvas;
    public GameObject CardGamePrefabs;
    public Image CircleReady;


    public Button ButtonResource;
    public Button ButtonCloseResource;
    public Button TurnButton;

    public Button PropButton;
    public Button BuildButton;
    public Button DefenceButton;
    public Button MissleButton;

    public Button MissleButton_1;
    public Button MissleButton_2;
    public Button MissleButton_3;

    public Button BomberButton;
    public Button BomberButton_1;

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
    float waitTurnTime = 2.0f;
    float _animationTime = 2.0f;
    bool _visiblePanel = true;

    private List<GameObject> TownViewList;

    [SerializeField]
    List<CountryLider> CountryLiderList;
  
    Controller _controller;
    MainModel _mainModel;
    private int flagIdPlayer = 5;
    public Text TextTypeWriter;

    void Awake()
    {
        CountryLiderList = null;
        
         _mainModel = new MainModel( CountryLiderPropagandaBuildingList, flagIdPlayer);
        CountryLiderList = _mainModel.GetCountryLiderList();
        
        
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
        PropButton.onClick.AddListener(() => PropMethod(PropButton));
        BuildButton.onClick.AddListener(() => BuildMethod(BuildButton));
        DefenceButton.onClick.AddListener(() => DefenceMethod(DefenceButton));

        MissleButton.onClick.AddListener(() => MissleMethod(1));
        MissleButton_1.onClick.AddListener(() => MissleMethod(2));
        MissleButton_2.onClick.AddListener(() => MissleMethod(3));
        MissleButton_3.onClick.AddListener(() => MissleMethod(4));

        BomberButton.onClick.AddListener(() => BomberMethod(1));
        BomberButton_1.onClick.AddListener(() => BomberMethod(2));

        //PromGameObject.onClick.AddListener(() => PropMethod(WarheadButton));

        var viewLiderButton = LiderButton_1.GetComponent<ViewLiderButton>();
        viewLiderButton.Init(LiderImageList, FlagImageList, _mainModel, IconCircleReadyList,0);

        var viewLiderButton_2 = LiderButton_2.GetComponent<ViewLiderButton>();
        viewLiderButton_2.Init(LiderImageList, FlagImageList, _mainModel, IconCircleReadyList,1);

        var viewLiderButton_3 = LiderButton_3.GetComponent<ViewLiderButton>();
        viewLiderButton_3.Init(LiderImageList, FlagImageList, _mainModel, IconCircleReadyList, 2);

        var viewLiderButton_4 = LiderButton_4.GetComponent<ViewLiderButton>();
        viewLiderButton_4.Init(LiderImageList, FlagImageList, _mainModel, IconCircleReadyList, 3);

        LiderButton_1.onClick.AddListener(() => LiderButton_1_Method(LiderButton_1));
        LiderButton_2.onClick.AddListener(() => LiderButton_2_Method(LiderButton_2));
        LiderButton_3.onClick.AddListener(() => LiderButton_3_Method(LiderButton_3));
        LiderButton_4.onClick.AddListener(() => LiderButton_4_Method(LiderButton_4));
        LiderButton_5.onClick.AddListener(() => LiderButton_5_Method(LiderButton_5));
   

        SetPropagand(5);
        
        new AICreateCommand().EstimationSetCommandAi(ResetAction, _mainModel.CountryLiderList, 
            _mainModel.GetTownList(), _mainModel._flagIdPlayer, _mainModel._flagIdPlayer);

        SelectCountryOne();
        SetAllCityVisibleComponent();
        ChangeImageLider();

        EnableButtonPlayer();

        //ReplaceCardGame();
        StartCoroutine(PrintTypeWriter("privet user"));
        //PrintTypeWriter();
        
        CanvasResourcePlayer.SetActive(false);
        CircleImageReadyParam(0, false);

        GlueTownView();
        // StartCoroutine(GlueTownView());
        
    }

    private void GlueTownView()
    {


        TownViewList = new List<GameObject>();
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

                    //TownViewList[i] = TownListView[i];
                    TownViewList.Add(TownListView[i]);
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

    private void ChangeImageLider()
    {

        ViewLiderButton viewLiderButton = LiderButton_1.GetComponent<ViewLiderButton>();
        viewLiderButton.ButtonLiderFrame();

        ViewLiderButton viewLiderButton_2 = LiderButton_2.GetComponent<ViewLiderButton>();
        viewLiderButton_2.ButtonLiderFrame();

        ViewLiderButton viewLiderButton_3 = LiderButton_3.GetComponent<ViewLiderButton>();
        viewLiderButton_3.ButtonLiderFrame();

        ViewLiderButton viewLiderButton_4 = LiderButton_4.GetComponent<ViewLiderButton>();
        viewLiderButton_4.ButtonLiderFrame();

        LiderButton_4.GetComponentInChildren<UnityEngine.UI.Text>().text = _mainModel.CountryLiderList[3].GetName() + " (" + _mainModel.CountryLiderList[3].GetAllOwnPopulation() + ")";
    }
  

    private void SelectCityTargetIdPlayer(int CityId)
    {
        
        City selectCityTarget = ClearCityTargetMark(CityId,true);
        EventController cityEvent = new EventController(Controller.Command.SelectCityEnemyTargetPlayer, new CityEvent( CityId));
        _controller.SendCommand(cityEvent);

        // TargetSity
        if (_mainModel._flagIdPlayer != selectCityTarget.FlagId)
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
        Debug.LogWarning(" TTT " + TownViewList);
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
    void ButtonResourceMethod(Button buttonResource)
    {
        
        CanvasResourcePlayer.SetActive(true);
        CanvasResourcePlayerPopulation.text = 
            " population " + _mainModel.GetCountryLiderList()[4].GetAllOwnPopulation()
            +"\n missle " + _mainModel.GetCountryLiderList()[4].GetMissleCount()
            + "\n bomber " + _mainModel.GetCountryLiderList()[4].GetBomberCount()
            ;
    }
    void ButtonCloseResourceMethod(Button buttonCloseResource) {
        CanvasResourcePlayer.SetActive(false);
        
    }
    void TurnButtonMethod(Button buttonPressed)
    {



        _visiblePanel = false;
        MoveMapNuclear();

        CanvasTacticRealSetText("Начало хода",0);
        // accept animation Central Building Propagation
        int indexLider = 0;
        foreach (CountryLider lider in _mainModel.CountryLiderList)
        {
            StartCoroutine(TurnOneLider(lider, indexLider));

            
            indexLider++;
        }
        float openWaitTime = waitTime + waitTurnTime * _mainModel.CountryLiderList.Count();

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
            _mainModel.GetTownList(), _mainModel._flagIdPlayer, _mainModel._flagIdPlayer);
    }
    private IEnumerator TurnText(string Name,int indexLider) {
       
       
        yield return new WaitForSeconds(waitTime+(waitTurnTime * indexLider));
        
        
    }

    private IEnumerator TurnOneLider(CountryLider lider, int indexLider)
    {
        yield return new WaitForSeconds(waitTime + (waitTurnTime * indexLider));

        EventController eventController = new EventController(Controller.Command.TurnSatisfyOneLider, new EventSendLider(lider.FlagId));
        _controller.SendCommand(eventController);


        
        CanvasTacticRealSetText(lider.GetName()+"  = "+ lider.GetCommandLider().GetNameCommand()+lider.GetEventTotalTurn(),
            lider.FlagId-1);


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
    //Propagand
    void SetPropagand(int FlagId)
    {
        
        EventController eventController = new EventController(Controller.Command.Propaganda, new EventSendLider(_mainModel._flagIdPlayer));
        _controller.SendCommand(eventController);

    }
    //Button
    void PropMethod(Button buttonPressed)
    {
        
        SetPropagand(flagIdPlayer);

        //new AICreateCommand().EstimationSetCommandAi(ResetAction, _mainModel.CountryLiderList, _mainModel.GetTownList(), _mainModel._flagIdPlayer, _mainModel._flagIdPlayer);
        
        StartCoroutine(PrintTypeWriter("\n propaganda"));
    }
    void BuildMethod(Button buttonPressed)
    {
     
        EventController eventController = new EventController(Controller.Command.Building, new EventSendLider(_mainModel._flagIdPlayer));
        _controller.SendCommand(eventController);

        StartCoroutine(PrintTypeWriter(" build weapon"));

    }
    void DefenceMethod(Button buttonPressed)
    {
        EventController eventController = new EventController(Controller.Command.Defence, new EventSendLider(_mainModel._flagIdPlayer));
        _controller.SendCommand(eventController);
        StartCoroutine(PrintTypeWriter(" defence"));

    }
    //Missle
    void MissleMethod(int IdMissle)
    {
        Debug.Log("=="+_mainModel.CountryLiderList[4].GetMissleSpecCount(IdMissle)+"  entered the selectable  IdMissle " + IdMissle);
        if (_mainModel.CountryLiderList[4].GetMissleSpecCount(IdMissle) > 0)
        {
            EventController eventController = new EventController(Controller.Command.Missle, new EventMissle(_mainModel._flagIdPlayer, IdMissle));
            _controller.SendCommand(eventController);
            StartCoroutine(PrintTypeWriter(" missle"));
        } else
        {
            StartCoroutine(PrintTypeWriter("not missle"));
        }


       

        
    }
    void BomberMethod(int SizeIdBomber)
    {
        EventController eventController = new EventController(Controller.Command.Bomber, new EventBomber(_mainModel._flagIdPlayer, SizeIdBomber));
        _controller.SendCommand(eventController);
        StartCoroutine(PrintTypeWriter(" bomber"));

    }
    float NuclearMapLeftX = 2.5f;
    float NuclearMapRightX = -2.4f;
    float NuclearMapTopY = -1.2f;
    float NuclearMapDowmY = 1.2f;
    void SelectCountryOne()
    {
        _targetNuclearMap = new Vector3(NuclearMapLeftX, NuclearMapTopY, 0);
    }
    void LiderButton_1_Method(Button buttonPressed)
    {
        EventController eventController = new EventController(Controller.Command.LiderTargetPlayer, new EventSendLider( 1));
        _controller.SendCommand(eventController);
     
        SelectCountryOne();

        ClearCityTargetMark(0,false);
    }
    void LiderButton_2_Method(Button buttonPressed)
    {
       
        EventController eventController = new EventController(Controller.Command.LiderTargetPlayer, new EventSendLider(2));
        _controller.SendCommand(eventController);
        _targetNuclearMap = new Vector3(NuclearMapRightX, NuclearMapTopY, 0);

        ClearCityTargetMark(0,false);
    }
    void LiderButton_3_Method(Button buttonPressed)
    {
       
        EventController eventController = new EventController(Controller.Command.LiderTargetPlayer, new EventSendLider(3));
        _controller.SendCommand(eventController);
        _targetNuclearMap = new Vector3(NuclearMapLeftX, NuclearMapDowmY, 0);

        ClearCityTargetMark(0,false);
    }
    void LiderButton_4_Method(Button buttonPressed)
    {

        EventController eventController = new EventController(Controller.Command.LiderTargetPlayer, new EventSendLider(4));
        _controller.SendCommand(eventController);
        _targetNuclearMap = new Vector3(NuclearMapRightX, NuclearMapDowmY, 0);

        ClearCityTargetMark(0,false);
    }
    void LiderButton_5_Method(Button buttonPressed)
    {
        
        _targetNuclearMap = new Vector3(NuclearMapRightX/3, NuclearMapDowmY/2, 0);
    }
 


    void ResetAction()
    {

        //SetCityVisible(false);

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

        //button player
        EnableButtonPlayer();

        ResetAction();

        ChangeImageLider();
    }
    void EnableButtonPlayer()
    {
    

        if (_mainModel.CountryLiderList[4].GetCommandLider().GetVisibleMissle())
        {
            //AttackMissleButton.GetComponent<Button>().interactable = true;
            MissleButton.GetComponent<Button>().interactable = false;

         
            var cityTarget = _mainModel.CountryLiderList[4].GetTargetCitySelectPlayer();
            if (cityTarget == null)
            {
                StartCoroutine(PrintTypeWriter("\n Ready. Not target for missle. Select Target!"));
            }
            else
            {
                StartCoroutine(PrintTypeWriter("\n Ready. Select target for missle"));
            }
            CircleImageReadyParam(1,true);
            EventController eventController = new EventController(Controller.Command.AttackMissle, new EventSendLider(_mainModel._flagIdPlayer));
            _controller.SendCommand(eventController);
        }
        else
        {
            
            MissleButton.GetComponent<Button>().interactable = true;
        }

        if (_mainModel.CountryLiderList[4].GetCommandLider().GetVisibleBomber())
        {
            //AttackBomberButton.GetComponent<Button>().interactable = true;

            BomberButton.GetComponent<Button>().interactable = false;
            //enableButton = true;
            
            var cityTarget = _mainModel.CountryLiderList[4].GetTargetCitySelectPlayer();
            if (cityTarget == null)
            {
                StartCoroutine(PrintTypeWriter("\n not target. Select Target!"));
            }
            else { 
                StartCoroutine(PrintTypeWriter("\n select target bomber"));
            }

            CircleImageReadyParam(0,true);
            
            EventController eventController = new EventController(Controller.Command.AttackBomber, new EventSendLider(_mainModel._flagIdPlayer));
            _controller.SendCommand(eventController);

        }
        else
        {
            //AttackBomberButton.GetComponent<Button>().interactable = false;
            BomberButton.GetComponent<Button>().interactable = true;
        }

        StartCoroutine(PrintTypeWriter("\n  * "+ _mainModel.CountryLiderList[4].GetEventTotalTurn()));

        Debug.Log("      turnBom im > "+ _mainModel.CountryLiderList[4].GetEventTotalTurn());
        ManagerButton();

        

        SetAllCityVisibleComponent();
    }
    private void ManagerButton() {
        BomberButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Light bomber ("
            + _mainModel.CountryLiderList[4].GetBomberCount() + ")";
        BomberButton_1.GetComponentInChildren<UnityEngine.UI.Text>().text = "Heavy bomber ("
            + _mainModel.CountryLiderList[4].GetBomberSpecCount(2) + ")";

        MissleButton.GetComponentInChildren<UnityEngine.UI.Text>().text =   "Light missle (" + _mainModel.CountryLiderList[4].GetMissleCount() + ")";
        MissleButton_1.GetComponentInChildren<UnityEngine.UI.Text>().text = "Medium missl (" + _mainModel.CountryLiderList[4].GetMissleSpecCount(2) + ")";
        MissleButton_2.GetComponentInChildren<UnityEngine.UI.Text>().text = "Heavy missle (" + _mainModel.CountryLiderList[4].GetMissleSpecCount(3) + ")";
        MissleButton_3.GetComponentInChildren<UnityEngine.UI.Text>().text = "S Heavy miss (" + _mainModel.CountryLiderList[4].GetMissleSpecCount(4) + ")";

        DefenceButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Defence (" + _mainModel.CountryLiderList[4].GetDefenceWeapon().Count() + ")";
    }


    private void CircleImageReadyParam(int IndexImage,bool Visible) {
        CircleReady.enabled = Visible;
        
        CircleReady.sprite = IconCircleReadyList[IndexImage];

    }

    private void UpdatePanelVisible() { 
        panelMain.GetComponent<Canvas>().enabled = _visiblePanel;
        CanvasTacticReal.GetComponent<Canvas>().enabled = (_visiblePanel==false);
        
    }
    private void CanvasTacticRealSetText(string InfoText,int FlagIndex)
    {
        CanvasTacticReal.GetComponentInChildren<UnityEngine.UI.Text>().text = InfoText;
        var CanvasTacticRealImage = CanvasTacticReal.GetComponentsInChildren<Image>();
        
        CanvasTacticRealImage[1].sprite = FlagImageList[FlagIndex];
    }

    void Update()
    {
        UpdatePanelVisible();



        BuildingCentral buildingCentral = new BuildingCentralHelper().GetBuildingCentral(_mainModel.CountryLiderList, _mainModel._flagIdPlayer);

        //player
        
        buildingCentral.VisibleBuilding(_mainModel.CountryLiderList[4].GetCommandLider());

        MoveAi();
        // visible label sity;
        SetAllCityVisibleLabelView(_visiblePanel == false);

        MoveMapNuclear();

        if (_mainModel._endGame)
        {
            
            //Application.LoadLevel("Victory");
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
            }
            
        }
    }
    private void SetAllCityVisibleLabelView(bool Visible)
    {
       
        
        if (TownViewList != null)
        {
            foreach (GameObject city in TownViewList)
            {
                //Debug.Log("0001 CORUTINE   mandLi  = " ) ;

                if (city)
                {

                    City townCity = city.GetComponent<City>();
                    townCity.SetVisibleLabel(Visible);
                }
                
            }
        }
    }

    private void MoveAi()
    {
        foreach (CountryLider lider in _mainModel.CountryLiderList)
        {

            if (lider.FlagId != _mainModel._flagIdPlayer)
            {
                BuildingCentral buildingCentral = new BuildingCentralHelper().GetBuildingCentral(_mainModel.CountryLiderList, lider.FlagId);

                buildingCentral.VisibleBuilding(lider.GetCommandLider());

            }
        }
    }
    IEnumerator PrintTypeWriter(string Message)
    {
        foreach (var item in Message)
        {
            TextTypeWriter.text += item;
            yield return new WaitForSeconds(0.1f);
        }

    }
}
