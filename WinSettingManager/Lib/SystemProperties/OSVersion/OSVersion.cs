namespace WinSettingManager.Lib.SystemProperties.OSVersion
{
    /// <summary>
    /// Version 4.0
    /// </summary>
    public partial class OSVersion
    {
        /// <summary>
        /// OS type. [Windows/Linux/Mac/Any]
        /// </summary>
        public OSFamily OSFamily { get; set; }

        /// <summary>
        /// OS name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// OS name alias.
        /// </summary>
        public string[] Alias { get; set; }

        /// <summary>
        /// Version name.
        /// </summary>
        public string VersionName { get; set; }

        /// <summary>
        /// Version name alias.
        /// </summary>
        public string[] VersionAlias { get; set; }

        /// <summary>
        /// OS edition.
        /// </summary>
        public Edition? Edition { get; set; }

        /// <summary>
        /// server / not server
        /// </summary>
        public bool? ServerOS { get; set; }

        /// <summary>
        /// embedded os / not embedded os
        /// </summary>
        public bool? EmbeddedOS { get; set; }

        /// <summary>
        /// for simple version compare.
        /// </summary>
        public int Serial { get; set; }

        #region operator

        #region <

        /// <summary>
        /// 小なりoperator。両方OSCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <(OSVersion x, OSVersion y)
        {
            return x is not null && y is not null ? ((x.Name == y.Name || x.OSFamily == OSFamily.Any || y.OSFamily == OSFamily.Any) && x.Serial < y.Serial) : false;
        }

        /// <summary>
        /// 小なりoperator。左辺のみOSCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <(OSVersion x, int y) { return x is not null ? x.Serial < y : false; }

        /// <summary>
        /// 小なりoperator。右辺のみOSCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <(int x, OSVersion y) { return y is not null ? x < y.Serial : false; }

        #endregion
        #region >

        /// <summary>
        /// 大なりoperator。両方OSCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >(OSVersion x, OSVersion y)
        {
            return x is not null && y is not null ? ((x.Name == y.Name || x.OSFamily == OSFamily.Any || y.OSFamily == OSFamily.Any) && x.Serial > y.Serial) : false;
        }

        /// <summary>
        /// 大なりoperator。左辺のみOSCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >(OSVersion x, int y) { return x is not null ? x.Serial > y : false; }

        /// <summary>
        /// 大なりoperator。右辺のみOSCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >(int x, OSVersion y) { return y is not null ? x > y.Serial : false; }


        #endregion
        #region <=

        /// <summary>
        /// 小なりイコールoperator。両方OSCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <=(OSVersion x, OSVersion y)
        {
            return x is not null && y is not null ? ((x.Name == y.Name || x.OSFamily == OSFamily.Any || y.OSFamily == OSFamily.Any) && x.Serial <= y.Serial) : false;
        }

        /// <summary>
        /// 小なりイコールoperator。左辺のみOSCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <=(OSVersion x, int y) { return x is not null ? x.Serial <= y : false; }

        /// <summary>
        /// 小なりイコールoperator。右辺のみOSCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator <=(int x, OSVersion y) { return y is not null ? x <= y.Serial : false; }

        #endregion
        #region >=

        /// <summary>
        /// 大なりイコールoperator。両方OSCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >=(OSVersion x, OSVersion y)
        {
            return x is not null && y is not null ? ((x.Name == y.Name || x.OSFamily == OSFamily.Any || y.OSFamily == OSFamily.Any) && x.Serial >= y.Serial) : false;
        }

        /// <summary>
        /// 大なりイコールoperator。左辺のみOSCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >=(OSVersion x, int y) { return x is not null ? x.Serial >= y : false; }

        /// <summary>
        /// 大なりイコールoperator。右辺のみOSCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator >=(int x, OSVersion y) { return y is not null ? x >= y.Serial : false; }

        #endregion
        #region ==

        /// <summary>
        /// == operator。両方OSCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator ==(OSVersion x, OSVersion y)
        {
            if (x is not null && y is not null) { return (x.Name == y.Name || x.OSFamily == OSFamily.Any || y.OSFamily == OSFamily.Any) && x.Serial == y.Serial; }
            if (x is null && y is null) { return true; }
            return false;
        }

        /// <summary>
        /// == operator。左辺がCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator ==(OSVersion x, int y) { return x is not null ? x.Serial == y : false; }

        /// <summary>
        /// == operator。右辺がCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator ==(int x, OSVersion y) { return y is not null ? x == y.Serial : false; }

        #endregion
        #region !=

        /// <summary>
        /// != operator。両方OSCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator !=(OSVersion x, OSVersion y)
        {
            if (x is not null && y is not null) { return (x.Name == y.Name || x.OSFamily == OSFamily.Any || y.OSFamily == OSFamily.Any) || x.Serial != y.Serial; }
            if (x is null && y is null) { return false; }
            return true;
        }

        /// <summary>
        /// != operator。左辺がCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator !=(OSVersion x, int y) { return x is not null ? x.Serial != y : true; }

        /// <summary>
        /// != operator。右辺がCompareインスタンス
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator !=(int x, OSVersion y) { return y is not null ? x != y.Serial : true; }

        #endregion

        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            return obj switch
            {
                OSVersion o => this.Serial == o.Serial,
                int i => this.Serial == i,
                long l => this.Serial == l,
                _ => false,
            };
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool IsMatch(string keyword)
        {
            if (this.Name.Equals(keyword, StringComparison.OrdinalIgnoreCase) ||
                (this.Alias?.Any(x => keyword.Equals(x, StringComparison.OrdinalIgnoreCase)) ?? false))
            {
                return true;
            }
            if (this.VersionName == keyword) return true;
            if (this.VersionAlias?.Any(x => x.Equals(keyword, StringComparison.OrdinalIgnoreCase)) ?? false) return true;
            if ((keyword.StartsWith(this.Name) || (this.Alias?.Any(x => keyword.StartsWith(x)) ?? false)) &&
                (keyword.EndsWith(this.VersionName) || (this.VersionAlias?.Any(x => keyword.EndsWith(x)) ?? false))) return true;
            return false;
        }

        public override string ToString()
        {
            return $"{Name} {Edition} [ver {VersionName}]";
        }

        #endregion
    }
}
