﻿#pragma checksum "C:\Users\Tara\documents\visual studio 2015\Projects\SafeNote\SafeNote\EnterPassword.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6C24D19D48E83338CFCD49CE85412419"
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
    partial class EnterPassword : 
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
                    this.PreviewControl = (global::Windows.UI.Xaml.Controls.CaptureElement)(target);
                }
                break;
            case 2:
                {
                    this.outputBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 3:
                {
                    this.Camera = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 61 "..\..\..\EnterPassword.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.Camera).Click += this.Camera_Click;
                    #line default
                }
                break;
            case 4:
                {
                    this.passwordBox = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                    #line 55 "..\..\..\EnterPassword.xaml"
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
                    this.login = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 57 "..\..\..\EnterPassword.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.login).Click += this.login_Click;
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

