﻿#pragma checksum "C:\Users\Tara\Documents\Visual Studio 2015\Projects\SafeNote\SafeNote\Settings.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C35CB1FD32DD097B3456C4D1314D32AD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SafeNote
{
    partial class Settings : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.cameraButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 48 "..\..\..\Settings.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.cameraButton).Click += this.Camera_Click;
                    #line default
                }
                break;
            case 2:
                {
                    this.image = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 3:
                {
                    this.imageErrorBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4:
                {
                    this.passwordBox = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                    #line 59 "..\..\..\Settings.xaml"
                    ((global::Windows.UI.Xaml.Controls.PasswordBox)this.passwordBox).PasswordChanged += this.passwordBox_PasswordChanged;
                    #line default
                }
                break;
            case 5:
                {
                    this.passwordErrorBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6:
                {
                    this.checkPasswordBox = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                    #line 63 "..\..\..\Settings.xaml"
                    ((global::Windows.UI.Xaml.Controls.PasswordBox)this.checkPasswordBox).PasswordChanged += this.checkPasswordBox_PasswordChanged;
                    #line default
                }
                break;
            case 7:
                {
                    this.checkPasswordErrorBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 8:
                {
                    this.submit = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 67 "..\..\..\Settings.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.submit).Click += this.submit_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

