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


    public GameObject NuclearMap;
    private Vector3 _targetNuclearMap;

    public GameObject Canvas;
    public GameObject CardGamePrefabs;



    public Button ButtonResource;
    public Button ButtonCloseResource;
    public Button TurnButton;

    public Button PropButton;
    public Button BuildButton;
    public Button DefenceButton;
    public Button MissleButton;
    public Button BomberButton;
    //public Button AttackBomberButton;
    //public Button AttackMissleButton;
    public Button WarheadButton;

    public Button LiderButton_1;
    public Button LiderButton_2;
    public Button LiderButton_3;
    public Button LiderButton_4;
    public Button LiderButton_5;

    public Image LiderImage_1;
    public Image LiderImage_2;
    public Image LiderImage_3;
    public Image LiderImage_4;
    public List<Sprite> LiderImageList;

    public List<GameObject> CountryLiderPropagandaBuildingList;
    public Image HpBar;
    public Image HpBarBomber;
    public Image HpBarWarhead;

    public List<Sprite> TownSpriteList;

    float waitTime = 4.0f;
    float waitTurnTime = 2.0f;
    float _animationTime = 2.0f;
    bool _visiblePanel = true;

    private GameObject[] TownList;

    [SerializeField]
    List<CountryLider> CountryLiderList;
    //int _flagIdPlayer =5;
    Controller _controller;
    MainModel _mainModel;
    private int flagIdPlayer = 5;
    public Text TextTypeWriter;

    void Awake()
    {
        CountryLiderList = null;
        
         _mainModel = new MainModel( CountryLiderPropagandaBuildingList, flagIdPlayer);
        CountryLiderList = _mainModel.GetCountryLiderList();
        TownList =new GameObject[_mainModel.GetTownList().Count];
        
    }
    IEnumerator PrintTypeWriter(string Message)
    {
        foreach (var item in Message)
        {
            TextTypeWriter.text += item;
            yield return new WaitForSeconds(0.1f);
        }

    }
    void Start()
    {
       



        GameObject[] TownListView = GameObject.FindGameObjectsWithTag("Town");
        for(int i=0;i< TownListView.Length;i++)
        {
            City townCity = TownListView[i].GetComponent<City>();
            
            CityModel cityModel = _mainModel.GetTownList()[i];
            townCity.SetCityModelView(cityModel);
            townCity.SetId(cityModel.GetId(), SelectCityTargetIdPlayer, TownSpriteList);
 
            TownList[i] = TownListView[i];

        }
        
        _controller = new Controller(_mainModel);


        _targetNuclearMap = new Vector3(0, 0, 0);


        ButtonResource.onClick.AddListener(() => ButtonResourceMethod(ButtonResource));
        ButtonCloseResource.onClick.AddListener(() => ButtonCloseResourceMethod(ButtonCloseResource));
        TurnButton.onClick.AddListener(() => TurnAttackMethod(TurnButton));
        PropButton.onClick.AddListener(() => PropMethod(PropButton));
        BuildButton.onClick.AddListener(() => BuildMethod(BuildButton));
        DefenceButton.onClick.AddListener(() => DefenceMethod(DefenceButton));
        MissleButton.onClick.AddListener(() => MissleMethod(MissleButton));
        BomberButton.onClick.AddListener(() => BomberMethod(BomberButton));
        //AttackBomberButton.onClick.AddListener(() => AttackBomberButtonMethod(AttackBomberButton));
        //AttackMissleButton.onClick.AddListener(() => AttackMissleButtonMethod(AttackMissleButton));
        WarheadButton.onClick.AddListener(() => WarheadMethod(WarheadButton));

        LiderButton_1.onClick.AddListener(() => LiderButton_1_Method(LiderButton_1));
        LiderButton_2.onClick.AddListener(() => LiderButton_2_Method(LiderButton_2));
        LiderButton_3.onClick.AddListener(() => LiderButton_3_Method(LiderButton_3));
        LiderButton_4.onClick.AddListener(() => LiderButton_4_Method(LiderButton_4));
        LiderButton_5.onClick.AddListener(() => LiderButton_5_Method(LiderButton_5));
        // LiderButton_1.Find("Button Name").GetComponentInChildren<Text>().text = "Button Text";


        SetPropagand(5);
        
        new AICreateCommand().EstimationSetCommandAi(ResetAction, _mainModel.CountryLiderList, 
            _mainModel.GetTownList(), _mainModel._flagIdPlayer, _mainModel._flagIdPlayer);

        GetEnergeLevelPercent();
        GetEnergeLevelPercentBomber();
        GetEnergeLevelPercentWarhead();

        SelectCountryOne();
        SetAllCityVisibleComponent();
        ChangeImageLider();

        EnableButtonPlayer();

        //ReplaceCardGame();
        StartCoroutine(PrintTypeWriter("privet user"));
        //PrintTypeWriter();
        //SetAllCityVisibleLabelView(true);
        CanvasResourcePlayer.SetActive(false);
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

        LiderImage_1.sprite = LiderImageList[0 + _mainModel.CountryLiderList[0].Mood];
        LiderImage_2.sprite = LiderImageList[8 + _mainModel.CountryLiderList[1].Mood];
        LiderImage_3.sprite = LiderImageList[16 + _mainModel.CountryLiderList[2].Mood];
        LiderImage_4.sprite = LiderImageList[24 + _mainModel.CountryLiderList[3].Mood];

        LiderButton_1.GetComponentInChildren<UnityEngine.UI.Text>().text = _mainModel.CountryLiderList[0].GetName()+" ("+ _mainModel.CountryLiderList[0].GetAllOwnPopulation()+")";
        LiderButton_2.GetComponentInChildren<UnityEngine.UI.Text>().text = _mainModel.CountryLiderList[1].GetName() + " (" + _mainModel.CountryLiderList[1].GetAllOwnPopulation() + ")";
        LiderButton_3.GetComponentInChildren<UnityEngine.UI.Text>().text = _mainModel.CountryLiderList[2].GetName() + " (" + _mainModel.CountryLiderList[2].GetAllOwnPopulation() + ")";
        LiderButton_4.GetComponentInChildren<UnityEngine.UI.Text>().text = _mainModel.CountryLiderList[3].GetName() + " (" + _mainModel.CountryLiderList[3].GetAllOwnPopulation() + ")";
    }

    private void SelectCityTargetIdPlayer(int CityId)
    {
        
        City selectCityTarget = ClearCityTargetMark(CityId,true);
        CityEvent cityEvent = new CityEvent(Controller.Command.SelectCityEnemyTargetPlayer, CityId);
        _controller.SendCommand(cityEvent);

        


        
        Debug.Log("0011  City =" + selectCityTarget);
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
        City selectCityTarget = null;
        foreach (GameObject city in TownList)
        {
            City townCity = city.GetComponent<City>();
            townCity.ClearTargetAim();

            if (townCity.GetId() == CityId)
            {
                selectCityTarget = townCity;

            }

        }
        if (Player)
        {
            //reset?
            CityEvent cityEvent = new CityEvent(Controller.Command.ResetSelectCityEnemyTargetPlayer, 0);
            _controller.SendCommand(cityEvent);
        }
        return selectCityTarget;
    }
    private void SetAllCityVisibleComponent()
    {
        foreach (var city in TownList)
        {
            City townCity = city.GetComponent<City>();
            townCity.SetVisible(false);
            townCity.SetVisibleShild(false);
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
        Debug.Log("JJJJJJJJJJJJ");
        CanvasResourcePlayer.SetActive(true);
       
    }
    void ButtonCloseResourceMethod(Button buttonCloseResource) {
        CanvasResourcePlayer.SetActive(false);
        Debug.Log("===" );
    }
    void TurnAttackMethod(Button buttonPressed)
    {



        _visiblePanel = false;
        MoveMapNuclear();

        CanvasTacticRealSetText("Начало хода");
        // accept animation Central Building Propagation
        int indexLider = 0;
        foreach (CountryLider lider in _mainModel.CountryLiderList)
        {
            StartCoroutine(TurnOneLider(lider, indexLider));

            //StartCoroutine(TurnText(lider.GetName(), indexLider));
            indexLider++;
        }
        float openWaitTime = waitTime + waitTurnTime * _mainModel.CountryLiderList.Count();

        StartCoroutine(OpenMenu(openWaitTime));
        StartCoroutine(AnimationPlayer(openWaitTime+_animationTime));

        // reset view
        foreach (GameObject city in TownList)
        {
            City townCity = city.GetComponent<City>();
            townCity.SetVisible(false);
            townCity.SetVisibleShild(false);
        }
    }
    private IEnumerator TurnText(string Name,int indexLider) {
       
       
        yield return new WaitForSeconds(waitTime+(waitTurnTime * indexLider));
        Debug.Log("0001 CORUTINE   mandLider = "+ Name);
        
    }

    private IEnumerator TurnOneLider(CountryLider lider, int indexLider)
    {
        yield return new WaitForSeconds(waitTime + (waitTurnTime * indexLider));

        CanvasTacticRealSetText(lider.GetName()+"  = "+ lider.GetCommandLider().GetNameCommand());

        BuildingCentral buildingCentral = lider.GetCentralBuildingPropogation().GetComponent<BuildingCentral>();
        buildingCentral.StartStateObject(TownList, waitTime + (waitTurnTime * indexLider));

        CityModel cityTown = lider.GetCommandLider().GetTargetCity();
        if (cityTown != null)
        {
            GameObject viewTown = new ViewTown().GetTownViewWithId(TownList, cityTown);
            City city = viewTown.GetComponent<City>();
            

            GameObject targetCityObj = new CityGameObjHelper().GetCityCameObjectWithId(TownList, cityTown.GetId());
            if (targetCityObj != null)
            {
                buildingCentral.SetTargetBomber(cityTown);
            }
        }
    }
    //Propagand
    void SetPropagand(int FlagId)
    {
        
        EventController eventController = new EventController(Controller.Command.Propaganda, _mainModel._flagIdPlayer);
        _controller.SendCommand(eventController);

    }
    //Button
    void PropMethod(Button buttonPressed)
    {
        
        SetPropagand(flagIdPlayer);

        new AICreateCommand().EstimationSetCommandAi(ResetAction, _mainModel.CountryLiderList, _mainModel.GetTownList(), _mainModel._flagIdPlayer, _mainModel._flagIdPlayer);
        
        StartCoroutine(PrintTypeWriter("\n propaganda"));
    }
    void BuildMethod(Button buttonPressed)
    {
     
        EventController eventController = new EventController(Controller.Command.Building, _mainModel._flagIdPlayer);
        _controller.SendCommand(eventController);

        StartCoroutine(PrintTypeWriter("build weapon"));

    }
    void DefenceMethod(Button buttonPressed)
    {
        EventController eventController = new EventController(Controller.Command.Defence, _mainModel._flagIdPlayer);
        _controller.SendCommand(eventController);
        StartCoroutine(PrintTypeWriter("defence"));

    }
    //Missle
    void MissleMethod(Button buttonPressed)
    {
        EventController eventController = new EventController(Controller.Command.Missle, _mainModel._flagIdPlayer);
        _controller.SendCommand(eventController);
        StartCoroutine(PrintTypeWriter("missle"));
    }
    void BomberMethod(Button buttonPressed)
    {
        EventController eventController = new EventController(Controller.Command.Bomber, _mainModel._flagIdPlayer);
        _controller.SendCommand(eventController);
        StartCoroutine(PrintTypeWriter("bomber"));

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
        EventController eventController = new EventController(Controller.Command.LiderTargetPlayer, 1);
        _controller.SendCommand(eventController);
     
        SelectCountryOne();

        ClearCityTargetMark(0,false);
    }
    void LiderButton_2_Method(Button buttonPressed)
    {
       
        EventController eventController = new EventController(Controller.Command.LiderTargetPlayer, 2);
        _controller.SendCommand(eventController);
        _targetNuclearMap = new Vector3(NuclearMapRightX, NuclearMapTopY, 0);

        ClearCityTargetMark(0,false);
    }
    void LiderButton_3_Method(Button buttonPressed)
    {
       
        EventController eventController = new EventController(Controller.Command.LiderTargetPlayer, 3);
        _controller.SendCommand(eventController);
        _targetNuclearMap = new Vector3(NuclearMapLeftX, NuclearMapDowmY, 0);

        ClearCityTargetMark(0,false);
    }
    void LiderButton_4_Method(Button buttonPressed)
    {

        EventController eventController = new EventController(Controller.Command.LiderTargetPlayer, 4);
        _controller.SendCommand(eventController);
        _targetNuclearMap = new Vector3(NuclearMapRightX, NuclearMapDowmY, 0);

        ClearCityTargetMark(0,false);
    }
    void LiderButton_5_Method(Button buttonPressed)
    {
        
        _targetNuclearMap = new Vector3(NuclearMapRightX/3, NuclearMapDowmY/2, 0);
    }
        // Bomber
    //void AttackBomberButtonMethod(Button buttonPressed)
    //{
       // EventController eventController = new EventController(Controller.Command.AttackBomber, _mainModel._flagIdPlayer);
       // _controller.SendCommand(eventController);

    //}
    // Missle
    /*
    void AttackMissleButtonMethod(Button buttonPressed)
    {
        EventController eventController = new EventController(Controller.Command.AttackMissle, _mainModel._flagIdPlayer);
        _controller.SendCommand(eventController);


    }
    */
    //WarheadMethod
    void WarheadMethod(Button buttonPressed)
    {
        EventController eventController = new EventController(Controller.Command.Warhead, _mainModel._flagIdPlayer);
        _controller.SendCommand(eventController);

        GetEnergeLevelPercentWarhead();
    }



    void ResetAction()
    {

        SetCityVisible(false);

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

        EventController eventController = new EventController(Controller.Command.TotalTurn, _mainModel._flagIdPlayer);
        _controller.SendCommand(eventController);

        //button player
        EnableButtonPlayer();

        GetEnergeLevelPercent();
        GetEnergeLevelPercentBomber();
        GetEnergeLevelPercentWarhead();
        ResetAction();

        SetCityVisible(true);
        ChangeImageLider();
    }
    void EnableButtonPlayer()
    {
        bool enableButton = false;

        if (_mainModel.CountryLiderList[4].GetCommandLider().GetVisibleMissle())
        {
            //AttackMissleButton.GetComponent<Button>().interactable = true;
            MissleButton.GetComponent<Button>().interactable = false;

            enableButton = true;
           // StartCoroutine(PrintTypeWriter("\n missle ready!"));
            EventController eventController = new EventController(Controller.Command.AttackMissle, _mainModel._flagIdPlayer);
            _controller.SendCommand(eventController);
            Debug.Log("00 i  = " );
            var cityTarget = _mainModel.CountryLiderList[4].GetTargetCitySelectPlayer();
            if (cityTarget == null)
            {
                StartCoroutine(PrintTypeWriter("\n Ready. Not target for missle. Select Target!"));
            }
            else
            {
                StartCoroutine(PrintTypeWriter("\n Ready. Select target for missle"));
            }
        }
        else
        {
            //AttackMissleButton.GetComponent<Button>().interactable = false;
            MissleButton.GetComponent<Button>().interactable = true;
        }

        if (_mainModel.CountryLiderList[4].GetCommandLider().GetVisibleBomber())
        {
            //AttackBomberButton.GetComponent<Button>().interactable = true;

            BomberButton.GetComponent<Button>().interactable = false;
            enableButton = true;
            
            var cityTarget = _mainModel.CountryLiderList[4].GetTargetCitySelectPlayer();
            if (cityTarget == null)
            {
                StartCoroutine(PrintTypeWriter("\n not target. Select Target!"));
            }
            else { 
                StartCoroutine(PrintTypeWriter("\n select target bomber"));
            }
            
            Debug.Log("0000_______  [ "+ cityTarget + "]    = "  );
            EventController eventController = new EventController(Controller.Command.AttackBomber, _mainModel._flagIdPlayer);
            _controller.SendCommand(eventController);
        }
        else
        {
            //AttackBomberButton.GetComponent<Button>().interactable = false;
            BomberButton.GetComponent<Button>().interactable = true;
        }
        if (enableButton)
        {
            WarheadButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            WarheadButton.GetComponent<Button>().interactable = false;
        }

    }

    void GetEnergeLevelPercent()
    {
        int EnergeLevelFull = 10;
        int missleCount = _mainModel.CountryLiderList[4].GetMissleCount();
        if (missleCount > EnergeLevelFull)
        {
            missleCount = EnergeLevelFull;
        }
        HpBar.fillAmount = (float)missleCount / EnergeLevelFull;
    }
    void GetEnergeLevelPercentBomber()
    {
        int EnergeLevelFull = 10;
        int missleCount = _mainModel.CountryLiderList[4].GetBomberCount();

        if (missleCount > EnergeLevelFull)
        {
            missleCount = EnergeLevelFull;
        }
        HpBarBomber.fillAmount = (float)missleCount / EnergeLevelFull;
    }
    void GetEnergeLevelPercentWarhead()
    {
        int EnergeLevelFull = 10;
        int missleCount = _mainModel.CountryLiderList[4].GetWarheadCount();

        if (missleCount > EnergeLevelFull)
        {
            missleCount = EnergeLevelFull;
        }
        HpBarWarhead.fillAmount = (float)missleCount / EnergeLevelFull;
    }
    void SetCityVisible(bool Visible)
    {
        
        if (_mainModel.CountryLiderList[4].GetCommandLider() != null)
        {
            CityModel cityModel = _mainModel.CountryLiderList[4].GetCommandLider().GetTargetCity();
            GameObject viewTown = new ViewTown().GetTownViewWithId(TownList, cityModel);
            City city = viewTown.GetComponent<City>();
            if (city != null)
            {
                city.SetVisible(Visible);
            }
        }
    }
    private void UpdatePanelVisible() { 
        panelMain.GetComponent<Canvas>().enabled = _visiblePanel;
        CanvasTacticReal.GetComponent<Canvas>().enabled = (_visiblePanel==false);
        
    }
    private void CanvasTacticRealSetText(string InfoText)
    {
        CanvasTacticReal.GetComponentInChildren<UnityEngine.UI.Text>().text = InfoText;
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
            Debug.Log("  _endGame  ");
            //Application.LoadLevel("Victory");
            SceneManager.LoadScene("Victory", LoadSceneMode.Single);
        }
    }
    private void SetAllCityVisibleLabelView(bool Visible)
    {
        foreach (var city in TownList)
        {
            City townCity = city.GetComponent<City>();
            townCity.SetVisibleLabel(Visible);
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

}
