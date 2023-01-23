using System.Collections.Generic;
using ToolBox.Serialization;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	#region Singleton
	[SerializeField] public static SaveSystem instance;
	private void Awake()
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
	public int _money;
	public List<int> _levelStars = new List<int>();

	private const string SAVE_KEY = "PlayerSaveData";

	private void Start()
	{
		DataSerializer.FileSaving += FileSaving;

		if (DataSerializer.TryLoad<SaveData>(SAVE_KEY, out var loadedData))
		{
			_whichLevel = loadedData.WhichLevel;
			_money = loadedData.Money;
			_levelStars = loadedData.LevelStars;
		}
	}

	// This method will be called before application quits
	private void FileSaving()
	{
		DataSerializer.Save(SAVE_KEY, new SaveData(_whichLevel, _money, _levelStars));
	}
}

public struct SaveData
{
	[SerializeField] private int whichLevel;
	[SerializeField] private int money;
	[SerializeField] private List<int> levelStars;

	public int WhichLevel => whichLevel;
	public int Money => money;
	public List<int> LevelStars => levelStars;

	public SaveData(int whichLevel, int money, List<int> levelStars)
	{
		this.whichLevel = whichLevel;
		this.money = money;
		this.levelStars = levelStars;
	}
}