using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using FirstView.Model;
namespace FirstView.BusinessLayer
{
    public class cUsers
    {
        public int Register(string Name, string Surname, string Username, string Password, string EmailAddress, string VerificationCode,
            string Address, string Town, string PostCode, string Telephone, string Mobile,string BankSortCode,string BankAccountNumber)
        {
            return FirstView.DataAccessLayer.cUsers.Register(Name, Surname, Username, Hash(Password), EmailAddress, VerificationCode, Address, Town, PostCode, Telephone, Mobile,BankSortCode,BankAccountNumber);
        }
        public int RegisterArtist(string Name, string Surname, string Username, string Password, string EmailAddress, string VerificationCode,
           string Address, string Town, string PostCode, string Telephone, string Mobile, string BankSortCode, string BankAccountNumber)
        {
            return FirstView.DataAccessLayer.cUsers.RegisterArtist(Name, Surname, Username, Hash(Password), EmailAddress, VerificationCode, Address, Town, PostCode, Telephone, Mobile, BankSortCode, BankAccountNumber);
        }
        public int RegisterCheckVerify(string VerificationCode)
        {
            return FirstView.DataAccessLayer.cUsers.RegisterCheckVerify(VerificationCode);
        }
        public void RegisterVerify(string VerificationCode)
        {
            FirstView.DataAccessLayer.cUsers.RegisterVerify(VerificationCode);
        }

        public bool CheckUsernameExists(string Username, string email)
        {
            return FirstView.DataAccessLayer.cUsers.CheckUsernameExists(Username, email);
        }

        public void ChangePassword(int UserID, string Password)
        {
            FirstView.DataAccessLayer.cUsers.ChangePassword(UserID, Hash(Password));
        }
        public bool CheckUsernameExists(string Username, int userId)
        {
            return FirstView.DataAccessLayer.cUsers.CheckUsernameExists(Username, userId);
        }
        public bool CheckUsernameExists(string Username)
        {
            return FirstView.DataAccessLayer.cUsers.CheckUsernameExists(Username);
        }
        public bool DoAuthenticate(string Username, string Password)
        {
            return FirstView.DataAccessLayer.cUsers.DoAuthenticate(Username, Hash(Password));
        }
        public string Hash(string ToHash)
        {
            if (ToHash.Length > 0)
            {
                Encoder enc = System.Text.Encoding.ASCII.GetEncoder();

                byte[] data = new byte[ToHash.Length];
                enc.GetBytes(ToHash.ToCharArray(), 0, ToHash.Length, data, 0, true);

                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] result = md5.ComputeHash(data);

                return BitConverter.ToString(result).Replace("-", "").ToLower();
            }
            else
            {
                return "";
            }
        }
        public DataView ListForLogin(string Username, string Password)
        {
            return FirstView.DataAccessLayer.cUsers.ListForLogin(Username, Hash(Password));
        }
        public DataView List(bool? IsDeleted, string Name)
        {
            return FirstView.DataAccessLayer.cUsers.List(IsDeleted, Name);
        }
        public DataView ListByID(int UserID)
        {
            return FirstView.DataAccessLayer.cUsers.ListByID(UserID);
        }
        public DataView ListByArtistID(int ArtistID)
        {
            return FirstView.DataAccessLayer.cUsers.ListByArtistID(ArtistID);
        }
        public void Add(string Name, string Surname, string EmailAddress, string Username, string Password, bool IsActive, bool IsAdmin,
            string Address, string Town, string PostCode, string Telephone, string Mobile, string BankSortCode, string BankAccountNumber)
        {
            FirstView.DataAccessLayer.cUsers.Add(Name, Surname, EmailAddress, Username, Hash(Password), IsActive, IsAdmin, Address, Town, PostCode, Telephone, Mobile, BankSortCode, BankAccountNumber);
        }
        public int Add2(string Name, string Surname, string EmailAddress, string Username, string Password, bool IsActive, bool IsAdmin, bool IsArtist,
            string Address, string Town, string PostCode, string Telephone, string Mobile, string BankSortCode, string BankAccountNumber)
        {
            return FirstView.DataAccessLayer.cUsers.Add2(Name, Surname, EmailAddress, Username, Hash(Password), IsActive, IsAdmin, IsArtist, Address, Town, PostCode, Telephone, Mobile, BankSortCode, BankAccountNumber);
        }
        public void Edit(int UserID, string Name, string Surname, string EmailAddress, bool ChangePass,
            string Password, bool IsActive, bool IsAdmin, bool IsVerified,
            string Address, string Town, string PostCode, string Telephone, string Mobile, string BankSortCode, string BankAccountNumber)
        {
            FirstView.DataAccessLayer.cUsers.Edit(UserID, Name, Surname, EmailAddress, ChangePass, Hash(Password), IsActive, IsAdmin, IsVerified, Address, Town, PostCode, Telephone, Mobile, BankSortCode, BankAccountNumber);
        }
        public void Edit(UserModel objUserModel)
        {
            objUserModel.Password = Hash(objUserModel.Password);
            FirstView.DataAccessLayer.cUsers.Edit(objUserModel);
        }
        public DataView RecoveryByEmail(string EmailAddress)
        {
            return FirstView.DataAccessLayer.cUsers.RecoveryByEmail(EmailAddress);
        }
        public DataView ListAllAdmins()
        {
            return FirstView.DataAccessLayer.cUsers.ListAllAdmins();
        }
        public void RecoveryTempPassword(string EmailAddress, string NewPassword, string DateTimeNow, string TempPasswordKey)
        {
            FirstView.DataAccessLayer.cUsers.RecoveryTempPassword(EmailAddress, Hash(NewPassword), DateTimeNow, TempPasswordKey);
        }
        public void RecoveryForgotPassword(string EmailAddress,string Username, string NewPassword, string DateTimeNow, string TempPasswordKey)
        {
            FirstView.DataAccessLayer.cUsers.RecoveryForgotPassword(EmailAddress, Username, Hash(NewPassword), DateTimeNow, TempPasswordKey);
        }
        public int SetNewPassword(string TempPassword, string TempPasswordKey, string NewPassword)
        {
            return FirstView.DataAccessLayer.cUsers.SetNewPassword(Hash(TempPassword), TempPasswordKey, Hash(NewPassword));
        }
        public int UndoPasswordReset(string TempPasswordKey)
        {
            return FirstView.DataAccessLayer.cUsers.UndoPasswordReset(TempPasswordKey);
        }
        public string Delete(int UserId, string LastModifiedUser)
        {
            return FirstView.DataAccessLayer.cUsers.Delete(UserId, LastModifiedUser);
        }
    }
}

