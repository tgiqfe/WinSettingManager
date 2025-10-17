using Receiver.DataContact;
using WinSettingManager.Lib.LocalAccount;

namespace Receiver.Functions
{
    public class LocalAccountMethods
    {
        public static async Task<DataContactLocalAccount> GetLocalUsers()
        {
            return await Task.Run(() =>
            {
                return new DataContactLocalAccount()
                {
                    LocalUsers = LocalUserFunctions.GetLocalUsers().
                        Select(x => new DataContactLocalAccount.LocalUser()
                        {
                            Name = x.Name,
                            FullName = x.FullName,
                            Description = x.Description,
                            UserMustChangePasswordAtNextLogon = x.UserMustChangePasswordAtNextLogon,
                            UserCannotChangePassword = x.UserCannotChangePassword,
                            PasswordNeverExpires = x.PasswordNeverExpires,
                            AccountIsDisabled = x.AccountIsDisabled,
                            AccountIsLockedOut = x.AccountIsLockedOut,
                            JoinedGroup = x.JoinedGroup,
                            ProfilePath = x.ProfilePath,
                            LogonScript = x.LogonScript,
                            HomeDirectory = x.HomeDirectory,
                            HomeDrive = x.HomeDrive,
                            SID = x.SID
                        }).ToArray()
                };
            });
        }

        public static async Task<DataContactLocalAccount> GetLocalGroups()
        {
            return await Task.Run(() =>
            {
                return new DataContactLocalAccount()
                {
                    LocalGroups = LocalGroupFunctions.GetLocalGroups().
                        Select(x => new DataContactLocalAccount.LocalGroup()
                        {
                            Name = x.Name,
                            Description = x.Description,
                            Members = x.Members,
                            SID = x.SID
                        }).ToArray()
                };
            });
        }
    }
}
