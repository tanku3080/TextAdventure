using System.Collections.Generic;
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
	//ボタンを生成するオブジェクトとポイント
	[SerializeField] GameObject buttonsParent = null;
	[SerializeField] Image charaImage = null;
	[SerializeField] Image subCharaImage = null;
	[SerializeField] Image backImage = null;
	[SerializeField] AudioSource audio = null;
	private int count = 0;
	/// <summary>imageで表示するキャラのスプライト</summary>
	private List<Sprite> imageSprits = new List<Sprite>();
	/// <summary>imageで表示する背景のスプライト</summary>
	private List<Sprite> backSprits = new List<Sprite>();

	[SerializeField, Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;

	private string currentText = string.Empty;
	private float timeUntilDisplay = 0;
	private float timeElapsed = 1;
	private int currentLine = 0;
	private int lastUpdateCharacter = -1;
	TextAsset asset;
	/// <summary>フェードが終わっているか確認するためのフラグ</summary>
	bool firstLineSeteFalg = false;

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
		charaImage.color -= new Color(0,0,255f);
		subCharaImage.color -= new Color(0,0,255f);
	}
	void LoadTextSet()
    {
		asset = Resources.Load<TextAsset>($"texts/Text{GameManager.Instance.nowChapter}");
		stringNum = asset.text;
		unit = stringNum.Split('\n');
	}
	void Update()
	{

        if (FadeAndSceneChange.Instance.FadeStop)
        {
            if (firstLineSeteFalg != true)
            {
				firstLineSeteFalg = true;
				SetNextLine();
			}
			//表示するものが無いならこの条件式に入る
			if (count == unit.Length && Input.GetKeyDown(KeyCode.Return) || count == unit.Length && Input.GetMouseButtonDown(0))
			{
				GameManager.Instance.nowChapter++;
				FadeAndSceneChange.Instance.FadeSystem(FadeAndSceneChange.FADE_STATUS.FADE_IN);
				LoadTextSet();
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

		//@はキャライメージ!は背景イメージ?は選択肢
        if (currentText.Contains("@") || currentText.Contains("!") || currentText.Contains("?"))
        {
            if (currentText.Contains("@"))
            {
				var t = currentText.Replace("@", "");
				Sprite loadImage = (Sprite)Resources.Load($"chara/{t}");
				charaImage.sprite = loadImage;
				charaImage.color += new Color(0,0,255f);
				imageSprits.Add(loadImage);
			}
            else if (currentText.Contains("!"))
            {
				var loadImage = (Sprite)Resources.Load("back/" + currentText.Replace("!",""));
				backSprits.Add(loadImage);
            }
            else if (currentText.Contains("?"))
            {
				//選択肢の表示ポイントを作る必要がある。インスタンシエイトでやる
            }
			currentLine++;
			SetNextLine();
        }
        else
        {
			timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
			timeElapsed = Time.time;
			currentLine++;
			count++;
			lastUpdateCharacter = -1;
		}
	}
}
