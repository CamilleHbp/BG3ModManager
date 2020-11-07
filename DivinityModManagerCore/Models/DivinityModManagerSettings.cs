﻿using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Windows.Input;
using DivinityModManager.Util;
using System.Reactive.Disposables;
using System.Reflection;
using Alphaleonis.Win32.Filesystem;

namespace DivinityModManager.Models
{
	[DataContract]
	public class DivinityModManagerSettings : ReactiveObject, IDisposable
	{
		private string gameDataPath = "";

		[DataMember]
		public string GameDataPath
		{
			get => gameDataPath;
			set 
			{
				if (value != gameDataPath) CanSaveSettings = true;
				this.RaiseAndSetIfChanged(ref gameDataPath, value);
			}
		}

		private string gameExecutable = "";

		[DataMember]
		public string GameExecutablePath
		{
			get => gameExecutable;
			set
			{
				if (value != gameExecutable) CanSaveSettings = true;
				this.RaiseAndSetIfChanged(ref gameExecutable, value);
			}
		}

		private bool gameStoryLogEnabled = false;

		[DataMember]
		public bool GameStoryLogEnabled
		{
			get => gameStoryLogEnabled;
			set
			{
				if (value != gameStoryLogEnabled) CanSaveSettings = true;
				this.RaiseAndSetIfChanged(ref gameStoryLogEnabled, value);
			}
		}

		private bool telemetryDisabled = false;

		[DataMember]
		public bool TelemetryDisabled
		{
			get => telemetryDisabled;
			set {
				if (value != telemetryDisabled) CanSaveSettings = true;
				this.RaiseAndSetIfChanged(ref telemetryDisabled, value); 
			}
		}

		private bool launchDX11 = false;

		[DataMember]
		public bool LaunchDX11
		{
			get => launchDX11;
			set {
				if (value != launchDX11) CanSaveSettings = true;
				this.RaiseAndSetIfChanged(ref launchDX11, value); 
			}
		}

		private string workshopPath = "";

		[DataMember]
		public string WorkshopPath
		{
			get => workshopPath;
			set 
			{
				if (value != workshopPath) CanSaveSettings = true;
				this.RaiseAndSetIfChanged(ref workshopPath, value);
			}
		}

		private string loadOrderPath = "Orders";

		[DataMember]
		public string LoadOrderPath
		{
			get => loadOrderPath;
			set 
			{
				if (value != loadOrderPath) CanSaveSettings = true;
				this.RaiseAndSetIfChanged(ref loadOrderPath, value); 
			}
		}

		private bool logEnabled = false;

		[DataMember]
		public bool LogEnabled
		{
			get => logEnabled;
			set
			{
				if (value != logEnabled) CanSaveSettings = true;
				this.RaiseAndSetIfChanged(ref logEnabled, value);
			}
		}

		private bool autoAddDependenciesWhenExporting = true;

		[DataMember]
		public bool AutoAddDependenciesWhenExporting
		{
			get => autoAddDependenciesWhenExporting;
			set
			{
				if (value != autoAddDependenciesWhenExporting) CanSaveSettings = true;
				this.RaiseAndSetIfChanged(ref autoAddDependenciesWhenExporting, value);
			}
		}

		private bool checkForUpdates = true;

		[DataMember]
		public bool CheckForUpdates
		{
			get => checkForUpdates;
			set { this.RaiseAndSetIfChanged(ref checkForUpdates, value); }
		}

		private long lastUpdateCheck = -1;

		[DataMember]
		public long LastUpdateCheck
		{
			get => lastUpdateCheck;
			set { this.RaiseAndSetIfChanged(ref lastUpdateCheck, value); }
		}

		private string lastOrder = "";

		[DataMember]
		public string LastOrder
		{
			get => lastOrder;
			set { this.RaiseAndSetIfChanged(ref lastOrder, value); }
		}

		private string lastLoadedOrderFilePath = "";

		[DataMember]
		public string LastLoadedOrderFilePath
		{
			get => lastLoadedOrderFilePath;
			set { this.RaiseAndSetIfChanged(ref lastLoadedOrderFilePath, value); }
		}

		private string lastExtractOutputPath = "";

		[DataMember]
		public string LastExtractOutputPath
		{
			get => lastExtractOutputPath;
			set { this.RaiseAndSetIfChanged(ref lastExtractOutputPath, value); }
		}

		private bool darkThemeEnabled = true;

		[DataMember]
		public bool DarkThemeEnabled
		{
			get => darkThemeEnabled;
			set { this.RaiseAndSetIfChanged(ref darkThemeEnabled, value); }
		}

		private OsiExtenderSettings extenderSettings;

		[DataMember]
		public OsiExtenderSettings ExtenderSettings
		{
			get => extenderSettings;
			set { this.RaiseAndSetIfChanged(ref extenderSettings, value); }
		}

		public string ExtenderLogDirectory
		{
			get
			{
				if (ExtenderSettings == null || String.IsNullOrWhiteSpace(ExtenderSettings.LogDirectory))
				{

					return Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "OsirisLogs");
				}
				return ExtenderSettings.LogDirectory;
			}
		}


		private DivinityGameLaunchWindowAction actionOnGameLaunch = DivinityGameLaunchWindowAction.None;

		[DataMember]
		public DivinityGameLaunchWindowAction ActionOnGameLaunch
		{
			get => actionOnGameLaunch;
			set { this.RaiseAndSetIfChanged(ref actionOnGameLaunch, value); }
		}

		private bool disableMissingModWarnings = false;

		[DataMember]
		public bool DisableMissingModWarnings
		{
			get => disableMissingModWarnings;
			set { this.RaiseAndSetIfChanged(ref disableMissingModWarnings, value); }
		}

		private bool disableWorkshopTagCheck = true;

		[DataMember]
		public bool DisableWorkshopTagCheck
		{
			get => disableWorkshopTagCheck;
			set { this.RaiseAndSetIfChanged(ref disableWorkshopTagCheck, value); }
		}

		private bool exportDefaultExtenderSettings = false;

		[DataMember]
		public bool ExportDefaultExtenderSettings
		{
			get => exportDefaultExtenderSettings;
			set
			{
				this.RaiseAndSetIfChanged(ref exportDefaultExtenderSettings, value);
			}

		}

		//Not saved for now

		private bool displayFileNames = false;

		public bool DisplayFileNames
		{
			get => displayFileNames;
			set { this.RaiseAndSetIfChanged(ref displayFileNames, value); }
		}

		private bool debugModeEnabled = false;

		[DataMember]
		public bool DebugModeEnabled
		{
			get => debugModeEnabled;
			set
			{
				this.RaiseAndSetIfChanged(ref debugModeEnabled, value);
				DivinityApp.DeveloperModeEnabled = value;
			}

		}

		private string gameLaunchParams = "";

		[DataMember]
		public string GameLaunchParams
		{
			get => gameLaunchParams;
			set { this.RaiseAndSetIfChanged(ref gameLaunchParams, value); }
		}

		private bool extenderTabIsVisible = false;

		public bool ExtenderTabIsVisible
		{
			get => extenderTabIsVisible;
			set { this.RaiseAndSetIfChanged(ref extenderTabIsVisible, value); }
		}

		public ICommand SaveSettingsCommand { get; set; }
		public ICommand OpenSettingsFolderCommand { get; set; }
		public ICommand ExportExtenderSettingsCommand { get; set; }
		public ICommand ResetExtenderSettingsToDefaultCommand { get; set; }
		public ICommand ClearWorkshopCacheCommand { get; set; }
		public ICommand AddLaunchParamCommand { get; set; }
		public ICommand ClearLaunchParamsCommand { get; set; }

		public CompositeDisposable Disposables { get; internal set; }

		private bool canSaveSettings = false;

		public bool CanSaveSettings
		{
			get => canSaveSettings;
			set { this.RaiseAndSetIfChanged(ref canSaveSettings, value); }
		}

		public bool SettingsWindowIsOpen { get; set; } = false;

		public void Dispose()
		{
			Disposables?.Dispose();
			Disposables = null;
		}

		public DivinityModManagerSettings()
		{
			Disposables = new CompositeDisposable();
			ExtenderSettings = new OsiExtenderSettings();

			var properties = typeof(DivinityModManagerSettings)
			.GetRuntimeProperties()
			.Where(prop => Attribute.IsDefined(prop, typeof(DataMemberAttribute)))
			.Select(prop => prop.Name)
			.ToArray();

			this.WhenAnyPropertyChanged(properties).Subscribe((c) =>
			{
				if (SettingsWindowIsOpen) CanSaveSettings = true;
			}).DisposeWith(Disposables);

			var extender_properties = typeof(OsiExtenderSettings)
			.GetRuntimeProperties()
			.Where(prop => Attribute.IsDefined(prop, typeof(DataMemberAttribute)))
			.Select(prop => prop.Name)
			.ToArray();

			ExtenderSettings.WhenAnyPropertyChanged(extender_properties).Subscribe((c) =>
			{
				if (SettingsWindowIsOpen) CanSaveSettings = true;
				this.RaisePropertyChanged("ExtenderLogDirectory");
			}).DisposeWith(Disposables);
		}
	}
}
