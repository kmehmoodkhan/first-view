using System;
using System.Data;
using System.Data.Common;
using FirstView.Model;
using MSSQL.DBHelper;
namespace FirstView.DataAccessLayer
{
    public class cUsers
    {
        public static int Register(string Name ,string Surname ,string Username ,string Password ,string EmailAddress, string VerificationCode,
            string Address, string Town, string PostCode, string Telephone, string Mobile, string BankSortCode, string BankAccountNumber)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            int ArtistID = 0;
            db.AddParameter("@Name", Name);
            db.AddParameter("@Surname", Surname);
            db.AddParameter("@Username", Username);
            db.AddParameter("@Password", Password);
            db.AddParameter("@EmailAddress", EmailAddress);
            db.AddParameter("@VerificationCode", VerificationCode);
            db.AddParameter("@Address", Address);
            db.AddParameter("@Town", Town);
            db.AddParameter("@PostCode", PostCode);
            db.AddParameter("@Telephone", Telephone);
            db.AddParameter("@Mobile", Mobile);
            db.AddParameter("@BankSortCode", BankSortCode);
            db.AddParameter("@BankAccountNumber", BankAccountNumber);
            ArtistID = Convert.ToInt32(db.ExecuteScalar("usp_Users_RegisterNewUser", System.Data.CommandType.StoredProcedure));
            return ArtistID;
        }
        public static int RegisterArtist(string Name, string Surname, string Username, string Password, string EmailAddress, string VerificationCode,
            string Address, string Town, string PostCode, string Telephone, string Mobile, string BankSortCode, string BankAccountNumber)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            int ArtistID = 0;
            db.AddParameter("@Name", Name);
            db.AddParameter("@Surname", Surname);
            db.AddParameter("@Username", Username);
            db.AddParameter("@Password", Password);
            db.AddParameter("@EmailAddress", EmailAddress);
            db.AddParameter("@VerificationCode", VerificationCode);
            db.AddParameter("@Address", Address);
            db.AddParameter("@Town", Town);
            db.AddParameter("@PostCode", PostCode);
            db.AddParameter("@Telephone", Telephone);
            db.AddParameter("@Mobile", Mobile);
            db.AddParameter("@BankSortCode", BankSortCode);
            db.AddParameter("@BankAccountNumber", BankAccountNumber);
            ArtistID = Convert.ToInt32(db.ExecuteScalar("usp_Users_RegisterNewArtist", System.Data.CommandType.StoredProcedure));
            return ArtistID;
        }
        public static bool CheckUsernameExists(string Username, string email)
        {
            bool Exists = false;
            DbDataReader reader;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@Username", Username);
            db.AddParameter("@Email", email);
            reader = db.ExecuteReader("usp_Users_CheckUsernameExistsForForgetPw", System.Data.CommandType.StoredProcedure);
            if (reader.HasRows)
            {
                Exists = true;
            }
            return Exists;
        }

        public static int RegisterCheckVerify(string VerificationCode)
        {
            int IsVerified = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@VerificationCode", VerificationCode);
            IsVerified = Convert.ToInt32(db.ExecuteScalar("usp_Users_RegisterCheckVerify", System.Data.CommandType.StoredProcedure));
            return IsVerified;
        }

        public static void RegisterVerify(string VerificationCode)
        {
            int i = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@VerificationCode", VerificationCode);
            i= db.ExecuteNonQuery("usp_Users_RegisterVerify", System.Data.CommandType.StoredProcedure);
        }

        public static void ChangePassword(int UserID, string Password)
        {
            int i = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@UserID", UserID);
            db.AddParameter("@Password", Password);
            i = db.ExecuteNonQuery("usp_Users_ChangePassword", System.Data.CommandType.StoredProcedure);
        }

        public static bool CheckUsernameExists(string Username, int userId)
        {
            bool Exists = false;
            DbDataReader reader;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@Username", Username);
            db.AddParameter("@UserId", userId);
            reader = db.ExecuteReader("usp_Users_CheckUsernameExistsForUpdate", System.Data.CommandType.StoredProcedure);
            if (reader.HasRows)
            {
                Exists = true;
            }
            return Exists;
        }

        public static bool CheckUsernameExists(string Username)
        {
            bool Exists = false;
            DbDataReader reader;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DataView dv = new DataView();
            db.AddParameter("@Username", Username);
            reader = db.ExecuteReader("usp_Users_CheckUsernameExists", System.Data.CommandType.StoredProcedure);
            if (reader.HasRows)
            {
                Exists = true;
            }
            return Exists;
        }

        public static bool DoAuthenticate(string Username, string Password)
        {
            bool isValid = false;
            DataView dv = new DataView();
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            DbDataReader reader;
            db.AddParameter("@Username", Username);
            db.AddParameter("@Password", Password);
            reader = db.ExecuteReader("usp_Users_Login", System.Data.CommandType.StoredProcedure);
            if (reader.HasRows == true)
            {
                isValid = true;
            }
            reader.Close();
            reader.Dispose();
            return isValid;
        }
        public static DataView ListForLogin(string Username, string Password)
        {
            DataView dv = new DataView();
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@Username", Username);
            db.AddParameter("@Password", Password);
            dv = db.ExecuteDataView("usp_Users_Login", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static void RecoveryForgotPassword(string EmailAddress, string Username, string NewPassword, string DateTimeNow, string TempPasswordKey)
        {
            int i = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@EmailAddress", EmailAddress);
            db.AddParameter("@Username", Username);
            db.AddParameter("@TempPassword", NewPassword);
            db.AddParameter("@DateTimeNow", DateTimeNow);
            db.AddParameter("@TempPasswordKey", TempPasswordKey);
            i = db.ExecuteNonQuery("usp_Users_RecoveryForgotPassword", System.Data.CommandType.StoredProcedure);
        }

        public static DataSet List(bool? IsDeleted, string Name,string Pattern)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@IsDeleted", IsDeleted);
            db.AddParameter("@Name", Name);
            db.AddParameter("@Pattern", Pattern);
            DataSet ds = db.ExecuteDataSet("usp_Users_ListAll", System.Data.CommandType.StoredProcedure);
            return ds;
        }

        public static DataView ListByID(int UserID)
        {
            DataView dv = new DataView();
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@UserID", UserID);
            dv = db.ExecuteDataView("usp_Users_ListByID", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static DataView ListByArtistID(int ArtistID)
        {
            DataView dv = new DataView();
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@ArtistID", ArtistID);
            dv = db.ExecuteDataView("usp_Users_ListByArtistID", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static void Add(string Name, string Surname, string EmailAddress, string Username, string Password, bool IsActive, bool IsAdmin,
            string Address, string Town, string PostCode, string Telephone, string Mobile,string BankSortCode, string BankAccountNumber)
        {
            int i = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@Name", Name);
            db.AddParameter("@Surname", Surname);
            db.AddParameter("@EmailAddress", EmailAddress);
            db.AddParameter("@Username", Username);
            db.AddParameter("@Password", Password);
            db.AddParameter("@IsActive", IsActive);
            db.AddParameter("@IsAdmin", IsAdmin);
            db.AddParameter("@Address", Address);
            db.AddParameter("@Town", Town);
            db.AddParameter("@PostCode", PostCode);
            db.AddParameter("@Telephone", Telephone);
            db.AddParameter("@Mobile", Mobile);
            db.AddParameter("@BankSortCode", BankSortCode);
            db.AddParameter("@BankAccountNumber", BankAccountNumber);
            i = db.ExecuteNonQuery("usp_Users_Add", System.Data.CommandType.StoredProcedure);
        }

        public static int Add2(string Name, string Surname, string EmailAddress, string Username, string Password, bool IsActive, bool IsAdmin, bool IsArtist,
            string Address, string Town, string PostCode, string Telephone, string Mobile, string BankSortCode, string BankAccountNumber)
        {
            int ArtistID = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@Name", Name);
            db.AddParameter("@Surname", Surname);
            db.AddParameter("@EmailAddress", EmailAddress);
            db.AddParameter("@Username", Username);
            db.AddParameter("@Password", Password);
            db.AddParameter("@IsActive", IsActive);
            db.AddParameter("@IsAdmin", IsAdmin);
            db.AddParameter("@IsArtist", IsArtist);
            db.AddParameter("@Address", Address);
            db.AddParameter("@Town", Town);
            db.AddParameter("@PostCode", PostCode);
            db.AddParameter("@Telephone", Telephone);
            db.AddParameter("@Mobile", Mobile);
            db.AddParameter("@BankSortCode", BankSortCode);
            db.AddParameter("@BankAccountNumber", BankAccountNumber);
            ArtistID = Convert.ToInt32(db.ExecuteScalar("usp_Users_Add2", System.Data.CommandType.StoredProcedure));
            return ArtistID;
        }

        public static void Edit(int UserID, string Name, string Surname, string EmailAddress, bool ChangePass,
            string Password, bool IsActive, bool IsAdmin, bool IsVerified,
            string Address, string Town, string PostCode, string Telephone, string Mobile, string BankSortCode, string BankAccountNumber)
        {
            int i = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@UserID", UserID);
            db.AddParameter("@Name", Name);
            db.AddParameter("@Surname", Surname);
            db.AddParameter("@EmailAddress", EmailAddress);
            db.AddParameter("@ChangePass", ChangePass);
            if (ChangePass == true)
            { db.AddParameter("@Password", Password); }
            else
            { db.AddParameter("@Password", ""); }
            db.AddParameter("@IsActive", IsActive);
            db.AddParameter("@IsAdmin", IsAdmin);
            db.AddParameter("@IsVerified", IsVerified);
            db.AddParameter("@Address", Address);
            db.AddParameter("@Town", Town);
            db.AddParameter("@PostCode", PostCode);
            db.AddParameter("@Telephone", Telephone);
            db.AddParameter("@Mobile", Mobile);
            db.AddParameter("@BankSortCode", BankSortCode);
            db.AddParameter("@BankAccountNumber", BankAccountNumber);
            i = db.ExecuteNonQuery("usp_Users_Edit", System.Data.CommandType.StoredProcedure);
        }

        public static void Edit(UserModel objUserModel)
        {
            int i = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);           
            db.AddParameter("@UserID", objUserModel.UserID);
            db.AddParameter("@Name", objUserModel.Name);
            db.AddParameter("@Username", objUserModel.Username);
            db.AddParameter("@Surname", objUserModel.Surname);
            db.AddParameter("@EmailAddress", objUserModel.Email);
            db.AddParameter("@Password", objUserModel.Password); 
            db.AddParameter("@Address", objUserModel.Address);
            db.AddParameter("@Town", objUserModel.Town);
            db.AddParameter("@PostCode", objUserModel.ZipCode);
            db.AddParameter("@Telephone", objUserModel.Telephone);
            db.AddParameter("@Mobile", objUserModel.Mobile);
            db.AddParameter("@BankSortCode", objUserModel.BankSortCode);
            db.AddParameter("@BankAccountNumber", objUserModel.BankAccountNumber);
            i = db.ExecuteNonQuery("usp_Users_EditPersonalInfo", System.Data.CommandType.StoredProcedure);
        }

        public static DataView RecoveryByEmail(string EmailAddress)
        {
            DataView dv = new DataView();
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@EmailAddress", EmailAddress);
            dv = db.ExecuteDataView("usp_Users_RecoveryByEmailAddress", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static DataView ListAllAdmins()
        {
            DataView dv = new DataView();
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            dv = db.ExecuteDataView("usp_Users_ListAllAdmin", System.Data.CommandType.StoredProcedure);
            return dv;
        }

        public static void RecoveryTempPassword(string EmailAddress, string NewPassword, string DateTimeNow, string TempPasswordKey)
        {
            int i = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@EmailAddress", EmailAddress);
            db.AddParameter("@TempPassword", NewPassword);
            db.AddParameter("@DateTimeNow", DateTimeNow);
            db.AddParameter("@TempPasswordKey", TempPasswordKey);
            i = db.ExecuteNonQuery("usp_Users_RecoveryTempPassword", System.Data.CommandType.StoredProcedure);
        }

        public static int SetNewPassword(string TempPassword, string TempPasswordKey, string NewPassword)
        {
            int Result = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);           
            db.AddParameter("@TempPassword", TempPassword);
            db.AddParameter("@TempPasswordKey", TempPasswordKey);
            db.AddParameter("@NewPassword", NewPassword);
            Result = Convert.ToInt32(db.ExecuteScalar("usp_Users_RecoverySetNewPassword", System.Data.CommandType.StoredProcedure));
            return Result;
        }

        public static int UndoPasswordReset(string TempPasswordKey)
        {
            int Result = 0;
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            db.AddParameter("@TempPasswordKey", TempPasswordKey);
            Result = Convert.ToInt32(db.ExecuteScalar("usp_Users_RecoveryUndoReset", System.Data.CommandType.StoredProcedure));
            return Result;
        }
        public static string Delete(int UserId, string LastModifiedUser)
        {
            DBHelper db = new DBHelper(DBHelper.ConnectionStr.DefaultConnection);
            string result = string.Empty;
            db.AddParameter("@UserId", UserId);
            db.AddParameter("@LastModifiedUser", LastModifiedUser);
            result =Convert.ToString(db.ExecuteScalar("usp_User_Delete", System.Data.CommandType.StoredProcedure));
            return result;
        }
    }
}

