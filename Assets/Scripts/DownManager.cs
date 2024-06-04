using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.SearchService;
using UnityEngine.UI;

public class DownManager : MonoBehaviour
{
    public GameObject waitMessage;
    public GameObject downMessage;

    public Slider downSlider;
    public TextMeshProUGUI sizeInfoText;
    public TextMeshProUGUI downValText;

    public AssetLabelReference defaultLabel;
    public AssetLabelReference matLabel;

    private long patchSize;
    private Dictionary<string,long> patchDic = new Dictionary<string,long>();

    private void Start()
    {
        waitMessage.SetActive(true);
        downMessage.SetActive(false);
        StartCoroutine(InitAddressabled());
        StartCoroutine(CheckUpdateFiles());
    }
    IEnumerator InitAddressabled()
    {
        var init = Addressables.InitializeAsync();
        yield return init;
    }
    IEnumerator CheckUpdateFiles()
    {
        List<string> labels = new List<string>() { defaultLabel.labelString, matLabel.labelString };
        patchSize = default;

        foreach (var label in labels)
        {
            var handle = Addressables.GetDownloadSizeAsync(label);

            yield return handle;

            patchSize += handle.Result;
            
        }
        if(patchSize > decimal.Zero)
        {
            waitMessage.SetActive(false);
            downMessage.SetActive(true);

            sizeInfoText.text = GetFileSize(patchSize);
        }
    }
    private string GetFileSize(long byteCnt)
    {
        string size = "0 bytes";

        if(byteCnt >= 1073741824.0)
        {
            size = string.Format("{0:##.##}", byteCnt / 1073741824.0) + " GB";
        }
        else if (byteCnt >= 1048576.0)
        {
            size = string.Format("{0:##.##}", byteCnt / 1048576.0) + " MB";
        }
        else if (byteCnt >= 1024.0)
        {
            size = string.Format("{0:##.##}", byteCnt / 1024.0) + " KB";
        }
        else if (byteCnt > 0 && byteCnt < 1024.0)
        {
            size = byteCnt.ToString() + "Bytes";
        }
        return size;
    }

    public void ButtonDownLoad()
    {
        StartCoroutine(PatchFiles());
    }
    IEnumerator PatchFiles()
    {
        List<string> labels = new List<string>() { defaultLabel.labelString, matLabel.labelString };

        foreach (var label in labels)
        {
            var handle = Addressables.GetDownloadSizeAsync(label);

            yield return handle;

            if(handle.Result != decimal.Zero)
            {
                StartCoroutine(DownLoadLabel(label));
            }
        }

        yield return CheckDownLoad();
    }
    IEnumerator DownLoadLabel(string label)
    {
        patchDic.Add(label, 0);

        var handle = Addressables.DownloadDependenciesAsync(label, false);
        while (!handle.IsDone)
        {
            patchDic[label] = handle.GetDownloadStatus().DownloadedBytes;
            yield return new WaitForEndOfFrame();
        }
        patchDic[label] = handle.GetDownloadStatus().TotalBytes;
        Addressables.Release(handle);
    }

    IEnumerator CheckDownLoad()
    {
        var total = 0f;

        downValText.text = "0 %";

        while (true)
        {
            total += patchDic.Sum(tmp => tmp.Value);

            downSlider.value = total / patchSize;
            downValText.text = (int)(downSlider.value * 100) + " %";
            if(total == patchSize)
            {
                LoadingManager.LoadScene("Main");
                break;
            }
            total = 0f;
            yield return new WaitForEndOfFrame();
        }
    }
}
