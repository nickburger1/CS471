﻿#pragma checksum "..\..\..\AddNewDemographicsInfo.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5FEF70CE4E1299DB5CDB1E5EE3E9A17C7B7768E5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using C_FGMS.UI;
using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Expression.Media;
using HandyControl.Expression.Shapes;
using HandyControl.Interactivity;
using HandyControl.Media.Animation;
using HandyControl.Media.Effects;
using HandyControl.Properties.Langs;
using HandyControl.Themes;
using HandyControl.Tools;
using HandyControl.Tools.Converter;
using HandyControl.Tools.Extension;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace C_FGMS.UI {
    
    
    /// <summary>
    /// AddNewDemographicsInfo
    /// </summary>
    public partial class AddNewDemographicsInfo : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\AddNewDemographicsInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grdCategory;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\AddNewDemographicsInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rdoGender;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\AddNewDemographicsInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rdoIdentifiesAs;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\AddNewDemographicsInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rdoEthnicity;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\AddNewDemographicsInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rdoRacialGroup;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\AddNewDemographicsInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\AddNewDemographicsInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDelete;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\AddNewDemographicsInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HandyControl.Controls.TextBox txtItem;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\AddNewDemographicsInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HandyControl.Controls.CheckComboBox cmbSelectItems;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/C_FGMS.UI;V1.0.0.0;component/addnewdemographicsinfo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AddNewDemographicsInfo.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.grdCategory = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.rdoGender = ((System.Windows.Controls.RadioButton)(target));
            
            #line 43 "..\..\..\AddNewDemographicsInfo.xaml"
            this.rdoGender.Checked += new System.Windows.RoutedEventHandler(this.rdoGender_Checked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.rdoIdentifiesAs = ((System.Windows.Controls.RadioButton)(target));
            
            #line 45 "..\..\..\AddNewDemographicsInfo.xaml"
            this.rdoIdentifiesAs.Checked += new System.Windows.RoutedEventHandler(this.rdoIdentifiesAs_Checked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.rdoEthnicity = ((System.Windows.Controls.RadioButton)(target));
            
            #line 47 "..\..\..\AddNewDemographicsInfo.xaml"
            this.rdoEthnicity.Checked += new System.Windows.RoutedEventHandler(this.rdoEthnicity_Checked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.rdoRacialGroup = ((System.Windows.Controls.RadioButton)(target));
            
            #line 50 "..\..\..\AddNewDemographicsInfo.xaml"
            this.rdoRacialGroup.Checked += new System.Windows.RoutedEventHandler(this.rdoRacialGroup_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\AddNewDemographicsInfo.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnDelete = ((System.Windows.Controls.Button)(target));
            
            #line 65 "..\..\..\AddNewDemographicsInfo.xaml"
            this.btnDelete.Click += new System.Windows.RoutedEventHandler(this.btnDelete_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.txtItem = ((HandyControl.Controls.TextBox)(target));
            return;
            case 9:
            this.cmbSelectItems = ((HandyControl.Controls.CheckComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

