﻿#pragma checksum "C:\Users\Tara\Documents\Mobile App Dev 2\NoteSafe\Safe-Note\SafeNote\Notes.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F210CBE8BA7A5B9D8BBDA7459DB2FEA4"
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
    partial class Notes : 
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
                    this.MyScrollView = (global::Windows.UI.Xaml.Controls.ScrollViewer)(target);
                }
                break;
            case 2:
                {
                    this.listView = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 3:
                {
                    this.newTitle = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 23 "..\..\..\Notes.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.newTitle).TextChanged += this.newTitle_TextChanged;
                    #line default
                }
                break;
            case 4:
                {
                    this.newBody = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 24 "..\..\..\Notes.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.newBody).TextChanged += this.newBody_TextChanged;
                    #line default
                }
                break;
            case 5:
                {
                    this.addNote = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 25 "..\..\..\Notes.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.addNote).Click += this.addNote_Click;
                    #line default
                }
                break;
            case 6:
                {
                    this.settingsButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 15 "..\..\..\Notes.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.settingsButton).Click += this.settingsButton_Click;
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

