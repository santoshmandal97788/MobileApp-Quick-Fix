//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("QuickFix.Views.LoginPage.xaml", "Views/LoginPage.xaml", typeof(global::QuickFix.Views.LoginPage))]

namespace QuickFix.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\LoginPage.xaml")]
    public partial class LoginPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::QuickFix.CustomRenderer.CustomEntry EmailEntry;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::QuickFix.CustomRenderer.CustomEntry PasswordEntry;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.CheckBox rebemberMe;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label signInLabel;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(LoginPage));
            EmailEntry = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::QuickFix.CustomRenderer.CustomEntry>(this, "EmailEntry");
            PasswordEntry = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::QuickFix.CustomRenderer.CustomEntry>(this, "PasswordEntry");
            rebemberMe = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.CheckBox>(this, "rebemberMe");
            signInLabel = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "signInLabel");
        }
    }
}
