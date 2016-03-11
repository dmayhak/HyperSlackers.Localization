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
	public static class Exceptions
	{
		#region Resource Manager
		
		private static global::System.Resources.ResourceManager resourceMan;
	
		/// <summary> Returns the cached ResourceManager instance used by this class. </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static global::System.Resources.ResourceManager ResourceManager 
		{
			get
			{
				if (object.ReferenceEquals(Exceptions.resourceMan, null))
				{
					global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Localized.Exceptions", typeof(Exceptions).Assembly);
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
		
		/// <summary> Cannot create a duplicate {itemName}. </summary>
		public static string CannotCreateDuplicate_Item(string itemName) { return Smart.Format(GetResourceString("CannotCreateDuplicate_Item"), new { itemName = itemName }); }
		/// <summary> {itemName} already exists. </summary>
		public static string Item_AlreadyExists(string itemName) { return Smart.Format(GetResourceString("Item_AlreadyExists"), new { itemName = itemName }); }
		/// <summary> {itemName} not found. </summary>
		public static string Item_NotFound(string itemName) { return Smart.Format(GetResourceString("Item_NotFound"), new { itemName = itemName }); }
		
		#endregion Keys
		
		#region Key Names
		
		public static class KeyNames
		{
			///<summary> PARAMS:{itemName} </summary>
			public const string CannotCreateDuplicate_Item = "CannotCreateDuplicate_Item";
			///<summary> PARAMS:{itemName} </summary>
			public const string Item_AlreadyExists = "Item_AlreadyExists";
			///<summary> PARAMS:{itemName} </summary>
			public const string Item_NotFound = "Item_NotFound";
		}
		
		#endregion Key Names
	}
	

}

