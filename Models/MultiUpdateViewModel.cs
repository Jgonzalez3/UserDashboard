using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserDashboard.Models{
    public class MultiUpdateViewModel{
        public UpdateViewModel UserUpdateInfo {get; set;}
        public PasswordUpdateViewModel UserUpdatePW {get; set;}
    }
}