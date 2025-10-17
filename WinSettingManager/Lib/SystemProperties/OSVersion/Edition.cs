namespace WinSettingManager.Lib.SystemProperties.OSVersion
{
    /// <summary>
    /// Version 4.0
    /// </summary>
    public enum Edition
    {
        None,
        Home,
        Pro,
        Professional,
        Enterprise,
        EnterprisePlus,
        EnterpriseEvaluation,
        Education,
        EnterpriseLTSB,
        EnterpriseLTSC,
        ProEducation,
        ProForWorkstations,
        Standard,
        Datacenter,
        Essentials,
        EssentialPlus,
        Server,
        AdvancedServer,
        DatacenterServer,
        Web,
        ComputeCluster,
        SmallBusiness,
        StorageServer,
        Foundation,
        LTS,
        Unknown,
    }

    public class EditionHelper
    {
        public static Edition Parse(string editionString)
        {
            return editionString.ToLower() switch
            {
                "home" => Edition.Home,
                "pro" => Edition.Pro,
                "professional" => Edition.Professional,
                "enterprise" => Edition.Enterprise,
                "enterprise plus" => Edition.EnterprisePlus,
                "enterprise evaluation" => Edition.EnterpriseEvaluation,
                "education" => Edition.Education,
                "enterprise ltsb" => Edition.EnterpriseLTSB,
                "enterprise ltsc" => Edition.EnterpriseLTSC,
                "proeducation" => Edition.ProEducation,
                "pro for workstations" => Edition.ProForWorkstations,
                "standard" => Edition.Standard,
                "datacenter" => Edition.Datacenter,
                "essentials" => Edition.Essentials,
                "essential plus" => Edition.EssentialPlus,
                "server" => Edition.Server,
                "advanced server" => Edition.AdvancedServer,
                "datacenter server" => Edition.DatacenterServer,
                "web" => Edition.Web,
                "compute cluster" => Edition.ComputeCluster,
                "small business" => Edition.SmallBusiness,
                "storage server" => Edition.StorageServer,
                "foundation" => Edition.Foundation,
                "lts" => Edition.LTS,
                _ => Edition.Unknown,
            };
        }
    }
}
