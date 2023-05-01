using System.Threading.Tasks;
using Bayat.SaveSystem;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    
    [SerializeField] private Save _save;
    
    private void Awake()
    {
        ServiceLocator.SaveLoadManager = this;
    }
    
    public async Task Init()
    {
        _save = await Load();
    }
    
    private async Task<Save> Load()
    {
        if (!await SaveSystemAPI.ExistsAsync("save.bin")) 
            return new Save();
        
        return await SaveSystemAPI.LoadAsync<Save>("save.bin");
    }
    
    public void LoadCurrency()
    {
        
    }
    
    public async Task AsyncSave()
    {
        _save.SetAllData();
        await SaveSystemAPI.SaveAsync("save.bin", _save);
    }

    public void Save(bool autoSave = false)
    {
        _save.SetAllData();
        SaveSystemAPI.SaveAsync("save.bin", _save);
    }
    
    public async void Reset()
    {
        await SaveSystemAPI.DeleteAsync("save.bin");
    }
}

public class Save
{
    [SerializeField] private int _gold;
    [SerializeField] private int _maxApple;

    public void SetAllData()
    {
    }

    public int GetGold() => _gold;
    public int GetMaxApple() => _maxApple;

}