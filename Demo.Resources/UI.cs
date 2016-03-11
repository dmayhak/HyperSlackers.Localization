// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Host.TemplateFile = C:\Dev\HyperSlackers\HyperSlackers.Localization\Demo.Resources\T4resx.tt

// Make sure the compiler doesn't complain about missing XML comments
#pragma warning disable 1591

namespace Localized
{
	using SmartFormat;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Linq;
	using System.Reflection;
	using System.Runtime.CompilerServices;
	using System.Text;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Web;
	using System.Web.Mvc;

	[DebuggerNonUserCodeAttribute()]
	[CompilerGeneratedAttribute()]
	public static class UI
	{
		#region Resource Manager
		
		private static global::System.Resources.ResourceManager resourceMan;
	
		/// <summary> Returns the cached ResourceManager instance used by this class. </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static global::System.Resources.ResourceManager ResourceManager 
		{
			get
			{
				if (object.ReferenceEquals(UI.resourceMan, null))
				{
					global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Localized.UI", typeof(UI).Assembly);
					// ???? global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("", typeof().Assembly);
					resourceMan = temp;
				}
			
				return resourceMan;
			}
		}
		
		/// <summary> Returns the formatted resource string. </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		private static string GetResourceString(string key)
		{
			var culture = Thread.CurrentThread.CurrentCulture;
			var str = ResourceManager.GetString(key, culture);
			
			return str;
		}
		
		/// <summary> Returns the formatted resource string. </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		private static HtmlString GetResourceHtmlString(string key)
		{
			var str = GetResourceString(key);
			
			return new HtmlString(str);
		}
		
		#endregion Resource Manager
		
		#region Keys
		
		/// <summary> &lt;h1&gt;HyperSlackers Localization Demo&lt;/h1&gt;
		///	    &lt;p class="lead"&gt;This simple demo program shows what our localization library does for you.&lt;/p&gt;
		///	    &lt;p&gt;&lt;a href="http://www.hyperslackers.com" class="btn btn-primary btn-lg"&gt;Learn more &raquo;&lt;/a&gt;&lt;/p&gt; </summary>
		public static HtmlString Home_Index_Jumbotron { get { return GetResourceHtmlString("Home_Index_Jumbotron"); } }
		/// <summary> HyperSlackers Localization Demo Home Page </summary>
		public static string Home_Index_Title { get { return GetResourceString("Home_Index_Title"); } }
		
		#endregion Keys
		
		#region Key Names
		
		public static class KeyNames
		{
			///<summary> HTML: </summary>
			public const string Home_Index_Jumbotron = "Home_Index_Jumbotron";
			public const string Home_Index_Title = "Home_Index_Title";
		}
		
		#endregion Key Names
	}
	

}
