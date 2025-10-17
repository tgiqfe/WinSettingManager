using Receiver.DataContact;
using WinSettingManager.Lib.LocalAccount;

namespace Receiver.Functions
{
    public class LocalAccountMethods
    {
        public static async Task<LocalAccountDataContact> GetLocalUsers()
        {
            return await Task.Run(() =>
            {
                return new LocalAccountDataContact()
                {
                    LocalUsers = LocalUser.Load().
                        Select(x => new LocalAccountDataContact.LocalUser()
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

        public static async Task<LocalAccountDataContact> GetLocalGroups()
        {
            return await Task.Run(() =>
            {
                return new LocalAccountDataContact()
                {
                    LocalGroups = LocalGroup.Load().
                        Select(x => new LocalAccountDataContact.LocalGroup()
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
