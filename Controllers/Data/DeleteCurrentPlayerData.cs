using System.IO;
using UnityEngine;

namespace Controllers.Data
{
    public class DeleteCurrentPlayerData : MonoBehaviour
    {
        public void DeletePlayerData()
        {
            // Check if the file exists
            if (SaveManager.SaveExist("SavePlayerDataState"))
            {
                // Delete the file
                PlayerPrefs.DeleteAll();
                File.Delete(Application.persistentDataPath+"/save/"+ "SavePlayerDataState" + ".connectinnoSave");
                Debug.Log("GameData has been deleted.");

                // Create a new empty GameData object
                PlayerDataState emptyData = new PlayerDataState();
                // Save the empty GameData to the same file to replace the deleted data
                SaveManager.SaveData(emptyData, "SavePlayerDataState");
            }
            else
            {
                Debug.Log("No GameData file found.");
            }
        }
    }
}