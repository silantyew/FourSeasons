﻿#pragma checksum "..\..\..\View\CheckInWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "37D7F6D9A5F03FA191EAAA9BECC98A1CAEFE95FF3EDAF4D083C48E5D24FBE075"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FourSeasons.View;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Converters;
using Xceed.Wpf.Toolkit.Core;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Mag.Converters;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace FourSeasons.View {
    
    
    /// <summary>
    /// CheckInWindow
    /// </summary>
    public partial class CheckInWindow : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label headLb;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label nameLb;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label clientsLb;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label roomLb;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label inWLb;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label inLb;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label outWLb;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label outLb;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label servicesLb;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox serviceComboBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label costLb;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label paymentLb;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image passportHint;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid bookstk;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox passportTxb;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label payLb;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.IntegerUpDown payUpDown;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnToCheckIn;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCompleteCheckIn;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\View\CheckInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCompleteCheckOut;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HotelFarAwayHome;component/view/checkinwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\CheckInWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.headLb = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.nameLb = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.clientsLb = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.roomLb = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.inWLb = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.inLb = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.outWLb = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.outLb = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.servicesLb = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.serviceComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 11:
            this.costLb = ((System.Windows.Controls.Label)(target));
            return;
            case 12:
            this.paymentLb = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            this.passportHint = ((System.Windows.Controls.Image)(target));
            return;
            case 14:
            this.bookstk = ((System.Windows.Controls.Grid)(target));
            return;
            case 15:
            this.passportTxb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 16:
            this.payLb = ((System.Windows.Controls.Label)(target));
            return;
            case 17:
            this.payUpDown = ((Xceed.Wpf.Toolkit.IntegerUpDown)(target));
            return;
            case 18:
            this.btnToCheckIn = ((System.Windows.Controls.Button)(target));
            return;
            case 19:
            this.btnCompleteCheckIn = ((System.Windows.Controls.Button)(target));
            return;
            case 20:
            this.btnCompleteCheckOut = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
