using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

//今後の課題：選択肢を動的に増やすことが出来るようにする。シェーダーについての理解を深める。
//Gameオブジェの子オブジェクトをbuttonListに取得する
public class TextLoad : MonoBehaviour
{
	private string[] unit;
	private string stringNum;
	Text uiText;
	[SerializeField] AudioClip nextButtonSfx;
	[SerializeField] GameObject buttons;
	bool onSelect = false;

	private AudioSource audio;
	private int count = 0;


	[SerializeField, Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;

	private string currentText = string.Empty;
	private float timeUntilDisplay = 0;
	private float timeElapsed = 1;
	private int currentLine = 0;
	private int lastUpdateCharacter = -1;
	TextAsset asset;

	// 文字の表示が完了しているかどうか
	public bool IsCompleteDisplayText
	{
		get { return Time.time > timeElapsed + timeUntilDisplay; }
	}

	void Start()
	{
		uiText = this.gameObject.GetComponent<Text>();
		LoadTextSet();
		audio = this.gameObject.GetComponent<AudioSource>();
		SetNextLine();
	}
	void LoadTextSet()
    {
		asset = Resources.Load<TextAsset>($"texts/Text{GameManager.Instance.nowChapter}");
		stringNum = asset.text;
		unit = stringNum.Split('\n');
	}
	void Update()
	{
		//ボタンを作成、表示する条件式
		if (onSelect)
		{
			onSelect = false;
		}


		//表示するものが無いならこの条件式に入る
		if (count == unit.Length && Input.GetKeyDown(KeyCode.Return) || count == unit.Length && Input.GetMouseButtonDown(0))
		{
			Debug.Log("呼ばれた");
			return;
		}
		// 文字の表示が完了してるならクリック時に次の行を表示する
		if (IsCompleteDisplayText)
		{
			if (currentLine < unit.Length && Input.GetMouseButtonDown(0) || currentLine < unit.Length && Input.GetKeyDown(KeyCode.Return))
			{
				audio.PlayOneShot(nextButtonSfx);
				SetNextLine();
			}
		}
		else
		{
			// 完了してないなら文字をすべて表示する
			if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
			{
				timeUntilDisplay = 0;
			}
		}

		int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
		if (displayCharacterCount != lastUpdateCharacter)
		{
			uiText.text = currentText.Substring(0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}
	}


	/// <summary>
	/// 次の文字をセットするメソッド
	/// </summary>
	//背景、人物、選択肢を条件式で処理を行う。
	//条件式に当てはまらなかったらシーンに表示される
	//コマンド文は頭文字に! + []の形式で記載される予定
	void SetNextLine()
	{
		currentText = unit[currentLine];

		if (currentText == string.Empty) currentLine++;

		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;
		currentLine++;
		count++;
		lastUpdateCharacter = -1;
	}
}
