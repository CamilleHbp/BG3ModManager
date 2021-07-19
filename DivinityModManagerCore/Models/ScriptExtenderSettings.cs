﻿using DivinityModManager.Extensions;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DivinityModManager.Models
{
	[DataContract]
	public class ScriptExtenderSettings : ReactiveObject
	{
		[Reactive]
		public bool ExtenderIsAvailable { get; set; } = false;

		[Reactive]
		public bool ExtenderUpdaterIsAvailable { get; set; } = false;

		[Reactive]
		public int ExtenderVersion { get; set; } = -1;

		[SettingsEntry("Enable Extensions", "Make the Osiris extension functionality available ingame or in the editor.")]
		[Reactive][DataMember]
		[DefaultValue(true)]
		public bool EnableExtensions { get; set; } = true;

		[SettingsEntry("Create Console", "Creates a console window that logs extender internals. Mainly useful for debugging.")]
		[Reactive][DataMember]
		[DefaultValue(false)]
		public bool CreateConsole { get; set; } = false;

		[SettingsEntry("Log Working Story Errors", "Log errors during Osiris story compilation to a log file (LogFailedCompile).")]
		[Reactive][DataMember]
		[DefaultValue(true)]
		public bool LogFailedCompile { get; set; } = true;

		[SettingsEntry("Enable Osiris Logging", "Enable logging of Osiris activity (rule evaluation, queries, etc.) to a log file.")]
		[Reactive][DataMember]
		[DefaultValue(false)]
		public bool EnableLogging { get; set; } = false;

		[SettingsEntry("Log Script Compilation", "Log Osiris story compilation to a log file.")]
		[Reactive][DataMember]
		[DefaultValue(false)]
		public bool LogCompile { get; set; } = false;

		[SettingsEntry("Log Directory", "Directory where the generated Osiris logs will be stored. Default is Documents\\OsirisLogs")]
		[Reactive][DataMember]
		public string LogDirectory { get; set; } = "";

		[SettingsEntry("Log Runtime", "Log extender console and script output to a log file.")]
		[Reactive][DataMember]
		[DefaultValue(false)]
		public bool LogRuntime { get; set; } = false;

		[SettingsEntry("Disable Mod Validation", "Disable module hashing when loading modules. Speeds up mod loading with no drawbacks.")]
		[Reactive][DataMember]
		[DefaultValue(true)]
		public bool DisableModValidation { get; set; } = true;

		[SettingsEntry("Enable Achievements", "Re-enable achievements for modded games.")]
		[Reactive][DataMember]
		[DefaultValue(true)]
		public bool EnableAchievements { get; set; } = true;

		[SettingsEntry("Send Crash Reports", "Upload minidumps to the crash report collection server after a game crash.")]
		[Reactive][DataMember]
		[DefaultValue(true)]
		public bool SendCrashReports { get; set; } = true;

		[SettingsEntry("Enable Osiris Debugger", "Enables the Osiris debugger interface (vscode extension).", true)]
		[Reactive][DataMember]
		[DefaultValue(false)]
		public bool EnableDebugger { get; set; } = false;

		[SettingsEntry("Osiris Debugger Port", "Port number the Osiris debugger will listen on (default 9999)", true)]
		[Reactive][DataMember]
		[DefaultValue(9999)]
		public int DebuggerPort { get; set; } = 9999;

		[SettingsEntry("Dump Network Strings", "Dumps the NetworkFixedString table to LogDirectory. Mainly useful for debugging desync issues.", true)]
		[Reactive][DataMember]
		[DefaultValue(false)]
		public bool DumpNetworkStrings { get; set; } = false;

		[SettingsEntry("Enable Developer Mode", "Enables various debug functionality for development purposes. This can be checked by mods to enable additional log messages and more.")]
		[Reactive][DataMember]
		[DefaultValue(false)]
		public bool DeveloperMode { get; set; } = false;

		[SettingsEntry("Enable Lua Debugger", "Enables the Lua debugger interface (vscode extension).", true)]
		[Reactive][DataMember]
		[DefaultValue(false)]
		public bool EnableLuaDebugger { get; set; } = false;

		public static ScriptExtenderSettings DefaultSettings = new ScriptExtenderSettings();

		public void SetToDefault()
		{
			PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this.GetType());
			foreach (PropertyDescriptor pr in props)
			{
				if (pr.CanResetValue(this))
				{
					pr.ResetValue(this);
				}
			}
		}

		public void Set(ScriptExtenderSettings osirisExtenderSettings)
		{
			EnableExtensions = osirisExtenderSettings.EnableExtensions;
			CreateConsole = osirisExtenderSettings.CreateConsole;
			EnableLogging = osirisExtenderSettings.EnableLogging;
			LogCompile = osirisExtenderSettings.LogCompile;
			if (osirisExtenderSettings.LogDirectory.IsExistingDirectory()) LogDirectory = osirisExtenderSettings.LogDirectory;
			DisableModValidation = osirisExtenderSettings.DisableModValidation;
			EnableAchievements = osirisExtenderSettings.EnableAchievements;
			SendCrashReports = osirisExtenderSettings.SendCrashReports;
			EnableDebugger = osirisExtenderSettings.EnableDebugger;
			DebuggerPort = osirisExtenderSettings.DebuggerPort;
			DeveloperMode = osirisExtenderSettings.DeveloperMode;
		}
	}
}
