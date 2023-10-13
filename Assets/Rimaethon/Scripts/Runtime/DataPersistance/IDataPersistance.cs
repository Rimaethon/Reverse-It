namespace Rimaethon.Runtime.DataPersistance
{
    public interface IDataPersistence
    {
        void LoadData(GameSettingsData data);

        void SaveData(GameSettingsData data);
    }
}