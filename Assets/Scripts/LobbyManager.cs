using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public AssetLabelReference defaultLabel;
    public AssetLabelReference matLabel;

    private long patchSize;
    public void ButtonClick()
    {
        StartCoroutine(CheckFileSize());
    }
    IEnumerator CheckFileSize()
    {
        List<string> labels = new List<string>() { defaultLabel.labelString, matLabel.labelString };
        patchSize = default;

        foreach (var label in labels)
        {
            var handle = Addressables.GetDownloadSizeAsync(label);

            yield return handle;

            patchSize += handle.Result;
        }
        if (patchSize > decimal.Zero)
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Down");
        }
        else
        {
            yield return new WaitForSeconds(2f);
            LoadingManager.LoadScene("Main");
        }
    }
}
