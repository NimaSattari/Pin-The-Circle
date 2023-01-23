using System.Collections.Generic;
using ToolBox.Serialization;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	#region Singleton
	[SerializeField] public static SaveSystem instance;
	private void Start()
	{
		if (SaveSystem.instance == null)
		{
			SaveSystem.instance = this;
		}
		else
		{
			if (SaveSystem.instance != this)
			{
				Destroy(this.gameObject);
			}
		}
		DontDestroyOnLoad(this.gameObject);
	}
	#endregion

	public int _whichLevel;
	public int _endlessModeFruitsKilledRecord;
	public int _timeTrailFruitKilledRecord;
	public int _money;
	public List<int> _levelStars = new List<int>();

	private const string SAVE_KEY = "PlayerSaveData";

	private void Awake()
	{
		DataSerializer.FileSaving += FileSaving;

		if (DataSerializer.TryLoad<SaveData>(SAVE_KEY, out var loadedData))
		{
			_whichLevel = loadedData.WhichLevel;
			_endlessModeFruitsKilledRecord = loadedData.EndlessModeFruitsKilledRecord;
			_timeTrailFruitKilledRecord = loadedData.TimeTrailFruitKilledRecord;
			_money = loadedData.Money;
			_levelStars = loadedData.LevelStars;
		}
	}

	// This method will be called before application quits
	public void FileSaving()
	{
		DataSerializer.Save(SAVE_KEY, new SaveData(_whichLevel, _endlessModeFruitsKilledRecord, _timeTrailFruitKilledRecord, _money, _levelStars));
	}
}

public struct SaveData
{
	[SerializeField] private int whichLevel;
	[SerializeField] private int endlessModeFruitsKilledRecord;
	[SerializeField] private int timeTrailFruitKilledRecord;
	[SerializeField] private int money;
	[SerializeField] private List<int> levelStars;

	public int WhichLevel => whichLevel;
	public int EndlessModeFruitsKilledRecord => endlessModeFruitsKilledRecord;
	public int TimeTrailFruitKilledRecord => timeTrailFruitKilledRecord;
	public int Money => money;
	public List<int> LevelStars => levelStars;

	public SaveData(int whichLevel, int endlessModeFruitsKilledRecord, int timeTrailFruitKilledRecord, int money, List<int> levelStars)
	{
		this.whichLevel = whichLevel;
		this.endlessModeFruitsKilledRecord = endlessModeFruitsKilledRecord;
		this.timeTrailFruitKilledRecord = timeTrailFruitKilledRecord;
		this.money = money;
		this.levelStars = levelStars;
	}
}