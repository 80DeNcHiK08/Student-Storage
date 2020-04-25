using System.Reflection;

#pragma warning disable CS0436
[assembly: AssemblyTitle(AssemblyInfo.Constants.TITLE)]
[assembly: AssemblyProduct(AssemblyInfo.Constants.DESCRIPTION)]
[assembly: AssemblyCopyright(AssemblyInfo.Constants.COPYRIGHT)]
[assembly: AssemblyCompany(AssemblyInfo.Constants.COMPANY)]
[assembly: AssemblyVersion(AssemblyInfo.Constants.VERSION)]
#pragma warning restore CS0436

namespace AssemblyInfo
{
	public static class Constants 
	{
        public const string TITLE = "Student Storage";
        public const string DESCRIPTION = "Program for collection data about students, groups and faculties";
        public const string COPYRIGHT = "Copyright © 2020 Miroshnichenko Denys";
        public const string COMPANY = "Oles' Honchar Dnipropetrovsk National University";
        public const string VERSION = "0.0.*";
	}
}
