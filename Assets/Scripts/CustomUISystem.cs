using System.IO.Enumeration;
using UnityEditor;
using UnityEngine;

public class CustomUISystem 
{
    //커스텀 UI 시스템

    public static void CreateCustomUI(string fileName)
    {
        string prefabPath = $"Assets/Prefabs/CustomUI/{fileName}.prefab"; //경로
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath); //에디터 전용 스크립트, 프로젝트 내 모든 에셋을 경로로 직접 지정해서 로드 가능

        if (prefab != null)
        {
            GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject; //프리팹 인스턴스화

            if(Selection.activeTransform != null)
            {
                instance.transform.SetParent(Selection.activeTransform, false); 
            }
            EditorGUIUtility.PingObject(instance); //생성되면 하이라키에서 강조된다.
            Undo.RegisterCreatedObjectUndo(instance, "Create " + instance.name); //언두 등록

        }
        else
        {
            Debug.LogError("Prefab not found at path: " + prefabPath); //프리팹이 없을 경우 에러
        }
    }

    [MenuItem("GameObject/CustomUI/Text", false, 1)]
    public static void CreateHelloText()
    {
        CreateCustomUI("Text");
    }

    [MenuItem("GameObject/CustomUI/Popup", false, 1)]
    public static void CreatePopup()
    {
        CreateCustomUI("Popup");
    }

    [MenuItem("GameObject/CustomUI/Button", false, 1)]
    public static void CreateButton()
    {
        CreateCustomUI("Button");
    }


    /*
    AssetDatabase => 경로로 프리펩 로드
    PrefabUtility  => 로드된 프리펩 생성
    Selection => 현재 활성화된 트랜스폼이나, 여러개를 선택해도 Objects로 가져 올 수 있다.
    EditorGUIUtility => 생성했을때 생성한 프리펩을 알려주기위해 PingObject 함수를 사용했다.
    Undo => 말그대로 Undo(실행취소) 기능을 위해 넣어주었다
    */
}
