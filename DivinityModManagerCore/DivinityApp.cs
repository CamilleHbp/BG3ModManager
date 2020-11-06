﻿using DivinityModManager.Models;
using DivinityModManager.Util;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DivinityModManager
{
	public static class DivinityApp
	{
		public const string DIR_DATA = "Data\\";
		public const string URL_REPO = @"https://github.com/LaughingLeader/BG3ModManager";
		public const string URL_CHANGELOG = @"https://github.com/LaughingLeader/BG3ModManager/blob/master/CHANGELOG.md";
		public const string URL_CHANGELOG_RAW = @"https://raw.githubusercontent.com/LaughingLeader/BG3ModManager/master/CHANGELOG.md";
		public const string URL_UPDATE = @"https://raw.githubusercontent.com/LaughingLeader/BG3ModManager/master/Update.xml";
		public const string URL_AUTHOR = @"https://github.com/LaughingLeader";
		public const string URL_ISSUES = @"https://github.com/LaughingLeader/BG3ModManager/issues";
		public const string URL_LICENSE = @"https://github.com/LaughingLeader/BG3ModManager/blob/master/LICENSE";
		public const string URL_DONATION = @"https://ko-fi.com/laughingleader";

		public const string XML_MOD_ORDER_MODULE = @"<node id=""Module""><attribute id=""UUID"" value=""{0}"" type=""FixedString""/></node>";
		public const string XML_MODULE_SHORT_DESC = @"<node id=""ModuleShortDesc""><attribute id=""Folder"" value=""{0}"" type=""LSWString""/><attribute id=""MD5"" value=""{1}"" type=""LSString""/><attribute id=""Name"" value=""{2}"" type=""FixedString""/><attribute id=""UUID"" value=""{3}"" type=""FixedString"" /><attribute id=""Version"" value=""{4}"" type=""int32""/></node>";
		public const string XML_MOD_SETTINGS_TEMPLATE = @"<?xml version=""1.0"" encoding=""UTF-8""?><save><header version=""2""/><version major=""3"" minor=""6"" revision=""9"" build=""0""/><region id=""ModuleSettings""><node id=""root""><children><node id=""ModOrder""><children>{0}</children></node><node id=""Mods""><children>{1}</children></node></children></node></region></save>";
		
		public const string PATH_APP_FEATURES = @"Resources/AppFeatures.json";
		public const string PATH_DEFAULT_PATHWAYS = @"Resources/DefaultPathways.json";
		public const string PATH_IGNORED_MODS = @"Resources/IgnoredMods.json";

		public static HashSet<DivinityModData> IgnoredMods { get; set; } = new HashSet<DivinityModData>();
		public static HashSet<DivinityModData> IgnoredDependencyMods { get; set; } = new HashSet<DivinityModData>();

		public static DivinityGlobalCommands Commands { get; private set; } = new DivinityGlobalCommands();
		public static DivinityGlobalEvents Events { get; private set; } = new DivinityGlobalEvents();

		public static event PropertyChangedEventHandler StaticPropertyChanged;

		private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
		{
			StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
		}

		private static bool developerModeEnabled = false;

		public static bool DeveloperModeEnabled
		{
			get => developerModeEnabled;
			set 
			{ 
				developerModeEnabled = value;
				NotifyStaticPropertyChanged();
			}
		}

		public static IObservable<Func<DivinityModDependencyData, bool>> DependencyFilter { get; set; }

		public static string DateTimeColumnFormat { get; set; } = "MM/dd/yyyy";
		public static string DateTimeTooltipFormat { get; set; } = "MMMM dd, yyyy";
	}
}
