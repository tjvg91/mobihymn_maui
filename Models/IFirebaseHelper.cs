using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobiHymnMaui.Models
{
	public interface IFirebaseHelper
	{
        Task LoginWithEmailPassword(string email, string password);
    }
}

