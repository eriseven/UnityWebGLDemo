using System;
using System.Collections;
using System.Collections.Generic;
using IngameDebugConsole;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SpineLoader : MonoBehaviour
{
    [SerializeField]
    private AssetReference spineAsset;

    private void Awake()
    {
        DebugLogConsole.AddCommand<string>("spine.load", "", LoadSpine);
    }

    void LoadSpine(string key)
    {
        Addressables.InstantiateAsync(key);
    }
}
