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
	public GameObject WingMisslePrefabs;
	public GameObject WingMissle;
	public GameObject Airport;
	public GameObject AirportAttack;

	private bool _visibleBomber;
	private bool _attackBomber;
	private bool _attackMissle;
	private float timeBomber =0f;
	
	bool SetAnglePosition;
	
	bool _animationProcess;
	BuildingCentralModel buildingCentralModel;
	List<GameObject> TownList;

	public BuildingCentral()
    {

    }

	public void Awake()
    {
		this.buildingCentralModel = new BuildingCentralModel();

	}
    public void SetTargetBomber(CityModel target) {
		// Vector3
		this.buildingCentralModel.SetTargetBomber(target);


	}
	public void VisibleBuilding(CommandLider commandLider){

		

		Propaganda.SetActive(commandLider.VisibleProp);
		Building.SetActive(commandLider.VisibleBuild);
		DefenceObject.SetActive(commandLider.VisibleDefence);
		MissleObject.SetActive(commandLider.GetVisibleMissle());
		Airport.SetActive(commandLider.VisibleAirport);
		AirportAttack.SetActive(commandLider.VisibleAttackAirport);
		if (WingMissle!=null) {
			WingMissle.SetActive(commandLider.VisibleAttackMissle);
			MissleOpenObject.SetActive(commandLider.VisibleAttackMissle);
		}
		
		//bomber
		if(BomberObject!=null)
		{
			if(commandLider.GetVisibleBomber()||commandLider.GetVisibleAttackBomber()) {
				BomberObject.SetActive(true);
				_visibleBomber = true;
			} else {
				BomberObject.SetActive(false);
				_visibleBomber = false;
			}
		}
		_attackBomber = commandLider.GetVisibleAttackBomber();
		_attackMissle = commandLider.VisibleAttackMissle;
		
	}

    void Update()
    {
		if(_animationProcess) {
			if(_visibleBomber){
			
				
				if(_attackBomber==false){

					
					if(SetAnglePosition==false){
						BomberObject.transform.position = new Vector3(Propaganda.transform.position.x,Propaganda.transform.position.y+1,0);

						BomberObject.transform.Rotate(new Vector3(0,0,90));
						SetAnglePosition=true;
					}
					
					timeBomber +=Time.deltaTime;
		
					Vector3 pivot = new Vector3(transform.position.x,transform.position.y,0);
					float speed =100.0f;

					if (BomberObject != null)
					{
						// To rotate around the world's up axis
						BomberObject.transform.RotateAround(pivot, Vector3.forward, speed * Time.deltaTime);
					}
					
		
					
				} else {
					SendBomberAndWing(BomberObject,5);
				
					
				}
			}
			if(_attackMissle){
				
				SendBomberAndWing(WingMissle,5);
			}
		}
    }
	public void StartStateObject(List<GameObject> townList,float TimeDelete)
	{
		this._animationProcess = true;
		SetAnglePosition = false;
		WingMissle = Instantiate(WingMisslePrefabs, Propaganda.transform.position, Quaternion.identity);
		BomberObject = Instantiate(BomberObjectPrefabs, Propaganda.transform.position, Quaternion.identity);
		this.TownList = townList;
		Destroy(WingMissle, TimeDelete);
		Destroy(BomberObject, TimeDelete);
	}
	public void ResetStateObject(){
		this._animationProcess = false;
		if (WingMissle != null)
        {
			Destroy(WingMissle);
        }
		if (BomberObject != null)
		{
			Destroy(BomberObject);
		}
	}
	void SendBomberAndWing(GameObject bomberObject,int Speed) {

				float step =  Speed * Time.deltaTime; // calculate distance to move
				if (bomberObject!=null) {
					

					var offset = 260f;
					GameObject cityTown = GetTownViewWithId(this.buildingCentralModel.GetTargetBomber());
					Vector3 targetBomber = cityTown.transform.position;
					bomberObject.transform.position = Vector3.MoveTowards(bomberObject.transform.position, targetBomber, step);

					 Vector2 direction = (Vector2)targetBomber - (Vector2)transform.position;
					 direction.Normalize();
					 float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
					 bomberObject.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
					//ExplodeTown
					var kol = targetBomber - bomberObject.transform.position;
					float dist = Vector3.Distance(targetBomber, bomberObject.transform.position);
					if (dist<1.5f) {
						//draw explode
						City city = cityTown.GetComponent<City>();
						city.SetVisibleExplode(true);

						
					}
			
		}
	}
	public GameObject GetTownViewWithId(CityModel cityModel)
    {
		foreach(GameObject cityTown in this.TownList)
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
