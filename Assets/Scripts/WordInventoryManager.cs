using System;
using System.Collections.Generic;
using UnityEngine;

public class WordInventoryManager : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();
    public GameObject slotPrefab;

    // 이벤트 정의: 시드 값이 생성될 때마다 호출될 이벤트
    public static event Action<int> OnSeedGenerated;

    void Start()
    {
        // 처음에 랜덤한 값으로 currentIndex 설정
        int currentIndex = UnityEngine.Random.Range(0, 100); // 예를 들어 0부터 9까지의 랜덤한 값

        LoadNextWord(currentIndex);
    }

    public void LoadNextWord(int currentIndex)
    {
        TextAsset jsonTextFile = Resources.Load<TextAsset>("Word"); // 리소스에서 JSON 파일 로드
        if (jsonTextFile == null)
        {
            Debug.LogError("Word.json file not found in Resources folder.");
            return;
        }

        WordSlot wordData = JsonUtility.FromJson<WordSlot>(jsonTextFile.text);
        List<int> availableIndices = new List<int>(); // 사용 가능한 인덱스를 저장할 리스트

        // 상태가 false인 인덱스를 리스트에 추가
        for (int i = 0; i < wordData.Word.Length; i++)
        {
            if (!wordData.Word[i].State)
            {
                availableIndices.Add(i);
            }
        }

        // 사용 가능한 인덱스 중에서 랜덤하게 선택
        int randomIndex = UnityEngine.Random.Range(0, availableIndices.Count);
        currentIndex = availableIndices[randomIndex];

        string word = wordData.Word[currentIndex].Spell;
        GenerateSlots(word);
        Debug.Log("Current Index: " + currentIndex);

        // 이벤트를 발생시켜 currentIndex 값을 전달
        OnSeedGenerated?.Invoke(currentIndex);
    }

    public bool AllSlotsFilled()
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.isEmpty)
            {
                return false;
            }
        }
        return true;
    }

    public void NextWord()
    {
        // 모든 슬롯 초기화
        foreach (InventorySlot slot in slots)
        {
            Destroy(slot.slotObj);
        }
        slots.Clear();

        // 다음 단어 출력
        int currentIndex = 0; // 이 부분은 필요에 따라서 currentIndex 값 설정
        LoadNextWord(currentIndex);
    }

    void GenerateSlots(string word)
    {
        GameObject slotPanel = GameObject.Find("WordPanel");
        for (int i = 0; i < word.Length; i++)
        {
            GameObject go = Instantiate(slotPrefab, slotPanel.transform, false);
            go.name = "Slot_" + i;
            InventorySlot slot = new InventorySlot();
            slot.isEmpty = true;
            slot.slotObj = go;
            slots.Add(slot);
            go.tag = word[i].ToString();
        }
    }
}