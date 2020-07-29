using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX1Utility
{
    public class ProfileSearcher
    {
        public string ProfileSearchByName(List<string> profileList, String profileName)
        {
            return profileList.Find(delegate(string profile) { return profile == profileName; });
        }

    }
}
