﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Service.Bank {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class OperationNames {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal OperationNames() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Service.Bank.OperationNames", typeof(OperationNames).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Odebrano przelew zewnętrzny.
        /// </summary>
        internal static string BookExternalTransferCommand {
            get {
                return ResourceManager.GetString("BookExternalTransferCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wpłata.
        /// </summary>
        internal static string DepositCommand {
            get {
                return ResourceManager.GetString("DepositCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Opłata bankowa.
        /// </summary>
        internal static string ExternalTransferChargeCommand {
            get {
                return ResourceManager.GetString("ExternalTransferChargeCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Przelew zewnętrzny.
        /// </summary>
        internal static string ExternalTransferCommand {
            get {
                return ResourceManager.GetString("ExternalTransferCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Przelew wewnętrzny.
        /// </summary>
        internal static string InternalTransferCommand {
            get {
                return ResourceManager.GetString("InternalTransferCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wypłata.
        /// </summary>
        internal static string WithdrawCommand {
            get {
                return ResourceManager.GetString("WithdrawCommand", resourceCulture);
            }
        }
    }
}
