﻿// T4resx.tt
//
// T4 template to generate classes for easy localization
//
// Requires SmartFormat and AutoRunCustomTool
//
// usage:
//     copy contents of this file to T4resx.tt in your resources project
//     set resx file build action to embedded resource
//     set custom tool to blank
//     set run custom tool to T4resx.tt



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
	public static class Enums
	{
		#region Resource Manager
		
		private static global::System.Resources.ResourceManager resourceMan;
	
		/// <summary> Returns the cached ResourceManager instance used by this class. </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static global::System.Resources.ResourceManager ResourceManager 
		{
			get
			{
				if (object.ReferenceEquals(Enums.resourceMan, null))
				{
					global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Localized.Enums", typeof(Enums).Assembly);
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
		
		/// <summary> Create </summary>
		public static string DataTableButtonType_Create { get { return GetResourceString("DataTableButtonType_Create"); } }
		/// <summary> Delete </summary>
		public static string DataTableButtonType_Delete { get { return GetResourceString("DataTableButtonType_Delete"); } }
		/// <summary> Details </summary>
		public static string DataTableButtonType_Details { get { return GetResourceString("DataTableButtonType_Details"); } }
		/// <summary> Edit </summary>
		public static string DataTableButtonType_Edit { get { return GetResourceString("DataTableButtonType_Edit"); } }
		/// <summary> Manage </summary>
		public static string DataTableButtonType_Manage { get { return GetResourceString("DataTableButtonType_Manage"); } }
		/// <summary> Administrator </summary>
		public static string RoleType_Admin { get { return GetResourceString("RoleType_Admin"); } }
		/// <summary> None </summary>
		public static string RoleType_None { get { return GetResourceString("RoleType_None"); } }
		/// <summary> Superuser </summary>
		public static string RoleType_Super { get { return GetResourceString("RoleType_Super"); } }
		
		#endregion Keys
		
		#region Key Names
		
		public static class KeyNames
		{
			public const string DataTableButtonType_Create = "DataTableButtonType_Create";
			public const string DataTableButtonType_Delete = "DataTableButtonType_Delete";
			public const string DataTableButtonType_Details = "DataTableButtonType_Details";
			public const string DataTableButtonType_Edit = "DataTableButtonType_Edit";
			public const string DataTableButtonType_Manage = "DataTableButtonType_Manage";
			public const string RoleType_Admin = "RoleType_Admin";
			public const string RoleType_None = "RoleType_None";
			public const string RoleType_Super = "RoleType_Super";
		}
		
		#endregion Key Names
	}
	

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
	

	[DebuggerNonUserCodeAttribute()]
	[CompilerGeneratedAttribute()]
	public static class Models
	{
		#region Resource Manager
		
		private static global::System.Resources.ResourceManager resourceMan;
	
		/// <summary> Returns the cached ResourceManager instance used by this class. </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static global::System.Resources.ResourceManager ResourceManager 
		{
			get
			{
				if (object.ReferenceEquals(Models.resourceMan, null))
				{
					global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Localized.Models", typeof(Models).Assembly);
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
		
		/// <summary> Confirm Password (from resx) </summary>
		public static string ConfirmPassword { get { return GetResourceString("ConfirmPassword"); } }
		/// <summary> Demo One Model (from resx) </summary>
		public static string DemoOneModel { get { return GetResourceString("DemoOneModel"); } }
		/// <summary> String Three (DemoOneModel) (from resx) </summary>
		public static string DemoOneModel_StringThree { get { return GetResourceString("DemoOneModel_StringThree"); } }
		/// <summary> Email (from resx) </summary>
		public static string Email { get { return GetResourceString("Email"); } }
		/// <summary> Password (from resx) </summary>
		public static string Password { get { return GetResourceString("Password"); } }
		/// <summary> Positive Number (from resx) </summary>
		public static string PositiveNumber { get { return GetResourceString("PositiveNumber"); } }
		/// <summary> Number between 10 and 20 (from resx) </summary>
		public static string RangedNumber { get { return GetResourceString("RangedNumber"); } }
		/// <summary> String Three (from resx) </summary>
		public static string StringThree { get { return GetResourceString("StringThree"); } }
		/// <summary> String Two (from resx) </summary>
		public static string StringTwo { get { return GetResourceString("StringTwo"); } }
		
		#endregion Keys
		
		#region Key Names
		
		public static class KeyNames
		{
			public const string ConfirmPassword = "ConfirmPassword";
			public const string DemoOneModel = "DemoOneModel";
			public const string DemoOneModel_StringThree = "DemoOneModel_StringThree";
			public const string Email = "Email";
			public const string Password = "Password";
			public const string PositiveNumber = "PositiveNumber";
			public const string RangedNumber = "RangedNumber";
			public const string StringThree = "StringThree";
			public const string StringTwo = "StringTwo";
		}
		
		#endregion Key Names
	}
	

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
	

	[DebuggerNonUserCodeAttribute()]
	[CompilerGeneratedAttribute()]
	public static class Validation
	{
		#region Resource Manager
		
		private static global::System.Resources.ResourceManager resourceMan;
	
		/// <summary> Returns the cached ResourceManager instance used by this class. </summary>
		[global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
		internal static global::System.Resources.ResourceManager ResourceManager 
		{
			get
			{
				if (object.ReferenceEquals(Validation.resourceMan, null))
				{
					global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Localized.Validation", typeof(Validation).Assembly);
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
		
		/// <summary> {0} does not match {1}. </summary>
		public static string Error_Compare { get { return GetResourceString("Error_Compare"); } }
		/// <summary> {0} is not a valid credit card number. </summary>
		public static string Error_CreditCard { get { return GetResourceString("Error_CreditCard"); } }
		/// <summary> {0} is not a valid email address. </summary>
		public static string Error_EmailAddress { get { return GetResourceString("Error_EmailAddress"); } }
		/// <summary> {0} is not a valid file. </summary>
		public static string Error_FileExtensions { get { return GetResourceString("Error_FileExtensions"); } }
		/// <summary> {0} can be at most {1} characters long. </summary>
		public static string Error_MaxLength { get { return GetResourceString("Error_MaxLength"); } }
		/// <summary> {0} can be at most {1}. </summary>
		public static string Error_MaxValue { get { return GetResourceString("Error_MaxValue"); } }
		/// <summary> {0} does not meet the password requirements. </summary>
		public static string Error_MembershipPassword { get { return GetResourceString("Error_MembershipPassword"); } }
		/// <summary> {0} must be at least {1} characters long. </summary>
		public static string Error_MinLength { get { return GetResourceString("Error_MinLength"); } }
		/// <summary> {0} must be at least {1}. </summary>
		public static string Error_MinValue { get { return GetResourceString("Error_MinValue"); } }
		/// <summary> {0} is not a valid phone number. </summary>
		public static string Error_Phone { get { return GetResourceString("Error_Phone"); } }
		/// <summary> {0} must be between {1} and {2}. </summary>
		public static string Error_Range { get { return GetResourceString("Error_Range"); } }
		/// <summary> {0} failed validation. </summary>
		public static string Error_RegularExpression { get { return GetResourceString("Error_RegularExpression"); } }
		/// <summary> {0} is required. </summary>
		public static string Error_Required { get { return GetResourceString("Error_Required"); } }
		/// <summary> {0} must be between {1} and {2} characters long. </summary>
		public static string Error_StringLength { get { return GetResourceString("Error_StringLength"); } }
		/// <summary> {0} can be at most {2} characters long. </summary>
		public static string Error_StringLengthMax { get { return GetResourceString("Error_StringLengthMax"); } }
		/// <summary> {0} is not a valid URL. </summary>
		public static string Error_Url { get { return GetResourceString("Error_Url"); } }
		
		#endregion Keys
		
		#region Key Names
		
		public static class KeyNames
		{
			public const string Error_Compare = "Error_Compare";
			public const string Error_CreditCard = "Error_CreditCard";
			public const string Error_EmailAddress = "Error_EmailAddress";
			public const string Error_FileExtensions = "Error_FileExtensions";
			public const string Error_MaxLength = "Error_MaxLength";
			public const string Error_MaxValue = "Error_MaxValue";
			public const string Error_MembershipPassword = "Error_MembershipPassword";
			public const string Error_MinLength = "Error_MinLength";
			public const string Error_MinValue = "Error_MinValue";
			public const string Error_Phone = "Error_Phone";
			public const string Error_Range = "Error_Range";
			public const string Error_RegularExpression = "Error_RegularExpression";
			public const string Error_Required = "Error_Required";
			public const string Error_StringLength = "Error_StringLength";
			public const string Error_StringLengthMax = "Error_StringLengthMax";
			public const string Error_Url = "Error_Url";
		}
		
		#endregion Key Names
	}
	

}

