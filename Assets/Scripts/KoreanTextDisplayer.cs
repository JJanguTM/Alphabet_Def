using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class KoreanTextDisplayer : MonoBehaviour
{
    public Text displayText;
    private int currentIndex;

    void Start()
    {
        WordInventoryManager.OnSeedGenerated += LoadKoreanText;
    }

    void LoadKoreanText(int seed)
    {
        string jsonFileName = "Word"; // JSON 파일 이름
        string jsonFilePath = Path.Combine("Assets/Resources", jsonFileName + ".json"); // 파일 경로

        // Resources 폴더에 있는 JSON 파일 읽어오기
        TextAsset jsonTextFile = Resources.Load<TextAsset>(jsonFileName);
        if (jsonTextFile == null)
        {
            Debug.LogError($"JSON file not found: {jsonFileName}");
            return;
        }
        
        // JSON 파일 내용 파싱
        WordSlot wordData = JsonUtility.FromJson<WordSlot>(jsonTextFile.text);

        // seed 값으로 currentIndex 설정
        currentIndex = seed % wordData.Word.Length;

        // currentIndex에 해당하는 한글 텍스트 출력
        string randomKorean = wordData.Word[currentIndex].Korean;
        displayText.text = randomKorean;
        Debug.Log($"Displayed Korean text: {randomKorean}, Korean index: {currentIndex}");
    }
}
