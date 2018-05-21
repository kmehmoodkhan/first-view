using System;

namespace FirstView.BusinessLayer
{
    public class cSettings
    {
        public void UpdateByID(int SettingID, string SettingValue)
        {
            FirstView.DataAccessLayer.cSettings.UpdateByID(SettingID,SettingValue);
        }
        public void UpdateByCode(string SettingCode, string SettingValue)
        {
            FirstView.DataAccessLayer.cSettings.UpdateByCode(SettingCode, SettingValue);
        }
        public string ListByCode(string SettingCode)
        {
           return FirstView.DataAccessLayer.cSettings.ListByCode(SettingCode);
        }
        public static string LogException(Exception ex)
        {
            return FirstView.DataAccessLayer.cSettings.LogException(ex);
        }
    }
}

