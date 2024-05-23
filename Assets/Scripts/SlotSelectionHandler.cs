using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSelectionHandler : MonoBehaviour
{
    public GameObject[] slotAlph;
    public WordInventoryManager right;

    void Start()
    {
        if (right == null)
        {
            Debug.LogError("WordInventoryManager 컴포넌트를 찾을 수 없습니다.");
        } 
    }

    void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        ProcessMouseClick();
    }
}

void ProcessMouseClick()
{
    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    if (hit.collider == null)
    {
        return;
    }

    string tag = hit.collider.gameObject.tag;
    InventorySlot emptySlot = FindEmptySlotWithTag(tag);
    if (emptySlot == null)
    {
        Debug.LogWarning("해당 태그를 가진 빈 슬롯이 없습니다.");
        return;
    }

    PlaceAlphabetInSlot(hit, emptySlot);
}

void PlaceAlphabetInSlot(RaycastHit2D hit, InventorySlot emptySlot)
{
    GameObject alph = Instantiate(GetRandomAlphabetWithTag(hit.collider.gameObject.tag), emptySlot.slotObj.transform, false);
    alph.transform.localPosition = Vector3.zero;
    emptySlot.isEmpty = false;

    Destroy(hit.collider.gameObject);

    if (right.AllSlotsFilled())
    {
        right.NextWord();
    }
}

    // 해당 태그를 가진 빈 슬롯을 찾아 반환
    InventorySlot FindEmptySlotWithTag(string tag)
    {
        foreach (InventorySlot slot in right.slots)
        {
            if (slot.slotObj.tag == tag && slot.isEmpty)
            {
                return slot;
            }
        }
        return null;
    }

    // 해당 태그와 일치하는 알파벳을 무작위로 선택하여 반환
    GameObject GetRandomAlphabetWithTag(string tag)
    {
        List<GameObject> candidates = new List<GameObject>();
        foreach (GameObject alph in slotAlph)
        {
            if (alph.tag == tag)
            {
                candidates.Add(alph);
            }
        }

        if (candidates.Count > 0)
        {
            return candidates[Random.Range(0, candidates.Count)];
        }
        else
        {
            Debug.LogWarning("태그와 동일한 태그를 가진 오브젝트가 없습니다: " + tag);
            return null;
        }
    }
}
