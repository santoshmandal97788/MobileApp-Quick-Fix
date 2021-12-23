using System;
using System.Collections.Generic;
using System.Text;


[assembly: Xamarin.Forms.Dependency(typeof(QuickFix.ILocSettings))]
namespace QuickFix
{
    public interface ILocSettings
    {
        void OpenSettings();
        bool isGpsAvailable();
    }
}
